using System;
using System.Collections.Generic;
using Realtime.LITJson;
using UnityEngine;

public class NLoginUIInfo
{
    public NLoginUIInfo()
    {
        //이미지 로고 삽입 여부
        showLogo = false;

        //UI로 노출할 Login Type
        loginTypes = new NCommon.LoginType[] { };
    }

    public bool showLogo { get; set; }
    public NCommon.LoginType[] loginTypes { get; set; }

    public string ToJson()
    {
        JsonData data = new JsonData();
        data["showLogo"] = showLogo ? "true" : "false";
        List<string> strLoginTypes = new List<string>();

        foreach (NCommon.LoginType elem in loginTypes)
        {
            strLoginTypes.Add(elem.ToString());
        }
        data["loginTypes"] = string.Join(",", strLoginTypes.ToArray());

        Debug.Log("NLoginUIInfo::ToJson - " + data.ToJson());
        return data.ToJson();
    }
}
