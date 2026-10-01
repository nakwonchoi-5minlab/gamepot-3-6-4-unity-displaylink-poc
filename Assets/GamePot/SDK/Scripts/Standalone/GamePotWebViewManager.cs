using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_STANDALONE
using GamePotUnity.Standalone.Networking;

namespace GamePotUnity.Standalone
{
    public class GamePotWebViewManager : MonoBehaviour
    {
        public static GamePotWebViewManager s_instance;
        public GameObject _webview;
        GameObject _hardwareKeyboardListener;

        Dictionary<string, string> _header = null;

        public static void initialize()
        {
#if VUPLEX
            GameObject webviewMgr = Resources.Load<GameObject>("GamePotWebViewManager");
            Instantiate(webviewMgr);
#endif
        }

        private void Awake()
        {
            // if(s_instance != null)
            //     DestroyImmediate(s_instance);

            // GameObject webviewMgr = Resources.Load<GameObject>("GamePotWebViewManager");
            //  Instantiate(webviewMgr).GetComponent<GamePotWebViewManager>();

            // this.gameObject.AddComponent<Canvas>();
            // this.gameObject.AddComponent<CanvasScaler>();
            // this.gameObject.AddComponent<GraphicRaycaster>();
            // this.gameObject.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;

            // GameObject _frame = new GameObject("Frame");
            // _frame.AddComponent<Image>();
            // _frame.transform.parent = this.gameObject.transform;

            // RectTransform rt = _frame.GetComponent<RectTransform>();
            // rt.anchorMin = new Vector2(0, 0);
            // rt.anchorMax = new Vector2(1, 1);

            // SetLeft(rt, 100.0f);
            // SetRight(rt, 100.0f);
            // SetTop(rt, 40.0f);
            // SetBottom(rt, 40.0f);

            s_instance = this;
            DontDestroyOnLoad(s_instance.gameObject);
            //Debug.Log("called don't deleted!");
        }

        private void Start()
        {
            s_instance.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            s_instance = null;
        }

        public static void showWebView()
        {
            s_instance.gameObject.SetActive(true);

            //if (s_instance._webview != null)
            //{
            //    GameObject.DestroyImmediate(s_instance._webview.gameObject);
            //}

//#if VUPLEX
//            if (s_instance._webview == null)
//                s_instance._webview = CanvasWebViewPrefab.Instantiate().gameObject;

//            GameObject _frame = s_instance.transform.GetChild(0).gameObject;
//            _webview.transform.parent = _frame.transform;
//            _webview.transform.localPosition = new Vector3(0, 0, 0);
//            _webview.transform.localScale = new Vector3(1, 1, 1);
//            RectTransform rt = _webview.GetComponent<RectTransform>();
//            rt.offsetMin = new Vector2(0, 0);
//            rt.offsetMax = new Vector2(0, 0);

//            CanvasWebViewPrefab c_webview = s_instance._webview.gameObject.GetComponent<CanvasWebViewPrefab>();

//            if (_header != null)
//            {
//                c_webview.Initialized += (initializedSender, initializedEventArgs) =>
//                {
//                    c_webview.WebView.LoadUrl(_url, _header);
//                };
//            }
//            else
//            {
//                c_webview.Initialized += (initializedSender, initializedEventArgs) =>
//                  {
//                      c_webview.WebView.LoadUrl(_url);
//                  };
//            }
//#endif


        }

        public void closeWebView()
        {
            if (s_instance._webview != null)
            {
                //s_instance._webview.
                GameObject.DestroyImmediate(s_instance._webview.gameObject);
            }
            s_instance.gameObject.SetActive(false);
        }


        public static void SetLeft(RectTransform rt, float left)
        {
            rt.offsetMin = new Vector2(left, rt.offsetMin.y);
        }

        public static void SetRight(RectTransform rt, float right)
        {
            rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
        }

        public static void SetTop(RectTransform rt, float top)
        {
            rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
        }

        public static void SetBottom(RectTransform rt, float bottom)
        {
            rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
        }

    }
}
#endif