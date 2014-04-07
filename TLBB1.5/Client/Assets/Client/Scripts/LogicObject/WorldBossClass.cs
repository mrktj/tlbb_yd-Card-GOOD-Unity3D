using System.Collections;
using Games.LogicObject;
using GCGame.Table;
using xjgame.message;

public class WorldBossClass
{
    public int id;//boss id
    public string name; //boss名称
    public string des;//boss描述
    public int templateId;//卡牌id
    public long currentHp;//当前血量
    public long totalHp;//总血量
    public int activeFlag;//是否开启 0未开启 1开启
    public int remainOpenTime;//距离开启时间
    public int aliveTime;//存活时间

    public WorldBossClass()
    { 
    }

    public WorldBossClass(WorldBoss msg)
    {
        id = msg.Id;
        name = msg.Name;
        templateId = msg.Cardid;
        currentHp = msg.CurrentHp;
        totalHp = msg.TotalHp;
        activeFlag = msg.ActiveFlag;
        remainOpenTime = msg.RemainOpenTime;
        aliveTime = msg.AliveTime;
        Tab_Card tab_card = TableManager.GetCardByID(msg.Cardid);
        if (tab_card != null)
        {
            Tab_Appearance tab_Appearance = TableManager.GetAppearanceByID(tab_card.Appearance);
            if (tab_Appearance != null)
            {
                name = LanguageManger.GetWords(tab_Appearance.Name);
                des = LanguageManger.GetWords(tab_Appearance.Story);
            }
        }
    }
}
