using UnityEngine;
using System.Collections;
using Realtime.LITJson;
using System;


public class NAgreeSendEmailInfo
{
    public string key { get; set; }
    public string expiredAt { get; set; }
}

public class NAgreeCheckEmailInfo
{
    /*
     * status
     * -1 : (confirm)key 인증에러
     * 0 : 이메일 인증 미진행
     * 1 : 이메일 인증 완료
     */
    public int status { get; set; }
}
