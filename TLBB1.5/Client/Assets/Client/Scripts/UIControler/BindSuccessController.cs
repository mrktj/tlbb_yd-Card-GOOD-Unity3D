using UnityEngine;
using System.Collections;
using Games.CharacterLogic;

public class BindSuccessController : MonoBehaviour {
	
	public GameObject mainUILogic;
	
	void OnEnable(){
		mainUILogic = GameObject.Find("MainUILogic");
		UILabel account = transform.FindChild("Label").GetComponent<UILabel>();
#if UNITY_ANDROID
		account.text = AndroidConfig.getBindAccount();
#else
		account.text = AccountInfo.Base64Decode(AccountManager.Instance.CurAccount.email);
#endif
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Cancel(){
		if(Obj_MyselfPlayer.GetMe().isPurchase)
		{
			mainUILogic.SendMessage("OnPurchaseWindow");
		}
		else
		{
			mainUILogic.SendMessage("OnHelpWindow");
		}
	}
}
