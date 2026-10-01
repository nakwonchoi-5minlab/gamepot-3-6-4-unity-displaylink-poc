using UnityEngine;
using System.Collections;

public class NCommon
{
	public enum LoginType
	{
		NONE,
		GOOGLE,
		GOOGLEPLAY,
		FACEBOOK,
		NAVER,
		GAMECENTER,
		TWITTER,
		LINE,
		APPLE,
		GUEST,
		EMAIL,
        OAUTH,
        THIRDPARTYSDK,
        STANDALONE,
        HUAWEI
	}

	public enum ChannelType
	{
		GOOGLEPLAY,
		GOOGLE,
		FACEBOOK,
		NAVER,
		GAMECENTER,
		TWITTER,
		LINE,
		OAUTH,
		APPLE,
		THIRDPARTYSDK,
		HUAWEI
	}

	public enum LinkingType
	{
		GOOGLEPLAY,
		GAMECENTER,
		GOOGLE,
		FACEBOOK,
		NAVER,
		TWITTER,
		LINE,
		APPLE,
        OAUTH,
        THIRDPARTYSDK,
        HUAWEI
	}

	public enum GameOrientation
	{
		portrait = 1,
		landscape = 2,
	}

	public enum GameLanguage
	{
		KOREAN = 0,
		ENGLISH = 1,
		CHINESE_CN = 2,
		CHINESE_TW = 3,
		GERMAN = 4,
		JAPANESE = 5
	}

	public enum ResultTrackingAuthorization
	{
		ATTrackingManagerAuthorizationStatusNotDetermined,
		ATTrackingManagerAuthorizationStatusRestricted,
		ATTrackingManagerAuthorizationStatusDenied,
		ATTrackingManagerAuthorizationStatusAuthorized,
		ATTrackingManagerAuthorizationStatusUnknown
	}

	// -------- RESPONSE RESULT STATE --------
	public enum ResultLogin
	{
		SUCCESS,
		CANCELLED,
		FAILED,
		NEED_UPDATE,
		MAINTENANCE,
		GAMESERVICE_CONFLICT,
		APP_CLOSE,
        EXIT
	}

	public enum ResultPurchase
	{
		SUCCESS,
		CANCELLED,
		FAILED
	}

	public enum ResultLinking
	{
		SUCCESS,
		CANCELLED,
		FAILED
	}

	public enum ResultCheckAppStatus
	{
		SUCCESS,
		FAILED,
		NEED_UPDATE,
		MAINTENANCE
	}

}

