using System.Collections.Generic;
using UnityEngine;
using Realtime.LITJson;

public class GamePotEventListener : MonoBehaviour
{
    public static GamePotEventListener s_instance;

    // --- INTERFACE ---
    public IGamePot GamePotInterface = null;

    // --- DELEGATE ---
    public static GamePotCallbackDelegate.CB_Common cbAppClose = null;
    public static GamePotCallbackDelegate.CB_Login cbLogin = null;
    public static GamePotCallbackDelegate.CB_Common cbDeleteMember = null;
    public static GamePotCallbackDelegate.CB_Common cbLogout = null;
    public static GamePotCallbackDelegate.CB_Common cbCoupon = null;
    public static GamePotCallbackDelegate.CB_Purchase cbPurchase = null;
    public static GamePotCallbackDelegate.CB_CreateLinking cbCreateLinking = null;
    public static GamePotCallbackDelegate.CB_Common cbDeleteLinking = null;
    public static GamePotCallbackDelegate.CB_Common cbPushEnable = null;
    public static GamePotCallbackDelegate.CB_Common cbPushNightEnable = null;
    public static GamePotCallbackDelegate.CB_Common cbPushADEnable = null;
    public static GamePotCallbackDelegate.CB_Common cbPushStatusEnable = null;
    public static GamePotCallbackDelegate.CB_ShowAgree cbShowAgree = null;
    public static GamePotCallbackDelegate.CB_ReceiveScheme cbReceiveScheme = null;
    public static GamePotCallbackDelegate.CB_ShowWebView cbShowWebView = null;
    public static GamePotCallbackDelegate.CB_PurchaseDetailList cbPurchaseDetailList = null;
    public static GamePotCallbackDelegate.CB_RequestTrackingAuthorization cbRequestTrackingAuthorization = null;
    public static GamePotCallbackDelegate.CB_CheckAppStatus cbCheckAppStatus = null;

    public static GamePotCallbackDelegate.CB_SendAgreeEmail cbSendAgreeEmail = null;
    public static GamePotCallbackDelegate.CB_CheckAgreeEmail cbCheckAgreeEmail = null;
    public static GamePotCallbackDelegate.CB_SetAgreeInfo cbSetAgreeInfo = null;

    public static GamePotCallbackDelegate.CB_SetUserData cbSetUserData = null;

    public static GamePotCallbackDelegate.CB_Common cbInitialized = null;
    public static GamePotCallbackDelegate.CB_StagePay cbStagePay = null;

    private void Awake()
    {
        s_instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        s_instance = null;
    }

    public void setListener(IGamePot v)
    {
        Debug.Log("GamePotEventListener::setListener()");
        GamePotInterface = v;
    }

    public void onAppClose()
    {
        Debug.Log("GamePotEventListener::onAppClose()");

        if (cbLogin != null)
        {
            cbLogin(NCommon.ResultLogin.APP_CLOSE, null, null);
            cbLogin = null;
        }
        else
        {
            if (GamePotInterface != null)
                GamePotInterface.onAppClose();
        }
    }

    public void onNeedUpdate(string result)
    {
        //Debug.Log("GamePotEventListener::onNeedUpdate-" + result);

        try
        {
            NAppStatus status = JsonMapper.ToObject<NAppStatus>(result);

            if (!string.IsNullOrEmpty(status.resultPayload))
            {
                status.userInfo = JsonMapper.ToObject<NUserInfo>(status.resultPayload);
            }

            if (cbLogin != null)
            {
                cbLogin(NCommon.ResultLogin.NEED_UPDATE, null, status);
            }
            else if (cbCheckAppStatus != null)
            {
                cbCheckAppStatus(NCommon.ResultCheckAppStatus.NEED_UPDATE, status);
                cbCheckAppStatus = null;
            }
            else
            {
                if (GamePotInterface != null)
                    GamePotInterface.onNeedUpdate(status);
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onNeedUpdate - " + e.ToString());
        }


    }

    public void onMainternance(string result)
    {
        //Debug.Log("GamePotEventListener::onMainternance-" + result);

        try
        {
            NAppStatus status = JsonMapper.ToObject<NAppStatus>(result);
            status.userInfo = null;

            if (cbLogin != null)
            {
                cbLogin(NCommon.ResultLogin.MAINTENANCE, null, status);
            }
            else if (cbCheckAppStatus != null)
            {
                cbCheckAppStatus(NCommon.ResultCheckAppStatus.MAINTENANCE, status);
                cbCheckAppStatus = null;
            }
            else
            {
                if (GamePotInterface != null)
                    GamePotInterface.onMainternance(status);
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onMainternance - " + e.ToString());
        }
    }
    
    public void onGameServiceConflict(string result)
    {
        //Debug.Log("GamePotEventListener::onGameServiceConflict()-" + result);

        try
        {
            NUserInfo userInfo = JsonMapper.ToObject<NUserInfo>(result);

            if (cbLogin != null)
            {
                cbLogin(NCommon.ResultLogin.GAMESERVICE_CONFLICT, userInfo, null );
                cbLogin = null;
            }
            else
            {
                if (GamePotInterface != null)
                {
                    GamePotInterface.onGameServiceConflict(userInfo);
                }
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onLoginFailure - " + e.ToString());
        }
    }

    public void onLoginSuccess(string result)
    {
        //Debug.Log("GamePotEventListener::onLoginSuccess-" + result);

        try
        {
            NUserInfo userInfo = JsonMapper.ToObject<NUserInfo>(result);

            if (cbLogin != null)
            {
                cbLogin(NCommon.ResultLogin.SUCCESS, userInfo);
                cbLogin = null;
            }
            else
            {
                if (GamePotInterface != null)
                    GamePotInterface.onLoginSuccess(userInfo);
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onLoginSuccess - " + e.ToString());
        }
    }

    public void onLoginCancel()
    {
        Debug.Log("GamePotEventListener::onLoginCancel()");
        if (GamePotInterface != null)
            GamePotInterface.onLoginCancel();

        if (cbLogin != null)
        {
            cbLogin(NCommon.ResultLogin.CANCELLED);
            cbLogin = null;
        }
    }

    public void onLoginExit()
    {
        Debug.Log("GamePotEventListener::onLoginExit()");
        if (GamePotInterface != null)
            GamePotInterface.onLoginExit();

        if (cbLogin != null)
        {
            cbLogin(NCommon.ResultLogin.EXIT);
            cbLogin = null;
        }
    }

    public void onLoginFailure(string result)
    {
        Debug.Log("GamePotEventListener::onLoginFailure()-" + result);

        try
        {
            NError error = JsonMapper.ToObject<NError>(result);

            if (cbLogin != null)
            {
                cbLogin(NCommon.ResultLogin.FAILED, null, null, error);
                cbLogin = null;
            }
            else
            {
                if (GamePotInterface != null)
                {
                    GamePotInterface.onLoginFailure(error);
                }
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onLoginFailure - " + e.ToString());
        }
    }

    public void onDeleteMemberFailure(string result)
    {
        Debug.Log("GamePotEventListener::onDeleteMemberFailure() - " + result);

        try
        {
            NError error = JsonMapper.ToObject<NError>(result);

            if (cbDeleteMember != null)
            {
                cbDeleteMember(false, error);
                cbDeleteMember = null;
            }
            else
            {
                if (GamePotInterface != null)
                {
                    GamePotInterface.onDeleteMemberFailure(error);
                }
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onDeleteMemberFailure - " + e.ToString());
        }
    }

    public void onDeleteMemberSuccess()
    {
        //Debug.Log("GamePotEventListener::onDeleteMemberSuccess()");

        if (cbDeleteMember != null)
        {
            cbDeleteMember(true);
            cbDeleteMember = null;
        }
        else
        {
            if (GamePotInterface != null)
                GamePotInterface.onDeleteMemberSuccess();
        }
    }

    public void onLogoutFailure(string result)
    {
        Debug.Log("GamePotEventListener::onLogOutFailure() - " + result);

        try
        {
            NError error = JsonMapper.ToObject<NError>(result);

            if (cbLogout != null)
            {
                cbLogout(false, error);
                cbLogout = null;
            }
            else
            {
                if (GamePotInterface != null)
                {
                    GamePotInterface.onLogoutFailure(error);
                }
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onLogOutFailure - " + e.ToString());
        }
    }

    public void onLogoutSuccess(string result)
    {
        //Debug.Log("GamePotEventListener::onLogOutSuccess()");

        if (cbLogout != null)
        {
            cbLogout(true);
            cbLogout = null;
        }
        else
        {
            if (GamePotInterface != null)
                GamePotInterface.onLogoutSuccess();
        }
    }

    // public void onShowGlinkResult(string result)
    // {
    //     Debug.Log("GamePotEventListener::onShowGlinkResult()");
    // 	//		GamePotInterface.onShowGlinkResult(result);
    // }

    public void onCouponSuccess(string result)
    {
        //Debug.Log("GamePotEventListener::onCouponSuccess() " + result);

        if (cbCoupon != null)
        {
            cbCoupon(true);
            cbCoupon = null;
        }
        else
        {
            if (GamePotInterface != null)
                GamePotInterface.onCouponSuccess();
        }
    }

    public void onCouponFailure(string result)
    {
        Debug.Log("GamePotEventListener::onCouponFailure() : " + result);

        try
        {
            NError error = JsonMapper.ToObject<NError>(result);

            if (cbCoupon != null)
            {
                cbCoupon(false, error);
                cbCoupon = null;
            }
            else
            {
                if (GamePotInterface != null)
                {
                    GamePotInterface.onCouponFailure(error);
                }
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onCouponFailure - " + e.ToString());
        }
    }

    public void onPurchaseSuccess(string result)
    {
        //Debug.Log("GamePotEventListener::onPurchaseSuccess()");

        try
        {
            NPurchaseInfo purchaseInfo = JsonMapper.ToObject<NPurchaseInfo>(result);

            if (cbPurchase != null)
            {
                cbPurchase(NCommon.ResultPurchase.SUCCESS, purchaseInfo);
                cbPurchase = null;
            }
            else
            {
                if (GamePotInterface != null)
                {
                    GamePotInterface.onPurchaseSuccess(purchaseInfo);
                }
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onPurchaseSuccess - " + e.ToString());
        }

    }

    public void onPurchaseFailure(string result)
    {
        Debug.Log("GamePotEventListener::onPurchaseFailure() - " + result);

        try
        {
            NError error = JsonMapper.ToObject<NError>(result);

            if (cbPurchase != null)
            {
                cbPurchase(NCommon.ResultPurchase.FAILED, null, error);
                cbPurchase = null;
            }
            else
            {
                if (GamePotInterface != null)
                {
                    GamePotInterface.onPurchaseFailure(error);
                }
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onPurchaseFailure - " + e.ToString());
        }

    }

    public void onPurchaseCancel()
    {
        Debug.Log("GamePotEventListener::onPurchaseCancel()");

        if (cbPurchase != null)
        {
            cbPurchase(NCommon.ResultPurchase.CANCELLED);
            cbPurchase = null;
        }
        else
        {
            if (GamePotInterface != null)
                GamePotInterface.onPurchaseCancel();
        }
    }

    public void onCreateLinkingCancel(string result)
    {
        Debug.Log("GamePotEventListener::onCreateLinkCancel()" + result);

        if (cbCreateLinking != null)
        {
            cbCreateLinking(NCommon.ResultLinking.CANCELLED);
            cbCreateLinking = null;
        }
        else
        {
            if (GamePotInterface != null)
                GamePotInterface.onCreateLinkingCancel();
        }
    }

    public void onCreateLinkingSuccess(string result)
    {
        //Debug.Log("GamePotEventListener::onCreateLinkSuccess() - " + result);

        try
        {
            NUserInfo userInfo = JsonMapper.ToObject<NUserInfo>(result);

            if (cbCreateLinking != null)
            {
                cbCreateLinking(NCommon.ResultLinking.SUCCESS, userInfo);
                cbCreateLinking = null;
            }
            else
            {
                if (GamePotInterface != null)
                {
                    GamePotInterface.onCreateLinkingSuccess(userInfo);
                }
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onCreateLinkingSuccess - " + e.ToString());
        }
    }

    public void onCreateLinkingFailure(string result)
    {
        Debug.Log("GamePotEventListener::onCreateLinkFailure() - " + result);

        try
        {
            NError error = JsonMapper.ToObject<NError>(result);

            if (cbCreateLinking != null)
            {
                cbCreateLinking(NCommon.ResultLinking.FAILED, null, error);
                cbCreateLinking = null;
            }
            else
            {
                if (GamePotInterface != null)
                {
                    GamePotInterface.onCreateLinkingFailure(error);
                }
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onCreateLinkFailure - " + e.ToString());
        }
    }

    public void onDeleteLinkingSuccess()
    {
        //Debug.Log("GamePotEventListener::onDeleteLinkSuccess()");

        if (cbDeleteLinking != null)
        {
            cbDeleteLinking(true);
            cbDeleteLinking = null;
        }
        else
        {
            if (GamePotInterface != null)
                GamePotInterface.onDeleteLinkingSuccess();
        }
    }

    public void onDeleteLinkingFailure(string result)
    {
        Debug.Log("GamePotEventListener::onDeleteLinkFailure() - " + result);

        try
        {
            NError error = JsonMapper.ToObject<NError>(result);

            if (cbDeleteLinking != null)
            {
                cbDeleteLinking(false, error);
                cbDeleteLinking = null;
            }
            else
            {
                if (GamePotInterface != null)
                {
                    GamePotInterface.onDeleteLinkingFailure(error);
                }
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onDeleteLinkingFailure - " + e.ToString());
        }
    }

    public void onPushSuccess()
    {
        //Debug.Log("GamePotEventListener::onPushSuccess()");

        if (cbPushEnable != null)
        {
            cbPushEnable(true);
            cbPushEnable = null;
        }
        else
        {
            if (GamePotInterface != null)
                GamePotInterface.onPushSuccess();
        }
    }

    public void onPushFailure(string result)
    {
        Debug.Log("GamePotEventListener::onPushFailure()" + result);

        try
        {
            NError error = JsonMapper.ToObject<NError>(result);

            if (cbPushEnable != null)
            {
                cbPushEnable(false, error);
                cbPushEnable = null;
            }
            else
            {
                if (GamePotInterface != null)
                {
                    GamePotInterface.onPushFailure(error);
                }
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onPushFailure - " + e.ToString());
        }
    }

    public void onPushNightSuccess()
    {
        //Debug.Log("GamePotEventListener::onPushNightSuccess()");

        if (cbPushNightEnable != null)
        {
            cbPushNightEnable(true);
            cbPushNightEnable = null;
        }
        else
        {
            if (GamePotInterface != null)
                GamePotInterface.onPushNightSuccess();
        }
    }

    public void onPushNightFailure(string result)
    {
        Debug.Log("GamePotEventListener::onPushNightFailure()" + result);

        try
        {
            NError error = JsonMapper.ToObject<NError>(result);

            if (cbPushNightEnable != null)
            {
                cbPushNightEnable(false, error);
                cbPushNightEnable = null;
            }
            else
            {
                if (GamePotInterface != null)
                {
                    GamePotInterface.onPushNightFailure(error);
                }
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onPushNightFailure - " + e.ToString());
        }
    }

    public void onPushAdSuccess()
    {
        //Debug.Log("GamePotEventListener::onPushAdSuccess()");

        if (cbPushADEnable != null)
        {
            cbPushADEnable(true);
            cbPushADEnable = null;
        }
        else
        {
            if (GamePotInterface != null)
                GamePotInterface.onPushAdSuccess();
        }
    }

    public void onPushAdFailure(string result)
    {
        Debug.Log("GamePotEventListener::onPushAdFailure()" + result);

        try
        {
            NError error = JsonMapper.ToObject<NError>(result);

            if (cbPushADEnable != null)
            {
                cbPushADEnable(false, error);
                cbPushADEnable = null;
            }
            else
            {
                if (GamePotInterface != null)
                {
                    GamePotInterface.onPushAdFailure(error);
                }
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onPushAdFailure - " + e.ToString());
        }
    }

    public void onPushStatusSuccess()
    {
        //Debug.Log("GamePotEventListener::onPushStatusSuccess()");

        if (cbPushStatusEnable != null)
        {
            cbPushStatusEnable(true);
            cbPushStatusEnable = null;
        }
        else
        {
            if (GamePotInterface != null)
                GamePotInterface.onPushStatusSuccess();
        }
    }

    public void onPushStatusFailure(string result)
    {
        Debug.Log("GamePotEventListener::onPushStatusFailure()" + result);

        try
        {
            NError error = JsonMapper.ToObject<NError>(result);

            if (cbPushStatusEnable != null)
            {
                cbPushStatusEnable(false, error);
                cbPushStatusEnable = null;
            }
            else
            {
                if (GamePotInterface != null)
                {
                    GamePotInterface.onPushStatusFailure(error);
                }
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onPushStatusFailure - " + e.ToString());
        }
    }

    public void onAgreeDialogSuccess(string result)
    {
        //Debug.Log("GamePotEventListener::onAgreeDialogSuccess() - " + result);

        try
        {
            NAgreeResultInfo resultInfo = JsonMapper.ToObject<NAgreeResultInfo>(result);
            //NAgreeResultInfo resultInfo = new NAgreeResultInfo();

            if (cbShowAgree != null)
            {
                cbShowAgree(true, resultInfo);
                cbShowAgree = null;
            }
            else
            {
                if (GamePotInterface != null)
                {
                    GamePotInterface.onAgreeDialogSuccess(resultInfo);
                }
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onAgreeDialogSuccess - " + e.ToString());
        }
    }

    public void onAgreeDialogFailure(string result)
    {
        Debug.Log("GamePotEventListener::onAgreeDialogFailure()" + result);

        try
        {
            NError error = JsonMapper.ToObject<NError>(result);

            if (cbShowAgree != null)
            {
                cbShowAgree(false, null, error);
                cbShowAgree = null;
            }
            else
            {
                if (GamePotInterface != null)
                {
                    GamePotInterface.onAgreeDialogFailure(error);
                }
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onAgreeDialogFailure - " + e.ToString());
        }
    }

    public void onReceiveScheme(string scheme)
    {
        Debug.Log("GamePotEventListener::onReceiveScheme()" + scheme);
        if (GamePotInterface != null)
        {
            GamePotInterface.onReceiveScheme(scheme);
        }
    }

    public void onLoadAchievementSuccess(string result)
    {
        //Debug.Log("GamePotEventListener::onLoadAchievementSuccess()" + result);

        try
        {
            if (GamePotInterface != null)
            {
                List<NAchievementInfo> resultInfo = JsonMapper.ToObject<List<NAchievementInfo>>(result);
                GamePotInterface.onLoadAchievementSuccess(resultInfo);
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onLoadAchievementSuccess - " + e.ToString());
        }
    }

    public void onLoadAchievementFailure(string result)
    {
        Debug.Log("GamePotEventListener::onLoadAchievementFailure()" + result);

        try
        {
            if (GamePotInterface != null)
            {
                NError error = JsonMapper.ToObject<NError>(result);
                GamePotInterface.onLoadAchievementFailure(error);
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onLoadAchievementFailure - " + e.ToString());
        }
    }

    public void onLoadAchievementCancel()
    {
        Debug.Log("GamePotEventListener::onLoadAchievementCancel()");
        if (GamePotInterface != null)
            GamePotInterface.onLoadAchievementCancel();
    }

    public void onWebviewClose(string result)
    {
        //Debug.Log("GamePotEventListener::onWebviewClose()" + result);
        if (cbShowWebView != null)
        {
            cbShowWebView(result);
            cbShowWebView = null;
        }
        else
        {
            if (GamePotInterface != null)
            {
                GamePotInterface.onWebviewClose(result);
            }
        }
    }

    public void onPurchaseDetailListSuccess(string result)
    {
        //Debug.Log("GamePotEventListener::onPurchaseDetailListSuccess() " + result);

        try
        {
            NPurchaseItem[] itemList = JsonMapper.ToObject<NPurchaseItem[]>(result);

            if (cbPurchaseDetailList != null)
            {
                cbPurchaseDetailList(true, itemList);
                cbPurchaseDetailList = null;
            }
            else
            {
                if (GamePotInterface != null)
                {
                    GamePotInterface.onPurchaseDetailListSuccess(itemList);
                }
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onPurchaseDetailListSuccess - " + e.ToString());
        }
    }

    public void onPurchaseDetailListFailure(string result)
    {
        Debug.Log("GamePotEventListener::onPurchaseDetailListFailure() " + result);

        try
        {
            NError error = JsonMapper.ToObject<NError>(result);

            if (cbPurchaseDetailList != null)
            {
                cbPurchaseDetailList(false, null, error);
                cbPurchaseDetailList = null;
            }
            else
            {
                if (GamePotInterface != null)
                {
                    GamePotInterface.onPurchaseDetailListFailure(error);
                }
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onPurchaseDetailListFailure - " + e.ToString());
        }
    }

    public void onRequestTrackingAuthorization(string _result)
    {
        Debug.Log("GamePotEventListener::onRequestTrackingAuthorization() " + _result);
        NResultTrackingAuthorization opt_result = new NResultTrackingAuthorization(_result);

        if (cbRequestTrackingAuthorization != null)
        {
            cbRequestTrackingAuthorization(opt_result);
            cbRequestTrackingAuthorization = null;
        }
        else
        {
            if (GamePotInterface != null)
            {
                GamePotInterface.onRequestTrackingAuthorization(opt_result);
            }
        }
    }

    public void onCheckAppStatusSuccess()
    {
        //Debug.Log("GamePotEventListener::onCheckAppStatusSuccess() ");

        if (cbCheckAppStatus != null)
        {
            cbCheckAppStatus(NCommon.ResultCheckAppStatus.SUCCESS);
            cbCheckAppStatus = null;
        }
        else
        {
            if (GamePotInterface != null)
            {
                GamePotInterface.onCheckAppStatusSuccess();
            }
        }
    }

    // 초기화 완료 콜백(onInitialized) - 델리게이트 경로
    public void onInitializedSuccess()
    {
        Debug.Log("GamePotEventListener::onInitializedSuccess()");
        if (cbInitialized != null)
        {
            cbInitialized(true);
            cbInitialized = null;
        }
    }

    public void onInitializedFailure(string _result)
    {
        Debug.Log("GamePotEventListener::onInitializedFailure() " + _result);
        if (cbInitialized != null)
        {
            NError error = null;
            try { error = JsonMapper.ToObject<NError>(_result); } catch { }
            cbInitialized(false, error);
            cbInitialized = null;
        }
    }

    // StagePay 웹뷰 종료 콜백(결과 JSON) - 델리게이트 경로
    public void onStagePayClose(string _result)
    {
        Debug.Log("GamePotEventListener::onStagePayClose() " + _result);
        if (cbStagePay != null)
        {
            cbStagePay(_result);
            cbStagePay = null;
        }
    }

    public void onCheckAppStatusFailure(string _result)
    {
        Debug.Log("GamePotEventListener::onCheckAppStatusFailure() " + _result);

        try
        {
            NError error = JsonMapper.ToObject<NError>(_result);

            if (cbCheckAppStatus != null)
            {
                cbCheckAppStatus(NCommon.ResultCheckAppStatus.FAILED, null, error);
                cbCheckAppStatus = null;
            }
            else
            {
                if (GamePotInterface != null)
                {
                    GamePotInterface.onCheckAppStatusFailure(error);
                }
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onCheckAppStatusFailure - " + e.ToString());
        }
    }

    public void onSendAgreeEmailSuccess(string _result)
    {
        //Debug.Log("GamePotEventListener::onSendAgreeEmailSuccess() " + _result);

        try
        {
            NAgreeSendEmailInfo sendEmailInfo = JsonMapper.ToObject<NAgreeSendEmailInfo>(_result);

            if (cbSendAgreeEmail != null)
            {
                cbSendAgreeEmail(true, sendEmailInfo);
                cbSendAgreeEmail = null;
            }
            else
            {
                if (GamePotInterface != null)
                {
                    GamePotInterface.onSendAgreeEmailSuccess(sendEmailInfo);
                }
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onSendAgreeEmailSuccess - " + e.ToString());
        }
    }

    public void onSendAgreeEmailFailure(string _error)
    {
        Debug.Log("GamePotEventListener::onSendAgreeEmailFailure() " + _error);

        try
        {
            NError error = JsonMapper.ToObject<NError>(_error);

            if (cbSendAgreeEmail != null)
            {
                cbSendAgreeEmail(false, null, error);
                cbSendAgreeEmail = null;
            }
            else
            {
                if (GamePotInterface != null)
                {
                    GamePotInterface.onSendAgreeEmailFailure(error);
                }
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onSendAgreeEmailFailure - " + e.ToString());
        }
    }

    public void onCheckAgreeEmailSuccess(string _result)
    {
        //Debug.Log("GamePotEventListener::onCheckAgreeEmailSuccess() " + _result);

        try
        {
            NAgreeCheckEmailInfo checkEmailInfo = JsonMapper.ToObject<NAgreeCheckEmailInfo>(_result);

            if (cbCheckAgreeEmail != null)
            {
                cbCheckAgreeEmail(true, checkEmailInfo);
                cbCheckAgreeEmail = null;
            }
            else
            {
                if (GamePotInterface != null)
                {
                    GamePotInterface.onCheckAgreeEmailSuccess(checkEmailInfo);
                }
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onCheckAgreeEmailSuccess - " + e.ToString());
        }
    }

    public void onCheckAgreeEmailFailure(string _error)
    {
        Debug.Log("GamePotEventListener::onCheckAgreeEmailFailure() " + _error);

        try
        {
            NError error = JsonMapper.ToObject<NError>(_error);

            if (cbCheckAgreeEmail != null)
            {
                cbCheckAgreeEmail(false, null, error);
                cbCheckAgreeEmail = null;
            }
            else
            {
                if (GamePotInterface != null)
                {
                    GamePotInterface.onCheckAgreeEmailFailure(error);
                }
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onCheckAgreeEmailFailure - " + e.ToString());
        }
    }

    public void onSetAgreeInfoSuccess()
    {
        //Debug.Log("GamePotEventListener::onSetAgreeInfoSuccess()");

        try
        {
            if (cbSetAgreeInfo != null)
            {
                cbSetAgreeInfo(true);
                cbSetAgreeInfo = null;
            }
            else
            {
                if (GamePotInterface != null)
                {
                    GamePotInterface.onSetAgreeInfoSuccess();
                }
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onSetAgreeInfoSuccess - " + e.ToString());
        }
    }

    public void onSetAgreeInfoFailure(string _error)
    {
        Debug.Log("GamePotEventListener::onSetAgreeInfoFailure() " + _error);

        try
        {
            NError error = JsonMapper.ToObject<NError>(_error);

            if (cbSetAgreeInfo != null)
            {
                cbSetAgreeInfo(false, error);
                cbSetAgreeInfo = null;
            }
            else
            {
                if (GamePotInterface != null)
                {
                    GamePotInterface.onSetAgreeInfoFailure(error);
                }
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onSetAgreeInfoFailure - " + e.ToString());
        }
    }

    public void onSetUserDataSuccess()
    {
        //Debug.Log("GamePotEventListener::onSetUserDataSuccess()");

        try
        {
            if (cbSetUserData != null)
            {
                cbSetUserData(true);
                cbSetUserData = null;
            }
            else
            {
                if (GamePotInterface != null)
                {
                    GamePotInterface.onSetUserDataSuccess();
                }
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onSetUserDataSuccess - " + e.ToString());
        }
    }

    public void onSetUserDataFailure(string _error)
    {
        Debug.Log("GamePotEventListener::onSetUserDataFailure() " + _error);

        try
        {
            NError error = JsonMapper.ToObject<NError>(_error);

            if (cbSetUserData != null)
            {
                cbSetUserData(false, error);
                cbSetUserData = null;
            }
            else
            {
                if (GamePotInterface != null)
                {
                    GamePotInterface.onSetUserDataFailure(error);
                }
            }
        }
        catch (JsonException e)
        {
            Debug.LogError("[GamePotEventListener] onSetUserDataFailure - " + e.ToString());
        }

    }

#if UNITY_STANDALONE_OSX
    public void onFetchProductsSuccess(string result)
    {
        
        if (GamePotInterface is IGamePotMacOptional mac)
        {
            mac.onFetchProductsSuccess(result);
        }
    }

    public void onFetchProductsFailure(string result)
    {
        Debug.LogError("GamePotEventListener::onFetchProductsFailure() - " + result);
        
        if (GamePotInterface is IGamePotMacOptional mac)
        {
            mac.onFetchProductsFailure(result);
        }
    }

    public void onPurchaseDeferred(string result)
    {
        
        if (GamePotInterface is IGamePotMacOptional mac)
        {
            mac.onPurchaseDeferred(result);
        }
    }

    public void onRestorePurchaseSuccess(string result)
    {
        
        if (GamePotInterface is IGamePotMacOptional mac)
        {
            mac.onRestorePurchaseSuccess(result);
        }
    }

    public void onRestorePurchaseFailure(string result)
    {
        Debug.LogError("GamePotEventListener::onRestorePurchaseFailure() - " + result);
        
        if (GamePotInterface is IGamePotMacOptional mac)
        {
            mac.onRestorePurchaseFailure(result);
        }
    }

    public void onRestorePurchasesComplete(string result)
    {
        Debug.Log("GamePotEventListener::onRestorePurchasesComplete() - " + result);
        
        if (GamePotInterface is IGamePotMacOptional mac)
        {
            mac.onRestorePurchasesComplete(result);
        }
    }
#endif
}
