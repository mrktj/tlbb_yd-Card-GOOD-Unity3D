using UnityEngine;
using System.Collections;
using card.net;
using Games.CharacterLogic;
using GCGame.Table;

public class ChangeNameController : MonoBehaviour {
	
	public GameObject mainUILogic;
	public UILabel money;
	public int iMoney = 20;
	public UIInput newName;
	
	enum NameType
	{
		NameTooShort,
		NameTooLong,
		ErrorLetter,
		
		NameOK,
	}
	//private int sex = 1;
	// Use this for initialization
	void Awake(){
		mainUILogic = GameObject.Find("MainUILogic");
		money.text = "[ff231a]"+iMoney;
		newName.text = "";
	}
	
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private int fnameMaxNum;
	private int lnameMaxNum;
	void OnEnable(){
		fnameMaxNum = NameMaxNum(nameType.FIRSTNAME);
		lnameMaxNum = NameMaxNum(nameType.LASTNAME);
		RandomName();
	}
	
	public void Cancel(){
		mainUILogic.SendMessage("OnHelpWindow");
	}
	
	public void Change(){
		string name = newName.text;
		NameType type = CompName(name);
		switch(type){
		case NameType.NameTooShort:
			BoxManager.showMessageByID((int)MessageIdEnum.Msg42);
			break;
		case NameType.NameTooLong:
			BoxManager.showMessageByID((int)MessageIdEnum.Msg43);
			break;
		case NameType.ErrorLetter:
			BoxManager.showMessageByID((int)MessageIdEnum.Msg44);
			break;
		case NameType.NameOK:
			NetworkSender.Instance().sendChangeName(ChangeDone, name);
			break;
		default:
			return;
		}
		Debug.Log("new name: "+name);
	}
	public void ChangeDone(bool isSuccess)
	{
		string name = newName.text;
		if(isSuccess)
		{
			mainUILogic.SendMessage("refreshTopBar");
			switch(Obj_MyselfPlayer.GetMe().changeNameType)
			{
			case 1:
//				("更名成功");
				BoxManager.showMessageByID((int)MessageIdEnum.Msg45);
				break;
			case 2:
//				("元宝不足，不能进行更名");
				BoxManager.showMessageByID((int)MessageIdEnum.Msg46);
				break;
			case 3:
//				("新名字中包含敏感词汇，无法进行更名");
				BoxManager.showMessageByID((int)MessageIdEnum.Msg47);
				break;
			}
			Debug.Log("更改名称成功!");
			newName.text = name;
		}
		else
		{
			Debug.Log("更改名称出错");
		}
	}
	
	private NameType CompName(string sName)
	{
		int cNum = 0;
		int eNum = 0;
		foreach(char s in sName)
		{
			//char ss = sName[i];
			Debug.Log(s.ToString());
			if((s >= 0x4e00)&&(s <= 0x9fbf))//chinese
			{
				cNum++;
			}
			else if(s >= '0'&&s <= '9')//number
			{
				eNum++;
			}
			else if((s >= 'a')&&(s <= 'z'))//letter
			{
				eNum++;
			}
			else if((s >= 'A')&&(s <= 'Z'))//letter
			{
				eNum++;
			}
			else
			{
				return NameType.ErrorLetter;
			}
		}
		if((cNum*2 + eNum)>12)
			return NameType.NameTooLong;
		if((cNum + eNum)<=0)
			return NameType.NameTooShort;
		return NameType.NameOK;
	}
	
	enum nameType{
		FIRSTNAME,
		LASTNAME,
	}
	
	public void RandomName(){//随机名字
		string firstName;
		string lastName;
		firstName = RandomFirstName();
		lastName = RandomLastName();
		newName.text = firstName + lastName;
	}
	private string RandomFirstName(){
		int fid = Random.Range(0 ,fnameMaxNum);
		fid += 1000;
		return TableManager.GetRandomnameByID(fid).Content;
	}
	private string RandomLastName(){
		int lid = Random.Range(0 ,lnameMaxNum);
		lid += 2000;
		return TableManager.GetRandomnameByID(lid).Content;
	}
	private int NameMaxNum(nameType type){
		int num=0;
		switch(type)
		{
		case nameType.FIRSTNAME:
			foreach(DictionaryEntry tr in TableManager.GetRandomname())
			{
				Tab_Randomname tbr = tr.Value as Tab_Randomname;
				if(tbr.Type == 0)
					num++;
			}
			break;
		case nameType.LASTNAME:
			foreach(DictionaryEntry tr in TableManager.GetRandomname())
			{
				Tab_Randomname tbr = tr.Value as Tab_Randomname;
				if(tbr.Type == 1)
					num++;
			}
			break;
		default:
			num = 0;
			break;
		}
		return num;
	}
}
