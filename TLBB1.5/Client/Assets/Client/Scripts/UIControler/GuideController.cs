using UnityEngine;
using System.Collections;
using card;
using xjgame.message;
using Games.CharacterLogic;
public class GuideController : MonoBehaviour {
	public GameObject[] preludes;
	public UISprite bg;
	public enum PRELUDE_STEP
	{
		NONE = -1,
		FIRST = 0,
		SECOND = 1,
		THIRD = 2,
		FOURTH = 3,
		FIFTH = 4,
		SIXTH = 5,
		END = 6,
	}
	public PRELUDE_STEP step = PRELUDE_STEP.NONE;
//	private LoginLogic mainLogic;
	// Use this for initialization
	void Start () {
//		mainLogic = GameObject.Find("LoginLogic").GetComponent<LoginLogic>();
		step = PRELUDE_STEP.FOURTH;
		NextStep();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void NextStep()
	{
		switch(step)
		{
		case PRELUDE_STEP.NONE:
			break;
		case PRELUDE_STEP.FIRST:
			preludes[(int)PRELUDE_STEP.FIRST].SetActive(true);
			preludes[(int)PRELUDE_STEP.SECOND].SetActive(false);
			preludes[(int)PRELUDE_STEP.THIRD].SetActive(false);
			preludes[(int)PRELUDE_STEP.FOURTH].SetActive(false);
			preludes[(int)PRELUDE_STEP.FIFTH].SetActive(false);
			preludes[(int)PRELUDE_STEP.SIXTH].SetActive(false);
			step = PRELUDE_STEP.SECOND;
			break;
		case PRELUDE_STEP.SECOND:
			preludes[(int)PRELUDE_STEP.FIRST].SetActive(false);
			preludes[(int)PRELUDE_STEP.SECOND].SetActive(true);
			preludes[(int)PRELUDE_STEP.THIRD].SetActive(false);
			preludes[(int)PRELUDE_STEP.FOURTH].SetActive(false);
			preludes[(int)PRELUDE_STEP.FIFTH].SetActive(false);
			preludes[(int)PRELUDE_STEP.SIXTH].SetActive(false);
			step = PRELUDE_STEP.THIRD;
			break;
		case PRELUDE_STEP.THIRD:
			preludes[(int)PRELUDE_STEP.FIRST].SetActive(false);
			preludes[(int)PRELUDE_STEP.SECOND].SetActive(false);
			preludes[(int)PRELUDE_STEP.THIRD].SetActive(true);
			preludes[(int)PRELUDE_STEP.FOURTH].SetActive(false);
			preludes[(int)PRELUDE_STEP.FIFTH].SetActive(false);
			preludes[(int)PRELUDE_STEP.SIXTH].SetActive(false);
			step = PRELUDE_STEP.FOURTH;
			break;
		case PRELUDE_STEP.FOURTH:
			bg.gameObject.SetActive(false);
			preludes[(int)PRELUDE_STEP.FIRST].SetActive(false);
			preludes[(int)PRELUDE_STEP.SECOND].SetActive(false);
			preludes[(int)PRELUDE_STEP.THIRD].SetActive(false);
			preludes[(int)PRELUDE_STEP.FOURTH].SetActive(true);
			preludes[(int)PRELUDE_STEP.FIFTH].SetActive(false);
			preludes[(int)PRELUDE_STEP.SIXTH].SetActive(true);
			step = PRELUDE_STEP.FIFTH;
			break;
		case PRELUDE_STEP.FIFTH:
			bg.gameObject.SetActive(false);
			preludes[(int)PRELUDE_STEP.FIRST].SetActive(false);
			preludes[(int)PRELUDE_STEP.SECOND].SetActive(false);
			preludes[(int)PRELUDE_STEP.THIRD].SetActive(false);
			preludes[(int)PRELUDE_STEP.FOURTH].SetActive(false);
			preludes[(int)PRELUDE_STEP.FIFTH].SetActive(true);
			preludes[(int)PRELUDE_STEP.SIXTH].SetActive(true);
			step = PRELUDE_STEP.SIXTH;
			break;
		case PRELUDE_STEP.SIXTH:
			bg.gameObject.SetActive(false);
			preludes[(int)PRELUDE_STEP.FIRST].SetActive(false);
			preludes[(int)PRELUDE_STEP.SECOND].SetActive(false);
			preludes[(int)PRELUDE_STEP.THIRD].SetActive(false);
			preludes[(int)PRELUDE_STEP.FOURTH].SetActive(false);
			preludes[(int)PRELUDE_STEP.FIFTH].SetActive(false);
			preludes[(int)PRELUDE_STEP.SIXTH].SetActive(true);
			Destroy(preludes[(int)PRELUDE_STEP.SIXTH].GetComponent<UIAnchor>());
			TweenPosition tp = preludes[(int)PRELUDE_STEP.SIXTH].AddComponent<TweenPosition>();
			tp.duration = 1.0f;
			tp.eventReceiver = gameObject;
			tp.callWhenFinished = "NextStep";
			tp.method = UITweener.Method.Linear;
			tp.style = UITweener.Style.Once;
			tp.from = preludes[(int)PRELUDE_STEP.SIXTH].transform.localPosition;
			tp.to = new Vector3(0,904,0);
			step = PRELUDE_STEP.END;
			break;
		case PRELUDE_STEP.END:
			//进入虚拟战斗动画
			SCBattleData battleData = ResourceManager.Instance.LoadGuideBattle();
			Obj_MyselfPlayer.GetMe().SetBattleData(battleData);
			Obj_MyselfPlayer.GetMe().curMainCopy = new Games.LogicObject.MainCopy(1003);
			Obj_MyselfPlayer.GetMe().curSubcopy = Obj_MyselfPlayer.GetMe().curMainCopy.subCopys[0];
			Obj_MyselfPlayer.GetMe().isGuideBattle = true;
			GameManager.Instance.LoadLevel(Utils.UI_NAME_Battle);
//			mainLogic.OnCardChooseWindow();
			break;
		}
	}
}
