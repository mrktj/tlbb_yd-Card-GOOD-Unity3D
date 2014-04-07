using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;
using Module.Log;
using card;
using card.net;

public class CardUpdateController : MonoBehaviour {

	public UILabel levelValue;
	public UILabel lvlUpdatedValue;
	public UILabel hpValue;
	public UILabel attackValue;
	public UILabel volSkillName;
	public UILabel volSkillLevel;
	public UISlider expProgressBar;
	public UISlider expUpdateBar;
	public UILabel labelSkillRate;
	public UILabel labelSkill;
	
	public UILabel hpAddValue;
	public UILabel AttackAddValue;
	public UILabel volSkillProb;
	public UILabel costMoney;
	public UILabel addExp;//--ÕâžöÒÑŸ­²»ÓÃ£¬ÔÝÊ±°ÑlabelÒþ²ØÁË
    public UILabel expLabel;  //--ŽËlabel 10000/30000  ×ó±ß±íÊŸ±»ÍÌÊÉµÄ¿šÅÆŽøÀŽµÄŸ­Ñé£¬ÓÒ±ß±íÊŸµœÂúŒ¶»¹ÐèÒª¶àÉÙŸ­Ñé
	
	public GameObject heroBaseInfo;
	public GameObject heroUpdateInfo;
	public GameObject mainHero;
	public GameObject heroIcon;
	public GameObject heroBg;
	public GameObject spriteAdd;
	public GameObject HeroUpdateInfoLine;  //---šš?¡ÀšŠ???????¡ì???????????????šš????????
    public GameObject CardInfoSp; //Ó¢ÐÛÐÅÏ¢µÄÒ»Ð©ÍŒÆ¬
	public GameObject[] materialIcons;
	public GameObject[] materialBgs;
	public GameObject[] materialAni;
	public GameObject animationMain;
	
	public UIButton selectHeroBtn;
	public UIButton selectMaterialBtn;
	public UIImageButton updateBtn;
	public GameObject costMoneyGo;
	public GameObject UpdateAniGo;
	
	private UserCardItem heroCardItem;
	private UserCardItem[] materialCardItems;
	private GameObject mainLogic;
	private int needMoney=0;
	private int needExp=0;
	private int moneycut=0;
	private int expcut=0;
	private bool startCut=false;
	private bool updateComplete=true;
	private int nowLev=0;
	private bool BarAni=false;
    private bool bBeGoingToUpdate = false; //ÅÐ¶ÏÊÇ·ñ»áÉýŒ¶
	private bool bInAnimation = false; //ÅÐ¶ÏÊÇ·ñÔÚ¶¯»­ÖÐ
	
	// Use this for initialization
	void Start () {
		mainLogic = GameObject.Find("MainUILogic");
		if(mainLogic == null)
		{
			Debug.LogError("Cant find MainUILogic !");
		}
		OnEnable();
	}
	
	// Update is called once per frame
	void Update () {
		if(BarAni)
		{
			if(Obj_MyselfPlayer.GetMe().updateHeroItem != null
				&& nowLev<Obj_MyselfPlayer.GetMe().updateHeroItem.level)
			{
				if(expProgressBar.sliderValue>=1)
				{
					expProgressBar.sliderValue=0;
					nowLev++;
					levelValue.text=nowLev.ToString();
				}
				expProgressBar.sliderValue+=Time.deltaTime;
			}
			else if(Obj_MyselfPlayer.GetMe().updateHeroItem != null 
				  && nowLev==Obj_MyselfPlayer.GetMe().updateHeroItem.level)
			{
				float fTargetPer = (float)heroCardItem.cardExp/(float)TableManager.GetCardexperienceByID(Obj_MyselfPlayer.GetMe().updateHeroItem.level).CardExp;
				if(expProgressBar.sliderValue < fTargetPer)
				{
					expProgressBar.sliderValue+=Time.deltaTime;
				}
				else
				{
					expProgressBar.sliderValue = fTargetPer;
					RefreshHero();
					BarAni=false;
				}
			}
		}
		
		if(animationMain.GetComponent<UISpriteAnimation>().enabled&&animationMain.GetComponent<UISpriteAnimation>().isPlaying==false)
		{
			animationMain.GetComponent<UISpriteAnimation>().enabled=false;
			animationMain.GetComponent<UISpriteAnimation>().Reset();
			animationMain.SetActive(false);
			mainHero.GetComponent<TweenScale>().enabled=false;
			mainHero.transform.localScale=new Vector3(1,1,1);
			bInAnimation = false;
		}
		if(startCut)
		{
			if(needExp-expcut>0)
			{
				needExp-=expcut;
				addExp.text=(needExp).ToString();
			}
			else
			{
				addExp.text="0";
			}
			if(needMoney-moneycut>0)
			{
				needMoney-=moneycut;
				costMoney.text=needMoney.ToString();
			}
			else
			{
				costMoney.text="0";
			}
			if(addExp.text=="0"&&costMoney.text=="0"&&!BarAni)
			{
				startCut=false;
				RefreshMaterials();
				RefreshTopBar();
			}
		}
	}
	
	//重置升级卡牌 和 材料卡牌
	void ResetHeroCardAndMaterials()
	{
		if(MainUILogic.GetLastWindow() == MainUILogic.ChildIndex.SelectHeroController)
		{
			return;
		}
		
		if(Obj_MyselfPlayer.GetMe().updateHeroItem != null)
		{
			 bool bheroCardInbag = Obj_MyselfPlayer.GetMe().IsCardInBagByID(Obj_MyselfPlayer.GetMe().updateHeroItem.cardID);
			if(	!bheroCardInbag)
			{
				Obj_MyselfPlayer.GetMe().updateHeroItem = null;
			}
		}
		
		UserCardItem[] mataerItemListTemp = Obj_MyselfPlayer.GetMe().updateMaterialItems;
		//把出售了的和保护的过滤
		for(int i=0; i<mataerItemListTemp.Length; i++)
		{	
			if(mataerItemListTemp[i] != null &&
			      (mataerItemListTemp[i].isProtected 
		              || !Obj_MyselfPlayer.GetMe().IsCardInBagByID(mataerItemListTemp[i].cardID) 
		                  || mataerItemListTemp[i].IsInFightArray() ))
			{
				mataerItemListTemp[i] = null;
			}
		}

	}
	
	void OnEnable()
	{
		if(mainLogic == null)
		{
			return;
		}

        GameObject.FindWithTag("main_ui_logic").GetComponent<MainUILogic>().SetMainUIBottomBarActive(true);
		bInAnimation = false;
		
		//œøÈëÉýŒ¶œçÃæ·ÖÁœÖÖ£¬Ò»ÖÖÊÇŽÓÑ¡ÔñÉýŒ¶œçÃæœøÀŽµÄ£¬Ò»ÖÖÊÇŽÓÆäËûŽ°¿ÚœøÀŽµÄ
		//if(mainLogic.GetComponent<MainUILogic>().GetLastWindowIndex() == MainUILogic.ChildIndex.SelectHeroController)
	//	{
		//	heroCardItem = Obj_MyselfPlayer.GetMe().updateHeroItem;
		//	materialCardItems = Obj_MyselfPlssszayer.GetMe().updateMaterialItems;
		//}
	//	else
	//	{
			//ÆäËûŽ°¿ÚœøÀŽµÄ
			
			//重置升级和材料卡牌
			ResetHeroCardAndMaterials();
		
			if(Obj_MyselfPlayer.GetMe().updateHeroItem != null)
			     
			{
				//if(Obj_MyselfPlayer.GetMe().updateHeroItem.IsFullLevel)
				//{
				
				//}
			
					heroCardItem = Obj_MyselfPlayer.GetMe().updateHeroItem;
				
					
				
					materialCardItems = Obj_MyselfPlayer.GetMe().updateMaterialItems;
				
			Debug.Log("Update Update Update Update Update");
			Debug.Log(MainUILogic.GetLastWindow());
			if( Obj_MyselfPlayer.GetMe().updateHeroItem.IsFullLevel()
					&& MainUILogic.GetLastWindow() != MainUILogic.ChildIndex.SelectHeroController)
				{
					Obj_MyselfPlayer.GetMe().updateHeroItem = null;
					Obj_MyselfPlayer.GetMe().updateMaterialItems = new UserCardItem[6];
					heroCardItem = Obj_MyselfPlayer.GetMe().updateHeroItem;
					materialCardItems = Obj_MyselfPlayer.GetMe().updateMaterialItems;
				}
				
			}
			else
			{
				Obj_MyselfPlayer.GetMe().updateHeroItem = null;
				Obj_MyselfPlayer.GetMe().updateMaterialItems = new UserCardItem[6];
				heroCardItem = Obj_MyselfPlayer.GetMe().updateHeroItem;
				materialCardItems = Obj_MyselfPlayer.GetMe().updateMaterialItems;
			}
		//}
		
		//heroCardItem = Obj_MyselfPlayer.GetMe().updateHeroItem;
		
        bBeGoingToUpdate = false;
		costMoney.text = "0";
		RefreshHero();
		RefreshMaterials();
		
		if(GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.UPDATE &&
			(GuideUpdate.Instance.curstep == (int)GuideUpdate.GUIDE_UPDATE_STEP.SELECT_2 ||
			GuideUpdate.Instance.curstep == (int)GuideUpdate.GUIDE_UPDATE_STEP.SELECT_4 ||
			GuideUpdate.Instance.curstep == (int)GuideUpdate.GUIDE_UPDATE_STEP.SELECT_7)){
			GuideUpdate.Instance.NextStep();
		}
       
	}

    void OnDisable()
    {
        this.AnimationFresh();
        this.EffFresh();
    }
	

    //动画效果刷新
    void AnimationFresh()
    {
        //?????????????¡ì??
        for (int i = 0; i < 6; i++)
        {
            iTween.Stop(materialAni[i]);
            materialAni[i].SetActive(false);
            materialAni[i].GetComponent<UISpriteAnimation>().Reset();
            materialAni[i].GetComponent<UISpriteAnimation>().enabled = false;
            materialAni[i].transform.localPosition = new Vector3(0, 0, -5);
        }

        //?????¡Â????
         animationMain.GetComponent<UISpriteAnimation>().enabled = false;
         animationMain.GetComponent<UISpriteAnimation>().Reset();
		 animationMain.GetComponent<UISprite>().enabled = false;
         animationMain.SetActive(false);
    }

    //?????????????¡ì??
    void EffFresh()
    {
        //????TweenAlpha¡ÁšŠ??
        for (int i=0; i<6; i++)
        {
            string strName = "Sprite" + i + "-Add";
            Destroy(materialBgs[i].transform.FindChild(strName).GetComponent<TweenAlpha>());
        }


        //?šª??¡ÁšŠ??
        for (int j = 0; j < 6; j++ )
        {
            string strNameIcon = "Sprite" + j + "-Add";
            Transform trans = materialBgs[j].transform.FindChild(strNameIcon);
            trans.GetComponent<UISprite>().alpha = 0.0f;
            TweenAlpha twAlpha = trans.gameObject.AddComponent<TweenAlpha>();
            twAlpha.from = 0.0f;
            twAlpha.to = 1.0f;
            twAlpha.alpha = 0.0f;
            twAlpha.duration = 1.0f;
            twAlpha.style = UITweener.Style.PingPong;

            if (heroCardItem != null)
            {
                trans.gameObject.SetActive(true);
            }
            else
            {
                trans.gameObject.SetActive(false);
            }

        }


        this.FreshLabelTweenEffect();
    }


    //Ë¢ÐÂÎÄ±ŸtweenÐ§¹û
    private void FreshLabelTweenEffect()
    {
        levelValue.transform.GetComponent<TweenAlpha>().Reset();
        lvlUpdatedValue.transform.GetComponent<TweenAlpha>().Reset();
        //expUpdateBar.transform.GetComponentInChildren<TweenAlpha>().Reset();

        //ÎÄ±ŸTweenAlpha¶¯»­Ë¢ÐÂ
        if (bBeGoingToUpdate)
        {
            //levelValue.transform.GetComponent<UILabel>().alpha = 0.0f;
			//levelValue.transform.GetComponent<TweenAlpha>().enabled = true;
            //lvlUpdatedValue.transform.GetComponent<TweenAlpha>().enabled = false;
			
			levelValue.gameObject.SetActive(false);
   			levelValue.transform.GetComponent<TweenAlpha>().enabled = false;
			levelValue.transform.GetComponent<TweenAlpha>().Reset();
			levelValue.transform.GetComponent<TweenAlpha>().from = 0;
			levelValue.transform.GetComponent<TweenAlpha>().alpha = 0;
			levelValue.transform.GetComponent<TweenAlpha>().to = 1;
			
			lvlUpdatedValue.gameObject.SetActive(true);
			lvlUpdatedValue.transform.GetComponent<TweenAlpha>().enabled = true;
			lvlUpdatedValue.transform.GetComponent<TweenAlpha>().Reset();
			lvlUpdatedValue.transform.GetComponent<TweenAlpha>().from = 0;
			lvlUpdatedValue.transform.GetComponent<TweenAlpha>().alpha = 0;
			lvlUpdatedValue.transform.GetComponent<TweenAlpha>().to = 1;
			
			expUpdateBar.gameObject.SetActive(true);
			Transform updateBarTrans = expUpdateBar.transform.Find("Foreground");
			updateBarTrans.GetComponent<TweenAlpha>().enabled = true;
			updateBarTrans.GetComponent<TweenAlpha>().Reset();
			updateBarTrans.GetComponent<TweenAlpha>().from = 0;
			updateBarTrans.GetComponent<TweenAlpha>().alpha = 0;
			updateBarTrans.GetComponent<TweenAlpha>().to = 1;
			
        }
        else
        {
			levelValue.gameObject.SetActive(true);
            levelValue.transform.GetComponent<UILabel>().alpha = 1.0f;
            levelValue.transform.GetComponent<TweenAlpha>().enabled = false;
			
			lvlUpdatedValue.transform.GetComponent<TweenAlpha>().enabled = false;
			lvlUpdatedValue.gameObject.SetActive(false);
        }

        //if (heroCardItem != null)
       // {
            //levelValue.gameObject.SetActive(true);
       // }
       // else
       // {
            //levelValue.gameObject.SetActive(false);
       // }
		
    }

	
	
	void RefreshHero()
	{
		if(heroCardItem != null)
		{
			levelValue.gameObject.SetActive(true);
			heroIcon.gameObject.SetActive(true);
			heroBg.gameObject.SetActive(false);
			heroIcon.GetComponent<CardLarge>().SetCardTemplateID(heroCardItem.templateID);
			
			heroBaseInfo.gameObject.SetActive(true);
			CalculateHeroData(false);
			//selectHeroBtn.gameObject.SetActive(false);
			//selectMaterialBtn.gameObject.SetActive(true);
            //updateBtn.isEnabled = true;
            //spriteAdd.SetActive(true);
			
			levelValue.gameObject.SetActive(true);
			lvlUpdatedValue.gameObject.SetActive(false);
			expUpdateBar.gameObject.SetActive(false);
			expProgressBar.gameObject.SetActive(true);
            HeroUpdateInfoLine.gameObject.SetActive(true);
            CardInfoSp.SetActive(true);
			
			costMoneyGo.SetActive(true);
	        UpdateAniGo.SetActive(true);
            
		}
		else
		{
			levelValue.gameObject.SetActive(true);
			heroIcon.gameObject.SetActive(false);
			heroBg.gameObject.SetActive(true);
			heroBaseInfo.gameObject.SetActive(false);
			//selectHeroBtn.gameObject.SetActive(true);
			//selectMaterialBtn.gameObject.SetActive(false);
			//updateBtn.isEnabled = false;
			//spriteAdd.SetActive(false);
			
			levelValue.gameObject.SetActive(false);
			lvlUpdatedValue.gameObject.SetActive(false);
			expProgressBar.gameObject.SetActive(false);
			expUpdateBar.gameObject.SetActive(false);
            HeroUpdateInfoLine.gameObject.SetActive(false);
            CardInfoSp.SetActive(false);
			costMoneyGo.SetActive(false);
	        UpdateAniGo.SetActive(false);
		}
	}
	
	//ÅÐ¶ÏÊÇ·ñÓÐÑ¡Ôñ²ÄÁÏ
	bool IsChoosedMaterials()
	{
		bool bContain = false;
		for(int i = 0; i < 6; i++)
		{
			if(materialCardItems[i] != null)
			{
				bContain = true;
				break;
			}
		}
		
		return bContain;
	}

	void RefreshMaterials()
	{
        //??????¡€??????????????? update¡ã???????
        bool bCanUpdate = false;
		
		for(int i = 0; i < 6; i++)
		{				
			if(materialCardItems[i] != null)
			{
                bCanUpdate = true;
				materialIcons[i].gameObject.SetActive(true);
				materialBgs[i].gameObject.SetActive(false);
				materialIcons[i].GetComponent<CardIcon>().SetCardTemplateID(materialCardItems[i].templateID);
			}
			else
			{
				materialIcons[i].gameObject.SetActive(false);
				materialBgs[i].gameObject.SetActive(true);
                
			}

//             TweenAlpha twAlpha = materialBgs[i].transform.FindChild(strName).GetComponent<TweenAlpha>();
// 
//             twAlpha.alpha = 1.0f;
//             twAlpha.from = 1.0f;
//             twAlpha.to = 0.0f;
//             twAlpha.duration = 1;
//             twAlpha.delay = 0;
//             twAlpha.Reset();
		}
        
      

        if (heroCardItem != null)
        {
            expProgressBar.gameObject.SetActive(!bCanUpdate);
            expUpdateBar.gameObject.SetActive(bCanUpdate);
        }
        else
        {
            expProgressBar.gameObject.SetActive(false);
            expUpdateBar.gameObject.SetActive(false);
        }
		
		mainHero.GetComponent<TweenScale>().enabled = false;
		mainHero.transform.localScale = new Vector3(1,1,1);
		
		 updateBtn.isEnabled = bCanUpdate;
		
		/*
         if (bCanUpdate)
         {
            updateBtn.transform.FindChild("Sprite").GetComponent<UISprite>().color = Color.white;
         }
         else
         {
             updateBtn.transform.FindChild("Sprite").GetComponent<UISprite>().color = Color.grey;
         }
         */

        if (heroCardItem != null)
        {
            int nMaxLev = TableManager.GetCardByID(heroCardItem.templateID).MaxLevel;
            if (heroCardItem.level >= nMaxLev)
            {
                //ÂúŒ¶ºóÒþ²ØÉÁËžµÄœø¶ÈÌõ£¬ ºÍ µÈŒ¶ÎÄ±Ÿ
                expLabel.gameObject.SetActive(false);

                expUpdateBar.gameObject.SetActive(false);
				expProgressBar.gameObject.SetActive(true);
            }
            else
            {
                //expLabel.gameObject.SetActive(bCanUpdate);
                expLabel.gameObject.SetActive(true);
            }

        }
        else
        {
            expLabel.gameObject.SetActive(false);
        }

        //ÏÈÔÚÕâŒÆËãÈ»ºóÔÙµ÷ÓÃÐ§¹ûžüÐÂ£¬ÒòÎªÀïÃæÓÐÅÐ¶ÏŽËÓ¢ÐÛÊÇ·ñ»áÉýŒ¶£¬Ó¢ÐÛµÈŒ¶ÎÄ±ŸÏÔÊŸ
        CalculateUpdateData();

        EffFresh();
	}
	void ClearHero()
	{
		//heroCardItem = Obj_MyselfPlayer.GetMe().updateHeroItem = null;
		RefreshHero();
	}
	void ClearMaterials()
	{
		for(int i = 0; i < materialCardItems.Length; i++)
		{
			materialCardItems[i] = null;
		}
        //updateBtn.transform.FindChild("Sprite").GetComponent<UISprite>().color = Color.gray;
		RefreshMaterials();
	}
	void CalculateHeroData(bool bInUpdateShow)
	{
		Tab_Card tbl_card = TableManager.GetCardByID(heroCardItem.templateID);
		//Tab_Skill tbl_skill_vol = TableManager.GetSkillByID(tbl_card.SkillVol);
		
		levelValue.text = heroCardItem.level.ToString();
		if(!bInUpdateShow)
		{
			nowLev = heroCardItem.level;
		}
		nowLev=heroCardItem.level;
		hpValue.text = heroCardItem.GetHp().ToString();
		attackValue.text = heroCardItem.GetAttack().ToString();//
        if (nowLev == tbl_card.MaxLevel)
        {
            expProgressBar.sliderValue = 0f;
        }
        else
        {
            expProgressBar.sliderValue = (float)heroCardItem.cardExp / (float)TableManager.GetCardexperienceByID(heroCardItem.level).CardExp;
        }
		
		this.FreshSkill();
	}
	
	//Ë¢ÐÂŒŒÄÜ
	private void FreshSkill()
	{
		Tab_Card tbl_card = TableManager.GetCardByID(heroCardItem.templateID);
		Tab_Skill tbl_skill_vol = TableManager.GetSkillByID(tbl_card.SkillVol);
		
		if(tbl_skill_vol != null && TableManager.GetSkillDisplayByID(tbl_skill_vol.Effect) != null)
		{
			volSkillName.gameObject.SetActive(true);
			volSkillLevel.gameObject.SetActive(true);
			
			volSkillName.text = LanguageManger.GetWords(TableManager.GetSkillDisplayByID(tbl_skill_vol.Effect).Name);
			volSkillLevel.text = heroCardItem.skillLevel + "/" + tbl_skill_vol.SkillMaxlevel;
		}
		else
		{
			volSkillName.gameObject.SetActive(false);
			volSkillLevel.gameObject.SetActive(false);
		}
	}

    //????????????????
    private float  CalculateSkillUpdateRate(UserCardItem[] materialCardItems)
    {
        if (heroCardItem == null)
        {
            return 0.0f;
        }

        //????????????
        float nSkillUpateRate = 0.0f;

        //???? ?? ???????????????š€????????????????
        List<UserCardItem> equalSkillCard = new List<UserCardItem>();
        Tab_Card tabCardHero = TableManager.GetCardByID(heroCardItem.templateID);
        for (int i = 0; i < materialCardItems.Length; i++)
        {
            if (materialCardItems[i] != null)
            {
                Tab_Card tabCardMaterial = TableManager.GetCardByID(materialCardItems[i].templateID);
                Tab_Skill tabCardMaterialSkill = TableManager.GetSkillByID(tabCardMaterial.SkillVol);
                Tab_Skill tabHeroSkill = TableManager.GetSkillByID(tabCardHero.SkillVol);

                if (tabHeroSkill.UpdateRule == tabCardMaterialSkill.UpdateRule)
                {
                    equalSkillCard.Add(materialCardItems[i]);
                }
            }
        }

        //????????¡€???????????????????????0
        if (equalSkillCard.Count == 0)
        {
            return 0;
        }

        Tab_Skillbasicchance sklChange = TableManager.GetSkillbasicchanceByID(1);
        //????????¡€??¡ì1-6?? ????????????????????????¡Àšª?????¡Â?? 0-5,?šŽ????????  ????????-1
        int nBaseRate = sklChange.GetBasicChancebyIndex(heroCardItem.skillLevel - 1);
        int[] rates = new int[equalSkillCard.Count];
        for ( int i=0; i < equalSkillCard.Count; i++)
        {
            UserCardItem card = equalSkillCard[i];
            //Tab_Card tmpCard = TableManager.GetCardByID(card.templateID);
            int skillLev = card.skillLevel;
            int cardStar = card.quality;
            Tab_Skillupdate sklUpRate = TableManager.GetSkillupdateByID(cardStar);

            rates[i] = sklUpRate.Chance + sklUpRate.GetSkillChancebyIndex(skillLev - 1) + nBaseRate;
        }

        //????????¡À?????????????????:??N??????????¡€?????????????????¡€??????? = 1 - N????????¡€?
        float tmpRate = 1;
        for (int j = 0; j < rates.Length; j++ )
        {
            tmpRate *= (1 - rates[j] / 100.0f);
        }
        nSkillUpateRate = 1 - tmpRate;

        return nSkillUpateRate;
    }


	
	void CalculateUpdateData()
	{
		if(heroCardItem == null)
		{
			heroUpdateInfo.gameObject.SetActive(false);
			return;
		}
		

        int money = heroCardItem.GetUpDateCostMoney(materialCardItems);

        float skillUpdateRate = this.CalculateSkillUpdateRate(materialCardItems);

        int exp = 0;
		for(int i = 0; i < materialCardItems.Length; i++)
		{
			if(materialCardItems[i] != null)
			{
				exp += TableManager.GetCardByID(materialCardItems[i].templateID).ExpBase * materialCardItems[i].level;
			}
		}

        int nMaxLev = TableManager.GetCardByID(heroCardItem.templateID).MaxLevel;
        int curTotalExp = (TableManager.GetCardexperienceByID(heroCardItem.level).TotalExp
			               - TableManager.GetCardexperienceByID(heroCardItem.level).CardExp) 
			                  + heroCardItem.cardExp;
       
		int nMaxTotalExp = TableManager.GetCardexperienceByID(nMaxLev).TotalExp - TableManager.GetCardexperienceByID(nMaxLev).CardExp;
        int nMaxLevNeedExp = nMaxTotalExp - curTotalExp;
        expLabel.text = exp + "/" + nMaxLevNeedExp;
        
		if(exp > 0)
		{
			
			
			//if(tbl_skill_vol.SkillMaxlevel == heroCardItem.skillLevel)
			//{
				//heroUpdateInfo.gameObject.SetActive(false);
			//}
			//else
			//{
				heroUpdateInfo.gameObject.SetActive(true);
			//}
			
			Tab_Card tbl_card = TableManager.GetCardByID(heroCardItem.templateID);
			Tab_Skill tbl_skill_vol = TableManager.GetSkillByID(tbl_card.SkillVol);
			
			if(!IsChoosedMaterials() || tbl_skill_vol.SkillMaxlevel == heroCardItem.skillLevel)
			{
				labelSkillRate.gameObject.SetActive(false);
				labelSkill.gameObject.SetActive(false);
			}
			else
			{
				labelSkillRate.gameObject.SetActive(true);
				labelSkill.gameObject.SetActive(true);
			}
			

            float fSkillRate = skillUpdateRate * 100;
            volSkillProb.text = (int)fSkillRate + "%";
			costMoney.text = money.ToString();

			//???????????
			if(heroCardItem.level < TableManager.GetCardByID(heroCardItem.templateID).MaxLevel)
			{
				int temp_lvl = heroCardItem.level;
				int temp_exp = exp;
				int temp_curexp = heroCardItem.cardExp;
				bool is_add = true;

                int dexp = TableManager.GetCardexperienceByID(temp_lvl).CardExp - temp_curexp;
                if (temp_exp < dexp)
                {
                    is_add = false;
                    temp_curexp += temp_exp;
                }
                
				while(is_add)
				{
					if(temp_lvl >= TableManager.GetCardByID(heroCardItem.templateID).MaxLevel)
					{
						is_add = false;
						break;
					}
					dexp = TableManager.GetCardexperienceByID(temp_lvl).CardExp - temp_curexp;
					
					if(temp_exp < dexp)
					{
						is_add = false;
						temp_curexp = temp_exp;
					}
					else
					{
						temp_exp -= dexp;
						temp_curexp = 0;
						temp_lvl++;
					}
				}
				
				if(temp_lvl > heroCardItem.level)
				{
					levelValue.gameObject.SetActive(false);
					lvlUpdatedValue.gameObject.SetActive(true);
					hpAddValue.gameObject.SetActive(true);
					AttackAddValue.gameObject.SetActive(true);
					expUpdateBar.gameObject.SetActive(true);
                    expUpdateBar.sliderValue = ((float)temp_curexp) / (float)TableManager.GetCardexperienceByID(temp_lvl).CardExp;
					lvlUpdatedValue.text = temp_lvl.ToString();
					hpAddValue.text = "+"+(TableManager.GetCardByID(heroCardItem.templateID).HpGrow * (temp_lvl - heroCardItem.level)).ToString();
					AttackAddValue.text = "+"+(TableManager.GetCardByID(heroCardItem.templateID).AttackGrow * (temp_lvl - heroCardItem.level)).ToString();
				}
				else
				{
					expUpdateBar.gameObject.SetActive(true);
					expUpdateBar.sliderValue=((float)temp_curexp)/(float)TableManager.GetCardexperienceByID(temp_lvl).CardExp;
					AttackAddValue.gameObject.SetActive(false);
					hpAddValue.gameObject.SetActive(false);
					lvlUpdatedValue .gameObject.SetActive(false);
					levelValue.gameObject.SetActive(true);
				}


                //ÅÐ¶ÏÖ®ºó»á²»»áÉýŒ¶,ŽÓ¶øÓ°Ïì£¬µÈŒ¶ÎÄ×ÖµÄÇÐ»»ÏÔÊŸ
                if (temp_lvl > heroCardItem.level)
                {
                    bBeGoingToUpdate = true;
                }
                else
                {
                    bBeGoingToUpdate = false;
                }

				addExp.text = exp.ToString();
				updateBtn.isEnabled = true;
                //updateBtn.transform.FindChild("Sprite").GetComponent<UISprite>().color = Color.white;
			}
			else //??????????¡Á???????????,??????????????
			{
                fSkillRate = skillUpdateRate*100;
                volSkillProb.text = (int)fSkillRate + "%";
				addExp.text = "0";
				AttackAddValue.gameObject.SetActive(false);
				hpAddValue.gameObject.SetActive(false);
				lvlUpdatedValue.gameObject.SetActive(false);
				levelValue.gameObject.SetActive(true);
                bBeGoingToUpdate = false;
			}
		}
		else
		{
			heroUpdateInfo.gameObject.SetActive(false);
		}
	}
	
	public void OnSelectHero()
	{
		if(GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.UPDATE &&
			GuideUpdate.Instance.curstep == (int)GuideUpdate.GUIDE_UPDATE_STEP.SELECT_3)
			GuideUpdate.Instance.NextStep();//¿šÅÆÉýŒ¶ÖžÒýœ×¶Î SELECT_2
		
		if(bInAnimation)
		{
			return;
		}
		
		Obj_MyselfPlayer.GetMe().curCultivateType = CultivateType.UPDATE;
		Obj_MyselfPlayer.GetMe().isSelectHero = true;
		GameObject.FindWithTag("main_ui_logic").GetComponent<MainUILogic>().OnCultivateSelectWindow();
	}

	public void OnSelectMaterial()
	{
		if(GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.UPDATE &&
			GuideUpdate.Instance.curstep == (int)GuideUpdate.GUIDE_UPDATE_STEP.SELECT_5)
			GuideUpdate.Instance.NextStep();//¿šÅÆÉýŒ¶ÖžÒýœ×¶Î SELECT_4
		
		if(heroCardItem == null || bInAnimation)
		{
			return;
		}
		
		Obj_MyselfPlayer.GetMe().curCultivateType = CultivateType.UPDATE;
		Obj_MyselfPlayer.GetMe().isSelectHero = false;
		GameObject.FindWithTag("main_ui_logic").GetComponent<MainUILogic>().OnCultivateSelectWindow();
        GameObject.FindWithTag("main_ui_logic").GetComponent<MainUILogic>().SetMainUIBottomBarActive(false);
	}
	
	public void BuyGoldFinish(bool isSucess)
	{
		if (isSucess)
		{
			if (mainLogic == null)
				mainLogic = GameObject.Find("MainUILogic");
			mainLogic.GetComponent<MainUILogic>().SendMessage("refreshTopBar");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg59);
			Debug.Log("Buy Gold Finish");
		}
		else
			Debug.LogError("Buy Gold Error");
	}
	
	public void OnUpdateHero()
	{
		if(GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.UPDATE &&
			GuideUpdate.Instance.curstep == (int)GuideUpdate.GUIDE_UPDATE_STEP.NONE_3)
			GuideUpdate.Instance.NextStep();//¿šÅÆÉýŒ¶ÖžÒýœ×¶Î SELECT_7
		
        if (int.Parse(costMoney.text) > Obj_MyselfPlayer.GetMe().money)
        {
//            BoxManager.showMessage("œðÇ®²»×ã");
//			BoxManager.showMessageByID((int)MessageIdEnum.Msg8);
			NetworkSender.Instance().buyGold(BuyGoldFinish,1);
            return;
        }
		
		int nMaxLev = TableManager.GetCardByID(heroCardItem.templateID).MaxLevel;
		Tab_Card tabCardHero = TableManager.GetCardByID(heroCardItem.templateID);
		Tab_Skill tabHeroSkill = TableManager.GetSkillByID(tabCardHero.SkillVol);
		
		if(heroCardItem.level == nMaxLev && heroCardItem.skillLevel < tabHeroSkill.SkillMaxlevel)
		{
			//BoxManager.showConfirmMessage("ÏÀÊ¿ÒÑŽï×îžßŒ¶±ð\n\nÏÀÊ¿ÂúŒ¶»¹¿ÉÊ¹ÓÃÏàÍ¬ŒŒÄÜÏÀÊ¿ÌáÉý¶ÀÃÅŸøŒŒŒÌÐøÉýŒ¶»áÎüÊÕµ±Ç°ÏÀÊ¿£¬\nÊÇ·ñŒÌÐø£¿");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg105);
			UIEventListener.Get(BoxManager.getYesButton()).onClick += SendUpdateMessage;
            UIEventListener.Get(BoxManager.getNoButton()).onClick += CancelUpdateCard;
			return;
		}
		else if(heroCardItem.level == nMaxLev && heroCardItem.skillLevel == tabHeroSkill.SkillMaxlevel)
		{
			
//			BoxManager.showConfirmMessage("ÏÀÊ¿Œ°Æä¶ÀÃÅŸøŒŒÒÑŽï×îžßŒ¶±ð\n\nŒÌÐøÉýŒ¶»áÎüÊÕµ±Ç°ÏÀÊ¿£¬\nÊÇ·ñŒÌÐø£¿¡£");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg106);
			UIEventListener.Get(BoxManager.getYesButton()).onClick += SendUpdateMessage;
			return;
		}
		
		this.SendUpdateMessage(null);
//		NetworkSender.Instance().CardUpdate(OnUpdateHeroRet);
	}
	
	private void CancelUpdateCard(GameObject btn)
	{
		
	}
	
	//·¢ËÍÉýŒ¶ÏûÏ¢
	private void SendUpdateMessage(GameObject btn)
	{
        //updateBtn.transform.FindChild("Sprite").GetComponent<UISprite>().color = Color.grey;
		
		if(GuideManager.GUIDE_STEP.UPDATE == GuideManager.Instance.currentStep &&
			GuideUpdate.Instance.curstep == (int)GuideUpdate.GUIDE_UPDATE_STEP.LABEL_6)
		{
			NetworkSender.Instance().CardUpdate(sendFinishStep);
		}else
		{

			NetworkSender.Instance().CardUpdate(OnUpdateHeroRet);
		}
	}
	
	public void sendFinishStep(bool isSuccess)
	{
//		NetworkSender.Instance().guideFinishStep(recFinishStep,GuideManager.GUIDE_STEP.CRAFT);
//		GuideManager.Instance.getCurrentGuideWindow().GetComponent<GuideCardUpdateController>().OnClickGuideItem(null);
		//GuideManager.Instance.FinishedStep(GuideManager.GUIDE_STEP.CRAFT);
		GuideUpdate.Instance.NextStep();
		recFinishStep(isSuccess);
	}
	public void recFinishStep(bool isSuccess)
	{
		OnUpdateHeroRet(isSuccess);
	}
	
	IEnumerator  MaterialUseComplete()
	{
		yield return new WaitForSeconds(0.5f);
		//if(nowLev < heroCardItem.level)
		//{
			BarAni = true;
		//}
		//else
		//{
		//	BarAni = false;
		//}
		
		ClearMaterials();
		
	}
	
	void OnUpdateHeroRet(bool isSucceed)
	{
		if(isSucceed)
		{
			bInAnimation = true;
			updateComplete=false;

            //³É¹ŠÉýŒ¶£¬ÏÔÊŸÉýŒ¶¶¯»­£¬œáÊøÎÄ×ÖtweenÐ§¹û
            bBeGoingToUpdate = false;
            this.FreshLabelTweenEffect();
			
			for(int i = 0; i < materialCardItems.Length; i++)
			{
				if(materialCardItems[i] != null)
				{
					materialAni[i].SetActive(true);
					materialAni[i].GetComponent<UISpriteAnimation>().enabled=true;
					Hashtable hash=new Hashtable();
					hash.Add("time",1.0f);
					hash.Add("position",animationMain.transform);
					hash.Add("delay",0.5f);
					hash.Add("oncomplete","AnimationComplete");
					hash.Add("oncompleteparams",materialAni[i]);
					hash.Add("oncompletetarget",gameObject);
					
					StartCoroutine(MaterialUseComplete());
					
					iTween.MoveTo(materialAni[i],hash);
				}
			}
			
			needMoney=int.Parse(costMoney.text);
			needExp=int.Parse(addExp.text);
			moneycut=int.Parse(costMoney.text)/50;
			if(moneycut==0)
			{
				moneycut=1;
			}
			expcut=int.Parse(addExp.text)/50;
			if(expcut==0)
			{
				expcut=1;
			}
			levelValue.gameObject.SetActive(true);
			lvlUpdatedValue.gameObject.SetActive(false);
			expUpdateBar.gameObject.SetActive(false);
			hpAddValue.gameObject.SetActive(false);
			AttackAddValue.gameObject.SetActive(false);
			mainHero.GetComponent<TweenScale>().enabled=true;
			startCut=true;
			Obj_MyselfPlayer.GetMe().ResetCultivateData();
			Obj_MyselfPlayer.GetMe().updateHeroItem = Obj_MyselfPlayer.GetMe().GetUserCardByGUID((long)heroCardItem.cardID);
			heroCardItem = Obj_MyselfPlayer.GetMe().updateHeroItem;
			AudioManager.Instance.PlayEffectSound("card_upgrade",false);
//			OnEnable();
			RefreshTopBar();
			this.FreshSkill();
		}
		else
		{
			LogModule.ErrorLog("OnUpdateHeroRet(), isSucceed = false");
		}
		updateBtn.isEnabled = false;
	}
	
	void AnimationComplete(GameObject go)
	{
		if(!updateComplete)
		{
			updateComplete=true;
			animationMain.SetActive(true);
			animationMain.GetComponent<UISpriteAnimation>().enabled=true;
			animationMain.GetComponent<UISprite>().enabled=true;
            //animationMain.GetComponent<UISpriteAnimation>()
		}
		go.SetActive(false);
		go.transform.localPosition=new Vector3(0,0,-5);
		go.GetComponent<UISpriteAnimation>().enabled=false;
	
	}

    //µÈŒ¶ÎÄ±ŸtweenœáÊø
    public void CurLevelLabelTweenFinished()
    {
        levelValue.gameObject.SetActive(false);
        levelValue.transform.GetComponent<TweenAlpha>().enabled = false;
        lvlUpdatedValue.gameObject.SetActive(true);
        lvlUpdatedValue.transform.GetComponent<TweenAlpha>().enabled = true;
    }


    //ÉýŒ¶ÎÄ±ŸtweenœáÊø
    public void UpdateLabelTweenFinished()
    {
        levelValue.gameObject.SetActive(true);
        levelValue.transform.GetComponent<TweenAlpha>().enabled = true;
        lvlUpdatedValue.gameObject.SetActive(false);
        lvlUpdatedValue.transform.GetComponent<TweenAlpha>().enabled = false;
    }
	
	public void OnTopLeftBtn()
	{
		//ÍõÃ÷ÀÚ : Í³ŒÆÄ£¿éŽúÂë -> Statistics
		//Èç¹ûÊÇGuideœ×¶Î,ÐèÒªÍ³ŒÆŽË°ŽÅ¥µÄµã»÷ÐÅÏ¢
			
		if (GameManager.Instance.isGuideMode())
			PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn28).ToString());
		GameObject.FindWithTag("main_ui_logic").SendMessage("ReturnToMainUI");
	}
	void RefreshTopBar()
	{
		GameObject.FindWithTag("main_controller").SendMessage("updateUserInfo");
	}
	
	//for guide
	public GameObject getGuideCardItem()
	{
		return transform.FindChild("MainHero/BG").gameObject;
	}
	public GameObject getGuideMaterialItem()
	{
		return transform.FindChild("Materials/UpdateMaterial-0").gameObject;
	}
	public GameObject getGuideConfirmButton()
	{
		return this.transform.FindChild("Buttons/Button-Update").gameObject;
	}
	public GameObject getGuideReturnButton()
	{
		return this.transform.FindChild("Buttons/TopLeftBtn").gameObject;
	}
}

