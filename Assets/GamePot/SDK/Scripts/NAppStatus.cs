using UnityEngine;
using System.Collections;
using Realtime.LITJson;
using System;

public class NAppStatus
{
    public string type { get; set; }
    public string message { get; set; }
    public string url { get; set; }
    public string currentAppVersion { get; set; }
    public string updateAppVersion { get; set; }
    public int currentAppVersionCode { get; set; }
    public int updateAppVersionCode { get; set; }
    public bool isForce { get; set; }
    public string resultPayload { get; set; }
    public double startedAt { get; set; }
    public double endedAt { get; set; }

    /// <summary>
    /// v2.0.3 Value exists when isForce is false
    /// </summary>
    public NUserInfo userInfo = null;

    public string ToJson()
    {
        JsonData data = new JsonData();

        data["type"] = type;
        data["message"] = message;

        if (type.Equals("maintenance"))
        {
            if (url != null && !url.Equals("null"))
            {
                data["url"] = url;
            }
            data["startedAt"] = startedAt;
            data["endedAt"] = endedAt;
        }
        else if (type.Equals("needupdate"))
        {
            if (currentAppVersion != null && !currentAppVersion.Equals("null"))
            {
                data["currentAppVersion"] = currentAppVersion;
            }
            if (updateAppVersion != null && !updateAppVersion.Equals("null"))
            {
                data["updateAppVersion"] = updateAppVersion;
            }

            if (url != null && !url.Equals("null"))
            {
                data["url"] = url;
            }

            data["currentAppVersionCode"] = currentAppVersionCode;
            data["updateAppVersionCode"] = updateAppVersionCode;
            data["isForce"] = isForce;


            NUserInfo nestedUserinfo = null;
            if (resultPayload != null && !resultPayload.Equals("null"))
                nestedUserinfo = JsonMapper.ToObject<NUserInfo>(resultPayload);

            if (nestedUserinfo != null)
                data["resultPayload"] = nestedUserinfo.ToJson();
        }

        //Debug.Log("NAppStatus::ToJson() - " + data.ToJson());

        return data.ToJson();
    }
}