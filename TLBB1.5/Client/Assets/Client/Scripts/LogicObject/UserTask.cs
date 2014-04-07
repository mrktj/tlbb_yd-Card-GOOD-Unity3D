using System.Collections;
using xjgame.message;

public class UserTask 
{
	public int templetID;//任务id
	public int process;//完成进度
	public int state;//完成情况 1完成 2进行中
	public string title;//任务名称
	public string content;//任务内容
	public int request;//任务要求
	public int cardTempletID;//card模板id,用户好友信息页面显示
	public int taskType;// 任务完成条件 0:登陆,1:累计登陆,2:ID等级,3:首次充值奖励,4:累计充值,5:完成关卡,6:累计战斗,7:累计胜利,8:连续登陆,9:连续登陆累积奖励；10:第一次改名；11:第一次副本失败；12:PVP排名；13:首次元宝抽奖奖励）
	public int gold;//金币
	public int dollar;//元宝
	public int propID;//道具ID
	public int power; //
	
	public UserTask()
	{
		
	}
	
	public UserTask(PBTask msg)
	{
		this.templetID = msg.Templet_id;
		this.process = msg.Process;
		this.state = msg.State;
	}
	
}