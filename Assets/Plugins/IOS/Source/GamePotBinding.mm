#import "GamePotManager.h"

/*
 * iOS 플러그인
 */

// Converts C style string to NSString
#define GetStringParam( _x_ ) ( _x_ != NULL ) ? [NSString stringWithUTF8String:_x_] : [NSString stringWithUTF8String:""]

extern "C"
{
     char* MakeStringCopy (const char* string)
    {
        if (string == NULL)
            return NULL;

        char* res = (char*)malloc(strlen(string) + 1);
        strcpy(res, string);
        return res;
    }
    void changeProjectIdByUnity (const char* projectId, const char* _sens_Push_Service_id)
    {
        [[GamePotManager sharedManager] changeProjectId:GetStringParam(projectId) sens_Push_Service_id:GetStringParam(_sens_Push_Service_id) ];
    }
    
    void changeProjectIdByUnityWithRegion (const char* projectId, const char* _sens_Push_Service_id, const char* region)
    {
        [[GamePotManager sharedManager] changeProjectId:GetStringParam(projectId) sens_Push_Service_id:GetStringParam(_sens_Push_Service_id) region:GetStringParam(region) ];
    }
    bool getShowWithDrawPopupByUnity ()
    {
        return [[GamePotManager sharedManager] getShowWithDrawPopup];
    }
    void setShowWithDrawPopupByUnity (bool enable)
    {
        [[GamePotManager sharedManager] setShowWithDrawPopup:enable];
    }
    void pluginVersionByUnity (const char* version)
    {
        [[GamePotManager sharedManager] pluginVersion:GetStringParam(version)];
    }
    //////////////////////
	// Common API
	//////////////////////

	char* getConfigByUnity (const char* key)
    {
        NSString *result = [[GamePotManager sharedManager] getConfig:GetStringParam(key)];

        if (result == nil) {
            result = @"";
        }

        return MakeStringCopy([result UTF8String]);
    }

    char* getConfigsByUnity ()
    {
        NSString *result = [[GamePotManager sharedManager] getConfigs];

        if (result == nil) {
            result = @"";
        }

        return MakeStringCopy([result UTF8String]);
    }

	void setLanguageByUnity (int gameLanguage)
    {
        [[GamePotManager sharedManager] setLanguage:gameLanguage];
    }

    //////////////////////
    // Channel API
    //////////////////////

    char * getLastLoginTypeByUnity ()
    {
         NSString *result = [[GamePotManager sharedManager] getLastLoginType];
        if (result == nil) {
            result = @"";
        }
        return MakeStringCopy([result UTF8String]);
    }
    char * getLinkedListByUnity()
    {
        NSString *result = [[GamePotManager sharedManager] getLinkedList];
        if (result == nil) {
            result = @"";
        }
        return MakeStringCopy([result UTF8String]);
    }

    char * getPurchaseItemByUnity()
    {
        NSString *result = [[GamePotManager sharedManager] getPurchaseItems];
        if (result == nil) {
            result = @"";
        }
        return MakeStringCopy([result UTF8String]);
    }

    void getPurchaseDetailListAsyncByUnity()
    {
        [[GamePotManager sharedManager] getPurchaseDetailListAsync];
    }

	void deleteMemberByUnity ()
    {
        [[GamePotManager sharedManager] deleteMember];
    }

    //////////////////////
    // Chat API
    //////////////////////

	void joinChannelByUnity (const char* prevChannel)
    {
        [[GamePotManager sharedManager] joinChannel:GetStringParam(prevChannel)];
    }

	void leaveChannelByUnity (const char* prevChannel)
    {
        [[GamePotManager sharedManager] leaveChannel:GetStringParam(prevChannel)];
    }

	void sendMessageByUnity (const char* prevChannel, const char* message)
    {
        [[GamePotManager sharedManager] sendMessage:GetStringParam(prevChannel) sendmessage:GetStringParam(message)];
    }

    void couponByUnity(const char* couponNumber, const char* userData)
    {
        [[GamePotManager sharedManager] coupon:GetStringParam(couponNumber) userData:GetStringParam(userData)];
    }

    // void trackingByUnity(const char* adActions, const char* info)
    // {
    //     [[GamePotManager sharedManager] tracking:GetStringParam(adActions) info:GetStringParam(info)];
    // }


    void logoutByUnity()
    {
        [[GamePotManager sharedManager] logout];
    }

    // void naverCafeInitGlobalByUnity()
    // {
    //     // 함수 없음
    // }


    void loginByUnity(const char* loginType)
    {
        [[GamePotManager sharedManager]login:GetStringParam(loginType)];
    }

    void loginWithEmailByUnity(const char* loginType, const char* username, const char* password)
    {
        [[GamePotManager sharedManager] setLogin:GetStringParam(username) password: GetStringParam(password)];
        [[GamePotManager sharedManager] login:GetStringParam(loginType)];
    }
    // void showLoginWithUIByUnity(const char* loginTypes[], int length)
    // {
    //     NSMutableArray* types = [[NSMutableArray alloc] init];
    //     for(int i=0; i < length; i++)
    //     {
    //         [types addObject:GetStringParam(loginTypes[i])];
    //     }
    //     [[GamePotManager sharedManager]showLoginWithUI:types];
    // }

    void showLoginWithUIByUnity(const char* info)
    {
        [[GamePotManager sharedManager] showLoginWithUI:GetStringParam(info)];
    }

    int sendLocalPushByUnity(const char* strDate, const char* strTitle, const char* strMessage)
    {
       return [[GamePotManager sharedManager] sendLocalPush:GetStringParam(strTitle) setMessage:GetStringParam(strMessage) setDate:GetStringParam(strDate)];
    }

    void cancelLocalPushByUnity(int pushId)
    {
        [[GamePotManager sharedManager] cancelLocalPush:pushId];
    }

    // void showNaverCafeByUnity(int menuIndex, BOOL landscape)
    // {
    //     [[GamePotManager sharedManager] showNaverCafe: menuIndex setLandScape: landscape];
    // }
    // void addAdByUnity(const char* adType)
    // {
    //     [[GamePotManager sharedManager] addAd:GetStringParam(adType)];
    // }

    void enableGameCenterByUnity(bool enable)
    {
        [[GamePotManager sharedManager] enableGameCenter:enable];
    }


    void purchaseByUnity(const char* productId, const char* uniqueId, const char* serverId, const char* playerId, const char* etc)
    {
        [[GamePotManager sharedManager] purchase:GetStringParam(productId) uniqueId:GetStringParam(uniqueId) serverId:GetStringParam(serverId) playerId:GetStringParam(playerId) etc:GetStringParam(etc)];
    }

    bool isLinkedByUnity(const char* linkType)
    {
        return [[GamePotManager sharedManager] isLinked:GetStringParam(linkType)];
    }

    void createLinkingByUnity(const char* linkType)
    {
        [[GamePotManager sharedManager] createLinking:GetStringParam(linkType)];
    }

    void deleteLinkingByUnity(const char* linkType)
    {
        [[GamePotManager sharedManager] deleteLinking:GetStringParam(linkType)];
    }

    void addChannelByUnity(const char* channelType)
    {
        [[GamePotManager sharedManager]addChannel:GetStringParam(channelType)];
    }

    // void setSandboxByUnity(BOOL enable)
    // {
    //     [[GamePotManager sharedManager] setSandbox:enable];
    // }
    // void setAdjustDataByUnity(const char* adjustBillingData)
    // {
    //     [[GamePotManager sharedManager] setAdjustData:GetStringParam(adjustBillingData)];
    // }

    void setPushByUnity(BOOL pushEnable)
    {
        [[GamePotManager sharedManager] setPush:pushEnable];
    }

    void setPushNightByUnity(BOOL pushEnable)
    {
        [[GamePotManager sharedManager] setPushNight:pushEnable];
    }

    void setPushAdByUnity(bool pushEnable)
    {
        [[GamePotManager sharedManager] setPushAd:pushEnable];
    }

    void setPushStateByUnity(bool pushEnable, bool nightPushEnable, bool adPushEnable)
    {
        [[GamePotManager sharedManager] setPushStatus:pushEnable setNight:nightPushEnable setAd:adPushEnable];
    }

    void showNoticeWebViewByUnity()
    {
        [[GamePotManager sharedManager] showNoticeWebView];
    }

    void showWebViewByUnity(const char* url)
    {
        [[GamePotManager sharedManager] showWebView:GetStringParam(url)];
    }

    void showWebViewWithFlagByUnity(const char* url, bool hasBackButton)
    {
        [[GamePotManager sharedManager] showWebView:GetStringParam(url) setBackButton:hasBackButton];
    }

    void showCSWebViewByUnity()
    {
        [[GamePotManager sharedManager] showCSWebView];
    }

    char* getPushStatusByUnity()
    {
         NSString *result = [[GamePotManager sharedManager] getPushStatus];
        if (result == nil) {
            result = @"";
        }
        return MakeStringCopy([result UTF8String]);
    }

    void showAppStatusPopupByUnity(const char* status)
    {
        [[GamePotManager sharedManager] showAppStatus:GetStringParam(status)];
    }

    void showAgreeDialogByUnity(const char* info)
    {
        [[GamePotManager sharedManager] showAgreeDialog:GetStringParam(info)];
    }

    void showVoidViewDebugByUnity()
    {
        [[GamePotManager sharedManager] showVoidViewDebug];
    }

    void setVoidOptionByUnity(const char* info)
    {
        [[GamePotManager sharedManager] setVoidOption:GetStringParam(info)];
    }

    void setVoidDialogHelpURLByUnity(const char* url)
    {
        [[GamePotManager sharedManager] setVoidDialogHelpURL:GetStringParam(url)];
    }

    void showTermsByUnity()
    {
        [[GamePotManager sharedManager] showTerms];
    }
    void showPrivacyByUnity()
    {
        [[GamePotManager sharedManager] showPrivacy];
    }

    void sendLogByUnity(const char* type, const char* errCode, const char* errMessage)
    {
        [[GamePotManager sharedManager] sendLog:GetStringParam(type) errCode:GetStringParam(errCode) errMessage:GetStringParam(errMessage)];
    }

    void setLoggerUseridByUnity(const char* userid)
    {
        [[GamePotManager sharedManager] setLoggerUserid:GetStringParam(userid)];
    }

    void showEventByUnity(const char* type)
    {
        [[GamePotManager sharedManager] showEvent:GetStringParam(type)];
    }

    void showNoticeByUnity()
    {
        [[GamePotManager sharedManager] showNotice];
    }

    void showNoticeWithFlagByUnity(BOOL showTodayButton)
    {
        [[GamePotManager sharedManager] showNotice:showTodayButton];
    }

    void showFaqByUnity()
    {
        [[GamePotManager sharedManager] showFaq];
    }

    bool characterInfoByUnity(const char* info)
    {
        return [[GamePotManager sharedManager] characterInfo:GetStringParam(info)];
    }

    char * getPushTokenByUnity()
    {
        NSString *result = [[GamePotManager sharedManager] getPushToken];
        if (result == nil) {
            result = @"";
        }
        return MakeStringCopy([result UTF8String]);
    }

    char * getFCMTokenByUnity()
    {
        NSString *result = [[GamePotManager sharedManager] getFCMToken];
        if (result == nil) {
            result = @"";
        }
        return MakeStringCopy([result UTF8String]);
    }

    void showRefundByUnity()
    {
        [[GamePotManager sharedManager] showRefund];
    }

    void sendPurchaseByThirdPartySDKByUnity(const char* productId, const char* transactionId, const char* currency, double price, const char* store, const char* paymentId, const char* uniqueId)
    {
        NSDecimalNumber* decimalPrice = [NSDecimalNumber decimalNumberWithDecimal:[[NSNumber numberWithDouble:price] decimalValue]];
        [[GamePotManager sharedManager] sendPurchaseByThirdPartySDK:GetStringParam(productId) transactionId:GetStringParam(transactionId) currency:GetStringParam(currency) price: decimalPrice paymentId:GetStringParam(paymentId) uniqueId:GetStringParam(uniqueId) storeId:GetStringParam(store)];
    }

    void loginByThirdPartySDKByUnity(const char* userId)
    {
        [[GamePotManager sharedManager] loginByThirdPartySDK:GetStringParam(userId)];
    }

    char* getCountryByUnity()
    {
        NSString* result = [[GamePotManager sharedManager] getCountry];
        if(result == nil){
            result = @"";
        }
        return MakeStringCopy([result UTF8String]);
    }

    char* getRemoteIPByUnity()
    {
        NSString* result = [[GamePotManager sharedManager] getRemoteIP];
        if(result == nil){
            result = @"";
        }
        return MakeStringCopy([result UTF8String]);
    }

    char* getGDPRCheckedListByUnity()
    {
        NSString* result = [[GamePotManager sharedManager] getGDPRCheckedList];

        if(result == nil){
            result = @"";
        }
        return MakeStringCopy([result UTF8String]);
    }

    void safetyToastByUnity(const char* message)
    {
        [[GamePotManager sharedManager] safetyToast:GetStringParam(message)];
    }

    void requestTrackingAuthorizationByUnity()
    {
        [[GamePotManager sharedManager] requestTrackingAuthorization];
    }

    void checkAppStatusByUnity()
    {
        [[GamePotManager sharedManager] checkAppStatus];
    }

    void setAutoAgreeByUnity(BOOL autoAgree)
    {
        [[GamePotManager sharedManager] setAutoAgree:autoAgree];
    }

    void setAutoAgreeBuilderByUnity(const char* info)
    {
        [[GamePotManager sharedManager] setAutoAgreeBuilder:GetStringParam(info)];
    }

    void sendAgreeEmailByUnity(const char* email)
    {
        [[GamePotManager sharedManager] sendAgreeEmail:GetStringParam(email)];
    }

    void checkAgreeEmailByUnity(const char* email, const char* key)
    {
        [[GamePotManager sharedManager] checkAgreeEmail:GetStringParam(email) confirmKey:GetStringParam(key)];
    }

    void setAgreeInfoByUnity(const char* info)
    {
        [[GamePotManager sharedManager] setAgreeInfo:GetStringParam(info)];
    }

    void setUserDataByUnity(const char* userData)
    {
        [[GamePotManager sharedManager] setUserData:GetStringParam(userData)];
    }

    char* getUserDataByUnity()
    {
         NSString* result = [[GamePotManager sharedManager] getUserData];

        if(result == nil){
            result = @"";
        }
        return MakeStringCopy([result UTF8String]);
    }

    char * getProjectIdByUnity()
    {
        NSString *result = [[GamePotManager sharedManager] getProjectId];
        if (result == nil) {
            result = @"";
        }
        return MakeStringCopy([result UTF8String]);
    }

	void showCouponWebViewByUnity()
	{
		[[GamePotManager sharedManager] showCouponWebView];
	}
}
