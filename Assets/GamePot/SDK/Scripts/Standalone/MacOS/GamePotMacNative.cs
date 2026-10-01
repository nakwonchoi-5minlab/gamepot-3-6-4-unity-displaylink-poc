using System;
using System.Runtime.InteropServices;
using UnityEngine;

#if UNITY_STANDALONE_OSX
namespace GamePotUnity.Standalone.macOS
{
    public static class GamePotMacNative
    {
        private const string PLUGIN_NAME = "GamePotMac";

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void NativeCallback(string json);

        #region Sign in with Apple

        [DllImport(PLUGIN_NAME)]
        private static extern void GamePotMac_InitAppleSignIn(string unityObjectName);

        [DllImport(PLUGIN_NAME)]
        private static extern void GamePotMac_SignInWithApple(string nonce);

        [DllImport(PLUGIN_NAME)]
        private static extern IntPtr GamePotMac_GetAppleSignInError();

        [DllImport(PLUGIN_NAME)]
        private static extern void GamePotMac_SetAppleSignInSuccessCallback(NativeCallback callback);

        [DllImport(PLUGIN_NAME)]
        private static extern void GamePotMac_SetAppleSignInFailureCallback(NativeCallback callback);

        public static void InitAppleSignIn(string unityObjectName)
        {
            GamePotMac_InitAppleSignIn(unityObjectName);
        }

        public static void SetAppleSignInSuccessCallback(NativeCallback callback)
        {
            GamePotMac_SetAppleSignInSuccessCallback(callback);
        }

        public static void SetAppleSignInFailureCallback(NativeCallback callback)
        {
            GamePotMac_SetAppleSignInFailureCallback(callback);
        }

        public static void SignInWithApple(string nonce = null)
        {
            GamePotMac_SignInWithApple(nonce);
        }

        public static string GetAppleSignInError()
        {
            IntPtr ptr = GamePotMac_GetAppleSignInError();
            return ptr != IntPtr.Zero ? Marshal.PtrToStringAuto(ptr) : null;
        }

        #endregion

        #region StoreKit (In-App Purchase)

        [DllImport(PLUGIN_NAME)]
        private static extern void GamePotMac_InitStoreKit(string unityObjectName);

        [DllImport(PLUGIN_NAME)]
        private static extern void GamePotMac_FetchProducts(string productIds);

        [DllImport(PLUGIN_NAME)]
        private static extern void GamePotMac_PurchaseProduct(string productId);

        [DllImport(PLUGIN_NAME)]
        private static extern void GamePotMac_PurchaseProductWithUserData(string productId, string userDataJson);

        [DllImport(PLUGIN_NAME)]
        private static extern void GamePotMac_RestorePurchases();

        [DllImport(PLUGIN_NAME)]
        private static extern void GamePotMac_FinishTransaction(string transactionId);

        [DllImport(PLUGIN_NAME)]
        private static extern IntPtr GamePotMac_GetStoreKitError();

        [DllImport(PLUGIN_NAME)]
        private static extern bool GamePotMac_CanMakePayments();

        [DllImport(PLUGIN_NAME)]
        private static extern void GamePotMac_SetFetchProductsSuccessCallback(NativeCallback callback);

        [DllImport(PLUGIN_NAME)]
        private static extern void GamePotMac_SetFetchProductsFailureCallback(NativeCallback callback);

        [DllImport(PLUGIN_NAME)]
        private static extern void GamePotMac_SetPurchaseSuccessCallback(NativeCallback callback);

        [DllImport(PLUGIN_NAME)]
        private static extern void GamePotMac_SetPurchaseFailureCallback(NativeCallback callback);

        [DllImport(PLUGIN_NAME)]
        private static extern void GamePotMac_SetPurchaseDeferredCallback(NativeCallback callback);

        [DllImport(PLUGIN_NAME)]
        private static extern void GamePotMac_SetRestorePurchaseSuccessCallback(NativeCallback callback);

        [DllImport(PLUGIN_NAME)]
        private static extern void GamePotMac_SetRestorePurchaseFailureCallback(NativeCallback callback);

        [DllImport(PLUGIN_NAME)]
        private static extern void GamePotMac_SetRestorePurchasesCompleteCallback(NativeCallback callback);

        public static void InitStoreKit(string unityObjectName)
        {
            GamePotMac_InitStoreKit(unityObjectName);
        }

        public static void SetFetchProductsSuccessCallback(NativeCallback callback)
        {
            GamePotMac_SetFetchProductsSuccessCallback(callback);
        }

        public static void SetFetchProductsFailureCallback(NativeCallback callback)
        {
            GamePotMac_SetFetchProductsFailureCallback(callback);
        }

        public static void SetPurchaseSuccessCallback(NativeCallback callback)
        {
            GamePotMac_SetPurchaseSuccessCallback(callback);
        }

        public static void SetPurchaseFailureCallback(NativeCallback callback)
        {
            GamePotMac_SetPurchaseFailureCallback(callback);
        }

        public static void SetPurchaseDeferredCallback(NativeCallback callback)
        {
            GamePotMac_SetPurchaseDeferredCallback(callback);
        }

        public static void SetRestorePurchaseSuccessCallback(NativeCallback callback)
        {
            GamePotMac_SetRestorePurchaseSuccessCallback(callback);
        }

        public static void SetRestorePurchaseFailureCallback(NativeCallback callback)
        {
            GamePotMac_SetRestorePurchaseFailureCallback(callback);
        }

        public static void SetRestorePurchasesCompleteCallback(NativeCallback callback)
        {
            GamePotMac_SetRestorePurchasesCompleteCallback(callback);
        }

        public static void FetchProducts(string productIds)
        {
            GamePotMac_FetchProducts(productIds);
        }

        public static void PurchaseProduct(string productId)
        {
            GamePotMac_PurchaseProduct(productId);
        }

        public static void PurchaseProductWithUserData(string productId, string userDataJson)
        {
            GamePotMac_PurchaseProductWithUserData(productId, userDataJson);
        }

        public static void RestorePurchases()
        {
            GamePotMac_RestorePurchases();
        }

        public static void FinishTransaction(string transactionId)
        {
            GamePotMac_FinishTransaction(transactionId);
        }

        public static string GetStoreKitError()
        {
            IntPtr ptr = GamePotMac_GetStoreKitError();
            return ptr != IntPtr.Zero ? Marshal.PtrToStringAuto(ptr) : null;
        }

        public static bool CanMakePayments()
        {
            return GamePotMac_CanMakePayments();
        }

        [DllImport(PLUGIN_NAME)]
        private static extern void GamePotMac_ProcessPendingTransactions();

        public static void ProcessPendingTransactions()
        {
            GamePotMac_ProcessPendingTransactions();
        }

        #endregion

        #region App Status Check

        [DllImport(PLUGIN_NAME)]
        private static extern void GamePotMac_SetAppleId(string appleId);

        [DllImport(PLUGIN_NAME)]
        private static extern int GamePotMac_GetCurrentVersionCode();

        [DllImport(PLUGIN_NAME)]
        private static extern void GamePotMac_ShowAppStatusPopup(string statusJson);

        public static void SetAppleId(string appleId)
        {
            GamePotMac_SetAppleId(appleId);
        }

        public static int GetCurrentVersionCode()
        {
            return GamePotMac_GetCurrentVersionCode();
        }

        public static void ShowAppStatusPopup(string statusJson)
        {
            GamePotMac_ShowAppStatusPopup(statusJson);
        }

        #endregion
    }

    public class GamePotMacCallbackReceiver : MonoBehaviour
    {
        public static GamePotMacCallbackReceiver Instance { get; private set; }

        // Callbacks
        public Action<string> OnAppleSignInSuccessCallback;
        public Action<string> OnAppleSignInFailureCallback;
        
        public Action<string> OnFetchProductsSuccessCallback;
        public Action<string> OnFetchProductsFailureCallback;
        
        public Action<string> OnPurchaseSuccessCallback;
        public Action<string> OnPurchaseFailureCallback;
        public Action<string> OnPurchaseDeferredCallback;
        
        public Action<string> OnRestorePurchaseSuccessCallback;
        public Action<string> OnRestorePurchaseFailureCallback;
        public Action<string> OnRestorePurchasesCompleteCallback;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Called from native plugin
        public void OnAppleSignInSuccess(string json)
        {
            OnAppleSignInSuccessCallback?.Invoke(json);
        }

        public void OnAppleSignInFailure(string json)
        {
            OnAppleSignInFailureCallback?.Invoke(json);
        }

        public void OnFetchProductsSuccess(string json)
        {
            OnFetchProductsSuccessCallback?.Invoke(json);
        }

        public void OnFetchProductsFailure(string json)
        {
            OnFetchProductsFailureCallback?.Invoke(json);
        }

        public void OnPurchaseSuccess(string json)
        {
            OnPurchaseSuccessCallback?.Invoke(json);
        }

        public void OnPurchaseFailure(string json)
        {
            OnPurchaseFailureCallback?.Invoke(json);
        }

        public void OnPurchaseDeferred(string json)
        {
            OnPurchaseDeferredCallback?.Invoke(json);
        }

        public void OnRestorePurchaseSuccess(string json)
        {
            OnRestorePurchaseSuccessCallback?.Invoke(json);
        }

        public void OnRestorePurchaseFailure(string json)
        {
            OnRestorePurchaseFailureCallback?.Invoke(json);
        }

        public void OnRestorePurchasesComplete(string json)
        {
            OnRestorePurchasesCompleteCallback?.Invoke(json);
        }
    }
}
#endif
