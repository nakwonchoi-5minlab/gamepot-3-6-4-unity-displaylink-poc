using UnityEngine;
using System.Collections;
using Realtime.LITJson;

public class NPurchaseInfo
{
    public string price { get; set; }               // 가격
    public string adjustKey { get; set; }           // adjust Key
    public string productId { get; set; }           // 아이템 ID
    public string currency { get; set; }            // 통화
    public string orderId { get; set; }             // 스토어 order id
    public string productName { get; set; }         // 아이템 이름
    public string gamepotOrderId { get; set; }      // GAMEPOT에서 생성한 order id 
    public string uniqueId { get; set; }            // (개발사에서 별도로 관리하는) 영수증 ID
    public string serverId { get; set; }            // (결제를 진행한 캐릭터의) 서버 아이디
    public string playerId { get; set; }            // (결제를 진행한 캐릭터의) 캐릭터 아이디
    public string etc { get; set; }                     // (결제를 진행한 캐릭터의) 기타 정보
    public string signature { get; set; }           // 스토어 signature 
    public string originalJSONData { get; set; }    // 영수증 Data
    public string originalData { get; set; }        // ios 영수증 Data
}