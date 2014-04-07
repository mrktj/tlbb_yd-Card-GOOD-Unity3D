using UnityEngine;
using System.Collections;
using Games.CharacterLogic;
using GCGame.Table;
using card.net;
using Games.LogicObject;

public class MailCheckController : MonoBehaviour
{

    private MailInfo mail = new MailInfo();
    private MainUILogic mainLogic;
    public UILabel senderName;
    public UILabel mailContent;
    public GameObject sendBtn;
    //public GameObject mailIcon;

    void Start()
    {
        mainLogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
    }
    void OnEnable()
    {
        mail = Obj_MyselfPlayer.GetMe().selectMail;
        senderName.text = mail.source;
        string contents = "";
        if (mail.mailType == 3)
        {
            contents += "附件：";
            switch (mail.itemType)
            {	//1:钱 2:宝石 3:卡牌 4:道具
                case 1:
                    contents += "金币 x " + mail.itemValue.ToString();
                    break;
                case 2:
                    contents += "元宝 x " + mail.itemValue.ToString();
                    break;
                case 3:
                    contents += "卡牌：" + TableManager.GetCardByID(mail.itemValue).Note + " x " + mail.itemNum.ToString();
                    break;
                case 4:
                    int itemid = mail.itemValue;
                    string itemName = "";
                    if (itemid == 10000)
                        itemName = "大力丸";
                    else if (itemid == 10001)
                        itemName = "强身丹";
                    contents += "道具：" + itemName + " x " + mail.itemNum.ToString();
                    break;
            }
            contents += "\r\n";
            contents += "正文：\r\n";
            contents += "    " + mail.content;
            mailContent.text = contents;
        }
        else
            mailContent.text = mail.content;
        RefreshUI();
    }

    public void RefreshUI()
    {
        if (mail.mailType == 1)
        {
            //好友消息邮件
            if (mail.icon_id != -1)
            {
                sendBtn.SetActive(true);
            }
            else
            {
                sendBtn.SetActive(false);
            }
            //mailIcon.SetActive(false);
        }
        else
        {
            //系统邮件
            sendBtn.SetActive(false);
            //mailIcon.SetActive(true);
            //UISprite icon = mailIcon.transform.FindChild("CardIconBtn/Sprite-Icon").GetComponent<UISprite>();
            //switch (mail.itemType)
            //{
            //    case 1:
            //        //钱
            //        icon.spriteName = "jinbi";
            //        break;
            //    case 2:
            //        //宝石
            //        icon.spriteName = "yuanbao";
            //        break;
            //    case 3:
            //        //卡牌
            //        if (TableManager.GetCardByID(mail.icon_id) != null)
            //        {
            //            UISprite icon_bg = mailIcon.transform.FindChild("CardIconBtn/Sprite-Frame").GetComponent<UISprite>();
            //            UISprite icon_border = mailIcon.transform.FindChild("CardIconBtn/Sprite-BG").GetComponent<UISprite>();
            //            int icon_star = TableManager.GetCardByID(mail.itemValue).Star;
            //            icon_bg.spriteName = UserCardItem.littleCardFrameName[icon_star];
            //            icon_border.spriteName = UserCardItem.littleCardBorderName[icon_star];
            //            string atlasname = TableManager.GetAppearanceByID(TableManager.GetCardByID(mail.icon_id).Appearance).HeadIcon;
            //            AtlasManager.Instance.setHeadName(icon, atlasname);
            //        }
            //        break;
            //    case 4:
            //        //道具
            //        icon.spriteName = "daojubao";
            //        break;
            //    default:
            //        break;
            //}
        }
    }

    public void SendMail()
    {
        UserFriend uf = new UserFriend();
        uf.name = mail.source;
        uf.guid = mail.friendID;
        mainLogic.sendMailState = 2;//2013-7-30 Jack Wen
        mainLogic.friendInfo = uf;
        mainLogic.OnSendMailWindow();
    }

    public void Cancle()
    {
        mainLogic.OnMailWindow();
    }
}
