#import "GamePotManager.h"
#import <GamePot/GamePot.h>
#import <GamePotChannel/GamePotChannel.h>
#import <StoreKit/StoreKit.h>
// #if __has_include(<GamePotAd/GamePotAd.h>)
// #import <GamePotAd/GamePotAd.h>
// #endif
// #if __has_include(<GamePotAdFacebook/GamePotAdFacebook.h>)
// #import <GamePotAdFacebook/GamePotAdFacebook.h>
// #endif
// #if __has_include(<GamePotAdAdjust/GamePotAdAdjust.h>)
// #import <GamePotAdAdjust/GamePotAdAdjust.h>
// #endif
#if __has_include(<Gamepot/GamePotSendLog.h>)
#import <Gamepot/GamePotSendLog.h>
#import <Gamepot/GamePotSendLogCharacter.h>
#endif

#if __has_include(<GamePotFacebook/GamePotFacebook.h>)
#import <GamePotFacebook/GamePotFacebook.h>
#endif
#if __has_include(<GamePotGameCenter/GamePotGameCenter.h>)
#import <GamePotGameCenter/GamePotGameCenter.h>
#endif
#if __has_include(<GamePotGoogleSignIn/GamePotGoogleSignIn.h>)
#import <GamePotGoogleSignIn/GamePotGoogleSignIn.h>
#endif
#if __has_include(<GamePotNaver/GamePotNaver.h>)
#import <GamePotNaver/GamePotNaver.h>
#endif
#if __has_include(<GamePotLogger/GamePotLogger.h>)
#import <GamePotLogger/GamePotLogger.h>
#endif
#if __has_include(<GamePotTwitter/GamePotTwitter.h>)
#import <GamePotTwitter/GamePotTwitter.h>
#endif
#if __has_include(<GamePotLine/GamePotLine.h>)
#import <GamePotLine/GamePotLine.h>
#endif
#if __has_include(<GamePotApple/GamePotApple.h>)
#import <GamePotApple/GamePotApple.h>
#endif
#if __has_include(<GamePotEmail/GamePotEmail.h>)
#import <GamePotEmail/GamePotEmail.h>
#endif
#if __has_include(<GamePotOAuth/GamePotOAuth.h>)
#import <GamePotOAuth/GamePotOAuth.h>
#endif
// #if __has_include(<GamePotNaverCafe/GamePotNaverCafe.h>)
// #import <GamePotNaverCafe/GamePotNaverCafe.h>
// #endif

#if __has_include(<AppTrackingTransparency/AppTrackingTransparency.h>)
#import <AppTrackingTransparency/AppTrackingTransparency.h>
#endif

UIViewController *UnityGetGLViewController();
const char* ListenerForUnity = "GamePotiOSManager";
@interface GamePotManager()  <GamePotPurchaseDelegate> {

}
@end

@implementation GamePotManager


@synthesize handler;

- (void)GamePotPurchaseSuccess:(GamePotPurchaseInfo *)_info
{
    // _info.Prdouctid와 adjustKey 비교 하여 adjustValue를 setAdjustKey에 추가
// #if __has_include(<GamePotAdAdjust/GamePotAdAdjust.h>)
    //    if(adjustBillingDic != nil && adjustBillingDic.count > 0)
    //    {
    //        for (NSString *key in adjustBillingDic) {
    //            if([[_info productId] isEqualToString:key])
    //                [_info setAdjustKey:[adjustBillingDic valueForKey:key]];
    //        }
    //    }
// #endif

// #if __has_include(<GamePotAd/GamePotAd.h>)
//        [[GamePotAd getInstance] tracking:BILLING obj:_info];
// #endif
    //    UnitySendMessage(ListenerForUnity, "onCharged", sendStr);

    UnitySendMessage(ListenerForUnity, "onPurchaseSuccess", [[_info toJsonString] UTF8String]);
}

- (void)GamePotPurchaseFail:(NSError *)_error
{
    // 결제 실패
    if(_error)
    {
        UnitySendMessage(ListenerForUnity, "onPurchaseFailure", [self getError:_error]);
    }
    else
    {
        UnitySendMessage(ListenerForUnity, "onPurchaseFailure", "");
    }
}

- (void)GamePotPurchaseCancel
{
    // 결제 중 유저에 의한 취소
    UnitySendMessage(ListenerForUnity, "onPurchaseCancel", "");
}

+ (GamePotManager *)sharedManager {

    static GamePotManager *sharedMyManager = nil;

    static dispatch_once_t onceToken;

    dispatch_once(&onceToken, ^{
        sharedMyManager = [[self alloc] init];
        [[GamePot getInstance] setPurchaseDelegate:sharedMyManager];
    });

    return sharedMyManager;
}

#pragma mark UnityBinding

- (void) changeProjectId:(NSString*)projectId sens_Push_Service_id:(NSString*)sensPushServiceId
{
     [[GamePot getInstance] changeProjectId:projectId sens_Push_Service_id:sensPushServiceId];
}

- (void) changeProjectId:(NSString*)projectId sens_Push_Service_id:(NSString*)sensPushServiceId region:(NSString*)region
{
     [[GamePot getInstance] changeProjectId:projectId sens_Push_Service_id:sensPushServiceId region:region];
}

- (bool) getShowWithDrawPopup
{
    return [[GamePot getInstance] getShowWithDrawPopup];
}
- (void) setShowWithDrawPopup:(BOOL)_enable
{
    [[GamePot getInstance] setShowWithDrawPopup:_enable];
}
- (void) pluginVersion:(NSString*)version
{
    NSLog(@"GamePot Plugin Version : %@", version);
}

//////////////////////
// Common API
/////////////////////

- (NSString*) getConfig:(NSString*) key
{
    NSLog(@"getConfig key = %@", key);
    @try
    {
         return [[GamePot getInstance] getConfig:key];
    }
    @catch(NSException *exception)
    {
        NSLog(@"GetConfig exception: %@", exception);
        return @"";
    }
}

- (NSString*) getConfigs
{
    @try
    {
        NSArray* array = [[GamePot getInstance] getConfigs];

        NSData *data = [NSJSONSerialization dataWithJSONObject:array
                                                       options:NSJSONWritingPrettyPrinted
                                                         error:nil];

        NSString *jsonStr = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
        return jsonStr;
    }
    @catch(NSException *exception)
    {
        NSLog(@"GetConfig exception: %@", exception);
        return @"";
    }

}

- (void) setLanguage:(int)gameLanguage
{
    NSLog(@"setLanguage gameLanguage = %@", gameLanguage);
}

//////////////////////
// Channel API
//////////////////////
- (int) sendLocalPush:(NSString*)strTitle setMessage:(NSString*)strMessage setDate:(NSString*)strDate
{
     return [[GamePot getInstance] sendLocalPush:strTitle setMessage:strMessage setDateString:strDate];
}
- (void) cancelLocalPush: (int) pushId
{
     [[GamePot getInstance] cancelLocalPush:pushId];
}
- (NSString *) getLinkedList
{
    return [[GamePotChannel getInstance] getLinkedListJsonString];
}

- (BOOL) isLinked:(NSString*)linkType
{
    GamePotChannelType type = NONE;

    if([linkType isEqualToString:@"GUEST"])
    {
        type = GUEST;
    }
    else if([linkType isEqualToString:@"GOOGLE"])
    {
        type = GOOGLE;
    }
    else if([linkType isEqualToString:@"FACEBOOK"])
    {
        type = FACEBOOK;
    }
    else if([linkType isEqualToString:@"GAMECENTER"])
    {
        type = GAMECENTER;
    }
    else if([linkType isEqualToString:@"NAVER"])
    {
        type = NAVER;
    }
    else if([linkType isEqualToString:@"TWITTER"])
    {
        type = TWITTER;
    }
    else if([linkType isEqualToString:@"LINE"])
    {
        type = LINE;
    }
    else if([linkType isEqualToString:@"APPLE"])
    {
        type = APPLE;
    }
    else if([linkType isEqualToString:@"EMAIL"])
    {
        type = EMAIL;
    }
    else if([linkType isEqualToString:@"OAUTH"])
    {
        type = OAUTH;
    }

    return [[GamePotChannel getInstance] isLinked:type];
}

- (NSString *) getPushStatus
{
    return [[GamePot getInstance] getPushStatusJsonString];
}

- (NSString *) getLastLoginType
{
    GamePotChannelType channelType = [[GamePotChannel getInstance] lastLoginType];
    NSString *strChannelType = @"NONE";
    switch (channelType) {
        case GOOGLE:
            strChannelType = @"GOOGLE";
            break;
        case FACEBOOK:
            strChannelType = @"FACEBOOK";
            break;
        case NAVER:
            strChannelType = @"NAVER";
            break;
        case GAMECENTER:
            strChannelType = @"GAMECENTER";
            break;
        case TWITTER:
            strChannelType = @"TWITTER";
            break;
        case LINE:
            strChannelType = @"LINE";
            break;
        case APPLE:
            strChannelType = @"APPLE";
            break;
        case GUEST:
            strChannelType = @"GUEST";
            break;
        case EMAIL:
            strChannelType = @"EMAIL";
            break;
        case OAUTH:
            strChannelType = @"OAUTH";
            break;
        default:
            break;
    }
    return strChannelType;
}
/**
@property(nonatomic) NSString* memberid;
@property(nonatomic) NSString* userid;
@property(nonatomic) NSString* name;
@property(nonatomic) NSString* profileUrl;
@property(nonatomic) NSString* token;
@property(nonatomic) NSString* email;
**/
- (const char*) getUser:(GamePotUserInfo*) userInfo
{
    NSString* jsonString = @"{}";

    if(userInfo != nil)
    {
        jsonString = [userInfo toJsonString];
    }

    return [jsonString cStringUsingEncoding:NSUTF8StringEncoding];
}
- (const char*) getError:(NSError*) error
{
     NSMutableDictionary* errorData = [[NSMutableDictionary alloc] init];
    [errorData setObject:[NSNumber numberWithInteger:[error code]] forKey:@"status"];
    [errorData setObject:[NSNumber numberWithInteger:[error code]] forKey:@"code"];
    [errorData setObject:[error localizedDescription] forKey:@"message"];

    NSError* jsonParsingError = nil;
    NSData* jsonData = [NSJSONSerialization dataWithJSONObject:errorData options:NSJSONWritingPrettyPrinted error:&jsonParsingError];
    if(jsonParsingError != nil)
    {
        NSLog(@"Login Error Result json parsing Error");
        return nil;
    }
    NSString* jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    return [jsonString cStringUsingEncoding:NSUTF8StringEncoding];
}
- (void) invokeGuestLogin
{
    dispatch_async(dispatch_get_main_queue(), ^{
        NSLog(@"invokeGuestLogin");
        @try
        {
            [[GamePotChannel getInstance] Login:GUEST viewController:UnityGetGLViewController() success:^(GamePotUserInfo* userInfo) {
                UnitySendMessage(ListenerForUnity, "onLoginSuccess", [self getUser:userInfo]);
            } cancel:^{
                UnitySendMessage(ListenerForUnity, "onLoginCancel",  "");
            } fail:^(NSError *error) {
                if(error)
                {
                    UnitySendMessage(ListenerForUnity, "onLoginFailure", [self getError:error]);
                    return;
                }
                UnitySendMessage(ListenerForUnity, "onLoginFailure", "");
            } update:^(GamePotAppStatus *appStatus) {

                if([appStatus resultPayload] != nil)
                {
                    [appStatus setResultPayload:[(GamePotUserInfo*)[appStatus resultPayload] toJsonString]];
                }

                UnitySendMessage(ListenerForUnity, "onNeedUpdate", [[appStatus toJsonString] UTF8String]);
            } maintenance:^(GamePotAppStatus *appStatus) {
                UnitySendMessage(ListenerForUnity, "onMainternance", [[appStatus toJsonString] UTF8String]);
            }];
        }
        @catch(NSException *exception)
        {
            NSLog(@"createGuestLogin exception: %@", exception);
            UnitySendMessage(ListenerForUnity, "onLoginFailure", "");
        }
    });
}

- (void) invokeGoogleLogin
{
    dispatch_async(dispatch_get_main_queue(), ^{
        NSLog(@"invokeGoogleLogin");
        @try
        {
            [[GamePotChannel getInstance] Login:GOOGLE viewController:UnityGetGLViewController() success:^(GamePotUserInfo* userInfo) {
                UnitySendMessage(ListenerForUnity, "onLoginSuccess", [self getUser:userInfo]);
            } cancel:^{
                UnitySendMessage(ListenerForUnity, "onLoginCancel",  "");
            } fail:^(NSError *error) {
                if(error)
                {
                    UnitySendMessage(ListenerForUnity, "onLoginFailure", [self getError:error]);
                    return;
                }
                UnitySendMessage(ListenerForUnity, "onLoginFailure", "");
            } update:^(GamePotAppStatus *appStatus) {
                if([appStatus resultPayload] != nil)
                {
                    [appStatus setResultPayload:[(GamePotUserInfo*)[appStatus resultPayload] toJsonString]];
                }

                UnitySendMessage(ListenerForUnity, "onNeedUpdate", [[appStatus toJsonString] UTF8String]);
            } maintenance:^(GamePotAppStatus *appStatus) {
                UnitySendMessage(ListenerForUnity, "onMainternance", [[appStatus toJsonString] UTF8String]);
            }];
        }
        @catch(NSException *exception)
        {
            NSLog(@"createGoogleLogin exception: %@", exception);
            UnitySendMessage(ListenerForUnity, "onLoginFailure", "");
        }
    });
}

- (void) invokeFBLogin
{
    dispatch_async(dispatch_get_main_queue(), ^{
        NSLog(@"invokeFBLogin");
        @try
        {
            [[GamePotChannel getInstance] Login:FACEBOOK viewController:UnityGetGLViewController() success:^(GamePotUserInfo* userInfo) {
                UnitySendMessage(ListenerForUnity, "onLoginSuccess", [self getUser:userInfo]);
            } cancel:^{
                UnitySendMessage(ListenerForUnity, "onLoginCancel",  "");
            } fail:^(NSError *error) {
                if(error)
                {
                    UnitySendMessage(ListenerForUnity, "onLoginFailure", [self getError:error]);
                    return;
                }
                UnitySendMessage(ListenerForUnity, "onLoginFailure", "");
            } update:^(GamePotAppStatus *appStatus) {
                if([appStatus resultPayload] != nil)
                {
                    [appStatus setResultPayload:[(GamePotUserInfo*)[appStatus resultPayload] toJsonString]];
                }
                UnitySendMessage(ListenerForUnity, "onNeedUpdate", [[appStatus toJsonString] UTF8String]);
            } maintenance:^(GamePotAppStatus *appStatus) {
                UnitySendMessage(ListenerForUnity, "onMainternance", [[appStatus toJsonString] UTF8String]);
            }];
        }
        @catch(NSException *exception)
        {
            NSLog(@"createFBLogin exception: %@", exception);
            UnitySendMessage(ListenerForUnity, "onLoginFailure", "");
        }
    });
}

- (void) invokeGameCenterLogin
{
    dispatch_async(dispatch_get_main_queue(), ^{
        NSLog(@"invokeGameCenterLogin");
        @try
        {
            [[GamePotChannel getInstance] Login:GAMECENTER viewController:UnityGetGLViewController() success:^(GamePotUserInfo* userInfo) {
                UnitySendMessage(ListenerForUnity, "onLoginSuccess", [self getUser:userInfo]);
            } cancel:^{
                UnitySendMessage(ListenerForUnity, "onLoginCancel",  "");
            } fail:^(NSError *error) {
                if(error)
                {
                    UnitySendMessage(ListenerForUnity, "onLoginFailure", [self getError:error]);
                    return;
                }
                UnitySendMessage(ListenerForUnity, "onLoginFailure", "");
            } update:^(GamePotAppStatus *appStatus) {
                if([appStatus resultPayload] != nil)
                {
                    [appStatus setResultPayload:[(GamePotUserInfo*)[appStatus resultPayload] toJsonString]];
                }
                UnitySendMessage(ListenerForUnity, "onNeedUpdate", [[appStatus toJsonString] UTF8String]);
            } maintenance:^(GamePotAppStatus *appStatus) {
                UnitySendMessage(ListenerForUnity, "onMainternance", [[appStatus toJsonString] UTF8String]);
            }];
        }
        @catch(NSException *exception)
        {
            NSLog(@"createGameCenterLogin exception: %@", exception);
            UnitySendMessage(ListenerForUnity, "onLoginFailure", "");
        }
    });
}

- (void) invokeNaverLogin
{
    dispatch_async(dispatch_get_main_queue(), ^{
        NSLog(@"invokeNaverLogin");
        @try
        {
            [[GamePotChannel getInstance] Login:NAVER viewController:UnityGetGLViewController() success:^(GamePotUserInfo* userInfo) {
                UnitySendMessage(ListenerForUnity, "onLoginSuccess", [self getUser:userInfo]);
            } cancel:^{
                UnitySendMessage(ListenerForUnity, "onLoginCancel",  "");
            } fail:^(NSError *error) {
                if(error)
                {
                    UnitySendMessage(ListenerForUnity, "onLoginFailure", [self getError:error]);
                    return;
                }
                UnitySendMessage(ListenerForUnity, "onLoginFailure", "");
            } update:^(GamePotAppStatus *appStatus) {
                if([appStatus resultPayload] != nil)
                {
                    [appStatus setResultPayload:[(GamePotUserInfo*)[appStatus resultPayload] toJsonString]];
                }
                UnitySendMessage(ListenerForUnity, "onNeedUpdate", [[appStatus toJsonString] UTF8String]);
            } maintenance:^(GamePotAppStatus *appStatus) {
                UnitySendMessage(ListenerForUnity, "onMainternance", [[appStatus toJsonString] UTF8String]);
            }];
        }
        @catch(NSException *exception)
        {
            NSLog(@"createNaverLogin exception: %@", exception);
            UnitySendMessage(ListenerForUnity, "onLoginFailure", "");
        }
    });
}

- (void) invokeTwitterLogin
{
    dispatch_async(dispatch_get_main_queue(), ^{
        NSLog(@"invokeTwitterLogin");
        @try
        {
            [[GamePotChannel getInstance] Login:TWITTER viewController:UnityGetGLViewController() success:^(GamePotUserInfo* userInfo) {
                UnitySendMessage(ListenerForUnity, "onLoginSuccess", [self getUser:userInfo]);
            } cancel:^{
                UnitySendMessage(ListenerForUnity, "onLoginCancel",  "");
            } fail:^(NSError *error) {
                if(error)
                {
                    UnitySendMessage(ListenerForUnity, "onLoginFailure", [self getError:error]);
                    return;
                }
                UnitySendMessage(ListenerForUnity, "onLoginFailure", "");
            } update:^(GamePotAppStatus *appStatus) {
                if([appStatus resultPayload] != nil)
                {
                    [appStatus setResultPayload:[(GamePotUserInfo*)[appStatus resultPayload] toJsonString]];
                }
                UnitySendMessage(ListenerForUnity, "onNeedUpdate", [[appStatus toJsonString] UTF8String]);
            } maintenance:^(GamePotAppStatus *appStatus) {
                UnitySendMessage(ListenerForUnity, "onMainternance", [[appStatus toJsonString] UTF8String]);
            }];
        }
        @catch(NSException *exception)
        {
            NSLog(@"createTwitterLogin exception: %@", exception);
            UnitySendMessage(ListenerForUnity, "onLoginFailure", "");
        }
    });
}
- (void ) invokeOAuthLogin
{
 dispatch_async(dispatch_get_main_queue(), ^{
        NSLog(@"invokeOAuthLogin");
        @try
        {
            [[GamePotChannel getInstance] Login:OAUTH viewController:UnityGetGLViewController() success:^(GamePotUserInfo* userInfo) {
                UnitySendMessage(ListenerForUnity, "onLoginSuccess", [self getUser:userInfo]);
            } cancel:^{
                UnitySendMessage(ListenerForUnity, "onLoginCancel",  "");
            } fail:^(NSError *error) {
                if(error)
                {
                    NSLog(@"invokeOAuthLogin-error: %@",error);
                    UnitySendMessage(ListenerForUnity, "onLoginFailure", [self getError:error]);
                    return;
                }
                UnitySendMessage(ListenerForUnity, "onLoginFailure", "");
            } update:^(GamePotAppStatus *appStatus) {
                if([appStatus resultPayload] != nil)
                {
                    [appStatus setResultPayload:[(GamePotUserInfo*)[appStatus resultPayload] toJsonString]];
                }
                UnitySendMessage(ListenerForUnity, "onNeedUpdate", [[appStatus toJsonString] UTF8String]);
            } maintenance:^(GamePotAppStatus *appStatus) {
                UnitySendMessage(ListenerForUnity, "onMainternance", [[appStatus toJsonString] UTF8String]);
            }];
        }
        @catch(NSException *exception)
        {
            NSLog(@"createOAuthLogin exception: %@", exception);
            UnitySendMessage(ListenerForUnity, "onLoginFailure", "");
        }
    });
}
- (void ) invokeEmailLogin
{
 dispatch_async(dispatch_get_main_queue(), ^{
        NSLog(@"invokeEmailLogin");
        @try
        {
            [[GamePotChannel getInstance] Login:EMAIL viewController:UnityGetGLViewController() success:^(GamePotUserInfo* userInfo) {
                UnitySendMessage(ListenerForUnity, "onLoginSuccess", [self getUser:userInfo]);
            } cancel:^{
                UnitySendMessage(ListenerForUnity, "onLoginCancel",  "");
            } fail:^(NSError *error) {
                if(error)
                {
                    NSLog(@"invokeEmailLogin-error: %@",error);
                    UnitySendMessage(ListenerForUnity, "onLoginFailure", [self getError:error]);
                    return;
                }
                UnitySendMessage(ListenerForUnity, "onLoginFailure", "");
            } update:^(GamePotAppStatus *appStatus) {
                if([appStatus resultPayload] != nil)
                {
                    [appStatus setResultPayload:[(GamePotUserInfo*)[appStatus resultPayload] toJsonString]];
                }
                UnitySendMessage(ListenerForUnity, "onNeedUpdate", [[appStatus toJsonString] UTF8String]);
            } maintenance:^(GamePotAppStatus *appStatus) {
                UnitySendMessage(ListenerForUnity, "onMainternance", [[appStatus toJsonString] UTF8String]);
            }];
        }
        @catch(NSException *exception)
        {
            NSLog(@"createEmailLogin exception: %@", exception);
            UnitySendMessage(ListenerForUnity, "onLoginFailure", "");
        }
    });
}
- (void) invokeLineLogin
{
    dispatch_async(dispatch_get_main_queue(), ^{
        NSLog(@"invokeLineLogin");
        @try
        {
            [[GamePotChannel getInstance] Login:LINE viewController:UnityGetGLViewController() success:^(GamePotUserInfo* userInfo) {
                UnitySendMessage(ListenerForUnity, "onLoginSuccess", [self getUser:userInfo]);
            } cancel:^{
                UnitySendMessage(ListenerForUnity, "onLoginCancel",  "");
            } fail:^(NSError *error) {
                if(error)
                {
                    UnitySendMessage(ListenerForUnity, "onLoginFailure", [self getError:error]);
                    return;
                }
                UnitySendMessage(ListenerForUnity, "onLoginFailure", "");
            } update:^(GamePotAppStatus *appStatus) {
                if([appStatus resultPayload] != nil)
                {
                    [appStatus setResultPayload:[(GamePotUserInfo*)[appStatus resultPayload] toJsonString]];
                }
                UnitySendMessage(ListenerForUnity, "onNeedUpdate", [[appStatus toJsonString] UTF8String]);
            } maintenance:^(GamePotAppStatus *appStatus) {
                UnitySendMessage(ListenerForUnity, "onMainternance", [[appStatus toJsonString] UTF8String]);
            }];
        }
        @catch(NSException *exception)
        {
            NSLog(@"createLineLogin exception: %@", exception);
            UnitySendMessage(ListenerForUnity, "onLoginFailure", "");
        }
    });
}

- (void) invokeAppleLogin
{
    dispatch_async(dispatch_get_main_queue(), ^{
        NSLog(@"invokeAppleLogin");
        @try
        {
            [[GamePotChannel getInstance] Login:APPLE viewController:UnityGetGLViewController() success:^(GamePotUserInfo* userInfo) {
                UnitySendMessage(ListenerForUnity, "onLoginSuccess", [self getUser:userInfo]);
            } cancel:^{
                UnitySendMessage(ListenerForUnity, "onLoginCancel",  "");
            } fail:^(NSError *error) {
                if(error)
                {
                    UnitySendMessage(ListenerForUnity, "onLoginFailure", [self getError:error]);
                    return;
                }
                UnitySendMessage(ListenerForUnity, "onLoginFailure", "");
            } update:^(GamePotAppStatus *appStatus) {
                if([appStatus resultPayload] != nil)
                {
                    [appStatus setResultPayload:[(GamePotUserInfo*)[appStatus resultPayload] toJsonString]];
                }
                UnitySendMessage(ListenerForUnity, "onNeedUpdate", [[appStatus toJsonString] UTF8String]);
            } maintenance:^(GamePotAppStatus *appStatus) {
                UnitySendMessage(ListenerForUnity, "onMainternance", [[appStatus toJsonString] UTF8String]);
            }];
        }
        @catch(NSException *exception)
        {
            NSLog(@"createAppleLogin exception: %@", exception);
            UnitySendMessage(ListenerForUnity, "onLoginFailure", "");
        }
    });
}

- (void) deleteMember
{
    NSLog(@"CommonDeleteMember");
    @try
    {
        [[GamePotChannel getInstance] DeleteMemberWithSuccess:^{
            //            const char *sendStr = [[NSString stringWithFormat:@"탈퇴를 성공했습니다."] UTF8String];
            UnitySendMessage(ListenerForUnity, "onDeleteMemberSuccess", "" );
        } fail:^(NSError *error) {
            NSDictionary *dic = [error userInfo];
            if(dic)
            {
                UnitySendMessage(ListenerForUnity, "onDeleteMemberFailure", [self getError:error]);
            }
            else
            {
                UnitySendMessage(ListenerForUnity, "onDeleteMemberFailure", "");
            }
        }];
    }
    @catch(NSException *exception)
    {
        NSLog(@"CommonDeleteMember exception: %@", exception);
        UnitySendMessage(ListenerForUnity, "onDeleteMemberFailure", "");
    }
}

//////////////////////
// Chat API
//////////////////////
- (void) coupon:(NSString*) couponNumber userData:(NSString*)userData {
    [[GamePot getInstance] coupon:couponNumber userData:userData handler:^(BOOL _success, NSError *_error) {
        if(_success)
        {
            UnitySendMessage(ListenerForUnity, "onCouponSuccess", "");
        }
        else
        {
            UnitySendMessage(ListenerForUnity, "onCouponFailure", [self getError:_error]);
        }
    }];
}

// - (void) createTutirialData:(NSString*) tutorialData
// {
// #if __has_include(<GamePotAd/GamePotAd.h>)
//     NSData* jsonData = [tutorialData dataUsingEncoding:NSUTF8StringEncoding];
//     NSMutableDictionary *dic = [NSJSONSerialization JSONObjectWithData:jsonData options:NSJSONWritingPrettyPrinted error:nil];
//     NSString * contentData = [dic valueForKey:@"contentData"];
//     NSString * contentId = [dic valueForKey:@"contentId"];
//     NSString * isSuccess = [dic valueForKey:@"isSuccess"];
//     NSString * adjustKey = [dic valueForKey:@"adjustKey"];

//     TrackerTutorial* tutorialEvent = [[TrackerTutorial alloc] init];
//     [tutorialEvent setContentData:contentData];
//     [tutorialEvent setContentId:contentId];
//     [tutorialEvent setSuccess:[isSuccess boolValue]];
//     [tutorialEvent setAdjustKey:adjustKey];

//     [[GamePotAd getInstance] tracking:TUTORIAL_COMPLETE obj:tutorialEvent];
// #endif
// }

// - (void) createLevelData:(NSString*) levelData
// {
// #if __has_include(<GamePotAd/GamePotAd.h>)
//     NSData* jsonData = [levelData dataUsingEncoding:NSUTF8StringEncoding];
//     NSMutableDictionary *dic = [NSJSONSerialization JSONObjectWithData:jsonData options:NSJSONWritingPrettyPrinted error:nil];
//     NSString * levelInfo = [dic valueForKey:@"level"];
//     NSString * adjustKey = [dic valueForKey:@"adjustKey"];

//     TrackerLevel* level = [[TrackerLevel alloc] init];

//     [level setLevel:levelInfo];
//     [level setAdjustKey:adjustKey];

//     [[GamePotAd getInstance] tracking:LEVEL obj:level];
// #endif
// }

// - (void) createEventData:(NSString*) eventData
// {
// #if __has_include(<GamePotAd/GamePotAd.h>)
//     NSData* jsonData = [eventData dataUsingEncoding:NSUTF8StringEncoding];
//     NSMutableDictionary *dic = [NSJSONSerialization JSONObjectWithData:jsonData options:NSJSONWritingPrettyPrinted error:nil];
//     NSString * eventInfo = [dic valueForKey:@"event"];
//     NSString * adjustKey = [dic valueForKey:@"adjustKey"];

//     TrackerEvent* event = [[TrackerEvent alloc] init];
//     [event setEvent:eventInfo];
//     [event setAdjustKey:adjustKey];

//     [[GamePotAd getInstance] tracking:EVENT obj:event];
// #endif
// }

//tracking 가공 함수 End

// - (void) tracking:(NSString*) adActions info:(NSString *)info {
//     if([adActions isEqualToString:@"TUTORIAL_COMPLETE"])
//         [self createTutirialData:info];
//     else if([adActions isEqualToString:@"LEVEL"])
//         [self createLevelData:info];
//     else if([adActions isEqualToString:@"EVENT"])
//         [self createEventData:info];
// }

// // NaverCafe
// - (void) naverCafeInit {
//     #if __has_include(<NNavarCafe/NNavarCafe.h>)
//     [[NNaverCafe getInstance] setup];
//     #endif
// }

// - (void) naverCafeInitGlobal {
//     #if __has_include(<NNavarCafe/NNavarCafe.h>)
//     [[NNaverCafe getInstance] setupGlobal];
//     #endif
// }

// - (void) naverCafeStartHome {
//     #if __has_include(<NNavarCafe/NNavarCafe.h>)
//     [[NNaverCafe getInstance] start:UnityGetGLViewController() setMenuIndex:0];
//     #endif
// }

// - (void) naverCafeStartImageWrite:(NSString*) imageUri {
//     #if __has_include(<NNavarCafe/NNavarCafe.h>)
//     [[NNaverCafe getInstance] startImageWrite:UnityGetGLViewController() setFilepath:imageUri];
//     #endif
// }

// - (void) naverCafeStartAllVideos {}
// - (void) naverCafeStartWrite {}
// - (void) naverCafeStartAllImages {}

// - (void) naverCafeStartVideoWrite:(NSString*)videoUri {
//     #if __has_include(<NNavarCafe/NNavarCafe.h>)
//     [[NNaverCafe getInstance] startVideoWrite:UnityGetGLViewController() setFilepath:videoUri];
//     #endif
// }



// - (void) naverCafeSetWidgetEnable:(BOOL) enable {
//     #if __has_include(<NNavarCafe/NNavarCafe.h>)
//     [[NNaverCafe getInstance] setWidgetEnable:enable];
//     #endif
// }

// - (void) naverCafeSetUseScreenshot:(BOOL) enable {
//     #if __has_include(<NNavarCafe/NNavarCafe.h>)
//     [[NNaverCafe getInstance] setUseWidgetScreenShot:enable];
//     #endif
// }

// - (void) naverCafeSetUserId:(NSString*) userId {
//     #if __has_include(<NNavarCafe/NNavarCafe.h>)
//     [[NNaverCafe getInstance] setUserId:userId];
//     #endif
// }

// - (void) naverCafeSetChannelCode:(NSString*) channelCode {
//     #if __has_include(<NNavarCafe/NNavarCafe.h>)
//     [[NNaverCafe getInstance] setChannelCode:channelCode];
//     #endif
// }

// - (void) naverCafeSetUseVideoRecord:(BOOL) enable {
//     #if __has_include(<NNavarCafe/NNavarCafe.h>)
//     [[NNaverCafe getInstance] setUseWidgetVideoRecord:enable];
//     #endif
// }

// - (void) naverCafeStartMenu:(int) menuId {
//     #if __has_include(<NNavarCafe/NNavarCafe.h>)
//     [[NNaverCafe getInstance] start:UnityGetGLViewController() setMenuIndex:menuId];
//     #endif
// }


- (void) logout
{
    [[GamePotChannel getInstance] LogoutWithSuccess:^{
        UnitySendMessage(ListenerForUnity, "onLogoutSuccess",  "");
    } fail:^(NSError *error) {
        UnitySendMessage(ListenerForUnity, "onLogoutFailure", [self getError:error]);
    }];
}
- (void ) setLogin: (NSString *) username  password:(NSString *) password  {
    [[GamePotChannel getInstance] setLogin:username password: password];
}
- (void) login:(NSString*) loginType {

    if([loginType isEqualToString:@"GUEST"]){
        [self invokeGuestLogin];
    }
    else if([loginType isEqualToString:@"GOOGLE"]){
        [self invokeGoogleLogin];
    }
    else if([loginType isEqualToString:@"FACEBOOK"]){
        [self invokeFBLogin];
    }
    else if([loginType isEqualToString:@"GAMECENTER"]){
        [self invokeGameCenterLogin];
    }
    else if([loginType isEqualToString:@"NAVER"]){
        [self invokeNaverLogin];
    }
    else if([loginType isEqualToString:@"TWITTER"]){
        [self invokeTwitterLogin];
    }
    else if([loginType isEqualToString:@"LINE"]){
        [self invokeLineLogin];
    }
    else if([loginType isEqualToString:@"APPLE"]){
        [self invokeAppleLogin];
    }
    else if([loginType isEqualToString:@"EMAIL"]){
        [self invokeEmailLogin];
    }
    else if([loginType isEqualToString:@"OAUTH"]){
        [self invokeOAuthLogin];
    }
    else if([loginType isEqualToString:@"NONE"]){}
}

-(void) showLoginWithUI:(NSString*) info
{
    NSError* error = nil;
    NSDictionary* dic = [NSJSONSerialization JSONObjectWithData:[info dataUsingEncoding:NSUTF8StringEncoding] options:NSJSONReadingMutableContainers error:&error];

    if(error)
    {
        NSLog(@"Update Parsing Error = %@",[error description]);
        return;
    }

    GamePotChannelLoginOption* option = [[GamePotChannelLoginOption alloc] init];

    if([GamePotUtil isEmptyString:[dic valueForKey:@"loginTypes"]] == NO)
    {
        NSString* data = [dic valueForKey:@"loginTypes"];
//        NSLog(@"loginTypes = %@", data);
        NSArray* arrData = [data componentsSeparatedByString:@","];

        NSMutableArray* order = [[NSMutableArray alloc] init];
        for(id key in arrData)
        {
            GamePotChannelType type = [GamePotChannelTypeUtil ChannelTypeEnumFromString:[key lowercaseString]];
            [order addObject:@(type)];
        }
        [option setOrder:order];
    }

    if([GamePotUtil isEmptyString:[dic valueForKey:@"showLogo"]] == NO)
    {
        NSString* data = [dic valueForKey:@"showLogo"];
        [option setShowLogo:[data isEqualToString:@"true"] ? YES : NO];
    }

    dispatch_async(dispatch_get_main_queue(), ^{
        @try {
            [[GamePotChannel getInstance] showLoginWithUI:UnityGetGLViewController() option:option success:^(GamePotUserInfo *userInfo) {
                UnitySendMessage(ListenerForUnity, "onLoginSuccess", [self getUser:userInfo]);
            }
            update:^(GamePotAppStatus *appStatus) {
                // 업데이트
                if([appStatus resultPayload] != nil)
                {
                    [appStatus setResultPayload:[(GamePotUserInfo*)[appStatus resultPayload] toJsonString]];
                }
                UnitySendMessage(ListenerForUnity, "onNeedUpdate", [[appStatus toJsonString] UTF8String]);
            } maintenance:^(GamePotAppStatus *appStatus) {
                UnitySendMessage(ListenerForUnity, "onMainternance", [[appStatus toJsonString] UTF8String]);
            } exit:^{
                // showLoginWithUI 종료
                UnitySendMessage(ListenerForUnity, "onLoginExit", "");
            }];
        }
        @catch (NSException *exception) {
            NSLog(@"createLoginWithUI exception: %@", exception);
            UnitySendMessage(ListenerForUnity, "onLoginFailure", "");
        }
    });
}


// - (void) addAd:(NSString*) adType
// {
// #if __has_include(<GamePotAd/GamePotAd.h>)
//     if([adType isEqualToString:@"FACEBOOK"]){
// #if __has_include(<GamePotAdFacebook/GamePotAdFacebook.h>)
//         [[GamePotAd getInstance] addAds:[[GamePotAdFacebook alloc] init]];
// #endif
//     }
//     else if([adType isEqualToString:@"ADJUST"]){
// #if __has_include(<GamePotAdAdjust/GamePotAdAdjust.h>)
//         [[GamePotAd getInstance] addAds:[[GamePotAdAdjust alloc] init]];
// #endif
//     }
// #endif
// }

- (void) enableGameCenter:(BOOL) enable;
{
    [[GamePotChannel getInstance] enableGameCenter:enable];
}

-(void) purchase:(NSString*)productId uniqueId:(NSString*)uniqueId serverId:(NSString*)serverId playerId:(NSString*)playerId etc:(NSString*)etc
{
    [[GamePot getInstance] purchase:productId uniqueId:uniqueId serverId:serverId playerId:playerId etc:etc];
}

- (void) showNaverCafe:(int)menuId setLandScape:(BOOL)landscape
{
#if __has_include(<GamePotNaverCafe/GamePotNaverCafe.h>)
    dispatch_async(dispatch_get_main_queue(), ^{
        [[GamePotNaverCafe getInstance] setOrientationIsLandscape:landscape];
        if(menuId > 0)
            [[GamePotNaverCafe getInstance] start:UnityGetGLViewController() setMenuIndex: menuId];
        else
            [[GamePotNaverCafe getInstance] start:UnityGetGLViewController()];
    });
#endif
}
- (void) deleteLinking:(NSString*)linkType
{
   GamePotChannelType channelType = NONE;
     if([linkType isEqualToString:@"GOOGLE"])
         channelType = GOOGLE;
     else if([linkType isEqualToString:@"FACEBOOK"])
        channelType = FACEBOOK;
     else if([linkType isEqualToString:@"GAMECENTER"])
        channelType = GAMECENTER;
     else if([linkType isEqualToString:@"NAVER"])
        channelType = NAVER;
     else if([linkType isEqualToString:@"FACEBOOK"])
        channelType = FACEBOOK;
     else if([linkType isEqualToString:@"TWITTER"])
        channelType = TWITTER;
     else if([linkType isEqualToString:@"LINE"])
        channelType = LINE;
    else if([linkType isEqualToString:@"APPLE"])
        channelType = APPLE;
    else if([linkType isEqualToString:@"EMAIL"])
        channelType = EMAIL;
     else
        return;

    [[GamePotChannel getInstance] DeleteLinking: channelType success:^ {
        UnitySendMessage(ListenerForUnity, "onDeleteLinkingSuccess", "");
    } fail:^(NSError *error) {
        if(error)
        {
            UnitySendMessage(ListenerForUnity, "onDeleteLinkingFailure", [self getError:error]);
            return;
        }

        UnitySendMessage(ListenerForUnity, "onCreateLinkingFailure", "");
    }];

}
- (void) createLinking:(NSString*)linkType
{
    GamePotChannelType channelType = NONE;
     if([linkType isEqualToString:@"GOOGLE"])
         channelType = GOOGLE;
     else if([linkType isEqualToString:@"FACEBOOK"])
        channelType = FACEBOOK;
     else if([linkType isEqualToString:@"GAMECENTER"])
        channelType = GAMECENTER;
     else if([linkType isEqualToString:@"NAVER"])
        channelType = NAVER;
     else if([linkType isEqualToString:@"FACEBOOK"])
        channelType = FACEBOOK;
     else if([linkType isEqualToString:@"TWITTER"])
        channelType = TWITTER;
     else if([linkType isEqualToString:@"LINE"])
        channelType = LINE;
     else if([linkType isEqualToString:@"APPLE"])
        channelType = APPLE;
     else if([linkType isEqualToString:@"EMAIL"])
        channelType = EMAIL;
     else
        return;

    dispatch_async(dispatch_get_main_queue(), ^{
        [[GamePotChannel getInstance] CreateLinking:channelType viewController:UnityGetGLViewController() success:^(GamePotUserInfo* userInfo) {
            UnitySendMessage(ListenerForUnity, "onCreateLinkingSuccess", [self getUser:userInfo]);
        } cancel:^{
            UnitySendMessage(ListenerForUnity, "onCreateLinkingCancel", "");
        } fail:^(NSError *error) {
            if(error)
            {
                UnitySendMessage(ListenerForUnity, "onCreateLinkingFailure", [self getError:error]);
                return;
            }

            UnitySendMessage(ListenerForUnity, "onCreateLinkingFailure", "");
        }];
    });
}

- (void) addChannel:(NSString*)channelType
{
    if([channelType isEqualToString:@"FACEBOOK"])
    {
#if __has_include(<GamePotFacebook/GamePotFacebook.h>)
        GamePotChannelInterface* facebook = [[GamePotFacebook alloc] init];
        [[GamePotChannel getInstance] addChannelWithType:FACEBOOK interface:facebook];
#endif
    }
    else if([channelType isEqualToString:@"GOOGLE"])
    {
#if __has_include(<GamePotGoogleSignIn/GamePotGoogleSignIn.h>)
        GamePotChannelInterface* google   = [[GamePotGoogleSignIn alloc] init];
        [[GamePotChannel getInstance] addChannelWithType:GOOGLE interface:google];
#endif
    }
    else if([channelType isEqualToString:@"GAMECENTER"])
    {
#if __has_include(<GamePotGameCenter/GamePotGameCenter.h>)
        GamePotChannelInterface* gamecenter = [[GamePotGameCenter alloc] init];
        [[GamePotChannel getInstance] addChannelWithType:GAMECENTER interface:gamecenter];
#endif
    }
    else if([channelType isEqualToString:@"NAVER"])
    {
#if __has_include(<GamePotNaver/GamePotNaver.h>)
        GamePotChannelInterface* naver    = [[GamePotNaver alloc] init];
        [[GamePotChannel getInstance] addChannelWithType:NAVER interface:naver];
#endif
    }
    else if([channelType isEqualToString:@"LINE"])
    {
#if __has_include(<GamePotLine/GamePotLine.h>)
        GamePotChannelInterface* line    = [[GamePotLine alloc] init];
        [[GamePotChannel getInstance] addChannelWithType:NAVER interface:line];
#endif
    }
    else if([channelType isEqualToString:@"TWITTER"])
    {
#if __has_include(<GamePotTwitter/GamePotTwitter.h>)
        GamePotChannelInterface* twitter    = [[GamePotTwitter alloc] init];
        [[GamePotChannel getInstance] addChannelWithType:NAVER interface:twitter];
#endif
    }
    else if([channelType isEqualToString:@"APPLE"])
    {
#if __has_include(<GamePotApple/GamePotApple.h>)
        GamePotChannelInterface* apple    = [[GamePotApple alloc] init];
        [[GamePotChannel getInstance] addChannelWithType:APPLE interface:apple];
#endif
    }
    else if([channelType isEqualToString:@"EMAIL"])
    {
#if __has_include(<GamePotEmail/GamePotEmail.h>)
        GamePotChannelInterface* email    = [[GamePotEmail alloc] init];
        [[GamePotChannel getInstance] addChannelWithType:EMAIL interface:email];
#endif
    }
    else if([channelType isEqualToString:@"OAUTH"])
    {
#if __has_include(<GamePotOAuth/GamePotOAuth.h>)
        GamePotChannelInterface* oauth    = [[GamePotOAuth alloc] init];
        [[GamePotChannel getInstance] addChannelWithType:OAUTH interface:oauth];
#endif
    }
}

- (NSString*) getPurchaseItems
{
   return [[GamePot getInstance] getPurchaseItemsJsonString];
}

- (void) getPurchaseDetailListAsync
{
    @try{
        [[GamePot getInstance] getPurchaseDetailListAsyncToJSON:^(BOOL _success, NSString *_items, NSError *_error)
        {
            if(_success)
            {
                if(_items)
                {
                    UnitySendMessage(ListenerForUnity, "onPurchaseDetailListSuccess", [(NSString*)_items UTF8String]);
                }
            }else
            {
                if(_error){
                    // NSLog(@"Error = %@", [_error localizedDescription]);
                    UnitySendMessage(ListenerForUnity, "onPurchaseDetailListFailure", [self getError:_error]);
                }
            }
        }];
    }
    @catch(NSException* ex){
        NSLog(@"Error : %@", ex);
    }
}

- (void) setAdjustData:(NSString*)adjustBillingData
{
    //    #if __has_include(<GamePotAdAdjust/GamePotAdAdjust.h>)
    //    NSData* jsonData = [adjustBillingData dataUsingEncoding:NSUTF8StringEncoding];
    //    adjustBillingDic = [NSJSONSerialization JSONObjectWithData:jsonData options:NSJSONWritingPrettyPrinted error:nil];
    //    #endif
}

- (void) setPush:(BOOL) pushEnable
{
    [[GamePot getInstance] setPushEnable:pushEnable success:^{
        UnitySendMessage(ListenerForUnity, "onPushSuccess","");
    } fail:^(NSError *error) {
        UnitySendMessage(ListenerForUnity, "onPushFailure", [self getError:error]);
    }];
}

- (void) setPushNight:(BOOL) pushEnable
{
    [[GamePot getInstance] setNightPushEnable:pushEnable success:^{
        UnitySendMessage(ListenerForUnity, "onPushNightSuccess","");
    } fail:^(NSError *error) {
        UnitySendMessage(ListenerForUnity, "onPushNightFailure", [self getError:error]);
    }];
}

- (void) setPushAd:(BOOL) pushEnable
{
    [[GamePot getInstance] setAdPushEnable:pushEnable success:^{
        UnitySendMessage(ListenerForUnity, "onPushAdSuccess","");
    } fail:^(NSError *error) {
        UnitySendMessage(ListenerForUnity, "onPushAdFailure", [self getError:error]);
    }];
}

- (void) setPushStatus:(BOOL) pushEnable setNight:(BOOL) nightPushEnable setAd:(BOOL) adPushEnable
{
    [[GamePot getInstance] setPushStatus:pushEnable night:nightPushEnable ad:adPushEnable success:^{
        UnitySendMessage(ListenerForUnity, "onPushStatusSuccess","");
    } fail:^(NSError *error) {
        UnitySendMessage(ListenerForUnity, "onPushStatusFailure", [self getError:error]);
    }];

}

- (void) showNoticeWebView
{
    dispatch_async(dispatch_get_main_queue(), ^{
        [[GamePot getInstance] showNoticeWebView:UnityGetGLViewController()];
    });
}

- (void) showCSWebView
{
    dispatch_async(dispatch_get_main_queue(), ^{
        [[GamePot getInstance] showHelpWebView:UnityGetGLViewController()];
    });
}

- (void) showWebView:(NSString*)url
{
    dispatch_async(dispatch_get_main_queue(), ^{
        [[GamePot getInstance] showWebView:UnityGetGLViewController() setType:WEBVIEW_NORMALWITHBACK setURL:url];
    });
}

- (void) showWebView:(NSString*)url setBackButton:(BOOL) hasBackButton
{
    if(hasBackButton) {
        dispatch_async(dispatch_get_main_queue(), ^{
            [[GamePot getInstance] showWebView:UnityGetGLViewController() setType:WEBVIEW_NORMALWITHBACK setURL:url];
        });
    } else {
        dispatch_async(dispatch_get_main_queue(), ^{
            [[GamePot getInstance] showWebView:UnityGetGLViewController() setType:WEBVIEW_NORMAL setURL:url];
        });
    }
}

// - (void) setSandbox:(BOOL) enable
// {
// #if __has_include(<GamePotAd/GamePotAd.h>)
//    [[GamePotAd getInstance] setSandbox:enable];
// #endif
// }

- (void) showAppStatus:(NSString*)status
{
    if(status == nil || [status isEqualToString:@""])
    {
        NSLog(@"Status is empty");

        return;
    }

    NSError* error = nil;

    NSDictionary* dic = [NSJSONSerialization JSONObjectWithData:[status dataUsingEncoding:NSUTF8StringEncoding] options:NSJSONReadingMutableContainers error:&error];

    if(error)
    {
        NSLog(@"Update Parsing Error = %@",[error description]);

        return;
    }

    NSString* type = [dic valueForKey:@"type"];

    if(type == nil || [type isEqual:[NSNull null]])
    {
        return;
    }

    NSLog(@"type = %@", type);

    if([type isEqualToString:@"maintenance"])
    {
        NSLog(@"Start MAINTENANCE");
        id objMessage = [dic valueForKey:@"message"];

        if(objMessage != nil && [objMessage isEqual:[NSNull null]] == NO)
        {
            NSString* url = [dic valueForKey:@"url"];

            if(url == nil || [url isEqual:[NSNull null]])
            {
                url = @"";
            }

            GamePotAppStatus* appStatus = [[GamePotAppStatus alloc] init:[dic objectForKey:@"message"] setURL:url];

            id objStartedAt = [dic valueForKey:@"startedAt"];
            id objEndedAt = [dic valueForKey:@"endedAt"];

            if(objStartedAt != nil && [objStartedAt isEqual:[NSNull null]] == NO
               && objEndedAt != nil && [objEndedAt isEqual:[NSNull null]] == NO)
            {
                //Native Framework 하위호환을 위해 startedAt / endedAt 분리
                @try
                {
                    [appStatus setStartedAt:[[dic objectForKey:@"startedAt"]longValue]];
                    [appStatus setEndedAt:[[dic objectForKey:@"endedAt"]longValue]];
                }
                @catch(NSException* error)
                {
                        NSLog(@"started/ended Setter Error = %@",[error description]);
                }
            }

            [[GamePot getInstance] showAppStatusPopup:UnityGetGLViewController() setAppStatus:appStatus setCloseHandler:^{
                UnitySendMessage(ListenerForUnity, "onAppClose","");
            }];
        }

    }
    else if([type isEqualToString:@"needupdate"])
    {
        NSLog(@"Start NEEDUPDATE");
        id objMessage = [dic valueForKey:@"message"];
        id objCurrentAppVersion = [dic valueForKey:@"currentAppVersion"];
        id objUpdateAppVersion = [dic valueForKey:@"updateAppVersion"];
        id objCurrentAppVersionCode = [dic valueForKey:@"currentAppVersionCode"];
        id objUpdateAppVersionCode = [dic valueForKey:@"updateAppVersionCode"];
        id objIsForce = [dic valueForKey:@"isForce"];
        id objResultPayload = [dic valueForKey:@"resultPayload"];
        id objUrl = [dic valueForKey:@"url"];

        if(objMessage != nil && [objMessage isEqual:[NSNull null]] == NO &&
            objCurrentAppVersion != nil && [objCurrentAppVersion isEqual:[NSNull null]] == NO &&
            objUpdateAppVersion != nil && [objUpdateAppVersion isEqual:[NSNull null]] == NO &&
            objCurrentAppVersionCode != nil && [objCurrentAppVersionCode isEqual:[NSNull null]] == NO &&
            objUpdateAppVersionCode != nil && [objUpdateAppVersionCode isEqual:[NSNull null]] == NO &&
            objIsForce != nil && [objIsForce isEqual:[NSNull null]] == NO &&
            objResultPayload != nil && [objResultPayload isEqual:[NSNull null]] == NO)
        {

            GamePotAppStatus* appStatus = [[GamePotAppStatus alloc] init:[dic objectForKey:@"message"]
                    setCurrentAppVersion:[dic objectForKey:@"currentAppVersion"]
                     setUpdateAppVersion:[dic objectForKey:@"updateAppVersion"]
            setCurrentAppVersionCode:[[dic objectForKey:@"currentAppVersionCode"] intValue]
             setUpdateAppVersionCode:[[dic objectForKey:@"updateAppVersionCode"] intValue]
                                            setForce:[[dic objectForKey:@"isForce"] boolValue]];

            //Native Framework 하위호환을 위해 SetUrl로 분리
            @try
            {
                NSString* function = @"setURL:";
                NSString* param = [dic objectForKey:@"url"];

                SEL _selector = NSSelectorFromString(function);

                if([appStatus respondsToSelector:_selector])
                {
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Warc-performSelector-leaks"
                    [appStatus performSelector:_selector withObject:param];
#pragma clang diagnostic pop
                }
            }
            @catch(NSException* error)
            {
                    NSLog(@"setURL Error = %@",[error description]);
            }

            // checkAppStatus api의 경우 로그인 정보(resultPayload)가 존재 하지 않음 / 이 경우 권장 업데이트일때 NEXT 버튼클릭시 무의미한 이벤트 발생하게 하여 이후 동작에 문제 없도록 수정
            if (objResultPayload == nil || [objResultPayload isEqual:[NSNull null]] == YES)
            {
                NSLog(@"objResultPayload is nil or null ");
                [[GamePot getInstance] showAppStatusPopup:UnityGetGLViewController()
                                             setAppStatus:appStatus
                                          setCloseHandler:^{
                                              UnitySendMessage(ListenerForUnity, "onAppClose","");
                                          } setNextHandler:^(NSObject *resultPayload) {
                                              UnitySendMessage(ListenerForUnity, "onLoginNext", "");
                                          }];
            }
            else
            {
            [appStatus setResultPayload:objResultPayload];

            [[GamePot getInstance] showAppStatusPopup:UnityGetGLViewController()
                                         setAppStatus:appStatus
                                      setCloseHandler:^{
                                          UnitySendMessage(ListenerForUnity, "onAppClose","");
                                      } setNextHandler:^(NSObject *resultPayload) {
                                          UnitySendMessage(ListenerForUnity, "onLoginSuccess", [(NSString*)resultPayload UTF8String]);
                                      }];
            }
        }
    }
}

- (void) showAgreeDialog:(NSString*)_option
{
    dispatch_async(dispatch_get_main_queue(), ^{

        //파라메터 변경위해, 임시 dictionary parsing
        NSError* error = nil;
        NSDictionary* dic = [NSJSONSerialization JSONObjectWithData:[_option dataUsingEncoding:NSUTF8StringEncoding] options:NSJSONReadingMutableContainers error:&error];
        if(error)
        {
            NSLog(@"Update Parsing Error = %@",[error description]);
            return;
        }

        GamePotAgreeOption* option = [[GamePotAgreeOption alloc] initWithString:_option];
        [option setFooterTopColor:[[GamePotUtil convertNumber:[dic valueForKey:@"contentBottomColor"]] intValue]];

        [[GamePot getInstance] showAgreeView:UnityGetGLViewController() option:option handler:^(GamePotAgreeInfo *result) {

            if(result != nil)
            {
                UnitySendMessage(ListenerForUnity, "onAgreeDialogSuccess", [[result toJsonString] UTF8String]);
            }
            else
            {
                NSMutableDictionary* details = [[NSMutableDictionary alloc] init];
                [details setObject:@"Agree Error" forKey:NSLocalizedDescriptionKey];

                NSError* error = [[NSError alloc] initWithDomain:@"GamePot" code:-1000 userInfo:details];
                UnitySendMessage(ListenerForUnity, "onAgreeDialogFailure", [self getError:error] );
            }
        }];
    });
}

- (void) setVoidOption:(NSString*)info
{
    [[GamePot getInstance] setVoidOptionWithString:info];
}

- (void) setVoidDialogHelpURL:(NSString*)url
{
    [[GamePot getInstance] setVoidDialogHelpURL:url];
}

- (void) showVoidViewDebug
{
    dispatch_async(dispatch_get_main_queue(), ^{
        [[GamePot getInstance] showVoidViewDebug:UnityGetGLViewController()];
    });
}

- (void)showTerms
{
    [[GamePot getInstance] showTerms:UnityGetGLViewController()];
}

- (void)showPrivacy
{
    [[GamePot getInstance] showPrivacy:UnityGetGLViewController()];
}

- (void)sendLog:(NSString*)type errCode:(NSString*)errCode errMessage:(NSString*)errMessage
{
#if __has_include(<GamePotLogger/GamePotLogger.h>)
    if([type isEqualToString:@"d"])
    {
        [GamePotLogger debug:errCode withMessage:errMessage];
    }
    else if([type isEqualToString:@"i"])
    {
        [GamePotLogger info:errCode withMessage:errMessage];
    }
    else if([type isEqualToString:@"w"])
    {
        [GamePotLogger warn:errCode withMessage:errMessage];
    }
    else if([type isEqualToString:@"e"])
    {
        [GamePotLogger error:errCode withMessage:errMessage];
    }
#endif

}

- (void)showEvent:(NSString *)type
{
    dispatch_async(dispatch_get_main_queue(), ^{
        [[GamePot getInstance] showEvent:UnityGetGLViewController() setType:type setSchemeHandler:^(NSString *scheme){
            UnitySendMessage(ListenerForUnity, "onReceiveScheme", [scheme UTF8String]);
        } setExitHandler:nil];
    });
}

- (void)showNotice
{
    dispatch_async(dispatch_get_main_queue(), ^{
        [[GamePot getInstance] showNotice:UnityGetGLViewController() setSchemeHandler:^(NSString *scheme) {
            UnitySendMessage(ListenerForUnity, "onReceiveScheme", [scheme UTF8String]);
        }];
    });
}

- (void)showNotice:(BOOL)showTodayButton
{
    dispatch_async(dispatch_get_main_queue(), ^{
    [[GamePot getInstance] showNotice:UnityGetGLViewController() setShowTodayButton:showTodayButton setSchemeHandler:^(NSString *scheme) {
         UnitySendMessage(ListenerForUnity, "onReceiveScheme", [scheme UTF8String]);
    } setExitHandler:nil];
        });
}

- (void)showFaq
{
    dispatch_async(dispatch_get_main_queue(), ^{
        [[GamePot getInstance] showFAQWebView:UnityGetGLViewController()];
    });

}

- (BOOL)characterInfo:(NSString *)info
{
     if(info == nil || [info isEqualToString:@""])
    {
        NSLog(@"Info is empty");
        return NO;
    }

#if __has_include(<Gamepot/GamePotSendLog.h>)
    NSError* error = nil;
    NSDictionary* dic = [NSJSONSerialization JSONObjectWithData:[info dataUsingEncoding:NSUTF8StringEncoding] options:NSJSONReadingMutableContainers error:&error];

    if(error)
    {
        NSLog(@"Update Parsing Error = %@",[error description]);

        return NO;
    }

    GamePotSendLogCharacter* body = [[GamePotSendLogCharacter alloc] init];

    for(id key in dic)
    {
        [body put:[dic valueForKey:key] forKey:key];
        NSLog(@"value: %@, Key: %@", [dic valueForKey:key], key);
    }

    BOOL result = [GamePotSendLog characterInfo:body];

    return result;
#else
    return NO;
#endif
}

- (void)setLoggerUserid:(NSString *)userid
{
    #if __has_include(<GamePotLogger/GamePotLogger.h>)
    [GamePotLogger setUserId:userid];
    #endif
}

- (NSString*) getPushToken
{
    @try
    {
         return [[GamePot getInstance] getPushToken];
    }
    @catch(NSException *exception)
    {
        NSLog(@"GetPushToken exception: %@", exception);
        return @"";
    }
}

- (NSString*) getFCMToken
{
    @try
    {
         return [[GamePot getInstance] getFCMToken];
    }
    @catch(NSException *exception)
    {
        NSLog(@"getFCMToken exception: %@", exception);
        return @"";
    }
}

- (void)showRefund
{
    dispatch_async(dispatch_get_main_queue(), ^{
        [[GamePot getInstance] showRefund:UnityGetGLViewController()];
    });
}


- (void)sendPurchaseByThirdPartySDK:(NSString *)productId
                            transactionId:(NSString*)_transactionId
                            currency:(NSString*)_currency
                            price:(NSDecimalNumber*)_price
                            paymentId:(NSString*)_paymentId
                            uniqueId:(NSString*)_uniqueId
                            storeId:(NSString*)_storeId
{
    [[GamePot getInstance] sendPurchaseByThirdPartySDK:productId transactionId:_transactionId currency:_currency price:_price paymentId:_paymentId uniqueId:_uniqueId storeId:_storeId
    success:^{
        UnitySendMessage(ListenerForUnity, "onPurchaseSuccess","");
    }
    fail:^(NSError *error) {
        if(error)
        {
            UnitySendMessage(ListenerForUnity, "onPurchaseFailure", [self getError:error]);
        }
        else
        {
            UnitySendMessage(ListenerForUnity, "onPurchaseFailure", "");
        }
    }];
}

- (void) loginByThirdPartySDK:(NSString*)_userId
{
    dispatch_async(dispatch_get_main_queue(), ^{
        NSLog(@"loginByThirdPartySDK");
        @try
        {
            [[GamePotChannel getInstance] loginByThirdPartySDK:UnityGetGLViewController() uId:_userId
                success:^(GamePotUserInfo *userInfo) {
                    UnitySendMessage(ListenerForUnity, "onLoginSuccess", [self getUser:userInfo]);
                    }
                fail:^(NSError *error) {
                    if(error)
                    {
                        UnitySendMessage(ListenerForUnity, "onLoginFailure", [self getError:error]);
                        return;
                    }
                    UnitySendMessage(ListenerForUnity, "onLoginFailure", "");
                }
                update:^(GamePotAppStatus *appStatus) {
                    if([appStatus resultPayload] != nil)
                    {
                        [appStatus setResultPayload:[(GamePotUserInfo*)[appStatus resultPayload] toJsonString]];
                    }
                    UnitySendMessage(ListenerForUnity, "onNeedUpdate", [[appStatus toJsonString] UTF8String]);
                }
                maintenance:^(GamePotAppStatus *appStatus) {
                    UnitySendMessage(ListenerForUnity, "onMainternance", [[appStatus toJsonString] UTF8String]);
                }];
        }
        @catch(NSException *exception)
        {
            NSLog(@"loginByThirdPartySDK exception: %@", exception);
            UnitySendMessage(ListenerForUnity, "onLoginFailure", "");
        }
    });
}

- (NSString*) getCountry
{
    @try
    {
         NSLog(@"getCountry = %@", [[GamePot getInstance] getCountry]);
         return [[GamePot getInstance] getCountry];
    }
    @catch(NSException *exception)
    {
        NSLog(@"getCountry exception: %@", exception);
        return @"";
    }
}

- (NSString*) getRemoteIP
{
    @try
    {
        NSLog(@"getRemoteIP = %@", [[GamePot getInstance] getRemoteIP]);
         return [[GamePot getInstance] getRemoteIP];
    }
    @catch(NSException *exception)
    {
        NSLog(@"getRemoteIP exception: %@", exception);
        return @"";
    }
}

- (NSString*) getGDPRCheckedList
{
    @try
    {
         NSLog(@"getGDPRCheckedList = %@", [[GamePot getInstance] getGDPRCheckedListJsonString]);
         return [[GamePot getInstance] getGDPRCheckedListJsonString];
    }
    @catch(NSException *exception)
    {
        NSLog(@"getGDPRCheckedList exception: %@", exception);
        return @"";
    }
}

- (void) safetyToast:(NSString*)_message
{
    dispatch_async(dispatch_get_main_queue(), ^{
        [[GamePot getInstance] safetyToast:UnityGetGLViewController() message:_message];
    });
}

- (void) requestTrackingAuthorization
{
    NSString* result = @"";

    #if __has_include(<AppTrackingTransparency/AppTrackingTransparency.h>)
   if (@available(iOS 14, *)) {
       if(NSClassFromString(@"ATTrackingManager"))
       {
           // 리스너 등록 되어 있지 않을 시 요청 팝업 발생 되지 않음.
           [ATTrackingManager requestTrackingAuthorizationWithCompletionHandler:^(ATTrackingManagerAuthorizationStatus status) {
               NSString* _result = @"";
               switch (status)
               {
                   case ATTrackingManagerAuthorizationStatusNotDetermined:
                       _result = @"ATTrackingManagerAuthorizationStatusNotDetermined";
                       break;
                   case ATTrackingManagerAuthorizationStatusRestricted:
                       _result = @"ATTrackingManagerAuthorizationStatusRestricted";
                       break;
                   case ATTrackingManagerAuthorizationStatusDenied:
                       _result = @"ATTrackingManagerAuthorizationStatusDenied";
                       break;
                   case ATTrackingManagerAuthorizationStatusAuthorized:
                       _result = @"ATTrackingManagerAuthorizationStatusAuthorized";
                       break;
                   default:
                       break;
               }
                UnitySendMessage(ListenerForUnity, "onRequestTrackingAuthorization", [_result UTF8String]);
           }];
            return;
       }
   }
    #endif
    UnitySendMessage(ListenerForUnity, "onRequestTrackingAuthorization", [result UTF8String]);
}

- (void) checkAppStatus
{
    @try
    {
        [[GamePot getInstance] checkAppStatus :^(){
            UnitySendMessage(ListenerForUnity, "onCheckAppStatusSuccess", "");
        }
                               setFailHandler:^(NSError* error){

            UnitySendMessage(ListenerForUnity, "onCheckAppStatusFailure", [self getError:error]);
        }
                             setUpdateHandler:^(GamePotAppStatus *status){
            if([status resultPayload] != nil)
            {
                [status setResultPayload:[(GamePotUserInfo*)[status resultPayload] toJsonString]];
            }
            UnitySendMessage(ListenerForUnity, "onNeedUpdate", [[status toJsonString] UTF8String]);
        }
                        setMaintenanceHandler:^(GamePotAppStatus *status){
            UnitySendMessage(ListenerForUnity, "onMainternance", [[status toJsonString] UTF8String]);
        }];
    }
    @catch(NSException *exception)
    {
        NSLog(@"checkAppStatus exception: %@", exception);
    }
}

- (void) setAutoAgree:(BOOL)autoAgree
{
    [[GamePot getInstance] setAutoAgree:autoAgree];
}
- (void) setAutoAgreeBuilder:(NSString*)_info
{
    GamePotAgreeOption* option = [[GamePotAgreeOption alloc] initWithString:_info];
    [[GamePot getInstance] setAgreeBuilder:option];
}

- (void) sendAgreeEmail:(NSString*)_email
{
    @try {
        [[GamePot getInstance] sendAgreeEmail:_email handler:^(NSDictionary *_data, NSError *_error) {

            if(_error)
            {
                UnitySendMessage(ListenerForUnity, "onSendAgreeEmailFailure", [self getError:_error]);
                return;
            }

            NSError* jsonParsingError = nil;
            NSData* jsonData =[NSJSONSerialization dataWithJSONObject:_data options:NSJSONWritingPrettyPrinted error:&jsonParsingError];
            if(jsonParsingError != nil)
            {
                NSLog(@"sendAgreeEmail Error Result json parsing Error");
                UnitySendMessage(ListenerForUnity, "onSendAgreeEmailFailure", "");
                return;
            }

            NSString* jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
            UnitySendMessage(ListenerForUnity, "onSendAgreeEmailSuccess", [jsonString UTF8String]);
        }];
    } @catch (NSException *exception) {
        NSLog(@"sendAgreeEmail exception: %@", exception);
        UnitySendMessage(ListenerForUnity, "onSendAgreeEmailFailure", "");
    }
}

- (void) checkAgreeEmail:(NSString*)_email
              confirmKey:(NSString *)_key
{
    @try {
        [[GamePot getInstance] checkAgreeEmail:_email
                                    confirmKey:_key
                                       handler:^(NSDictionary *_data, NSError *_error){
            if(_error)
            {
                UnitySendMessage(ListenerForUnity, "onCheckAgreeEmailFailure", [self getError:_error]);
                return;
            }

            NSError* jsonParsingError = nil;
            NSData* jsonData =[NSJSONSerialization dataWithJSONObject:_data options:NSJSONWritingPrettyPrinted error:&jsonParsingError];

            if(jsonParsingError != nil)
            {
                NSLog(@"checkAgreeEmail Error Result json parsing Error");
                UnitySendMessage(ListenerForUnity, "onCheckAgreeEmailFailure", "");
                return;
            }

            NSString* jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
            UnitySendMessage(ListenerForUnity, "onCheckAgreeEmailSuccess", [jsonString UTF8String]);
        }];
    } @catch (NSException *exception) {
        NSLog(@"checkAgreeEmail exception: %@", exception);
        UnitySendMessage(ListenerForUnity, "onCheckAgreeEmailFailure", "");
    }
}

- (void) setAgreeInfo:(NSString*)_info
{
    GamePotAgreeInfo* info = [[GamePotAgreeInfo alloc] init];
    NSError* error = nil;
    NSDictionary* dic = [NSJSONSerialization JSONObjectWithData:[_info dataUsingEncoding:NSUTF8StringEncoding] options:NSJSONReadingMutableContainers error:&error];

    if(error)
    {
        //native 단으로 넘어가기 전에 생기는 error 때는 따로 콜백 없음(기존 api 방식 따름)
        NSLog(@"setAgreeInfo Parsing Error = %@",[error description]);
        return;
    }

    if([GamePotUtil isEmptyString:[dic valueForKey:@"agree"]] == NO)
    {
        NSString* data = [dic valueForKey:@"agree"];
        [info setAgree:[data isEqualToString:@"true"] ? YES:NO];
    }

    if([GamePotUtil isEmptyString:[dic valueForKey:@"agreePush"]] == NO)
    {
        NSString* data = [dic valueForKey:@"agreePush"];
        [info setAgreePush:[data isEqualToString:@"true"] ? YES:NO];
    }

    if([GamePotUtil isEmptyString:[dic valueForKey:@"agreeNight"]] == NO)
    {
        NSString* data = [dic valueForKey:@"agreeNight"];
        [info setAgreeNight:[data isEqualToString:@"true"] ? YES:NO];
    }

    if([GamePotUtil isEmptyString:[dic valueForKey:@"agreeGDPR"]] == NO)
    {
        NSString* data = [dic valueForKey:@"agreeGDPR"];
        [info setAgreeGDPR:data];
    }

    int _status = [[dic valueForKey:@"agreeGDPRStatus"] intValue];
    if(_status)
    {
        [info setAgreeGDPRStatus:_status];
    }

    @try {
        [[GamePot getInstance] setAgreeInfo:info handler:^(BOOL _success, NSError *_error) {
            if(_success)
            {
                UnitySendMessage(ListenerForUnity, "onSetAgreeInfoSuccess", "");
                return;
            }
            UnitySendMessage(ListenerForUnity, "onSetAgreeInfoFailure", [self getError:_error]);
        }];
    } @catch (NSException *exception) {
        NSLog(@"setAgreeInfo exception: %@", exception);
        UnitySendMessage(ListenerForUnity, "onSetAgreeInfoFailure", "");
    }
}

- (NSString*) getUserData
{

    NSDictionary* userData = [[GamePot getInstance] getUserData];

    if(userData == nil)
    {
        return @"";
    }

    NSError* jsonParsingError = nil;

    NSData* jsonData = [NSJSONSerialization dataWithJSONObject:userData options:NSJSONWritingPrettyPrinted error:&jsonParsingError];

    if(jsonParsingError != nil)
    {
        NSLog(@"getUserData Error Result json parsing Error");
        return @"";
    }

    NSString* jsonDataStr = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];

    NSLog(@"getUserData = %@", jsonDataStr);
    return jsonDataStr;

}

- (void) setUserData:(NSString*)_userData
{
    if([GamePotUtil isEmptyString:_userData] == YES)
    {
        NSLog(@"setUserData data is nil");
        return;
    }

    NSData *jsonData = [_userData dataUsingEncoding:NSUTF8StringEncoding];
    NSError *error = nil;
    NSDictionary *dict = [NSJSONSerialization JSONObjectWithData:jsonData options:NSJSONReadingMutableContainers error:&error];

    if(error != nil)
    {
        NSLog(@"setUserData Error Result json parsing Error");
        return;
    }

    [[GamePot getInstance] setUserData:dict handler:^(BOOL _success, NSError *_error) {

        if(_error)
        {
             UnitySendMessage(ListenerForUnity, "onSetUserDataFailure", [self getError:_error]);
            return;
        }

        UnitySendMessage(ListenerForUnity, "onSetUserDataSuccess", "");

    }];

}

- (NSString*) getProjectId
{
    @try
    {
         return [[GamePot getInstance] getProjectId];
    }
    @catch(NSException *exception)
    {
        NSLog(@"getProjectId exception: %@", exception);
        return @"";
    }
}

- (void) showCouponWebView
{
    dispatch_async(dispatch_get_main_queue(), ^{
        [[GamePot getInstance] showCouponWebView:UnityGetGLViewController()];
    });
}


@end
