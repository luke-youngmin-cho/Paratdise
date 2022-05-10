using UnityEngine;
/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/11
/// 최종수정일 : 
/// 설명 : 
/// 
/// 구글 결제 관련데이터
/// </summary>
class GooglePurchaseData
{
    // INAPP_PURCHASE_DATA
    public string inAppPurchaseData;
    // INAPP_DATA_SIGNATURE
    public string inAppDataSignature;

    public GooglePurchaseJson json;

    [System.Serializable]
    private struct GooglePurchaseReceipt
    {
        public string Payload;
    }

    [System.Serializable]
    private struct GooglePurchasePayload
    {
        public string json;
        public string signature;
    }

    [System.Serializable]
    public struct GooglePurchaseJson
    {
        public string autoRenewing;
        public string orderId;
        public string packageName;
        public string productId;
        public string purchaseTime;
        public string purchaseState;
        public string developerPayload;
        public string purchaseToken;
    }

    public GooglePurchaseData(string receipt)
    {
        try
        {
            var purchaseReceipt = JsonUtility.FromJson<GooglePurchaseReceipt>(receipt);
            var purchasePayload = JsonUtility.FromJson<GooglePurchasePayload>(purchaseReceipt.Payload);
            var inAppJsonData = JsonUtility.FromJson<GooglePurchaseJson>(purchasePayload.json);

            inAppPurchaseData = purchasePayload.json;
            inAppDataSignature = purchasePayload.signature;
            json = inAppJsonData;
        }
        catch
        {
            Debug.Log("Could not parse receipt: " + receipt);
            inAppPurchaseData = "";
            inAppDataSignature = "";
        }
    }
}