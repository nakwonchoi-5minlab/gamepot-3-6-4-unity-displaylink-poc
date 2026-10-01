#import <Foundation/Foundation.h>

extern UIViewController *UnityGetGLViewController();
typedef void (*delegate_GamePot_handler)(const char *);

@interface GamePotManager : NSObject
{
#if __has_include(<GamePotAdAdjust/GamePotAdAdjust.h>)
    NSMutableArray *adjustBillingDic;
#endif
    delegate_GamePot_handler handler;
}
@property(assign) delegate_GamePot_handler handler;

+ (GamePotManager *)sharedManager;

- (void)changeProjectId:(NSString *)projectId sens_Push_Service_id:(NSString *)sensPushServiceId;
- (void)changeProjectId:(NSString *)projectId sens_Push_Service_id:(NSString *)sensPushServiceId region:(NSString *)region;
- (bool)getShowWithDrawPopup;
- (bool)setShowWithDrawPopup:(bool)_enable;
- (void)pluginVersion:(NSString *)version;
//////////////////////
// Common API
/////////////////////
- (void)setLanguage:(int)gameLanguage;
//////////////////////
// Channel API
//////////////////////
- (NSString *)getLastLoginType;
- (NSString *)getLinkedList;
- (NSString *)getPurchaseItems;
- (void)getPurchaseDetailListAsync;
- (NSString *)getPushStatus;
- (NSString *)getConfig:(NSString *)key;
- (NSString *)getConfigs;
- (void)invokeGuestLogin;
- (void)invokeGoogleLogin;
- (void)invokeFBLogin;
- (void)invokeEmailLogin;
- (void)invokeGameCenterLogin;
- (void)createGoogleLinking;
- (void)createFBLinking;
- (void)createGameCenterLinking;
- (void)deleteMember;
//////////////////////
// Chat API
//////////////////////
- (void)joinChannel:(NSString *)prevChannel;
- (void)leaveChannel:(NSString *)prevChannel;
- (void)sendMessage:(NSString *)prevChannel sendmessage:(NSString *)msg;
- (void)coupon:(NSString *)couponNumber userData:(NSString *)userData;
// - (void) naverCafeStartImageWrite:(NSString*) imageUri;
// - (void) tracking:(NSString*) adActions info:(NSString*)info;
// - (void) naverCafeInit;
// - (void) naverCafeStartHome;
// - (void) setSandbox:(BOOL) enable;
- (void)logout;
// - (void) naverCafeInitGlobal;
- (void)login:(NSString *)loginType;
- (void)showLoginWithUI:(NSString *)info;

// - (void) addAd:(NSString*) adType;
- (void)enableGameCenter:(BOOL)enable;
- (int)sendLocalPush:(NSString *)strTitle setMessage:(NSString *)strMessage setDate:(NSString *)strDate;
- (void)cancelLocalPush:(int)pushId;
- (void)purchase:(NSString *)productId uniqueId:(NSString *)uniqueId serverId:(NSString *)serverId playerId:(NSString *)playerId etc:(NSString *)etc;
// - (void) showNaverCafe:(int)menuId setLandScape:(BOOL)landscape;
- (BOOL)isLinked:(NSString *)linkType;
- (void)isLinkSample;
- (void)createLinking:(NSString *)linkType;
- (void)deleteLinking:(NSString *)linkType;
- (void)addChannel:(NSString *)channelType;
// - (void) setAdjustData:(NSString*)adjustBillingData;
- (void)setPush:(BOOL)pushEnable;
- (void)setPushNight:(BOOL)pushEnable;

- (void)setPushAd:(BOOL)pushEnable;
- (void)setPushStatus:(BOOL)pushEnable setNight:(BOOL)nightPushEnable setAd:(BOOL)adPushEnable;
- (void)showNoticeWebView;
- (void)showCSWebView;
- (void)showWebView:(NSString *)url;
- (void)showWebView:(NSString *)url setBackButton:(BOOL)hasBackButton;

- (void)showAppStatus:(NSString *)status;
- (void)showAgreeDialog:(NSString *)_option;
- (void)showVoidViewDebug;
- (void)setVoidOption:(NSString *)info;
- (void)setVoidDialogHelpURL:(NSString *)url;

- (void)showTerms;
- (void)showPrivacy;
- (void)sendLog:(NSString *)type errCode:(NSString *)errCode errMessage:(NSString *)errMessage;
- (void)showEvent:(NSString *)type;
- (void)showNotice;
- (void)showNotice:(BOOL)showTodayButton;
- (void)showFaq;
- (BOOL)characterInfo:(NSString *)info;
- (void)setLoggerUserid:(NSString *)userid;
- (NSString *)getPushToken;
- (NSString *)getFCMToken;
- (void)showRefund;
- (NSString *)getCountry;
- (NSString *)getRemoteIP;
- (void)setLogin:(NSString *)username
        password: (NSString *)password;

- (void)sendPurchaseByThirdPartySDK:(NSString *)_productId
                      transactionId:(NSString *)_transactionId
                           currency:(NSString *)_currency
                              price:(NSDecimalNumber *)_price
                          paymentId:(NSString *)_paymentId
                           uniqueId:(NSString *)_uniqueId
                            storeId:(NSString *)_storeId;

- (void)loginByThirdPartySDK:(NSString *)_userId;

- (NSString *)getGDPRCheckedList;
- (void)safetyToast:(NSString *)_message;
- (void)requestTrackingAuthorization;
- (void)checkAppStatus;
- (void)setAutoAgree:(BOOL)autoAgree;
- (void)setAutoAgreeBuilder:(NSString *)_info;

- (void)sendAgreeEmail:(NSString *)_email;
- (void)checkAgreeEmail:(NSString *)_email
             confirmKey:(NSString *)_key;

- (void)setAgreeInfo:(NSString *)_info;

- (NSString *)getUserData;
- (void)setUserData:(NSString *)_userData;
- (NSString *)getProjectId;
- (void)showCouponWebView;

@end
