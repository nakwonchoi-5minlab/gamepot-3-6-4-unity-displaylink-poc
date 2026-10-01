using UnityEngine;
using System.Collections;
using Realtime.LITJson;

public class NAgreeInfo
{
    public NAgreeInfo()
    {
        theme = "MATERIAL_BLUE";
        headerBackGradient = new string[] { };
        headerBottomColor = "";
        headerIconDrawable = "";
        headerTitle = "{none}";
        headerTitleColor = "";

        contentBackGradient = new string[] { };
        contentIconDrawable = "";
        contentIconColor = "";
        contentCheckColor = "";
        contentTitleColor = "";
        contentShowColor = "";
        contentBottomColor = "";


        footerBackGradient = new string[] { };
        footerButtonGradient = new string[] { };
        footerButtonOutlineColor = "";
        footerTitle = "";
        footerTitleColor = "";

        footerOtherButtonGradient = new string[] { };
        footerOtherTitleColor = "";
        footerOtherButtonOutlineColor = "";

        ageTitle = "";
        ageDescription = "";
        ageOverMessage = "";
        ageUnderMessage = "";
        emailConfirmIconDrawable = "";
        emailCancelIconDrawable = "";
        emailBack = "";
        emailTitle = "";
        emailSend = "";
        emailConfirm = "";
        emailDescription = "";
        emailSubDescription = "";
        emailPlaceHolder = "";
        emailValidateFormatError = "";
        emailSendSuccess = "";
        emailSendFail = "";
        emailValidateFail = "";
        emailAuthFail = "";


        showToastPushStatus = false;
        pushToastMsg = "";
        nightPushToastMsg = "";

        checkIconColor = "";
        backIconColor = "";

        showPush = true;
        showNightPush = false;

        allMessage = "";
        termMessage = "";
        privacyMessage = "";
        pushMessage = "";
        nightPushMessage = "";

        termDetailUrl = "";
        privacyDetailUrl = "";
        GDPRDetailURL = "";
        pushDetailURL = "";
        nightPushDetailURL = "";

        adCustomDetailURL = "";
        adNoCustomDetailURL = "";
        currentCountry = "";
    }

    // 기본 테마
    public string theme { get; set; }

    // 타이틀
    // 배경 색상(gradient)
    public string[] headerBackGradient { get; set; }
    // 타이틀 영역 하단 라인 색상
    public string headerBottomColor { get; set; }
    // 아이콘 이미지 파일명(aos - drawable / ios - bundle)
    public string headerIconDrawable { get; set; }
    // 제목
    public string headerTitle { get; set; }
    // 제목 색상
    public string headerTitleColor { get; set; }

    // 컨텐츠
    // 배경 색상(gradient)
    public string[] contentBackGradient { get; set; }
    // 아이콘 이미지 파일명(aos - drawable / ios - bundle)
    public string contentIconDrawable { get; set; }
    // 아이콘 색상
    public string contentIconColor { get; set; }
    // 체크버튼 색상
    public string contentCheckColor { get; set; }
    // 체크내용 색상
    public string contentTitleColor { get; set; }
    // 보기문구 색상
    public string contentShowColor { get; set; }
    // 보기 영역 하단 라인 색상
    public string contentBottomColor { get; set; }


    // 하단(게임시작)
    // 배경 색상(gradient)
    public string[] footerBackGradient { get; set; }
    // 게임시작 버튼 배경 색상(gradient)
    public string[] footerButtonGradient { get; set; }
    // 게임시작 버튼 외곽선 색상
    public string footerButtonOutlineColor { get; set; }
    // 게임시작 문구
    public string footerTitle { get; set; }
    // 게임시작 문구 색상
    public string footerTitleColor { get; set; }

    // (gradient)
    public string[] footerOtherButtonGradient { get; set; }
    public string footerOtherTitleColor { get; set; }
    public string footerOtherButtonOutlineColor { get; set; }
    public string ageTitle { get; set; }
    public string ageDescription { get; set; }
    public string ageOverMessage { get; set; }
    public string ageUnderMessage { get; set; }
    public string emailConfirmIconDrawable { get; set; }
    public string emailCancelIconDrawable { get; set; }
    public string emailBack { get; set; }
    public string emailTitle { get; set; }
    public string emailSend { get; set; }
    public string emailConfirm { get; set; }
    public string emailDescription { get; set; }
    public string emailSubDescription { get; set; }
    public string emailPlaceHolder { get; set; }
    public string emailValidateFormatError { get; set; }
    public string emailSendSuccess { get; set; }
    public string emailSendFail { get; set; }
    public string emailValidateFail { get; set; }
    public string emailAuthFail { get; set; }

    public bool showToastPushStatus { get; set; }
    public string pushToastMsg { get; set; }
    public string nightPushToastMsg { get; set; }



    // 이메일 보호자 동의 승인체크 색상
    public string checkIconColor { get; set; }
    // 이메일 보호자 동의 취소 색깔
    public string backIconColor { get; set; }


    //일반푸시 노출 여부
    public bool showPush { get; set; }

    // 야간푸시 노출 여부
    public bool showNightPush { get; set; }

    // '모두 동의' 문구 변경 시
    public string allMessage { get; set; }

    // '이용 약관' 문구 변경 시
    public string termMessage { get; set; }

    // '개인정보 취급방침' 문구 변경 시
    public string privacyMessage { get; set; }

    // '일반 푸시' 문구 변경 시
    public string pushMessage { get; set; }

    // '야간 푸시' 문구 변경 시
    public string nightPushMessage { get; set; }

    public string pushDetailURL { get; set; }

    public string nightPushDetailURL { get; set; }

    public string termDetailUrl { get; set; }
    public string privacyDetailUrl { get; set; }

    public string GDPRDetailURL { get; set; }
    public string adCustomDetailURL { get; set; }
    public string adNoCustomDetailURL { get; set; }

    public string currentCountry { get; set; }
    private int _isEUCountry = -1;

    // '연령 제한' 팝업 노출 여부 
    public bool ageCertificationShow { get; set; }

    public void setEUCountry(bool flag)
    {
        _isEUCountry = flag ? 1 : 0;
    }

    public bool isEUCountry()
    {
        return _isEUCountry == 1;
    }



    public string ToJson()
    {
        JsonData data = new JsonData();

        data["theme"] = theme;
        data["headerBackGradient"] = string.Join(",", headerBackGradient);
        data["headerBottomColor"] = headerBottomColor;
        data["headerIconDrawable"] = headerIconDrawable;
        data["headerTitle"] = headerTitle;
        data["headerTitleColor"] = headerTitleColor;

        data["contentBackGradient"] = string.Join(",", contentBackGradient);
        data["contentIconDrawable"] = contentIconDrawable;
        data["contentIconColor"] = contentIconColor;
        data["contentCheckColor"] = contentCheckColor;
        data["contentTitleColor"] = contentTitleColor;
        data["contentShowColor"] = contentShowColor;
        data["contentBottomColor"] = contentBottomColor;

        data["footerBackGradient"] = string.Join(",", footerBackGradient);
        data["footerButtonGradient"] = string.Join(",", footerButtonGradient);
        data["footerButtonOutlineColor"] = footerButtonOutlineColor;
        data["footerTitle"] = footerTitle;
        data["footerTitleColor"] = footerTitleColor;

        data["footerOtherButtonGradient"] = string.Join(",", footerOtherButtonGradient);
        data["footerOtherTitleColor"] = footerOtherTitleColor;
        data["footerOtherButtonOutlineColor"] = footerOtherButtonOutlineColor;

        data["ageTitle"] = ageTitle;
        data["ageDescription"] = ageDescription;
        data["ageOverMessage"] = ageOverMessage;
        data["ageUnderMessage"] = ageUnderMessage;

        data["emailConfirmIconDrawable"] = emailConfirmIconDrawable;
        data["emailCancelIconDrawable"] = emailCancelIconDrawable;
        data["emailBack"] = emailBack;
        data["emailTitle"] = emailTitle;
        data["emailSend"] = emailSend;
        data["emailConfirm"] = emailConfirm;
        data["emailDescription"] = emailDescription;
        data["emailSubDescription"] = emailSubDescription;
        data["emailPlaceHolder"] = emailPlaceHolder;
        data["emailValidateFormatError"] = emailValidateFormatError;
        data["emailSendSuccess"] = emailSendSuccess;
        data["emailSendFail"] = emailSendFail;
        data["emailValidateFail"] = emailValidateFail;
        data["emailAuthFail"] = emailAuthFail;

        data["showToastPushStatus"] = showToastPushStatus ? "true" : "false";
        data["pushToastMsg"] = pushToastMsg;
        data["nightPushToastMsg"] = nightPushToastMsg;

        data["checkIconColor"] = checkIconColor;
        data["backIconColor"] = backIconColor;

        data["showPush"] = showPush ? "true" : "false";
        data["showNightPush"] = showNightPush ? "true" : "false";

        data["allMessage"] = allMessage;
        data["termMessage"] = termMessage;
        data["privacyMessage"] = privacyMessage;

        data["pushMessage"] = pushMessage;
        data["nightPushMessage"] = nightPushMessage;

        data["pushDetailURL"] = pushDetailURL;
        data["nightPushDetailURL"] = nightPushDetailURL;

        data["termDetailUrl"] = termDetailUrl;
        data["privacyDetailUrl"] = privacyDetailUrl;
        data["GDPRDetailURL"] = GDPRDetailURL;
        data["adCustomDetailURL"] = adCustomDetailURL;
        data["adNoCustomDetailURL"] = adNoCustomDetailURL;

        data["currentCountry"] = currentCountry;

        if (_isEUCountry != -1)
            data["isEUCountry"] = isEUCountry() ? "true" : "false";

        data["ageCertificationShow"] = ageCertificationShow ? "true" : "false";

        Debug.Log("NAgreeInfo::ToJson() - " + data.ToJson());
        return data.ToJson();
    }
}
