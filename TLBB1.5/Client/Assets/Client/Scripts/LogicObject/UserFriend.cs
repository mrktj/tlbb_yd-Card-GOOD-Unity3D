using System.Collections;
using xjgame.message;

public class UserFriend 
{
	
	public long guid;//好友唯一id
	public string name;//好友名字
	public int	level;//好友等级
	public int	lastOnlineHours;//XX小时前登录 -1离线
	public int  lastLogoutHours;//XX分钟前登出 -1在线
	public bool canGivePower;
	public bool canGetPower;//接受体力
	public int	acceptPowerDay;//XX天前给您体力
	public int	givePowerDay;//XX天前接受您体力
	
	public int cardTempletID;//card模板id,用户好友信息页面显示
	public int 	skillLevel;
	public int 	cardLevel;
	public int	attAdd;
	public int	hpAdd;
    public int studySkillId;//学习技能id
    public int studySkillLev;//学习技能等级

	public int nQxzbTopStar;//群雄爭霸對用的星級第一名
	
	
	public static string[] friendStarTop = new string[]{
			"",
			"",
			"",
			"firstTitle_lan",
			"firstTitle_zi",
			"firstTitle_cheng",
			"firstTitle_hong",
			"firstTitle_jin"
		};	
	
	public static string[] friendStarTopBg = new string[]{
			"",
			"",
			"",
			"rankBoard_lan",
			"rankBoard_zi",
			"rankBoard_cheng",
			"rankBoard_hong",
			"rankBoard_huang"
		};	
	
	public UserFriend()
	{
		
	}
	
	public UserFriend(PBFriend msg)
	{
		this.guid = msg.Guid;
		this.name = msg.Name;
		this.level = msg.Level;
		this.lastOnlineHours = msg.Last_online_hours;
		this.lastLogoutHours = msg.Last_logout_hours;
		this.canGetPower = msg.Can_get_power == 1;
		this.canGivePower = msg.Can_give_power == 1;
		this.acceptPowerDay = msg.Accept_power_day;
		this.givePowerDay = msg.Give_power_day;
		this.cardTempletID = msg.Assist_card_templet_id;
		
		this.skillLevel = msg.Assist_card_skill_lev;
		this.cardLevel = msg.Assist_card_level;
		this.attAdd = msg.Assist_card_att_times;
		this.hpAdd = msg.Assist_card_hp_times;
        this.studySkillId = msg.StudySkillID;
        this.studySkillLev = msg.StudySkillLev;
		
		this.nQxzbTopStar = msg.QxzbTopStar;
	}
	
}

