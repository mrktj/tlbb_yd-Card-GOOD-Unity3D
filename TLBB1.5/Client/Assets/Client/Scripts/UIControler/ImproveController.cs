using UnityEngine;
using System.Collections;
using Games.CharacterLogic;
using System.Collections.Generic;
using GCGame.Table;
using card.net;
using Games.LogicObject;

public class ImproveController : MonoBehaviour {

	private MainUILogic mainLogic;
	private MainController mainController;
	public GameObject grid;
	public UIImageButton activeBtn;
	public UIImageButton resetBtn;
	private GameObject updateAni1 ;
	private	GameObject updateAni2 ;
	private	GameObject updateAni3 ;
	
	public int globalLastSelected = 0;
	public int[] lastSelected = {0,0,0,0,0};
	//升级消耗的类型 0-金碎片 1-木碎片 2-水碎片 3-火碎片 4-土碎片
	private Dictionary<int,string> ConsumeType = new Dictionary<int, string> {{0,"jin"},{1,"mu"},{2,"shui"},{3,"huo"},{4,"tu"}};
	private Dictionary<int,string> fengshuiName = new Dictionary<int, string> {{1,"top_dian"},{2,"left_dian"},{3,"right_dian"},{4,"bottom_dian"}};
	private Dictionary<int,int> wuxing = FengShuiData.Instance().WuxingInfor;
	// Use this for initialization
	void Start () {

	}
	
	void OnEnable () {
		mainLogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();	
		mainController = mainLogic.mainController.GetComponent<MainController>();
		mainController.hideTopBar();
		mainController.showBottomBar();
		
		RefreshFengShuiInfor();
		
		//默认点击的风水阵
		if (globalLastSelected == 0)
			TopClicked(transform.FindChild("Graph/Grid/1/top").gameObject);
		
	}
	
	public void TopClicked (GameObject button)
	{
		//大风水阵的序号
		int index = int.Parse(button.transform.parent.gameObject.name);
		changeClickLight(index,lastSelected[index-1],1);
		//记录第index大风水最后一次选择的小风水编号为1 -- WML
		lastSelected[index-1] = 1;
		globalLastSelected = (index-1)*4 + 1;
		InitInforBar(globalLastSelected);
	}
	
	public void LeftClicked (GameObject button)
	{
		//大风水阵的序号
		int index = int.Parse(button.transform.parent.gameObject.name);
		changeClickLight(index,lastSelected[index-1],2);
		lastSelected[index-1] = 2;
		globalLastSelected = (index-1)*4 + 2;
		InitInforBar(globalLastSelected);
	}
	
	public void RightClicked (GameObject button)
	{
		//大风水阵的序号
		int index = int.Parse(button.transform.parent.gameObject.name);
		changeClickLight(index,lastSelected[index-1],3);
		lastSelected[index-1] = 3;
		globalLastSelected = (index-1)*4 + 3;
		InitInforBar(globalLastSelected);
	}
	
	public void BottomClicked (GameObject button)
	{
		//大风水阵的序号
		int index = int.Parse(button.transform.parent.gameObject.name);
		changeClickLight(index,lastSelected[index-1],4);
		lastSelected[index-1] = 4;
		globalLastSelected = (index-1)*4 + 4;
		InitInforBar(globalLastSelected);
	}
	
//	public void CenterClicked (GameObject button)
//	{
//	    //大风水阵的序号
//		int index = int.Parse(button.transform.parent.gameObject.name);
//		Debug.LogError("Center -> ID : [" + button.transform.parent.gameObject.name+"]");
//	}
	
	//设置信息框内容
	private void InitInforBar(int id)
	{
		wuxing = FengShuiData.Instance().WuxingInfor;
		int lev = wuxing[id];
		bool isLargeFS = TableManager.GetFengshuiByID(id).Type == 1 ? false : true;
		int maxLev = 10;
		//设置按钮的显示 大风水不显示 小风水显示
		if (activeBtn == null || resetBtn == null)
			return;
		activeBtn.isEnabled = true;
		resetBtn.isEnabled = true;
		activeBtn.gameObject.SetActive(!isLargeFS);
		resetBtn.gameObject.SetActive(!isLargeFS);
		transform.FindChild("Sprite/heidi").gameObject.SetActive(!isLargeFS);
		//控制消耗的显示 大风水不显示 小风水显示
		GameObject xiaohao = transform.FindChild("Xiaohao").gameObject;
		xiaohao.SetActive(!isLargeFS);
		
		//控制显示第一行
		string text = TableManager.GetLanguageByID(TableManager.GetFengshuiByID(id).EffectDesc).Chinese;
		int improve1 = 0;
		int improve2 = 0;
		int countOfEffect = 0;
		int showLev = lev;
		if (lev == 0)
		{
			text = "激活后： " + text;
			showLev = 1;		
		}
		if (TableManager.GetFengshuiByID(id).GetEffectDatabyIndex(0) != -1)
		{
			improve1 = TableManager.GetFengshuiByID(id).GetEffectDatabyIndex(0)*showLev;
			countOfEffect++;
		}
		if (TableManager.GetFengshuiByID(id).GetEffectDatabyIndex(1) != -1)
		{
			improve2 = TableManager.GetFengshuiByID(id).GetEffectDatabyIndex(1)*showLev;
			countOfEffect++;
		}
		if (countOfEffect == 1)
			text = string.Format(text,showLev.ToString(),improve1.ToString());
		else if (countOfEffect == 2)
			text = string.Format(text,showLev.ToString(),improve1.ToString(),improve2.ToString());
		transform.FindChild("Labels/line1").gameObject.GetComponent<UILabel>().text = text;
		//控制显示第二行及信息框其他内容
		GameObject line2 = transform.FindChild("Labels/line2").gameObject;
		
		//等级为零 未开启
		if (lev == 0)
		{
			line2.SetActive(isLargeFS);
			if (isLargeFS) //如果是大风水
				line2.GetComponent<UILabel>().text = TableManager.GetLanguageByID(TableManager.GetFengshuiByID(id).ActiveClaim).Chinese;
			else {
				xiaohao.transform.FindChild("item").gameObject.GetComponent<UISprite>().spriteName = "jinlong";
				int consume = TableManager.GetFengshuiByID(id).GetActivationbyIndex(0);
				xiaohao.transform.FindChild("number").gameObject.GetComponent<UILabel>().text = consume.ToString();
				resetBtn.isEnabled = false;
				activeBtn.isEnabled = true;
				activeBtn.transform.FindChild("Sprite").gameObject.GetComponent<UISprite>().spriteName = "jihuo";
			}
		}else if (lev > 0 && lev < maxLev) //已开启 未满级
		{
			text = TableManager.GetLanguageByID(TableManager.GetFengshuiByID(id).EffectDesc).Chinese;
			line2.SetActive(true);
			string str;
			if (isLargeFS)//如果是大风水
			{
				str = TableManager.GetLanguageByID(TableManager.GetFengshuiByID(id).NextlevClaim).Chinese;
				str = string.Format(str,lev+1);
			}else{
				improve1 = (improve1/showLev)*(showLev+1);
				improve2 = (improve2/showLev)*(showLev+1);
				if (countOfEffect == 1)
					text = string.Format(text,(showLev+1).ToString(),improve1.ToString());
				else if (countOfEffect == 2)
					text = string.Format(text,(showLev+1).ToString(),improve1.ToString(),improve2.ToString());
				str = "下一级： " + text;
				int consumeType = TableManager.GetFengshuiByID(id).LevelItemId;
				xiaohao.transform.FindChild("item").gameObject.GetComponent<UISprite>().spriteName = ConsumeType[consumeType];
				xiaohao.transform.FindChild("number").gameObject.GetComponent<UILabel>().text = TableManager.GetFengshuiByID(id).LevelItemCount.ToString();
				resetBtn.isEnabled = true;
				activeBtn.isEnabled = true;
				activeBtn.transform.FindChild("Sprite").gameObject.GetComponent<UISprite>().spriteName = "update";
			}

			line2.GetComponent<UILabel>().text = str;
		}else if (lev == 10){ //已开启 已满级
			line2.SetActive(false);
			xiaohao.SetActive(false);
			transform.FindChild("Sprite/heidi").gameObject.SetActive(false);
			if (!isLargeFS)//如果不是大风水
			{
				resetBtn.isEnabled = true;
				activeBtn.isEnabled = false;
				activeBtn.transform.FindChild("Sprite").gameObject.GetComponent<UISprite>().spriteName = "update";
			}
		}else
			Debug.LogError("Error : FengShui Lev = " + lev);

//		index += 3999;
//		string text = TableManager.GetLanguageByID(index).Chinese;
//		transform.FindChild("Labels/line1").gameObject.GetComponent<UILabel>().text = text;
//		text = TableManager.GetLanguageByID(index+1).Chinese;
//		transform.FindChild("Labels/line2").gameObject.GetComponent<UILabel>().text = text;
	}
	
	public void RefreshFengShuiInfor()
	{
		wuxing = FengShuiData.Instance().WuxingInfor;
//		for(int i = 0;i<grid.transform.childCount;i++)
//		{
//			GameObject go = grid.transform.GetChild(i).gameObject;
//			Destroy(go);
//		}
		bool hadItem = false;
		if(grid.transform.childCount > 0)
			hadItem = true;
		for (int i = 1; i <= 5; i++)
		{
			//如果Grid不为空,不添加新的item
			GameObject item;
			if(!hadItem)
			{
				item = ResourceManager.Instance.loadWidget("ImproveItem");
				item.transform.parent = grid.transform;
				item.transform.localPosition = new Vector3(0, 0, -2);
        		item.transform.localScale = new Vector3(1, 1, 1);
				item.name = i.ToString();
			}else
				item = transform.FindChild("Graph/Grid/" + i).gameObject;
			
			//关闭风水升级动画
			item.transform.FindChild("update_ani1").gameObject.SetActive(false);
			item.transform.FindChild("update_ani2").gameObject.SetActive(false);
			item.transform.FindChild("update_ani3").gameObject.SetActive(false);
			
			//设置风水的名字和等级
			item.transform.FindChild("topName").gameObject.GetComponent<UILabel>().text = TableManager.GetFengshuiByID((i-1)*4+1).Name;
			item.transform.FindChild("leftName").gameObject.GetComponent<UILabel>().text = TableManager.GetFengshuiByID((i-1)*4+2).Name;
			item.transform.FindChild("rightName").gameObject.GetComponent<UILabel>().text = TableManager.GetFengshuiByID((i-1)*4+3).Name;
//			Debug.LogError(" i = " + i + " / (i-1)*4+1 = " + ((i-1)*4+1));
			
			item.transform.FindChild("topLev").gameObject.GetComponent<UILabel>().text = "Lv: " + wuxing[(i-1)*4+1].ToString();
			item.transform.FindChild("leftLev").gameObject.GetComponent<UILabel>().text = "Lv: " + wuxing[(i-1)*4+2].ToString();
			item.transform.FindChild("rightLev").gameObject.GetComponent<UILabel>().text = "Lv: " + wuxing[(i-1)*4+3].ToString();
			item.transform.FindChild("topLev").gameObject.SetActive(wuxing[(i-1)*4+1] > 0 ? true : false);
			item.transform.FindChild("leftLev").gameObject.SetActive(wuxing[(i-1)*4+2] > 0 ? true : false);
			item.transform.FindChild("rightLev").gameObject.SetActive(wuxing[(i-1)*4+3] > 0 ? true : false);
			
			
			//设置风水阵的icon
			GameObject topPic = item.transform.FindChild("top_pic").gameObject;
			GameObject leftPic = item.transform.FindChild("left_pic").gameObject;
			GameObject rightPic = item.transform.FindChild("right_pic").gameObject;
			GameObject bottomPic = item.transform.FindChild("bottom_pic").gameObject;	
			
			topPic.GetComponent<UISprite>().spriteName = TableManager.GetFengshuiByID((i-1)*4+1).Icon;
			leftPic.GetComponent<UISprite>().spriteName = TableManager.GetFengshuiByID((i-1)*4+2).Icon;
			rightPic.GetComponent<UISprite>().spriteName = TableManager.GetFengshuiByID((i-1)*4+3).Icon;
			
			//添加点击检测
			UIEventListener.Get(topPic).onClick += TopClicked;
			UIEventListener.Get(leftPic).onClick += LeftClicked;
			UIEventListener.Get(rightPic).onClick += RightClicked;
			UIEventListener.Get(bottomPic).onClick += BottomClicked;
			
			//设置风水阵的状态
			item.transform.FindChild("top_ani").gameObject.SetActive(wuxing[(i-1)*4+1] > 0 ? true : false);
			item.transform.FindChild("left_ani").gameObject.SetActive(wuxing[(i-1)*4+2] > 0 ? true : false);
			item.transform.FindChild("right_ani").gameObject.SetActive(wuxing[(i-1)*4+3] > 0 ? true : false);
			item.transform.FindChild("top_zhe").gameObject.SetActive(wuxing[(i-1)*4+1] > 0 ? false : true);
			item.transform.FindChild("left_zhe").gameObject.SetActive(wuxing[(i-1)*4+2] > 0 ? false : true);
			item.transform.FindChild("right_zhe").gameObject.SetActive(wuxing[(i-1)*4+3] > 0 ? false : true);
			if ( wuxing[(i-1)*4+1] > 0 && wuxing[(i-1)*4+2] > 0 && wuxing[(i-1)*4+3] > 0 )
			{
				item.transform.FindChild("bottom_bg").gameObject.GetComponent<UISprite>().spriteName = "dajineng_jihuo";
				bottomPic.GetComponent<UISprite>().spriteName = TableManager.GetFengshuiByID((i-1)*4+4).Icon + "_chufa";
			}else
			{
				item.transform.FindChild("bottom_bg").gameObject.GetComponent<UISprite>().spriteName = "dajineng";
				bottomPic.GetComponent<UISprite>().spriteName = TableManager.GetFengshuiByID((i-1)*4+4).Icon;
			}
		}
		if (!FengShuiData.Instance().SuipianInfor.ContainsKey(0))
			FengShuiData.Instance().SuipianInfor.Add(0,0);
		transform.FindChild("Labels/jin").gameObject.GetComponent<UILabel>().text = FengShuiData.Instance().SuipianInfor[0].ToString();
		if (!FengShuiData.Instance().SuipianInfor.ContainsKey(1))
			FengShuiData.Instance().SuipianInfor.Add(1,0);
		transform.FindChild("Labels/mu").gameObject.GetComponent<UILabel>().text = FengShuiData.Instance().SuipianInfor[1].ToString();
		if (!FengShuiData.Instance().SuipianInfor.ContainsKey(2))
			FengShuiData.Instance().SuipianInfor.Add(2,0);
		transform.FindChild("Labels/shui").gameObject.GetComponent<UILabel>().text = FengShuiData.Instance().SuipianInfor[2].ToString();
		if (!FengShuiData.Instance().SuipianInfor.ContainsKey(3))
			FengShuiData.Instance().SuipianInfor.Add(3,0);
		transform.FindChild("Labels/huo").gameObject.GetComponent<UILabel>().text = FengShuiData.Instance().SuipianInfor[3].ToString();
		if (!FengShuiData.Instance().SuipianInfor.ContainsKey(4))
			FengShuiData.Instance().SuipianInfor.Add(4,0);
		transform.FindChild("Labels/tu").gameObject.GetComponent<UILabel>().text = FengShuiData.Instance().SuipianInfor[4].ToString();

		transform.FindChild("Labels/count").gameObject.GetComponent<UILabel>().text = FengShuiData.Instance().star.ToString();
	}
	
	public void changeClickLight(int index, int side, int curSide)
	{
		//缓存升级动画
		updateAni1 = transform.FindChild("Graph/Grid/"+index+"/update_ani1").gameObject;
		updateAni2 = transform.FindChild("Graph/Grid/"+index+"/update_ani2").gameObject;
		updateAni3 = transform.FindChild("Graph/Grid/"+index+"/update_ani3").gameObject;
		
		if (side == curSide)
			return;
		string sideName;
		if (side != 0)
		{
			sideName = fengshuiName[side];
			transform.FindChild("Graph/Grid/" + index + "/" + sideName).gameObject.SetActive(false);
		}
		sideName = fengshuiName[curSide];
		transform.FindChild("Graph/Grid/" + index + "/" + sideName).gameObject.SetActive(true);
	}
	
	public void ActiveBtnClicked()
	{
		if (globalLastSelected <= 0)
		{
			Debug.LogError("Global Last Selected Error : " + globalLastSelected);
			return;
		}
		if ( activeBtn.transform.FindChild("Sprite").gameObject.GetComponent<UISprite>().spriteName == "update") //如果是升级
		{
			int consumeType = TableManager.GetFengshuiByID(globalLastSelected).LevelItemId;
			if (TableManager.GetFengshuiByID(globalLastSelected).LevelItemCount > FengShuiData.Instance().SuipianInfor[consumeType])
			{
				BoxManager.showMessageByID((int)MessageIdEnum.Msg183);
				UIEventListener.Get(BoxManager.buttonYes).onClick += GoPVEWindow;
				return;
			}
			NetworkSender.Instance().updateFengshui(UpdateDone,globalLastSelected);
		}
		else
		{
			if (TableManager.GetFengshuiByID(globalLastSelected).GetActivationbyIndex(0) > FengShuiData.Instance().star)
			{
				Debug.Log("Request : " + TableManager.GetFengshuiByID(globalLastSelected).GetActivationbyIndex(0));
				BoxManager.showMessageByID((int)MessageIdEnum.Msg187);
				UIEventListener.Get(BoxManager.buttonYes).onClick += GoPVEWindow;
				return;
			}
			NetworkSender.Instance().activeFengshui(ActiveDone,globalLastSelected);
		}
	}
	
	public void GoPVEWindow (GameObject button)
	{
		mainLogic.LoadMainToPveSceneList();
	}
	
	public void ResetBtnClicked()
	{
		if (globalLastSelected <= 0)
		{
			Debug.LogError("Global Last Selected Error : " + globalLastSelected);
			return;
		}
		if (wuxing[globalLastSelected] > 1)
		{
			double returnNum = TableManager.GetFengshuiByID(globalLastSelected).LevelItemCount * 0.5 * (wuxing[globalLastSelected]-1);
			BoxManager.showMessageByID((int)MessageIdEnum.Msg181,returnNum.ToString());
			UIEventListener.Get(BoxManager.buttonYes).onClick += ResetSure;
		}
		else
		{
			BoxManager.showMessageByID((int)MessageIdEnum.Msg182);
			UIEventListener.Get(BoxManager.buttonYes).onClick += ResetSure;
		}
	}
	
	public void ResetSure (GameObject button)
	{
		NetworkSender.Instance().resetFengshui(ResetDone,globalLastSelected);
	}
	
	public void ActiveDone(bool isSuccess)
	{
		if (isSuccess)
		{
			//激活成功
			Debug.Log("Active Fengshui Success!");
			RefreshFengShuiInfor();
			InitInforBar(globalLastSelected);
			showAnimation();
		}
		else
			BoxManager.showMessage("风水激活失败",ClientConfigure.title); //WML MARK
			//激活失败
//			Debug.LogError("Active Fengshui Failed!");
	}
	public void UpdateDone(bool isSuccess)
	{
		if (isSuccess)
		{
			//升级成功
//			Debug.LogError("Update Fengshui Success!");
			RefreshFengShuiInfor();
			InitInforBar(globalLastSelected);
			showAnimation();
		}
		else
			BoxManager.showMessage("风水升级失败",ClientConfigure.title); //WML MARK
			//升级失败
//			Debug.LogError("Update Fengshui Failed!");
	}
	public void ResetDone(bool isSuccess)
	{
		if (isSuccess)
		{
			//重置成功
//			Debug.LogError("Reset Fengshui Success!");
			RefreshFengShuiInfor();
			InitInforBar(globalLastSelected);
		}
		else
			BoxManager.showMessage("风水重置失败",ClientConfigure.title); //WML MARK
			//重置失败
//			Debug.LogError("Reset Fengshui Failed!");
	}
	public void ReturnToMainUI()
	{
		mainLogic.ReturnToMainUI();
	}
	
	public void showAnimation()
	{
		int curIndex = globalLastSelected / 4 + 1;
		int curFengshui = globalLastSelected % 4;
		updateAni1.SetActive(true);
		updateAni1.transform.localPosition = transform.FindChild("Graph/Grid/" + curIndex + "/" + fengshuiName[curFengshui]).localPosition;
		updateAni1.GetComponent<UISpriteAnimation>().Reset();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (updateAni1.activeSelf && !updateAni1.GetComponent<UISpriteAnimation>().isPlaying)
		{
			updateAni1.SetActive(false);
			updateAni2.SetActive(true);
			int curFengshui = globalLastSelected % 4;
			int rotation = 0;
			switch (curFengshui)
			{
			case 2:
				rotation = 45;
				break;
			case 3:
				rotation = -45;
				break;
			default:
				rotation = 0;
				break;
			}
			updateAni2.transform.localRotation = Quaternion.AngleAxis(rotation, Vector3.forward);
			updateAni2.GetComponent<UISpriteAnimation>().Reset();
		}
		else if (updateAni2.activeSelf && !updateAni2.GetComponent<UISpriteAnimation>().isPlaying)
		{
			updateAni2.SetActive(false);
			updateAni3.SetActive(true);
			updateAni3.GetComponent<UISpriteAnimation>().Reset();
		}
		else if (updateAni3.activeSelf && !updateAni3.GetComponent<UISpriteAnimation>().isPlaying)
		{
			updateAni3.SetActive(false);
		}
	}
	
	void OnDisable()
	{
		mainController.showTopBar();
	}
}
