using UnityEngine;
using System.Collections;

using Games.CharacterLogic;
using Games.LogicObject;

public class PataController : MonoBehaviour {
	
	public GameObject lefttimeslabel;
	public GameObject currentPatafloorlabel;
	public GameObject startBattleBtn;
	private MainUILogic mainLogic;
	private MainController mainController;
	// Use this for initialization
	void Start () {

	}
	
	void OnEnable(){
		if(mainLogic == null)
			mainLogic = GameObject.Find("MainUILogic").GetComponent<MainUILogic>();
		if(mainController == null)
			mainController = GameObject.Find("MainController").GetComponent<MainController>();
		
		mainController.ShowActivityTopUI(ActivityType.E_ACTIVITY_TYPE_PATA);
		mainLogic.SetMainUIBottomBarActive(true);
		
		if(lefttimeslabel != null)
			lefttimeslabel.GetComponent<UILabel>().text = Obj_MyselfPlayer.GetMe().pataTimes.ToString();
		if(currentPatafloorlabel != null)
			currentPatafloorlabel.GetComponent<UILabel>().text = Obj_MyselfPlayer.GetMe().pataNum.ToString();
		if(Obj_MyselfPlayer.GetMe().pataTimes > 0 || (Obj_MyselfPlayer.GetMe().pataTimes == 0 && Obj_MyselfPlayer.GetMe().pataNum > 0)){
			if( startBattleBtn != null){
				startBattleBtn.collider.enabled = true;
				startBattleBtn.transform.FindChild("Sprite").GetComponent<UISprite>().spriteName = "kaishizhandou";
				startBattleBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "anniu_1";
				UIEventListener.Get(startBattleBtn).onClick += OnStartBattle;
				
			}
		}
		else{
			if( startBattleBtn != null){
				startBattleBtn.collider.enabled = false;
				startBattleBtn.transform.FindChild("Sprite").GetComponent<UISprite>().spriteName = "kaishizhandou_2";
				startBattleBtn.transform.FindChild("Background").GetComponent<UISprite>().spriteName = "anniu_3";
			}
		}
	}
	
	void OnDisable()
	{
		mainController.ShowtopInfo();	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnStartBattle(GameObject go){
		//验证当前领导力是否合法
		int nowLeaderShip = Obj_MyselfPlayer.GetMe().GetLeaderShipValue();
		if( nowLeaderShip > Obj_MyselfPlayer.GetMe().leadership ) {
			Debug.LogWarning("current leadership forward max leadership");
			BoxManager.showMessage("领导力超出上限，请调整当前队伍",ClientConfigure.title);
			return;
		}
		
		if(mainLogic != null)
		{
			Obj_MyselfPlayer.GetMe().battleType = Games.Battle.BattleType.CHONG_LOU;
			mainLogic.OnBattleBefore();
		}
	}
}
