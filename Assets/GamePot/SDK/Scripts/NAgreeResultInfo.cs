using UnityEngine;
using System;
using System.Collections;
using Realtime.LITJson;

public class NAgreeResultInfo
{
    public bool agree { get; set; }
    public bool agreePush { get; set; }
    public bool agreeNight { get; set; }

    public string agreeGDPR { get; set; }
    public int agreeGDPRStatus { get; set; }
    public string emailVerified { get; set; }

    public string[] getAgreeGDPR()
    {
        return agreeGDPR.Split(',');
    }

    public string ToJson()
    {
        JsonData data = new JsonData();

        data["agree"] = agree ? "true" : "false";
        data["agreePush"] = agreePush ? "true" : "false";
        data["agreeNight"] = agreeNight ? "true" : "false";
        data["agreeGDPR"] = agreeGDPR;
        data["agreeGDPRStatus"] = agreeGDPRStatus;
        data["emailVerified"] = emailVerified;

        Debug.Log("NAgreeResultInfo::ToJson() - " + data.ToJson());
        return data.ToJson();
    }
}