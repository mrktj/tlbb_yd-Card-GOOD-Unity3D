using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GCGame.Table;
using System;
using Games.CharacterLogic;

public class HeroInfoController : MonoBehaviour {

	private string heroInfoItem = "HeroInfoItem";
	public GameObject gridHero;
	public UILabel labelMaxPage;
	public UILabel labelNowPage;
	private GameObject mainLogic;
	//每页英雄数量
	private int maxPerPage=20;
	private int page=0;
	private int pagemax=0;
	private int cardIndex=0;
	private int nowMaxIndex=20;
	private GameObject newHero;
	private float dragX=0;
	
	private List<HeroInfo> heroList=new List<HeroInfo>();
	
	void Start()
	{
		mainLogic = GameObject.Find("MainUILogic");
	}
	void OnEnable()
	{
		heroList=Obj_MyselfPlayer.GetMe().heroList;
//		Hashtable cardList=TableManager.GetCard();
//		foreach(DictionaryEntry dic in cardList)
//		{
//			HeroInfo hero=new HeroInfo();
//			hero.templateId=Convert.ToInt32(dic.Key);
//			heroList.Add(hero);
//		}
		pagemax=heroList.Count/maxPerPage;
		if((double)heroList.Count/(double)maxPerPage>pagemax)
		{
			pagemax+=1;
		}
		if(maxPerPage>heroList.Count)
		{
			maxPerPage=heroList.Count;
		}
		//当前页最大index
		nowMaxIndex=maxPerPage;
		newHero = ResourceManager.Instance.loadWidget(heroInfoItem);//(GameObject)Instantiate(heroInfoItem);
		newHero.transform.parent = gridHero.transform;
		newHero.transform.localPosition = new Vector3(0,0,-1);
		newHero.transform.localScale = new Vector3(1,1,1);
		UIEventListener.Get(newHero).onDrag+=OnDragItem;
		UIEventListener.Get(newHero).onPress+=OnPressItem;
		for(;cardIndex<nowMaxIndex;cardIndex++)
		{
			GameObject button=newHero.GetComponent<HeroInfoItem>().heroList[cardIndex];
			button.name=heroList[cardIndex].templateId.ToString();
			UIEventListener.Get(button).onDrag+=OnDragItem;
			UIEventListener.Get(button).onPress+=OnPressItem;
			RefreshButton(button);
		}
		labelMaxPage.text=pagemax.ToString();
		gridHero.GetComponent<UIGrid>().repositionNow = true;
		labelNowPage.text=(page+1).ToString();
	}
	
	void OnDisable()
	{
		maxPerPage=15;
	 	page=0;
	 	pagemax=0;
	 	cardIndex=0;
	 	nowMaxIndex=0;
	 	dragX=0;
		Destroy(newHero);
	}
	
	public void OnPressItem(GameObject go,bool state)
	{
		if(state)
		{
			dragX=0;
		}
		else
		{
			if(dragX>0)
			{
				//前一页
				page-=1;
				if(page<0)
				{
					page=0;
					return;
				}
				for(int i=0;i<maxPerPage;i++)
				{
					GameObject button=newHero.GetComponent<HeroInfoItem>().heroList[i];
					button.name="";
					UISprite icon = button.transform.FindChild("Sprite-Icon").GetComponent<UISprite>();
					icon.spriteName = "";
					UIEventListener.Get(button).onClick-=OnHeroInfo;
				}
				if(page==pagemax-2)
				{
					cardIndex-=((heroList.Count%(maxPerPage*(pagemax-1)))+maxPerPage);
					nowMaxIndex-=(heroList.Count%(maxPerPage*(pagemax-1)));
				}
				else
				{
					cardIndex-=(maxPerPage+maxPerPage);
					nowMaxIndex-=maxPerPage;
				}
				int pageIndex=0;
				for(;cardIndex<nowMaxIndex;cardIndex++)
				{
					GameObject button=newHero.GetComponent<HeroInfoItem>().heroList[pageIndex];
					button.name=heroList[cardIndex].templateId.ToString();
					RefreshButton(button);
					pageIndex++;
				}
				labelNowPage.text=(page+1).ToString();
			}
			else if(dragX<0)
			{
				//后一页
				page+=1;
				if(page>=pagemax)
				{
					page=pagemax-1;
					return;
				}
				for(int i=0;i<maxPerPage;i++)
				{
					GameObject button=newHero.GetComponent<HeroInfoItem>().heroList[i];
					button.name="";
					UISprite icon = button.transform.FindChild("Sprite-Icon").GetComponent<UISprite>();
					icon.spriteName = "";
					UIEventListener.Get(button).onClick-=OnHeroInfo;
				}
				if(heroList.Count-cardIndex>maxPerPage)
				{
					nowMaxIndex+=maxPerPage;
				}
				else
				{
					nowMaxIndex+=heroList.Count-cardIndex;
				}
				int pageIndex=0;
				for(;cardIndex<nowMaxIndex;cardIndex++)
				{
					GameObject button=newHero.GetComponent<HeroInfoItem>().heroList[pageIndex];
					button.name=heroList[cardIndex].templateId.ToString();
					RefreshButton(button);
					pageIndex++;
				}
				labelNowPage.text=(page+1).ToString();
			}
		}
	}
	
	public void RefreshButton(GameObject button)
	{
		UISprite icon = button.transform.FindChild("Sprite-Icon").GetComponent<UISprite>();
		GameObject shadow=(button.transform.FindChild("BG-Label")).gameObject;
		if(heroList[cardIndex].state==0)
		{
			//问号
			icon.spriteName = TableManager.GetAppearanceByID(TableManager.GetCardByID(heroList[cardIndex].templateId).Appearance).HeadIcon;
			UIEventListener.Get(button).onClick+=OnHeroInfo;
			shadow.SetActive(false);
		}
		else if(heroList[cardIndex].state==1)
		{
			//阴影
			icon.spriteName = TableManager.GetAppearanceByID(TableManager.GetCardByID(heroList[cardIndex].templateId).Appearance).HeadIcon;
			UIEventListener.Get(button).onClick+=OnHeroInfo;
			shadow.SetActive(true);
		}
		else if(heroList[cardIndex].state==2)
		{
			//高亮
			icon.spriteName = TableManager.GetAppearanceByID(TableManager.GetCardByID(heroList[cardIndex].templateId).Appearance).HeadIcon;
			UIEventListener.Get(button).onClick+=OnHeroInfo;
			shadow.SetActive(false);
		}
	}
	
	public void OnDragItem(GameObject go,Vector2 delta)
	{
		dragX=delta.x;
	}
	
	public void OnHeroInfo(GameObject go)
	{
		Obj_MyselfPlayer.GetMe().heroWindowState=0;
		Obj_MyselfPlayer.GetMe().heroInfoTemplateId=int.Parse(go.name);
		mainLogic.SendMessage("OnHeroInfoDetailWindow");
	}
	
	public void ReturnToMainUI()
	{
		mainLogic.SendMessage("ReturnToMainUI");
	}
	
}
