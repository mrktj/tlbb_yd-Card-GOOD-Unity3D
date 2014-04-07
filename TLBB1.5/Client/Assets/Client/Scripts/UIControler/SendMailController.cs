using UnityEngine;
using System.Collections;
using card.net;
using Games.CharacterLogic;

public class SendMailController : MonoBehaviour {
	private MainUILogic mainLogic;
	public UserFriend friendInfo;
	public UILabel inputText;
	public UILabel fontCount;
	public UILabel friendName;
	public UIInput input;
	private int fontMax=50;
	// Use this for initialization
	void Start () {
	}
	
	void Update()
	{
		fontCount.text=(fontMax-input.text.Length).ToString();
	}
	
	void OnEnable()
	{
		mainLogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
		friendInfo=mainLogic.friendInfo;
		friendName.text=friendInfo.name;
	}
	
	void OnDisable()
	{
		inputText.text="";
		input.text="";
		friendName.text="";
	}
	
	public void Cancle()
	{
		//-------------------------------------------------------------------------
		//2013-7-30 Jack Wen
		int state = mainLogic.GetComponent<MainUILogic>().sendMailState;
		if(state == 1)
		{
			Obj_MyselfPlayer.GetMe().currentFriend = mainLogic.GetComponent<MainUILogic>().friendInfo;
			mainLogic.SendMessage("LoadFriendInfoWindow");
		}
		else
			mainLogic.OnMailWindow();
		//-------------------------------------------------------------------------
	}
	
	public void SendMail()
	{
		if(input.text.Length==0)
		{
//			BoxManager.showMessage("邮件内容不能为空");
			BoxManager.showMessageByID((int)MessageIdEnum.Msg58);
			return;
		}
		NameType type = CompName(input.text);
		if(type==NameType.ErrorLetter)
		{
            BoxManager.showMessageByID((int)MessageIdEnum.Msg233);
			return;
		}
		NetworkSender.Instance().sendMail(OnSendMailDone,friendInfo.guid,inputText.text);
	}
	
	private NameType CompName(string sName)
	{
		int cNum = 0;
		int eNum = 0;
		string code = "~！@#￥%……&*（）——+·-=【】、{}|；’：“，。、《》？”";
		foreach(char s in sName)
		{
			//char ss = sName[i];
			Debug.Log(s.ToString());
			if((s >= 0x4e00)&&(s <= 0x9fbf))//chinese
			{
				cNum++;
			}
			else if((int)s>=0&&(int)s<128)
			{
				eNum++;
			}
			else if((s>=0x21)&&(s<=0x7e))
			{
				eNum++;
			}
			else if(code.Contains(s.ToString()))
			{
				eNum++;
			}
			else
			{
				return NameType.ErrorLetter;
			}
		}
		return NameType.NameOK;
	}
	
	enum NameType
	{
		NameTooShort,
		NameTooLong,
		ErrorLetter,
		NameOK,
	}
	
	public void OnSendMailDone(bool isSuccess)
	{
		mainLogic.backToPreviousWindow();
//		if(Obj_MyselfPlayer.GetMe().mailState==1)
//		{
//			mainLogic.backToPreviousWindow();
//		}
//		else
//		{
//			BoxManager.showMessage("好友收件箱已满");
//			return;
//		}
	}
	
}
