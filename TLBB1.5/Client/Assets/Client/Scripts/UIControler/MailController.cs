using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;
using card.net;
using System;
using System.Linq;

public class MailController : MonoBehaviour
{
    private string mailListItem = "MailListItem";
    public GameObject gridMail;
    private string friendMailListItem = "FriendMailListItem";
    private GameObject mainLogic;
    public UILabel mailCount;
    public UISprite title;
    public UIScrollBar scrollBar;
    public GameObject friendApplyBtn;
    public GameObject refuseAllBtn;
    public GameObject prompt;
    private bool showMessage = true;
    private string normalMail_Key = "NormalMail_Key";
    private string friendApply_Key = "FriendApply_Key";
    private List<MailInfo> mailList = new List<MailInfo>();
    private List<GameObject> items = new List<GameObject>();
    private bool isFriendMail = false;
    private bool isReceiveGoods = false;
    private string receivegoodsStr = "";
    private int normalMailCount = 0;
    private int friendMailCount = 0;
    void Start()
    {
        mainLogic = GameObject.Find("MainUILogic");
    }

    void OnEnable()
    {
        refuseAllBtn.SetActive(false);
        friendApplyBtn.SetActive(true);
        NetworkSender.Instance().getMailList(UpdateMailListDone);
    }

    void OnDisable()
    {
        mailCount.text = "";
        title.spriteName = "shuxinlan";
        title.MakePixelPerfect();
        if (isFriendMail)
        {
            ScrollData scData = new ScrollData(scrollBar.scrollValue);
            Obj_MyselfPlayer.GetMe().SetScrollValue(friendApply_Key, scData);
        }
        else
        {
            ScrollData scData = new ScrollData(scrollBar.scrollValue);
            Obj_MyselfPlayer.GetMe().SetScrollValue(normalMail_Key, scData);
        }
        isFriendMail = false;
        showMessage = true;
        mailList.Clear();
        DestroyItems();
    }

    /// <summary>
    /// 协同 改变ScrollValue值
    /// </summary>
    /// <returns></returns>
    private IEnumerator SetScrollValue()
    {
        yield return 0;
        FreshBar();
    }

    /// <summary>
    /// 根据相关信息刷新邮件界面
    /// </summary>
    private void ShowWindows()
    {
        scrollBar.scrollValue = 0;
        if (isFriendMail)
        {
            //好友申请
            friendApplyBtn.SetActive(false);
            friendMailCount = 0;
            for (int i = 0; i < mailList.Count; i++)
            {
                if (mailList[i].mailType == 2)
                {
                    if (TableManager.GetCardByID(mailList[i].cardTempleId) == null)
                    {
                        //服务器发来的好友申请数据为空打出错误消息
                        Debug.LogError("*** cardTempleId is null");
                        Debug.LogError("*** cardId" + mailList[i].cardId);
                        Debug.LogError("*** content" + mailList[i].content);
                        Debug.LogError("*** source" + mailList[i].source);
                        Debug.LogError("*** friendID" + mailList[i].friendID);
                        Debug.LogError("*** icon_id" + mailList[i].icon_id);
                        Debug.LogError("*** cardTempleId" + mailList[i].cardTempleId);
                        Debug.LogError("*** level" + mailList[i].level);
                    }
                    else
                    {
                        GameObject newMail = ResourceManager.Instance.loadWidget(friendMailListItem);
                        //邮件id
                        newMail.transform.parent = gridMail.transform;

                        bool success = newMail.GetComponent<UIMailListItemView>().InitFriendMailWithMail(mailList[i]);

                        if (success)
                        {
                            GameObject cardBtn = newMail.GetComponent<UIMailListItemView>().GetCardIconBtn();
                            GameObject yesBtn = newMail.GetComponent<UIMailListItemView>().GetYesBtn();
                            GameObject noBtn = newMail.GetComponent<UIMailListItemView>().GetNoBtn();

                            UIEventListener.Get(yesBtn).onClick += AgreeFriendApply;
                            UIEventListener.Get(noBtn).onClick += RefuseFriendApply;
                            UIEventListener.Get(cardBtn).onClick += SelectCardInfo;
                        }
                        items.Add(newMail);
                        friendMailCount++;
                    }
                }
            }
            if (Obj_MyselfPlayer.GetMe().friendMailIsFull == 1 && showMessage)
            {
                //BoxManager.showMessage("好友申请已满，请处理申请");
                BoxManager.showMessageByID((int)MessageIdEnum.Msg53);
                showMessage = false;
            }
            gridMail.GetComponent<UIGrid>().repositionNow = true;
            title.spriteName = "haoyoushenqing";
            title.MakePixelPerfect();
            mailCount.text = friendMailCount + "/20";
            //拒绝全部按钮显示
            if (friendMailCount > 0)
            {
                refuseAllBtn.SetActive(true);
            }
            else
            {
                refuseAllBtn.SetActive(false);
            }
        }
        else
        {
            //邮件
            refuseAllBtn.SetActive(false);
            friendApplyBtn.SetActive(true);
            normalMailCount = 0;
            for (int i = mailList.Count - 1; i >= 0; i--)
            {
                if (mailList[i].mailType != 2)
                {

                    GameObject newMail = ResourceManager.Instance.loadWidget(mailListItem);
                    //邮件id
                    newMail.name = mailList[i].mailID.ToString();
                    newMail.transform.parent = gridMail.transform;
                    bool success = newMail.GetComponent<UIMailListItemView>().InitItemMailWithMail(mailList[i]);

                    if (success)
                    {
                        GameObject deleteBtn = newMail.GetComponent<UIMailListItemView>().GetDeleteBtn();
                        GameObject checkBtn = newMail.GetComponent<UIMailListItemView>().GetCheckBtn();
                        GameObject getBtn = newMail.GetComponent<UIMailListItemView>().GetGetBtn();

                        UIEventListener.Get(checkBtn).onClick += ShowMailContent;
                        UIEventListener.Get(deleteBtn).onClick += DeleteMail;
                        UIEventListener.Get(getBtn).onClick += ReceiveMail;
                    }

                    items.Add(newMail);
                    normalMailCount++;
                }
            }
            if (Obj_MyselfPlayer.GetMe().mailIsFull == 1 && showMessage)
            {
                BoxManager.showMessageByID((int)MessageIdEnum.Msg54);
                showMessage = false;
            }
            gridMail.GetComponent<UIGrid>().repositionNow = true;
            title.spriteName = "shuxinlan";
            title.MakePixelPerfect();
            mailCount.text = normalMailCount + "/20";
        }
    }

    /// <summary>
    /// ScrollBarValue记忆赋值
    /// </summary>
    private void FreshBar()
    {
        if (isFriendMail)
        {
            if (Obj_MyselfPlayer.GetMe().scrollRecord.ContainsKey(friendApply_Key))
            {
                if (friendMailCount > 4)
                {
                    scrollBar.scrollValue = Obj_MyselfPlayer.GetMe().scrollRecord[friendApply_Key].scrollValue;
                }
                else
                {
                    scrollBar.scrollValue = 0;
                }
            }
        }
        else
        {
            if (Obj_MyselfPlayer.GetMe().scrollRecord.ContainsKey(normalMail_Key))
            {
                if (normalMailCount > 4)
                {
                    scrollBar.scrollValue = Obj_MyselfPlayer.GetMe().scrollRecord[normalMail_Key].scrollValue;
                }
                else
                {
                    scrollBar.scrollValue = 0;
                }
            }
        }
    }

    /// <summary>
    /// 接收系统邮件
    /// </summary>
    /// <param name="item"></param>
    private void ReceiveMail(GameObject item)
    {
        for (int i = 0; i < mailList.Count; i++)
        {
            if (mailList[i].mailID.ToString() == item.name)
            {
                if (mailList[i].itemType == 3 && Obj_MyselfPlayer.GetMe().cardBagList.Count >= Obj_MyselfPlayer.GetMe().bagMax)
                {
                    //					BoxManager.showBagFullBox("您携带的侠士已经达到上限可以将侠士吸收、出售或者扩充您的背包.");
                    BoxManager.showMessageByID((int)MessageIdEnum.Msg74);
                    return;
                }
                NetworkSender.Instance().receiveGoods(RestartWindow, mailList[i].mailID);
                isReceiveGoods = true;
                string contents = "";
                switch (mailList[i].itemType)
                {	//1:钱 2:宝石 3:卡牌 4:道具
                    case 1:
                        contents += "金币 x " + mailList[i].itemValue.ToString();
                        break;
                    case 2:
                        contents += "元宝 x " + mailList[i].itemValue.ToString();
                        break;
                    case 3:
                        contents += "卡牌:" + TableManager.GetCardByID(mailList[i].itemValue).Note + " x " + mailList[i].itemNum.ToString();
                        break;
                    case 4:
                        int itemid = mailList[i].itemValue;
                        string itemName = "";
                        if (itemid == 10000)
                            itemName = "大力丸";
                        else if (itemid == 10001)
                            itemName = "强身丹";
                        contents += "道具：" + itemName + " x " + mailList[i].itemNum.ToString();
                        break;
                }
                receivegoodsStr = "获得 " + contents + " !";
                break;
            }
        }
    }

    /// <summary>
    /// 点击好友申请头像显示人物信息
    /// </summary>
    /// <param name="item"></param>
    private void SelectCardInfo(GameObject item)
    {
        foreach (MailInfo mail in mailList)
        {
            if (mail.mailID == long.Parse(item.name))
            {
                UserCardItem card = new UserCardItem();
                card.cardID = mail.cardId;
                card.addQualityAtt = mail.attackTimes;
                card.addQualityHp = mail.hpTimes;
                card.level = mail.cardLevel;
                if (mail.cardTempleId > 0)
                {
                    card.quality = TableManager.GetCardByID(mail.cardTempleId).Star;
                    card.templateID = mail.cardTempleId;
                }
                else
                {
                    card.quality = 4;  //默认一个卡牌形象//
                    card.templateID = 1014;
                }
                card.skillLevel = mail.skillLevel;
                card.skillStudyId = mail.studySkillId;
                card.skillStudyLev = mail.studySkillLev;
                BoxManager.showCardInfoMessage(card);
                break;
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
    /// 获取邮件信息完成回调
    /// </summary>
    /// <param name="bSuccess"></param>
    private void UpdateMailListDone(bool bSuccess)
    {
        mailList = Obj_MyselfPlayer.GetMe().mailList;
        int mailCount = 0;
        int friendMailCount = 0;
        for (int i = 0; i < mailList.Count; i++)
        {
            if (mailList[i].mailType == 2)
            {
                mailCount++;
                friendMailCount++;
            }
            else
            {
                if (mailList[i].mailState == 1)
                {
                    mailCount++;
                }
            }
        }
        if (friendMailCount > 0)
        {
            prompt.transform.FindChild("Label").GetComponent<UILabel>().text = friendMailCount.ToString();
            prompt.SetActive(true);
        }
        else
        {
            prompt.SetActive(false);
        }
        DestroyItems();
        ShowWindows();
        StartCoroutine(SetScrollValue());
        if (isReceiveGoods)
        {
            BoxManager.showMessage(receivegoodsStr, "接收奖品");
            isReceiveGoods = false;
            receivegoodsStr = "";
        }
    }

    /// <summary>
    /// 同意好友申请
    /// </summary>
    /// <param name="item"></param>
    private void AgreeFriendApply(GameObject item)
    {
        for (int i = 0; i < mailList.Count; i++)
        {
            if (mailList[i].mailID.ToString() == item.name)
            {
                NetworkSender.Instance().sendFriendApplyResult(RestartWindow, mailList[i].mailID, 1);
                break;
            }
        }
    }

    /// <summary>
    /// 拒绝好友申请
    /// </summary>
    /// <param name="item"></param>
    private void RefuseFriendApply(GameObject item)
    {
        for (int i = 0; i < mailList.Count; i++)
        {
            if (mailList[i].mailID.ToString() == item.name)
            {
                NetworkSender.Instance().sendFriendApplyResult(RestartWindow, mailList[i].mailID, 2);
                break;
            }
        }
    }

    /// <summary>
    /// 刷新界面信息
    /// </summary>
    /// <param name="isSuccess"></param>
    private void RestartWindow(bool isSuccess)
    {

        GameObject.FindWithTag("main_controller").SendMessage("updateUserInfo");
        if (isFriendMail)
        {
            ScrollData scData = new ScrollData(scrollBar.scrollValue);
            Obj_MyselfPlayer.GetMe().SetScrollValue(friendApply_Key, scData);
        }
        else
        {
            ScrollData scData = new ScrollData(scrollBar.scrollValue);
            Obj_MyselfPlayer.GetMe().SetScrollValue(normalMail_Key, scData);
        }
        NetworkSender.Instance().getMailList(UpdateMailListDone);

    }

    /// <summary>
    /// 读邮件
    /// </summary>
    /// <param name="item"></param>
    private void ShowMailContent(GameObject item)
    {
        for (int i = 0; i < mailList.Count; i++)
        {
            if (mailList[i].mailID.ToString() == item.name)
            {
                Obj_MyselfPlayer.GetMe().selectMail = mailList[i];
                NetworkSender.Instance().readMail(OnShowMailContentDone, mailList[i].mailID);
                break;
            }
        }
    }

    /// <summary>
    /// 点击读取信息回调 切换到查看邮件界面
    /// </summary>
    /// <param name="isSuccess"></param>
    private void OnShowMailContentDone(bool isSuccess)
    {
        int mailCount = 0;
        for (int i = 0; i < mailList.Count; i++)
        {
            if (mailList[i].mailType == 2)
            {
                mailCount++;
            }
            else
            {
                if (mailList[i].mailState == 1)
                {
                    mailCount++;
                }
            }
        }
        mainLogic.SendMessage("OnMailCheckWindow");
    }

    /// <summary>
    /// 删除邮件
    /// </summary>
    /// <param name="item"></param>
    private void DeleteMail(GameObject item)
    {
        for (int i = 0; i < mailList.Count; i++)
        {
            if (mailList[i].mailID.ToString() == item.name)
            {

                NetworkSender.Instance().deleteMail(RestartWindow, mailList[i].mailID);
                break;
            }
        }
    }

    /// <summary>
    /// 回主界面 好友申请返回到邮件
    /// </summary>
    private void ReturnToMainUI()
    {
        if (isFriendMail)
        {
            isFriendMail = false;
            showMessage = true;
            ScrollData scData = new ScrollData(scrollBar.scrollValue);
            Obj_MyselfPlayer.GetMe().SetScrollValue(friendApply_Key, scData);
            NetworkSender.Instance().getMailList(UpdateMailListDone);
        }
        else
        {
            mainLogic.SendMessage("ReturnToMainUI");
        }
    }

    /// <summary>
    /// 显示好友申请
    /// </summary>
    private void ShowFriendApply()
    {
        isFriendMail = true;
        showMessage = true;
        ScrollData scData = new ScrollData(scrollBar.scrollValue);
        Obj_MyselfPlayer.GetMe().SetScrollValue(normalMail_Key, scData);
        NetworkSender.Instance().getMailList(UpdateMailListDone);
    }

    /// <summary>
    /// 一键拒绝
    /// </summary>
    private void RefuseAllApply()
    {
        BoxManager.showConfirmMessage("是否删除当前全部好友申请？", ClientConfigure.title);
        UIEventListener.Get(BoxManager.buttonYes).onClick += RefuseAll;
    }

    /// <summary>
    /// 一键拒绝
    /// </summary>
    /// <param name="go"></param>
    private void RefuseAll(GameObject go)
    {
        NetworkSender.Instance().refuseAllApply(RestartWindow);
    }
}
