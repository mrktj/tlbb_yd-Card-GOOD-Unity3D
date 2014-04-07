using System.Collections;
using xjgame.message;
using Games.LogicObject;

public class UserFinishTask
{
	public int templetID;//任务id
	public int gold;//金币
	public int dollar;//元宝
	public int power;//体力
	public UserCardItem card=new UserCardItem();//卡牌
	
	public UserFinishTask ()
	{
		
	}
	
	public UserFinishTask(SCFinishTask msg)
	{
//		templetID=msg.Templet_id;
//		gold=msg.Money;
//		dollar=msg.Diamond;
//		power=msg.Power;
//		if(msg.Card!=null)
//		{
//        	card.cardID = msg.Card.CardId;
//        	card.templateID = msg.Card.TemplateId;
//        	card.level = msg.Card.Level;
//        	card.fightIndex = msg.Card.FightIndex;
//		}
	}
}

