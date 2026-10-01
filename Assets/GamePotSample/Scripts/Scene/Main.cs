using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using GamePotUnity;


namespace GamePotDemo
{
    public class Main : MonoBehaviour, IGamePot
    {
        [SerializeField]
        private GameObject settingUI;

        [SerializeField]
        private Text googleLinkText;

        [SerializeField]
        private Text facebookLinkText;

        //[SerializeField]
        //private Text appleLinkText;

        [SerializeField]
        private Text memberId;

        [SerializeField]
        private GameObject popupRoot;

        [SerializeField]
        private GameObject pushToggle;

        [SerializeField]
        private GameObject nightPushToggle;

        [SerializeField]
        private GameObject loadingPannel;

        private Color active = new Color(14f / 255f, 247f / 255f, 38f / 255f);
        private Color dimmed = new Color(182f / 255f, 178f / 255f, 166f / 255f);

        private void Start()
        {
            GamePot.initPlugin();
            GamePot.setListener(this);
            linkingStatusUpdate();

            settingUI.SetActive(false);

            string member_id = GamePot.getMemberId();
            if (!string.IsNullOrEmpty(member_id))
            {
                memberId.text = member_id;
            }

            ////GDPR Test Code(2020.9.23)
            //string[] gdpr_list = GamePot.getGDPRCheckedList();
            //int idx = 0;
            //foreach (string elem in gdpr_list)
            //{
            //    Debug.LogFormat("{0}. [ {1} ]", ++idx, elem);
            //}

        }

        private void linkingStatusUpdate()
        {
            List<NLinkingInfo> linkedList = GamePot.getLinkedList();

            Color connected_color = new Color(0.13f, 0.13f, 0.13f);
            Color disconnected_color = new Color(0.922f, 0.431f, 0.573f);

            googleLinkText.text = "연결하기";
            googleLinkText.color = disconnected_color;

            facebookLinkText.text = "연결하기";
            facebookLinkText.color = disconnected_color;

            //appleLinkText.text = "연결하기";
            //appleLinkText.color = disconnected_color;

            if (linkedList != null)
            {
                foreach (NLinkingInfo item in linkedList)
                {
                    if (item.provider == NCommon.LinkingType.GOOGLE)
                    {
                        googleLinkText.text = "해제하기";
                        googleLinkText.color = connected_color;
                    }
                    else if (item.provider == NCommon.LinkingType.FACEBOOK)
                    {
                        facebookLinkText.text = "해제하기";
                        facebookLinkText.color = connected_color;
                    }
                    //else if (item.provider == NCommon.LinkingType.APPLE)
                    //{
                    //    appleLinkText.text = "해제하기";
                    //    appleLinkText.color = connected_color;
                    //}
                }
            }

        }

        private bool getLinkingStatus(NCommon.LinkingType linkingType)
        {
            List<NLinkingInfo> linkedList = GamePot.getLinkedList();

            if (linkedList != null)
            {
                foreach (NLinkingInfo item in linkedList)
                {
                    if (linkingType == item.provider)
                    {
                        return true;
                    }
                }
            }
            return false;
        }



        #region UIButton.onClick
        public void ClickSettingsButton()
        {
            settingUI.SetActive(true);

            try
            {
                SliderToggle push = pushToggle.GetComponent<SliderToggle>();
                SliderToggle night_push = nightPushToggle.GetComponent<SliderToggle>();

                push.SetToggle(GamePot.getPushStatus().enable);
                night_push.SetToggle(GamePot.getPushStatus().night);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }

        }

        public void ClickSettingExitButton()
        {
            settingUI.SetActive(false);
        }

        public void ClickNoticeButton()
        {
            GamePot.showNotice(true);
        }

        public void ClickCouponButton()
        {
            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_Coupon", "쿠폰", "쿠폰번호");
        }

        public void ClickCharacterInfo()
        {
            GamePotSendLogCharacter c_info = new GamePotSendLogCharacter();
            c_info.put("name", "User");
            c_info.put("player_id", "player id");
            c_info.put("server_id", "server id");
            c_info.put("level", "123");
            c_info.put("userdata", "user data");

           bool result = GamePot.characterInfo(c_info);

            //For STANDALONE Platform

            // bool result = GamePot.characterInfo(c_info,
            //     (bool success, NError err) =>
            //     {
            //         string msg = success ? "로그전송 성공 : " + c_info.toString() : "로그전송 실패 : " + err.ToJson();
            //         PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "Send CharacterInfo", msg);
            //     });

        }

        public void ClickInAppButton()
        {
            CustomizedPopup.PopupButtonInfo[] btn_info = new CustomizedPopup.PopupButtonInfo[3];

#if UNITY_IOS
            btn_info[0].callback = () =>
            {
                GamePot.purchase("gamepot001");
            };

            btn_info[1].callback = () =>
            {
                GamePot.purchase("gamepot002");
            };
#else
            btn_info[0].callback = () =>
           {
               GamePot.purchase("purchase_001");
           };

            btn_info[1].callback = () =>
            {
                GamePot.purchase("purchase_002");
            };
#endif


            btn_info[2].callback = () =>
            {
                GamePot.purchase("purchase_003");
            };

            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_Purchase", "결제", "결제할 아이템을 선택해주세요", btn_info);
        }


        public void ClickShowCustomWebView(InputField input)
        {
            // GamePot.showCSWebView();
            GamePot.showWebView(input.text);
        }


        public void ClickInAppButton(int idx)
        {
            if (0 <= idx && idx < 3)
            {
                string product_name = "purchase_00" + idx;
#if !UNITY_EDITOR && UNITY_IOS
        GamePot.purchase(product_name);
#elif !UNITY_EDITOR && UNITY_ANDROID
        GamePot.purchase(product_name);
#elif UNITY_EDITOR
#endif
            }
        }

        public void ClickCSButton()
        {
            GamePot.showCSWebView();
        }

        public void ClickLinkingGoogleButton()
        {
            if (getLinkingStatus(NCommon.LinkingType.GOOGLE) == true)
            {
                GamePot.deleteLinking(NCommon.LinkingType.GOOGLE);
            }
            else
            {
                GamePot.createLinking(NCommon.LinkingType.GOOGLE);
            }
        }

        public void ClickLinkingFacebookButton()
        {
            if (getLinkingStatus(NCommon.LinkingType.FACEBOOK) == true)
            {
                GamePot.deleteLinking(NCommon.LinkingType.FACEBOOK);
            }
            else
            {
                GamePot.createLinking(NCommon.LinkingType.FACEBOOK);
            }
        }

        public void ClickLinkingAppleButton()
        {
            if (getLinkingStatus(NCommon.LinkingType.APPLE) == true)
            {
                GamePot.deleteLinking(NCommon.LinkingType.APPLE);
            }
            else
            {
                GamePot.createLinking(NCommon.LinkingType.APPLE);
            }
        }

        public void ClickPushButton()
        {
            SliderToggle push = pushToggle.GetComponent<SliderToggle>();
            push.callback = (bool status) =>
            {
                GamePot.setPushStatus(status, (bool success, NError err) =>
                {
                    if (!success)
                        push.SetToggle(!status);
                });
            };
            push.ChangeToggle();
        }


        public void ClickNightPushButton()
        {
            SliderToggle night_push = nightPushToggle.GetComponent<SliderToggle>();
            night_push.callback = (bool status) =>
            {
                GamePot.setPushNightStatus(status, (bool success, NError err) =>
                {
                    if (!success)
                        night_push.SetToggle(!status);
                });
            };
            night_push.ChangeToggle();
        }


        public void ClickLogoutButton()
        {
            GamePot.logout();
        }

        public void ClickWithDrawnButton()
        {
            GamePot.deleteMember();
        }

        public void ClickLocalPush()
        {
            CustomizedPopup.PopupButtonInfo[] btn_info = new CustomizedPopup.PopupButtonInfo[2];

            btn_info[1].callback = () =>
            {
                DateTime current = DateTime.Now;
                current = current.AddSeconds(10);
                GamePot.sendLocalPush(current, "title", "content");
            };
            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnTwo", "로컬 푸시", "확인 버튼을 누르면 10초 후 Push 메세지가 전송됩니다.", btn_info);
        }

        public void ClickTerms()
        {
            GamePot.showTerms();
        }

        public void ClickGetPurchaseDetail()
        {
            GamePot.getPurchaseDetailListAsync((bool success, NPurchaseItem[] purchaseInfoList, NError error) =>
            {
                string result = "data is empty!";
                if (success)
                {
                    if (purchaseInfoList != null)
                        result = purchaseInfoList[0].productId;
                    if (purchaseInfoList.Length > 1)
                    {
                        for (int i = 1; i < purchaseInfoList.Length; i++)
                            result += "\n" + purchaseInfoList[i].productId;
                    }
                }
                else
                {
                    result = error.ToJson();
                }


                PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "getPurchaseDetailListAsync", result);
            });
        }

        public void ClickPrivacy()
        {
            GamePot.showPrivacy();
        }

        public void ClickShowAchivement()
        {
            GamePot.showAchievement();
        }

        public void ClickShowLeaderboard()
        {
            GamePot.showLeaderboard();
        }


        public void ClickLoadAchievements()
        {
            GamePot.loadAchievement();
        }


        public void ClickSetAgreeInfo()
        {
            NAgreeResultInfo info = new NAgreeResultInfo();
            info.agree = true;
            info.agreeNight = true;
            info.agreePush = true;
            info.agreeGDPRStatus = 4;
            info.agreeGDPR = "tt";


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

        // GamePot Interface
        public void onAppClose()
        {
            Application.Quit();
        }

        public void onNeedUpdate(NAppStatus status)
        {
        }

        public void onMainternance(NAppStatus status)
        {
        }

        public void onLoginCancel()
        {
        }

        public void onLoginExit()
        {
        }

        public void onLoginSuccess(NUserInfo userInfo)
        {
        }

        public void onLoginFailure(NError error)
        {
        }

        public void onDeleteMemberSuccess()
        {
            CustomizedPopup.PopupButtonInfo[] btn_info = new CustomizedPopup.PopupButtonInfo[1];
            btn_info[0].callback = () => { SceneManager.LoadSceneAsync("login"); };
            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "회원탈퇴", "회원탈퇴 되었습니다.\n로그인 화면으로 이동 합니다.", btn_info);
        }

        public void onDeleteMemberFailure(NError error)
        {
            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "회원탈퇴", error.message);
        }

        public void onLogoutSuccess()
        {
            CustomizedPopup.PopupButtonInfo[] btn_info = new CustomizedPopup.PopupButtonInfo[1];
            btn_info[0].callback = () => { SceneManager.LoadSceneAsync("login"); };
            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "로그아웃", "로그아웃 되었습니다.\n로그인 화면으로 이동 합니다.", btn_info);
        }

        public void onLogoutFailure(NError error)
        {
            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "로그아웃", error.message);
        }

        public void onCouponSuccess()
        {
            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "쿠폰", "쿠폰 아이템이 지급되었습니다.");
        }

        public void onCouponFailure(NError error)
        {
            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "쿠폰", error.message);
        }

        public void onPurchaseSuccess(NPurchaseInfo purchaseInfo)
        {
            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "결제", "결제 성공하였습니다.");
        }

        public void onPurchaseFailure(NError error)
        {
            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "결제", error.message);
        }

        public void onPurchaseCancel()
        {
            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "결제", "결제를 취소 하였습니다.");
        }

        public void onCreateLinkingSuccess(NUserInfo userInfo)
        {
            linkingStatusUpdate();
        }

        public void onCreateLinkingFailure(NError error)
        {
            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "연동", error.message);
            linkingStatusUpdate();
        }

        public void onCreateLinkingCancel()
        {
            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "연동", "연동이 취소 되었습니다.");
            linkingStatusUpdate();
        }

        public void onDeleteLinkingSuccess()
        {
            linkingStatusUpdate();
        }

        public void onDeleteLinkingFailure(NError error)
        {
            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "연동", error.message);
            linkingStatusUpdate();
        }

        public void onPushSuccess()
        {
        }

        public void onPushFailure(NError error)
        {
            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "Push", error.message);
        }

        public void onPushNightSuccess()
        {

        }

        public void onPushNightFailure(NError error)
        {
            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "Push", error.message);
        }

        public void onPushAdSuccess()
        {
        }

        public void onPushAdFailure(NError error)
        {
            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "Push AD", error.message);
        }

        public void onPushStatusSuccess()
        {
        }

        public void onPushStatusFailure(NError error)
        {
            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "Push Status", error.message);
        }

        public void onAgreeDialogSuccess(NAgreeResultInfo info)
        {
        }

        public void onAgreeDialogFailure(NError error)
        {
        }

        public void onReceiveScheme(string scheme)
        {
            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "Scheme", scheme);
        }

        public void onLoadAchievementSuccess(List<NAchievementInfo> info)
        {
            string msg = "";

            for (int i = 0; i < info.Count; i++)
            {
                msg += info[i].id + ", " + info[i].unlocked + "\n";
            }
            PopupManager.ShowCustomPopup(popupRoot, "GamePotSamplePopup_BtnOne", "Achievement", msg);
        }

        public void onLoadAchievementFailure(NError error)
        {
        }

        public void onLoadAchievementCancel()
        {
        }

        public void onWebviewClose(string result)
        {
        }

        public void onPurchaseDetailListSuccess(NPurchaseItem[] purchaseInfoList)
        {
            string result = "";
            foreach (NPurchaseItem elem in purchaseInfoList)
            {
                result += "productId : " + elem.productId + "\n";
            }
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
        }

        public void onSendAgreeEmailFailure(NError error)
        {
        }

        public void onCheckAgreeEmailSuccess(NAgreeCheckEmailInfo resultInfo)
        {
        }

        public void onCheckAgreeEmailFailure(NError error)
        {
        }

        public void onSetAgreeInfoSuccess()
        {
        }

        public void onSetAgreeInfoFailure(NError error)
        {
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
