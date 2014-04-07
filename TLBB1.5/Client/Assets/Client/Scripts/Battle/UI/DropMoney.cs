using UnityEngine;
using System.Collections;

public class DropMoney : MonoBehaviour {
	
	private GameObject battleTopUI;
	private int mMoney;
	public int Money
	{
		set
		{
			mMoney = value;
		}
	}
	// Use this for initialization
	void Start () {
		battleTopUI = GameObject.Find("BattleTopUI");
		if(battleTopUI == null)
		{
			Debug.LogError("Can't find BattleTopUI !");
		}
	}
	
	
	public void OnDropMoneyAnimFinish()
	{
		battleTopUI.GetComponent<BattleTopUI>().AddMoney(mMoney);
		//隐藏--
		//gameObject.transform.localPosition += new Vector3(0, 0, 50);
		Destroy(gameObject, 0.1f);
	}
}
