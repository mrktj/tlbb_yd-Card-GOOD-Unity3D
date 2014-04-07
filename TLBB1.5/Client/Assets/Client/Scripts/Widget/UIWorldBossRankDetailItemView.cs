using UnityEngine;
using System.Collections;
using GCGame.Table;
using Games.LogicObject;

public class UIWorldBossRankDetailItemView : MonoBehaviour
{
    public UILabel playerName;
    public UILabel level;
    public UISprite iconBG;
    public UISprite iconFrame;
    public UISprite iconCard;
    public UILabel rank;
    public UILabel fight;
    public UILabel damage;

    public void InitItem(WorldBossDamageRankInfoClass rankInfo)
    {
        transform.localPosition = new Vector3(0, 0, -1);
        transform.localScale = new Vector3(1, 1, 1);
        playerName.text = rankInfo.name;
        level.text = rankInfo.level.ToString();
        rank.text = rankInfo.rank.ToString();
        fight.text = rankInfo.fighting.ToString();
        damage.text = rankInfo.totalDamage.ToString();
        Tab_Card tabCard = TableManager.GetCardByID(rankInfo.templateId);
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
    }
}
