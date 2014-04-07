using System.Collections;
using GCGame.Table;
using xjgame.message;

public class WorldBossDamageRankInfoClass
{
    public int rank;//排名
    public long pid;//玩家id
    public string name;//玩家名字
    public long totalDamage;//玩家伤害
    public int fighting;//战斗力
    public int templateId;//卡牌id
    public int level;//玩家等级

    public WorldBossDamageRankInfoClass()
    { 
    }

    public WorldBossDamageRankInfoClass(WorldBossDamageRankInfo msg)
    {
        rank = msg.Rank;
        pid = msg.Pid;
        name = msg.Name;
        totalDamage = msg.TotalDamage;
        templateId = msg.Cardid;
        fighting = msg.Power;
        level = msg.PlayerLev;
    }
}
