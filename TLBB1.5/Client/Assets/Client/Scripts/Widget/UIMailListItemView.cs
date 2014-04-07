//
//  UIMailListItemView.cs
//
//  Created by JackWen on 13-11-30.
//  Copyright (c) 2013年 JackWen. All rights reserved.
//
//	Mail UI View
//
using System;
using UnityEngine;
using System.Collections;
using Games.LogicObject;
using GCGame.Table;

public class UIMailListItemView : MonoBehaviour
{

    //friend apply mail
    public UILabel labelName;
    public UILabel labelLevelValue;
    public UILabel labelLevel;

    public UISprite icon;
    public UISprite iconBg;
    public UISprite iconBorder;

    public GameObject cardBtn;
    public GameObject YesBtn;
    public GameObject NoBtn;
    public GameObject[] star;

    public bool InitFriendMailWithMail(MailInfo mail)
    {
        transform.name = mail.mailID.ToString();
        transform.localPosition = new Vector3(0, 0, -1);
        transform.localScale = new Vector3(1, 1, 1);

        if (mail == null)
            return false;

        YesBtn.name = mail.mailID.ToString();
        NoBtn.name = mail.mailID.ToString();
        cardBtn.name = mail.mailID.ToString();
        cardBtn.name = mail.mailID.ToString();
        labelLevel.text = mail.cardLevel.ToString();

        labelName.text = mail.source;
        labelLevelValue.text = mail.level.ToString();

        Tab_Card tabCard = TableManager.GetCardByID(mail.icon_id);
        if (tabCard == null)
            return false;
        Tab_Appearance tabApp = TableManager.GetAppearanceByID(tabCard.Appearance);
        if (tabApp == null)
            return false;
        Tab_Card tabCardTemp = TableManager.GetCardByID(mail.cardTempleId);
        if (tabCardTemp == null)
            return false;

        string atlasname = tabApp.HeadIcon;
        AtlasManager.Instance.setHeadName(icon, atlasname);
        icon.transform.localScale = new Vector3(82, 82, 1);
        //------------------------卡牌背景及外框--------------------------------
        //2013-10-12 Jack Wen
        int icon_star = tabCard.Star;
        iconBg.spriteName = UserCardItem.littleCardFrameName[icon_star];
        iconBorder.spriteName = UserCardItem.littleCardBorderName[icon_star];
        //--------------------------------------------------------------------
        for (int j = 1; j <= 7; j++)
        {
            if (j <= tabCardTemp.Star)
            {
                star[j - 1].SetActive(true);
            }
            else
            {
                star[j - 1].SetActive(false);
            }
        }

        return true;
    }

    public GameObject GetCardIconBtn()
    {
        return cardBtn;
    }
    public GameObject GetYesBtn()
    {
        return YesBtn;
    }
    public GameObject GetNoBtn()
    {
        return NoBtn;
    }


    //mail
    public UILabel labelCount;
    public UILabel labelSource;
    public UILabel labelTime;
    public UILabel labelContent;

    public UISprite mailIcon;
    public UISprite mailIconBg;
    public UISprite mailIconBorder;

    public GameObject readTip;
    public GameObject checkBtn;
    public GameObject deleteBtn;
    public GameObject getBtn;

    public bool InitItemMailWithMail(MailInfo mail)
    {

        transform.name = mail.mailID.ToString();
        transform.localPosition = new Vector3(0, 0, -1);
        transform.localScale = new Vector3(1, 1, 1);

        if (mail == null)
            return false;

        //删除按钮
        deleteBtn.name = mail.mailID.ToString();
        //查看按钮
        checkBtn.name = mail.mailID.ToString();
        //领取按钮
        getBtn.name = mail.mailID.ToString();

        if (mail.mailType == 1)
        {
            //好友消息
            checkBtn.SetActive(true);
            //查看消息
            deleteBtn.SetActive(true);
            //删除邮件
            getBtn.SetActive(false);
            labelCount.gameObject.SetActive(false);
        }
        else if (mail.mailType == 3)
        {
            //系统邮件
            if (mail.mailState == 3)
            {
                //已领取
                deleteBtn.SetActive(true);
                //删除邮件
                getBtn.SetActive(false);
            }
            else if (mail.mailState == 1 || mail.mailState == 2)
            {
                //未领取
                deleteBtn.SetActive(false);
                getBtn.SetActive(true);
                //接受系统邮件
            }
            if (mail.itemNum > 1)
            {
                labelCount.gameObject.SetActive(true);
                labelCount.text = mail.itemNum.ToString();
            }
            else
            {
                labelCount.gameObject.SetActive(false);
            }
            checkBtn.SetActive(true);
            //查看邮件内容
        }

        labelSource.text = mail.source;
        labelTime.text = (new DateTime(1970, 1, 1, 8, 0, 0).AddMilliseconds(mail.fromTime)).ToString("yyy-MM-dd HH:mm:ss");
        if (mail.mailType == 3)
        {
            //邮件类型 3为系统邮件
            switch (mail.itemType)
            {	//1:钱 2:宝石 3:卡牌 4:道具
                case 1:
                    labelContent.text = "金币 x " + mail.itemValue.ToString();
                    break;
                case 2:
                    labelContent.text = "元宝 x " + mail.itemValue.ToString();
                    break;
                case 3:
                    labelContent.text = "卡牌：" + TableManager.GetCardByID(mail.itemValue).Note + " x " + mail.itemNum.ToString();
                    break;
                case 4:
                    int itemid = mail.itemValue;
                    string itemName = "";
                    if (itemid == 10000)
                        itemName = "大力丸";
                    else if (itemid == 10001)
                        itemName = "强身丹";
                    labelContent.text = "道具：" + itemName + " x " + mail.itemNum.ToString();
                    break;
            }
        }
        else
        {
            if (mail.content.Length > 8)
            {
                labelContent.text = mail.content.Substring(0, 8) + "....";
            }
            else
            {
                labelContent.text = mail.content;
            }
        }
        //邮件状态1：已读2：未读3：已收取
        if (mail.mailState == 1)
        {
            readTip.SetActive(true);
        }
        else
        {
            readTip.SetActive(false);
        }
        //邮件图片填充
        if (mail.mailType == 1)
        {
            //好友消息
            if (TableManager.GetCardByID(mail.icon_id) != null)
            {
                string atlasname = TableManager.GetAppearanceByID(TableManager.GetCardByID(mail.icon_id).Appearance).HeadIcon;
                AtlasManager.Instance.setHeadName(mailIcon, atlasname);
                int icon_star = TableManager.GetCardByID(mail.icon_id).Star;
                mailIconBg.spriteName = UserCardItem.littleCardFrameName[icon_star];
                mailIconBorder.spriteName = UserCardItem.littleCardBorderName[icon_star];
                mailIcon.transform.localScale = new Vector3(82, 82, 1);
            }
            else
            {
                if (mail.icon_id == -1)
                {
                    mailIcon.spriteName = "sysmail";
                    mailIconBg.spriteName = "kapai_xiao_huang";
                    mailIconBorder.spriteName = "kapai_waikuang_xiao_huang";
                    mailIcon.transform.localScale = new Vector3(82, 82, 1);
                }
            }
        }
        else
        {
            switch (mail.itemType)
            {
                case 1:
                    //钱
                    mailIcon.spriteName = "jinbi";
                    break;
                case 2:
                    //宝石
                    mailIcon.spriteName = "yuanbao";
                    break;
                case 3:
                    //卡牌
                    if (TableManager.GetCardByID(mail.icon_id) != null)
                    {
                        int icon_star = TableManager.GetCardByID(mail.itemValue).Star;
                        mailIconBg.spriteName = UserCardItem.littleCardFrameName[icon_star];
                        mailIconBorder.spriteName = UserCardItem.littleCardBorderName[icon_star];
                        string atlasname = TableManager.GetAppearanceByID(TableManager.GetCardByID(mail.icon_id).Appearance).HeadIcon;
                        AtlasManager.Instance.setHeadName(mailIcon, atlasname);
                    }
                    break;
                case 4:
                    //道具
                    mailIcon.spriteName = "daojubao";
                    break;
                default:
                    break;
            }
        }

        return true;
    }

    public GameObject GetCheckBtn()
    {
        return checkBtn;
    }
    public GameObject GetDeleteBtn()
    {
        return deleteBtn;
    }
    public GameObject GetGetBtn()
    {
        return getBtn;
    }

}
