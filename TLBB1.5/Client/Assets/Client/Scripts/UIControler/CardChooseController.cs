using UnityEngine;
using System.Collections;
using GCGame.Table;
using Games.LogicObject;
using System.Collections.Generic;
using card.net;
using card;
public class CardChooseController : MonoBehaviour {
	
	public UITexture[] hero_heti1;
	public UITexture hero_heti2;
	
	public UITexture hero_azhu;
	public UITexture hero_menggu;
	public UITexture hero_wangyuyan;
	
	//private GameObject mainLogic;
	
	private int[] defultCardTempleID = {1016,1012,1008};
	//记录上一个选中的卡牌ID , 默认 -1 为表示第一个卡牌
	private int lastCardId = -1;
		
	// Use this for initialization
	void Start () {
		
		StartCoroutine(AtlasManager.Instance.SetBodyNameWithShader(hero_azhu,"heti_azhu_4","GrayScale"));
		StartCoroutine(AtlasManager.Instance.SetBodyNameWithShader(hero_menggu,"heti_yinchuangongzhu_4","GrayScale"));
		StartCoroutine(AtlasManager.Instance.SetBodyNameWithShader(hero_wangyuyan,"heti_wangyuyan_4","Unlit/Transparent Colored"));
		
		//mainLogic = GameObject.Find("LoginLogic");
		
		for(int i=0;i<defultCardTempleID.Length;i++)
		{
			GameObject card = this.transform.FindChild("Sprites/" + defultCardTempleID[i]).gameObject;
			UIEventListener.Get(card).onClick += cardChange;
			if (int.Parse(card.name) == 1008)
				cardChange(card);
		}	
		GuideManager.Instance.checkGuideState();
		
		AudioManager.Instance.PlayBackgroundMusic("guide");
	}
	
	public void cardChange(GameObject item)
	{
		//王明磊 : 统计模块代码 -> Statistics
		Debug.Log("cardChange: " + item.name);
		int currentCardID = int.Parse(item.name);
		if (lastCardId != -1 && lastCardId != currentCardID)
		{
			GameObject lastItem = this.transform.FindChild("Sprites/" + lastCardId.ToString()).gameObject;
			UITexture lastTex = lastItem.transform.FindChild("Hero").gameObject.GetComponent<UITexture>();
			lastTex.shader = Shader.Find("GrayScale");
			UISprite lastMingzi = lastItem.transform.FindChild("mingzi").gameObject.GetComponent<UISprite>();
			if (!lastMingzi.spriteName.Contains("_hui"))
				lastMingzi.spriteName = lastMingzi.spriteName + "_hui";
		}
		UITexture cardTex = item.transform.FindChild("Hero").gameObject.GetComponent<UITexture>();
		cardTex.shader = Shader.Find("Unlit/Transparent Colored");
		UISprite mingzi = item.transform.FindChild("mingzi").gameObject.GetComponent<UISprite>();
		if (mingzi.spriteName.Contains("_hui"))
		{
			mingzi.spriteName = mingzi.spriteName.Replace("_hui","");
		}
		UISprite jiNengZhi = this.transform.FindChild("Sprites/Jineng-zi/Sprite").gameObject.GetComponent<UISprite>();
		
		for (int i = 0; i < 3; i++)
		{
			hero_heti1[i].gameObject.SetActive(false);
		}
		switch (currentCardID)
		{
		case 1008:
			jiNengZhi.spriteName = "wenzi_liumaishenjian_08";
			hero_heti1[1].gameObject.SetActive(true);
//			AtlasManager.Instance.SetBodyNameRemainScale(hero_heti1[1],"heti_wangyuyan_4");
			AtlasManager.Instance.SetBodyNameRemainScale(hero_heti2,"heti_duanyu_5");
			SetLabel(4018);
			PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn7).ToString());
			break;
		case 1012:
			jiNengZhi.spriteName = "wenzi_xianglongshibazhang_08";
//			AtlasManager.Instance.SetBodyNameRemainScale(hero_heti1[0],"heti_azhu_4");
			hero_heti1[0].gameObject.SetActive(true);
			AtlasManager.Instance.SetBodyNameRemainScale(hero_heti2,"heti_qiaofeng_5");
			SetLabel(4000);
			PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn6).ToString());
			break;
		case 1016:
			jiNengZhi.spriteName = "wenzi_tianshanliuyangzhang_08";
//			AtlasManager.Instance.SetBodyNameRemainScale(hero_heti1[2],"heti_yinchuangongzhu_4");
			hero_heti1[2].gameObject.SetActive(true);
			AtlasManager.Instance.SetBodyNameRemainScale(hero_heti2,"heti_xuzhu_5");
			SetLabel(4009);
			PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn5).ToString());
			break;
		}

		GuideManager.Instance.SetTempletID(currentCardID);//.selectedTempletID = ;
		lastCardId = currentCardID;
	}
	
	public void SetLabel(int num)
	{
		for (int i = 1; i <=9; i++)
		{
			UILabel label = this.transform.FindChild("Labels/" + i.ToString()).gameObject.GetComponent<UILabel>();
			label.text = TableManager.GetLanguageByID(num+i-1).Chinese;
		}
	}
	
	public void cardSelected()
	{
		string name;
		switch (lastCardId)
		{
		case 1008:
			name = "王语嫣";
			break;
		case 1012:
			name = "阿朱";
			break;
		case 1016:
			name = "梦姑";
			break;
		default:
			name = "阿朱";
			break;
		}
		BoxManager.showMessageByID((int)MessageIdEnum.Msg160,name);
		UIEventListener.Get(BoxManager.buttonYes).onClick += OnConfrimSelected;
		//王明磊 : 统计模块代码 -> Statistics
		PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn8).ToString());
//		mainLogic.SendMessage("OnCardInfoWindow");
	}
	
	void OnConfrimSelected(GameObject button)
	{
		//王明磊 : 统计模块代码 -> Statistics
		PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn9).ToString());
//		OnCreateNewUser("侠客",GuideManager.Instance.selectedTempletID);
//		BoxManager.showConfirmMessage("是否需要新手引导?");
		
		//新版本确认必须进行新手引导
		OnCreateNewUser(GuideManager.Instance.selectedTempletID);
//		BoxManager.showMessageByID((int)MessageIdEnum.Msg68);
//		UIEventListener.Get(BoxManager.getYesButton()).onClick += StartGuide;
//      UIEventListener.Get(BoxManager.getNoButton()).onClick += SkipGuide;	

	}
	
	void OnCreateNewUser(int cardTempletId)
	{
		string defaultName = "侠客";
		NetworkSender.Instance().createNewUser(OnCreateNewUserDone,defaultName,cardTempletId);
	}
	
	void OnCreateNewUserDone(bool isSuccess)
	{
//		NetworkSender.Instance().guideFinishStep(OnFinishGuideCardChooseDone,GuideManager.GUIDE_STEP.CARD_CHOOSE);
		GuideManager.Instance.FinishedStep(GuideManager.GUIDE_STEP.CARD_CHOOSE);
		GameManager.Instance.LoadLevel(Utils.UI_NAME_main);
	}

	void OnFinishGuideCardChooseDone(bool isSuccess)
	{
		GameManager.Instance.LoadLevel(Utils.UI_NAME_main);
	}
}
