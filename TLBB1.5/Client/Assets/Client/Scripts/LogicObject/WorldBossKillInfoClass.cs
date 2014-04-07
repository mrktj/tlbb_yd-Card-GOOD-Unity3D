using System.Collections;
using System.Collections.Generic;
using GCGame.Table;
using xjgame.message;

public class WorldBossKillInfoClass
{
    public WorldBossClass lastBossInfo = new WorldBossClass();//boss信息
    public WorldBossDamageRankInfoClass lastKiller = new WorldBossDamageRankInfoClass();//最后一刀
    public List<WorldBossDamageRankInfoClass> top3Killer = new List<WorldBossDamageRankInfoClass>();//伤害前三

    public WorldBossKillInfoClass()
    {
    }

    public WorldBossKillInfoClass(WorldBossKillInfo msg)
    {
        if (msg.LastBossInfo != null)
        {
            lastBossInfo = new WorldBossClass(msg.LastBossInfo);
        }
        if (msg.LastKiller != null)
        {
            lastKiller = new WorldBossDamageRankInfoClass(msg.LastKiller);
        }
        if (msg.top3KillerList != null)
        {
            foreach (WorldBossDamageRankInfo data in msg.top3KillerList)
            {
                WorldBossDamageRankInfoClass info = new WorldBossDamageRankInfoClass(data);
                top3Killer.Add(info);
            }
        }
    }
}
