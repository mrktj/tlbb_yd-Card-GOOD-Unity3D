using UnityEngine;
using System.Collections;
using GCGame.Table;

public class QxzbRewardInstrucitonController : MonoBehaviour {
	
	
	//说明文字的文本
	public UILabel instructionLabel;
	
	public GameObject mainUILogicObj;
	void OnEnable()
	{
		if(mainUILogicObj == null)
		{
			 mainUILogicObj = GameObject.Find("/MainUILogic");
		}
		
		//活动奖励说明文字
		Tab_Language language = TableManager.GetLanguageByID(100092);
		if(language != null 
			&& instructionLabel != null)
		{
			instructionLabel.text = language.Chinese;
		}
		
		
	}	
	
	void ReturnToQxzbPvP()
	{
		if(mainUILogicObj != null)
		{
			mainUILogicObj.GetComponent<MainUILogic>().OnQxzbPvPWindow();
		}
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
