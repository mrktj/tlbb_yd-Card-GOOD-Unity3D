using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardListItemPool{
	
	private static GameObject gObj = null;
	private static CardListItemPool _instance = null;
	
	// 数组的最大值
	   private int nMaxItemtype = 50;
	// 索引
       private int nIndex = 0;
	// 对应CardItemMap 的名字
       private string[] ListName;
	// cardItem的List 数组
       private BetterList<GameObject>[] cardItemMap;
	
	// 没一种类型Item在池中的最大值
       private int nEachTypeItemMaxNum = 20;
	
	public CardListItemPool()
	{
		ListName = new string[nMaxItemtype];
		cardItemMap = new BetterList<UnityEngine.GameObject>[nMaxItemtype];
	}
	
	
	public static CardListItemPool Instance
	{
		get
		{
			if(_instance == null)
			{
				//Transform panelTrans = UICamera.currentCamera.transform.Find("Anchor/Panel");
				gObj = new GameObject();
				gObj.name = "CardListItemPool";
				//gObj.transform.parent = panelTrans;
				gObj.transform.localScale = new Vector3(1,1,1);
				gObj.SetActive(false);
				GameObject.DontDestroyOnLoad(gObj);
				_instance = new CardListItemPool();
			}
			return _instance;			
		}

	}
	
	// 获得Item
	public GameObject GetListItem(string strItemName)
	{
		GameObject itemOBj = null;
		
		BetterList<GameObject> list = GetList(strItemName);
		if(list != null)
		{
			if(list.size > 0)
			{
				itemOBj = list[0];
				list.RemoveAt(0);
			}
		}
		
		if(itemOBj == null)
		{
			itemOBj = ResourceManager.Instance.loadWidget(strItemName);
		}
		
		itemOBj.name = "Item";
		itemOBj.SetActive(true);
		//itemOBj.transform.parent = null;	
		return itemOBj;
	}
	
	
	// 获得List
	private BetterList<GameObject> GetList(string strItemName)
	{
		for(int i=0; i<nIndex; i++)
		{
			if(strItemName == ListName[i])
			{
				return cardItemMap[i];
			}
		}
		
		return null;
	}
	
	
	// 隐藏Item 放入池中
	public void DestroyItemAndPushToPool(GameObject goItem, string strName)
	{
		if(goItem == null)
		{
			return;
		}
		
		BetterList<GameObject> list = GetList(strName);
		
		if(list == null)
		{
			ListName[nIndex] = strName;
			cardItemMap[nIndex] = new BetterList<GameObject>();
			list = cardItemMap[nIndex];
			nIndex++;
		}
		
		//goItem.transform.GetComponent<UIDragPanelContents>().draggablePanel = null;
		UIEventListener[] events =  goItem.transform.GetComponentsInChildren<UIEventListener>(true);
		for(int i=0; i<events.Length; i++)
		{
			events[i].onClick = null;
			events[i].onPress = null;
		}
		
		if(gObj == null)
		{
			Debug.Log("gObj == null");
			return;
		}
		
		goItem.transform.parent = gObj.transform;
		goItem.SetActive(false);
		//goItem.name = "Item";
		if(list.size >= nEachTypeItemMaxNum)
		{
			GameObject.Destroy(goItem);
			return;
		}
	    list.Add(goItem);
	}
	
	//清除池中的Item
	public void ClearItemsInPool(string itemTypeName)
	{
		BetterList<GameObject> list = GetList(itemTypeName);
		if(list == null)
		{
			return;
		}
		
		foreach(var obj in list)
		{
			obj.transform.parent = null;
			GameObject.Destroy(obj);
		}
		
		list.Clear();
	}
}
