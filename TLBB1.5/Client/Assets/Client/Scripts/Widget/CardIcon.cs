using UnityEngine;
using System.Collections;

using GCGame.Table;
using Module.Log;
using Games.LogicObject;

public class CardIcon : MonoBehaviour {
	
	public UISprite cardIcon;
	public UISprite iconFrame;
	public UISprite iconBorder;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SetCardTemplateID(int id)
	{
		this.setCard(id);
	}
	
	public void SetCardTemplateID(int id, Color color)
	{
		this.setCard(id);
		//设置颜色
		if(cardIcon != null)
		{
			cardIcon.color = color;
		}
		
	}
	
	private void setCard(int nTempID)
	{
		if(TableManager.GetCardByID(nTempID) == null)
		{
			LogModule.ErrorLog("SetCardTemplateID(),card icon, no such template id: " + nTempID);
			return;
		}
		
		string atlasname = TableManager.GetAppearanceByID(TableManager.GetCardByID(nTempID).Appearance).HeadIcon;
		AtlasManager.Instance.setHeadName(cardIcon,  atlasname);
		
		//iconFrame.spriteName = UserCardItem.iconFrameName[TableManager.GetCardByID(nTempID).Star];
		//iconFrame.transform.localScale=new Vector3(106,106,1);

        //cardIcon.transform.localScale = new Vector3(90,90, 1);
		//iconFrame.MakePixelPerfect();
		
		//------------------------卡牌背景及外框--------------------------------
		//2013-10-12 Jack Wen
		if(iconBorder != null){
			int icon_star = TableManager.GetCardByID(nTempID).Star;
			iconFrame.spriteName = UserCardItem.littleCardFrameName[icon_star];
			iconBorder.spriteName = UserCardItem.littleCardBorderName[icon_star];
		}
		//--------------------------------------------------------------------
	}
	
	
}
