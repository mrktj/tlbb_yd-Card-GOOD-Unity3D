using UnityEngine;
using System.Collections;
using card.net;
using Games.CharacterLogic;
using Games.LogicObject;

public class PPHelper : MonoBehaviour {
	
	public delegate void ProductPurchasedFinish(string result);
    public static ProductPurchasedFinish purchaseFinish;
	
	public static string LoginTokenKey = "";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void GetTokenKey(string strTokenKey)
	{
		LoginTokenKey = strTokenKey;
	}
	
	public void PPLogout()
	{
		LoginTokenKey = "";
		HTTPClientAPI.cleanSessionId();
		//reset login当前菜单为splahcontroller
		LoginLogic.needResetLogin = true;
		MainUILogic.needResetLogin = true;
		//清除临时切换数据
		//update
		//Obj_MyselfPlayer.GetMe().updateHeroItem = null;
		//Obj_MyselfPlayer.GetMe().updateMaterialItems = new UserCardItem[6];
		//evolution
		Obj_MyselfPlayer.GetMe().evolutionHeroItem = null;
		Obj_MyselfPlayer.GetMe().evolutionMaterialItems = new UserCardItem[5];
		//strengthen
		Obj_MyselfPlayer.GetMe().strengthenHeroItem = null;
		
		Obj_MyselfPlayer.ReleaseMe();
		
		//清空新手引导状态
		GuideManager.Instance.guideTimeOut();
		  //NetworkSender.Instance().Login(LoginDone, AccountManager.Instance.GetLoginAccountID());
		//回到主菜单
		GameManager.Instance.LoadLevel(Utils.UI_NAME_Login);
	}
	
	public void PPPayResult(string result)
    {
        Debug.Log("reveive pppay result : " + result);

        ProductPurchasedFinish temp = purchaseFinish;
        purchaseFinish = null;
        if (null != temp)
        {
            temp(result);
        }
        
    }
	
}
