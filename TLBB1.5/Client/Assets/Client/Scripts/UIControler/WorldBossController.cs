using UnityEngine;
using System.Collections;
using Games.CharacterLogic;
using System.Collections.Generic;
using System;
using Games.Battle;
using card.net;

public class WorldBossController : MonoBehaviour
{
    public UILabel bossName;
    public UILabel bossDes;
    public UILabel bossHp;
    public UILabel openTime;
    public UILabel lastTime;
    public UILabel lastPlayer;
    public UILabel[] topPlayers;
    public UISlider hpBar;
    public UILabel battleCount;
    public UILabel damage;
    public UILabel rank;
    public UIGrid grid;
    public UILabel resurgenceCD;
    public UILabel buffDes;
    public GameObject worldBossCard;
    public GameObject lastBossCard;
    public GameObject open;
    public GameObject close;
    public GameObject killed;
    public GameObject notKilled;
    public GameObject attBtn;
    public GameObject resBtn;
    public GameObject rankUp;
    private GameObject mainLogic;
    private MainController mainController;
    private WorldBossClass activeBoss = new WorldBossClass();
    private WorldBossKillInfoClass lastKillInfo = new WorldBossKillInfoClass();
    private WorldBossAttInfoClass currentKillInfo = new WorldBossAttInfoClass();
    private List<GameObject> items = new List<GameObject>();
    private string rankInfoItem = "WorldBossRankItem";
    private DateTime nextTime = new DateTime();
    private DateTime battleCD = new DateTime();
    private bool startCD = false;
    private int buffCount = 0;
    private int cost = 50;

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
        ErrorEventListener.SetHandler((int)xjgame.message.ErrorType.WB_BATTLE_BOSS_DEAD, WorldBossError);
        ErrorEventListener.SetHandler((int)xjgame.message.ErrorType.WB_BATTLE_BOSS_HIDE, WorldBossError);
        GameObject.FindWithTag("main_controller").SendMessage("showBottomBar");
        Obj_MyselfPlayer.GetMe().battleType = BattleType.WORLD_BOSS;
        mainController.ShowActivityTopUI(ActivityType.E_ACTIVITY_TYPE_WORLD_BOSS);
        activeBoss = Obj_MyselfPlayer.GetMe().activeBoss;
        lastKillInfo = Obj_MyselfPlayer.GetMe().lastKillInfo;
        currentKillInfo = Obj_MyselfPlayer.GetMe().currentKillInfo;
        nextTime = Obj_MyselfPlayer.GetMe().activeBossCD;
        battleCD = Obj_MyselfPlayer.GetMe().resurgenceCD;
        FreshResurgenceInfo();
        FreshUI();
    }

    void OnDisable()
    {
        DestroyItems();
        mainController.ShowtopInfo();
    }

    void Update()
    {
        //未开启状态倒计时
        if (activeBoss.activeFlag == 0)
        {
            if (nextTime >= DateTime.Now)
            {
                TimeSpan time = nextTime - DateTime.Now;
                openTime.text = time.Hours.ToString("00") + ":" + time.Minutes.ToString("00") + ":" + time.Seconds.ToString("00");
            }
            else
            {
                activeBoss.activeFlag = 1;
                FreshUI();
            }
        }
        if (startCD)
        {
            if (battleCD >= DateTime.Now)
            {
                TimeSpan time = battleCD - DateTime.Now;
                resurgenceCD.text = ((int)time.TotalSeconds).ToString();
            }
            else
            {
                resBtn.SetActive(false);
                attBtn.SetActive(true);
                startCD = false;
            }
        }
    }

    /// <summary>
    /// 错误信息处理
    /// </summary>
    private void WorldBossError()
    {
        NetworkSender.Instance().RequestWorldBossInfo(BackToWorldBoss);
    }

    /// <summary>
    /// 刷新界面
    /// </summary>
    /// <param name="result"></param>
    private void BackToWorldBoss(bool result)
    {
        activeBoss = Obj_MyselfPlayer.GetMe().activeBoss;
        lastKillInfo = Obj_MyselfPlayer.GetMe().lastKillInfo;
        currentKillInfo = Obj_MyselfPlayer.GetMe().currentKillInfo;
        nextTime = Obj_MyselfPlayer.GetMe().activeBossCD;
        battleCD = Obj_MyselfPlayer.GetMe().resurgenceCD;
        FreshUI();
    }

    /// <summary>
    /// 刷新界面
    /// </summary>
    private void FreshUI()
    {
        //公共信息
        bossName.text = activeBoss.name;
        bossDes.text = activeBoss.des;
        if (activeBoss.totalHp != 0)
        {
            float percent = (float)activeBoss.currentHp / (float)activeBoss.totalHp;
            bossHp.text = ((int)(percent * 100)).ToString() + "%";
            hpBar.sliderValue = percent;
        }
        else
        {
            bossHp.text = "100%";
            hpBar.sliderValue = 1;
        }
        if (activeBoss.templateId != 0)
        {
            worldBossCard.GetComponent<CardLarge>().SetCardTemplateID(activeBoss.templateId);
        }
        if (lastKillInfo.lastBossInfo.templateId != 0)
        {
            lastBossCard.GetComponent<CardIcon>().SetCardTemplateID(lastKillInfo.lastBossInfo.templateId);
        }
        if (activeBoss.activeFlag == 1)
        {
            //开启信息
            close.SetActive(false);
            open.SetActive(true);
            battleCount.text = currentKillInfo.attTimes.ToString();
            damage.text = currentKillInfo.totalDamage.ToString();
            if (currentKillInfo.attRank >= 100)
            {
                rank.text = "99";
                rankUp.SetActive(true);
            }
            else
            {
                rank.text = currentKillInfo.attRank.ToString();
                rankUp.SetActive(false);
            }
            //当前排名信息
            DestroyItems();
            foreach (WorldBossDamageRankInfoClass rankInfo in currentKillInfo.rankInfoList)
            {
                GameObject newItem = ResourceManager.Instance.loadWidget(rankInfoItem);
                newItem.transform.parent = grid.transform;
                newItem.GetComponent<UIWorldBossRankItemView>().InitItem(rankInfo);
                items.Add(newItem);
            }
            grid.repositionNow = true;
            buffCount = currentKillInfo.buffCount;
            buffDes.text = "生命+" + buffCount + "%  攻击+" + buffCount + "%";
        }
        else
        {
            //未开启信息
            open.SetActive(false);
            close.SetActive(true);
            if (lastKillInfo.lastBossInfo.aliveTime != -1)
            {
                //上次已击杀
                float killMinute = (float)lastKillInfo.lastBossInfo.aliveTime / (float)60;
                int killResult = 0;
                if (killMinute > (int)killMinute)
                {
                    killResult = (int)killMinute + 1;
                }
                else
                {
                    killResult = (int)killMinute;
                }
                lastTime.text = killResult + "分钟";
                lastPlayer.text = lastKillInfo.lastKiller.name;
                for (int i = 0; i < topPlayers.Length; i++)
                {
                    if (lastKillInfo.top3Killer.Count > i)
                    {
                        topPlayers[i].text = lastKillInfo.top3Killer[i].name;
                    }
                    else
                    {
                        topPlayers[i].text = "";
                    }
                }
                notKilled.SetActive(false);
                killed.SetActive(true);
            }
            else
            {
                //上次未击杀
                killed.SetActive(false);
                notKilled.SetActive(true);
            }
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
    /// 刷新复活信息
    /// </summary>
    private void FreshResurgenceInfo()
    {
        if (battleCD >= DateTime.Now)
        {
            startCD = true;
            attBtn.SetActive(false);
            resBtn.SetActive(true);
        }
        else
        {
            startCD = false;
            resBtn.SetActive(false);
            attBtn.SetActive(true);
        }
    }

    /// <summary>
    /// 切换到选择阵型窗口
    /// </summary>
    private void OnBattleBefore()
    {
        if (Obj_MyselfPlayer.GetMe().hasWorldBossReward == 1)
        {
            //有未领取的奖励
            BoxManager.showMessageByID((int)MessageIdEnum.Msg231);
            UIEventListener.Get(BoxManager.getYesButton()).onClick += ToReward;
            return;
        }
        if (Obj_MyselfPlayer.GetMe().GetLeaderShipValue() > Obj_MyselfPlayer.GetMe().leadership)
        {
            //领导力不足
            BoxManager.showMessageByID((int)MessageIdEnum.Msg55);
            return;
        }
        mainLogic.SendMessage("OnBattleBefore");
    }

    /// <summary>
    /// 复活按钮
    /// </summary>
    private void Resurgence()
    {
        if (Obj_MyselfPlayer.GetMe().dollar < cost)
        {
            //元宝不足
            BoxManager.showMessageByID((int)MessageIdEnum.Msg178);
            UIEventListener.Get(BoxManager.getYesButton()).onClick += YuanBaoPrompt;
            return;
        }
        NetworkSender.Instance().RequestWorldBossResurgence(RequestWorldBossResurgenceDone);
    }

    /// <summary>
    /// 复活回调 刷新信息
    /// </summary>
    /// <param name="isSuccess"></param>
    private void RequestWorldBossResurgenceDone(bool isSuccess)
    {
        GameObject.FindWithTag("main_controller").SendMessage("updateUserInfo");
        battleCD = Obj_MyselfPlayer.GetMe().resurgenceCD;
        FreshResurgenceInfo();
    }

    /// <summary>
    /// 祝福按钮
    /// </summary>
    private void Buff()
    {
        if (buffCount == 20)
        {
            //次数到上限
            BoxManager.showMessageByID((int)MessageIdEnum.Msg232);
            return;
        }
        if (Obj_MyselfPlayer.GetMe().dollar < cost)
        {
            //元宝不足
            BoxManager.showMessageByID((int)MessageIdEnum.Msg178);
            UIEventListener.Get(BoxManager.getYesButton()).onClick += YuanBaoPrompt;
            return;
        }
        NetworkSender.Instance().RequestWorldBossBuff(RequestWorldBossBuffDone);
    }

    /// <summary>
    /// 获得祝福回调 刷新信息
    /// </summary>
    /// <param name="isSuccess"></param>
    private void RequestWorldBossBuffDone(bool isSuccess)
    {
        GameObject.FindWithTag("main_controller").SendMessage("updateUserInfo");
        currentKillInfo = Obj_MyselfPlayer.GetMe().currentKillInfo;
        FreshUI();
    }

    /// <summary>
    /// 确定后切换到领取奖励页面
    /// </summary>
    private void ToReward(GameObject btn)
    {
        ToWorldBossRankWindow();
    }

    /// <summary>
    /// 跳转到充值页面
    /// </summary>
    /// <param name="btn"></param>
    private void YuanBaoPrompt(GameObject btn)
    {
        mainLogic.SendMessage("OnPurchaseWindow");
    }

    /// <summary>
    /// 切换到排行榜
    /// </summary>
    private void ToWorldBossRankWindow()
    {
        NetworkSender.Instance().RequeseWorldBossWeekRank(RequeseWorldBossWeekRankDone);
    }

    /// <summary>
    /// 周排行获取后回调切换窗口
    /// </summary>
    /// <param name="isSuccess"></param>
    private void RequeseWorldBossWeekRankDone(bool isSuccess)
    {
        mainLogic.SendMessage("OnWorldBossRankWindow");
    }
}
