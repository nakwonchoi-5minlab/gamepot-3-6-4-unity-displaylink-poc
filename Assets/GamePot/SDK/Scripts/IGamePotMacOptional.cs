#if UNITY_STANDALONE_OSX
public interface IGamePotMacOptional
{
    void onFetchProductsSuccess(string result);
    void onFetchProductsFailure(string result);

    void onPurchaseDeferred(string result);

    void onRestorePurchaseSuccess(string result);
    void onRestorePurchaseFailure(string result);
    void onRestorePurchasesComplete(string result);
}
#endif
