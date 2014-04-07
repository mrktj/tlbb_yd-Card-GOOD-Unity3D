using UnityEngine;
using System.Collections;

using Games.CharacterLogic;
using Games.LogicObject;

public class ReviveDialog : MonoBehaviour {
	
	public UILabel title;
	public UILabel text_1;
	public UILabel text_2;
	
	public delegate void ReviveHandler(bool yes);
	public ReviveHandler reviveHandler;
	
	// Use this for initialization当前拥有5000个金币	确认花费1000个金币复活吗？--
	//当前拥有金币数量:
	//复活需要金币数量:
	void Start () {
	
	}
	
	
	public void ShowDialog(int money)
	{
		text_1.text = (Obj_MyselfPlayer.GetMe().money).ToString();
		text_2.text = money.ToString();
	}
	
	void OnComfirmBtn()
	{
		//gameObject.SetActive(false);
		reviveHandler(true);
	}
	void OnCancelBtn()
	{
		//gameObject.SetActive(false);
		reviveHandler(false);
	}
	void onBgBtn()
	{
		//Do nothing
	}
}
