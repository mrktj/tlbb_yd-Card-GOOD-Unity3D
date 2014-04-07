using UnityEngine;
using System.Collections;

public class HelpController : MonoBehaviour {
	
	private GameObject mainLogic;
	
	// Use this for initialization
	void Start () {
		mainLogic = GameObject.Find("MainUILogic");
	}
	
	public void OnHeroInfoWindow()
	{
		mainLogic.SendMessage("OnHeroInfoWindow");
	}
}
