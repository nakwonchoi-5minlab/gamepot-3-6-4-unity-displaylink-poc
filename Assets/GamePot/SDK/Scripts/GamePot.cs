using System.Collections.Generic;
using UnityEngine;
using Realtime.LITJson;
using GamePotUnity.SimpleJSON;
using System;
using UnityEngine.XR;
using static NCommon;

#if UNITY_STANDALONE
using GamePotUnity.Standalone;
#elif UNITY_IOS
using GamePotUnityiOS;
#elif UNITY_ANDROID
using GamePotUnityAOS;
#endif

namespace GamePotUnity
{
    public class GamePot
    {
        public static string UnityPluginVersion = "3.6.3-b";

        public static NCommon.LoginType getLastLoginType()
        {
            string result = "";

#if UNITY_EDITOR
            Debug.Log("GamePot - Not Supported in UNITY_EDITOR. getLastLoginType always returns NONE");
#elif UNITY_STANDALONE
            Debug.Log("GamePot - Not Supported in UNITY_STANDALONE. getLastLoginType always returns NONE");
#elif UNITY_IOS
                result = GamePotUnityPluginiOS.getLastLoginType();
#elif UNITY_ANDROID
            result = GamePotUnityPluginAOS.getLastLoginType();
#endif

            switch (result)
            {
                case "GOOGLE":
                    return NCommon.LoginType.GOOGLE;
                case "GOOGLEPLAY":
                    return NCommon.LoginType.GOOGLEPLAY;
                case "FACEBOOK":
                    return NCommon.LoginType.FACEBOOK;
                case "NAVER":
                    return NCommon.LoginType.NAVER;
                case "GAMECENTER":
                    return NCommon.LoginType.GAMECENTER;
                case "TWITTER":
                    return NCommon.LoginType.TWITTER;
                case "LINE":
                    return NCommon.LoginType.LINE;
                case "OAUTH":
                    return NCommon.LoginType.OAUTH;
                case "APPLE":
                    return NCommon.LoginType.APPLE;
                case "GUEST":
                    return NCommon.LoginType.GUEST;
                case "THIRDPARTYSDK":
                    return NCommon.LoginType.THIRDPARTYSDK;
                case "EMAIL":
                    return NCommon.LoginType.EMAIL;
                case "HUAWEI":
                    return NCommon.LoginType.HUAWEI;
            }

            return NCommon.LoginType.NONE;
        }

        //changeProjectId 추가 - 동일 조건(리전/인앱 정보)의 대시보드간 프로젝트 아이디 이동 관련 
        public static void changeProjectId(string projectId)
        {
#if UNITY_EDITOR
            {
                 Debug.Log("GamePot - UNITY_EDITOR changeProjectId always returns empty");
            }
#elif UNITY_STANDALONE
            GamePotUnityPluginStandalone.changeProjectId(projectId, "");
#elif UNITY_IOS
            GamePotUnityPluginiOS.changeProjectId(projectId);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.changeProjectId(projectId);
#endif 
        }

        public static void changeProjectId(string projectId, string sens_Push_Service_id)
        {
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR changeProjectId always returns empty");
            }
#elif UNITY_STANDALONE
            GamePotUnityPluginStandalone.changeProjectId(projectId, sens_Push_Service_id);
#elif UNITY_IOS
            GamePotUnityPluginiOS.changeProjectId(projectId,sens_Push_Service_id);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.changeProjectId(projectId,sens_Push_Service_id);
#endif
        }

        public static void changeProjectId(string projectId, string sens_Push_Service_id, string region)
        {
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR changeProjectId always returns empty");
            }
#elif UNITY_STANDALONE
            GamePotUnityPluginStandalone.changeProjectId(projectId, sens_Push_Service_id, region);
#elif UNITY_IOS
            GamePotUnityPluginiOS.changeProjectId(projectId, sens_Push_Service_id, region);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.changeProjectId(projectId, sens_Push_Service_id, region);
#endif
        }

        // get/set ShowWithDrawPopup - 탈퇴 철회 팝업 ON/OFF
        public static bool getShowWithDrawPopup()
        {
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR getShowWithDrawPopup always returns empty");
                return false;
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - Not Supported in UNITY_STANDALONE. getShowWithDrawPopup always returns empty");
                //return GamePotUnityPluginStandalone.getShowWithDrawPopup();
                return false;
            }
#elif UNITY_IOS
            return GamePotUnityPluginiOS.getShowWithDrawPopup();
#elif UNITY_ANDROID
            return GamePotUnityPluginAOS.getShowWithDrawPopup();
#endif
        }
        public static void setShowWithDrawPopup(bool enable)
        {
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR getShowWithDrawPopup always returns empty");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - Not Supported in UNITY_STANDALONE. setShowWithDrawPopup always returns empty");
                //GamePotUnityPluginStandalone.setShowWithDrawPopup(enable);
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.setShowWithDrawPopup(enable);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.setShowWithDrawPopup(enable);
#endif
        }

        public static string getMemberId()
        {
            if (GamePotSettings.MemberInfo == null)
                return "";

            return GamePotSettings.MemberInfo.memberid;
        }

        public static string getMemberName()
        {
            if (GamePotSettings.MemberInfo == null)
                return "";

            return GamePotSettings.MemberInfo.name;
        }

        public static string getMemberEmail()
        {
            if (GamePotSettings.MemberInfo == null)
                return "";

            return GamePotSettings.MemberInfo.email;
        }

        public static string getMemberProfileUrl()
        {
            if (GamePotSettings.MemberInfo == null)
                return "";

            return GamePotSettings.MemberInfo.profileUrl;
        }

        public static string getMemberSocialId()
        {
            if (GamePotSettings.MemberInfo == null)
                return "";

            return GamePotSettings.MemberInfo.userid;
        }

        public static string getLanguage()
        {
            string result = "";

#if UNITY_EDITOR
            Debug.Log("GamePot - Not Supported in UNITY_EDITOR. getLanguage always returns empty string");
#elif UNITY_STANDALONE
            Debug.Log("GamePot - Not Supported in UNITY_STANDALONE. getLanguage always returns empty string");
#elif UNITY_IOS
            Debug.Log("GamePot - Not Supported in UNITY_IOS. getLanguage always returns empty string");
            // result = "ko-KR";
            //result = GamePotUnityPluginiOS.getLanguage();
#elif UNITY_ANDROID
            Debug.Log("GamePot - Not Supported in UNITY_ANDROID. getLanguage always returns empty string");
            // result = GamePotUnityPluginAOS.getLanguage();
#endif
            return result;
        }

        public static List<NLinkingInfo> getLinkedList()
        {
            string result = "";

#if UNITY_EDITOR
            {
                Debug.Log("GamePot - Not Supported in UNITY_EDITOR. getLinkedList always returns empty linked list");
            }
#elif UNITY_STANDALONE
            Debug.Log("GamePot - Not Supported in UNITY_STANDALONE. getLanguage always returns empty string");
#elif UNITY_IOS
            result = GamePotUnityPluginiOS.getLinkedList();
#elif UNITY_ANDROID
            result = GamePotUnityPluginAOS.getLinkedList();
#endif

            JsonData data = JsonMapper.ToObject(result);
            List<NLinkingInfo> itemData = new List<NLinkingInfo>();
            if (data.IsArray || data.IsObject)
            {
                foreach (JsonData item in data)
                {
                    Debug.Log("GamePot::getLinkedList-" + item["provider"]);

                    NLinkingInfo info = new NLinkingInfo();
                    if (item["provider"].ToString() == "google")
                        info.provider = NCommon.LinkingType.GOOGLE;
                    else if (item["provider"].ToString() == "facebook")
                        info.provider = NCommon.LinkingType.FACEBOOK;
                    else if (item["provider"].ToString() == "naver")
                        info.provider = NCommon.LinkingType.NAVER;
                    else if (item["provider"].ToString() == "googleplay")
                        info.provider = NCommon.LinkingType.GOOGLEPLAY;
                    else if (item["provider"].ToString() == "gamecenter")
                        info.provider = NCommon.LinkingType.GAMECENTER;
                    else if (item["provider"].ToString() == "line")
                        info.provider = NCommon.LinkingType.LINE;
                    else if (item["provider"].ToString() == "twitter")
                        info.provider = NCommon.LinkingType.TWITTER;
                    else if (item["provider"].ToString() == "apple")
                        info.provider = NCommon.LinkingType.APPLE;
                    else if (item["provider"].ToString() == "thirdpartysdk")
                        info.provider = NCommon.LinkingType.THIRDPARTYSDK;
                    else if (item["provider"].ToString() == "huawei")
                        info.provider = NCommon.LinkingType.HUAWEI;
                    itemData.Add(info);
                }
            }
            return itemData;
        }


        public static void initPlugin()
        {
#if UNITY_EDITOR && !UNITY_STANDALONE
            if (GamePotEventListener.s_instance == null)
            {
                Debug.Log("GamePot - Creating GamePot Native Bridge Receiver");
                new GameObject("GamePotEditorManager", typeof(GamePotEventListener));
            }
#elif UNITY_STANDALONE
            GamePotUnityPluginStandalone.initPlugin();
#elif UNITY_IOS
            GamePotUnityPluginiOS.initPlugin();
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.initPlugin();
#endif

        }

        public static void setListener(IGamePot inter)
        {
            if (GamePotEventListener.s_instance != null)
            {
                GamePotEventListener.s_instance.setListener(inter);
            }
            else
            {
                Debug.LogError("GamePotEventListener instance is null");
            }
        }

        //
        //        //업적, 리더보드 사용 시 추가
        //        public static void enableGameCenter(bool enable)
        //        {
        //           #if UNITY_IOS
        //			GamePotUnityPluginiOS.enableGameCenter(enable);
        //            #elif UNITY_EDITOR
        //            #endif
        //        }
        //
        //         public static void addAd(NCommon.AdType adType)
        //         {
        //#if UNITY_IOS
        // 			GamePotUnityPluginiOS.addAd(adType);
        // #elif !UNITY_EDITOR && UNITY_ANDROID
        // 			GamePotUnityPluginAOS.addAd(adType);
        // #elif UNITY_EDITOR
        // #endif
        //         }


        //         public static void tracking(NCommon.AdActions adActions, string adjustKey)
        //         {
        //#if UNITY_IOS
        // 			GamePotUnityPluginiOS.tracking(adActions, adjustKey);
        // #elif !UNITY_EDITOR && UNITY_ANDROID
        // 			GamePotUnityPluginAOS.tracking(adActions, adjustKey);
        // #elif UNITY_EDITOR
        // #endif
        //         }
        //         public static void showNaverCafe(int menuIndex, bool landscape)
        //         {
        //#if UNITY_IOS
        // 			GamePotUnityPluginiOS.showNaverCafe(menuIndex, landscape);
        // #elif !UNITY_EDITOR && UNITY_ANDROID
        // 			GamePotUnityPluginAOS.showNaverCafe(menuIndex, landscape);
        // #elif UNITY_EDITOR
        // #endif
        //         }
        //         public static void tracking(NCommon.AdActions adActions, TrackingInfo trackingInfo)
        //         {
        //#if UNITY_IOS
        // 			GamePotUnityPluginiOS.tracking(adActions, trackingInfo);
        // #elif !UNITY_EDITOR && UNITY_ANDROID
        // 			GamePotUnityPluginAOS.tracking(adActions, trackingInfo);
        // #elif UNITY_EDITOR
        // #endif
        //         }

        //Login

        //send Local Push
        public static int sendLocalPush(DateTime sendDate, string title, string message)
        {
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR sendLocalPush always return -1");
                return -1;
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE sendLocalPush always return -1");
                return -1;
            }
#elif UNITY_IOS
            return GamePotUnityPluginiOS.sendLocalPush(sendDate.ToString("yyyy-MM-dd HH:mm:ss"), title, message);
#elif UNITY_ANDROID
            return GamePotUnityPluginAOS.sendLocalPush(sendDate, title, message);
#else
            return -1;
#endif
        }


        public static bool isLinked(string linkType)
        {
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR always returns isLinked false");
                return false;
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE always returns isLinked false");
                return false;
            }
#elif UNITY_IOS
            return GamePotUnityPluginiOS.isLinked(linkType);
#elif UNITY_ANDROID
            return GamePotUnityPluginAOS.isLinked(linkType);
#else
            return false;
#endif
        }


        public static void cancelLocalPush(string pushId)
        {
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR cancelLocalPush not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE cancelLocalPush not supported");
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.cancelLocalPush(Int32.Parse(pushId));
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.cancelLocalPush(Int32.Parse(pushId));
#endif
        }
		
        public static void loginWithEmail(string username, string password)
        {
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR cancelLocalPush not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE cancelLocalPush not supported");
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.loginWithEmail(NCommon.LoginType.EMAIL.ToString(), username, password);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.loginWithEmail(username, password);
#endif
        }

        public static void loginWithEmail(string username, string password, GamePotCallbackDelegate.CB_Login cbLogin)
        {
            GamePotEventListener.cbLogin = cbLogin;
            loginWithEmail( username,  password);
        }

        public static void signInWithGameService()
        {
#if UNITY_ANDROID
            GamePotUnityPluginAOS.signInWithGameService();
#else
            {
                Debug.Log("GamePot - signInWithGameService is only supported on ANDROID");
            }
#endif
        }


        public static void signInWithGameService(GamePotCallbackDelegate.CB_Login cbLogin)
        {
            GamePotEventListener.cbLogin = cbLogin;
            signInWithGameService();
        }

        public static void signInResolverConflict()
        {
            
#if UNITY_EDITOR
        {
            Debug.Log("GamePot - UNITY_EDITOR signInResolverConflict not supported");
        }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE signInResolverConflict not supported");
            }
#elif UNITY_IOS
            {
                Debug.Log("GamePot - UNITY_IOS signInResolverConflict not supported");
            }
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.signInResolverConflict();
#endif
        }
        
        public static void signInSwitchAccount()
        {
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR signInSwitchAccount not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE signInSwitchAccount not supported");
            }
#elif UNITY_IOS
            {
                Debug.Log("GamePot - UNITY_IOS signInSwitchAccount not supported");
            }
#elif UNITY_ANDROID
            {
            GamePotUnityPluginAOS.signInSwitchAccount();
            }
#endif
        }

        public static void login(NCommon.LoginType loginType)
        {
#if UNITY_EDITOR && !UNITY_STANDALONE
            {
                GamePotEventListener listener = GamePotEventListener.s_instance;
                if (loginType == NCommon.LoginType.GUEST)
                {
                    Debug.Log("GamePot - UNITY_EDITOR DUMMY CALLBACK login with GUEST. It does not communicate with server.");
                    // Dummy userInfo for UnityEditor Development mode
                    if (listener != null)
                    {
                        NUserInfo userInfo = new NUserInfo
                        {
                            userid = "UE-" + SystemInfo.deviceUniqueIdentifier,
                            name = "UnityEditor",
                            memberid = "UnityEditor_Member",
                            token = ""
                        };
                        listener.onLoginSuccess(userInfo.ToJson());
                    }
                    else
                    {
                        Debug.LogError("GamePot UNITY_EDITOR listener is NULL");
                    }
                }
                else
                {
                    Debug.LogFormat("GamePot - UNITY_EDITOR DUMMY CALLBACK with {0} login cancelled", loginType);
                    listener.onLoginCancel();
                }
            }
#elif UNITY_STANDALONE
            {
                {
                    //if (loginType == NCommon.LoginType.STANDALONE)
                    //{
                    GamePotUnityPluginStandalone.login(loginType);
                    //}
                    //else
                    //{
                    //Debug.LogFormat("GamePot - login : UNITY_STANDALONE doesn't supports {0}. Only support LoginType.STANDALONE", loginType);
                    //}
                }
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.login(loginType);

            ////2021.06.04 자동 tracking 팝업 로직 native 안으로 이전.
            //requestTrackingAuthorization((NResultTrackingAuthorization resultState) => {});
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.login(loginType);
#endif
        }

        /// <summary>
        /// Login (callback delegate)
        /// </summary>
        /// <param name="loginType">Login Type</param>
        /// <param name="callback"></param>
        public static void login(NCommon.LoginType loginType, GamePotCallbackDelegate.CB_Login cbLogin)
        {
            GamePotEventListener.cbLogin = cbLogin;
            login(loginType);
        }

        public static void showLoginWithUI(NLoginUIInfo info)
        {
            Debug.Log("[GPUnity][Call] showLoginWithUI");
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR  showLoginWithUI Routed Login with GUEST. It does not communicate with server.");
                login(NCommon.LoginType.GUEST);
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.showLoginWithUI(info != null ? info.ToJson() : null);

            ////2021.06.04 자동 tracking 팝업 로직 native 안으로 이전.
            //requestTrackingAuthorization((NResultTrackingAuthorization resultState) => {});
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.showLoginWithUI(info != null ? info.ToJson() : null);
#endif
        }

        public static void showLoginWithUI(NLoginUIInfo info, GamePotCallbackDelegate.CB_Login cbLogin)
        {
            GamePotEventListener.cbLogin = cbLogin;
            showLoginWithUI(info);
        }


        //         public static void setSandbox(bool enable)
        //         {
        //#if UNITY_IOS
        // 			GamePotUnityPluginiOS.setSandbox(enable);
        // #elif !UNITY_EDITOR && UNITY_ANDROID
        // 			GamePotUnityPluginAOS.setSandbox(enable);
        // #elif UNITY_EDITOR
        // #endif
        //         }

        public static void deleteMember()
        {
#if UNITY_EDITOR
            Debug.Log("GamePot - UNITY_EDITOR DUMMY CALLBACK deleteMember always success");
            // Temporary user info for UnityEditor Development mode
            if (GamePotEventListener.s_instance != null)
            {
                GamePotEventListener.s_instance.onDeleteMemberSuccess();
            }
            else
            {
                Debug.LogError("GamePot UNITY_EDITOR listener is NULL");
            }
#elif UNITY_STANDALONE
            Debug.Log("GamePot - UNITY_STANDALONE DUMMY CALLBACK deleteMember always success");
            // Temporary user info for UNITY_STANDALONE Development mode
            if (GamePotEventListener.s_instance != null)
            {
                GamePotEventListener.s_instance.onDeleteMemberSuccess();
            }
            else
            {
                Debug.LogError("GamePot UNITY_STANDALONE listener is NULL");
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.deleteMember();
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.deleteMember();
#endif
        }

        /// <summary>
        /// Delete Member (callback delegate)
        /// </summary>
        /// <param name="cbDeleteMember"></param>
        public static void deleteMember(GamePotCallbackDelegate.CB_Common cbDeleteMember)
        {
            GamePotEventListener.cbDeleteMember = cbDeleteMember;
            deleteMember();
        }

        public static void logout()
        {
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR DUMMY CALLBACK logout always success");
                // Temporary user info for UnityEditor Development mode
                if (GamePotEventListener.s_instance != null)
                {
                    GamePotEventListener.s_instance.onLogoutSuccess("");
                }
                else
                {
                    Debug.LogError("GamePot UNITY_EDITOR listener is NULL");
                }
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE DUMMY CALLBACK logout always success");
                // Temporary user info for UNITY_STANDALONE Development mode
                if (GamePotEventListener.s_instance != null)
                {
                    GamePotEventListener.s_instance.onLogoutSuccess("");
                }
                else
                {
                    Debug.LogError("GamePot UNITY_STANDALONE listener is NULL");
                }
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.logout();
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.logout();
#endif
        }

        /// <summary>
        /// Logout (callback delegate)
        /// </summary>
        /// <param name="cbPurchase"></param>
        public static void logout(GamePotCallbackDelegate.CB_Common cbLogout)
        {
            GamePotEventListener.cbLogout = cbLogout;
            logout();
        }


        public static string getConfig(string key)
        {
            string result = "";
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR getConfig always returns empty");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE getConfig always returns empty");
            }
#elif UNITY_IOS
            result = GamePotUnityPluginiOS.getConfig(key);
#elif UNITY_ANDROID
            result = GamePotUnityPluginAOS.getConfig(key);
#endif
            return result;
        }

        public static string getConfigs()
        {
            string result = "";
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR getConfigs always returns empty");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE getConfigs always returns empty");
            }
#elif UNITY_IOS
            result = GamePotUnityPluginiOS.getConfigs();
#elif UNITY_ANDROID
            result = GamePotUnityPluginAOS.getConfigs();
#endif
            return result;
        }

        public static JSONNode getUserData()
        {
            string result = "";
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR getUserData always returns empty");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE getUserData always returns empty");
            }
#elif UNITY_IOS
            result = GamePotUnityPluginiOS.getUserData();
#elif UNITY_ANDROID
            result = GamePotUnityPluginAOS.getUserData();
#endif

            JSONNode jsonData = JSON.Parse(result);

            return jsonData;
        }

        public static void setUserData(JSONNode userData)
        {
            Debug.Log("GamePot - setUserData : " + userData.ToString());

#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR setUserData not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE setUserData not supported");
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.setUserData(userData.ToString());
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.setUserData(userData.ToString());
#endif
        }

        public static void setUserData(JSONNode userData, GamePotCallbackDelegate.CB_SetUserData cbSetUserData)
        {
            GamePotEventListener.cbSetUserData = cbSetUserData;
            setUserData(userData);
        }


        public static void coupon(string couponNumber)
        {
            coupon(couponNumber, "");
        }

        public static void coupon(string couponNumber, string userData)
        {
#if UNITY_EDITOR && !UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_EDITOR DUMMY CALLBACK coupon always success");
                // Temporary user info for UNITY_EDITOR Development mode
                if (GamePotEventListener.s_instance != null)
                {
                    GamePotEventListener.s_instance.onCouponSuccess("");
                }
                else
                {
                    Debug.LogError("GamePot UNITY_EDITOR listener is NULL");
                }
            }
#elif UNITY_STANDALONE
            {
                GamePotUnityPluginStandalone.coupon(couponNumber, userData);
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.coupon(couponNumber, userData);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.coupon(couponNumber, userData);
#endif
        }

        /// <summary>
        /// Coupon (callback delegate)
        /// </summary>
        /// <param name="couponNumber"></param>
        /// <param name="cbCoupon">Callback function</param>
        public static void coupon(string couponNumber, GamePotCallbackDelegate.CB_Common cbCoupon)
        {
            GamePotEventListener.cbCoupon = cbCoupon;
            coupon(couponNumber, "");
        }

        /// <summary>
        /// Coupon (callback delegate)
        /// </summary>
        /// <param name="couponNumber"></param>
        /// <param name="userData"></param>
        /// <param name="cbCoupon">Callback function</param>
        public static void coupon(string couponNumber, string userData, GamePotCallbackDelegate.CB_Common cbCoupon)
        {
            GamePotEventListener.cbCoupon = cbCoupon;
            coupon(couponNumber, userData);
        }


        public static void setLanguage(NCommon.GameLanguage gameLanguage)
        {
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR setLanguage not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE setLanguage not supported");
            }
#elif UNITY_IOS
            Debug.Log("GamePot - setLanguage not supported");
            // GamePotUnityPluginiOS.setLanguage((int)gameLanguage);
#elif UNITY_ANDROID
            Debug.Log("GamePot - setLanguage not supported");
            // GamePotUnityPluginAOS.setLanguage((int)gameLanguage);
#endif
        }

        public static void purchase(string productId)
        {
            purchase(productId, "", "", "", "");
        }

        public static void purchase(string productId, string uniqueId)
        {
            purchase(productId, uniqueId, "", "", "");
        }

        public static void purchase(string productId, string uniqueId, string serverId, string playerId, string etc)
        {
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR DUMMY CALLBACK purchase always cancelled");
                // Temporary user info for UNITY_EDITOR Development mode
                if (GamePotEventListener.s_instance != null)
                {
                    GamePotEventListener.s_instance.onPurchaseCancel();
                }
                else
                {
                    Debug.LogError("GamePot UNITY_EDITOR listener is NULL");
                }
            }
#elif UNITY_STANDALONE
            {
                // macOS 환경에서는 네이티브 StoreKit 결제 사용
#if UNITY_STANDALONE_OSX
                Debug.Log($"GamePot - UNITY_STANDALONE_OSX purchasing product: {productId}");
                // iOS SDK와 동일하게 purchase 메서드 호출 (checkPurchase -> fetchProducts -> purchaseProduct -> createPurchase)
                GamePotUnity.Standalone.GamePotUnityPluginStandalone.purchase(productId, uniqueId, serverId, playerId, etc);
#else
                Debug.Log("GamePot - UNITY_STANDALONE DUMMY CALLBACK purchase always cancelled");
                // Temporary user info for UNITY_STANDALONE Development mode
                if (GamePotEventListener.s_instance != null)
                {
                    GamePotEventListener.s_instance.onPurchaseCancel();
                }
                else
                {
                    Debug.LogError("GamePot UNITY_STANDALONE listener is NULL");
                }
#endif
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.purchase(productId, uniqueId, serverId, playerId, etc);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.purchase(productId, uniqueId, serverId, playerId, etc);
#endif
        }


        /// <summary>
        /// Purchase (callback delegate)
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="cbPurchase"></param>
        public static void purchase(string productId, GamePotCallbackDelegate.CB_Purchase cbPurchase)
        {
            GamePotEventListener.cbPurchase = cbPurchase;
            purchase(productId, "", "", "", "");
        }

        /// <summary>
        /// Purchase (callback delegate)
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="uniqueId"></param>
        /// <param name="cbPurchase"></param>
        public static void purchase(string productId, string uniqueId, GamePotCallbackDelegate.CB_Purchase cbPurchase)
        {
            GamePotEventListener.cbPurchase = cbPurchase;
            purchase(productId, uniqueId);
        }

        /// <summary>
        /// Purchase (callback delegate)
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="uniqueId"></param>
        /// <param name="serverId"></param>
        /// <param name="playerId"></param>
        /// <param name="etc"></param>
        /// <param name="cbPurchase"></param>

        public static void purchase(string productId, string uniqueId, string serverId, string playerId, string etc, GamePotCallbackDelegate.CB_Purchase cbPurchase)
        {
            GamePotEventListener.cbPurchase = cbPurchase;
            purchase(productId, uniqueId, serverId, playerId, etc);
        }

        /// <summary>
        /// Purchase with subscription offer token (Google Play Android only)
        /// </summary>
        public static void purchase(string productId, string uniqueId, string serverId, string playerId, string etc, string offerId)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            GamePotUnityPluginAOS.purchase(productId, uniqueId, serverId, playerId, etc, offerId);
#else
            // offerId(구독 오퍼 토큰)는 Google Play(Android) 전용 → 그 외 플랫폼은 표준 결제로 폴백
            Debug.Log("GamePot - purchase with offerId is Android(Google Play subscription) only; fallback to standard purchase.");
            purchase(productId, uniqueId, serverId, playerId, etc);
#endif
        }

        /// <summary>
        /// Purchase with subscription offer token (callback delegate)
        /// </summary>
        public static void purchase(string productId, string uniqueId, string serverId, string playerId, string etc, string offerId, GamePotCallbackDelegate.CB_Purchase cbPurchase)
        {
            GamePotEventListener.cbPurchase = cbPurchase;
            purchase(productId, uniqueId, serverId, playerId, etc, offerId);
        }

        /// <summary>
        /// 초기화(setup) 완료 콜백 등록 (Android)
        /// </summary>
        public static void onInitialized()
        {
            Debug.Log("[GPUnity][Call] onInitialized");
#if UNITY_ANDROID && !UNITY_EDITOR
            GamePotUnityPluginAOS.onInitialized();
#else
            Debug.Log("GamePot - onInitialized is Android only.");
#endif
        }

        public static void onInitialized(GamePotCallbackDelegate.CB_Common cbInitialized)
        {
            GamePotEventListener.cbInitialized = cbInitialized;
            onInitialized();
        }

        /// <summary>
        /// 초기화(setup) 진행 중 여부 (Android)
        /// </summary>
        public static bool isSetupInProgress()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return GamePotUnityPluginAOS.isSetupInProgress();
#else
            return false;
#endif
        }

        /// <summary>
        /// StagePay 결제 화면 (Android only). 종료 시 cbStagePay(결과 JSON) 콜백.
        /// </summary>
        public static void showStagePay(string appId, string pg, string productId)
        {
            Debug.Log("[GPUnity][Call] showStagePay");
#if UNITY_ANDROID && !UNITY_EDITOR
            GamePotUnityPluginAOS.showStagePay(appId, pg, productId);
#else
            Debug.Log("GamePot - showStagePay is Android only.");
#endif
        }

        public static void showStagePay(string appId, string pg, string productId, GamePotCallbackDelegate.CB_StagePay cbStagePay)
        {
            GamePotEventListener.cbStagePay = cbStagePay;
            showStagePay(appId, pg, productId);
        }

        public static void showStagePay(string appId, string pg, string productId, string uniqueId, string serverId, string playerId, string etc, string options, string metadata)
        {
            Debug.Log("[GPUnity][Call] showStagePay(full)");
#if UNITY_ANDROID && !UNITY_EDITOR
            GamePotUnityPluginAOS.showStagePay(appId, pg, productId, uniqueId, serverId, playerId, etc, options, metadata);
#else
            Debug.Log("GamePot - showStagePay is Android only.");
#endif
        }

        public static void showStagePay(string appId, string pg, string productId, string uniqueId, string serverId, string playerId, string etc, string options, string metadata, GamePotCallbackDelegate.CB_StagePay cbStagePay)
        {
            GamePotEventListener.cbStagePay = cbStagePay;
            showStagePay(appId, pg, productId, uniqueId, serverId, playerId, etc, options, metadata);
        }



        public static NPurchaseItem[] getPurchaseItems()
        {
#if UNITY_EDITOR
            Debug.Log("GamePot - UNITY_EDITOR getPurchaseItems always returns empty");
            return new NPurchaseItem[0];
#elif UNITY_STANDALONE
#if UNITY_STANDALONE_OSX
            string result = "";
            result = GamePotUnityPluginStandalone.getPurchaseItems();
            NPurchaseItem[] itemData = JsonMapper.ToObject<NPurchaseItem[]>(result);
            return itemData;
#else
            Debug.Log("GamePot - UNITY_STANDALONE getPurchaseItems always returns empty");
            return new NPurchaseItem[0];
#endif
#elif UNITY_IOS
            string result = "";
            result = GamePotUnityPluginiOS.getPurchaseItems();
            NPurchaseItem[] itemData = JsonMapper.ToObject<NPurchaseItem[]>(result);
            return itemData;
#elif UNITY_ANDROID
            string result = "";
            result = GamePotUnityPluginAOS.getPurchaseItems();
            NPurchaseItem[] itemData = JsonMapper.ToObject<NPurchaseItem[]>(result);
            return itemData;
#else
            Debug.Log("GamePot - getPurchaseItems not supported on this platform");
            return new NPurchaseItem[0];
#endif
        }


        public static void getPurchaseDetailListAsync()
        {
#if UNITY_EDITOR
            Debug.Log("GamePot - UNITY_EDITOR getPurchaseDetailListAsync not supported");
#elif UNITY_STANDALONE
            {
#if UNITY_STANDALONE_OSX
            // macOS Standalone에서는 캐시된 아이템 목록을 비동기 콜백으로 전달
            GamePotUnity.Standalone.GamePotUnityPluginStandalone.getPurchaseDetailListAsync();
#else
            Debug.Log("GamePot - UNITY_STANDALONE getPurchaseDetailListAsync not supported");
#endif
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.getPurchaseDetailListAsync();
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.getPurchaseDetailListAsync();
#endif
        }

        public static void getPurchaseDetailListAsync(GamePotCallbackDelegate.CB_PurchaseDetailList cbPurchaseDetailList)
        {
            GamePotEventListener.cbPurchaseDetailList = cbPurchaseDetailList;
            getPurchaseDetailListAsync();
        }


        public static void createLinking(NCommon.LinkingType linkType)
        {
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY EDITOR DUMMY CALLBACK createLinking always cancelled");
                // Temporary user info for UnityEditor Development mode
                if (GamePotEventListener.s_instance != null)
                {
                    GamePotEventListener.s_instance.onCreateLinkingCancel("");
                }
                else
                {
                    Debug.LogError("GamePot UnityEditor listener is NULL");
                }
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.createLinking(linkType);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.createLinking(linkType);
#endif
        }

        /// <summary>
        /// Create Linking (callback delegate)
        /// </summary>
        /// <param name="linkType"></param>
        /// <param name="cbCreateLinking">Callback Function</param>
        public static void createLinking(NCommon.LinkingType linkType, GamePotCallbackDelegate.CB_CreateLinking cbCreateLinking)
        {
            GamePotEventListener.cbCreateLinking = cbCreateLinking;
            createLinking(linkType);
        }


        public static void deleteLinking(NCommon.LinkingType linkType)
        {
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR DUMMY CALLBACK deleteLinking always faulure");
                // Temporary user info for UnityEditor Development mode
                if (GamePotEventListener.s_instance != null)
                {
                    GamePotEventListener.s_instance.onDeleteLinkingFailure("");
                }
                else
                {
                    Debug.LogError("GamePot UnityEditor listener is NULL");
                }
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE DUMMY CALLBACK deleteLinking always faulure");
                // Temporary user info for UNITY_STANDALONE Development mode
                if (GamePotEventListener.s_instance != null)
                {
                    GamePotEventListener.s_instance.onDeleteLinkingFailure("");
                }
                else
                {
                    Debug.LogError("GamePot UNITY_STANDALONE listener is NULL");
                }
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.deleteLinking(linkType);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.deleteLinking(linkType);
#endif
        }

        /// <summary>
        /// Delete Linking (callback delegate)
        /// </summary>
        /// <param name="linkType"></param>
        /// <param name="cbDeleteLinking">Callback Function</param>
        public static void deleteLinking(NCommon.LinkingType linkType, GamePotCallbackDelegate.CB_Common cbDeleteLinking)
        {
            GamePotEventListener.cbDeleteLinking = cbDeleteLinking;
            deleteLinking(linkType);
        }

        public static void setPushStatus(bool pushEnable)
        {
            Debug.Log("[GPUnity][Call] setPushStatus : " + pushEnable);
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR DUMMY CALLBACK setPush always success");
                // Temporary user info for UNITY_EDITOR Development mode
                if (GamePotEventListener.s_instance != null)
                {
                    GamePotEventListener.s_instance.onPushSuccess();
                }
                else
                {
                    Debug.LogError("GamePot UNITY_EDITOR listener is NULL");
                }
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE DUMMY CALLBACK setPush always success");
                // Temporary user info for UNITY_STANDALONE Development mode
                if (GamePotEventListener.s_instance != null)
                {
                    GamePotEventListener.s_instance.onPushSuccess();
                }
                else
                {
                    Debug.LogError("GamePot UNITY_STANDALONE listener is NULL");
                }
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.setPush(pushEnable);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.setPush(pushEnable);
#endif
        }

        /// <summary>
        /// Push Enable
        /// </summary>
        /// <param name="pushEnable"></param>
        /// <param name="cbPushEnable">Callback Function</param>
        public static void setPushStatus(bool pushEnable, GamePotCallbackDelegate.CB_Common cbPushEnable)
        {
            GamePotEventListener.cbPushEnable = cbPushEnable;
            setPushStatus(pushEnable);
        }

        public static void setPushNightStatus(bool nightPushEnable)
        {
            Debug.Log("[GPUnity][Call] setPushNightStatus : " + nightPushEnable);
#if UNITY_EDITOR
            {
                // Temporary user info for UNITY_EDITOR Development mode
                if (GamePotEventListener.s_instance != null)
                {
                    GamePotEventListener.s_instance.onPushNightSuccess();
                }
                else
                {
                    Debug.LogError("GamePot UNITY_EDITOR listener is NULL");
                }
            }
#elif UNITY_STANDALONE
            {
                // Temporary user info for UNITY_STANDALONE Development mode
                if (GamePotEventListener.s_instance != null)
                {
                    GamePotEventListener.s_instance.onPushNightSuccess();
                }
                else
                {
                    Debug.LogError("GamePot UNITY_STANDALONE listener is NULL");
                }
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.setPushNight(nightPushEnable);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.setPushNight(nightPushEnable);
#endif
        }

        /// <summary>
        /// Night Push Enable
        /// </summary>
        /// <param name="nightPushEnable"></param>
        /// <param name="cbPushNightEnable">Callback Function</param>
        public static void setPushNightStatus(bool nightPushEnable, GamePotCallbackDelegate.CB_Common cbPushNightEnable)
        {
            GamePotEventListener.cbPushNightEnable = cbPushNightEnable;
            setPushNightStatus(nightPushEnable);
        }

        public static void setPushADStatus(bool adPushEnable)
        {
            Debug.Log("[GPUnity][Call] setPushADStatus : " + adPushEnable);

#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR DUMMY CALLBACK setPushAd always success");
                // Temporary user info for UNITY_EDITOR Development mode
                if (GamePotEventListener.s_instance != null)
                {
                    GamePotEventListener.s_instance.onPushAdSuccess();
                }
                else
                {
                    Debug.LogError("GamePot UNITY_EDITOR listener is NULL");
                }
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE DUMMY CALLBACK setPushAd always success");
                // Temporary user info for UNITY_STANDALONE Development mode
                if (GamePotEventListener.s_instance != null)
                {
                    GamePotEventListener.s_instance.onPushAdSuccess();
                }
                else
                {
                    Debug.LogError("GamePot UNITY_STANDALONE listener is NULL");
                }
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.setPushAd(adPushEnable);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.setPushAd(adPushEnable);

#endif
        }

        /// <summary>
        /// Set Push AD Status
        /// </summary>
        /// <param name="adPushEnable"></param>
        /// <param name="cbPushADEnable">Callback Function</param>
        public static void setPushADStatus(bool adPushEnable, GamePotCallbackDelegate.CB_Common cbPushADEnable)
        {
            GamePotEventListener.cbPushADEnable = cbPushADEnable;
            setPushADStatus(adPushEnable);
        }

        public static void setPushStatus(bool pushEnable, bool nightPushEnable, bool adPushEnable)
        {
            Debug.Log("[GPUnity][Call] setPush : " + pushEnable + " NightPush : " + nightPushEnable + " adPush : " + adPushEnable);
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR DUMMY CALLBACK setPushState always success");
                // Temporary user info for UNITY_EDITOR Development mode
                if (GamePotEventListener.s_instance != null)
                {
                    GamePotEventListener.s_instance.onPushStatusSuccess();
                }
                else
                {
                    Debug.LogError("GamePot UNITY_EDITOR listener is NULL");
                }
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE DUMMY CALLBACK setPushState always success");
                // Temporary user info for UNITY_STANDALONE Development mode
                if (GamePotEventListener.s_instance != null)
                {
                    GamePotEventListener.s_instance.onPushStatusSuccess();
                }
                else
                {
                    Debug.LogError("GamePot UNITY_STANDALONE listener is NULL");
                }
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.setPushState(pushEnable, nightPushEnable, adPushEnable);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.setPushState(pushEnable, nightPushEnable, adPushEnable);
#endif
        }

        /// <summary>
        /// Set Push Status at once
        /// </summary>
        /// <param name="pushEnable"></param>
        /// <param name="nightPushEnable"></param>
        /// <param name="adPushEnable"></param>
        /// <param name="cbPushStatusEnable">Callback Function</param>
        public static void setPushStatus(bool pushEnable, bool nightPushEnable, bool adPushEnable, GamePotCallbackDelegate.CB_Common cbPushStatusEnable)
        {
            GamePotEventListener.cbPushStatusEnable = cbPushStatusEnable;
            setPushStatus(pushEnable, nightPushEnable, adPushEnable);
        }


        public static NPushInfo getPushStatus()
        {
            Debug.Log("[GPUnity][Call] getPushStatus");
#if UNITY_EDITOR
            Debug.Log("GamePot - UNITY_EDITOR getPushStatus always returns empty");
            return new NPushInfo();
#elif UNITY_STANDALONE
            Debug.Log("GamePot - UNITY_STANDALONE getPushStatus always returns empty");
            return new NPushInfo();
#elif UNITY_IOS
            string result = "";
            result = GamePotUnityPluginiOS.getPushStatus();
            Debug.Log("[GPUnity][Call] getPushStatus result : " + result);
            NPushInfo pushInfo = JsonMapper.ToObject<NPushInfo>(result);
            return pushInfo;
#elif UNITY_ANDROID
            string result = "";
            result = GamePotUnityPluginAOS.getPushStatus();
            Debug.Log("[GPUnity][Call] getPushStatus result : " + result);
            NPushInfo pushInfo = JsonMapper.ToObject<NPushInfo>(result);
            return pushInfo;
#else
            Debug.Log("GamePot - getPushStatus not supported on this platform");
            return new NPushInfo();
#endif
        }

        public static void showNoticeWebView()
        {
            Debug.Log("[GPUnity][Call] showNoticeWebView");

#if UNITY_EDITOR && !UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_EDITOR showNoticeWebView not supported");
            }
#elif UNITY_STANDALONE
            {
                GamePotUnityPluginStandalone.showNoticeWebView();
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.showNoticeWebView();
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.showNoticeWebView();
#endif
        }


        public static void showWebView(string url, GamePotCallbackDelegate.CB_ShowWebView cbShowWebView)
        {
            GamePotEventListener.cbShowWebView = cbShowWebView;
            showWebView(url);
        }


        public static void showWebView(string url)
        {
            Debug.Log("[GPUnity][Call] showWebView url : " + url);

#if UNITY_EDITOR && !UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_EDITOR showWebView not supported");
            }
#elif UNITY_STANDALONE
            {
                GamePotUnityPluginStandalone.showWebView(url);
            }
#elif UNITY_IOS
            {
                GamePotEventListener listener = GamePotEventListener.s_instance;
                GamePotUnityPluginiOS.showWebView(url);

                // IOS doesn't return Any Callback
                listener.onWebviewClose("");
            }
#elif UNITY_ANDROID
                GamePotUnityPluginAOS.showWebView(url);
#endif
        }

        public static void showWebView(string url,bool hasBackButton, GamePotCallbackDelegate.CB_ShowWebView cbShowWebView)
        {
            GamePotEventListener.cbShowWebView = cbShowWebView;
            showWebView(url,hasBackButton);
        }

        public static void showWebView(string url,bool hasBackButton)
        {
            Debug.Log("[GPUnity][Call] showWebView url : " + url + " hasBackButton :" + hasBackButton);
#if UNITY_EDITOR && !UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_EDITOR showWebView not supported");
            }
#elif UNITY_STANDALONE
            {
                GamePotUnityPluginStandalone.showWebView(url);
            }
#elif UNITY_IOS
            {
                GamePotEventListener listener = GamePotEventListener.s_instance;
                GamePotUnityPluginiOS.showWebView(url,hasBackButton);
                listener.onWebviewClose("");
            }
#elif UNITY_ANDROID
                GamePotUnityPluginAOS.showWebView(url,hasBackButton);
#endif
        }
        public static void showCSWebView()
        {
            Debug.Log("[GPUnity][Call] showCSWebView");

#if UNITY_EDITOR && !UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_EDITOR showNoticeWebView not supported");
            }
#elif UNITY_STANDALONE
            {
                GamePotUnityPluginStandalone.showCSWebView();
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.showCSWebView();
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.showCSWebView();
#endif
        }

        public static void showAppStatusPopup(string status)
        {
            //Debug.Log("[GPUnity][Call] showAppStatusPopup - " + status);
            Debug.Log("[GPUnity][Call] showAppStatusPopup");

#if UNITY_EDITOR && !UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_EDITOR showAppStatusPopup not supported");
            }
#elif UNITY_STANDALONE
            {
                GamePotUnityPluginStandalone.showAppStatusPopup(status);
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.showAppStatusPopup(status);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.showAppStatusPopup(status);
#endif
        }
        
        public static void showConflictAccountPopup()
        {
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR showConflictAccountPopup not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE showConflictAccountPopup not supported");
            }
#elif UNITY_IOS
            {
                Debug.Log("GamePot - UNITY_IOS showConflictAccountPopup not supported");
            }
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.showConflictAccountPopup();
#endif
        }

        // public static void showAgreeDialog()
        // {
        //     NAgreeInfo info = null;
        //     showAgreeDialog(info);
        // }

        public static void showAgreeDialog(NAgreeInfo info = null)
        {
            Debug.Log("[GPUnity][Call] showAgreeDialog");
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR DUMMY CALLBACK showAgreeDialog always success");
                // Temporary user info for UNITY_EDITOR Development mode
                if (GamePotEventListener.s_instance != null)
                {
                    string successResultJson = "{\"agree\":true,\"agreePush\":true, \"agreeNight\":true}";
                    GamePotEventListener.s_instance.onAgreeDialogSuccess(successResultJson);
                }
                else
                {
                    Debug.LogError("GamePot UNITY_EDITOR listener is NULL");
                }
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE DUMMY CALLBACK showAgreeDialog always success");
                // Temporary user info for UNITY_STANDALONE Development mode
                if (GamePotEventListener.s_instance != null)
                {
                    string successResultJson = "{\"agree\":true,\"agreePush\":true, \"agreeNight\":true}";
                    GamePotEventListener.s_instance.onAgreeDialogSuccess(successResultJson);
                }
                else
                {
                    Debug.LogError("GamePot UNITY_STANDALONE listener is NULL");
                }
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.showAgreeDialog(info != null ? info.ToJson().ToString() : null);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.showAgreeDialog(info != null ? info.ToJson().ToString() : null);
#endif
        }

        /// <summary>
        /// Show Agree Popup
        /// </summary>
        /// <param name="cbShowAgree">Callback Function</param>
        public static void showAgreeDialog(GamePotCallbackDelegate.CB_ShowAgree cbShowAgree)
        {
            GamePotEventListener.cbShowAgree = cbShowAgree;
            showAgreeDialog(null, cbShowAgree);
        }

        /// <summary>
        /// Show Agree Popup
        /// </summary>
        /// <param name="info"></param>
        /// <param name="cbShowAgree">Callback Function</param>
        public static void showAgreeDialog(NAgreeInfo info, GamePotCallbackDelegate.CB_ShowAgree cbShowAgree)
        {
            GamePotEventListener.cbShowAgree = cbShowAgree;
            showAgreeDialog(info);
        }

        public static void setVoidBuilder(NVoidInfo info)
        {
            Debug.Log("[GPUnity][Call] setVoidBuilder");
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR setVoidBuilder not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE setVoidBuilder not supported");
            }
#elif UNITY_IOS
             GamePotUnityPluginiOS.setVoidOption(info != null ? info.ToJson().ToString() : null);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.setVoidBuilder(info != null ? info.ToJson().ToString() : null);
#endif
        }

        public static void setVoidDialogHelpURL(string url)
        {
            Debug.Log("[GPUnity][Call] setVoidDialogHelpURL - " + url);
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR setVoidDialogHelpURL not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE setVoidDialogHelpURL not supported");
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.setVoidDialogHelpURL(url);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.setVoidDialogHelpURL(url);
#endif
        }


        public static void showVoidDialogDebug()
        {
            Debug.Log("[GPUnity][Call] showVoidViewDebug");
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR showVoidViewDebug not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE showVoidViewDebug not supported");
            }
#elif UNITY_IOS
        GamePotUnityPluginiOS.showVoidViewDebug();
#elif UNITY_ANDROID
        GamePotUnityPluginAOS.showVoidDialogDebug();
#endif
        }


        public static void showTerms()
        {
            Debug.Log("[GPUnity][Call] showTerms");
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR showTerms not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE showTerms not supported");
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.showTerms();
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.showTerms();
#endif
        }

        public static void showPrivacy()
        {
            Debug.Log("[GPUnity][Call] showPrivacy");
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR showPrivacy not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE showPrivacy not supported");
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.showPrivacy();
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.showPrivacy();
#endif
        }

        public static void showNotice(bool showTodayButton)
        {
            Debug.Log("[GPUnity][Call] showNotice");
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR showNotice not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE showNotice not supported");
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.showNotice(showTodayButton);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.showNotice(showTodayButton);
#endif
        }

        public static void showNotice()
        {
            showNotice(true);
        }

        public static void showEvent(string type)
        {
            Debug.Log("[GPUnity][Call] showEvent");
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR showEvent not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE showEvent not supported");
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.showEvent(type);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.showEvent(type);
#endif
        }

        public static void showFaq()
        {
            Debug.Log("[GPUnity][Call] showFaq");
#if UNITY_EDITOR && !UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_EDITOR showEvent not supported");
            }
#elif UNITY_STANDALONE
            {
                GamePotUnityPluginStandalone.showFaq();
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.showFaq();
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.showFaq();
#endif
        }

        public static void d(string errCode, string errMessage)
        {
            Debug.Log("[GPUnity][Call] d");
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR sendLog d not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE sendLog d not supported");
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.sendLog("d", errCode, errMessage);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.sendLog("d", errCode, errMessage);
#endif
        }

        public static void i(string errCode, string errMessage)
        {
            Debug.Log("[GPUnity][Call] i");
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR sendLog i not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE sendLog i not supported");
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.sendLog("i", errCode, errMessage);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.sendLog("i", errCode, errMessage);
#endif
        }

        public static void w(string errCode, string errMessage)
        {
            Debug.Log("[GPUnity][Call] w");
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR sendLog w not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE sendLog w not supported");
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.sendLog("w", errCode, errMessage);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.sendLog("w", errCode, errMessage);
#endif
        }

        public static void e(string errCode, string errMessage)
        {
            Debug.Log("[GPUnity][Call] e");
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR sendLog e not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE sendLog e not supported");
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.sendLog("e", errCode, errMessage);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.sendLog("e", errCode, errMessage);
#endif
        }

        public static void setLoggerUserid(string userid)
        {
            Debug.Log("[GPUnity][Call] setLoggerUserid");
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR setLoggerUserid not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE setLoggerUserid not supported");
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.setLoggerUserid(userid);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.setLoggerUserid(userid);
#endif
        }

        public static void showAchievement()
        {
            Debug.Log("[GPUnity][Call] showAchievement");
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR showAchievement not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE showAchievement not supported");
            }
#elif UNITY_IOS
            {
                Debug.Log("GamePot - UNITY_IOS showAchievement not supported");
            }
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.showAchievement();
#endif
        }

        public static void showLeaderboard()
        {
            Debug.Log("[GPUnity][Call] showLeaderboard");
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR showLeaderboard not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE showLeaderboard not supported");
            }
#elif UNITY_IOS
            {
                Debug.Log("GamePot - UNITY_IOS showLeaderboard not supported");
            }
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.showLeaderboard();
#endif
        }

        public static void unlockAchievement(string achievementId)
        {
            Debug.Log("[GPUnity][Call] unlockAchievement - " + achievementId);
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR unlockAchievement not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE unlockAchievement not supported");
            }
#elif UNITY_IOS
            {
                Debug.Log("GamePot - UNITY_IOS unlockAchievement not supported");
            }
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.unlockAchievement(achievementId);
#endif
        }

        public static void incrementAchievement(string achievementId, string count)
        {
            Debug.Log("[GPUnity][Call] incrementAchievement - " + achievementId + ", " + count);
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR incrementAchievement not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE incrementAchievement not supported");
            }
#elif UNITY_IOS
            {
                Debug.Log("GamePot - UNITY_IOS incrementAchievement not supported");
            }
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.incrementAchievement(achievementId, count);
#endif
        }

        public static void submitScoreLeaderboard(string leaderBoardId, string leaderBoardScore)
        {
            Debug.Log("[GPUnity][Call] submitScoreLeaderboard - " + leaderBoardId + ", " + leaderBoardScore);
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR submitScoreLeaderboard not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE submitScoreLeaderboard not supported");
            }
#elif UNITY_IOS
            {
                Debug.Log("GamePot - UNITY_IOS submitScoreLeaderboard not supported");
            }
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.submitScoreLeaderboard(leaderBoardId, leaderBoardScore);
#endif
        }

        public static void loadAchievement()
        {
            Debug.Log("[GPUnity][Call] loadAchievement");
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR loadAchievement not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE loadAchievement not supported");
            }
#elif UNITY_IOS
            {
                Debug.Log("GamePot - UNITY_IOS loadAchievement not supported");
            }
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.loadAchievement();
#endif
        }

        public static void purchaseThirdPayments(string productId)
        {
            purchaseThirdPayments(productId, "", "", "", "");
        }

        public static void purchaseThirdPayments(string productId, string uniqueId)
        {
            purchaseThirdPayments(productId, uniqueId, "", "", "");
        }

        public static void purchaseThirdPayments(string productId, string uniqueId, string serverId, string playerId, string etc)
        {
            Debug.Log("[GPUnity][Call] purchaseThirdPayments - " + productId + ", " + uniqueId + ", " + serverId + ", " + playerId + ", " + etc);
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR purchaseThirdPayments not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE purchaseThirdPayments not supported");
            }
#elif UNITY_IOS
            {
                Debug.Log("GamePot - UNITY_IOS purchaseThirdPayments not supported");
            }
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.purchaseThirdPayments(productId, uniqueId, serverId, playerId, etc);
#endif
        }


        /// <summary>
        /// Purchase Thrid Party Payments
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="cbPurchase">Callback Function</param>
        public static void purchaseThirdPayments(string productId, GamePotCallbackDelegate.CB_Purchase cbPurchase)
        {
            GamePotEventListener.cbPurchase = cbPurchase;
            purchaseThirdPayments(productId, "", "", "", "");
        }

        /// <summary>
        /// Purchase Thrid Party Payments
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="uniqueId"></param>
        /// <param name="cbPurchase">Callback Function</param>
        public static void purchaseThirdPayments(string productId, string uniqueId, GamePotCallbackDelegate.CB_Purchase cbPurchase)
        {
            GamePotEventListener.cbPurchase = cbPurchase;
            purchaseThirdPayments(productId, uniqueId, "", "", "");
        }

        /// <summary>
        /// Purchase Thrid Party Payments
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="uniqueId"></param>
        /// <param name="serverId"></param>
        /// <param name="playerId"></param>
        /// <param name="etc"></param>
        /// <param name="cbPurchase">Callback Function</param>
        public static void purchaseThirdPayments(string productId, string uniqueId, string serverId, string playerId, string etc, GamePotCallbackDelegate.CB_Purchase cbPurchase)
        {
            GamePotEventListener.cbPurchase = cbPurchase;
            purchaseThirdPayments(productId, uniqueId, serverId, playerId, etc);
        }


        public static NPurchaseItem[] getPurchaseThirdPaymentsItems()
        {
            Debug.Log("[GPUnity][Call] getPurchaseThirdPaymentsItems");
#if UNITY_EDITOR
            Debug.Log("GamePot - UNITY_EDITOR getPurchaseThirdPaymentsItems always returns empty");
            return new NPurchaseItem[0];
#elif UNITY_STANDALONE
            Debug.Log("GamePot - UNITY_STANDALONE getPurchaseThirdPaymentsItems always returns empty");
            return new NPurchaseItem[0];
#elif UNITY_IOS
            Debug.Log("GamePot - UNITY_IOS getPurchaseThirdPaymentsItems always returns empty");
            return new NPurchaseItem[0];
#elif UNITY_ANDROID
            string result = "";
            result = GamePotUnityPluginAOS.getPurchaseThirdPaymentsItems();
            NPurchaseItem[] itemData = JsonMapper.ToObject<NPurchaseItem[]>(result);
            return itemData;
#else
            Debug.Log("GamePot - getPurchaseThirdPaymentsItems not supported on this platform");
            return new NPurchaseItem[0];
#endif
        }

        public static bool characterInfo(GamePotSendLogCharacter info)
        {
            Debug.Log("[GPUnity][Call] characterInfo");
            bool result = false;

            if (info == null)
            {
                Debug.LogError("GamePotSendLogCharacter is null");
                return false;
            }
#if UNITY_EDITOR && !UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_EDITOR characterInfo always returns false");
            }
#elif UNITY_STANDALONE
            {
                result = GamePotUnityPluginStandalone.characterInfo(info.toString());
            }
#elif UNITY_IOS
            result = GamePotUnityPluginiOS.characterInfo(info.toString());
#elif UNITY_ANDROID
            result = GamePotUnityPluginAOS.characterInfo(info.toString());
#endif
            return result;
        }

        public static bool characterInfo(GamePotSendLogCharacter info, GamePotCallbackDelegate.CB_Common callback)
        {
            bool result = false;
#if UNITY_STANDALONE
            {
                result = GamePotUnityPluginStandalone.characterInfo(info.toString(), callback);
            }
#else
            Debug.Log("GamePot - characterInfo (with callback) support only UNITY_STANDALONE");
#endif
            return result;
        }


        public static string getPushToken()
        {
            Debug.Log("[GPUnity][Call] getFCMToken");
            string token = "";

#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR getFCMToken always returns empty string");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE getFCMToken always returns empty string");
            }
#elif UNITY_IOS
            token = GamePotUnityPluginiOS.getPushToken();
#elif UNITY_ANDROID
            token = GamePotUnityPluginAOS.getFCMToken();
#endif

            return token;
        }

        public static string getFCMToken()
        {
            Debug.Log("[GPUnity][Call] getFCMToken");
            string token = "";

#if UNITY_EDITOR
            {
                Debug.Log("GamePot -  UNITY_EDITOR getFCMToken always returns empty string");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE getFCMToken always returns empty string");
            }
#elif UNITY_IOS
            token = GamePotUnityPluginiOS.getFCMToken();
#elif UNITY_ANDROID
            token = GamePotUnityPluginAOS.getFCMToken();
#endif

            return token;
        }


        public static void showRefund()
        {
            Debug.Log("[GPUnity][Call] showRefund");

#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR showRefund not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE showRefund not supported");
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.showRefund();
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.showRefund();
#endif
        }

        public static void sendPurchaseByThirdPartySDK(string productId, string transactionId, string currency, double price, string store, string paymentId, string uniqueId)
        {
            Debug.Log("[GPUnity][Call] sendPurchaseByThirdPartySDK");

#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR sendPurchaseByThirdPartySDK not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE sendPurchaseByThirdPartySDK not supported");
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.sendPurchaseByThirdPartySDK(productId, transactionId, currency, price, store, paymentId, uniqueId);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.sendPurchaseByThirdPartySDK(productId, transactionId, currency, price, store, paymentId, uniqueId);
#endif
        }


        public static void loginByThirdPartySDK(string userId)
        {
            Debug.Log("[GPUnity][Call] loginByThirdPartySDK");

#if UNITY_EDITOR && !UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_EDITOR loginByThirdPartySDK not supported");
            }
#elif UNITY_STANDALONE
            {
                GamePotUnityPluginStandalone.loginByThirdPartySDK(userId);
                //Debug.Log("GamePot - UNITY_STANDALONE loginByThirdPartySDK not supported");
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.loginByThirdPartySDK(userId);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.loginByThirdPartySDK(userId);
#endif
        }

        public static void loginByThirdPartySDK(string userId, GamePotCallbackDelegate.CB_Login cbLogin)
        {
            GamePotEventListener.cbLogin = cbLogin;
            loginByThirdPartySDK(userId);
        }

        public static string getCountry()
        {
            Debug.Log("[GPUnity][Call] getCountry");
            string result = "";

#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR getCountry not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE getCountry not supported");
            }
#elif UNITY_IOS
            result = GamePotUnityPluginiOS.getCountry();
#elif UNITY_ANDROID
            result = GamePotUnityPluginAOS.getCountry();
#endif

            return result;
        }


        public static string getRemoteIP()
        {
            Debug.Log("[GPUnity][Call] getRemoteIP");
            string result = "";

#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR getRemoteIP not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE getRemoteIP not supported");
            }
#elif UNITY_IOS
            result = GamePotUnityPluginiOS.getRemoteIP();
#elif UNITY_ANDROID
            result = GamePotUnityPluginAOS.getRemoteIP();
#endif

            return result;
        }

        public static String[] getGDPRCheckedList()
        {
            Debug.Log("[GPUnity][Call] getGDPRCheckedList");
            string result = "";

#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR getGDPRCheckedList not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE getGDPRCheckedList not supported");
            }
#elif UNITY_IOS
            result = GamePotUnityPluginiOS.getGDPRCheckedList();
#elif UNITY_ANDROID
            result = GamePotUnityPluginAOS.getGDPRCheckedList();
#endif

            String[] resultArray = JsonMapper.ToObject<String[]>(result);
            return resultArray;
        }

        public static void safetyToast(String message)
        {
            Debug.Log("[GPUnity][Call] safetyToast");
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR safetyToast not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE safetyToast not supported");
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.safetyToast(message);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.safetyToast(message);
#endif
        }

        public static void requestTrackingAuthorization()
        {
            Debug.Log("[GPUnity][Call] requestTrackingAuthorization");
#if !UNITY_EDITOR && UNITY_IOS
                GamePotUnityPluginiOS.requestTrackingAuthorization();
#else
            {
                Debug.Log("GamePot - requestTrackingAuthorization not supported in this platform");
            }
#endif
        }

        /// <summary>
        /// Request TrackingAuthorization
        /// </summary>
        /// <param name="cbRequestTrackingAuthorization">Callback Function</param>
        public static void requestTrackingAuthorization(GamePotCallbackDelegate.CB_RequestTrackingAuthorization cbRequestTrackingAuthorization)
        {
            GamePotEventListener.cbRequestTrackingAuthorization = cbRequestTrackingAuthorization;
            requestTrackingAuthorization();
        }

        public static void setAutoAgree(bool autoAgree)
        {
            Debug.Log("[GPUnity][Call] setAutoAgree");
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR setAutoAgree not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE setAutoAgree not supported");
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.setAutoAgree(autoAgree);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.setAutoAgree(autoAgree);
#endif
        }

        public static void setAutoAgreeBuilder(NAgreeInfo info)
        {
            Debug.Log("[GPUnity][Call] setAutoAgreeBuilder");
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR setAutoAgreeBuilder not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE setAutoAgreeBuilder not supported");
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.setAutoAgreeBuilder(info != null ? info.ToJson().ToString() : null);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.setAutoAgreeBuilder(info != null ? info.ToJson().ToString() : null);
#endif
        }

        public static void checkAppStatus()
        {
            Debug.Log("[GPUnity][Call] checkAppStatus");
#if UNITY_EDITOR && !UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_EDITOR checkAppStatus not supported");
            }
#elif UNITY_STANDALONE
            {
                GamePotUnityPluginStandalone.checkAppStatus();
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.checkAppStatus();
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.checkAppStatus();
#endif
        }

        /// <summary>
        /// Check App Status (점검, 업데이트 확인)
        /// </summary>
        /// <param name="cbCheckAppStatus">Callback Function</param>
        public static void checkAppStatus(GamePotCallbackDelegate.CB_CheckAppStatus cbCheckAppStatus)
        {
            GamePotEventListener.cbCheckAppStatus = cbCheckAppStatus;
            checkAppStatus();
        }


        public static void sendAgreeEmail(string email)
        {
            Debug.Log("[GPUnity][Call] sendAgreeEmail");
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR sendAgreeEmail not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE sendAgreeEmail not supported");
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.sendAgreeEmail(email);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.sendAgreeEmail(email);
#endif
        }

        public static void sendAgreeEmail(string email, GamePotCallbackDelegate.CB_SendAgreeEmail cbSendAgreeEmail)
        {
            GamePotEventListener.cbSendAgreeEmail = cbSendAgreeEmail;
            sendAgreeEmail(email);
        }

        public static void checkAgreeEmail(string email, string key)
        {
            Debug.Log("[GPUnity][Call] checkAgreeEmail");
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR checkAgreeEmail not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE checkAgreeEmail not supported");
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.checkAgreeEmail(email, key);
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.checkAgreeEmail(email, key);
#endif
        }

        public static void checkAgreeEmail(string email, string key, GamePotCallbackDelegate.CB_CheckAgreeEmail cbCheckAgreeEmail)
        {
            GamePotEventListener.cbCheckAgreeEmail = cbCheckAgreeEmail;
            checkAgreeEmail(email, key);
        }

        public static void setAgreeInfo(NAgreeResultInfo agreeInfo)
        {
            Debug.Log("[GPUnity][Call] setAgreeInfo");
#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR setAgreeInfo not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE setAgreeInfo not supported");
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.setAgreeInfo(agreeInfo.ToJson());
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.setAgreeInfo(agreeInfo.ToJson());
#endif
        }

        public static void setAgreeInfo(NAgreeResultInfo agreeInfo, GamePotCallbackDelegate.CB_SetAgreeInfo cbSetAgreeInfo)
        {
            GamePotEventListener.cbSetAgreeInfo = cbSetAgreeInfo;
            setAgreeInfo(agreeInfo);
        }
        public static void enableGPG(bool flag)
        {
            Debug.Log("[GPUnity][Call] enableGPG");
#if UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE enableGPG not supported");
            }
#elif UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR enableGPG not supported");
            }
#elif UNITY_IOS
            {
                Debug.Log("GamePot - UNITY_IOS enableGPG not supported");
            }
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.enableGPG(flag);
#endif
        }

        public static string getProjectId()
        {
            Debug.Log("[GPUnity][Call] getProjectId");
            string token = "";

#if UNITY_EDITOR
            {
                Debug.Log("GamePot - UNITY_EDITOR getProjectId always returns empty string");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE getProjectId always returns empty string");
            }
#elif UNITY_IOS
            token = GamePotUnityPluginiOS.getProjectId();
#elif UNITY_ANDROID
            token = GamePotUnityPluginAOS.getProjectId();
#endif

            return token;
        }
        
        public static void showCouponWebView()
        {
            Debug.Log("[GPUnity][Call] showCouponWebView");

#if UNITY_EDITOR && !UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_EDITOR showCouponWebView not supported");
            }
#elif UNITY_STANDALONE
            {
                Debug.Log("GamePot - UNITY_STANDALONE showCouponWebView not supported");
            }
#elif UNITY_IOS
            GamePotUnityPluginiOS.showCouponWebView();
#elif UNITY_ANDROID
            GamePotUnityPluginAOS.showCouponWebView();
#endif
        }

    }
}