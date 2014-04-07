using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Games.CharacterLogic;
using Games.LogicObject;

public class HeroList : MonoBehaviour {
			
	public GameObject heroListItem;
	public GameObject heroListPanel;
	public GameObject Grid;
	public GameObject cardIconPrefab;
	
	public List<HeroTableItem> heroItemList; 
	void Awake(){
        CreateList();
	}
	// Use this for initialization
	void Start () {
		Grid.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void CreateList()
	{
		GameObject parent = Grid;
		heroItemList = new List<HeroTableItem>();
		//List<UserCardItem> cardList = Obj_MyselfPlayer.GetMe().cardList;
//		int i = 0;
//		foreach(UserCardItem cardItem in Obj_MyselfPlayer.GetMe().cardMap)
//		{
//			HeroTableItem heroItem = new HeroTableItem(cardItem.cardID, cardItem.level);
//			
//			GameObject newItem =  (GameObject)Instantiate(heroListItem);
//			newItem.transform.parent = parent.transform;
//			newItem.transform.position = new Vector3(0, 0, 0);
//			newItem.transform.localScale = new Vector3(1, 1, 1);
//			
//			Transform tf = newItem.transform.FindChild("Label-Name");
//			UILabel name = tf.GetComponent<UILabel>();
//			name.text = heroItem.name;
//			
//			tf = newItem.transform.FindChild("Label-Level-Value");
//			if(tf != null)
//			{
//				UILabel level =tf.GetComponent<UILabel>();
//				level.text = heroItem.level.ToString();
//			}
//			
//			tf = newItem.transform.FindChild("Label-Hp-Value");
//			if(tf != null)
//			{
//				UILabel hp = tf.GetComponent<UILabel>();
//				hp.text = heroItem.hp.ToString();
//			}
//			
//			tf = newItem.transform.FindChild("Label-Attack-Value");
//			if(tf != null)
//			{
//				UILabel attack = tf.GetComponent<UILabel>();
//				attack.text = heroItem.attack.ToString();
//			}
//
//			UISprite locked = newItem.transform.FindChild("Sprite-Lock").GetComponent<UISprite>();
//			if(heroItem.locked)
//			{
//				//locked.GetAtlasSprite().name = "";
//			}
//			else
//			{
//				//locked.GetAtlasSprite().name = "";
//			}
//			
//			UIButton cardIcon = newItem.transform.FindChild("CardIcon/Button-CardIcon").GetComponent<UIButton>();
//			UISprite frame = cardIcon.transform.FindChild("Sprite-Frame").GetComponent<UISprite>();
//			frame.spriteName = heroItem.cardIcon.frame;
//
//			UISprite icon = cardIcon.transform.FindChild("Sprite-Icon").GetComponent<UISprite>();
//			icon.name = heroItem.cardIcon.icon;
//			
//			heroItemList.Add(heroItem);
//			i++;
//		}
//         UIButtonMessage loadmsg = newItem.GetComponent<UIButtonMessage>();
//         loadmsg.target = loadtarget;
//         loadmsg.functionName = "LoadBattleUIScene";
		parent.GetComponent<UIGrid>().repositionNow = true;
	}
	
}
