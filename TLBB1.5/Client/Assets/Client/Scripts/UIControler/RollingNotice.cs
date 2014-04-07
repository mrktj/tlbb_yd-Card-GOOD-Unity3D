using UnityEngine;
using System.Collections;
using GCGame.Table;

public class RollingNotice : MonoBehaviour {
	
	public GameObject bottomInfo;
	public GameObject info;//滚动信息
	public GameObject background;//滚动背景
	public Animation aInfo;
	private UISprite backgd;
	
	public int noticeID1 = 0;
	public int noticeID2 = 0;
	static bool moveNotCD = true;
	
	
	private const float changeTime = 2.0f;//公告停留时间，策划 = 2second
	private const float CDTime = 300.0f;//大CD时间，策划 = 5min
	//private static float lastCDTime = 0;
	private static float lastTime = 0;
	
	//----------信息滚动阶段时间点--------------
	private const float step2Time = 2.0f;
	//---------------设置结束------------------------
	
	
	public enum noticeType{
		NONE = 0,
		HELP,		//帮助信息
		SYSTEM,		//系统介绍
		RAIDER,		//攻略信息
		INFO,		//提示信息
		
		NUM,
	}
	
	private enum updateStep{
		NONE,
		
		NOTICEIN,
		NOTICESTAY,
		NOTICEOUT,
	}
	updateStep nowstep = updateStep.NONE;
	
	public static noticeType gblType = noticeType.NONE;
	private static bool showStaticNotice = false;
	private static bool animationPlaying = false;
	// Use this for initialization
	void Start () {
		Init();
	}
	
	// Update is called once per frame
	void Update () {
		if(showStaticNotice)
			return;
		lastTime += Time.deltaTime;
		if(moveNotCD){			
			if(nowstep == updateStep.NOTICESTAY &&
				lastTime > step2Time &&
				!animationPlaying)
			{
				aInfo.Play("RollingNoticeOut");
				animationPlaying = true;
				lastTime = 0;
				nowstep = updateStep.NOTICEOUT;
			}
		}
		else{
			if(nowstep == updateStep.NONE &&
				lastTime >CDTime &&
				!animationPlaying)
			{
				aInfo.Play("RollingNoticeIn");
				animationPlaying = true;
				lastTime = 0;
				nowstep = updateStep.NOTICEIN;
				moveNotCD = true;
			}
		}
		//lastCDTime += Time.deltaTime;
	}
	
	void OnEnable(){
		Init();
		
	}
	public void Init(){
		noticeID1 = 0;
		noticeID2 = RandomNotice();	
		backgd = background.GetComponent<UISprite>();
		animationPlaying = false;
		
		moveNotCD = false;
		nowstep = updateStep.NONE;
		lastTime = CDTime - 5.0f;//延迟5秒，进行界面加载
		HideAll();
		ChangeInfoLabel();
		
		Time.timeScale = 1;//防止战斗结束后时间过快
	}
	public void HideAll(){
		backgd.alpha = 0;
		info.transform.localPosition = new Vector3(-1024, info.transform.localPosition.y, info.transform.localPosition.z);
	}
	
	public void StepStay(){
		nowstep = updateStep.NOTICESTAY;
		lastTime = 0;
		animationPlaying = false;
	}
	public void StepNone(){
		nowstep = updateStep.NONE;
		lastTime = 0;
		moveNotCD = false;
		animationPlaying = false;
	}
	
	
	//Function of random notice
	private int RandomNotice(){
		return RandomNoticeWitnType(gblType);
	}
	
	private int RandomNoticeWitnType(noticeType type = noticeType.NONE){
		int n4;
		int n1;
		int n;
		
		switch(type)
		{
		case noticeType.NONE:{
			int max = NoticeMaxNum(noticeType.NONE);
			n4 = Random.Range((int)noticeType.HELP ,(int)noticeType.INFO);
			n1 = Random.Range(0 ,max);
			n = n4*1000+n1;
		
			if(n != noticeID1 && n != noticeID2 && TableManager.GetRollingnoticeByID(n)!=null)
				return n;
			else
				return RandomNoticeWitnType();
		}
			
		case noticeType.HELP:{
			return RandomCell(noticeType.HELP);
		}
			break;
		case noticeType.SYSTEM:{
			return RandomCell(noticeType.SYSTEM);
		}
			break;
		case noticeType.RAIDER:{
			return RandomCell(noticeType.RAIDER);
		}
			break;
		case noticeType.INFO:{
			return RandomCell(noticeType.INFO);
		}
			break;
		default:
			return 0;
		}
		return 0;
	}
	private int RandomCell(noticeType type)
	{
		int max = NoticeMaxNum(type);
		int n1 = Random.Range(0 ,max);
		int n = (int)type*1000+n1;
	
		if(n != noticeID1 && n != noticeID2 && TableManager.GetRollingnoticeByID(n)!=null)
			return n;
		else if(max == 1)
			return n;
		else
			return RandomCell(type);
	}
	private int NoticeMaxNum(noticeType type){
		int num = 0;
		switch(type)
		{
		case noticeType.NONE:
			num = 40;
			break;
		case noticeType.HELP:
			num = NotiveMaxNumCell(noticeType.HELP);
			break;
		case noticeType.SYSTEM:
			num = NotiveMaxNumCell(noticeType.SYSTEM);
			break;
		case noticeType.RAIDER:
			num = NotiveMaxNumCell(noticeType.RAIDER);
			break;
		case noticeType.INFO:
			num = NotiveMaxNumCell(noticeType.INFO);
			break;
		default:
			return 0;
		}
		return num;
	}
	private int NotiveMaxNumCell(noticeType type){
		int num = 0;
		int typenum = ((int)type-1);
		foreach(DictionaryEntry tr in TableManager.GetRollingnotice())
		{
			Tab_Rollingnotice tbr = tr.Value as Tab_Rollingnotice;
			if(tbr.Type == typenum)
				num++;
		}
		return num;
	}
	
	public static void SetRollingType(noticeType type){
		gblType = type;
		moveNotCD = true;
		//lastCDTime = 0;
		lastTime = 0;
	}
	
	private void ChangeInfoLabel(){	
		int temp = RandomNotice();
		noticeID1 = noticeID2;
		noticeID2 = temp;
		UILabel labelInfo = info.transform.GetComponent<UILabel>();
		labelInfo.text = TableManager.GetRollingnoticeByID(noticeID2).Content;
	}
	
	/* 2013-8-7 Jack Wen
	 * 提供滚动公告接口，用于实现滚动公告的动画切换之后的后续操作*/
	public void showNoticeBar(){
		bottomInfo.SetActive(true);
	}
	public void hideNoticeBar(){
		bottomInfo.SetActive(false);
	}
	
	public void ShowStaticNotice(int noticeID){
		showStaticNotice = true;
		//Stop Animation
		aInfo.Stop();
		//Init Label-Info
		noticeID1 = noticeID2;
		noticeID2 = noticeID;		
		UILabel labelInfo = info.transform.GetComponent<UILabel>();
		labelInfo.text = TableManager.GetRollingnoticeByID(noticeID2).Content;
		
		animationPlaying = false;
		moveNotCD = true;
		nowstep = updateStep.NOTICESTAY;
		lastTime = step2Time;
		//Set inof's location & background' alpha
		info.transform.localPosition = Vector3.zero;
		backgd.alpha = 1;
	}
	public void NotShowStaticNotice(){
		showStaticNotice = false;
		animationPlaying = false;
	}
	
	public static string GetRandomNotice()
	{
		Hashtable table = TableManager.GetRollingnotice();
		ICollection vc = table.Values;
		int randomIndex = Random.Range(0,vc.Count);
		int i = 0;
		foreach(System.Object notice in vc)
		{
			if(i++ == randomIndex)
			{
				return ((Tab_Rollingnotice)notice).Content;
			}
		}
		return "";
	}
	
}
