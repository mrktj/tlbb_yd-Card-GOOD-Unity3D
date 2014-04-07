using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Games.LogicObject;
using card.net;
using Games.CharacterLogic;
using GCGame.Table;

public class PVPController : MonoBehaviour
{

    public GameObject listParent;
    private GameObject logicTarget;
    private List<GameObject> itemList;
    private string strListItemName = "PVPCardItem";
    private Transform trans;

    public GameObject goHeroInfo;
    public GameObject promtoBtn;
    public UISprite heroIcon;
    public UILabel heroRankLabel;
    public UILabel HeroFightNumLabel;
    public UILabel HeroNameLabel;
    public UILabel SocreLabel;
    public UILabel leftTimeLabel;
    public UILabel HeroLevLabel;
    private string pvp_Key = "PVP_Key";

    void Awake()
    {
        trans = transform;
        logicTarget = GameObject.Find("MainUILogic");
        itemList = new List<GameObject>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        promtoBtn.SetActive(false);
        goHeroInfo.SetActive(false);
        //Obj_MyselfPlayer.GetMe().bInPvP = true;
		Obj_MyselfPlayer.GetMe().battleType = Games.Battle.BattleType.PVP;
        SendGetPvPPlayerList();

    }

    void OnDisable()
    {
        UIDraggablePanel uiDragPanel = listParent.transform.parent.GetComponent<UIDraggablePanel>();
        ScrollData scData = new ScrollData(uiDragPanel.verticalScrollBar.scrollValue);
        Obj_MyselfPlayer.GetMe().SetScrollValue(pvp_Key, scData);
        foreach (GameObject item in itemList)
        {
            GameObject fightBtn = item.transform.FindChild("fightBtn").gameObject;
            fightBtn.SetActive(false);
            CardListItemPool.Instance.DestroyItemAndPushToPool(item, strListItemName);
        }

        itemList.Clear();
    }

    //发送获得PVP玩家列表
    private void SendGetPvPPlayerList()
    {
        NetworkSender.Instance().GetPvPPlayerInfoList(RecallGetListFun);
    }

    //刷新英雄信息
    private void FreshheroInfo()
    {
        goHeroInfo.SetActive(true);
        long learderID = Obj_MyselfPlayer.GetMe().GetTeamLeaderCardID();
        UserCardItem card = Obj_MyselfPlayer.GetMe().GetUserCardByGUID(learderID);
        //templeID < 0 情况的容错处理,暂时显示默认头像，且错误头像点击不给予反映//
        if (card.templateID > 0)
        {
            heroIcon.spriteName = TableManager.GetAppearanceByID(TableManager.GetCardByID(card.templateID).Appearance).HeadIcon;
            //------------------------卡牌背景及外框--------------------------------
            //2013-10-12 Jack Wen
            UISprite icon_bg = heroIcon.transform.parent.FindChild("Sprite-BG").GetComponent<UISprite>();
            UISprite icon_border = heroIcon.transform.parent.FindChild("sp-Outside").GetComponent<UISprite>();
            int icon_star = TableManager.GetCardByID(card.templateID).Star;
            icon_bg.spriteName = UserCardItem.littleCardFrameName[icon_star];
            icon_border.spriteName = UserCardItem.littleCardBorderName[icon_star];
            //--------------------------------------------------------------------
            //玩家主卡牌等级等级
            long leadCardID = Obj_MyselfPlayer.GetMe().GetTeamLeaderCardID();
            UserCardItem leaderCard = Obj_MyselfPlayer.GetMe().GetUserCardByGUID(leadCardID);
            HeroLevLabel.text = leaderCard.level.ToString();

            GameObject iconGo = trans.FindChild("HeroInfo/CardIcon/CardIconBtn").gameObject;
            UIEventListener.Get(iconGo).onClick = ShowHeroCardInfo;
        }

        //排名
        heroRankLabel.text = Obj_MyselfPlayer.GetMe().nHeroRank.ToString();
        //获得战斗力
        HeroFightNumLabel.text = Obj_MyselfPlayer.GetMe().GetFightValue().ToString();
        //玩家名字
        HeroNameLabel.text = Obj_MyselfPlayer.GetMe().accountName;
        //玩家当前排名对应的占领积分
        SocreLabel.text = GetOccupyScoreByRank(Obj_MyselfPlayer.GetMe().nHeroRank).ToString();
        //pvp剩余次数
        leftTimeLabel.text = Obj_MyselfPlayer.GetMe().nPvPTimes.ToString();
        //玩家主卡牌等级等级
        //long leadCardID = Obj_MyselfPlayer.GetMe().GetTeamLeaderCardID();
        //UserCardItem leaderCard = Obj_MyselfPlayer.GetMe().GetUserCardByGUID(leadCardID);
        //HeroLevLabel.text =  leaderCard.level.ToString();

    }

    public void ShowHeroCardInfo(GameObject btn)
    {
        long leaderGuid = Obj_MyselfPlayer.GetMe().GetTeamLeaderCardID();
        UserCardItem card = Obj_MyselfPlayer.GetMe().GetUserCardByGUID(leaderGuid);
        if (card != null)
        {
            BoxManager.showCardInfoMessage(card);
        }
    }

    //根据排名获得占领积分
    private int GetOccupyScoreByRank(int nRank)
    {
        int nOcupyScore = 0;
        int nLen = TableManager.GetPvpScore().Count;
        for (int i = 1; i <= nLen; i++)
        {
            //这里的RankMin 是排名的数字，名次越小，数字越大
            if (TableManager.GetPvpScoreByID(i).RankMin >= nRank
                  && TableManager.GetPvpScoreByID(i).RankMax <= nRank)
            {
                nOcupyScore = TableManager.GetPvpScoreByID(i).Score;
                break;
            }
        }

        return nOcupyScore;
    }



    //回发信息回调函数
    public void RecallGetListFun(bool bSuccess)
    {
        FreshheroInfo();

        //判断是否显示领取提示
        if (this.IsCanGetPvPShopItem())
        {
            promtoBtn.SetActive(true);
        }
        else
        {
            promtoBtn.SetActive(false);
        }

        foreach (PVPPlayerInfo playerInfo in Obj_MyselfPlayer.GetMe().pvpPlayerInfoList)
        {
            //如果列表里有自己，就跳过
            //if(playerInfo.nlGUID == Obj_MyselfPlayer.GetMe().accountID)
            //{
            //	continue;
            //}

            GameObject listItem = CardListItemPool.Instance.GetListItem(strListItemName);
            listItem.transform.parent = listParent.transform;

            listItem.GetComponent<UIPVPListItemView>().InitWithPlayerInfo(playerInfo, ShowCardInfo, OnSelectPvPItem);//JackWen 2013/12/13-10:51

            itemList.Add(listItem);
        }

        UIGrid grid = listParent.GetComponent<UIGrid>();
        grid.Reposition();
        grid.repositionNow = true;
        UIDraggablePanel uiDragPanel = listParent.transform.parent.GetComponent<UIDraggablePanel>();
        if (Obj_MyselfPlayer.GetMe().scrollRecord.ContainsKey(pvp_Key))
        {
            if (Obj_MyselfPlayer.GetMe().pvpPlayerInfoList != null && Obj_MyselfPlayer.GetMe().pvpPlayerInfoList.Count > 4)
            {
                uiDragPanel.verticalScrollBar.scrollValue = Obj_MyselfPlayer.GetMe().scrollRecord[pvp_Key].scrollValue;
            }
            else
            {
                uiDragPanel.verticalScrollBar.scrollValue = 1.0f;
            }
        }
        else
        {
            uiDragPanel.verticalScrollBar.scrollValue = 1.0f;
        }
        //uiDragPanel.verticalScrollBar.scrollValue = 0;
        //uiDragPanel.UpdateScrollbars(true);
        //uiDragPanel.ResetPosition();
        //listParent.SendMessage("UpdateDrawcalls");

    }

    //显示卡牌信息
    public void ShowCardInfo(GameObject btn)
    {
        long guid = long.Parse(btn.transform.parent.parent.name);
        PVPPlayerInfo playerInfo = Obj_MyselfPlayer.GetMe().GetPvPPlayerInfoByGuid(guid);

        if (playerInfo != null && playerInfo.nlGUID != Obj_MyselfPlayer.GetMe().accountID)
        {
            UserCardItem card = new UserCardItem();
            card.cardID = playerInfo.nlGUID;
            card.templateID = playerInfo.nTempleID;
            card.level = playerInfo.nLev;
            card.skillLevel = playerInfo.skill_level;//主动技能等级
            card.addQualityAtt = playerInfo.add_quality_att;//攻击强化次数
            card.addQualityHp = playerInfo.add_quality_hp;//血量强化次数
            card.skillStudyId = playerInfo.studySkillId;
            card.skillStudyLev = playerInfo.studySkillLev;
            BoxManager.showCardInfoMessage(card);
        }
        else
        {
            long nleader = Obj_MyselfPlayer.GetMe().GetTeamLeaderCardID();
            UserCardItem card = Obj_MyselfPlayer.GetMe().GetUserCardByGUID(nleader);
            BoxManager.showCardInfoMessage(card);
        }

    }


    public void ReturnToMainUI()
    {
        logicTarget.SendMessage("ReturnToMainUI");
    }

    //选择PVP 对手
    public void OnSelectPvPItem(GameObject go)
    {
        if (Obj_MyselfPlayer.GetMe().nPvPTimes <= 0)
        {
            BoxManager.showMessageByID((int)MessageIdEnum.Msg112);
            return;
        }
        //领导力
        int nowLeaderShip = Obj_MyselfPlayer.GetMe().GetLeaderShipValue();
        if (nowLeaderShip > Obj_MyselfPlayer.GetMe().leadership)
        {
            //			BoxManager.showMessage("当前领导力不足");
            BoxManager.showMessageByID((int)MessageIdEnum.Msg55);
            return;
        }

        long guid = long.Parse(go.transform.parent.name);
        foreach (PVPPlayerInfo playerInfo in Obj_MyselfPlayer.GetMe().pvpPlayerInfoList)
        {
            if (playerInfo.nlGUID == guid)
            {
                Obj_MyselfPlayer.GetMe().pvpChoosePlayer = playerInfo;
                break;
            }
        }

        logicTarget.SendMessage("OnBattleBefore");
    }


    //判断是否能获得PVP商城的东西，就是能否能领奖
    public bool IsCanGetPvPShopItem()
    {

        int nLen = TableManager.GetPvpShop().Count;
        for (int i = 1; i <= nLen; i++)
        {
            int nScore = TableManager.GetPvpShopByID(i).Score;
            if (Obj_MyselfPlayer.GetMe().nlTotalScore >= nScore)
            {
                return true;
            }
        }

        return false;
    }
	
	public void ReturnToPvPTotal()
	{
		logicTarget.GetComponent<MainUILogic>().OnPvPTotalWindow();
	}



    //显示积分商城
    public void OnShowScoreShop()
    {
        logicTarget.SendMessage("OnPvPShop");
    }
}
