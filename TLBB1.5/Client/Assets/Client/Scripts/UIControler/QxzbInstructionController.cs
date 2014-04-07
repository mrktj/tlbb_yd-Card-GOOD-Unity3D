using UnityEngine;
using System.Collections;
using GCGame.Table;

public class QxzbInstructionController : MonoBehaviour {
	
	//说明文字的文本
	public UILabel instructionLabel;
	
	public GameObject mainUILogicObj;
	
	// Use this for initialization
	void Start () {
		
	}
	
	void OnEnable()
	{
		if(mainUILogicObj == null)
		{
			mainUILogicObj = GameObject.Find("/MainUILogic");
		}
		//新PVP说明
		Tab_Language language = TableManager.GetLanguageByID(100088);
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
	
	// Update is called once per frame
	void Update () {
	
	}
}
