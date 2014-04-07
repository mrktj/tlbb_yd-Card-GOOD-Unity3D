//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//
//using Games.CharacterLogic;
//using Games.LogicObject;
//using GCGame.Table;
//
//public class UpdateController : MonoBehaviour {
//	
//	public GameObject labelManager;
//	public GameObject labelNumManager;
//	public GameObject labelMaterialManager;
//	
//	public GameObject labelLevel;	
//	public GameObject labelSkill;
//	public GameObject labelSkillChance;	
//	public GameObject labelHSkill;
//	public GameObject labelHSkillChance;	
//	public GameObject labelAttack;
//	public GameObject labelHp;	
//	public GameObject labelMoney;
//	public GameObject labelExp;
//	
//	public GameObject updateHeroBtn;
//	public GameObject[] materialHeroBtn;
//	
//	private UserCardItem updateHero;
//	private UserCardItem[] materialHero;
//	private int materialNum;
//	private string[] element;
////	private int money;
////	private int exp;
////	private float chance;
//	
//	void Awake()
//	{
//		materialNum = 0;
//		materialHero = new UserCardItem[6];
//		labelManager.SetActive(false);
//		labelNumManager.SetActive(false);
//		labelMaterialManager.SetActive(false);
//		if(SelectHeroController.materialList == null)
//		SelectHeroController.materialList = new List<UserCardItem>();
//		
//		element = new string[5];
//		element[0] = "card_nature_jin";
//		element[1] = "card_nature_mu";
//		element[2] = "card_nature_shui";
//		element[3] = "card_nature_huo";
//		element[4] = "card_nature_tu";
//	}
//	// Use this for initialization
//	void Start () {
//	
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		
//	}
//	
//	void OnDisable()
//	{
//	}
//	
//	void OnEnable()
//	{	
//		DisableMaterial();
//		materialNum = SelectHeroController.materialList.Count;
//		if(materialNum > 0 && materialNum <= 6)
//		{
//			for(int i=0; i < materialNum; i++)
//			{
//				materialHero[i] = SelectHeroController.materialList[i];
//			}
//			int money = updateHero.level * 100 * materialNum;
//			labelMoney.transform.GetComponent<UILabel>().text = money.ToString();
//			
//			labelMaterialManager.SetActive(true);
//			transform.FindChild("UpdateBtn").GetComponent<UIButton>().isEnabled = true;
//			transform.FindChild("UpdateBtn").GetComponent<TweenAlpha>().enabled = true;
//		}
//		else if(materialNum == 0)
//		{
//			//all material's head icon is disabled
//			labelMaterialManager.SetActive(false);
//			transform.FindChild("UpdateBtn").GetComponent<UIButton>().isEnabled = false;
//			transform.FindChild("UpdateBtn").GetComponent<TweenAlpha>().enabled = false;
//		}
//		FreashMaterialUI();
//	}
//	
//	/* 
//	 * 英雄升级按钮
//	 * 具体功能待加入
//	 * */
//	public void HeroUpdate()
//	{
//		//update
//		
//		materialNum = 0;
//		
//		//clean hero data
//	}
//	
//	void SetUpdateLabel(UserCardItem card)
//	{
//		UISprite icon = updateHeroBtn.transform.FindChild("Card").GetComponent<UISprite>();
//		icon.spriteName = TableManager.GetAppearanceByID(TableManager.GetCardByID(card.templateID).Appearance).BodyIcon;
//		icon.MakePixelPerfect();
//		
//		UISprite nature = updateHeroBtn.transform.FindChild("Category").GetComponent<UISprite>();
//		nature.spriteName = element[ TableManager.GetCardByID(card.templateID).Element ];
//		nature.MakePixelPerfect();
//		
//		labelLevel.transform.GetComponent<UILabel>().text = card.level.ToString();
//		labelAttack.transform.GetComponent<UILabel>().text = (TableManager.GetCardByID(card.templateID).AttackBase + TableManager.GetCardByID(card.templateID).AttackGrow * card.level).ToString();
//		labelHp.transform.GetComponent<UILabel>().text = (TableManager.GetCardByID(card.templateID).HpBase + TableManager.GetCardByID(card.templateID).HpGrow * card.level).ToString();		
//	/*
//		labelSkill.transform.GetComponent<UILabel>().text;
//		labelSkillChance;
//	
//		labelHSkill;
//		labelHSkillChance;
//	*/
//	}
//	
//	void FreashMaterialUI()
//	{
//		if(materialNum <= 0)
//			return;
//		for(int i=0; i<materialNum; i++)
//		{
//			UISprite icon = materialHeroBtn[i].transform.FindChild("Sprite-Icon").GetComponent<UISprite>();
//			icon.spriteName = TableManager.GetAppearanceByID(TableManager.GetCardByID(materialHero[i].templateID).Appearance).HeadIcon;
//			materialHeroBtn[i].transform.FindChild("Sprite-Icon").gameObject.SetActive(true);
//		}
//	}
//	
//	void DisableMaterial()
//	{
//		for(int i=0; i < materialHeroBtn.Length; i++)
//		{
//			materialHeroBtn[i].transform.FindChild("Sprite-Icon").gameObject.SetActive(false);
//		}
//	}
//	
//	public void SelectUpdateHero(GameObject go)
//	{
//		
//		foreach(UserCardItem temp in Obj_MyselfPlayer.GetMe().cardBagList)
//		{
//			if(temp.cardID == long.Parse(go.name))
//			{
//				updateHero = temp;
//				break;
//			}
//		}
//		SelectHeroController.updateHero = updateHero;
//		materialNum = 0;
//		labelMaterialManager.SetActive(false);
//		
//		SetUpdateLabel(updateHero);
//		
//		labelManager.SetActive(true);
//		labelNumManager.SetActive(true);
//		
//		for(int i=0; i<materialHeroBtn.Length; i++)
//		{
//			materialHeroBtn[i].transform.GetComponent<UIButton>().isEnabled = true;
//			materialHeroBtn[i].transform.FindChild("Sprite-Icon").gameObject.SetActive(false);
//		}
//	}	
//}
