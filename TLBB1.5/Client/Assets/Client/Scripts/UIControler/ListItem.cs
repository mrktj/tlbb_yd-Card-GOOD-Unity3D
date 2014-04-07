using UnityEngine;
using System.Collections;

using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;
using Module.Log;

public class ListItem : MonoBehaviour {
	
	public GameObject cardIcon;
	public UILabel heroName;
	public UILabel attackValue;
	public UILabel hpValue;
	public UILabel levelValue;
	
	public UILabel maxLevel;
	public UILabel maxTip;
	public UILabel warningTip;
	public UISprite selectMark;
	
	//private UserCardItem cardItem;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SetUserCardItem(UserCardItem item)
	{
		//cardItem = item;
	}
	
}
