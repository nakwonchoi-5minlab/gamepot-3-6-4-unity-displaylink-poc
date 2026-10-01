using UnityEngine;
using System.Collections;
using Realtime.LITJson;

public class NResultTrackingAuthorization
{
    public NCommon.ResultTrackingAuthorization  authorization { get; set; }  							       // 계정 연동 정보

    public NResultTrackingAuthorization(string trackingAuthoriazation)
    {
        switch(trackingAuthoriazation)
        {
            case "ATTrackingManagerAuthorizationStatusNotDetermined" :
                    authorization =  NCommon.ResultTrackingAuthorization.ATTrackingManagerAuthorizationStatusNotDetermined;
                break;
            case "ATTrackingManagerAuthorizationStatusRestricted" :
                    authorization =  NCommon.ResultTrackingAuthorization.ATTrackingManagerAuthorizationStatusRestricted;
                break;
            case "ATTrackingManagerAuthorizationStatusDenied" :
                    authorization =  NCommon.ResultTrackingAuthorization.ATTrackingManagerAuthorizationStatusDenied;
                break;
             case "ATTrackingManagerAuthorizationStatusAuthorized" :
                    authorization =  NCommon.ResultTrackingAuthorization.ATTrackingManagerAuthorizationStatusAuthorized;
                break;
            default :
                     //디폴트는 NotDeterminded로..
                    authorization =  NCommon.ResultTrackingAuthorization.ATTrackingManagerAuthorizationStatusUnknown;
                break;
        }
    }
}