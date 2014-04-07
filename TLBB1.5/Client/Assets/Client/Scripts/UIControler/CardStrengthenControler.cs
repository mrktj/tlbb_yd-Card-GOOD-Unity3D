using UnityEngine;
using System.Collections;

using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;
using card.net;
using Module.Log;

public class CardStrengthenControler : MonoBehaviour {
	
	public GameObject heroObject;
	public UILabel heroLevel;
	
	public UILabel hpStrengthenValue;
	public UILabel hpCurValue;
	public UILabel hpAddValue;
	public UILabel hpTimeValue;  //血量强化次数文本
	public UILabel hpCostMoney;
	public UILabel hpCostItem;
	public UIImageButton hpConfirm;
	
	public UILabel attSthValue;
	public UILabel attCurValue;
	public UILabel attAddValue;
	public UILabel attTimeValue;   //攻击强化次数文本
	public UILabel attCostMoney;
	public UILabel attCostItem;
    public UILabel attConfirmBtnLabel;  //---强化攻击上的文字
    public UILabel hpComfirmBtnLabel;   //---强化生命上的文字
	public UIImageButton attConfirm;
	private GameObject mainUILogic;
    public GameObject strengAnimAttack;  //攻击强化光效
    public GameObject strengAnimHp;      //血量强化光效
	public GameObject addIcon;          //闪动的选择侠士图标
	public GameObject hpStrengthInfo;
	public GameObject AttStrengthInfo;
	public GameObject hpAddIcon;
	public GameObject AttAddIcon;
	
	private UserCardItem heroCardItem = null;
	//private bool isHpStrengthen = true;
	private bool isCanHpSth = false;
	private bool isCanAttSth = false;
	private int hpItemCount = 0;
	private int attItemCount = 0;
	private int hporattack=0;

	
	// Use this for initialization 
	void Start () {
		mainUILogic = GameObject.Find("MainUILogic");
		Reset();
		RefreshUI(true);
		
		GameObject.FindWithTag("main_controller").SendMessage("ShowStaticNotice", 4000);//固定显示提示信息
	}
	
	// Update is called once per frame
	void Update () {
		if(strengAnimAttack.GetComponent<UISpriteAnimation>().enabled&&strengAnimAttack.GetComponent<UISpriteAnimation>().isPlaying==false)
		{
			strengAnimAttack.GetComponent<UISpriteAnimation>().enabled=false;
			strengAnimAttack.GetComponent<UISpriteAnimation>().Reset();
			strengAnimAttack.SetActive(false);
		}

        if (strengAnimHp.GetComponent<UISpriteAnimation>().enabled && strengAnimHp.GetComponent<UISpriteAnimation>().isPlaying == false)
		{
            strengAnimHp.GetComponent<UISpriteAnimation>().enabled = false;
            strengAnimHp.GetComponent<UISpriteAnimation>().Reset();
            strengAnimHp.SetActive(false);
		}

        
	}
	
	void OnEnable()
	{
		if(mainUILogic == null)
		{
			return;
		}
		heroCardItem = Obj_MyselfPlayer.GetMe().strengthenHeroItem;
		
		RefreshUI(true);
		
		GameObject mainControl = GameObject.FindWithTag("main_controller");
		if(mainControl != null)
		{
			mainControl.SendMessage("ShowStaticNotice", 4000);//固定显示提示信息
		}
		
		//for guide
		/*
		if(GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.STRENGTHEN){
			if(GuideStrengthen.Instance.curstep == (int)GuideStrengthen.GUIDE_STRENGTHEN_STEP.LABEL_1)
				GuideStrengthen.Instance.NextStep();
		}
		*/
	}

    void OnDisable()
    {
        strengAnimAttack.GetComponent<UISpriteAnimation>().enabled = false;
        strengAnimAttack.GetComponent<UISpriteAnimation>().Reset();
        strengAnimAttack.SetActive(false);

        strengAnimHp.GetComponent<UISpriteAnimation>().enabled = false;
        strengAnimHp.GetComponent<UISpriteAnimation>().Reset();
        strengAnimHp.SetActive(false);
		
		GameObject mainControl = GameObject.FindWithTag("main_controller");
		if(mainControl != null)
		{
			mainControl.SendMessage("NotShowStaticNotice");//取消公告固定显示
		}
    }

	void Reset()
	{
		heroCardItem = null;
		//isHpStrengthen = true;
		isCanHpSth = false;
		isCanAttSth = false;
		hpItemCount = 0;
		attItemCount = 0;
	}
	
	void RefreshUI(bool bFirstShow)
	{
		Calculate();
		if(heroCardItem != null)
		{
			heroObject.gameObject.SetActive(true);
            addIcon.SetActive(false);
			heroLevel.gameObject.SetActive(true);
			
			hpStrengthenValue.gameObject.SetActive(true);
			hpCurValue.gameObject.SetActive(true);
			hpCostMoney.gameObject.SetActive(true);
			hpConfirm.isEnabled = true;
			
			attSthValue.gameObject.SetActive(true);
			attCurValue.gameObject.SetActive(true);
			attCostMoney.gameObject.SetActive(true);
			attConfirm.isEnabled = true;
			
			heroObject.GetComponent<CardLarge>().SetCardTemplateID(heroCardItem.templateID);
			heroLevel.text = heroCardItem.level + "/" + TableManager.GetCardByID(heroCardItem.templateID).MaxLevel;
			
			//hpStrengthInfo.SetActive(true);
			//AttStrengthInfo.SetActive(true);
			
			if(bFirstShow)
			{
				attTimeValue.gameObject.SetActive(false);
       			attTimeValue.transform.GetComponent<TweenAlpha>().enabled = false;
				attTimeValue.transform.GetComponent<TweenAlpha>().Reset();
				attTimeValue.transform.GetComponent<TweenAlpha>().from = 0;
				attTimeValue.transform.GetComponent<TweenAlpha>().to = 1;
				
        		attAddValue.gameObject.SetActive(true);
       			attAddValue.transform.GetComponent<TweenAlpha>().enabled = true;
				attAddValue.transform.GetComponent<TweenAlpha>().Reset();
				attAddValue.transform.GetComponent<TweenAlpha>().from = 0;
				attAddValue.transform.GetComponent<TweenAlpha>().to = 1;
				
				
				hpTimeValue.gameObject.SetActive(false);
				hpTimeValue.transform.GetComponent<TweenAlpha>().enabled = false;
				hpTimeValue.transform.GetComponent<TweenAlpha>().Reset();
				hpTimeValue.transform.GetComponent<TweenAlpha>().from = 0;
				hpTimeValue.transform.GetComponent<TweenAlpha>().to = 1;
				
				hpAddValue.gameObject.SetActive(true);
				hpAddValue.transform.GetComponent<TweenAlpha>().enabled = true;
				hpAddValue.transform.GetComponent<TweenAlpha>().Reset();
				hpAddValue.transform.GetComponent<TweenAlpha>().from = 0;
				hpAddValue.transform.GetComponent<TweenAlpha>().to = 1;
			}
			//生命强化--

			//hpCurValue.text = heroCardItem.GetHp().ToString();
			hpCurValue.text = (heroCardItem.GetHpBase() + heroCardItem.GetFengShuiHp() + heroCardItem.GetStudySkillHp()).ToString();
			hpAddValue.text = "+" + heroCardItem.GetHpAdd().ToString();
			hpCostMoney.text = ((heroCardItem.addQualityHp + 1)*6000).ToString();
			
			this.FreshStrenthLeftTime();
			//hpCostItem.text = "1/" + hpItemCount.ToString();
            if (isCanHpSth && heroCardItem.addQualityHp < 99 &&Obj_MyselfPlayer.GetMe().GetItemCountByType(ItemType.HP) > 0)
			{
				hpConfirm.isEnabled = true;
			}
			else
			{
				hpConfirm.isEnabled = false;
			}
			


 
			attCurValue.text = (heroCardItem.GetAttackBase() + heroCardItem.GetFengShuiAttc()).ToString();
			attAddValue.text = "+" + heroCardItem.GetAttackAdd().ToString();
			attCostMoney.text = ((heroCardItem.addQualityAtt + 1)*6000).ToString();
			//attCostItem.text = "1/" + attItemCount.ToString();
            if (isCanAttSth && heroCardItem.addQualityAtt < 99 && Obj_MyselfPlayer.GetMe().GetItemCountByType(ItemType.ATTACK) > 0)
			{
				attConfirm.isEnabled = true;
			}
			else
			{
				attConfirm.isEnabled = false;
			}


            //当强化到最高99次时不可强化
			/*
            if ()
            {
                attConfirm.isEnabled = false;
                //attConfirmBtnLabel.text = "强化已达上限";
               // attConfirmBtnLabel.color = Color.grey;
                //attSthValue.color = Color.white;
                //attSthValue.gameObject.SetActive(false);
            }
            else if (Obj_MyselfPlayer.GetMe().GetItemCountByType(ItemType.ATTACK) > 0)
            {
                attConfirm.isEnabled = true;
               // attConfirmBtnLabel.text = "大力丸";
               // attConfirmBtnLabel.color = Color.white;
               // attSthValue.color = Color.white;
               // attSthValue.gameObject.SetActive(true);
            }
            else
            {
                attConfirm.isEnabled = false;
               // attConfirmBtnLabel.text = "大力丸";
               // attConfirmBtnLabel.color = Color.white;
                //attSthValue.color = Color.red;
                attSthValue.gameObject.SetActive(true);
            }


            if (heroCardItem.addQualityHp == 99)
            {
                hpConfirm.isEnabled = false;
                hpComfirmBtnLabel.text = "强化已达上限";
                hpComfirmBtnLabel.color = Color.grey;
                hpStrengthenValue.color = Color.white;
                hpStrengthenValue.gameObject.SetActive(false);
            }
            else if (Obj_MyselfPlayer.GetMe().GetItemCountByType(ItemType.HP) > 0)
            {
                hpConfirm.isEnabled = true;
                hpComfirmBtnLabel.text = "强身丸";
                hpStrengthenValue.gameObject.SetActive(true);
                hpComfirmBtnLabel.color = Color.white;
                hpStrengthenValue.color = Color.white;
            }
            else
            {
                hpConfirm.isEnabled = false;
                hpComfirmBtnLabel.text = "强身丸";
                hpStrengthenValue.gameObject.SetActive(true);
                hpComfirmBtnLabel.color = Color.white;
                hpStrengthenValue.color = Color.red;
            }
            */
			
			if(Obj_MyselfPlayer.GetMe().GetItemCountByType(ItemType.HP) <= 99)
			{
				hpStrengthenValue.text = "X" + Obj_MyselfPlayer.GetMe().GetItemCountByType(ItemType.HP);
				hpAddIcon.SetActive(false);
			}
			else
			{
				hpStrengthenValue.text = "X" + 99;
				hpAddIcon.SetActive(true);
			}
            
			if(Obj_MyselfPlayer.GetMe().GetItemCountByType(ItemType.ATTACK) <= 99)
			{
				attSthValue.text = "X" + Obj_MyselfPlayer.GetMe().GetItemCountByType(ItemType.ATTACK);
				AttAddIcon.SetActive(false);
			}
			else
			{
				attSthValue.text = "X" + 99;
				AttAddIcon.SetActive(true);
			}
            


		}
		else
		{
			heroObject.gameObject.SetActive(false);
            addIcon.SetActive(true);
			heroLevel.gameObject.SetActive(false);
			//cb:这里需要默认显示数量

//			hpStrengthenValue.gameObject.SetActive(false);
			hpCurValue.gameObject.SetActive(false);
			hpAddValue.gameObject.SetActive(false);
			hpTimeValue.gameObject.SetActive(false);
			hpCostMoney.gameObject.SetActive(false);
			hpConfirm.isEnabled = false;
			//cb:这里需要默认显示数量
//			attSthValue.gameObject.SetActive(false);
			attCurValue.gameObject.SetActive(false);
			attAddValue.gameObject.SetActive(false);
			attTimeValue.gameObject.SetActive(false);
			attCostMoney.gameObject.SetActive(false);
			attConfirm.isEnabled = false;

            attConfirmBtnLabel.text = "大力丸";
            hpComfirmBtnLabel.text = "强身丸";
            attSthValue.gameObject.SetActive(true);
            hpStrengthenValue.gameObject.SetActive(true);
            attConfirmBtnLabel.color = Color.white;
            hpComfirmBtnLabel.color = Color.white;

			//hpStrengthInfo.SetActive(false);
			//AttStrengthInfo.SetActive(false);

             //药品数量为零的提示显示
            if (Obj_MyselfPlayer.GetMe().GetItemCountByType(ItemType.HP) > 0)
            {
                hpStrengthenValue.color = Color.white;
            }
            else
            {
                hpStrengthenValue.color = Color.red;
            }

            if(Obj_MyselfPlayer.GetMe().GetItemCountByType(ItemType.HP) <= 99)
			{
				hpStrengthenValue.text = "X" + Obj_MyselfPlayer.GetMe().GetItemCountByType(ItemType.HP);
				hpAddIcon.SetActive(false);
			}
			else
			{
				hpStrengthenValue.text = "X" + 99;
				hpAddIcon.SetActive(true);
			}
			

            if (Obj_MyselfPlayer.GetMe().GetItemCountByType(ItemType.ATTACK) > 0)
            {
                attSthValue.color = Color.white;
            }
            else
            {
                attSthValue.color = Color.red;
            }

           	if(Obj_MyselfPlayer.GetMe().GetItemCountByType(ItemType.ATTACK) <= 99)
			{
				attSthValue.text = "X" + Obj_MyselfPlayer.GetMe().GetItemCountByType(ItemType.ATTACK);
				AttAddIcon.SetActive(false);
			}
			else
			{
				attSthValue.text = "X" + 99;
				AttAddIcon.SetActive(true);
			}


		}
	}
	
	void Calculate()
	{
		if(heroCardItem == null)
		{
			Reset();
			return;
		}
		
		UserItem attItem = Obj_MyselfPlayer.GetMe().GetUserItemByID(10000);
		UserItem hpItem = Obj_MyselfPlayer.GetMe().GetUserItemByID(10001);
		if(attItem != null)
		{
			attItemCount = attItem.itemCount;
		}
		else
		{
			attItemCount = 0;
		}
		
		if(hpItem != null)
		{
			hpItemCount = hpItem.itemCount;
		}
		else
		{
			hpItemCount = 0 ;
		}
		
		
		if(hpItem != null && hpItemCount > 0 && heroCardItem.addQualityHp < 100)
		{
			isCanHpSth = true;
		}
		else
		{
			isCanHpSth = true;
		}
		if(attItem != null && attItemCount > 0 && heroCardItem.addQualityAtt < 100)
		{
			isCanAttSth = true;
		}
		else
		{
			isCanAttSth = true;
		}
		
		
	}

	void ClearUI()
	{
		
	}
	public void OnSelectHero()
	{
		/*
		if(GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.STRENGTHEN)
			GuideStrengthen.Instance.NextStep();////修炼指引 SELECT_2
		*/
		Obj_MyselfPlayer.GetMe().curCultivateType = CultivateType.STRENGTHEN;
		Obj_MyselfPlayer.GetMe().isSelectHero = true;
		GameObject.FindWithTag("main_ui_logic").GetComponent<MainUILogic>().OnCultivateSelectWindow();
	}
	public void OnSelectHeroRet(GameObject go)
	{
		foreach(UserCardItem temp in Obj_MyselfPlayer.GetMe().cardBagList)
		{
			if(temp.cardID == long.Parse(go.name))
			{
				heroCardItem = temp;
				break;
			}
		}
		RefreshUI(true);
	}
	public void OnHpPageBtn()
	{
		//isHpStrengthen = true;
		RefreshUI(false);
	}
	public void OnAttackPageBtn()
	{
		//isHpStrengthen = false;
		RefreshUI(false);
	}
	public void sendFinishStep(bool isSuccess)
	{
		/*
		if(GuideManager.GUIDE_STEP.STRENGTHEN == GuideManager.Instance.currentStep)
		{
			//GuideManager.Instance.getCurrentGuideWindow().GetComponent<GuideCardStrengthenController>().OnClickGuideItem(null);
		}
		*/
		//GuideManager.Instance.FinishedStep(GuideManager.GUIDE_STEP.STRENGTHEN);
		recFinishStep(isSuccess);
	}
	public void recFinishStep(bool isSuccess)
	{
		OnCardStrengthenRet(isSuccess);
	}
	public void OnHpStrengthenConfirm()
	{
		/*
		if(GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.STRENGTHEN)
			GuideStrengthen.Instance.NextStep();//修炼指引 SELECT_4
		*/
        //如果金钱不足，提示
        if (heroCardItem != null && heroCardItem.GetStrengthHpCostMoney()> Obj_MyselfPlayer.GetMe().money)
        {
//            BoxManager.showMessage("金钱不足");
//			BoxManager.showMessageByID((int)MessageIdEnum.Msg9);
			NetworkSender.Instance().buyGold(BuyGoldFinish,1);
            return;
        }
        

		hporattack=1;
		NetworkSender.Instance().CardStrengthen(OnCardStrengthenRet, heroCardItem, 1);

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
	
	public void OnAttackStrengthenConfirm()
	{
        //如果金钱不足，提示
        if (heroCardItem != null && heroCardItem.GetStrengthAttackCostMoney() > Obj_MyselfPlayer.GetMe().money)
        {
//            BoxManager.showMessage("金钱不足");
//			BoxManager.showMessageByID((int)MessageIdEnum.Msg9);
			NetworkSender.Instance().buyGold(BuyGoldFinish,1);
			return;
        }

		hporattack=0;
		/*
		if(GuideManager.GUIDE_STEP.STRENGTHEN == GuideManager.Instance.currentStep)
		{
			//GuideStrengthen.Instance.NextStep();//修炼指引 SELECT_5
			NetworkSender.Instance().CardStrengthen(sendFinishStep, heroCardItem, 0);
		}else
		{*/
			NetworkSender.Instance().CardStrengthen(OnCardStrengthenRet, heroCardItem, 0);/*
		}*/
	}
	
	//攻击力强化次数文本结束时调用的
	public void AttackTimeLabelTweenFinished()
	{
		attTimeValue.gameObject.SetActive(false);
        attTimeValue.transform.GetComponent<TweenAlpha>().enabled = false;
        attAddValue.gameObject.SetActive(true);
        attAddValue.transform.GetComponent<TweenAlpha>().enabled = true;
	}

    //攻击力增加标签 Tween结束时的调用
    public void AttackAddLabelTweenFinished()
    {
		attTimeValue.gameObject.SetActive(true);
        attTimeValue.transform.GetComponent<TweenAlpha>().enabled = true;
        attAddValue.gameObject.SetActive(false);
        attAddValue.transform.GetComponent<TweenAlpha>().enabled = false;
    }
	
	//血量强化次数Tween结束时调用的
	public void HpTimeLabelTweenFinished()
	{
		hpTimeValue.gameObject.SetActive(false);
        hpTimeValue.transform.GetComponent<TweenAlpha>().enabled = false;
        hpAddValue.gameObject.SetActive(true);
        hpAddValue.transform.GetComponent<TweenAlpha>().enabled = true;
	}

    //血量增加标签 Tween结束时的调用
    public void HpAddLabelTweenFinished()
    {
		hpTimeValue.gameObject.SetActive(true);
        hpTimeValue.transform.GetComponent<TweenAlpha>().enabled = true;
        hpAddValue.gameObject.SetActive(false);
        hpAddValue.transform.GetComponent<TweenAlpha>().enabled = false;
    }

	public void OnTopLeftBtn()
	{
		//王明磊 : 统计模块代码 -> Statistics
		//如果是Guide阶段,需要统计此按钮的点击信息
		if (GameManager.Instance.isGuideMode())
			PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn34).ToString());
		GameObject.FindWithTag("main_ui_logic").GetComponent<MainUILogic>().ReturnToMainUI();
	}
	
	//刷新强化次数
	private void FreshStrenthLeftTime()
	{
		hpTimeValue.text = heroCardItem.addQualityHp + "/99";
		attTimeValue.text = heroCardItem.addQualityAtt + "/99";
		
		if(heroCardItem.addQualityHp >= 99)
		{
			hpTimeValue.color = Color.red;
		}
		else
		{
			hpTimeValue.color = Color.yellow;
		}
		
		if(heroCardItem.addQualityAtt >= 99)
		{
			attTimeValue.color = Color.red;
		}
		else
		{
			attTimeValue.color = Color.yellow;
		}
		
	}
	
	public void OnCardStrengthenRet(bool bSucceed)
	{
		if(bSucceed)
		{
			/*
			if(GuideManager.GUIDE_STEP.STRENGTHEN == GuideManager.Instance.currentStep)
			{
				//GuideManager.Instance.getCurrentGuideWindow().GetComponent<GuideCardStrengthenController>().OnClickGuideItem(null);
			}
			*/
			heroCardItem = Obj_MyselfPlayer.GetMe().GetUserCardByGUID((long)heroCardItem.cardID);
			RefreshUI(false);
			RefreshTopBar();
			
			this.FreshStrenthLeftTime();
			if(hporattack==1)
			{
				AudioManager.Instance.PlayEffectSound("card_improve_life");
				//AudioController.Play("card_improve_life");
                strengAnimHp.SetActive(true);
                strengAnimHp.GetComponent<UISpriteAnimation>().enabled = true;
                
			}
			else
			{
				AudioManager.Instance.PlayEffectSound("card_improve_attack");
				//AudioController.Play("card_improve_attack");
				strengAnimAttack.SetActive(true);
				strengAnimAttack.GetComponent<UISpriteAnimation>().enabled=true;
			}
			
			
		}
		else
		{
			LogModule.ErrorLog("OnCardStrengthenRet(), card strengthen failed!");
		}
	}
	
	void RefreshTopBar()
	{
		GameObject.FindWithTag("main_controller").SendMessage("updateUserInfo");
	}
	
	//for guide
	public GameObject getHeroBgButton()
	{
		return this.transform.FindChild("MainHero/HeroBg").gameObject;
	}
	public GameObject getAttackButton()
	{
		return this.transform.FindChild("Attack_Confirm").gameObject;
	}
	public GameObject getHPButton()
	{
		return this.transform.FindChild("HP_Confirm").gameObject;
	}
	public GameObject getBackButton()
	{
		return this.transform.FindChild("TopLeftBtn").gameObject;
	}
}

