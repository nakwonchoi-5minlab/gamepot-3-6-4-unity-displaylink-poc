//
//  GamePotOAuth.h
//  GamePotOAuth
//
//  Created by Kong Shinbae on 4/11/24.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import <WebKit/WebKit.h>
#import <GamePotChannel/GamePotChannel.h>

#define GAMEPOTOAUTH_VERSION @"2.1.0"

//! Project version number for GamePotOAuth.
FOUNDATION_EXPORT double GamePotOAuthVersionNumber;

//! Project version string for GamePotOAuth.
FOUNDATION_EXPORT const unsigned char GamePotOAuthVersionString[];

// In this header, you should import all the public headers of your framework using statements like #import <GamePotOAuth/PublicHeader.h>

#define OAUTH_ACCESSTOKEN @"oauth_accessToken"
#define OAUTH_USERID @"oauth_userid"
#define OAUTH_EMAIL @"oauth_email"
@interface GamePotOAuth : GamePotChannelInterface



- (void)doInit;

- (BOOL)doValidLogin;

- (void)doLoginWithViewController:(UIViewController*)_viewController
                          success:(GamePotChannelManagerSuccess)_success
                           cancel:(GamePotChannelManagerCancel)_cancel
                             fail:(GamePotChannelManagerFail)_fail;

- (void)doLocalUser:(GamePotChannelManagerSuccess)_success fail:(GamePotChannelManagerFail)_fail;

- (void)doLogout:(GamePotCommonSuccess)_success fail:(GamePotCommonFail)_fail;

- (void)application:(UIApplication*)_application didFinishLaunchingWithOptions:(NSDictionary*)_launchOptions;

- (BOOL)application:(UIApplication *)application openURL:(NSURL *)url options:(NSDictionary<UIApplicationOpenURLOptionsKey,id> *)options;

@end
