using UnityEngine;
using System.Runtime.InteropServices;
using System;
using GamePotUnity;

#if UNITY_IOS

namespace GamePotUnityiOS
{

    public class GamePotUnityPluginiOS
    {
        #region iOS DLL native functions

        [DllImport("__Internal")]
        private static extern void changeProjectIdByUnity(String projectId);

        [DllImport("__Internal")]
        private static extern void changeProjectIdByUnity(String projectId,String sens_Push_Service_id);

        [DllImport("__Internal")]
        private static extern void changeProjectIdByUnityWithRegion(String projectId, String sens_Push_Service_id, String region);

        [DllImport("__Internal")]
        private static extern bool getShowWithDrawPopupByUnity();

        [DllImport("__Internal")]
        private static extern void setShowWithDrawPopupByUnity(bool enable);

        [DllImport("__Internal")]
        private static extern void pluginVersionByUnity(String version);

        [DllImport("__Internal")]
        private static extern void joinChannelByUnity(String prevChannel);

        [DllImport("__Internal")]
        private static extern void leaveChannelByUnity(String prevChannel);

        [DllImport("__Internal")]
        private static extern void sendMessageByUnity(String prevChannel, String message);

        [DllImport("__Internal")]
        private static extern string getLastLoginTypeByUnity();

        [DllImport("__Internal")]
        private static extern void loginByUnity(string loginType);

        [DllImport("__Internal")]
        private static extern void showLoginWithUIByUnity(string info);

        [DllImport("__Internal")]
        private static extern void deleteMemberByUnity();

        [DllImport("__Internal")]
        private static extern string getConfigByUnity(String key);

        [DllImport("__Internal")]
        private static extern string getConfigsByUnity();

        [DllImport("__Internal")]
        private static extern void purchaseByUnity(String productId, String uniqueId, String serverId, String playerId, String etc);

        [DllImport("__Internal")]
        private static extern void couponByUnity(String couponNumber, String userData);

        [DllImport("__Internal")]
        private static extern void setLanguageByUnity(int gameLanguage);

        [DllImport("__Internal")]
        private static extern int sendLocalPushByUnity(string sdate, string title, string text);

        [DllImport("__Internal")]
        private static extern void cancelLocalPushByUnity(int pushId);

        [DllImport("__Internal")]
        private static extern string getLinkedListByUnity();

        [DllImport("__Internal")]
        private static extern bool isLinkedByUnity(string linkType);

        [DllImport("__Internal")]
        private static extern void logoutByUnity();

        [DllImport("__Internal")]
        private static extern void loginWithEmailByUnity(string loginType, string username, string password);

        [DllImport("__Internal")]
        private static extern void createLinkingByUnity(string linkType);

        [DllImport("__Internal")]
        private static extern void deleteLinkingByUnity(string linkType);

        [DllImport("__Internal")]
        private static extern void addChannelByUnity(string linkType);

        [DllImport("__Internal")]
        private static extern void enableGameCenterByUnity(bool enable);

        [DllImport("__Internal")]
        private static extern string getPurchaseItemByUnity();

        [DllImport("__Internal")]
        private static extern void getPurchaseDetailListAsyncByUnity();

        [DllImport("__Internal")]
        private static extern void setPushByUnity(bool pushEnable);

        [DllImport("__Internal")]
        private static extern void setPushNightByUnity(bool pushEnable);

        [DllImport("__Internal")]
        private static extern void setPushAdByUnity(bool pushEnable);

        [DllImport("__Internal")]
        private static extern void setPushStateByUnity(bool pushEnable, bool nightPushEnable, bool adPushEnable);

        [DllImport("__Internal")]
        private static extern string getPushStatusByUnity();

        [DllImport("__Internal")]
        private static extern void showNoticeWebViewByUnity();

        [DllImport("__Internal")]
        private static extern void showCSWebViewByUnity();

        [DllImport("__Internal")]
        private static extern void showWebViewByUnity(string url);

        [DllImport("__Internal")]
        private static extern void showWebViewWithFlagByUnity(string url, bool hasBackButton);
        [DllImport("__Internal")]
        private static extern void showAppStatusPopupByUnity(string status);

        [DllImport("__Internal")]
        private static extern void showAgreeDialogByUnity(string info);

        [DllImport("__Internal")]
        private static extern void showVoidViewDebugByUnity();

        [DllImport("__Internal")]
        private static extern void setVoidOptionByUnity(string info);

        [DllImport("__Internal")]
        private static extern void setVoidDialogHelpURLByUnity(string url);

        [DllImport("__Internal")]
        private static extern void showTermsByUnity();

        [DllImport("__Internal")]
        private static extern void showPrivacyByUnity();

        [DllImport("__Internal")]
        private static extern void sendLogByUnity(string type, string errCode, string errMessage);

        [DllImport("__Internal")]
        private static extern void setLoggerUseridByUnity(string userid);

        [DllImport("__Internal")]
        private static extern void showEventByUnity(string type);

        [DllImport("__Internal")]
        private static extern void showNoticeByUnity();

        [DllImport("__Internal")]
        private static extern void showNoticeWithFlagByUnity(bool showTodayButton);

        [DllImport("__Internal")]
        private static extern void showFaqByUnity();

        [DllImport("__Internal")]
        private static extern bool characterInfoByUnity(string info);

        [DllImport("__Internal")]
        private static extern string getPushTokenByUnity();

        [DllImport("__Internal")]
        private static extern string getFCMTokenByUnity();

        [DllImport("__Internal")]
        private static extern void showRefundByUnity();

        [DllImport("__Internal")]
        private static extern void sendPurchaseByThirdPartySDKByUnity(string productId, string transactionId, string currency, double price, string store, string paymentId, string uniqueId);

        [DllImport("__Internal")]
        private static extern void loginByThirdPartySDKByUnity(string userId);

        [DllImport("__Internal")]
        private static extern string getCountryByUnity();

        [DllImport("__Internal")]
        private static extern string getRemoteIPByUnity();

        [DllImport("__Internal")]
        private static extern string getGDPRCheckedListByUnity();

        [DllImport("__Internal")]
        private static extern void safetyToastByUnity(string message);

        [DllImport("__Internal")]
        private static extern void requestTrackingAuthorizationByUnity();

        [DllImport("__Internal")]
        private static extern void checkAppStatusByUnity();

        [DllImport("__Internal")]
        private static extern void setAutoAgreeByUnity(bool autoAgree);

        [DllImport("__Internal")]
        private static extern void setAutoAgreeBuilderByUnity(string info);

        [DllImport("__Internal")]
        private static extern void sendAgreeEmailByUnity(string email);

        [DllImport("__Internal")]
        private static extern void checkAgreeEmailByUnity(string email, string key);

        [DllImport("__Internal")]
        private static extern void setAgreeInfoByUnity(string info);

        [DllImport("__Internal")]
        private static extern void setUserDataByUnity(string userData);

        [DllImport("__Internal")]
        private static extern string getUserDataByUnity();

        [DllImport("__Internal")]
        private static extern string getProjectIdByUnity();

        [DllImport("__Internal")]
        private static extern void showCouponWebViewByUnity();

        #endregion


        public static void initPlugin()
        {
            if (GamePotEventListener.s_instance != null) return;

            Debug.Log("GamePot - Creating GamePot iOS Native Bridge Receiver");
            new GameObject("GamePotiOSManager", typeof(GamePotEventListener));
            pluginVersion(GamePot.UnityPluginVersion);

        }

        public static void setListener(IGamePot inter)
        {
            Debug.Log("GamePotEventListener::setListener");
            if (GamePotEventListener.s_instance != null)
            {
                GamePotEventListener.s_instance.setListener(inter);
            }
            else
            {
                Debug.LogError("GamePot - GamePotEventListener instance is NULL");
            }
        }
        
        public static void changeProjectId(String projectId)
        {
            changeProjectIdByUnity(projectId,"");
        }

        public static void changeProjectId(String projectId, String sens_Push_Service_id)
        {
            changeProjectIdByUnity(projectId,sens_Push_Service_id);
        }

        public static void changeProjectId(String projectId, String sens_Push_Service_id, String region)
        {
            changeProjectIdByUnityWithRegion(projectId, sens_Push_Service_id, region);
        }

        public static bool getShowWithDrawPopup()
        {
            return getShowWithDrawPopupByUnity();
        }

        public static void setShowWithDrawPopup(bool enable)
        {
            setShowWithDrawPopupByUnity(enable);
        }

        public static void pluginVersion(String version)
        {
            pluginVersionByUnity(version);
        }

        //////////////////////
        // Common API
        //////////////////////

        //////////////////////
        // Chat API
        //////////////////////

        //public static void joinChannel(String prevChannel)
        //{
        //    joinChannelByUnity(prevChannel);
        //}

        //public static void leaveChannel(String prevChannel)
        //{
        //    leaveChannelByUnity(prevChannel);
        //}

        //public static void sendMessage(String prevChannel, String message)
        //{
        //    Debug.Log("chatMessage - prevChannel :  " + prevChannel + " / message : " + message);
        //    sendMessageByUnity(prevChannel, message);
        //}

        // public static void addAd(NCommon.AdType adType)
        // {
        //     addAdByUnity(adType.ToString());
        // }

        // [DllImport("__Internal")]
        // private static extern void addAdByUnity(string adType);

        // public static void tracking(NCommon.AdActions adActions, string adjustKey)
        // {
        //     trackingByUnity(adActions.ToString(), adjustKey);
        // }

        // public static void tracking(NCommon.AdActions adActions, TrackingInfo trackingInfo)
        // {
        //     trackingByUnity(adActions.ToString(), trackingInfo.trackingInfoConvertToJson());
        // }

        // [DllImport("__Internal")]
        // private static extern void trackingByUnity(string adActions, string adjustKey);

        public static string getLastLoginType()
        {
            return getLastLoginTypeByUnity();
        }

        public static void login(NCommon.LoginType loginType)
        {
            loginByUnity(loginType.ToString());
        }

        //public static void showLoginWithUI(string[] loginTypes)
        //{
        //    int length = loginTypes.Length;
        //    showLoginWithUIByUnity(loginTypes, length);
        //}

        public static void showLoginWithUI(string info)
        {
            showLoginWithUIByUnity(info);
        }

        // public static void setSandbox(bool enable)
        // {
        //     setSandboxByUnity(enable);
        // }

        // [DllImport("__Internal")]
        // private static extern void setSandboxByUnity(bool enable);

        //public static void naverCafeInit()
        //{
        //    naverCafeInitByUnity();
        //}

        //[DllImport("__Internal")]
        //private static extern void naverCafeInitByUnity();

        // public static void naverCafeInitGlobal()
        // {
        //     naverCafeInitGlobalByUnity();
        // }

        // [DllImport("__Internal")]
        // private static extern void naverCafeInitGlobalByUnity();

        public static void deleteMember()
        {
            deleteMemberByUnity();
        }

        public static string getConfig(String key)
        {
            return getConfigByUnity(key);
        }

        public static string getConfigs()
        {
            return getConfigsByUnity();
        }

        public static void purchase(String productId, String uniqueId, String serverId, String playerId, String etc)
        {
            purchaseByUnity(productId, uniqueId, serverId, playerId, etc);
        }

        public static void coupon(String couponNumber, String userData)
        {
            couponByUnity(couponNumber, userData);
        }

        public static void setLanguage(int gameLanguage)
        {
            setLanguageByUnity(gameLanguage);
        }

        public static int sendLocalPush(string sdate, string title, string text)
        {
            return sendLocalPushByUnity(sdate, title, text);
        }

        public static void cancelLocalPush(int pushId)
        {
            cancelLocalPushByUnity(pushId);
        }

        public static string getLinkedList()
        {
            return getLinkedListByUnity();
        }

        public static bool isLinked(string linkType)
        {
            return isLinkedByUnity(linkType);
        }

        public static void loginWithEmail(string loginType, string username, string password)
        {
            loginWithEmailByUnity(loginType, username, password);
        }
        //public static string getLanguage()
        //{
        //	return getLanguageByUnity();
        //}

        //[DllImport("__Internal")]
        //private static extern string getLanguageByUnity();

        // public static void showNaverCafe(int menuIndex, bool landscape)
        // {
        //     showNaverCafeByUnity(menuIndex, landscape);
        // }
        // [DllImport("__Internal")]
        // private static extern void showNaverCafeByUnity(int menuIndex, bool landscape);

        public static void logout()
        {
            logoutByUnity();
        }

        public static void createLinking(NCommon.LinkingType linkingType)
        {
            createLinkingByUnity(linkingType.ToString());
        }

        public static void deleteLinking(NCommon.LinkingType linkingType)
        {
            deleteLinkingByUnity(linkingType.ToString());
        }

        public static void addChannel(NCommon.ChannelType channelType)
        {
            addChannelByUnity(channelType.ToString());
        }

        public static void enableGameCenter(bool enable)
        {
            enableGameCenterByUnity(enable);
        }

        public static string getPurchaseItems()
        {
            return getPurchaseItemByUnity();
        }

        public static void getPurchaseDetailListAsync()
        {
            getPurchaseDetailListAsyncByUnity();
        }

        // TODO: push
        public static void setPush(bool pushEnable)
        {
            setPushByUnity(pushEnable);
        }

        public static void setPushNight(bool pushEnable)
        {
            setPushNightByUnity(pushEnable);
        }

        public static void setPushAd(bool pushEnable)
        {
            setPushAdByUnity(pushEnable);
        }

        public static void setPushState(bool pushEnable, bool nightPushEnable, bool adPushEnable)
        {
            setPushStateByUnity(pushEnable, nightPushEnable, adPushEnable);
        }

        public static string getPushStatus()
        {
            return getPushStatusByUnity();
        }

        public static void showNoticeWebView()
        {
            showNoticeWebViewByUnity();
        }

        public static void showCSWebView()
        {
            showCSWebViewByUnity();
        }

        public static void showWebView(string url)
        {
            showWebViewByUnity(url);
        }

        public static void showWebView(string url, bool hasBackButton)
        {
            showWebViewWithFlagByUnity(url, hasBackButton);
        }

        public static void showAppStatusPopup(string status)
        {
            showAppStatusPopupByUnity(status);
        }

        public static void showAgreeDialog(string info)
        {
            showAgreeDialogByUnity(info);
        }

        public static void showVoidViewDebug()
        {
            showVoidViewDebugByUnity();
        }

        public static void setVoidOption(string info)
        {
            setVoidOptionByUnity(info);
        }

        public static void setVoidDialogHelpURL(string url)
        {
            setVoidDialogHelpURLByUnity(url);
        }

        public static void showTerms()
        {
            showTermsByUnity();
        }

        public static void showPrivacy()
        {
            showPrivacyByUnity();
        }

        public static void sendLog(string type, string errCode, string errMessage)
        {
            sendLogByUnity(type, errCode, errMessage);
        }

        public static void setLoggerUserid(string userid)
        {
            setLoggerUseridByUnity(userid);
        }

        public static void showEvent(string type)
        {
            showEventByUnity(type);
        }

        public static void showNotice()
        {
            showNoticeByUnity();
        }

        public static void showNotice(bool showTodayButton)
        {
            showNoticeWithFlagByUnity(showTodayButton);
        }

        public static void showFaq()
        {
            showFaqByUnity();
        }

        public static bool characterInfo(string info)
        {
            return characterInfoByUnity(info);
        }

        public static string getPushToken()
        {
            return getPushTokenByUnity();
        }

        public static string getFCMToken()
        {
#if UNITY_IOS
            {
                return getFCMTokenByUnity();
            }
#else
            {
                Debug.Log("GamePot - UNITY EDITOR getFCMToken always returns emprt string");
                return "";
            }
#endif
        }

        public static void showRefund()
        {
            showRefundByUnity();
        }

        public static void sendPurchaseByThirdPartySDK(string productId, string transactionId, string currency, double price, string store, string paymentId, string uniqueId)
        {
            sendPurchaseByThirdPartySDKByUnity(productId, transactionId, currency, price, store, paymentId, uniqueId);
        }

        public static void loginByThirdPartySDK(string userId)
        {
            loginByThirdPartySDKByUnity(userId);
        }

        public static string getCountry()
        {
            return getCountryByUnity();
        }

        public static string getRemoteIP()
        {
            return getRemoteIPByUnity();
        }

        public static string getGDPRCheckedList()
        {
            return getGDPRCheckedListByUnity();
        }

        public static void safetyToast(string message)
        {
            safetyToastByUnity(message);
        }

        public static void requestTrackingAuthorization()
        {
            requestTrackingAuthorizationByUnity();
        }

        public static void checkAppStatus()
        {
            checkAppStatusByUnity();
        }

        public static void setAutoAgree(bool autoAgree)
        {
            setAutoAgreeByUnity(autoAgree);
        }

        public static void setAutoAgreeBuilder(string info)
        {
            setAutoAgreeBuilderByUnity(info);
        }

        public static void sendAgreeEmail(string email)
        {
            sendAgreeEmailByUnity(email);
        }

        public static void checkAgreeEmail(string email, string key)
        {
            checkAgreeEmailByUnity(email, key);
        }

        public static void setAgreeInfo(string info)
        {
            setAgreeInfoByUnity(info);
        }

        public static void setUserData(string userData)
        {
            setUserDataByUnity(userData);
        }

        public static string getUserData()
        {
            return getUserDataByUnity();
        }

        public static string getProjectId()
        {
            return getProjectIdByUnity();
        }

        public static void showCouponWebView()
        {
            showCouponWebViewByUnity();
        }       

    }
}
#endif
