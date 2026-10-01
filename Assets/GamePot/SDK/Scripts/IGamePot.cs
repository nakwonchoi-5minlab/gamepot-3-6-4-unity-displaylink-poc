using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IGamePot
{

    void onAppClose();
    void onNeedUpdate(NAppStatus status);
    void onMainternance(NAppStatus status);
    void onGameServiceConflict(NUserInfo userInfo);
    void onLoginCancel();
    void onLoginExit();
    void onLoginSuccess(NUserInfo userInfo);
    void onLoginFailure(NError error);
    void onDeleteMemberSuccess();
    void onDeleteMemberFailure(NError error);
    void onLogoutSuccess();
    void onLogoutFailure(NError error);
    void onCouponSuccess();
    void onCouponFailure(NError error);
    void onPurchaseSuccess(NPurchaseInfo purchaseInfo);
    void onPurchaseFailure(NError error);
    void onPurchaseCancel();
    void onCreateLinkingSuccess(NUserInfo userInfo);
    void onCreateLinkingFailure(NError error);
    void onCreateLinkingCancel();
    void onDeleteLinkingSuccess();
    void onDeleteLinkingFailure(NError error);
    void onPushSuccess();
    void onPushFailure(NError error);
    void onPushNightSuccess();
    void onPushNightFailure(NError error);
    void onPushAdSuccess();
    void onPushAdFailure(NError error);
    void onPushStatusSuccess();
    void onPushStatusFailure(NError error);
    void onAgreeDialogSuccess(NAgreeResultInfo info);
    void onAgreeDialogFailure(NError error);
    void onReceiveScheme(string scheme);
    void onLoadAchievementSuccess(List<NAchievementInfo> info);
    void onLoadAchievementFailure(NError error);
    void onLoadAchievementCancel();
    void onWebviewClose(string result);
    void onPurchaseDetailListSuccess(NPurchaseItem[] purchaseInfoList);
    void onPurchaseDetailListFailure(NError error);
    void onRequestTrackingAuthorization(NResultTrackingAuthorization resultState);
    void onCheckAppStatusSuccess();
    void onCheckAppStatusFailure(NError error);

    void onSendAgreeEmailSuccess(NAgreeSendEmailInfo resultInfo);
    void onSendAgreeEmailFailure(NError error);

    void onCheckAgreeEmailSuccess(NAgreeCheckEmailInfo resultInfo);
    void onCheckAgreeEmailFailure(NError error);

    void onSetAgreeInfoSuccess();
    void onSetAgreeInfoFailure(NError error);

    void onSetUserDataSuccess();
    void onSetUserDataFailure(NError error);
}
