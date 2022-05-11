using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/11
/// 최종수정일 : 
/// 설명 : 
/// 
/// 인앱결제 관련 구매 / 체크 진행 클래스
/// </summary>
// Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
public class Purchaser : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.
                                                                // General product identifiers for the consumable, non-consumable, and subscription products.
                                                                // Use these handles in the code to reference which product to purchase. Also use these values
                                                                // when defining the Product Identifiers on the store. Except, for illustration purposes, the
                                                                // kProductIDSubscription - it has custom Apple and Google identifiers. We declare their store-
                                                                // specific mapping to Unity Purchasing's AddProduct, below.

    public static string kProductIDRemoveAd = "remove_ads";
    public static string kProductIDMonth = "subscribe_monthly";
    public static string kProductIDYear = "subscribe_yearly";
    // Apple App Store-specific product identifier for the subscription product.
    private static string kProductNameAppleRemoveAd = "remove_ads";
    private static string kProductNameAppleMonth = "subscribe_monthly";
    private static string kProductNameAppleYear = "subscribe_yearly";
    // Google Play Store-specific product identifier subscription product.
    private static string kProductNameGoogleRemoveAd = "remove_ads";
    private static string kProductNameGoogleMonth = "subscribe_monthly";
    private static string kProductNameGoogleYear = "subscribe_yearly";
    // Does the math to see if your apple subscription is past its experation date  
    public static e_Purchased purchasedFlags = e_Purchased.None;
    public static CMDState CMDState = CMDState.BUSY;
    public static Purchaser instance;

    public GameObject internetConnectionWarningPopUp;
    public bool IsSubActive(AppleInAppPurchaseReceipt e)
    {
        if (e.subscriptionExpirationDate > DateTime.Now.ToUniversalTime())
        {
            return true; //HAS_ACTIVE_SUBSCRIPTION
        }
        else
        {
            return false;
        }
    }
    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        // If we haven't set up the Unity Purchasing reference
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            
            StartCoroutine(InitCoroutine());
        }
    }
    IEnumerator InitCoroutine()
    {
        yield return new WaitUntil(() => InternetConnection.IsGoogleWebsiteReachable() == true);
        InitializePurchasing();
        yield return new WaitUntil(() => IsInitialized() == true);
        CMDState = CMDState.IDLE;
    }
    public void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }
        // Create a builder, first passing in a suite of Unity provided stores.
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        // Add a product to sell / restore by way of its identifier, associating the general identifier
        // with its store-specific identifiers.
        // And finish adding the subscription product. Notice this uses store-specific IDs, illustrating
        // if the Product ID was configured differently between Apple and Google stores. Also note that
        // one uses the general kProductIDSubscription handle inside the game - the store-specific IDs
        // must only be referenced here.
        builder.AddProduct(kProductIDRemoveAd,  ProductType.NonConsumable, new IDs() {
            { kProductNameAppleRemoveAd, AppleAppStore.Name  },
            { kProductNameGoogleRemoveAd, GooglePlay.Name },
            });
        builder.AddProduct(kProductIDMonth, ProductType.Subscription, new IDs(){
            { kProductNameAppleMonth, AppleAppStore.Name},
            { kProductNameGoogleMonth, GooglePlay.Name},
            });
        builder.AddProduct(kProductIDYear, ProductType.Subscription, new IDs(){
            { kProductNameAppleYear, AppleAppStore.Name },
            { kProductNameGoogleYear, GooglePlay.Name },
            });
        Debug.Log("add products done");
        // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration
        // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
        UnityPurchasing.Initialize(this, builder);
    }
    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;

    }
    public void BuyRemoveAd()
    {
        BuyProductID(kProductIDRemoveAd);
    }
    public void BuyMonth()
    {
        // Buy the subscription product using its the general identifier. Expect a response either
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        // Notice how we use the general product identifier in spite of this ID being mapped to
        // custom store-specific identifiers above.
        BuyProductID(kProductIDMonth);
    }
    public void BuyYear()
    {
        // Buy the subscription product using its the general identifier. Expect a response either
        // through ProcessPurchase or OnPurchaseFailed asynchronously.
        // Notice how we use the general product identifier in spite of this ID being mapped to
        // custom store-specific identifiers above.
        BuyProductID(kProductIDYear);
    }
    void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);
            // If the look up found a product for this device's store and that product is ready to be sold ...
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or
            // retrying initiailization.
            Instantiate(internetConnectionWarningPopUp);
            Destroy(internetConnectionWarningPopUp, 2f);
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }
    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google.
    // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    public void RestorePurchases()
    {
        // If Purchasing has not yet been set up ...
        if (!IsInitialized())
        {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }
        // If we are running on an Apple device ...
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            // ... begin restoring purchases
            Debug.Log("RestorePurchases started ...");
            // Fetch the Apple store-specific subsystem.
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in
            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions((result) => {
                // The first phase of restoration. If no more responses are received on ProcessPurchase then
                // no purchases are available to be restored.
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        // Otherwise ...
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            internetConnectionWarningPopUp.SetActive(true);
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
        //This can be called anytime after initialization
        //And it should probably be limited to Google Play and not just Android
#if UNITY_ANDROID
        foreach (Product p in controller.products.all)
        {
            if (p.hasReceipt)
            {
                GooglePurchaseData data = new GooglePurchaseData(p.receipt);
                if ((data.json.productId == kProductNameGoogleMonth)|
                    (data.json.productId == kProductNameGoogleYear))
                {
                    purchasedFlags = e_Purchased.Premium;
                }
                else if (data.json.productId == kProductNameGoogleRemoveAd)
                {
                    purchasedFlags = e_Purchased.RemovedAds;
                }
            }
        }
#endif
#if UNITY_IOS
        // Apple will only update your receipt when it is changed, until then you will have
        // to save the last one provided locally ot be able to track the subscription yourself
        string localsave = PlayerPrefs.GetString ("Receipt", null);
        // I store the data locally Obfuscated to make it harder to cheat
        // so I must call the validator to made the Receipt readable.
        // remember you must run the Obfuscater in the Unity IAP window to create the Tangle files
        if(!String.IsNullOrEmpty(localsave))
        {
            var validator = new CrossPlatformValidator(GooglePlayTangle.Data(),
                AppleTangle.Data(), Application.identifier);
            var localResult = validator.Validate (localsave);
            Debug.Log ("Local Receipt: " + localResult);
            foreach (IPurchaseReceipt productReceipt in localResult) {
                Debug.Log ("IsInitialized local data");
                Debug.Log(productReceipt.productID);
                Debug.Log(productReceipt.purchaseDate);
                Debug.Log(productReceipt.transactionID);
                AppleInAppPurchaseReceipt apple = productReceipt as AppleInAppPurchaseReceipt;
                if (null != apple) {
                    Debug.Log ("On Initialized Apple:");
                    Debug.Log("TransactionID: "+apple.originalTransactionIdentifier);
                    Debug.Log("ExpirationDate: "+apple.subscriptionExpirationDate);
                    Debug.Log("Purchase Date: "+apple.purchaseDate);
                    // runs the experation time compared to the current time
                    if (IsSubActive (apple)) {
                        Debug.Log ("Sub is Active");

                        if ((productReceipt.productID == kProductNameAppleMonth)|
                            (productReceipt.productID == kProductNameAppleYear))
                        {
                            purchasedFlags = e_Purchased.Preimum;
                        }
                        else if (productReceipt.productID == kProductNameAppleRemoveAd)
                        {
                            purchasedFlags = e_Purchased.RemovedAds;
                        }
                    } else {
                        Debug.Log ("Sub is NOT Active");
                    }
                }
            }  
        }
#endif
    }
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {

        // Or ... a subscription product has been purchased by this user.
        if (args.purchasedProduct.definition.id == kProductIDMonth)
        {
            purchasedFlags = e_Purchased.Premium;
            Admob_Banner.instance.ToggleBannerView(false);
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            /*PlayerPrefs.SetString("Receipt", args.purchasedProduct.receipt);
            PlayerPrefs.Save();
            // Prepare the validator with the secrets we prepared in the Editor
            // obfuscation window.
            var validator = new CrossPlatformValidator(GooglePlayTangle.Data(),
                AppleTangle.Data(), Application.identifier);
            var result = validator.Validate(args.purchasedProduct.receipt);
            // For informational purposes, we list the receipt(s)
            Debug.Log("Receipt is valid.");
            foreach (IPurchaseReceipt productReceipt in result)
            {
                Debug.Log(productReceipt.productID);
                Debug.Log(productReceipt.purchaseDate);
                Debug.Log(productReceipt.transactionID);
                GooglePlayReceipt google = productReceipt as GooglePlayReceipt;
                if (null != google)
                {
                    Debug.Log("Google");
                    Debug.Log(google.ToString());
                    Debug.Log(google.purchaseToken);
                }
                AppleInAppPurchaseReceipt apple = productReceipt as AppleInAppPurchaseReceipt;
                if (null != apple)
                {
                    Debug.Log("ProcessPurchase Apple:");
                    Debug.Log(apple.originalTransactionIdentifier);
                    Debug.Log(apple.subscriptionExpirationDate);
                    if (IsSubActive(apple))
                    {
                        Debug.Log("Sub is Active");
                    }
                    else
                    {
                        Debug.Log("Sub is NOT Active");
                    }
                }
            }*/
        }
        // Or ... a Year subscription product has been purchased by this user.
        else if (args.purchasedProduct.definition.id == kProductIDYear)
        {
            purchasedFlags = e_Purchased.Premium;
            Admob_Banner.instance.ToggleBannerView(false);
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            /*PlayerPrefs.SetString("Receipt", args.purchasedProduct.receipt);
            PlayerPrefs.Save();
            // Prepare the validator with the secrets we prepared in the Editor
            // obfuscation window.
            var validator = new CrossPlatformValidator(GooglePlayTangle.Data(),
                AppleTangle.Data(), Application.identifier);
            var result = validator.Validate(args.purchasedProduct.receipt);
            // For informational purposes, we list the receipt(s)
            Debug.Log("Receipt is valid.");
            foreach (IPurchaseReceipt productReceipt in result)
            {
                Debug.Log(productReceipt.productID);
                Debug.Log(productReceipt.purchaseDate);
                Debug.Log(productReceipt.transactionID);
                GooglePlayReceipt google = productReceipt as GooglePlayReceipt;
                if (null != google)
                {
                    Debug.Log("Google");
                    Debug.Log(google.ToString());
                    Debug.Log(google.purchaseToken);
                }
                AppleInAppPurchaseReceipt apple = productReceipt as AppleInAppPurchaseReceipt;
                if (null != apple)
                {
                    Debug.Log("ProcessPurchase Apple:");
                    Debug.Log(apple.originalTransactionIdentifier);
                    Debug.Log(apple.subscriptionExpirationDate);
                    if (IsSubActive(apple))
                    {
                        Debug.Log("Sub is Active");
                    }
                    else
                    {
                        Debug.Log("Sub is NOT Active");
                    }
                }
            }*/
        }
        else if(args.purchasedProduct.definition.id == kProductIDRemoveAd)
        {
            purchasedFlags = e_Purchased.RemovedAds;
            Admob_Banner.instance.ToggleBannerView(false);
        }
        // Return a flag indicating whether this product has completely been received, or if the application needs
        // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still
        // saving purchased products to the cloud, and when that save is delayed.
        return PurchaseProcessingResult.Complete;
    }
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing
        // this reason with the user to guide their troubleshooting actions.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }

    public bool HadPurchased(string productId)
    {
        if (!IsInitialized()) return false;

        var product = m_StoreController.products.WithID(productId);

        if (product != null)
        {
            return product.hasReceipt;
        }
        return false;
    }
    
    public void RefreshPurchaseFlag()
    {
        if(IsInitialized() == false)
        {
            internetConnectionWarningPopUp.SetActive(true);
            return;
        }
            
#if UNITY_ANDROID
        foreach (Product p in m_StoreController.products.all)
        {
            if (p.hasReceipt)
            {
                GooglePurchaseData data = new GooglePurchaseData(p.receipt);
                if ((data.json.productId == kProductNameGoogleMonth) |
                    (data.json.productId == kProductNameGoogleYear))
                {
                    purchasedFlags = e_Purchased.Premium;
                    break;
                }
                else if (data.json.productId == kProductNameGoogleRemoveAd)
                {
                    purchasedFlags = e_Purchased.RemovedAds;
                }
            }
        }
#endif
    }
    public e_Purchased GetPurchasedFlag()
    {
        RefreshPurchaseFlag();
        return purchasedFlags;
    }
    public bool IsAdRemoved()
    {
        bool tmpIsit = false;
        e_Purchased tmpFlag = GetPurchasedFlag();
        if((tmpFlag == e_Purchased.Premium) |
            (tmpFlag == e_Purchased.RemovedAds))
        {
            tmpIsit = true;
        }
        return tmpIsit;
    }
}

public enum e_Purchased
{
    None = 0,
    RemovedAds = 1,
    Premium = 2,
}
