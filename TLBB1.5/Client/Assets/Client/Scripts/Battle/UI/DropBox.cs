using UnityEngine;
using System.Collections;

public class DropBox : MonoBehaviour {
	
	private GameObject battleTopUI;
	
	// Use this for initialization
	void Start () {
		battleTopUI = GameObject.Find("BattleTopUI");
		if(battleTopUI == null)
		{
			Debug.LogError("Can't find BattleTopUI !");
		}
	}
	
	
	public void OnDropCardAnimFinish()
	{
		battleTopUI.GetComponent<BattleTopUI>().AddCard(1);
		//隐藏宝箱--
		//gameObject.transform.localPosition += new Vector3(0, 0, 50);
		Destroy(gameObject, 0.1f);
	}
	public void OnDropItemAnimFinish()
	{
		battleTopUI.GetComponent<BattleTopUI>().AddItem(1);
		//隐藏宝箱--
		//gameObject.transform.localPosition += new Vector3(0, 0, 50);
		Destroy(gameObject, 0.1f);
	}
}
