using UnityEngine;
using System.Collections;

using GCGame.Table;
using Module.Log;
using Games.LogicObject;

public class CardLarge : MonoBehaviour {
	
	public UITexture cardIcon;
	public UISprite cardFrame;
	public UISprite category;
	public UISprite catFrame;
	public UISprite nameBg;
	public UISprite cardBoard;
	public UILabel nameLabel;
	
	private int cardTempID;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SetCardTemplateID(int id)
	{
		cardTempID = id;
		Tab_Card cardTable = TableManager.GetCardByID(cardTempID);
		if(cardTable == null)
		{
			LogModule.ErrorLog("SetCardTemplateID(), no such template id: " + id);
			return;
		}
		
//		string atlasname = TableManager.GetAppearanceByID(TableManager.GetCardByID(cardTempID).Appearance).BodyIcon;
		
		Tab_Appearance apperanceTab = TableManager.GetAppearanceByID(cardTable.Appearance);
		if(apperanceTab != null 
			    && nameLabel != null)
		{
			nameLabel.text = LanguageManger.GetWords(apperanceTab.Name);;
		}
		
		if(nameBg != null)
		{
			nameBg.spriteName = UserCardItem.largeCardNameBg[cardTable.Star];
			nameBg.MakePixelPerfect();
		}
		
		if(cardBoard != null)
		{
			cardBoard.spriteName = UserCardItem.largeCardBorderName[cardTable.Star];
			cardBoard.MakePixelPerfect();
		}
		
		AtlasManager.Instance.setBodyByTempletID(cardIcon,cardTempID);//.SetBodyName(cardIcon, atlasname);
		cardFrame.spriteName = UserCardItem.cardFrameName[cardTable.Star];
		cardFrame.MakePixelPerfect();
		
		//catFrame.spriteName = UserCardItem.elementFrameName[TableManager.GetCardByID(id).Star];
		//catFrame.MakePixelPerfect();
		
		category.spriteName = UserCardItem.elementTypeName[cardTable.Element];
		category.MakePixelPerfect();
	}
}
