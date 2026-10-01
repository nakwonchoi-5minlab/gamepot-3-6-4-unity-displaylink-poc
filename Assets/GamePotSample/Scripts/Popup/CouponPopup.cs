using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GamePotUnity;

namespace GamePotDemo
{
    public class CouponPopup : CustomizedPopup
    {
        public void ClickCouponButton(Text couponCode)
        {
            GamePot.coupon(couponCode.text);
        }
    }
}


