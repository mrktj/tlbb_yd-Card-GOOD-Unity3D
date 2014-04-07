using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Games.LogicObject;
using Games.CharacterLogic;



/************************************************************************/
/* zbz编辑 
 * (1)  在显示前要先设置  "onCalculateShowItemsFunction" 函数，用来计算当前要显示的卡牌，以List的形式返回
   (2)  在每次显示前会调用  "onShowCardItemFunction" 函数
 * (3)  在所有显示调用完后会调用 "onShowCardItemCompleteFunction" 函数
 * (4)  每次拖拽结束时 会调用 "onDragonFinishedClearItemsFun" ,在这里进行清理上个页面显示的item
 *  
 * 以上的函数都为代理函数，必须设置才能有调用效果
   */
/************************************************************************/
public class BagItemsPage : MonoBehaviour {

    //窗口控制器所在的GameObject
    public GameObject windowControl = null;

    //排序规则函数
    public delegate List<UserCardItem> CalculateShowItemsFunction();
    public CalculateShowItemsFunction onCalculateShowItemsFunction = null;

    public delegate List<UserFriend> CalculateShowFriendsItemsFunction();
    public CalculateShowFriendsItemsFunction onCalculateShowFriendsItemsFunction = null;
	
	public delegate List<AssistFriend> CalculateShowAssistFriendItemsFunction();
	public CalculateShowAssistFriendItemsFunction onCalculateShowAssistFriendItemsFunction = null;
	
	public delegate List<PurchaseRecord> CalculateShowPurchaseRecordItemsFunction();
	public CalculateShowPurchaseRecordItemsFunction onCalculateShowPurchaseRecordItemsFunction = null;
	
    //----拖拽界面（要拖拽的界面）
    public GameObject goDragPanel;
    //拖拽完后清理之前item用的回调函数
    public UIDraggablePanel.OnDragFinished onDragonFinishedClearItemsFun;

    //显示item时调用的
    public delegate void ShowCardItem(UserCardItem cardItem);
    public ShowCardItem onShowCardItemFunction = null;

    public delegate void ShowFriendsCardItem(UserFriend FriendItem);
    public ShowFriendsCardItem onShowFriendItemFunction = null;
	
	public delegate void ShowAssistFriendItem(AssistFriend AssistFrienditem);
	public ShowAssistFriendItem onShowAssistFriendItemFunction = null;
	
	public delegate void ShowPurchaseRecordItem(PurchaseRecord PurchaseRecordItem);
	public ShowPurchaseRecordItem onShowPurchaseRecordItemsFunction = null;

    //显示item显示完的时候调用的
    public delegate void ShowCardItemComplete();
    public ShowCardItemComplete onShowCardItemCompleteFunction = null;


    //物品的最大显示数量
    public int nItemsMaxNum = 20;
    //当前页面
    private int nCurPage = 1;
    //最大页
    private int nMaxPage = 0;
    //记录是否开始分页
    private bool bStart = false;
    //处理过的Items
    private List<UserCardItem> cardItems;
    //处理过的FriendItems
    private List<UserFriend> FriendsItems;
	//处理过的AssistFriendItems
	private List<AssistFriend> AssistFriendItems;
	//处理过的PurchaseRecordItems
	private List<PurchaseRecord> PurchaseRecordItems;
 
    //标记向左，向右拖拽 Sprite
    public GameObject JianTouLeft;          //----向左的箭头
    public GameObject JianTouLeftHui;       //----向左的灰态箭头
    public GameObject JianTouRight;         //----向右的箭头
    public GameObject JianTouRightHui;      //----向右的灰态箭头

    //分页显示
    public UILabel pageLabel;

    //背包数量显示
    public UILabel pageCardNumLabel;

    //判断是否是第一次进入
    private bool bFirst = true;

   public enum JianTouMode
	{
		PageMini,  //页数最小
		Normal,    //在中间
		PageMax,   //页数最大
	}

    /// <summary>
    /// 此处是要控制显示的一些文本和图片
    /// 
    /// </summary>



    void OnEnable()
    {


    }

	// Use this for initialization
	void Start () {
        if (goDragPanel != null)
	    {
            goDragPanel.GetComponent<UIDraggablePanel>().onDragFinished += OnDragonItemFinished;
	    }
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//重置列表
	public void Reset()
	{
		nCurPage = 1;
		this.FreshShow();
	}
	

    //刷新显示
	public  void FreshShow()
	{
        //拖拽完的回调
        if (onDragonFinishedClearItemsFun != null)
        {
            onDragonFinishedClearItemsFun(UIDraggablePanel.DragonMode.None);
        }

        this.CalculteShowItem();

        //刷新箭头
        this.FreshShowJianTouAndLabel();
	}

    //计算要显示的Item,并返回
    public  void CalculteShowItem()
    {

        if (!bStart)
        {
            //如果还没开始就返回
            return;
        }
		
		int e_count = 0;
		
        if (onCalculateShowItemsFunction != null)
        {
            cardItems = onCalculateShowItemsFunction();
			e_count = cardItems.Count;
        }
        else if (onCalculateShowFriendsItemsFunction != null)
        {
            FriendsItems = onCalculateShowFriendsItemsFunction();
			e_count = FriendsItems.Count;
        }
		else if (onCalculateShowAssistFriendItemsFunction != null)
		{
			AssistFriendItems = onCalculateShowAssistFriendItemsFunction();
			e_count = AssistFriendItems.Count;
		}
		else if (onCalculateShowPurchaseRecordItemsFunction != null)
		{
			PurchaseRecordItems = onCalculateShowPurchaseRecordItemsFunction();
			e_count = PurchaseRecordItems.Count;
		}
//         else //如果 onSortItemsFunction 没有设置，直接拿背包里的Items
//         {
//             cardItems = Obj_MyselfPlayer.GetMe().cardBagList;
//         }

        //分页显示背包中的东西
        if (e_count <= nItemsMaxNum)
        {
            nMaxPage = 1;
        }
        else if (e_count % nItemsMaxNum != 0)
        {
            nMaxPage = e_count / nItemsMaxNum + 1;
        }
        else
        {
            nMaxPage = e_count / nItemsMaxNum;
        }
		
		List<UserCardItem> cardList = new List<UserCardItem>();
		List<UserFriend> friednList = new List<UserFriend>();
		List<AssistFriend> assistFriendList = new List<AssistFriend>();
		List<PurchaseRecord> purchaseRecordList = new List<PurchaseRecord>();
		if(cardItems != null){
	        for (int i = (nCurPage - 1) * nItemsMaxNum; i < nCurPage * nItemsMaxNum; i++)
	        {
	            if (i < cardItems.Count)
	            {
	                cardList.Add(cardItems[i]);
	            }
	        }
		}
		else if(FriendsItems != null){
			if(nCurPage > nMaxPage){
				nCurPage = nMaxPage;
			}
			for (int i = (nCurPage - 1) * nItemsMaxNum; i < nCurPage * nItemsMaxNum; i++)
	        {
	            if (i < FriendsItems.Count)
	            {
	                friednList.Add(FriendsItems[i]);
	            }
	        }
		}
		else if(AssistFriendItems != null)
		{
			for (int i = (nCurPage - 1) * nItemsMaxNum; i < nCurPage * nItemsMaxNum; i++)
	        {
	            if (i < AssistFriendItems.Count)
	            {
	                assistFriendList.Add(AssistFriendItems[i]);
	            }
	        }
		}
		else if(PurchaseRecordItems != null)
		{
			for (int i = (nCurPage - 1) * nItemsMaxNum; i < nCurPage * nItemsMaxNum; i++)
	        {
	            if (i < PurchaseRecordItems.Count)
	            {
	                purchaseRecordList.Add(PurchaseRecordItems[i]);
	            }
	        }
		}

        //所在控制的窗口不为空时，调用函数 ShowCardItem()
        if (windowControl != null)
        {

            if (onCalculateShowItemsFunction != null)
            {
                foreach (UserCardItem cardItem in cardList)
                {
                    if (onShowCardItemFunction != null)
                    {
                        onShowCardItemFunction(cardItem);
                    }
                    //windowControl.SendMessage("ShowCardItem", cardItem, SendMessageOptions.DontRequireReceiver);
                }
            }
            else if (onCalculateShowFriendsItemsFunction != null)
            {
                foreach (UserFriend friendItem in friednList)
                {
                    if (onShowFriendItemFunction != null)
                    {
                        onShowFriendItemFunction(friendItem);
                    }
                    //windowControl.SendMessage("ShowCardItem", cardItem, SendMessageOptions.DontRequireReceiver);
                }
            }
            else if (onCalculateShowAssistFriendItemsFunction != null)
            {
                foreach (AssistFriend assistFriendItem in assistFriendList)
                {
                    if (onShowAssistFriendItemFunction != null)
                    {
                        onShowAssistFriendItemFunction(assistFriendItem);
                    }
                    //windowControl.SendMessage("ShowCardItem", cardItem, SendMessageOptions.DontRequireReceiver);
                }
            }
			else if (onCalculateShowPurchaseRecordItemsFunction != null)
			{
				foreach (PurchaseRecord purchaseRecordItem in purchaseRecordList)
                {
                    if (onShowPurchaseRecordItemsFunction != null)
                    {
                        onShowPurchaseRecordItemsFunction(purchaseRecordItem);
                    }
                    //windowControl.SendMessage("ShowCardItem", cardItem, SendMessageOptions.DontRequireReceiver);
                }
			}

            //结束时调用 ShowCardItemComplete()
            if (onShowCardItemCompleteFunction != null)
            {
                onShowCardItemCompleteFunction();
            }
            //windowControl.SendMessage("ShowCardItemComplete");
        }

        //刷新
        this.FreshShowJianTouAndLabel();

        //第一次不掉重置Panel，不然会蹦
        if (!bFirst)
        {
            this.resetPanel();
        }
        bFirst = false;
       
    }

    //重置Panel,调整坐标
    public void resetPanel()
	{
         if (goDragPanel != null)
         {
            goDragPanel.transform.GetComponent<UIDraggablePanel>().verticalScrollBar.scrollValue = 0;
            goDragPanel.transform.GetComponent<UIDraggablePanel>().UpdateScrollbars(true);
            goDragPanel.transform.GetComponent<UIDraggablePanel>().ResetPosition();
             
             //UIGrid grid = goDragPanel.transform.GetComponentInChildren<UIGrid>();
			 UIGrid[] grids = goDragPanel.transform.GetComponentsInChildren<UIGrid>();
			 if(grids != null)
			 {
				for(int i=0; i < grids.Length; i++)
				{
					if(grids[i] != null && grids[i].gameObject.activeSelf)
					{
						grids[i].repositionNow = true;
						grids[i].Reposition();
					}
				}
			 }

             goDragPanel.SendMessage("UpdateDrawcalls");
         }
       
// 		 listParent.GetComponent<UIGrid>().repositionNow = true;
// 	     listParent.GetComponent<UIGrid>().Reposition ();
//       listParent.transform.parent.gameObject.SendMessage("UpdateDrawcalls");
	}


    public void OnDragonItemFinished(UIDraggablePanel.DragonMode dragonMode)
    {
        if (dragonMode == UIDraggablePanel.DragonMode.None)
        {
            return;
        }

        //如果是向右时，已到最大页数，不能再拖动
        if (nCurPage >= nMaxPage && dragonMode == UIDraggablePanel.DragonMode.Left)
        {
            nCurPage = nMaxPage;
            return;
        }//如果是向左时，已到小页数，不能再拖动
        else if (nCurPage <= 1 && dragonMode == UIDraggablePanel.DragonMode.Right)
        {
            nCurPage = 1;
            return;
        }
        else if (dragonMode == UIDraggablePanel.DragonMode.Left)
        {
            nCurPage++;
        }
        else if (dragonMode == UIDraggablePanel.DragonMode.Right)
        {
            nCurPage--;
        }

        //拖拽完的回调
        if (onDragonFinishedClearItemsFun != null)
        {
            onDragonFinishedClearItemsFun(dragonMode);
        }

        this.CalculteShowItem();

        //刷新箭头
        this.FreshShowJianTouAndLabel();

    }

     //刷新箭头 和 显示的文本
    private void FreshShowJianTouAndLabel()
    {

        if (JianTouLeft == null
               || JianTouLeftHui  == null
                   || JianTouRight == null
                       || JianTouRightHui == null)
        {
            return;
        }

        JianTouMode mode;
        if (nCurPage >= nMaxPage)
        {
            mode = JianTouMode.PageMax;
        }
        else if (nCurPage <= 1)
        {
            mode = JianTouMode.PageMini;
        }
        else
        {
             mode = JianTouMode.Normal;
        }

        if (nMaxPage > 1)
        {
            switch (mode)
            {
                case JianTouMode.PageMini:
                    {
                        JianTouLeft.SetActive(false);
                        JianTouLeftHui.SetActive(true);
                        JianTouRight.SetActive(true);
                        JianTouRightHui.SetActive(false);
                    }
                    break;

                case JianTouMode.Normal:
                    {
                        JianTouLeft.SetActive(true);
                        JianTouLeftHui.SetActive(false);
                        JianTouRight.SetActive(true);
                        JianTouRightHui.SetActive(false);
                    }
                    break;

                case JianTouMode.PageMax:
                    {
                        JianTouLeft.SetActive(true);
                        JianTouLeftHui.SetActive(false);
                        JianTouRight.SetActive(false);
                        JianTouRightHui.SetActive(true);
                    }
                    break;
            }
        }
        else
        {
            JianTouLeft.SetActive(false);
            JianTouLeftHui.SetActive(false);
            JianTouRight.SetActive(false);
            JianTouRightHui.SetActive(false);
        }
        
      

        pageLabel.text = nCurPage + "/" + nMaxPage;

        if (pageCardNumLabel != null)
        {
            if (cardItems != null)
            {
                pageCardNumLabel.text = cardItems.Count + "/" + Obj_MyselfPlayer.GetMe().bagMax;
            }
            else if (FriendsItems != null)
            {
                pageCardNumLabel.text = FriendsItems.Count + "/" + Obj_MyselfPlayer.GetMe().bagMax;
            }
            else
            {
                pageCardNumLabel.text = AssistFriendItems.Count + "/" + Obj_MyselfPlayer.GetMe().bagMax;
            }
           
        }

    }

    //开始计算显示的分页
    public void StartBagItemPages()
    {
        if (!Obj_MyselfPlayer.GetMe().bShowCardInfo)
        {
            nCurPage = 1;
        }
        Obj_MyselfPlayer.GetMe().bShowCardInfo = false;
        bStart = true;
        this.CalculteShowItem();
    }
	
	//当前item的数量
	public int currentItemsNum
	{
		get
		{
			if(cardItems == null)
			{
				return 0;
			}
			
			return cardItems.Count;
		}
	}
	
	//JackWen 加入部分好友界面使用方法
	public int GetCurrentPage(){
		return nCurPage;
	}
	public int GetMaxPage(){
		return nMaxPage;
	}
	public void SetCurrentPage(int curpage){
		nCurPage = curpage;
	}
	public void StartBagItemPages(int page)
    {
        if (!Obj_MyselfPlayer.GetMe().bShowCardInfo)
        {
            nCurPage = page;
        }
        Obj_MyselfPlayer.GetMe().bShowCardInfo = false;
        bStart = true;
        this.CalculteShowItem();
    }

}
