using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;
using card.net;

public class TaskController : MonoBehaviour
{
    public GameObject gridTask;
    private string taskListItem = "TaskListItem";
    private GameObject mainLogic;
    private List<UserTask> taskList;
    private int taskId = 0;
    private int powerTaskID = -1;
    public UIScrollBar scrollBar;
    public UIDraggablePanel dragPanel;
    public GameObject guideItem;
    List<GameObject> items = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        mainLogic = GameObject.Find("MainUILogic");
    }

    void OnEnable()
    {
        NetworkSender.Instance().getTaskList(UpdateTaskListDone);
    }

    void OnDisable()
    {
        scrollBar.scrollValue = 0;
        DestroyItems();
    }

    /// <summary>
    /// 清空
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
    /// 根据奖赏信息刷新界面
    /// </summary>
    private void ShowWindows()
    {
        taskList = Obj_MyselfPlayer.GetMe().taskList;
        if (taskList != null)
        {
            taskList.Sort(CompareTo);
            //副本奖励提前
            List<UserTask> copyTask = new List<UserTask>();
            for (int n = 0; n < taskList.Count; n++)
            {
                if (taskList[n].taskType == 5 && taskList[n].state == 1)
                {
                    copyTask.Add(taskList[n]);
                    taskList.RemoveAt(n);
                    n--;
                }
            }
            copyTask.Sort(CompareTo);
            for (int m = 0; m < copyTask.Count; m++)
            {
                taskList.Insert(0, copyTask[m]);
            }
            copyTask.Clear();
            for (int i = 0; i < taskList.Count; i++)
            {
                GameObject newItem = ResourceManager.Instance.loadWidget(taskListItem);
                newItem.transform.parent = gridTask.transform;
                newItem.transform.localPosition = new Vector3(0, 0, -1);
                newItem.transform.localScale = new Vector3(1, 1, 1);
                newItem.name = taskList[i].templetID.ToString();
                //领取奖励
                UIEventListener.Get(newItem).onClick += FinishTask;
                //奖赏图片
                UISprite icon = newItem.transform.FindChild("CardIcon/CardIconBtn/Sprite-Icon").GetComponent<UISprite>();
                if (TableManager.GetCardByID(taskList[i].cardTempletID) != null)
                {
                    icon.spriteName = TableManager.GetAppearanceByID(TableManager.GetCardByID(taskList[i].cardTempletID).Appearance).HeadIcon;
                }
                else if (taskList[i].dollar > 0)
                    icon.spriteName = "yuanbao";
                else if (taskList[i].power > 0)
                    icon.spriteName = "tili";
                else if (taskList[i].gold > 0)
                    icon.spriteName = "jinbi";
                else if (taskList[i].propID > 0)
                    icon.spriteName = "daojubao";
                //名称
                UILabel titleLabel = newItem.transform.FindChild("Label-Title").GetComponent<UILabel>();
                titleLabel.text = taskList[i].title.ToString();
                Tab_Quest questList=TableManager.GetQuestByID(taskList[i].templetID);
                if(questList!=null)
                {
                    //奖励详细信息
                    if(questList.RewardOne!="-1")
                    {
                        UILabel goods1 = newItem.transform.FindChild("Label-Goods1").GetComponent<UILabel>();
                        goods1.text = questList.RewardOne;
                    }
                    if(questList.RewardTwo!="-1")
                    {
                        UILabel goods2 = newItem.transform.FindChild("Label-Goods2").GetComponent<UILabel>();
                        goods2.text = questList.RewardTwo;
                    }
                }
                //根据奖赏类型赋值奖赏进度信息
                UILabel stateLabel = newItem.transform.FindChild("Label-State-Value").GetComponent<UILabel>();
                if (taskList[i].taskType == 5 || taskList[i].taskType == 12)
                {
                    if (taskList[i].state == 1)
                    {
                        stateLabel.text = "1/1";
                    }
                    else
                    {
                        stateLabel.text = "0/1";
                    }
                }
                else
                {
                    stateLabel.text = taskList[i].process + "/" + taskList[i].request;
                }
                //奖赏状态信息赋值
                UISprite resultDoing = newItem.transform.FindChild("Sprite_doing").GetComponent<UISprite>();
                UISprite resultDone = newItem.transform.FindChild("Sprite_done").GetComponent<UISprite>();
                if (taskList[i].state == 1)
                {
                    resultDone.gameObject.SetActive(true);
                    resultDoing.gameObject.SetActive(false);
                }
                else
                {
                    Destroy(newItem.GetComponent<UIImageButton>());
                    resultDone.gameObject.SetActive(false);
                    resultDoing.gameObject.SetActive(true);
                }
                //for guide
                if (taskList[i].templetID == 1009)
                    guideItem = newItem;
                items.Add(newItem);
            }
        }
        gridTask.GetComponent<UIGrid>().repositionNow = true;
    }

    /// <summary>
    /// 排序算法
    /// </summary>
    /// <param name="taskA"></param>
    /// <param name="taskB"></param>
    /// <returns></returns>
    static private int CompareTo(UserTask taskA, UserTask taskB)
    {
        //排序顺序按照等级
        if (taskB.state != taskA.state)
        {
            return (-1 * taskB.state.CompareTo(taskA.state));
        }
        else
        {
            return taskA.templetID.CompareTo(taskB.templetID);
        }
    }

    /// <summary>
    /// 领取奖励
    /// </summary>
    /// <param name="item"></param>
    private void FinishTask(GameObject item)
    {
        for (int i = 0; i < taskList.Count; i++)
        {
            if (taskList[i].templetID == int.Parse(item.name) && taskList[i].state == 1)
            {
                taskId = taskList[i].templetID;
                if (TableManager.GetQuestByID(taskId).RewardPower > 0)
                {
                    if (Obj_MyselfPlayer.GetMe().power == TableManager.GetIdexperienceByID(Obj_MyselfPlayer.GetMe().level).IDPhysicalValue)
                    {
                        //						BoxManager.showMessage("当前体力已满，无法领取");
                        BoxManager.showMessageByID((int)MessageIdEnum.Msg60);
                        return;
                    }
                    else if (Obj_MyselfPlayer.GetMe().power > 0)
                    {
                        BoxManager.showMessageByID((int)MessageIdEnum.Msg161);
                        powerTaskID = taskList[i].templetID;
                        UIEventListener.Get(BoxManager.buttonYes).onClick += SureToGetPower;
                        return;
                    }
                }
                NetworkSender.Instance().sendFinishTask(UpdateTaskList, taskList[i].templetID);
                break;
            }
        }
    }

    /// <summary>
    /// 确认补满体力
    /// </summary>
    /// <param name="button"></param>
    private void SureToGetPower(GameObject button)
    {
        if (powerTaskID != -1)
            NetworkSender.Instance().sendFinishTask(UpdateTaskList, powerTaskID);
        powerTaskID = -1;
    }

    /// <summary>
    /// 领取奖励完成
    /// </summary>
    /// <param name="bSuccess"></param>
    private void UpdateTaskList(bool bSuccess)
    {
        if (bSuccess)
        {
            Debug.Log("TableManager.GetQuestByID(taskId).RewardDollar=" + TableManager.GetQuestByID(taskId).RewardDollar);
            //BoxManager.showMessage("任务完成!");
            AudioManager.Instance.PlayEffectSound("getreward");
            //AudioController.Play("getreward");
            GameObject.FindWithTag("main_controller").SendMessage("updateUserInfo");
            ShowMessageBox(bSuccess);
        }
    }

    /// <summary>
    /// 获取奖赏信息
    /// </summary>
    /// <param name="go"></param>
    private void GetTaskList(GameObject go)
    {
        NetworkSender.Instance().getTaskList(UpdateTaskListDone);
    }

    /// <summary>
    /// 领取奖励完成后显示弹出框
    /// </summary>
    /// <param name="bSuccess"></param>
    public void ShowMessageBox(bool bSuccess)
    {
        if (bSuccess)
        {
            if (TableManager.GetQuestByID(taskId).RewardGold != -1)
            {
                //				BoxManager.showMessage("获得"+TableManager.GetQuestByID(taskId).RewardGold+"金币");
                BoxManager.showMessageByID((int)MessageIdEnum.Msg61, TableManager.GetQuestByID(taskId).RewardGold.ToString());
                UIEventListener.Get(BoxManager.getYesButton()).onClick += GetTaskList;
            }
            else if (TableManager.GetQuestByID(taskId).RewardDollar != -1)
            {
                //				BoxManager.showMessage("获得"+TableManager.GetQuestByID(taskId).RewardDollar+"元宝");
                BoxManager.showMessageByID((int)MessageIdEnum.Msg62, TableManager.GetQuestByID(taskId).RewardDollar.ToString());
                UIEventListener.Get(BoxManager.getYesButton()).onClick += GetTaskList;
            }
            else if (TableManager.GetQuestByID(taskId).RewardPower != -1)
            {
                //				BoxManager.showMessage("体力已补满");
                BoxManager.showMessageByID((int)MessageIdEnum.Msg63);
                UIEventListener.Get(BoxManager.getYesButton()).onClick += GetTaskList;
            }
            else if (TableManager.GetQuestByID(taskId).AttitemNUM != -1 || TableManager.GetQuestByID(taskId).HpitemNUM != -1)
            {
                //				BoxManager.showMessage("获得大力丸*"+TableManager.GetQuestByID(taskId).AttitemNUM+"\n强身丹*"+TableManager.GetQuestByID(taskId).HpitemNUM);
#if UNITY_ANDROID
                int anum = TableManager.GetQuestByID(taskId).AttitemNUM;
                int hnum = TableManager.GetQuestByID(taskId).HpitemNUM;
                if (anum == -1)
                    anum = 0;
                if (hnum == -1)
                    hnum = 0;
				int AttitemNUM = TableManager.GetQuestByID(taskId).AttitemNUM; //大力丸
				int HpitemNUM = TableManager.GetQuestByID(taskId).HpitemNUM; //强身丹
				if (AttitemNUM <= 0 && HpitemNUM > 0) //没有大力丸 只有强身丹
					BoxManager.showMessageByID((int)MessageIdEnum.Msg149,HpitemNUM.ToString());
				else if (AttitemNUM > 0 && HpitemNUM <= 0)//没有强身丹 只有大力丸
					BoxManager.showMessageByID((int)MessageIdEnum.Msg148,AttitemNUM.ToString());
				else
					BoxManager.showMessageByID((int)MessageIdEnum.Msg64, anum.ToString(), hnum.ToString());
#else
                int AttitemNUM = TableManager.GetQuestByID(taskId).AttitemNUM; //大力丸
                int HpitemNUM = TableManager.GetQuestByID(taskId).HpitemNUM; //强身丹
                if (AttitemNUM <= 0 && HpitemNUM > 0) //没有大力丸 只有强身丹
                    BoxManager.showMessageByID((int)MessageIdEnum.Msg149, HpitemNUM.ToString());
                else if (AttitemNUM > 0 && HpitemNUM <= 0)//没有强身丹 只有大力丸
                    BoxManager.showMessageByID((int)MessageIdEnum.Msg148, AttitemNUM.ToString());
                else
                    BoxManager.showMessageByID((int)MessageIdEnum.Msg64, TableManager.GetQuestByID(taskId).AttitemNUM.ToString(), TableManager.GetQuestByID(taskId).HpitemNUM.ToString());
#endif
                UIEventListener.Get(BoxManager.getYesButton()).onClick += GetTaskList;

            }
        }
    }

    /// <summary>
    /// 获取奖赏列表回调
    /// </summary>
    /// <param name="bSuccess"></param>
    private void UpdateTaskListDone(bool bSuccess)
    {
        if (taskList != null)
        {
            //刷新奖赏数量信息
            int mailCount = 0;
            for (int i = 0; i < taskList.Count; i++)
            {
                if (taskList[i].state == 1)
                {
                    mailCount++;
                }
            }
            Obj_MyselfPlayer.GetMe().taskListCount = mailCount;
        }
        DestroyItems();
        ShowWindows();
        GameObject.FindWithTag("main_controller").SendMessage("updateUserInfo");
        if (GuideManager.Instance.currentStep == GuideManager.GUIDE_STEP.GIFT)
            GuideGift.Instance.NextStep();//领赏引导 SELECT_2
    }

    /// <summary>
    /// 返回主界面
    /// </summary>
    private void ReturnToMainUI()
    {
        mainLogic.SendMessage("ReturnToMainUI");
    }

    public GameObject GetGuideItem()
    {
        //临时变量，实际应返回所需奖赏item
        return guideItem;
    }

    public void SetPanelListEnable(bool bEnable)
    {
        dragPanel.enabled = bEnable;
    }
}