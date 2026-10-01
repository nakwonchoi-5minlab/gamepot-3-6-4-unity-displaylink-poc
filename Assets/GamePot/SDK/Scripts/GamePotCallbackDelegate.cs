public class GamePotCallbackDelegate
{
    public delegate void CB_Common(bool success, NError error = null);
    public delegate void CB_Login(NCommon.ResultLogin resultState, NUserInfo userInfo = null, NAppStatus appStatus = null, NError error = null);
    public delegate void CB_Purchase(NCommon.ResultPurchase resultState, NPurchaseInfo purchaseInfo = null, NError error = null);
    public delegate void CB_CreateLinking(NCommon.ResultLinking resultState, NUserInfo userInfo = null, NError error = null);
    public delegate void CB_ShowAgree(bool success, NAgreeResultInfo agreeInfo = null, NError error = null);
    public delegate void CB_ReceiveScheme(string scheme);
    public delegate void CB_ShowWebView(string jsonObject);
    public delegate void CB_PurchaseDetailList(bool success, NPurchaseItem[] purchaseInfoList = null, NError error = null);
    public delegate void CB_RequestTrackingAuthorization(NResultTrackingAuthorization resultState);
    public delegate void CB_CheckAppStatus(NCommon.ResultCheckAppStatus resultState, NAppStatus appStatus = null, NError error = null);

    public delegate void CB_SendAgreeEmail(bool success, NAgreeSendEmailInfo sendEmailInfo = null, NError error = null);
    public delegate void CB_CheckAgreeEmail(bool success, NAgreeCheckEmailInfo checkEmailInfo = null, NError error = null);
    public delegate void CB_SetAgreeInfo(bool success, NError error = null);
    public delegate void CB_SetUserData(bool success, NError error = null);
    public delegate void CB_StagePay(string result);
}