using UnityEngine;
using System.Collections;

public class PvPTotalController : MonoBehaviour {
	
	private MainUILogic mainLogic;
	
	// Use this for initialization
	void Start () {
		 mainLogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//显示pvp
	private void OnProcessShowPvP()
	{
		if(mainLogic != null)
		{
			mainLogic.OnPVPWindow();
		}
	}
	
	//显示群雄争霸pvp
	private void OnProcessShowQxzbPvP()
	{
		if(mainLogic != null)
		{
			mainLogic.OnQxzbPvPWindow();
		}
	}
	
	public void ReturnToMainUI()
	{
		if(mainLogic != null)
		{
			mainLogic.gameObject.SendMessage("ReturnToMainUI");
		}
		
	}
}
