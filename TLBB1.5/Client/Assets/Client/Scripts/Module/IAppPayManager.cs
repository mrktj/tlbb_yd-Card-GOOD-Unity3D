using UnityEngine;
using System.Collections;

public class IAppPayManager : MonoBehaviour {

    public delegate void ProductPurchasedFinish(string result);
    public static ProductPurchasedFinish purchaseFinish;
    void Awake()
    {
        // Set the GameObject name to the class name for easy access from Obj-C
        gameObject.name = this.GetType().ToString();
        
    }

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void IAppPayReuslt(string strResult)
    {
        Debug.Log("reveive iapppay result : " + strResult);

        ProductPurchasedFinish temp = purchaseFinish;
        purchaseFinish = null;
        if (null != temp)
        {
            temp(strResult);
        }
        
    }
}
