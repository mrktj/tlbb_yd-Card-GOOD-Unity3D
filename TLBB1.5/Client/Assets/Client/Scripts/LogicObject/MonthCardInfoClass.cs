using System.Collections;
using xjgame.message;
using System.Collections.Generic;


public class MonthCardInfoClass
{
    public int totalTime;//活动持续时间
    public int remainTime;//活动剩余时间
    public int totalPurchase;//活动期间总共要消费金额
    public int currentPurchase;//活动期间当前已消费金额
    public List<MonthRewardClass> rewardsList = new List<MonthRewardClass>();//活动奖励
    public int monthCardState;//活动当前状态，关闭：0；开启：1；完成：2；已领取：3
    public int rewardRemainDays;//奖励剩余时间

    public MonthCardInfoClass()
    {

    }
}
