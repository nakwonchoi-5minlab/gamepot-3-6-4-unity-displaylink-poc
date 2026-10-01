using UnityEngine;
using UnityEngine.UI;

namespace GamePotDemo
{

    public class CustomizedPopup : MonoBehaviour
    {
        public delegate void popupCallback();

        public struct PopupButtonInfo
        {
            public string btnText;
            public popupCallback callback;
        }


        [SerializeField]
        private Text title;
        [SerializeField]
        private Text message;

        [SerializeField]
        private GameObject[] buttons;

        private popupCallback[] callbacks;


        public void SetPopup(string title, string message, PopupButtonInfo[] btn_info)
        {
            SetText(title, message);
            setButton(btn_info);
        }

        protected void SetText(string title, string message)
        {
            if (!string.IsNullOrEmpty(title))
                this.title.text = title;
            if (!string.IsNullOrEmpty(message))
                this.message.text = message;
        }

        public void setButton(PopupButtonInfo[] input_info)
        {
            if (input_info == null)
                return;

            if (buttons.Length > 0)
                this.callbacks = new popupCallback[buttons.Length];


            for (int i = 0; i < input_info.Length && i < buttons.Length; i++)
            {
                if (input_info[i].callback != null)
                    this.callbacks[i] = input_info[i].callback;

                if (!string.IsNullOrEmpty(input_info[i].btnText)
                && buttons[i].transform.GetChild(0) != null
                && buttons[i].transform.GetChild(0).GetComponent<Text>() != null)
                {
                    Text btn_text = buttons[i].transform.GetChild(0).GetComponent<Text>();
                    btn_text.text = input_info[i].btnText;
                }
            }
        }

        #region UIButton.onClick

        virtual public void ClickButton(int idx)
        {
            if (callbacks != null && callbacks[idx] != null)
            {
                callbacks[idx]();
            }
        }

        public void ClosePopup()
        {
            Destroy(gameObject);
        }
        #endregion
    }

}
