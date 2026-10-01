using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;
using GamePotUnity.SimpleJSON;
using System.IO;
using System.Text;
using System.Linq;
using Realtime.LITJson;


#if VUPLEX
using Vuplex.WebView;
using Vuplex.WebView.Demos;
#endif

#if UNITY_STANDALONE
using GamePotUnity.Standalone.Networking;

#if UNITY_STANDALONE_OSX
using GamePotUnity.Standalone.macOS;
#endif



namespace GamePotUnity.Standalone
{

    public class GamePotUnityPluginStandalone
    {
        #region Public Properties
        public static string MEMBER_ID { get; set; }
        public static string ADID { get { return _ADID; } }

        public static string PROJECT_ID { get; set; }
        public static string TOKEN { get; set; }
        public static string STORE { get; set; }
        public static string REGION {get; set; }
        #endregion

        #region Private Properties
        private static bool _initialized = false;
        private static string _ADID = "";
        private static bool _Beta = false;

        // changeProjectId 관련
        private static string _originalProjectId = "";
        private const string PREF_KEY_CHANGED_PROJECT_ID = "gamepot_changed_project_id";
        private const string PREF_KEY_CHANGED_REGION = "gamepot_changed_region";

#if UNITY_STANDALONE_OSX
        private static JSONNode _initializeData = null;
        private static List<string> _itemListApple = new List<string>();
        private static bool _getPurchaseDetailListInProgress = false;
#endif
        #endregion


        /// <summary>
        /// initPlugin - SDK 초기화
        /// </summary>
        /// <param name="isReinitialize">재초기화 여부 (changeProjectId 후 호출 시 true)</param>
        public static void initPlugin(bool isReinitialize = false)
        {

            if (isReinitialize)
            {
                _initialized = false;
                TOKEN = "";
                MEMBER_ID = "";
                GamePotSettings.MemberInfo = null;  // 유저 정보 리셋 (Android setting = new GamePotSetting()과 동일)
                // Note: PROJECT_ID, REGION은 changeProjectId에서 이미 변경됨
            }

            string json = null;
            string filePath = null;

            string dataPath = Application.dataPath;
            string contentsPath = dataPath;

            if (dataPath.EndsWith("/Resources/Data"))
            {
                contentsPath = Path.GetDirectoryName(Path.GetDirectoryName(dataPath));
            }

            string path1 = Path.Combine(contentsPath, "GamePotStandalone_Config.json");

            string path2 = Path.Combine(contentsPath, "Resources", "Data", "GamePotStandalone_Config.json");

            string path3 = Path.Combine(contentsPath, "Resources", "Data", "StreamingAssets", "GamePotStandalone_Config.json");

            if (File.Exists(path1)) filePath = path1;
            else if (File.Exists(path2)) filePath = path2;
            else if (File.Exists(path3)) filePath = path3;

            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                 json = File.ReadAllText(filePath);
                 Debug.Log($"[GamePotStandalone] Config loaded successfully from: {filePath}");
            }
            else
            {
                Debug.LogError($"[GamePotStandalone] GamePotStandalone_Config.json not found in any of the expected locations:\n1. {path1}\n2. {path2}\n3. {path3}");
                return;
            }

            JSONNode config = JSONNode.Parse(json);

            // 재초기화가 아닌 경우에만 Config에서 값 로드
            if (!isReinitialize)
            {
                PROJECT_ID = config["PROJECT_ID"];
#if UNITY_STANDALONE_OSX
                STORE = "mac";
#else
                STORE = config["STORE"];
#endif
                REGION = config["REGION"];

                // changeProjectId로 변경된 값이 있는지 확인
                string changedProjectId = PlayerPrefs.GetString(PREF_KEY_CHANGED_PROJECT_ID, "");
                string changedRegion = PlayerPrefs.GetString(PREF_KEY_CHANGED_REGION, "");

                if (!string.IsNullOrEmpty(changedProjectId))
                {
                    PROJECT_ID = changedProjectId;
                }

                if (!string.IsNullOrEmpty(changedRegion))
                {
                    REGION = changedRegion;
                }
            }
            else
            {
                // 재초기화 시에도 STORE는 Config에서 로드
#if UNITY_STANDALONE_OSX
                STORE = "mac";
#else
                STORE = config["STORE"];
#endif
            }

            GraphQLRequest.setup(config["API_URL"]);

            //Config 환경변수 confirm
            if (ReferenceEquals(PROJECT_ID, null) || string.IsNullOrEmpty(PROJECT_ID))
            {
                Debug.LogError("[GamePotStandalone] initPlugin : Project ID needs to set in GamePotConfig!!");
                return;
            }

            if (ReferenceEquals(STORE, null) || string.IsNullOrEmpty(STORE))
            {
                Debug.LogError("[GamePotStandalone] initPlugin : STORE Value needs to set in GamePotConfig!!");
                return;
            }

            // 싱글톤 객체는 이미 있으면 재사용 (재초기화 시에도 유지)
            if (GamePotEventListener.s_instance == null)
            {
                new GameObject("GamePotStandaloneManager", typeof(GamePotEventListener));
            }
            else if (!isReinitialize)
            {
                // 최초 초기화인데 이미 싱글톤이 있으면 리턴 (기존 동작 유지)
                Debug.Log("[GamePotStandalone] initPlugin: Already initialized, skipping...");
                return;
            }

            GamePotStandalone.Initialize(PROJECT_ID, STORE);
            
#if UNITY_STANDALONE_OSX
            GamePotStandalone.SetVersionCodeProvider(() => GetCurrentVersionCode());
#endif

            //necessary instance initialized
            if (GamePotWebViewManager.s_instance == null)
            {
                Debug.Log("GamePot - Creating GamePotWebViewManager");
                GamePotWebViewManager.initialize();
            }

            //get ADID (async) - 재초기화 시에도 갱신
            Application.RequestAdvertisingIdentifierAsync((string adid, bool trackingEnabled, string error) =>
            {
                _ADID = adid;
            });

            GamePotChat.initialize();

            //먼저 shared 탐색. -> 없으면 포워딩
            string server_url = _Beta ? PlayerPrefs.GetString("gamepot_domain_beta", "") : PlayerPrefs.GetString("gamepot_domain", "");
            string socket_url = _Beta ? PlayerPrefs.GetString("gamepot_socket_beta", "") : PlayerPrefs.GetString("gamepot_socket", "");

            // 재초기화 시에는 항상 캐시 무시 (changeProjectId에서 캐시 클리어됨)
            if (isReinitialize)
            {
                server_url = "";
                socket_url = "";
            }
            else
            {
                // 최초 초기화 시 리전 변경 여부 확인
                string changedRegion = PlayerPrefs.GetString(PREF_KEY_CHANGED_REGION, "");
                bool hasRegionChanged = !string.IsNullOrEmpty(changedRegion);
                if (hasRegionChanged)
                {
                    server_url = "";
                    socket_url = "";
                }
            }

            if (string.IsNullOrEmpty(server_url.Trim()) || string.IsNullOrEmpty(socket_url.Trim()))
            {
                //캐싱된 주소가 없을 때 (또는 리전 변경으로 캐시 무시됨)
                RefreshForwardURL((bool success) =>
                {
                    if (success)
                    {
                        server_url = _Beta ? PlayerPrefs.GetString("gamepot_domain_beta", "") : PlayerPrefs.GetString("gamepot_domain", "");
                        socket_url = _Beta ? PlayerPrefs.GetString("gamepot_socket_beta", "") : PlayerPrefs.GetString("gamepot_socket", "");
                        GraphQLRequest.setup(string.IsNullOrEmpty(config["API_URL"]) ? server_url : (string)config["API_URL"]);
                        _initialized = true;

#if UNITY_STANDALONE_OSX
                        FetchInitializeData();
#endif
                    }
                    else
                    {
                        Debug.LogError("[GamePotUnityStandalone] Failed to initPlugin()!");
                    }
                });
            }
            else
            {
                //string opt_server_url = string.IsNullOrEmpty(config["API_URL"]) ? server_url : (string)config["API_URL"];
                GraphQLRequest.setup(string.IsNullOrEmpty(config["API_URL"]) ? server_url : (string)config["API_URL"]);

                //Refresh Forward Url Again
                RefreshForwardURL((bool success) =>
                {
                    if (success)
                    {
                        string _f_url = _Beta ? PlayerPrefs.GetString("gamepot_domain_beta", "") : PlayerPrefs.GetString("gamepot_domain", "");

                        if (string.IsNullOrEmpty(config["API_URL"]) && (_f_url != server_url))
                        {
                            GraphQLRequest.setup(_f_url);
                        }
                    }
                    else
                    {
                        Debug.LogError("[GamePotUnityStandalone] Failed to Refresh Forward Url!");
                    }
                });

                _initialized = true;

                //Debug.Log($"[GamePotStandalone] initPlugin: Initialization completed (cached server_url: {server_url})");

#if UNITY_STANDALONE_OSX
                FetchInitializeData();
#endif
            }

#if UNITY_EDITOR
            UnityInterConnector interconnector = GamePotChat.s_unityInterconnector;
            interconnector.mainThread = System.Threading.Thread.CurrentThread;
#endif

        }

        /// <summary>
        /// changeProjectId - 런타임 중 프로젝트 ID 변경 (Android API 호환)
        /// </summary>
        /// <param name="projectId">새로운 프로젝트 ID</param>
        /// <param name="sensPushServiceId">SENS Push Service ID (Standalone에서는 미사용, API 호환용)</param>
        public static void changeProjectId(string projectId, string sensPushServiceId)
        {
            changeProjectId(projectId, sensPushServiceId, "");
        }

        /// <summary>
        /// changeProjectId - 런타임 중 프로젝트 ID 및 리전 변경 (Android API 호환)
        /// </summary>
        /// <param name="projectId">새로운 프로젝트 ID</param>
        /// <param name="sensPushServiceId">SENS Push Service ID (Standalone에서는 미사용, API 호환용)</param>
        /// <param name="region">리전 (kr, sg, jp, eu, usa)</param>
        public static void changeProjectId(string projectId, string sensPushServiceId, string region)
        {
            // 1. 원본 projectId 백업
            _originalProjectId = PROJECT_ID;

            // 2. 새 값 설정
            PROJECT_ID = projectId;
            if (!string.IsNullOrEmpty(region))
            {
                REGION = region;
            }

            // 3. PlayerPrefs에 저장 (앱 재시작 시 유지)
            PlayerPrefs.SetString(PREF_KEY_CHANGED_PROJECT_ID, projectId);
            if (!string.IsNullOrEmpty(region))
            {
                PlayerPrefs.SetString(PREF_KEY_CHANGED_REGION, region);
            }
            PlayerPrefs.Save();

            // 4. 캐시된 URL 클리어 (리전 변경에 대응)
            if (_Beta)
            {
                PlayerPrefs.DeleteKey("gamepot_domain_beta");
                PlayerPrefs.DeleteKey("gamepot_socket_beta");
            }
            else
            {
                PlayerPrefs.DeleteKey("gamepot_domain");
                PlayerPrefs.DeleteKey("gamepot_socket");
            }
            PlayerPrefs.Save();

            // 5. 전체 재초기화 수행 (Android setup() 재호출과 동일)
            initPlugin(true);
        }

        /// <summary>
        /// 원본 프로젝트 ID 반환
        /// </summary>
        public static string getOriginalProjectId()
        {
            return _originalProjectId;
        }

        /// <summary>
        /// 저장된 changeProjectId 값 초기화 (원래 설정으로 복원 시 사용)
        /// </summary>
        public static void clearChangedProjectId()
        {
            PlayerPrefs.DeleteKey(PREF_KEY_CHANGED_PROJECT_ID);
            PlayerPrefs.DeleteKey(PREF_KEY_CHANGED_REGION);
            PlayerPrefs.Save();
        }

#if UNITY_STANDALONE_OSX
        private static void FetchInitializeData()
        {
            Debug.Log("[GamePotUnityStandalone] FetchInitializeData - Fetching itemlist from server...");

            GraphQLRequest.initializeV2(PROJECT_ID, STORE, (bool success, JSONNode result) =>
            {
                if (success && result != null)
                {
                    _initializeData = result;
                    
                    try
                    {
                        JSONNode initV2 = result["initializeV2"];
                        
                        // Apple ID 추출 및 네이티브로 전달 (iOS와 동일한 경로)
                        if (initV2 != null && initV2["project"] != null && initV2["project"]["app_id"] != null)
                        {
                            string appleId = initV2["project"]["app_id"]["apple"];
                            if (!string.IsNullOrEmpty(appleId))
                            {
                                //Debug.Log($"[GamePotUnityStandalone] FetchInitializeData - Apple ID found: {appleId}");
                                GamePotMacNative.SetAppleId(appleId);
                            }
                            else
                            {
                                Debug.LogWarning("[GamePotUnityStandalone] FetchInitializeData - Apple ID is empty");
                            }
                        }
                        else
                        {
                            Debug.LogWarning("[GamePotUnityStandalone] FetchInitializeData - project.app_id.apple not found in response");
                        }
                        
                        if (initV2 != null && initV2["itemlist"] != null && initV2["itemlist"]["apple"] != null)
                        {
                            JSONArray appleItems = initV2["itemlist"]["apple"].AsArray;
                            _itemListApple.Clear();
                            
                            List<string> productIds = new List<string>();
                            foreach (JSONNode item in appleItems)
                            {
                                string storeItemId = item["store_item_id"];
                                if (!string.IsNullOrEmpty(storeItemId))
                                {
                                    _itemListApple.Add(storeItemId);
                                    productIds.Add(storeItemId);
                                }
                            }
                            
                            
                            if (productIds.Count > 0)
                            {
                                Debug.Log("[GamePotUnityStandalone] FetchInitializeData - Automatically fetching product info from store...");
                                fetchProducts(string.Join(",", productIds));
                            }
                        }
                        else
                        {
                            Debug.LogWarning("[GamePotUnityStandalone] FetchInitializeData - No itemlist.apple found in response");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"[GamePotUnityStandalone] FetchInitializeData - Error parsing itemlist: {ex.Message}");
                    }
                }
                else
                {
                    Debug.LogError("[GamePotUnityStandalone] FetchInitializeData - initializeV2 failed");
                }
            });
        }
#endif

        private static void ProcessPendingTransactionsAfterLogin()
        {
#if UNITY_STANDALONE_OSX
            Debug.Log("[GamePotUnityStandalone] ProcessPendingTransactionsAfterLogin - Checking for pending transactions...");
            
            try
            {
                if (HasPendingPurchase())
                {
                    Debug.Log("[GamePotUnityStandalone] ProcessPendingTransactionsAfterLogin - Found saved pending purchase info. Processing...");
                    JSONNode pendingInfo = LoadPendingPurchaseInfo();
                    if (pendingInfo != null)
                    {
                        string productId = pendingInfo["productId"] ?? "";
                        string transactionId = pendingInfo["transactionId"] ?? "";
                        string receipt = pendingInfo["receipt"] ?? "";
                        string priceStr = pendingInfo["price"] ?? "";
                        
                        bool hasRequiredFields = !string.IsNullOrEmpty(productId) 
                            && !string.IsNullOrEmpty(transactionId) 
                            && !string.IsNullOrEmpty(receipt)
                            && !string.IsNullOrEmpty(priceStr);
                        
                        if (hasRequiredFields)
                        {
                            ProcessPendingPurchaseDirectly(pendingInfo);
                            return; // 저장된 pending 정보 처리 후 종료
                        }
                        else
                        {
                            ClearPendingPurchaseInfo();
                        }
                    }
                }
                
                Debug.Log("[GamePotUnityStandalone] ProcessPendingTransactionsAfterLogin - Checking Native pending transactions...");
                GamePotMacNative.ProcessPendingTransactions();
            }
            catch (Exception ex)
            {
                Debug.LogError($"[GamePotUnityStandalone] ProcessPendingTransactionsAfterLogin - Error: {ex.Message}");
            }
#endif
        }

        private static void RefreshForwardURL(System.Action<bool> callback = null)
        {
            Debug.Log("[GamePotUnityStandalone] initPlugin() - Goto Forwarding Table..");

            string licenseUrl = _Beta ? "https://fw.gamepot.beta.ntruss.com/v1" : "https://gamepot.apigw.ntruss.com/fw/v1";

            if(REGION != null && REGION.ToLower() == "sg")
            {
                licenseUrl = "https://gamepot.apigw.ntruss.com/fw/sg-v1";
            }
            
            if(REGION != null && REGION.ToLower() == "jp")
            {
                licenseUrl = "https://gamepot.apigw.ntruss.com/fw/jp-v1";
            }

            if(REGION != null && REGION.ToLower() =="eu")
            {
                licenseUrl = "https://gamepot.apigw.ntruss.com/fw/de-v1";
            }

            if(REGION != null && REGION.ToLower() =="us")
            {
                licenseUrl = "https://z6e9mxbp5r.apigw.ntruss.com/fw/us-v1";
            }

            if(REGION != null && REGION.ToLower() =="beta")
            {
                licenseUrl = "https://fw.gamepot.beta.ntruss.com/v1";
            }

            //forwarding table
            WebRequest.RequestObject www
                = new WebRequest.RequestObject()
                {
                    mode = UnityWebRequest.kHttpVerbGET,
                    uri = licenseUrl + "/config",
                    header = new Dictionary<string, string>() { { "projectid", PROJECT_ID } },
                    callback = (long responseCode, string result) =>
                    {
#if GAMECHAT_DEBUG
                            Debug.LogFormat("Return from forwading Table : {0}", result);
#endif

                        if (responseCode != 200)
                        {
                            Debug.LogError("[GamePotUnityStandalone] RefreshForwardURL - Failed to Visit forwading Table - " + result);
                            if (callback != null) callback(false);
                            return;
                        }

                        JSONNode forwarding_url = JSONNode.Parse(result);
                        string server_url = "";
                        string socket_url = "";

                        try
                        {
                            server_url = forwarding_url["domain"];
                            socket_url = forwarding_url["socket"];
                            socket_url = socket_url.Replace("https://", "wss://");

                            //Debug.Log("[GamePotUnityStandalone] refresh server_url : " + server_url);
                            //Debug.Log("[GamePotUnityStandalone] refresh socket_url : " + socket_url);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError("[GamePotUnityStandalone] RefreshForwardURL - " + ex.ToString());
                            if (callback != null) callback(false);
                            return;
                        }

                        if (string.IsNullOrEmpty(server_url) || string.IsNullOrEmpty(socket_url))
                        {
                            Debug.LogErrorFormat("[GamePotUnityStandalone] RefreshForwardURL -  Failed to Refresh. server_url or socket_url is empty. ServerUrl : {0}, SocketUrl : {1}", server_url, socket_url);
                            if (callback != null) callback(false);
                            return;
                        }

                        //Refresh Caching
                        if (_Beta)
                        {
                            PlayerPrefs.SetString("gamepot_domain_beta", server_url);
                            PlayerPrefs.SetString("gamepot_socket_beta", socket_url);
                        }
                        else
                        {
                            PlayerPrefs.SetString("gamepot_domain", server_url);
                            PlayerPrefs.SetString("gamepot_socket", socket_url);
                        }

                        Debug.Log("[GamePotUnityStandalone] RefreshForwardURL - Forwarded Completed!");
                        if (callback != null) callback(true);
                    }
                };
            WebRequest.Http(www);
        }


        public static void login(NCommon.LoginType loginType)
        {
#if UNITY_STANDALONE_OSX
            if (loginType == NCommon.LoginType.APPLE)
            {
                Debug.Log("[GamePotStandalone] Starting Apple login flow");
                loginWithApple();
                return;
            }
#endif

            CheckAppStatusInternal((bool success, NError err) =>
            {
                GamePotEventListener listener = GamePotEventListener.s_instance;
                if (success)
                {
                    GraphQLRequest.setMember(PROJECT_ID, TOKEN, ADID, GamePotDeviceInfo.DeviceModel, GamePotDeviceInfo.NetworkType, GamePotDeviceInfo.DeviceOSVersion, GamePotDeviceInfo.DeviceModel);

                    ////Todo : need code renewal
                    string socket_url = _Beta ? PlayerPrefs.GetString("gamepot_socket_beta", "") : PlayerPrefs.GetString("gamepot_socket", "");
                    GamePotChat.setValue(PROJECT_ID, TOKEN, STORE);
                    // 항상 gsoc로 설정
                    socket_url = "wss://gsoc.gamepot.ntruss.com";
                    GamePotChat.setup(socket_url);    //CCU

                    listener.onLoginSuccess(GamePotSettings.MemberInfo.ToJson());
                    
#if UNITY_STANDALONE_OSX
                    ProcessPendingTransactionsAfterLogin();
#endif
                }
                else
                {
                    listener.onLoginFailure(err.ToJson());
                }
            });
        }


        public static void loginByThirdPartySDK(string providerId)
        {
            if (_initialized)
            {
                GraphQLRequest.signInV3(PROJECT_ID, STORE, NCommon.LoginType.THIRDPARTYSDK.ToString().ToLower(), providerId,
                    "", "",
                    (bool success, JSONNode result) =>
                    {
                        GamePotEventListener listener = GamePotEventListener.s_instance;
                        if (listener == null)
                        {
                            Debug.LogError("[GamePotStandalone] signInV3 - GamePotEventListener not initialized");
                            return;
                        }

                        if (success)
                        {
                            //GamePotSettings.MemberInfo = new NUserInfo();
                            GamePotSettings.MemberInfo = JsonMapper.ToObject<NUserInfo>(result.ToString());
                            login(NCommon.LoginType.THIRDPARTYSDK);
                        }
                        else
                        {
                            NError err = new NError(result);
                            listener.onLoginFailure(err.ToJson());
                        }
                    });
            }
        }

        /// <summary>
        /// macOS: CheckAppStatus
        /// Windows/Linux: maintenanceAppV2
        /// </summary>
        public static void checkAppStatus()
        {
#if UNITY_STANDALONE_OSX
            // macOS: CheckAppStatus
            if (string.IsNullOrEmpty(PROJECT_ID))
            {
                Debug.LogError("[GamePotStandalone] checkAppStatus - PROJECT_ID is not initialized");
                GamePotCallbackDelegate.CB_CheckAppStatus callback = GamePotEventListener.cbCheckAppStatus;
                if (callback != null)
                {
                    NError error = new NError { code = NError.CODE_NOT_INITALIZE, message = "PROJECT_ID is not initialized" };
                    callback(NCommon.ResultCheckAppStatus.FAILED, null, error);
                }
                else
                {
                    Debug.LogError("[GamePotStandalone] checkAppStatus - cbCheckAppStatus is null");
                }
                return;
            }

            GamePotStandalone.CheckAppStatus(PROJECT_ID, STORE,
                (GamePotUnity.Standalone.Models.ResultCheckAppStatus dllStatus,
                 GamePotUnity.Standalone.Models.NAppStatus dllAppStatus,
                 string errorMessage) =>
            {
                NCommon.ResultCheckAppStatus status = (NCommon.ResultCheckAppStatus)dllStatus;

                if (dllStatus == GamePotUnity.Standalone.Models.ResultCheckAppStatus.FAILED)
                {
                    Debug.LogError($"[GamePotStandalone] checkAppStatus failed: {errorMessage}");
                    NError error = new NError { code = NError.CODE_UNKNOWN_ERROR, message = errorMessage };
                    GamePotCallbackDelegate.CB_CheckAppStatus callback = GamePotEventListener.cbCheckAppStatus;
                    callback?.Invoke(status, null, error);
                    return;
                }

                NAppStatus unityAppStatus = ConvertDllAppStatusToUnity(dllAppStatus);

                // MAINTENANCE 처리
                if (dllStatus == GamePotUnity.Standalone.Models.ResultCheckAppStatus.MAINTENANCE)
                {
                    GamePotCallbackDelegate.CB_CheckAppStatus callback = GamePotEventListener.cbCheckAppStatus;
                    callback?.Invoke(status, unityAppStatus, null);
                    return;
                }

                // NEED_UPDATE 처리
                if (dllStatus == GamePotUnity.Standalone.Models.ResultCheckAppStatus.NEED_UPDATE)
                {
                    GamePotCallbackDelegate.CB_CheckAppStatus callback = GamePotEventListener.cbCheckAppStatus;
                    callback?.Invoke(status, unityAppStatus, null);
                    return;
                }

                // SUCCESS
                Debug.Log("[GamePotStandalone] App status: Normal");
                GamePotCallbackDelegate.CB_CheckAppStatus callback2 = GamePotEventListener.cbCheckAppStatus;
                callback2?.Invoke(status, unityAppStatus, null);
            });
#else
            // Windows/Linux: maintenanceAppV2 사용
            if (!_initialized)
            {
                Debug.LogError("[GamePotStandalone] checkAppStatus: Plugin not initialized");
                NError error = new NError { code = NError.CODE_NOT_INITALIZE, message = "Plugin not initialized" };
                GamePotCallbackDelegate.CB_CheckAppStatus callback = GamePotEventListener.cbCheckAppStatus;
                callback?.Invoke(NCommon.ResultCheckAppStatus.FAILED, null, error);
                return;
            }

            NUserInfo user_info = GamePotSettings.MemberInfo;
            if (user_info == null)
            {
                Debug.LogError("[GamePotStandalone] checkAppStatus: MemberInfo is null. User must login first.");
                NError error = new NError { code = NError.CODE_MEMBERID_IS_EMPTY, message = "User not logged in" };
                GamePotCallbackDelegate.CB_CheckAppStatus callback = GamePotEventListener.cbCheckAppStatus;
                callback?.Invoke(NCommon.ResultCheckAppStatus.FAILED, null, error);
                return;
            }

            MEMBER_ID = user_info.memberid;
            TOKEN = user_info.token;

            GraphQLRequest.maintenanceAppV2(PROJECT_ID, TOKEN, STORE,
                (bool success, JSONNode result) =>
                {
                    if (success)
                    {
                        if (result != null)
                        {
                            // ON_MAINTENANCE
                            GamePotCallbackDelegate.CB_CheckAppStatus callback = GamePotEventListener.cbCheckAppStatus;
                            callback?.Invoke(NCommon.ResultCheckAppStatus.MAINTENANCE, null, null);
                            return;
                        }
                        GamePotCallbackDelegate.CB_CheckAppStatus callback = GamePotEventListener.cbCheckAppStatus;
                        callback?.Invoke(NCommon.ResultCheckAppStatus.SUCCESS, null, null);
                    }
                    else
                    {
                        GamePotCallbackDelegate.CB_CheckAppStatus callback = GamePotEventListener.cbCheckAppStatus;
                        callback?.Invoke(NCommon.ResultCheckAppStatus.FAILED, null, new NError(result););
                    }
                });
#endif
        }

        /// <summary>
        /// 내부용 checkAppStatus (login에서 호출)
        /// maintenanceAppV2 사용
        /// </summary>
        private static void CheckAppStatusInternal(GamePotCallbackDelegate.CB_Common callback)
        {
            if (!_initialized)
            {
                Debug.LogError("[GamePotStandalone] CheckAppStatusInternal: Plugin not initialized");
                callback(false, new NError { code = NError.CODE_NOT_INITALIZE, message = "Plugin not initialized" });
                return;
            }

            NUserInfo user_info = GamePotSettings.MemberInfo;
            if (user_info == null)
            {
                Debug.LogError("[GamePotStandalone] CheckAppStatusInternal: MemberInfo is null");
                callback(false, new NError { code = NError.CODE_MEMBERID_IS_EMPTY, message = "User not logged in" });
                return;
            }

            MEMBER_ID = user_info.memberid;
            TOKEN = user_info.token;

            //Debug.LogFormat("[GamePotStandalone] CheckAppStatusInternal - Member id: {0}", MEMBER_ID);
            //Debug.LogFormat("[GamePotStandalone] CheckAppStatusInternal - TOKEN: {0}", TOKEN);

            GraphQLRequest.maintenanceAppV2(PROJECT_ID, TOKEN, STORE,
                (bool success, JSONNode result) =>
                {
                    GamePotEventListener listener = GamePotEventListener.s_instance;
                    if (listener == null)
                    {
                        Debug.LogError("[GamePotStandalone] CheckAppStatusInternal - GamePotEventListener not initialized");
                        callback(false, new NError { code = NError.CODE_UNKNOWN_ERROR, message = "GamePotEventListener not initialized" });
                        return;
                    }

                    if (success)
                    {
                        // ON_MAINTENANCE
                        if (result != null)
                        {
                            listener.onMainternance(result.ToString());
                            return;
                        }
                        // SUCCESS
                        callback(true);
                    }
                    else
                    {
                        callback(false, new NError(result));
                    }
                });
        }

        /// <summary>
        /// DLL NAppStatus를 Unity NAppStatus로 변환
        /// </summary>
        private static NAppStatus ConvertDllAppStatusToUnity(GamePotUnity.Standalone.Models.NAppStatus dllAppStatus)
        {
            if (dllAppStatus == null) return null;

            return new NAppStatus
            {
                type = dllAppStatus.type,
                message = dllAppStatus.message,
                url = dllAppStatus.url,
                currentAppVersion = dllAppStatus.currentAppVersion,
                updateAppVersion = dllAppStatus.updateAppVersion,
                currentAppVersionCode = dllAppStatus.currentAppVersionCode,
                updateAppVersionCode = dllAppStatus.updateAppVersionCode,
                isForce = dllAppStatus.isForce,
                startedAt = dllAppStatus.startedAt,
                endedAt = dllAppStatus.endedAt
            };
        }

        /// <summary>
        /// CFBundleVersion 가져오기 (macOS)
        /// </summary>
        private static int GetCurrentVersionCode()
        {
#if UNITY_STANDALONE_OSX
            try
            {
                return GamePotMacNative.GetCurrentVersionCode();
            }
            catch (Exception ex)
            {
                Debug.LogError($"[GamePotStandalone] Failed to get version code from native: {ex.Message}");
                return 0;
            }
#else
            return 0;
#endif
        }

        public static void showWebView(string _url, Dictionary<string, string> _header = null)
        {
            if (GamePotWebViewManager.s_instance == null)
            {
                Debug.LogError("[GamePotUnityStandalone] showCSWebView - GamePotUnityStandalone is not initialized!!");
                return;
            }

#if VUPLEX
            GamePotWebViewManager webview_mgr = GamePotWebViewManager.s_instance;
            webview_mgr._webview = CanvasWebViewPrefab.Instantiate().gameObject;

            GameObject _frame = webview_mgr.transform.GetChild(0).gameObject;
            webview_mgr._webview.transform.parent = _frame.transform;
            webview_mgr._webview.transform.localPosition = new Vector3(0, 0, 0);
            webview_mgr._webview.transform.localScale = new Vector3(1, 1, 1);
            RectTransform rt = webview_mgr._webview.GetComponent<RectTransform>();
            rt.offsetMin = new Vector2(0, 0);
            rt.offsetMax = new Vector2(0, 0);


            CanvasWebViewPrefab curWebView  = webview_mgr._webview.gameObject.GetComponent<CanvasWebViewPrefab>();

            if (_header != null)
            {
                curWebView.Initialized += (initializedSender, initializedEventArgs) =>
                {
                    curWebView.WebView.LoadUrl(_url, _header);
                    curWebView.WebView.MessageEmitted += (object sender, EventArgs<string> eventArgs)=>
                    {
                        var json = eventArgs.Value;
                    };
                };
            }
            else
            {
                curWebView.Initialized += (initializedSender, initializedEventArgs) =>
                {
                    curWebView.WebView.LoadUrl(_url);
                    curWebView.WebView.MessageEmitted += (object sender, EventArgs<string> eventArgs) =>
                    {
                        var json = eventArgs.Value;
                    };
                };
            }
#endif

            GamePotWebViewManager.showWebView();
        }

        public static void showCSWebView()
        {
            // if (GamePotWebViewManager.s_instance == null)
            // {
            //     Debug.LogError("[GamePotUnityStandalone] showCSWebView GamePotUnityStandalone is not initialized!!");
            //     return;
            // }
            // GamePotWebViewManager.s_instance.showWebView("https://gsrpkjibrmls4086645.gcdn.ntruss.com/demo/cs/question?projectid=ab2775b4-cf09-4794-9480-decd607a7f8a&store=google&memberid=4e125b06-462f-4c9f-8dbe-b1447bc9e370&device=android&sdkversion=2.1.2&language=ko");
        }

        public static void showFaq()
        {
            // if (GamePotWebViewManager.s_instance == null)
            // {
            //     Debug.LogError("[GamePotUnityStandalone] showCSWebView GamePotUnityStandalone is not initialized!!");
            //     return;
            // }
            // GamePotWebViewManager.s_instance.showWebView("https://gsrpkjibrmls4086645.gcdn.ntruss.com/demo/cs/faq?projectid=ab2775b4-cf09-4794-9480-decd607a7f8a&store=google&memberid=4e125b06-462f-4c9f-8dbe-b1447bc9e370&device=android&sdkversion=2.1.2&language=ko");
        }

        public static void showNoticeWebView()
        {
            if (GamePotWebViewManager.s_instance == null)
            {
                Debug.LogError("[GamePotUnityStandalone] showCSWebView GamePotUnityStandalone is not initialized!!");
                return;
            }
            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("projectid", PROJECT_ID);
            header.Add("store", STORE);
            showWebView(GraphQLRequest.BASE_URL + "/notice", header);
        }

        public static void coupon(string couponNumber, string userData)
        {
            if (_initialized)
            {
                GraphQLRequest.useCoupon(PROJECT_ID, TOKEN, couponNumber, STORE, userData,
                    (bool success, JSONNode result) =>
                    {
                        GamePotEventListener listener = GamePotEventListener.s_instance;
                        if (listener == null)
                        {
                            Debug.LogError("[GamePotStandalone] maintenanceAppV2 - GamePotEventListener not initialized");
                            return;
                        }

                        if (success)
                        {
                            listener.onCouponSuccess(result.ToString());
                        }
                        else
                        {
                            NError err = new NError(result);
                            listener.onCouponFailure(err.ToJson());
                        }
                    });
            }
        }

        public static bool characterInfo(string _data, GamePotCallbackDelegate.CB_Common callback = null)
        {
            if (_initialized)
            {
                JSONNode parsed = JSONNode.Parse(_data);
                parsed.Add("project_id", PROJECT_ID);
                parsed.Add("user_id", MEMBER_ID);

                //Wrapping JSON Object
                JSONNode _body = new JSONObject();
                _body.Add("body", parsed);
                JSONArray _arr_body = new JSONArray();
                _arr_body.Add(_body);
                JSONNode _opt_body = JSONNode.Parse(_arr_body.ToString());

                Dictionary<string, string> _header = new Dictionary<string, string>();
                _header.Add("Content-Type", "application/json");
                _header.Add("x-api-key", TOKEN);

                string end_point = GraphQLRequest.BASE_URL + "/v1/objects/GamePlayerProfile";

                NetworkListener networkListener = new NetworkListener()
                {
                    onNetworkSuccess = (JSONNode data) =>
                    {
                        if (data["status"] != null && data["status"] == 1)
                        {
                            if (callback != null) callback(true);
                            return;
                        }
                        //status code 1 반환 안됐을 때,
                        if (callback != null) callback(false, new NError { code = NError.CODE_UNKNOWN_ERROR, message = data.ToString() });
                    },
                    onNetworkFailure = (JSONNode error) =>
                    {
                        if (callback != null) callback(false, new NError(error));
                    }
                };

                WebRequest.LoadRequest(end_point, 2, networkListener, _header, _opt_body);
                return true;
            }
            return false;
        }


#if UNITY_STANDALONE_OSX
        #region MACOS NATIVE FEATURES

        private static GamePotMacCallbackReceiver _macCallbackReceiver;
        private static bool _macFeaturesInitialized;
        
        private static List<NPurchaseItem> _cachedPurchaseItems = new List<NPurchaseItem>();
        
        private static bool _purchaseInProgress = false;
        private static string _currentPurchaseProductId;
        private static string _currentPurchaseUniqueId;
        private static string _currentPurchaseServerId;
        private static string _currentPurchasePlayerId;
        private static string _currentPurchaseEtc;
        private static string _currentPurchaseUserData;
        
        private const string PREF_KEY_PENDING_PURCHASE = "gamepot_pending_purchase";

        private static void EnsureMacNative()
        {
            if (!_macFeaturesInitialized)
            {
                InitMacOSNativeFeatures();
                _macFeaturesInitialized = true;
            }
        }
        
        
        private static void SavePendingPurchaseInfo(string productId, string transactionId, string receipt,
            string price, string currency, string country, string productName,
            string uniqueId, string serverId, string playerId, string etc)
        {
            try
            {
                JSONObject pendingInfo = new JSONObject();
                pendingInfo["productId"] = productId;
                pendingInfo["transactionId"] = transactionId;
                pendingInfo["receipt"] = receipt;
                pendingInfo["price"] = price;
                pendingInfo["currency"] = currency;
                pendingInfo["country"] = country;
                pendingInfo["productName"] = productName;
                pendingInfo["uniqueId"] = uniqueId;
                pendingInfo["serverId"] = serverId;
                pendingInfo["playerId"] = playerId;
                pendingInfo["etc"] = etc;
                pendingInfo["savedAt"] = DateTime.UtcNow.ToString("o");
                
                string jsonStr = pendingInfo.ToString();
                PlayerPrefs.SetString(PREF_KEY_PENDING_PURCHASE, jsonStr);
                PlayerPrefs.Save();
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"[GamePotStandalone] Failed to save pending purchase info: {ex.Message}");
            }
        }
        
        private static JSONNode LoadPendingPurchaseInfo()
        {
            try
            {
                string jsonStr = PlayerPrefs.GetString(PREF_KEY_PENDING_PURCHASE, "");
                if (!string.IsNullOrEmpty(jsonStr))
                {
                    JSONNode pendingInfo = JSONNode.Parse(jsonStr);
                    return pendingInfo;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[GamePotStandalone] Failed to load pending purchase info: {ex.Message}");
            }
            return null;
        }
        
        private static void ClearPendingPurchaseInfo()
        {
            PlayerPrefs.DeleteKey(PREF_KEY_PENDING_PURCHASE);
            PlayerPrefs.Save();
            Debug.Log("[GamePotStandalone] Cleared pending purchase info");
        }
        
        private static bool HasPendingPurchase()
        {
            return !string.IsNullOrEmpty(PlayerPrefs.GetString(PREF_KEY_PENDING_PURCHASE, ""));
        }

        private static void InitMacOSNativeFeatures()
        {
            if (_macCallbackReceiver == null)
            {
                GameObject receiverObj = new GameObject("GamePotMacCallbackReceiver", typeof(GamePotMacCallbackReceiver));
                GameObject.DontDestroyOnLoad(receiverObj);
                _macCallbackReceiver = receiverObj.GetComponent<GamePotMacCallbackReceiver>();

                GamePotMacNative.InitAppleSignIn("GamePotMacCallbackReceiver");
                GamePotMacNative.InitStoreKit("GamePotMacCallbackReceiver");

                GamePotMacNative.SetFetchProductsSuccessCallback(new GamePotMacNative.NativeCallback(OnMacOSFetchProductsSuccess));
                GamePotMacNative.SetFetchProductsFailureCallback(new GamePotMacNative.NativeCallback(OnMacOSFetchProductsFailure));
                GamePotMacNative.SetPurchaseSuccessCallback(new GamePotMacNative.NativeCallback(OnMacOSPurchaseSuccess));
                GamePotMacNative.SetPurchaseFailureCallback(new GamePotMacNative.NativeCallback(OnMacOSPurchaseFailure));
                GamePotMacNative.SetPurchaseDeferredCallback(new GamePotMacNative.NativeCallback(OnMacOSPurchaseDeferred));
                GamePotMacNative.SetRestorePurchaseSuccessCallback(new GamePotMacNative.NativeCallback(OnMacOSRestorePurchaseSuccess));
                GamePotMacNative.SetRestorePurchaseFailureCallback(new GamePotMacNative.NativeCallback(OnMacOSRestorePurchaseFailure));
                GamePotMacNative.SetRestorePurchasesCompleteCallback(new GamePotMacNative.NativeCallback(OnMacOSRestorePurchasesComplete));

                GamePotMacNative.SetAppleSignInSuccessCallback(new GamePotMacNative.NativeCallback(OnNativeAppleSignInSuccess));
                GamePotMacNative.SetAppleSignInFailureCallback(new GamePotMacNative.NativeCallback(OnNativeAppleSignInFailure));

                GamePotStandalone.OnAppleLoginSuccess = (standaloneUserInfo) => 
                {
                    NUserInfo userInfo = new NUserInfo
                    {
                        memberid = standaloneUserInfo.memberid,
                        name = standaloneUserInfo.name,
                        profileUrl = standaloneUserInfo.profileUrl,
                        email = standaloneUserInfo.email,
                        token = standaloneUserInfo.token,
                        userid = standaloneUserInfo.userid,
                        rawData = standaloneUserInfo.rawData
                    };
                    OnAppleLoginSuccess(userInfo);
                };
                
                GamePotStandalone.OnAppleLoginFailure = (standaloneError) => 
                {
                    NError error = new NError
                    {
                        code = standaloneError.code,
                        message = standaloneError.message
                    };
                    OnAppleLoginFailure(error);
                };

                GamePotStandalone.OnAppleLoginMaintenance = (maintenanceJson) => 
                {
                    // Maintenance detected during Apple login
                    Debug.LogError("[GamePotStandalone] App is under maintenance during Apple login");
                    GamePotEventListener listener = GamePotEventListener.s_instance;
                    if (listener != null)
                    {
                        listener.onMainternance(maintenanceJson);
                    }
                };
                
                GamePotStandalone.OnAppleLoginUpdate = (updateJson) => 
                {
                    // Update detected during Apple login
                    Debug.Log("[GamePotStandalone] App update available during Apple login");
                    GamePotEventListener listener = GamePotEventListener.s_instance;
                    if (listener != null)
                    {
                        listener.onNeedUpdate(updateJson);
                    }
                };

                Debug.Log("[GamePotStandalone] macOS native features initialized with callbacks");
            }

        }

        [AOT.MonoPInvokeCallback(typeof(GamePotMacNative.NativeCallback))]
        private static void OnNativeAppleSignInSuccess(string json)
        {

            try
            {
                JSONNode result = JSONNode.Parse(json);
                string identityToken = result["identityToken"];
                string user = result["user"];
                string email = result["email"];
                string fullName = result["fullName"];

                if (string.IsNullOrEmpty(user))
                {
                    Debug.LogError("[GamePotUnityStandalone] Apple user identifier is empty");
                    NError error = new NError { code = NError.CODE_MEMBERID_IS_EMPTY, message = "Apple user identifier is empty" };
                    OnAppleLoginFailure(error);
                    return;
                }

                if (string.IsNullOrEmpty(identityToken))
                {
                    Debug.LogError("[GamePotUnityStandalone] identityToken is empty");
                    NError error = new NError { code = NError.CODE_UNKNOWN_ERROR, message = "identityToken is empty" };
                    OnAppleLoginFailure(error);
                    return;
                }

                Debug.Log("[GamePotUnityStandalone] Delegating to GamePotStandalone.ProcessAppleLogin");
                GamePotStandalone.ProcessAppleLogin(user, email, fullName, identityToken);
            }
            catch (Exception ex)
            {
                Debug.LogError($"[GamePotUnityStandalone] Error parsing Apple Sign In result: {ex.Message}");
                NError error = new NError { code = NError.CODE_UNKNOWN_ERROR, message = ex.Message };
                OnAppleLoginFailure(error);
            }
        }

        [AOT.MonoPInvokeCallback(typeof(GamePotMacNative.NativeCallback))]
        private static void OnNativeAppleSignInFailure(string json)
        {
            Debug.LogError($"[GamePotUnityStandalone] Native Apple Sign In failure: {json}");
            
            try
            {
                JSONNode result = JSONNode.Parse(json);
                NError err = new NError
                {
                    code = result["errorCode"] != null ? result["errorCode"].AsInt : NError.CODE_UNKNOWN_ERROR,
                    message = result["errorMessage"] != null ? result["errorMessage"] : "Apple Sign In failed"
                };
                OnAppleLoginFailure(err);
            }
            catch
            {
                NError err = new NError { code = NError.CODE_UNKNOWN_ERROR, message = json };
                OnAppleLoginFailure(err);
            }
        }

        private static void SetupMacOSCallbacks()
        {
            if (_macCallbackReceiver == null) return;


            _macCallbackReceiver.OnFetchProductsSuccessCallback = OnMacOSFetchProductsSuccess;
            _macCallbackReceiver.OnFetchProductsFailureCallback = OnMacOSFetchProductsFailure;
            _macCallbackReceiver.OnPurchaseSuccessCallback = OnMacOSPurchaseSuccess;
            _macCallbackReceiver.OnPurchaseFailureCallback = OnMacOSPurchaseFailure;
            _macCallbackReceiver.OnPurchaseDeferredCallback = OnMacOSPurchaseDeferred;
            _macCallbackReceiver.OnRestorePurchaseSuccessCallback = OnMacOSRestorePurchaseSuccess;
            _macCallbackReceiver.OnRestorePurchaseFailureCallback = OnMacOSRestorePurchaseFailure;
            _macCallbackReceiver.OnRestorePurchasesCompleteCallback = OnMacOSRestorePurchasesComplete;
        }

        public static void loginWithApple(string nonce = null)
        {
            EnsureMacNative();
            if (!_initialized)
            {
                Debug.LogError("[GamePotStandalone] loginWithApple: Plugin not initialized. Call initPlugin first.");
                return;
            }

            Debug.Log("[GamePotStandalone] Starting Sign in with Apple...");
            GamePotMacNative.SignInWithApple(nonce);
        }

        private static void OnAppleLoginSuccess(NUserInfo userInfo)
        {
            
            GamePotSettings.MemberInfo = userInfo;
            
            //string socket_url = _Beta ? PlayerPrefs.GetString("gamepot_socket_beta", "") : PlayerPrefs.GetString("gamepot_socket", "");
            GamePotChat.setValue(PROJECT_ID, userInfo.token, STORE);
            string socket_url = "wss://gsoc.gamepot.ntruss.com"; // 항상 gsoc로 설정
            GamePotChat.setup(socket_url);    //CCU

            MEMBER_ID = userInfo.memberid;
            TOKEN = userInfo.token;

            GamePotEventListener listener = GamePotEventListener.s_instance;
            if (listener != null)
            {
                listener.onLoginSuccess(GamePotSettings.MemberInfo.ToJson());
            }
            
#if UNITY_STANDALONE_OSX
            ProcessPendingTransactionsAfterLogin();
#endif
        }

        private static void OnAppleLoginFailure(NError error)
        {
            Debug.LogError($"[GamePotStandalone] Apple login failure callback received: {error.message}");
            
            GamePotEventListener listener = GamePotEventListener.s_instance;
            if (listener != null)
            {
                listener.onLoginFailure(error.ToJson());
            }
        }

        public static void fetchProducts(string productIds)
        {
            EnsureMacNative();
            if (!_initialized)
            {
                Debug.LogError("[GamePotStandalone] fetchProducts: Plugin not initialized.");
                return;
            }

            if (!GamePotMacNative.CanMakePayments())
            {
                Debug.LogError("[GamePotStandalone] Payments are disabled on this device");
                GamePotEventListener listener = GamePotEventListener.s_instance;
                if (listener != null)
                {
                    NError err = new NError { code = NError.CODE_UNKNOWN_ERROR, message = "Payments are disabled" };
                    listener.onPurchaseFailure(err.ToJson());
                }
                return;
            }

            GamePotMacNative.FetchProducts(productIds);
        }

        public static void purchase(string productId, string uniqueId, string serverId, string playerId, string etc)
        {
            EnsureMacNative();
            if (!_initialized)
            {
                Debug.LogError("[GamePotStandalone] purchase: Plugin not initialized.");
                return;
            }

            if (_purchaseInProgress)
            {
                Debug.LogWarning("[GamePotStandalone] Purchase already in progress");
                return;
            }

            if (!GamePotMacNative.CanMakePayments())
            {
                Debug.LogError("[GamePotStandalone] Payments are disabled on this device");
                GamePotEventListener listener = GamePotEventListener.s_instance;
                if (listener != null)
                {
                    NError err = new NError { code = NError.CODE_UNKNOWN_ERROR, message = "Payments are disabled" };
                    listener.onPurchaseFailure(err.ToJson());
                }
                return;
            }

            _purchaseInProgress = true;
            _currentPurchaseProductId = productId;
            _currentPurchaseUniqueId = uniqueId ?? "";
            _currentPurchaseServerId = serverId ?? "";
            _currentPurchasePlayerId = playerId ?? "";
            _currentPurchaseEtc = etc ?? "";

            try
            {
                JSONNode userData = new JSONObject();
                userData.Add("unique_id", _currentPurchaseUniqueId);
                userData.Add("server_id", _currentPurchaseServerId);
                userData.Add("player_id", _currentPurchasePlayerId);
                userData.Add("etc", _currentPurchaseEtc);
                _currentPurchaseUserData = userData.ToString();
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[GamePotStandalone] Failed to create userData JSON: {e.Message}");
                _currentPurchaseUserData = "{}";
            }

            
            JSONNode pendingInfo = LoadPendingPurchaseInfo();
            if (pendingInfo != null && !string.IsNullOrEmpty(pendingInfo["transactionId"]))
            {
                string pendingProductId = pendingInfo["productId"] ?? "";
                string pendingTransactionId = pendingInfo["transactionId"] ?? "";
                string pendingReceipt = pendingInfo["receipt"] ?? "";
                string pendingPriceStr = pendingInfo["price"] ?? "";
                
                bool hasRequiredFields = !string.IsNullOrEmpty(pendingProductId) 
                    && !string.IsNullOrEmpty(pendingTransactionId) 
                    && !string.IsNullOrEmpty(pendingReceipt)
                    && !string.IsNullOrEmpty(pendingPriceStr) && pendingPriceStr != "0";
                
                if (hasRequiredFields)
                {
                    
                    ProcessPendingPurchaseDirectly(pendingInfo);
                    return;
                }
                else
                {
                    ClearPendingPurchaseInfo();
                }
            }

            GamePotStandalone.CheckPurchase(
                PROJECT_ID,
                TOKEN,
                productId,
                STORE, // storeId
                "apple", // paymentId
                _currentPurchaseUserData,
                (bool success, JSONNode result) =>
                {
                    if (success)
                    {
                        OnCheckPurchaseResult(true, result);
                    }
                    else
                    {
                        OnCheckPurchaseResult(false, result);
                    }
                }
            );
        }
        
        private static void ProcessPendingPurchaseDirectly(JSONNode pendingInfo)
        {
            string transactionId = pendingInfo["transactionId"] ?? "";
            string productId = pendingInfo["productId"] ?? _currentPurchaseProductId ?? "";
            string receipt = pendingInfo["receipt"] ?? "";
            string currency = pendingInfo["currency"] ?? "";
            string country = pendingInfo["country"] ?? "";
            string priceStr = pendingInfo["price"] ?? "0";
            string productName = pendingInfo["productName"] ?? "";
            
            bool hasRequiredFields = !string.IsNullOrEmpty(productId) 
                && !string.IsNullOrEmpty(transactionId) 
                && !string.IsNullOrEmpty(receipt)
                && !string.IsNullOrEmpty(priceStr) && priceStr != "0";
            
            if (!hasRequiredFields)
            {
                
                ClearPendingPurchaseInfo();
                
                GamePotMacNative.ProcessPendingTransactions();
                return;
            }
            
            string uniqueId = pendingInfo["uniqueId"] ?? _currentPurchaseUniqueId ?? "";
            string serverId = pendingInfo["serverId"] ?? _currentPurchaseServerId ?? "";
            string playerId = pendingInfo["playerId"] ?? _currentPurchasePlayerId ?? "";
            string etc = pendingInfo["etc"] ?? _currentPurchaseEtc ?? "";
            
            double price = 0;
            double.TryParse(priceStr, out price);
            
            JSONNode userData = new JSONObject();
            userData.Add("unique_id", uniqueId);
            userData.Add("server_id", serverId);
            userData.Add("player_id", playerId);
            userData.Add("etc", etc);
            string userDataJson = userData.ToString();
            
            
            GamePotStandalone.CreatePurchase(
                STORE, // storeId
                PROJECT_ID, // projectId
                TOKEN, // token
                transactionId, // orderId
                "",
                productId, // itemId
                receipt, // receipt
                "apple", // paymentId
                currency, // currency
                country, // country
                price, // price
                userDataJson, // userData
                (bool success, JSONNode result) =>
                {
                    if (success)
                    {
                        OnCreatePurchaseSuccess(transactionId, productId, productName, price.ToString(), currency, transactionId, uniqueId, serverId, playerId, etc, receipt, result);
                    }
                    else
                    {
                        OnCreatePurchaseFailure(transactionId, result);
                    }
                }
            );
        }

        private static void OnCheckPurchaseResult(bool success, JSONNode result)
        {
            if (!success)
            {
                Debug.LogError($"[GamePotStandalone] checkPurchase failed: {result?.ToString()}");
                int errorCode = NError.CODE_UNKNOWN_ERROR;
                string errorMessage = "Unknown error";
                if (result != null)
                {
                    if (result["code"] != null)
                    {
                        errorCode = result["code"].AsInt;
                    }
                    if (result["message"] != null)
                    {
                        errorMessage = result["message"].Value;
                    }
                }
                FailPurchase(errorCode, $"CheckPurchase error occurred: {errorMessage}");
                return;
            }


            JSONNode checkPurchaseData = result?["checkPurchase"];
            if (checkPurchaseData != null && checkPurchaseData["status"] != null)
            {
                int status = checkPurchaseData["status"].AsInt;
                if (status == 1)
                {
                    GamePotMacNative.FetchProducts(_currentPurchaseProductId);
                }
                else
                {
                    Debug.LogError($"[GamePotStandalone] checkPurchase failed with status: {status}");
                    string code = checkPurchaseData["code"]?.Value;
                    FailPurchase(status, $"CheckPurchase error occurred. (status={status}, code={code})");
                }
            }
            else
            {
                Debug.LogError("[GamePotStandalone] checkPurchase response has no status field");
                FailPurchase(NError.CODE_UNKNOWN_ERROR, "CheckPurchase error occurred. (no status)");
            }
        }

        private static void FailPurchase(string errorMessage)
        {
            FailPurchase(NError.CODE_UNKNOWN_ERROR, errorMessage);
        }

        private static void FailPurchase(int errorCode, string errorMessage)
        {
            _purchaseInProgress = false;
            ClearPurchaseState();

            GamePotEventListener listener = GamePotEventListener.s_instance;
            if (listener != null)
            {
                NError err = new NError { code = errorCode, message = errorMessage };
                listener.onPurchaseFailure(err.ToJson());
            }
        }

        private static void ClearPurchaseState()
        {
            _currentPurchaseProductId = null;
            _currentPurchaseUniqueId = null;
            _currentPurchaseServerId = null;
            _currentPurchasePlayerId = null;
            _currentPurchaseEtc = null;
            _currentPurchaseUserData = null;
        }

        public static void purchaseProduct(string productId)
        {
            purchaseProduct(productId, null);
        }

        public static void purchaseProduct(string productId, string userDataJson)
        {
            EnsureMacNative();
            if (!_initialized)
            {
                Debug.LogError("[GamePotStandalone] purchaseProduct: Plugin not initialized.");
                return;
            }

            if (!GamePotMacNative.CanMakePayments())
            {
                Debug.LogError("[GamePotStandalone] Payments are disabled on this device");
                GamePotEventListener listener = GamePotEventListener.s_instance;
                if (listener != null)
                {
                    NError err = new NError { code = NError.CODE_UNKNOWN_ERROR, message = "Payments are disabled" };
                    listener.onPurchaseFailure(err.ToJson());
                }
                return;
            }

            GamePotMacNative.PurchaseProductWithUserData(productId, userDataJson);
        }

        public static void restorePurchases()
        {
            EnsureMacNative();
            if (!_initialized)
            {
                Debug.LogError("[GamePotStandalone] restorePurchases: Plugin not initialized.");
                return;
            }

            Debug.Log("[GamePotStandalone] Restoring purchases...");
            GamePotMacNative.RestorePurchases();
        }

        public static string getPurchaseItems()
        {
            EnsureMacNative();
            
            if (_cachedPurchaseItems == null || _cachedPurchaseItems.Count == 0)
            {
                Debug.LogWarning("[GamePotStandalone] getPurchaseItems: No cached items. Use fetchProducts to load product information first.");
                return "[]";
            }

            string json = JsonMapper.ToJson(_cachedPurchaseItems);
            return json;
        }

        public static void getPurchaseDetailListAsync()
        {
            Debug.Log("[GamePotStandalone] getPurchaseDetailListAsync called");
            
            if (!_initialized)
            {
                Debug.LogError("[GamePotStandalone] getPurchaseDetailListAsync: Not initialized");
                NError error = new NError { code = NError.CODE_NOT_INITALIZE, message = "GamePot is not initialized" };
                GamePotEventListener listener = GamePotEventListener.s_instance;
                if (listener != null)
                {
                    listener.onPurchaseDetailListFailure(error.ToJson());
                }
                return;
            }
            
            if (_itemListApple == null || _itemListApple.Count == 0)
            {
                Debug.LogWarning("[GamePotStandalone] getPurchaseDetailListAsync: No items in itemlist.apple");
                NError error = new NError { code = NError.CODE_UNKNOWN_ERROR, message = "productItem List is nil or size 0" };
                GamePotEventListener listener = GamePotEventListener.s_instance;
                if (listener != null)
                {
                    listener.onPurchaseDetailListFailure(error.ToJson());
                }
                return;
            }
            
            if (_cachedPurchaseItems != null && _cachedPurchaseItems.Count > 0)
            {
                string json = JsonMapper.ToJson(_cachedPurchaseItems);
                GamePotEventListener listener = GamePotEventListener.s_instance;
                if (listener != null)
                {
                    listener.onPurchaseDetailListSuccess(json);
                }
                return;
            }
            
            Debug.Log("[GamePotStandalone] getPurchaseDetailListAsync: Fetching products from store...");
            
            _getPurchaseDetailListInProgress = true;
            
            fetchProducts(string.Join(",", _itemListApple));
        }



        [AOT.MonoPInvokeCallback(typeof(GamePotMacNative.NativeCallback))]
        private static void OnMacOSFetchProductsSuccess(string json)
        {
            
            try
            {
                JSONNode productsNode = JSONNode.Parse(json);
                _cachedPurchaseItems.Clear();
                
                if (productsNode["products"] != null && productsNode["products"].IsArray)
                {
                    foreach (JSONNode productNode in productsNode["products"].AsArray)
                    {
                        NPurchaseItem item = new NPurchaseItem
                        {
                            productId = productNode["productId"],
                            price = productNode["price"],
                            price_amount = productNode["priceAmount"] != null ? productNode["priceAmount"].Value : "0",
                            price_amount_micros = productNode["priceAmountMicros"] != null ? productNode["priceAmountMicros"].Value : "0",
                            price_currency_code = productNode["currencyCode"],
                            title = productNode["title"],
                            description = productNode["description"]
                        };
                        _cachedPurchaseItems.Add(item);
                    }
                }
                
            }
            catch (Exception ex)
            {
                Debug.LogError($"[GamePotStandalone] Error parsing products: {ex.Message}");
                _cachedPurchaseItems.Clear();
            }
            
            if (_purchaseInProgress && !string.IsNullOrEmpty(_currentPurchaseProductId))
            {
                GamePotMacNative.PurchaseProductWithUserData(_currentPurchaseProductId, _currentPurchaseUserData);
                return; // 이벤트 리스너에는 결제 완료 후 전달
            }
            
            if (_getPurchaseDetailListInProgress)
            {
                _getPurchaseDetailListInProgress = false;
                
                string itemsJson = JsonMapper.ToJson(_cachedPurchaseItems);
                GamePotEventListener detailListListener = GamePotEventListener.s_instance;
                if (detailListListener != null)
                {
                    detailListListener.onPurchaseDetailListSuccess(itemsJson);
                }
                return;
            }
            
            GamePotEventListener listener = GamePotEventListener.s_instance;
            if (listener != null)
            {
                listener.onFetchProductsSuccess(json);
            }
        }

        [AOT.MonoPInvokeCallback(typeof(GamePotMacNative.NativeCallback))]
        private static void OnMacOSFetchProductsFailure(string json)
        {
            Debug.LogError($"[GamePotStandalone] Fetch Products failure: {json}");
            
            if (_purchaseInProgress)
            {
                FailPurchase($"Failed to fetch product information: {json}");
                return;
            }
            
            if (_getPurchaseDetailListInProgress)
            {
                _getPurchaseDetailListInProgress = false;
                Debug.LogError("[GamePotStandalone] FetchProducts failed during getPurchaseDetailListAsync");
                
                NError error = new NError { code = NError.CODE_UNKNOWN_ERROR, message = $"Failed to fetch products: {json}" };
                GamePotEventListener detailListListener = GamePotEventListener.s_instance;
                if (detailListListener != null)
                {
                    detailListListener.onPurchaseDetailListFailure(error.ToJson());
                }
                return;
            }
            
            GamePotEventListener listener = GamePotEventListener.s_instance;
            if (listener != null)
            {
                listener.onFetchProductsFailure(json);
            }
        }

        [AOT.MonoPInvokeCallback(typeof(GamePotMacNative.NativeCallback))]
        private static void OnMacOSPurchaseSuccess(string json)
        {
            
            try
            {
                JSONNode purchaseData = JSONNode.Parse(json);
                
                string transactionId = purchaseData["transactionId"];
                string productId = purchaseData["productId"] ?? _currentPurchaseProductId ?? "";
                string receipt = purchaseData["receipt"]; // Base64 encoded receipt
                
                string currency = purchaseData["currency"] ?? "";
                string country = purchaseData["country"] ?? "";
                string priceStr = purchaseData["price"] ?? "0";
                string priceString = purchaseData["priceString"] ?? "";
                string productName = purchaseData["productName"] ?? "";
                string orderId = purchaseData["orderId"] ?? transactionId;
                
                string uniqueId = purchaseData["uniqueId"] ?? "";
                string serverId = purchaseData["serverId"] ?? "";
                string playerId = purchaseData["playerId"] ?? "";
                string etc = purchaseData["etc"] ?? "";
                
                bool userDataFromNativeIsEmpty = string.IsNullOrEmpty(uniqueId) && string.IsNullOrEmpty(serverId) 
                    && string.IsNullOrEmpty(playerId) && string.IsNullOrEmpty(etc);
                
                if (userDataFromNativeIsEmpty)
                {
                    if (!string.IsNullOrEmpty(_currentPurchaseUniqueId) || !string.IsNullOrEmpty(_currentPurchaseServerId) 
                        || !string.IsNullOrEmpty(_currentPurchasePlayerId) || !string.IsNullOrEmpty(_currentPurchaseEtc))
                    {
                        Debug.Log("[GamePotStandalone] Using current purchase userData");
                        uniqueId = _currentPurchaseUniqueId ?? "";
                        serverId = _currentPurchaseServerId ?? "";
                        playerId = _currentPurchasePlayerId ?? "";
                        etc = _currentPurchaseEtc ?? "";
                    }
                    else
                    {
                        JSONNode savedPendingInfo = LoadPendingPurchaseInfo();
                        if (savedPendingInfo != null)
                        {
                            Debug.Log("[GamePotStandalone] Recovering userData from saved pending info");
                            uniqueId = savedPendingInfo["uniqueId"] ?? "";
                            serverId = savedPendingInfo["serverId"] ?? "";
                            playerId = savedPendingInfo["playerId"] ?? "";
                            etc = savedPendingInfo["etc"] ?? "";
                        }
                        else
                        {
                            Debug.LogWarning("[GamePotStandalone] No saved pending info found. userData will be empty.");
                        }
                    }
                }
                
                double price = 0;
                double.TryParse(priceStr, out price);
                
                JSONNode userData = new JSONObject();
                userData.Add("unique_id", uniqueId);
                userData.Add("server_id", serverId);
                userData.Add("player_id", playerId);
                userData.Add("etc", etc);
                string userDataJson = userData.ToString();
                
                
                SavePendingPurchaseInfo(productId, transactionId, receipt, priceStr, currency, country, productName, uniqueId, serverId, playerId, etc);
                
                if (string.IsNullOrEmpty(TOKEN) || string.IsNullOrEmpty(MEMBER_ID))
                {
                    
                    if (_purchaseInProgress)
                    {
                        _purchaseInProgress = false;
                        ClearPurchaseState();
                        FailPurchase(NError.CODE_NOT_INITALIZE, "Please login before making a purchase. The purchase will be processed after login.");
                    }
                    return;
                }
                
                
                GamePotStandalone.CreatePurchase(
                    STORE, // storeId
                    PROJECT_ID, // projectId
                    TOKEN, // token
                    transactionId, // orderId
                    "",
                    productId, // itemId
                    receipt, // receipt
                    "apple", // paymentId
                    currency, // currency
                    country, // country
                    price, // price
                    userDataJson, // userData
                    (bool success, JSONNode result) =>
                    {
                        if (success)
                        {
                            OnCreatePurchaseSuccess(transactionId, productId, productName, price.ToString(), currency, orderId, uniqueId, serverId, playerId, etc, receipt, result);
                        }
                        else
                        {
                            OnCreatePurchaseFailure(transactionId, result);
                        }
                    }
                );
            }
            catch (Exception ex)
            {
                Debug.LogError($"[GamePotStandalone] Failed to parse purchase data: {ex.Message}");
                if (_purchaseInProgress)
                {
                    FailPurchase($"Failed to parse purchase data: {ex.Message}");
                }
            }
        }

        private static void OnCreatePurchaseSuccess(string transactionId, string productId, string productName, 
            string price, string currency, string orderId, string uniqueId, string serverId, string playerId, 
            string etc, string receipt, JSONNode result)
        {
            
            ClearPendingPurchaseInfo();
            
            if (!string.IsNullOrEmpty(transactionId))
            {
                GamePotMacNative.FinishTransaction(transactionId);
            }
            
            _purchaseInProgress = false;
            ClearPurchaseState();
            
            JSONObject purchaseInfo = new JSONObject();
            purchaseInfo["price"] = price;
            purchaseInfo["productId"] = productId;
            purchaseInfo["currency"] = currency;
            purchaseInfo["orderId"] = orderId;
            purchaseInfo["productName"] = productName;
            purchaseInfo["gamepotOrderId"] = ""; // 해당 값은 제공하지 않음
            purchaseInfo["uniqueId"] = uniqueId;
            purchaseInfo["serverId"] = serverId;
            purchaseInfo["playerId"] = playerId;
            purchaseInfo["etc"] = etc;
            purchaseInfo["signature"] = "";  // Apple에서는 빈 문자열
            purchaseInfo["originalJSONData"] = receipt;
            
            string purchaseJson = purchaseInfo.ToString();
            
            GamePotEventListener listener = GamePotEventListener.s_instance;
            if (listener != null)
            {
                listener.onPurchaseSuccess(purchaseJson);
            }
        }

        private static void OnCreatePurchaseFailure(string transactionId, JSONNode error)
        {
            Debug.LogError($"[GamePotStandalone] createPurchase failed for transaction {transactionId}: {error?.ToString()}");
            
            int errorCode = NError.CODE_UNKNOWN_ERROR;
            string errorMessage = "Unknown error";
            if (error != null)
            {
                if (error["code"] != null)
                {
                    errorCode = error["code"].AsInt;
                }
                if (error["message"] != null)
                {
                    errorMessage = error["message"].Value;
                }
            }
            
            if (errorCode == 405)
            {
                
                try
                {
                    GamePotMacNative.FinishTransaction(transactionId);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"[GamePotStandalone] Failed to call finishTransaction for 405 error: {ex.Message}");
                }
                
                ClearPendingPurchaseInfo();
                
                _purchaseInProgress = false;
                ClearPurchaseState();
                FailPurchase(errorCode, $"Duplication Error: {errorMessage}");
                return;
            }
            
            
            if (_purchaseInProgress)
            {
                FailPurchase(errorCode, $"createPurchase error occurred: {errorMessage}");
            }
            else
            {
            }
        }

        [AOT.MonoPInvokeCallback(typeof(GamePotMacNative.NativeCallback))]
        private static void OnMacOSPurchaseFailure(string json)
        {
            Debug.LogError($"[GamePotStandalone] Purchase failure (native): {json}");
            
            if (_purchaseInProgress)
            {
                _purchaseInProgress = false;
                ClearPurchaseState();
            }
            
            int errorCode = NError.CODE_UNKNOWN_ERROR;
            string errorMessage = "Unknown error";
            try
            {
                JSONNode errorJson = JSON.Parse(json);
                if (errorJson != null)
                {
                    if (errorJson["errorCode"] != null)
                    {
                        errorCode = errorJson["errorCode"].AsInt;
                    }
                    else if (errorJson["code"] != null)
                    {
                        errorCode = errorJson["code"].AsInt;
                    }
                    if (errorJson["errorMessage"] != null)
                    {
                        errorMessage = errorJson["errorMessage"].Value;
                    }
                    else if (errorJson["message"] != null)
                    {
                        errorMessage = errorJson["message"].Value;
                    }
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning($"[GamePotStandalone] Failed to parse purchase failure json: {ex.Message}");
            }
            
            GamePotEventListener listener = GamePotEventListener.s_instance;
            if (listener != null)
            {
                NError err = new NError { code = errorCode, message = errorMessage };
                listener.onPurchaseFailure(err.ToJson());
            }
        }

        [AOT.MonoPInvokeCallback(typeof(GamePotMacNative.NativeCallback))]
        private static void OnMacOSPurchaseDeferred(string json)
        {
            
            GamePotEventListener listener = GamePotEventListener.s_instance;
            if (listener != null)
            {
                listener.onPurchaseDeferred(json);
            }
        }

        [AOT.MonoPInvokeCallback(typeof(GamePotMacNative.NativeCallback))]
        private static void OnMacOSRestorePurchaseSuccess(string json)
        {
            
            GamePotEventListener listener = GamePotEventListener.s_instance;
            if (listener != null)
            {
                listener.onRestorePurchaseSuccess(json);
            }
        }

        [AOT.MonoPInvokeCallback(typeof(GamePotMacNative.NativeCallback))]
        private static void OnMacOSRestorePurchaseFailure(string json)
        {
            Debug.LogError($"[GamePotStandalone] Restore Purchase failure: {json}");
            
            GamePotEventListener listener = GamePotEventListener.s_instance;
            if (listener != null)
            {
                listener.onRestorePurchaseFailure(json);
            }
        }

        [AOT.MonoPInvokeCallback(typeof(GamePotMacNative.NativeCallback))]
        private static void OnMacOSRestorePurchasesComplete(string json)
        {
            Debug.Log("[GamePotStandalone] Restore Purchases complete");
            
            GamePotEventListener listener = GamePotEventListener.s_instance;
            if (listener != null)
            {
                listener.onRestorePurchasesComplete(json);
            }
        }

        #endregion

        #region App Status Check

        /// <summary>
        /// 게임팟 제공 점검/업데이트 팝업 표시 (macOS 전용)
        /// </summary>
        public static void showAppStatusPopup(string statusJson)
        {
#if UNITY_STANDALONE_OSX
            //Debug.Log($"[GamePotStandalone] showAppStatusPopup - statusJson: {statusJson}");
            GamePotMacNative.ShowAppStatusPopup(statusJson);
#else
            Debug.LogWarning("[GamePotStandalone] showAppStatusPopup is only supported on macOS");
#endif
        }

        #endregion
#endif

    }
}
#endif