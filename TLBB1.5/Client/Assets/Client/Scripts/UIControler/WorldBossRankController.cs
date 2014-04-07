using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Games.CharacterLogic;
using GCGame.Table;
using Games.LogicObject;
using card.net;

public class WorldBossRankController : MonoBehaviour
{
    public UILabel name;
    public UILabel fighting;
    public UILabel damage;
    public UILabel rank;
    public UIGrid grid;
    public UISprite iconBG;
    public UISprite iconFrame;
    public UISprite iconCard;
    public UIImageButton getRewardBtn;
    public GameObject rankInfo;
    private MainController mainController;
    private GameObject mainLogic;
    private string rankInfoItem = "WorldBossRankDetailItem";
    private List<WorldBossDamageRankInfoClass> rankList = new List<WorldBossDamageRankInfoClass>();
    private List<GameObject> items = new List<GameObject>();
    private WorldBossDamageRankInfoClass playerInfo = new WorldBossDamageRankInfoClass();

    // Use this for initialization
    void Start()
    {
        mainLogic = GameObject.Find("MainUILogic");
    }

    void OnEnable()
    {
        if (mainController == null)
        {
            mainController = GameObject.Find("MainController").GetComponent<MainController>();
        }
        mainController.ShowActivityTopUI(ActivityType.E_ACTIVITY_TYPE_WORLD_BOSS);
        rankList = Obj_MyselfPlayer.GetMe().worldBossWeekRankList;
        playerInfo = Obj_MyselfPlayer.GetMe().playerRank;
        FreshUI();
    }

    void OnDisable()
    {
        DestroyItems();
        mainController.ShowtopInfo();	
    }

    /// <summary>
    /// 刷新界面
    /// </summary>
    private void FreshUI()
    {
        //列表信息
        DestroyItems();
        if (rankList != null)
        {
            foreach (WorldBossDamageRankInfoClass rankInfo in rankList)
            {
                GameObject newItem = ResourceManager.Instance.loadWidget(rankInfoItem);
                newItem.transform.parent = grid.transform;
                newItem.GetComponent<UIWorldBossRankDetailItemView>().InitItem(rankInfo);
                items.Add(newItem);
            }
        }
        grid.repositionNow = true;
        //玩家信息
        name.text = Obj_MyselfPlayer.GetMe().accountName;
        fighting.text = Obj_MyselfPlayer.GetMe().GetFightValue().ToString();
        damage.text = playerInfo.totalDamage.ToString();
        if (playerInfo.rank == -1)
        {
            rankInfo.SetActive(false);
        }
        else
        {
            rank.text = playerInfo.rank.ToString();
            rankInfo.SetActive(true);
        }
        foreach (UserCardItem card in Obj_MyselfPlayer.GetMe().cardBagList)
        {
            if (card.cardID == Obj_MyselfPlayer.GetMe().teamMemberArray[0])
            {
                Tab_Card tabCard = TableManager.GetCardByID(card.templateID);
                if (tabCard != null)
                {
                    int icon_star = tabCard.Star;
                    iconBG.spriteName = UserCardItem.littleCardFrameName[icon_star];
                    iconFrame.spriteName = UserCardItem.littleCardBorderName[icon_star];
                    Tab_Appearance tabAppearance = TableManager.GetAppearanceByID(tabCard.Appearance);
                    if (tabAppearance != null)
                    {
                        iconCard.spriteName = tabAppearance.HeadIcon;
                    }
                }
                break;
            }
        }
        if (Obj_MyselfPlayer.GetMe().hasWorldBossReward == 1)
        {
            getRewardBtn.isEnabled = true;
        }
        else
        {
            getRewardBtn.isEnabled = false;
        }
    }

    /// <summary>
    /// 清空界面
    /// </summary>
    private void DestroyItems()
    {
        foreach (GameObject obj in items)
        {
            Destroy(obj);
        }
        items.Clear();
    }

    /// <summary>
    /// 返回上个窗口
    /// </summary>
    private void ReturnToPrevious()
    {
        mainLogic.SendMessage("OnWorldBossWindow");
    }

    /// <summary>
    /// 转到奖励说明窗口
    /// </summary>
    private void ToAwardWindow()
    {
        mainLogic.SendMessage("OnWorldBossAwardWindow");
    }

    /// <summary>
    /// 领取奖励按钮
    /// </summary>
    private void GetAward()
    {
        NetworkSender.Instance().RequestGetWorldBossReward(RequestGetWorldBossRewardDone);
    }

    /// <summary>
    /// 获取奖励后回调
    /// </summary>
    /// <param name="isSuccess"></param>
    private void RequestGetWorldBossRewardDone(bool isSuccess)
    {
        GameObject.FindWithTag("main_controller").SendMessage("updateUserInfo");
        Hashtable hashTab = TableManager.GetWorldbossaward();
        foreach (DictionaryEntry dic in hashTab)
        {
            Tab_Worldbossaward award = (Tab_Worldbossaward)dic.Value;
            if (award.Type == 4)
            {
                if (award.Par == Obj_MyselfPlayer.GetMe().rewardLev)
                {
                    string content = "获得 ";
                    if (award.AwardGold != -1)
                    {
                        content += "金币 " + award.AwardGold + "  ";
                    }
                    if (award.AwardYuanbao != -1)
                    {
                        content += "元宝 " + award.AwardYuanbao + "  ";
                    }
                    if (award.AwardLeader != -1)
                    {
                        content += "领导力 " + award.AwardLeader;
                    }
                    BoxManager.showMessage(content, "");
                    break;
                }
            }
        }
        if (Obj_MyselfPlayer.GetMe().hasWorldBossReward == 1)
        {
            getRewardBtn.isEnabled = true;
        }
        else
        {
            getRewardBtn.isEnabled = false;
        }
    }
}
