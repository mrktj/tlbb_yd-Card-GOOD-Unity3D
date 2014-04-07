using System.Collections;
using xjgame.message;
using Games.LogicObject;

public class MailInfo 
{
	public long mailID;//邮件id
	public int mailType;//邮件类型 1:信息 2:好友申请 3:物品
	public string source;//发送人
	public int level;//等级
	public long friendID;//好友id
	public string content;//邮件内容
	public long fromTime;//发送时间
	public int itemType;//1:钱 2:宝石 3:卡牌 4:道具
	public int itemValue;//如果属于钱和宝石：数量  卡牌道具：id
	public int mailState;//1：已读2：未读3已收取
	public int icon_id;//图id -1为系统信息
	
	public long cardId;
	public int cardTempleId;//卡牌模板id
	public int cardSkillId;//助战英雄队长技
	public int cardLevel;//助战卡牌等级
	public int attackTimes;//攻击强化次数
	public int hpTimes;//生命强化次数
	public int skillLevel;//技能等级
    public int itemNum;//卡牌道具数量
    public int studySkillId;//学习技能id
    public int studySkillLev;//学习技能等级
	
	
	//public UserCardItem card=new UserCardItem();//卡牌
	
	public MailInfo()
	{
		
	}
	
	public MailInfo(PBMail msg)
	{
		this.mailID=msg.Mail_id;
		this.mailType=msg.Type;
		if(msg.Type==3)
		{
			this.source="系统邮件";
		}
		else
		{
			this.source=msg.From;
		}
		this.level=msg.Level;
		this.friendID=msg.Friend_id;
		this.content=msg.Content;
		this.fromTime=msg.From_time;
		this.itemType=msg.Item_type;
		this.itemValue=msg.Item_value;
		this.mailState=msg.State;
		this.icon_id=msg.Icon_id;
		this.cardTempleId = msg.Assist_card_templet_id;
		this.cardSkillId = msg.Assist_card_skill_id;
		this.cardLevel = msg.Assist_card_level;
		this.attackTimes=msg.Assist_card_att_times;
		this.hpTimes=msg.Assist_card_hp_times;
		this.skillLevel=msg.Assist_card_skill_lev;
		this.cardId=msg.Assist_card_guid;
        this.itemNum = msg.Item_num;
        this.studySkillId = msg.StudySkillID;
        this.studySkillLev = msg.StudySkillLev;
//		if(msg.Card!=null)
//		{
//        	card.cardID = msg.Card.CardId;
//        	card.templateID = msg.Card.TemplateId;
//        	card.level = msg.Card.Level;
//		}
	}
}

