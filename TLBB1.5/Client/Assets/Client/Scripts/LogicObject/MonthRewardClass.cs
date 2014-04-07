using System.Collections;
using xjgame.message;

public class MonthRewardClass
{
    public int type;//1=金钱 2=卡牌 3=道具 4=元宝
    public int id;//只有卡牌和道具有值，金钱和元宝为-1
    public int num;//奖励数量

    public MonthRewardClass()
    {
    }
}