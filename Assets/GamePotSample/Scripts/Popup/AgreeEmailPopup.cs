using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GamePotUnity;

namespace GamePotDemo
{
    public class AgreeEmailPopup : CustomizedPopup
    {
        private string email;

        private string key;

        public void SetEmail(InputField inputEmail)
        {
            email = inputEmail.text.Trim();
        }
        public void SetKey(InputField inputKey)
        {
            key = inputKey.text.Trim();
        }

        public void ClickSendEmailButton()
        {
            GamePot.sendAgreeEmail(email);
        }

        public void ClickCheckEmailButton()
        {
            GamePot.checkAgreeEmail(email, key);
        }

    }
}

