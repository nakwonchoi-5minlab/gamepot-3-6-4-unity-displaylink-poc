using UnityEngine;
using System.Collections;
using Realtime.LITJson;


public class NPurchaseItem
{
    public string productId { get; set; }
    public string type { get; set; }
    public string price { get; set; }
    public string price_amount { get; set; }
    public string price_amount_micros { get; set; }
    public string price_currency_code { get; set; }
    public string price_with_currency { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public JsonData subscription_offer_details { get; set; }
}