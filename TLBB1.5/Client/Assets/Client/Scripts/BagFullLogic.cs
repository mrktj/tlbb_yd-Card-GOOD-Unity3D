using UnityEngine;
using System.Collections;
using Games.CharacterLogic;
using Games.LogicObject;

public class BagFullLogic : MonoBehaviour {
	
	private GameObject mainLogic = null;
	
	public void goShopWindow ()
	{
		BoxManager.removeMessage();
		if (mainLogic == null)
			mainLogic = GameObject.Find("/MainUILogic");
		mainLogic.SendMessage("OnShopWindow");
		
	}
	
	public void goCardUpdateWindow (){
		BoxManager.removeMessage();
		if (mainLogic == null)
			mainLogic = GameObject.Find("/MainUILogic");
		//Obj_MyselfPlayer.GetMe().updateHeroItem = null;
		//Obj_MyselfPlayer.GetMe().updateMaterialItems = new UserCardItem[6];
		mainLogic.SendMessage("onUpdateWindow");
	}
	
	public void goCardSellWindow () {
		BoxManager.removeMessage();
		if (mainLogic == null)
			mainLogic = GameObject.Find("/MainUILogic");
		mainLogic.SendMessage("OnCardSellWindow");
	}

}
