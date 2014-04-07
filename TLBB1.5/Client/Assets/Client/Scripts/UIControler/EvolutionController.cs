using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;
using card;
using card.net;
using Module.Log;

public class EvolutionController : MonoBehaviour {
	
	public GameObject evolutionBefore;
	public GameObject evolutionAfter;
	public GameObject[] evolutionMaterial;
	public GameObject[] downAnim;
	public UILabel[] materialNum;
	public GameObject leftAnim;
	public GameObject rightAnim;
	public GameObject evolutionBtn;
	public GameObject heroBg;
	public GameObject targetBg;
	public GameObject materialIcons;
	private GameObject mainUILogic;
	
	public GameObject heroInfo;
	public UILabel heroLevel;
	public UILabel heroHp;
	public UILabel heroAttack;
	public UILabel heroSkillName;
	public UILabel heroSkillLevel;
    public UILabel evolutionCost;       //--œø»¯ËùÐèœð±Ò
	public UILabel heroHpGrow;
	public UILabel heroAttackGrow;
	
	public GameObject tgtInfo;
    public GameObject evolutionBtnSpLabel;   //--œø»¯°ŽÅ¥ÉÏµÄÎÄ×ÖÍŒÆ¬
	public UILabel tgtLevel;
	public UILabel tgtHp;
	public UILabel tgtAttack;
	public UILabel tgtSkillName;
	public UILabel tgtSkillLevel;
	public UILabel tgtHpGrow;
	public UILabel tgtAttackGrow;
	
	private UserCardItem heroCardItem = null;
    private UserCardItem targetCardItem = null;  //œø»¯ºóµÄ¿šÅÆ
	private UserCardItem[] materialCardItems = new UserCardItem[5];	
	private int[] matIDArray = new int[5];  //²ÄÁÏ¿šÆ¬ID
	private bool isCanEvo = false;
	private bool isMatEnough = false;
	private bool completeEvo=false;
	private bool bInAnimation = false; // 判断现在是否处于动画播放中
	
	void Awake()
	{

	}
	// Use this for initialization
	void Start () {
		
		/*
		for(int i = 0; i < 5; i++)
		{
			materialCardItems[i] = null;
		}
		*/
		//FreshUI();
	}
	
	// Update is called once per frame
	void Update () {	
		if(completeEvo)
		{
			if(downAnim[0].GetComponent<UISpriteAnimation>().enabled&&!downAnim[0].GetComponent<UISpriteAnimation>().isPlaying)
			{
				for(int i = 0; i < 5; i++)
				{
					downAnim[i].SetActive(false);
					downAnim[i].GetComponent<UISpriteAnimation>().enabled=false;
					downAnim[i].GetComponent<UISpriteAnimation>().Reset();
				}
				leftAnim.SetActive(true);
				leftAnim.GetComponent<UISpriteAnimation>().enabled=true;
			}
			else if(leftAnim.GetComponent<UISpriteAnimation>().enabled&&!leftAnim.GetComponent<UISpriteAnimation>().isPlaying)
			{
				leftAnim.SetActive(false);
				leftAnim.GetComponent<UISpriteAnimation>().enabled=false;
				leftAnim.GetComponent<UISpriteAnimation>().Reset();
				
				rightAnim.SetActive(true);
				rightAnim.GetComponent<UISpriteAnimation>().enabled=true;
			}
			else if(rightAnim.GetComponent<UISpriteAnimation>().enabled&&!rightAnim.GetComponent<UISpriteAnimation>().isPlaying)
			{
				rightAnim.SetActive(false);
				rightAnim.GetComponent<UISpriteAnimation>().enabled=false;
				rightAnim.GetComponent<UISpriteAnimation>().Reset();
			}
			
			if(!leftAnim.GetComponent<UISpriteAnimation>().enabled
				&&!downAnim[0].GetComponent<UISpriteAnimation>().enabled
				   &&!rightAnim.GetComponent<UISpriteAnimation>().enabled)
			{
				bInAnimation = false;
				AfterEvolution();
				RefreshTopBar();
				completeEvo=false;
			}
		}
	}
	
	void OnEnable()
	{
		//fresh base UI
		if(mainUILogic == null)
		{
			mainUILogic = GameObject.Find("MainUILogic");
		}
		
		bInAnimation = false;
		heroCardItem = Obj_MyselfPlayer.GetMe().evolutionHeroItem;
		Calculate();
		FreshUI();
		
		GameObject mainControl = GameObject.FindWithTag("main_controller");
		if(mainControl != null)
		{
			mainControl.SendMessage("ShowStaticNotice", 4001);//固定显示提示信息
		}
	}
	
	void OnDisable()
	{
		ResetEffect();
		
		GameObject mainControl = GameObject.FindWithTag("main_controller");
		if(mainControl != null)
		{
			mainControl.SendMessage("NotShowStaticNotice");//取消公告固定显示
		}
	}
	
	// 刷新效果
	void ResetEffect()
	{
		completeEvo = false;
		leftAnim.SetActive(false);
		leftAnim.GetComponent<UISpriteAnimation>().enabled=false;
		leftAnim.GetComponent<UISpriteAnimation>().Reset();
		
		//targetBg.transform.Find("spWaifaguang").gameObject.SetActive(false);
		rightAnim.SetActive(false);
		rightAnim.GetComponent<UISpriteAnimation>().enabled=false;
		rightAnim.GetComponent<UISpriteAnimation>().Reset();
		
		for(int i = 0; i < 5; i++)
		{
			downAnim[i].SetActive(false);
			downAnim[i].GetComponent<UISpriteAnimation>().enabled=false;
			downAnim[i].GetComponent<UISpriteAnimation>().Reset();
		}
	}
	
	void FreshUI()
	{
		if(heroCardItem != null)
		{
			int next = TableManager.GetCardByID(heroCardItem.templateID).NextCard;
			if(TableManager.GetCardByID(next) == null)
			{
				next = heroCardItem.templateID;
			}
			evolutionBefore.gameObject.SetActive(true);
			evolutionAfter.gameObject.SetActive(true);

			evolutionBefore.GetComponent<CardLarge>().SetCardTemplateID(heroCardItem.templateID);
			evolutionAfter.GetComponent<CardLarge>().SetCardTemplateID(next);
            evolutionAfter.name = next.ToString();  //-----žøÍŒ±êž¶ÏàÓŠµÄ TempleID
			
			UserCardItem tgtCardItem = new UserCardItem();
			tgtCardItem.templateID = next;
			tgtCardItem.level = TableManager.GetCardByID(next).LevelBase;
			//tgtCardItem.skillLevel = TableManager.GetSkillByID(TableManager.GetCardByID(next).SkillVol).SkillLevel;
            tgtCardItem.skillLevel = heroCardItem.skillLevel;        //Ÿ«œø¿šÅÆºóµÄŒŒÄÜµÈŒ¶ºÍÇ¿»¯¶ŒÒª±£Áô
			tgtCardItem.addQualityAtt = heroCardItem.addQualityAtt;
			tgtCardItem.addQualityHp = heroCardItem.addQualityHp;
            targetCardItem = tgtCardItem;
			//œø»¯Ç°µÄÐÅÏ¢--
			heroInfo.SetActive(true);
            int heroMaxLevel = TableManager.GetCardByID(heroCardItem.templateID).MaxLevel;
			heroLevel.text = heroCardItem.level + "/" + heroMaxLevel;
            if (heroCardItem.level < heroMaxLevel)  //----µÈŒ¶²»×ã£¬ÓÃºì×ÖÀŽÌáÊŸ
            {
                heroLevel.color = Color.red;
            }
            else
            {
                heroLevel.color = Color.white;
            }

			heroHp.text = heroCardItem.GetHp().ToString();
			heroHpGrow.text = TableManager.GetCardByID(heroCardItem.templateID).HpGrow.ToString();
			heroAttack.text = heroCardItem.GetAttack().ToString();
			heroAttackGrow.text = TableManager.GetCardByID(heroCardItem.templateID).AttackGrow.ToString();
			
			int skill_id = TableManager.GetCardByID(heroCardItem.templateID).SkillVol;
			int skiil_dis = TableManager.GetSkillByID(skill_id).Effect;
			heroSkillName.text = LanguageManger.GetWords(TableManager.GetSkillDisplayByID(skiil_dis).Name);
			heroSkillLevel.text = heroCardItem.skillLevel + "/" + TableManager.GetSkillByID(skill_id).SkillMaxlevel;


			//œø»¯ºóµÄÐÅÏ¢--
			tgtInfo.SetActive(true);
			tgtLevel.text = tgtCardItem.level + "/" + TableManager.GetCardByID(tgtCardItem.templateID).MaxLevel;
			tgtHp.text = tgtCardItem.GetHp().ToString();
			tgtHpGrow.text = TableManager.GetCardByID(tgtCardItem.templateID).HpGrow.ToString();
			tgtAttack.text = tgtCardItem.GetAttack().ToString();
			tgtAttackGrow.text = TableManager.GetCardByID(tgtCardItem.templateID).AttackGrow.ToString();
			skill_id = TableManager.GetCardByID(tgtCardItem.templateID).SkillVol;
			skiil_dis = TableManager.GetSkillByID(skill_id).Effect;
			tgtSkillName.text = LanguageManger.GetWords(TableManager.GetSkillDisplayByID(skiil_dis).Name);
			tgtSkillLevel.text = tgtCardItem.skillLevel + "/" + TableManager.GetSkillByID(skill_id).SkillMaxlevel;


			//evolution material
			/*
			for(int i = 0; i < 5; i++)
			{
				if(materialCardItems[i] != null)
				{
					evolutionMaterial[i].gameObject.SetActive(true);
					evolutionMaterial[i].GetComponent<CardIcon>().SetCardTemplateID(materialCardItems[i].templateID);
				}
				else
				{
					evolutionMaterial[i].gameObject.SetActive(false);
				}
			}
			*/
			
			for(int i=0; i<5; i++)
			{
				if(matIDArray[i] > 0 && materialCardItems[i] != null)
				{
					evolutionMaterial[i].gameObject.SetActive(true);
					//evolutionMaterial[i].GetComponent<CardIcon>().SetCardTemplateID(matIDArray[i]);
                    //ÓÐ²ÄÁÏµÄÊ±ºòÏÔÊŸÕý³£
					evolutionMaterial[i].GetComponent<CardIcon>().SetCardTemplateID(matIDArray[i] , Color.white);
				}
				else if(matIDArray[i] > 0)
				{
                    //È±ÉÙ²ÄÁÏµÄÊ±ºò»ÒÌ¬
					evolutionMaterial[i].gameObject.SetActive(true);
					evolutionMaterial[i].GetComponent<CardIcon>().SetCardTemplateID(matIDArray[i] , Color.grey);
				}
                else
                {
                    evolutionMaterial[i].gameObject.SetActive(false);
                }

                evolutionMaterial[i].name = matIDArray[i].ToString();
			}

            //ÔÝÊ±ÕâÑù×ö ÐÇŒ¶*10000
            evolutionCost.text = "" + heroCardItem.quality * 10000;
			
			//targetBg.transform.Find("spWaifaguang").gameObject.SetActive(true);
		}
		else
		{
			
			//targetBg.transform.Find("spWaifaguang").gameObject.SetActive(false);
			tgtSkillName.text = "";
            heroSkillName.text = "";
            heroSkillLevel.text = "";
            evolutionCost.text = "";
			evolutionBefore.gameObject.SetActive(false);
			evolutionAfter.gameObject.SetActive(false);
			heroInfo.SetActive(false);
			tgtInfo.SetActive(false);
			for(int i = 0; i < 5; i++)
			{
				evolutionMaterial[i].gameObject.SetActive(false);
			}
		}

		if(isCanEvo)
		{
            evolutionBtn.transform.GetComponent<UIImageButton>().isEnabled = true;
			evolutionAfter.transform.FindChild("Panel/Card").GetComponent<UITexture>().color = Color.white;
            //if (evolutionBtnSpLabel != null)
            //{
               // evolutionBtnSpLabel.GetComponent<UISprite>().color = Color.white;
           // }
		}
		else
		{
			evolutionBtn.transform.GetComponent<UIImageButton>().isEnabled = false;
           // if (evolutionBtnSpLabel != null)
           // {
              //  evolutionBtnSpLabel.GetComponent<UISprite>().color = Color.grey;
          //  }

            //Transform gbTest = evolutionAfter.transform.FindChild("Panel/Card");
			evolutionAfter.transform.FindChild("Panel/Card").GetComponent<UITexture>().color = Color.grey;
		}

      
	}
	
	public void OnSelectHero()
	{
		//处于动画播放中，不能点击此按钮
		if(bInAnimation)
		{
			return;
		}
		
		Obj_MyselfPlayer.GetMe().curCultivateType = CultivateType.EVOLUTION;
		Obj_MyselfPlayer.GetMe().isSelectHero = true;
		GameObject.FindWithTag("main_ui_logic").GetComponent<MainUILogic>().OnCultivateSelectWindow();
	}

	void Calculate()
	{
		for(int i = 0; i < 5; i++)
		{
			materialCardItems[i] = null;
			//matIDArray[i] = -1;
		}
		
		if(heroCardItem != null)
		{
			//Ñ¡ÔñµÄÓ¢ÐÛµÄÀàÐÍID
			int temp_id = heroCardItem.templateID;

			//»ñµÃÓ¢ÐÛÐÇŒ¶

			int star = TableManager.GetCardByID(temp_id).Star;

            int nEvolveID = 0;
            //Ð¡ÓÚ6µÄÇé¿öžúÎåÐÐÎÞ¹Ø
            if (star < 6)
            {
                nEvolveID = star;
            }
            else//ÐÇŒ¶Îª6µÄ¿šÅÆÉýµœ7ÐÇÒªžùŸÝÎåÐÐÀŽÅÐ¶Ï(ÐÇŒ¶+ÎåÐÐID)
            {
                nEvolveID += (star + heroCardItem.GetAttributeID());
            }

            for (int i = 0; i < 5; i++)
            {
                matIDArray[i] = TableManager.GetEvolveByID(nEvolveID).GetCardIDbyIndex(i);
				
				int ncardNum = 0;
				bool bFind = false;
                foreach (UserCardItem item in Obj_MyselfPlayer.GetMe().cardBagList)
                {
                    //±»±£»€µÄ¿šÅÆ²»ÄÜ±»ÍÌ
                    if (!item.isProtected && !item.IsInFightArray() && item.templateID == matIDArray[i])
                    {
						ncardNum++;
						
						if(!bFind)
						{
							materialCardItems[i] = item;
							bFind = true;
						}
						/*
                        bool isUsed = false;
                        for (int j = 0; j < 5; j++)
                        {
                            if (materialCardItems[j] != null &&
                                materialCardItems[j].cardID == item.cardID)
                            {
                                isUsed = true;
                            }
                        }
                        if (!isUsed)
                        {
                            materialCardItems[i] = item;
                        }
                        */
                    }
                }
				
				
				if(ncardNum>0)
				{
					materialNum[i].color = Color.green;
				}
				else
				{
					materialNum[i].color = Color.red;
				}
				materialNum[i].text = ncardNum+"/1";
					
            }

			
			
			Obj_MyselfPlayer.GetMe().evolutionMaterialItems = materialCardItems;
			isMatEnough = true;
			for(int i = 0; i < 5; i++)
			{
				//È±²ÄÁÏµÄÇé¿ö
				if(matIDArray[i]>0 && materialCardItems[i] == null)
				{
					isMatEnough = false;
					break;
				}
			}
			isCanEvo = isMatEnough & heroCardItem.IsFullLevel();
			
		}
		else
		{
			isCanEvo = false;
		}
	}
	void AfterEvolution()
	{
		evolutionBefore.gameObject.SetActive(false);
		evolutionBtn.transform.GetComponent<UIImageButton>().isEnabled = false;
        evolutionAfter.transform.FindChild("Panel/Card").GetComponent<UITexture>().color = Color.white;
		heroInfo.SetActive(false);
		heroSkillName.text = "";
		for(int i = 0; i < 5; i++)
		{
			evolutionMaterial[i].gameObject.SetActive(false);
		}
		evolutionCost.text = "";
	}


    //œø»¯ºóµÄ¿šÅÆ£¬µã»÷µÄÏìÓŠº¯Êý
    public void OnTargetCardIconBtn(GameObject selected)
    {
        long selID = long.Parse(selected.transform.name);
        Debug.Log(selID);
        if (targetCardItem != null && selID > 0)
        {
            BoxManager.showCardInfoMessage(targetCardItem);
        }
    }

    //²ÄÁÏÐ¡ÍŒ±êµã»÷µÄÏìÓŠº¯Êý
    public void OnSelectMaterial(GameObject selected)
	{
        Debug.Log(selected.transform.name);
        long selID = long.Parse(selected.transform.name);
        Debug.Log(selID);
        if (selID > 0)
        {
            this.OnShowTargetInfo(selID);
        }

	}

	private void OnShowTargetInfo(long TargetID)
	{
        int cardTempleID = (int)TargetID;
        BoxManager.showCardInfoMessage(-1, cardTempleID);
	}
	
	public void OnCardEvolution()
	{
        if (Obj_MyselfPlayer.GetMe().money < int.Parse(evolutionCost.text)  )
        {
            //BoxManager.showMessage("金币不足");
//			BoxManager.showMessageByID((int)MessageIdEnum.Msg14);
			NetworkSender.Instance().buyGold(BuyGoldFinish,1);
            return;
        }
		
		if(Obj_MyselfPlayer.GetMe().evolutionHeroItem.IsInQxzbFightArray())
		{
			BoxManager.showMessageByID((int)MessageIdEnum.Msg195);
			UIEventListener.Get(BoxManager.getYesButton()).onClick += ComfirmEvolution;
			return;
		}
		
		
        //µã»÷ºó£¬œø»¯°ŽÅ¥Òþ²Ø
		NetworkSender.Instance().CardEvolution(OnEvolutionHeroRet);
	}
	
	private void ComfirmEvolution(GameObject gObj)
	{
		NetworkSender.Instance().CardEvolution(OnEvolutionHeroRet);
	}
	
	
	public void BuyGoldFinish(bool isSucess)
	{
		if (isSucess)
		{
			if (mainUILogic == null)
				mainUILogic = GameObject.Find("MainUILogic");
			mainUILogic.GetComponent<MainUILogic>().SendMessage("refreshTopBar");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg59);
			Debug.Log("Buy Gold Finish");
		}
		else
			Debug.LogError("Buy Gold Error");
	}
	void OnEvolutionHeroRet(bool isSucceed)
	{
		evolutionBtn.transform.GetComponent<UIImageButton>().isEnabled = false;
		if(isSucceed)
		{
			//开始播放动画了
			bInAnimation = true;
			for(int i = 0; i < 5; i++)
			{
				if(materialCardItems[i] != null)
				{
					downAnim[i].SetActive(true);
					downAnim[i].GetComponent<UISpriteAnimation>().enabled=true;
				}
			}
			completeEvo=true;
			AudioManager.Instance.PlayEffectSound("card_evolute",false);
//			AfterEvolution();
			RefreshTopBar();
		}
		else
		{
			LogModule.ErrorLog("OnUpdateHeroRet(), isSucceed = false");
		}
	}
	public void OnTopLeftBtn()
	{
		GameObject.FindWithTag("main_ui_logic").GetComponent<MainUILogic>().ReturnToMainUI();
	}
	
	void RefreshTopBar()
	{
		GameObject.FindWithTag("main_controller").SendMessage("updateUserInfo");
	}
}

