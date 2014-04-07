using UnityEngine;
using System.Collections;
using Games.LogicObject;
using GCGame.Table;

public class UISkillHeroItemView : MonoBehaviour
{

    public UILabel cardName;//卡牌名称
    public UISprite cardProperty;//卡牌五行
    public UISprite cardIconSpriteIcon;//卡牌头像
    public UISprite cardIconSpriteFrame;//卡牌背景
    public UISprite cardIconSpriteBG;//卡牌边框
    public UISprite skillInfoIcon;//等级或经验图标
    public UILabel cardLev;//卡牌等级
    public UILabel skillName;//技能名称
    public UILabel skillInfo;//技能信息 等级或提供经验
    public UICheckbox chexkBox;//选中checkbox
    public GameObject cardIconBtn;//点击头像按钮
    public UISprite backGround;//背景
    public UILabel cardHp;//血量
    public UILabel cardAttack;//攻击

    /// <summary>
    /// 初始化Item
    /// </summary>
    /// <param name="card"></param>
    /// <param name="isUpdateChild"></param>
    public void InitItem(UserCardItem card, bool isUpdateChild)
    {
        transform.localPosition = new Vector3(0, 0, -1);
        transform.localScale = new Vector3(1, 1, 1);
        transform.name = card.cardID.ToString();
        cardHp.text = card.GetHp().ToString();
        cardAttack.text = card.GetAttack().ToString();
        cardLev.text = card.level.ToString();
        Tab_Studyskill tabStydyskill = TableManager.GetStudyskillByID(card.skillStudyId);
        if (tabStydyskill != null)
        {
            string color = "";
            switch (tabStydyskill.SkillQuality)
            {
                case 0:
                    color = "[2d8560]";
                    break;
                case 1:
                    color = "[2368ad]";
                    break;
                case 2:
                    color = "[852bed]";
                    break;
                default:
                    break;
            }
            skillName.text = color + tabStydyskill.SkillName;
            if (isUpdateChild)
            {
                skillInfo.text = tabStydyskill.SkillExperience.ToString();
            }
            else
            {
                skillInfo.text = card.skillStudyLev.ToString();
            }
        }
        else
        {
            skillName.text = "";
            skillInfo.text = "0";
        }
        if (isUpdateChild)
        {
            skillInfoIcon.spriteName = "zhujiemian_zi_13";
            skillInfoIcon.transform.localScale = new Vector3(45, 24, 1);
        }
        else
        {
            skillInfoIcon.spriteName = "haoyou_dengji";
            skillInfoIcon.MakePixelPerfect();
        }
        Tab_Card tabCard = TableManager.GetCardByID(card.templateID);
        if (tabCard != null)
        {
            Tab_Appearance tabAppearance = TableManager.GetAppearanceByID(tabCard.Appearance);
            if (tabAppearance != null)
            {
                cardName.text = LanguageManger.GetWords(tabAppearance.Name);
                string atlasname = tabAppearance.HeadIcon;
                AtlasManager.Instance.setHeadName(cardIconSpriteIcon, atlasname);
            }
            cardIconSpriteFrame.spriteName = UserCardItem.littleCardFrameName[tabCard.Star];
            cardIconSpriteBG.spriteName = UserCardItem.littleCardBorderName[tabCard.Star];
            cardProperty.spriteName = UserCardItem.elementTypeName[tabCard.Element];
        }
        chexkBox.isChecked = false;
        backGround.alpha = 1;
    }

    /// <summary>
    /// UI恢复初始状态
    /// </summary>
    public void UIClear()
    {
        chexkBox.isChecked = false;
        backGround.alpha = 1;
    }

    /// <summary>
    /// 头像按钮
    /// </summary>
    /// <returns></returns>
    public GameObject GetCardIconBtn()
    {
        return cardIconBtn;
    }

    /// <summary>
    /// CheckBox
    /// </summary>
    public bool IsChecked
    {
        get { return chexkBox.isChecked; }
        set { chexkBox.isChecked = value; }
    }
}
