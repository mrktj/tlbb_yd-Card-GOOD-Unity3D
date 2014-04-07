using UnityEngine;
using System.Collections;

public class BattleFailController : MonoBehaviour {
	
	private MainUILogic mainLogic;
	
	// Use this for initialization
	void Start () {
		mainLogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnDisable()
	{
		GameObject.FindWithTag("main_controller").SendMessage("showBottomBar");
	}
	
	public void onEvolutionWindow()
	{
		//王明磊 : 统计模块代码 -> Statistics
		PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn46).ToString());
		mainLogic.onEvolutionWindow();
	}
	
	public void OnCardStrengthenWindow()
	{
		//王明磊 : 统计模块代码 -> Statistics
		//如果不是Guide阶段,需要统计此按钮的点击信息
		if (!GameManager.Instance.isGuideMode())
			PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn48).ToString());
		mainLogic.OnCardStrengthenWindow();
	}
	
	public void onUpdateWindow()
	{
		//王明磊 : 统计模块代码 -> Statistics
		//如果不是Guide阶段,需要统计此按钮的点击信息
		if (!GameManager.Instance.isGuideMode())
			PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn45).ToString());
		mainLogic.onUpdateWindow();
	}

	public void OnDiamondLotteryWindow()
	{
		mainLogic.OnDiamondLotteryWindow();
	}
	
	public void ReturnToCopyWindow()
	{
		mainLogic.LoadPveBossList();
	}
}
