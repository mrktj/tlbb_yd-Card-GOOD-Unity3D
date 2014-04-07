using System.Collections;
using card.net;
using xjgame.message;

public class AssistFriend 
{
	
	public long guid;//好友唯一id
	public string name;//好友名字
	public int	level;//好友等级
	public bool isMyFriend;//
	public int friendShipNum;//
	public long cardGuiId;//卡牌模板id
	public int cardTempleId;//卡牌模板id
	public int cardSkillId;//助战英雄队长技
	public int cardLevel;//助战卡牌等级
	public int attackTimes;//攻击强化次数
	public int hpTimes;//生命强化次数
	public int skillLevel;
    public int studySkillId;//学习技能id
    public int studySkillLev;//学习技能等级
    public int nQxzbTopStar;//群雄爭霸對用的星級第一名

	public AssistFriend()
	{
		
	}
	
	public AssistFriend(/*PBMessage.*/PBFriend msg)
	{
		this.guid = msg.Guid;
		this.name = msg.Name;
		this.level = msg.Level;
		this.isMyFriend =msg.Assist_is_my_friend == 1;
		this.friendShipNum = msg.Assist_friendship_num;
		this.cardGuiId = msg.Assist_card_guid;
		this.cardTempleId = msg.Assist_card_templet_id;
		this.cardSkillId = msg.Assist_card_skill_id;
		this.cardLevel = msg.Assist_card_level;
		this.attackTimes=msg.Assist_card_att_times;
		this.hpTimes=msg.Assist_card_hp_times;
		this.skillLevel=msg.Assist_card_skill_lev;
        this.studySkillId = msg.StudySkillID;
        this.studySkillLev = msg.StudySkillLev;
        this.nQxzbTopStar = msg.QxzbTopStar;
	}
	
}

