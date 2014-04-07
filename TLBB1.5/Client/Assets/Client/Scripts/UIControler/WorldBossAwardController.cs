using UnityEngine;
using System.Collections;
using GCGame.Table;

public class WorldBossAwardController : MonoBehaviour
{
    private GameObject mainLogic;
    public UITexture card;
    public UILabel[] leaderShip;
    public UILabel[] dolar;
    private MainController mainController;

    // Use this for initialization
    void Start()
    {
        mainLogic = GameObject.Find("MainUILogic");
        AtlasManager.Instance.SetBodyName(card, "heti_wangyuyan_4");
        FreshUI();
    }

    void OnEnable()
    {
        if (mainController == null)
        {
            mainController = GameObject.Find("MainController").GetComponent<MainController>();
        }
        GameObject.FindWithTag("main_controller").SendMessage("showBottomBar");
        mainController.ShowActivityTopUI(ActivityType.E_ACTIVITY_TYPE_WORLD_BOSS);
    }

    void OnDisable()
    {
        mainController.ShowtopInfo();
    }

    /// <summary>
    /// 刷新界面
    /// </summary>
    private void FreshUI()
    {
        Hashtable hashTab = TableManager.GetWorldbossaward();
        foreach (DictionaryEntry dic in hashTab)
        {
            Tab_Worldbossaward award = (Tab_Worldbossaward)dic.Value;
            if (award.Type == 4)
            {
                if(award.AwardLeader!=-1)
                {
                    leaderShip[award.Par-1].text="永久领导力提升"+award.AwardLeader+"点";
                    leaderShip[award.Par-1].gameObject.SetActive(true);
                }
                else
                {
                    leaderShip[award.Par-1].gameObject.SetActive(false);
                }
                if (award.AwardYuanbao != -1)
                {
                    dolar[award.Par - 1].text = award.AwardYuanbao.ToString();
                    dolar[award.Par - 1].gameObject.SetActive(true);
                }
                else
                {
                    dolar[award.Par - 1].gameObject.SetActive(false);
                }
            }
        }
    }

    /// <summary>
    /// 返回上个窗口
    /// </summary>
    private void ReturnToPrevious()
    {
        mainLogic.SendMessage("OnWorldBossRankWindow");
    }
}
