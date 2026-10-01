using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using GamePotUnity;
using UnityEngine.Android;


namespace GamePotDemo
{
    public class Login : MonoBehaviour, IGamePot
    {
        [SerializeField]
        private GameObject loginButtonContainer;

        [SerializeField]
        public GameObject popupRoot;

        int apiLevel;

        private void Awake()
        {
            GamePot.initPlugin();
            GamePot.setListener(this);
        }

        private void Start()
        {

#if UNITY_ANDROID
        string androidInfo = SystemInfo.operatingSystem;
		Debug.Log("androidInfo: " + androidInfo);
		apiLevel = int.Parse(androidInfo.Substring(androidInfo.IndexOf("-") + 1, 2));
		Debug.Log("apiLevel: " + apiLevel);

		if (apiLevel >= 33 &&
			!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
		{
			Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
		}
#endif

            ActiveLoginButton(true);
#if UNITY_ANDROID || UNITY_IOS

            //로그인 시, 자동으로 이용약관 UI 생성
            GamePot.setAutoAgree(true);

            NAgreeInfo _auto_agree_info = new NAgreeInfo();
            // _auto_agree_info.theme = "MATERIAL_GREEN";
            _auto_agree_info.ageCertificationShow = true;
            // _auto_agree_info.ageOverMessage = "14세 이상";
            // _auto_agree_info.ageUnderMessage = "14세 미만";
            // _auto_agree_info.ageDescription = "14세 이하는 인증을 받아야 합니다.";

            GamePot.setAutoAgreeBuilder(_auto_agree_info);

            NCommon.LoginType type = GamePot.getLastLoginType();
            if (type != NCommon.LoginType.NONE && type != NCommon.LoginType.THIRDPARTYSDK)
            {
                GamePot.login(type);
            }
#endif
        }

        private void SwitchLoginButton(GameObject loginButton, bool flag)
        {
            float opacity = flag ? 1.0f : 0.3f;
            var image = loginButton.transform.Find("Image").GetComponent<Image>();
            if (image != null)
            {
                var tmp_color = image.color;
                tmp_color.a = opacity;
                image.color = tmp_color;
            }

            var text = loginButton.transform.Find("Text").GetComponent<Text>();
            if (text != null)
            {
                var tmp_color = text.color;
                tmp_color.a = opacity;
                text.color = tmp_color;
            }

            var line = loginButton.transform.Find("Line").GetComponent<Image>();
            if (line != null)
            {
                var tmp_color = line.color;
                tmp_color.a = opacity;
                line.color = tmp_color;
            }

            var button = loginButton.GetComponent<Button>();
            if (button != null)
                button.interactable = flag;
        }

        #region UIButton.Click
        public void ClickAppleIDLoginButton()
        {
            GamePot.login(NCommon.LoginType.APPLE);
        }

        public void ClickGameCenterLoginButton()
        {
            GamePot.login(NCommon.LoginType.GAMECENTER);
        }

        public void ClickGoogleLoginButton()
        {
            GamePot.login(NCommon.LoginType.GOOGLE);
        }

        public void ClickFacebookLoginButton()
        {
            GamePot.login(NCommon.LoginType.FACEBOOK);
        }

        public void ClickNaverLoginButton()
        {
            GamePot.login(NCommon.LoginType.NAVER);
        }

        public void ClickLineLoginButton()
        {
            GamePot.login(NCommon.LoginType.LINE);
        }

        public void ClickTwitterLoginButton()
        {
            GamePot.login(NCommon.LoginType.TWITTER);
        }

        public void ClickGuestLoginButton()
        {
            // GamePot.loginByThirdPartySDK("thirdpartysdk-lgh3");
            GamePot.login(NCommon.LoginType.GUEST);
        }

        public void ClickLoginWithUIButton()
        {
            NLoginUIInfo info = new NLoginUIInfo();
            info.showLogo = true;
            info.loginTypes = new NCommon.LoginType[]{
                NCommon.LoginType.APPLE,
                NCommon.LoginType.GOOGLE,
                NCommon.LoginType.FACEBOOK,
                NCommon.LoginType.NAVER,
                NCommon.LoginType.LINE,
                NCommon.LoginType.THIRDPARTYSDK
            };

            GamePot.showLoginWithUI(info);
        }

        public void Click3rdPartyLoginButton(InputField input)
        {
            GamePot.loginByThirdPartySDK(input.text);
        }


        public void ClickAgreeButton()
        {
            NAgreeInfo info = new NAgreeInfo();
            //info.theme = "green";
            //info.showPush = true;
            //info.showNightPush = true;
            //info.pushMessage = "일반 푸시 메시지";
            //info.pushDetailURL = "";
            //info.nightPushDetailURL = "";


            // info.theme = "green";
            // info.headerBackGradient = new string[] { "0xFF1F1F1F", "0xFF1F1F1F" };
            // info.headerTitleColor = "0xFFFFFFFF";
            // info.headerBottomColor = "0xFF909090";
            // // info.headerTitle = "test";
            // // info.headerIconDrawable = "ic_stat_gamepot_small";

            // info.contentBackGradient = new string[] { "0xFFE4E4E4", "0xFFE4E4E4" };
            // //info.contentIconColor = "0xFF0429ff";
            // info.contentCheckColor = "0xFF8C8C8C";
            // info.contentTitleColor = "0xFF313131";
            // info.contentShowColor = "0xFF9B9B9B";
            // info.contentBottomColor = "0xFF909090";
            // //info.contentIconDrawable = "ic_stat_gamepot_agree";

            // info.footerBackGradient = new string[] { "0xFFE4E4E4", "0xFFE4E4E4" };
            // info.footerButtonGradient = new string[] { "0xFF986E5F", "0xFF986E5F" };
            // info.footerButtonOutlineColor = "0x00000000";
            // info.footerTitleColor = "0xFFFFFFFF";
            // info.showToastPushStatus = true;
            //info.setEUCountry(true);
            info.ageCertificationShow = true;


            GamePot.showAgreeDialog(info, (success, agreeInfo, error) => {

                string _result = "";
                _result += "agreePush : " + agreeInfo.agreePush + "\n";
                _result += "agreeNightPush : " + agreeInfo.agreeNight + "\n";
                _result += "agreeGDPR : " + agreeInfo.agreeGDPR + "\n";
                _result += "agreeGDPRStatus : " + agreeInfo.agreeGDPRStatus + "\n";
                _result += "emailVerified : " + agreeInfo.emailVerified + "\n";

                PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "showAgreeDialog", "result : " + _result);
            });
        }


        public void ClickCheckAppStatus()
        {
            GamePot.checkAppStatus((NCommon.ResultCheckAppStatus resultState,
                NAppStatus appStatus, NError error) =>
            {
                string result = resultState.ToString() + "\n";

                if (resultState == NCommon.ResultCheckAppStatus.MAINTENANCE
                || resultState == NCommon.ResultCheckAppStatus.NEED_UPDATE)
                {
                    result += appStatus.ToJson() + "\n";
                }

                if (resultState == NCommon.ResultCheckAppStatus.FAILED)
                {
                    result += appStatus.ToJson() + "\n";
                }

                PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "checkAppStatus", "result : " + result);
            });
        }


        public void ClickAgreeEmail()
        {
            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_AgreeEmail", "Email 인증", null);
        }

        public void ClickSetAgreeInfo()
        {
            NAgreeResultInfo info = new NAgreeResultInfo();
            info.agree = true;
            info.agreeNight = true;
            info.agreePush = true;
            info.agreeGDPRStatus = 4;


            GamePot.setAgreeInfo(info, (success, error) => {

                string result = "result : ";

                if (success)
                {
                    result += "success";
                }

                if (error != null)
                {
                    result += error.ToJson();
                }

                PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "onCheckAgreeEmailResult", result);
            });
        }

        #endregion

        private void ActiveLoginButton(bool isActive)
        {
            loginButtonContainer.SetActive(isActive);
        }

        // GamePot Interface
        public void onAppClose()
        {
            Application.Quit();
        }

        public void onNeedUpdate(NAppStatus status)
        {
            GamePot.showAppStatusPopup(status.ToJson());
        }

        public void onMainternance(NAppStatus status)
        {
            string link_url = "";
            if (!ReferenceEquals(status.url, null))
                link_url = status.url;

            if (string.IsNullOrEmpty(link_url))
            {
                CustomizedPopup.PopupButtonInfo[] btn_info = new CustomizedPopup.PopupButtonInfo[1];

                btn_info[0].btnText = "종료";
                btn_info[0].callback = () =>
                {
                    Application.Quit();
                };

                System.DateTime time = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                string startDate = System.Convert.ToString(time.AddSeconds(status.startedAt));
                string endDate = System.Convert.ToString(time.AddSeconds(status.endedAt));
                PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "점검 중", status.message + "\n\n [ " + startDate + "] ~ [ " + endDate + " ]", btn_info);
            }
            else
            {
                CustomizedPopup.PopupButtonInfo[] btn_info = new CustomizedPopup.PopupButtonInfo[2];

                btn_info[0].btnText = "종료";
                btn_info[0].callback = () =>
                {
                    Application.Quit();
                };

                btn_info[1].btnText = "자세히보기";
                btn_info[1].callback = () =>
                {
                    GamePot.showWebView(link_url);
                };

                System.DateTime time = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                string startDate = System.Convert.ToString(time.AddSeconds(status.startedAt));
                string endDate = System.Convert.ToString(time.AddSeconds(status.endedAt));
                PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnTwo", "점검 중", status.message + "\n\n [ " + startDate + "] ~ [ " + endDate + " ]", btn_info);
            }
            //Native App Status Popup
            //GamePot.showAppStatusPopup(status.ToJson());
        }

        public void onLoginCancel()
        {
            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "로그인", "로그인이 취소 되었습니다.");
        }

        public void onLoginExit()
        {
            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "로그인", "ShowLoginWithUI 취소");
        }

        public void onLoginSuccess(NUserInfo userInfo)
        {
            GamePotSettings.MemberInfo = userInfo;
            SceneManager.LoadSceneAsync("Main");
        }

        public void onLoginFailure(NError error)
        {
            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "로그인", error.message);
        }

        public void onDeleteMemberSuccess()
        {
        }

        public void onDeleteMemberFailure(NError error)
        {
        }

        public void onLogoutSuccess()
        {
        }

        public void onLogoutFailure(NError error)
        {
        }

        public void onCouponSuccess()
        {
        }

        public void onCouponFailure(NError error)
        {
        }

        public void onPurchaseSuccess(NPurchaseInfo purchaseInfo)
        {
        }

        public void onPurchaseFailure(NError error)
        {
        }

        public void onPurchaseCancel()
        {
        }

        public void onCreateLinkingSuccess(NUserInfo userInfo)
        {
        }

        public void onCreateLinkingFailure(NError error)
        {
        }

        public void onCreateLinkingCancel()
        {
        }

        public void onDeleteLinkingSuccess()
        {
        }

        public void onDeleteLinkingFailure(NError error)
        {
        }

        public void onPushSuccess()
        {
        }

        public void onPushFailure(NError error)
        {
        }

        public void onPushNightSuccess()
        {
        }

        public void onPushNightFailure(NError error)
        {
        }

        public void onPushAdSuccess()
        {
        }

        public void onPushAdFailure(NError error)
        {
        }

        public void onPushStatusSuccess()
        {
        }

        public void onPushStatusFailure(NError error)
        {
        }

        public void onAgreeDialogSuccess(NAgreeResultInfo info)
        {
            Debug.Log("onAgreeDialogSuccess : " + info.emailVerified);
        }

        public void onAgreeDialogFailure(NError error)
        {
            Debug.Log("onAgreeDialogFailure - " + error);
        }

        public void onReceiveScheme(string scheme)
        {
        }

        public void onLoadAchievementSuccess(List<NAchievementInfo> info)
        {
        }

        public void onLoadAchievementFailure(NError error)
        {
        }

        public void onLoadAchievementCancel()
        {
        }

        public void onWebviewClose(string result)
        {
            Debug.Log("gamepot webview return : " + result);
        }

        public void onPurchaseDetailListSuccess(NPurchaseItem[] purchaseInfoList)
        {
        }

        public void onPurchaseDetailListFailure(NError error)
        {
        }

        public void onRequestTrackingAuthorization(NResultTrackingAuthorization resultState)
        {
        }

        public void onCheckAppStatusSuccess()
        {
        }

        public void onCheckAppStatusFailure(NError error)
        {
        }

        public void onSendAgreeEmailSuccess(NAgreeSendEmailInfo resultInfo)
        {
            string result = "key : " + resultInfo.key + "\n";
            result += "expiredAt : " + resultInfo.expiredAt + "\n";
            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "onSendAgreeEmailSuccess", result);

        }

        public void onSendAgreeEmailFailure(NError error)
        {
            string result = "error : " + error.ToJson();
            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "onSendAgreeEmailFailure", result);

        }

        public void onCheckAgreeEmailSuccess(NAgreeCheckEmailInfo resultInfo)
        {
            string result = "status : " + resultInfo.status + "\n";
            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "onCheckAgreeEmailSuccess", result);

        }

        public void onCheckAgreeEmailFailure(NError error)
        {
            string result = "error : " + error.ToJson();
            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "onCheckAgreeEmailFailure", result);
        }

        public void onSetAgreeInfoSuccess()
        {
            //PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "onSetAgreeInfoSuccess", "success");
        }

        public void onSetAgreeInfoFailure(NError error)
        {
            //string result = "error : " + error.ToJson();
            //PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "onCheckAgreeEmailFailure", result);
        }

        public void onSetUserDataSuccess()
        {
            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "SetUserData", "success");
        }

        public void onSetUserDataFailure(NError error)
        {
            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "SetUserData", error.ToJson());
        }

        public void onGameServiceConflict(NUserInfo userInfo)
        {
            
        }
    }
}