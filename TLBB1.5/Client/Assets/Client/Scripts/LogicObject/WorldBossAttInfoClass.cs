using System.Collections;
using System.Collections.Generic;
using GCGame.Table;
using xjgame.message;

public class WorldBossAttInfoClass
{
    public int attTimes;//尝试次数
    public long currentDamage;//本次伤害
    public long totalDamage;//总伤害
    public int attRank;//当前排名
    public int battleCD;//复活时间
    public int buffCount;//祝福次数
    public List<WorldBossDamageRankInfoClass> rankInfoList = new List<WorldBossDamageRankInfoClass>();//其他玩家伤害信息

    public WorldBossAttInfoClass()
    { 
    }

    public WorldBossAttInfoClass(WorldBossAttInfo msg)
    {
        attTimes = msg.AttTimes;
        currentDamage = msg.CurrentDamage;
        totalDamage = msg.TotalDamage;
        attRank = msg.AttRank;
        battleCD = msg.FuhuoTime;
        buffCount = msg.ZhufuTimes;
        if (msg.rankInfoList != null)
        {
            foreach (WorldBossDamageRankInfo data in msg.rankInfoList)
            {
                WorldBossDamageRankInfoClass rankInfo = new WorldBossDamageRankInfoClass(data);
                rankInfoList.Add(rankInfo);
            }
        }
    }
}
