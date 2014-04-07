using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using card;
public class IAPHelper : MonoBehaviour {

	// Use this for initialization

    public enum FailType
    {
        CANCEL,
        FETCH_PRODUCTLIST_FAIL,
        PRODUCTLIST_EMPTY,
        PRODUCT_NOT_EXSIT,
        BUYFAIL,
        CANNOTBUY,
    }
	public class ProductInfo
	{
		public string pid;
        public int count;
        public string receipt;
        public string tid;
	};
	
	public string CurErrorInfo = "";
	public delegate void DelFinish(bool bSuccess);
	private DelFinish m_delFinish;
	public static ProductInfo CurProduct = new ProductInfo();
	
	private static IAPHelper m_instance;
	public static IAPHelper Instance()
	{
		return m_instance;
	}
	public static bool isBuying = false; 
	public void Buy(string pid, DelFinish onFinish)
	{

		isBuying = true;
		CurProduct.pid = pid;
		m_delFinish = onFinish;
#if UNITY_IPHONE
		if(StoreKitBinding.canMakePayments())
		{
			string[] strRequests = {pid};
			StoreKitBinding.requestProductData(strRequests);
		}
		else
		{
			BuyFail(FailType.CANNOTBUY, "");
			Debug.Log("can't purchase");
		}
#else
        if (null != m_delFinish) m_delFinish(false);
#endif 
	}

    public void FinishPendingTrans()
    {
#if UNITY_IPHONE
        StoreKitBinding.finishPendingTransactions();
#endif
    }
    void BuyFail(FailType type, string moreInfo)
	{
        switch (type)
        {
            case FailType.BUYFAIL:
                //CurErrorInfo = Utils.DicDesc(1002) + moreInfo;
                break;
            case FailType.FETCH_PRODUCTLIST_FAIL:
                //CurErrorInfo = Utils.DicDesc(1003);
                break;
            case FailType.PRODUCT_NOT_EXSIT:
                //CurErrorInfo = Utils.DicDesc(1004) + moreInfo;
                break;
            case FailType.CANCEL:
                //CurErrorInfo = Utils.DicDesc(1000);
                break;
            case FailType.CANNOTBUY:
                //CurErrorInfo = Utils.DicDesc(1005);
                break;
            case FailType.PRODUCTLIST_EMPTY:
                break;
            default:
                CurErrorInfo = moreInfo;
                break;
        }
        Debug.Log("buyFail");
        if (null != m_delFinish) m_delFinish(false);
	
	}

    void Update()
    {
        PurchaseHelper.Instance().Update(UnityEngine.Time.deltaTime);
    }
	void Start()
	{
// 		if(m_instance != null)
// 		{
// 			Destroy(gameObject);
// 			return;
// 		}
        m_instance = this;
#if UNITY_IPHONE
    
		// Listens to all the StoreKit events.  All event listeners MUST be removed before this object is disposed!
		
		StoreKitManager.productPurchaseAwaitingConfirmationEvent += productPurchaseAwaitingConfirmationEvent;
		StoreKitManager.purchaseSuccessfulEvent += purchaseSuccessful;
		StoreKitManager.purchaseCancelledEvent += purchaseCancelled;
		StoreKitManager.purchaseFailedEvent += purchaseFailed;
		StoreKitManager.productListReceivedEvent += productListReceivedEvent;
		StoreKitManager.productListRequestFailedEvent += productListRequestFailed;
		StoreKitManager.restoreTransactionsFailedEvent += restoreTransactionsFailed;
		StoreKitManager.restoreTransactionsFinishedEvent += restoreTransactionsFinished;
		StoreKitManager.paymentQueueUpdatedDownloadsEvent += paymentQueueUpdatedDownloadsEvent;
		/*
		StoreKitManager.purchaseSuccessful += purchaseSuccessful;
		StoreKitManager.purchaseCancelled += purchaseCancelled;
		StoreKitManager.purchaseFailed += purchaseFailed;
		StoreKitManager.receiptValidationFailed += receiptValidationFailed;
		StoreKitManager.receiptValidationRawResponseReceived += receiptValidationRawResponseReceived;
		StoreKitManager.receiptValidationSuccessful += receiptValidationSuccessful;
		StoreKitManager.productListReceived += productListReceived;
		StoreKitManager.productListRequestFailed += productListRequestFailed;
		StoreKitManager.restoreTransactionsFailed += restoreTransactionsFailed;
		StoreKitManager.restoreTransactionsFinished += restoreTransactionsFinished;restoreTransactionsFinished
		*/
		
#endif
	}
	
	#if UNITY_IPHONE
	void OnDisable()
	{
		StoreKitManager.productPurchaseAwaitingConfirmationEvent -= productPurchaseAwaitingConfirmationEvent;
		StoreKitManager.purchaseSuccessfulEvent -= purchaseSuccessful;
		StoreKitManager.purchaseCancelledEvent -= purchaseCancelled;
		StoreKitManager.purchaseFailedEvent -= purchaseFailed;
		StoreKitManager.productListReceivedEvent -= productListReceivedEvent;
		StoreKitManager.productListRequestFailedEvent -= productListRequestFailed;
		StoreKitManager.restoreTransactionsFailedEvent -= restoreTransactionsFailed;
		StoreKitManager.restoreTransactionsFinishedEvent -= restoreTransactionsFinished;
		StoreKitManager.paymentQueueUpdatedDownloadsEvent -= paymentQueueUpdatedDownloadsEvent;
		/*
		// Remove all the event handlers
		StoreKitManager.purchaseSuccessful -= purchaseSuccessful;
		StoreKitManager.purchaseCancelled -= purchaseCancelled;
		StoreKitManager.purchaseFailed -= purchaseFailed;
		StoreKitManager.receiptValidationFailed -= receiptValidationFailed;
		StoreKitManager.receiptValidationRawResponseReceived -= receiptValidationRawResponseReceived;
		StoreKitManager.receiptValidationSuccessful -= receiptValidationSuccessful;
		StoreKitManager.productListReceived -= productListReceived;
		StoreKitManager.productListRequestFailed -= productListRequestFailed;
		StoreKitManager.restoreTransactionsFailed -= restoreTransactionsFailed;
		StoreKitManager.restoreTransactionsFinished -= restoreTransactionsFinished;
		*/
	}
	
	void productListReceivedEvent( List<StoreKitProduct> productList )
	{
		Debug.Log( "productListReceivedEvent. total products received: " + productList.Count );
		
		// print the products to the console
		foreach( StoreKitProduct product in productList )
			Debug.Log( product.ToString() + "\n" );
		
		Debug.Log( "total productsReceived: " + productList.Count );
		
		// Do something more useful with the products than printing them to the console
		foreach( StoreKitProduct product in productList )
		{
			if(product.productIdentifier == CurProduct.pid)
			{
				if(isBuying)//;// = true;
				{
					Debug.Log("FindProduct" + CurProduct.pid);
					StoreKitBinding.purchaseProduct(CurProduct.pid, 1);
					isBuying = false;
				}
				return;
				
			}
			Debug.Log( product.ToString() + "\n" );
		}

         BuyFail(IAPHelper.FailType.PRODUCT_NOT_EXSIT, "");
	}
	
	
	void productListRequestFailed( string error )
	{
		Debug.Log( "productListRequestFailed: " + error );
		BuyFail(IAPHelper.FailType.FETCH_PRODUCTLIST_FAIL, error);
	}
	

	void purchaseFailed( string error )
	{
		Debug.Log( "purchase failed with error: " + error );
        BuyFail(IAPHelper.FailType.BUYFAIL, error);
	}
	

	void purchaseCancelled( string error )
	{
		Debug.Log( "purchase cancelled with error: " + error );
        BuyFail(IAPHelper.FailType.CANCEL, error);
	}
	
	
	void productPurchaseAwaitingConfirmationEvent( StoreKitTransaction transaction )
	{
		Debug.Log( "productPurchaseAwaitingConfirmationEvent: " + transaction );
	}
	
	
	void purchaseSuccessful( StoreKitTransaction transaction )
	{
		Debug.Log( "purchased product: " + transaction );
		
		Debug.Log( "purchased product: " + transaction );
		Debug.Log("buy success");

		CurProduct.tid = transaction.transactionIdentifier;
        CurProduct.count = transaction.quantity;
        CurProduct.pid = transaction.productIdentifier;
        CurProduct.receipt = transaction.base64EncodedTransactionReceipt;
		if(null != m_delFinish) m_delFinish(true);
	}
	
	
	void restoreTransactionsFailed( string error )
	{
		Debug.Log( "restoreTransactionsFailed: " + error );
	}
	
	
	void restoreTransactionsFinished()
	{
		Debug.Log( "restoreTransactionsFinished" );
	}
	
	
	void paymentQueueUpdatedDownloadsEvent( List<StoreKitDownload> downloads )
	{
		Debug.Log( "paymentQueueUpdatedDownloadsEvent: " );
		foreach( var dl in downloads )
			Debug.Log( dl );
	}
	
#endif
}

