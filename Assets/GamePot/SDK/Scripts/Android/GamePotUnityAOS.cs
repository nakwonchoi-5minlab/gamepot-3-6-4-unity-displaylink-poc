using System.Collections.Generic;
using UnityEngine;
using static NCommon;


namespace GamePotUnityAOS
{
    public class GamePotUnityPluginAOS
    {
        private static bool _initialized = false;
        private static AndroidJavaClass mGamePotClass = null;

        //Native Android Class Name
        protected const string GAMEPOT_CLASS_NAME = "io.gamepot.unity.plugin.GamePotUnityPlugin";


        public static void initPlugin()
        {
            if (GamePotEventListener.s_instance != null)
                return;

            // NATIVE LIBRARY
            try
            {
                mGamePotClass = new AndroidJavaClass(GAMEPOT_CLASS_NAME + "$SDK");
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex);
            }


            if (mGamePotClass != null)
            {
                Debug.Log("GamePotUnityPluginAOS Success!!!");
                _initialized = true;
            }
            else
            {
                Debug.LogError("GamePotUnityPluginAOS  FAIL!!!");
            }


            if (GamePotEventListener.s_instance == null)
            {
                Debug.Log("GamePot - Creating GamePot Android Native Bridge Receiver");
                new GameObject("GamePotAndroidManager", typeof(GamePotEventListener));
            }
        }


        public static void setListener(IGamePot inter)
        {
            Debug.Log("GamePotUnityPluginAOS::setListener");
            if (GamePotEventListener.s_instance != null)
            {
                GamePotEventListener.s_instance.setListener(inter);
            }
            else
            {
                Debug.LogError("GamePot - GamePotEventListener instance is NULL");
            }
        }

        //////////////////////
        // Common API
        //////////////////////
        public static void changeProjectId(string projectId)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("changeProjectId", projectId, "");
        }

        public static void changeProjectId(string projectId, string sens_Push_Service_id)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("changeProjectId", projectId, sens_Push_Service_id);
        }

        public static void changeProjectId(string projectId, string sens_Push_Service_id, string region)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("changeProjectId", projectId, sens_Push_Service_id, region);
        }

        public static bool getShowWithDrawPopup()
        {
            bool result = false;
            if (mGamePotClass != null)
            {
                result = mGamePotClass.CallStatic<bool>("getShowWithDrawPopup");
            }
            return result;
        }

        public static void setShowWithDrawPopup(bool enable)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("setShowWithDrawPopup", enable);
        }

        public static void setLanguage(int gameLanguage)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("setLanguage", gameLanguage);
        }

        //////////////////////
        // Channel API
        //////////////////////
        public static int sendLocalPush(System.DateTime sendDate, string title, string message)
        {
            int result = -1;
            if (mGamePotClass != null)
                result = mGamePotClass.CallStatic<int>("sendLocalPush", sendDate.ToString("yyyy-MM-dd HH:mm:ss"), title, message);
            return result;
        }


        public static void cancelLocalPush(int pushId)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic<bool>("cancelLocalPush", pushId);
        }


        public static string getLanguage()
        {
            if (mGamePotClass != null)
            {
                return mGamePotClass.CallStatic<string>("getLanguage");
            }
            return "";
        }

        public static string getLastLoginType()
        {
            if (mGamePotClass != null)
            {
                return mGamePotClass.CallStatic<string>("getLastLoginType");
            }
            return "";
        }
        public static void loginWithEmail(string username, string password)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("loginWithEmail", username, password);
        }
        public static void login(NCommon.LoginType loginType)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("login", loginType.ToString());
        }

        public static void showLoginWithUI(string info)
        {
            if (mGamePotClass != null)
            {
                mGamePotClass.CallStatic("showLoginWithUI", info);
            }
        }

        //public static void showLoginWithUI(string[] loginTypes)
        //{
        //    if (mGamePotClass != null)
        //    {
        //        mGamePotClass.CallStatic("showLoginWithUI", javaArrayFromCS(loginTypes));
        //    }
        //}

        //private static AndroidJavaObject javaArrayFromCS(string[] values)
        //{
        //    AndroidJavaClass arrayClass = new AndroidJavaClass("java.lang.reflect.Array");
        //    AndroidJavaObject arrayObject = arrayClass.CallStatic<AndroidJavaObject>("newInstance", new AndroidJavaClass("java.lang.String"), values.Length);
        //    for (int i = 0; i < values.Length; i++)
        //    {
        //        arrayClass.CallStatic("set", arrayObject, i, new AndroidJavaObject("java.lang.String", values[i]));
        //    }
        //    return arrayObject;
        //}




        //public static void closeLoginWithUI()
        //{
        //    if (mGamePotClass != null)
        //        mGamePotClass.CallStatic("closeLoginWithUI");
        //}

        // public static void addAd(NCommon.AdType adType)
        // {
        //     if (mGamePotClass != null)
        //         mGamePotClass.CallStatic("addAd", adType.ToString());
        // }
        // public static void tracking(NCommon.AdActions adActions, string adjustKey)
        // {
        //     if (mGamePotClass != null)
        //         mGamePotClass.CallStatic("tracking", adActions.ToString(), adjustKey);
        // }

        // public static void tracking(NCommon.AdActions adActions, TrackingInfo trackingInfo)
        // {
        //     if (mGamePotClass != null)
        //         mGamePotClass.CallStatic("tracking", adActions.ToString(), trackingInfo.trackingInfoConvertToJson());
        // }

        // public static void setSandbox(bool enable)
        // {
        //     if (mGamePotClass != null)
        //         mGamePotClass.CallStatic("setSandbox", enable);
        // }

        public static void deleteMember()
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("deleteMember");
        }

        public static void logout()
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("logout");
        }

        public static string getConfig(string key)
        {
            if (mGamePotClass != null)
            {
                return mGamePotClass.CallStatic<string>("getConfig", key);
            }
            return "";
        }

        public static string getConfigs()
        {
            if (mGamePotClass != null)
            {
                return mGamePotClass.CallStatic<string>("getConfigs");
            }
            return "";
        }

        public static string getUserData()
        {
            if (mGamePotClass != null)
            {
                return mGamePotClass.CallStatic<string>("getUserData");
            }
            return "";
        }

        public static void setUserData(string userData)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("setUserData", userData);
        }

        public static void coupon(string couponNumber, string userData)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("coupon", couponNumber, userData);
        }

        public static void purchase(string productId, string uniqueId, string serverId, string playerId, string etc)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("purchase", productId, uniqueId, serverId, playerId, etc);
        }

        public static void purchase(string productId, string uniqueId, string serverId, string playerId, string etc, string offerId)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("purchase", productId, uniqueId, serverId, playerId, etc, offerId);
        }

        public static void onInitialized()
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("onInitialized");
        }

        public static bool isSetupInProgress()
        {
            if (mGamePotClass != null)
                return mGamePotClass.CallStatic<bool>("isSetupInProgress");
            return false;
        }

        public static void showStagePay(string appId, string pg, string productId)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("showStagePay", appId, pg, productId);
        }

        public static void showStagePay(string appId, string pg, string productId, string uniqueId, string serverId, string playerId, string etc, string options, string metadata)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("showStagePay", appId, pg, productId, uniqueId, serverId, playerId, etc, options, metadata);
        }

        public static string getPurchaseItems()
        {
            if (mGamePotClass != null)
            {
                return mGamePotClass.CallStatic<string>("getPurchaseItems");
            }
            return "";
        }

        public static void getPurchaseDetailListAsync()
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("getPurchaseDetailListAsync");
        }

        public static bool isLinked(string linkType)
        {
            if (mGamePotClass != null)
            {
                return mGamePotClass.CallStatic<bool>("isLinked", linkType);
            }
            return false;
        }


        public static string getLinkedList()
        {
            if (mGamePotClass != null)
            {
                return mGamePotClass.CallStatic<string>("getLinkedList");
            }
            return "";
        }

        public static void createLinking(NCommon.LinkingType linkType)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("createLinking", linkType.ToString());
        }

        public static void deleteLinking(NCommon.LinkingType linkType)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("deleteLinking", linkType.ToString());
        }

        public static void addChannel(NCommon.ChannelType channelType)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("addChannel", channelType.ToString());
        }

        public static void setPush(bool pushEnable)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("setPush", pushEnable);
        }

        public static void setPushNight(bool pushEnable)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("setPushNightStatus", pushEnable);
        }

        public static void setPushAd(bool pushEnable)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("setPushAdStatus", pushEnable);
        }

        public static void setPushState(bool pushEnable, bool nightPushEnable, bool adPushEnable)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("setPushStatus", pushEnable, nightPushEnable, adPushEnable);
        }

        public static string getPushStatus()
        {
            if (mGamePotClass != null)
            {
                return mGamePotClass.CallStatic<string>("getPushStatus");
            }
            return "";
        }

        public static void showNoticeWebView()
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("showNoticeWebView");
        }

        public static void showWebView(string url)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("showWebView", url);
        }

        public static void showWebView(string url, bool hasBackButton)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("showWebView", url, hasBackButton);
        }

        public static void showCSWebView()
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("showCSWebView");
        }

        public static void showAppStatusPopup(string status)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("showAppStatusPopup", status);
        }

        public static void showAgreeDialog(string info)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("showAgreeDialog", info);
        }

        public static void setVoidBuilder(string info)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("setVoidBuilder", info);
        }

        public static void showVoidDialogDebug()
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("showVoidDialogDebug");
        }

        public static void setVoidDialogHelpURL(string url)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("setVoidDialogHelpURL", url);
        }

        public static void showTerms()
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("showTerms");
        }

        public static void showPrivacy()
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("showPrivacy");
        }

        public static void showEvent(string type)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("showEvent", type);
        }

        public static void showNotice()
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("showNotice");
        }

        public static void showNotice(bool showTodayButton)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("showNotice", showTodayButton);
        }

        public static void showFaq()
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("showFaq");
        }

        public static void setLoggerUserid(string userid)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("setLoggerUserid", userid);
        }

        public static void sendLog(string type, string errCode, string errMessage)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("sendLog", type, errCode, errMessage);
        }

        public static void showAchievement()
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("showAchievement");
        }

        public static void showLeaderboard()
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("showLeaderboard");
        }

        public static void unlockAchievement(string achievementId)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("unlockAchievement", achievementId);
        }

        public static void incrementAchievement(string achievementId, string count)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("incrementAchievement", achievementId, count);
        }

        public static void submitScoreLeaderboard(string leaderBoardId, string leaderBoardScore)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("submitScoreLeaderboard", leaderBoardId, leaderBoardScore);
        }

        public static void loadAchievement()
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("loadAchievement");
        }

        public static void purchaseThirdPayments(string productId, string uniqueId, string serverId, string playerId, string etc)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("purchaseThirdPayments", productId, uniqueId, serverId, playerId, etc);
        }

        public static string getPurchaseThirdPaymentsItems()
        {
            if (mGamePotClass != null)
            {
                return mGamePotClass.CallStatic<string>("getPurchaseThirdPaymentsItems");
            }
            return "";
        }

        public static bool characterInfo(string data)
        {
            if (mGamePotClass != null)
            {
                return mGamePotClass.CallStatic<bool>("characterInfo", data);
            }
            return false;
        }

        public static string getFCMToken()
        {
            if (mGamePotClass != null)
            {
                return mGamePotClass.CallStatic<string>("getFCMToken");
            }
            Debug.Log("GamePot -  GamePotUnityPluginAOS not initialized!!");
            return "";
        }


        public static void showRefund()
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("showRefund");

        }

        public static void sendPurchaseByThirdPartySDK(string productId, string transactionId, string currency, double price, string store, string paymentId, string uniqueId)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("sendPurchaseByThirdPartySDK", productId, transactionId, currency, price, store, paymentId, uniqueId);
        }

        public static void loginByThirdPartySDK(string userId)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("loginByThirdPartySDK", userId);
        }

        public static string getCountry()
        {
            string result = "";

            if (mGamePotClass != null)
                result = mGamePotClass.CallStatic<string>("getCountry");

            return result;
        }


        public static string getRemoteIP()
        {
            string result = "";

            if (mGamePotClass != null)
                result = mGamePotClass.CallStatic<string>("getRemoteIP");

            return result;
        }

        public static string getGDPRCheckedList()
        {
            string result = "";

            if (mGamePotClass != null)
                result = mGamePotClass.CallStatic<string>("getGDPRCheckedList");

            return result;
        }

        public static void safetyToast(string message)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("safetyToast", message);
        }

        public static void setAutoAgree(bool autoAgree)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("setAutoAgree", autoAgree);
        }

        public static void setAutoAgreeBuilder(string info)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("setAutoAgreeBuilder", info);
        }

        public static void checkAppStatus()
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("checkAppStatus");
        }

        public static void sendAgreeEmail(string email)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("sendAgreeEmail", email);
        }

        public static void checkAgreeEmail(string email, string key)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("checkAgreeEmail", email, key);
        }

        public static void setAgreeInfo(string data)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("setAgreeInfo", data);
        }

        public static void enableGPG(bool flag)
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("enableGPG", flag);
        }

        public static string getProjectId()
        {
            if (mGamePotClass != null)
            {
                return mGamePotClass.CallStatic<string>("getProjectId");
            }
            return "";
        }

        public static void showCouponWebView()
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("showCouponWebView");
        }
        
        public static void signInWithGameService()
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("signInWithGameService");
        } 
        
        public static void showConflictAccountPopup()
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("showConflictAccountPopup");
        }
        
        public static void signInResolverConflict()
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("signInResolverConflict");
        }
        
        public static void signInSwitchAccount()
        {
            if (mGamePotClass != null)
                mGamePotClass.CallStatic("signInSwitchAccount");
        }
    }
}