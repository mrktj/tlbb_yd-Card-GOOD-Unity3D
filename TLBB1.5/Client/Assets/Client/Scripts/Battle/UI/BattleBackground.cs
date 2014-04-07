using UnityEngine;
using System.Collections;

using GCGame.Table;
using Module.Log;
using Games.CharacterLogic;
using Games.LogicObject;
using Games.Battle;

public class BattleBackground : MonoBehaviour
{
	
	public GameObject headSprite;
	public GameObject bottomSprite;
	public GameObject middleSprite;
	
	public GameObject[] ChonglouSprite ;
	
	private float fastSpeed = 280f;
	private float slowSpeed = 142f;
	private float moveSpeed;
	private int subCopyID = -1;
	private bool mIsMove = false;
	private float yTop = 1024f;
	private float yBottom = 0f;
	private int mFrameCounter = 0;
	private int roundCounter = 1;
    private const float bgPos_1 = -50f;
	private const float bgPos_2 = -420f;
	private const float bgPos_3 = -840f;
	private BattleType battleType = BattleType.PVE;
	// Use this for initialization
	void Start ()
	{
		battleType = Obj_MyselfPlayer.GetMe ().battleType;
		ResourceManager.Instance.load_map_num = 0;
		if(battleType == BattleType.CHONG_LOU)
		{
			StartCoroutine (ResourceManager.Instance.AsyncLoadBattleBackground (ChonglouSprite[0], "BattleUICollectionChonglou", "qianchonglou"));
			StartCoroutine (ResourceManager.Instance.AsyncLoadBattleBackground (ChonglouSprite[1], "BattleUICollectionChonglou", "qianchonglou"));
			StartCoroutine (ResourceManager.Instance.AsyncLoadBattleBackground (ChonglouSprite[2], "BattleUICollectionChonglou", "qianchonglou"));
			
			return;
		}
		
		string sceneLib = "";
		string sceneName = "";
        
		
		switch(Obj_MyselfPlayer.GetMe ().battleType)
		{
		case BattleType.PVE:
            Tab_Copydetail tab_det = TableManager.GetCopydetailByID (subCopyID);
			if (tab_det == null) 
            {
				Debug.LogWarning ("SetSubCopyID(), set scene background failed! id = " + subCopyID);
				//这里如果没有,用第一个副本的场景, 无量剑西宗	1
				subCopyID = 134;
				tab_det = TableManager.GetCopydetailByID (subCopyID);
			}
			sceneLib = tab_det.SceneLib;
			sceneName = tab_det.SceneName;
			break;
		case BattleType.PVP:
		case BattleType.QxzbPvP:
            Tab_Copydetail tab_detail = null;
			while (tab_detail == null) 
            {
				int va = Random.Range (1, 180);
				tab_detail = TableManager.GetCopydetailByID (va);
			}
            sceneLib = tab_detail.SceneLib;
		    sceneName = tab_detail.SceneName;
			break;
		case BattleType.WORLD_BOSS:
			Tab_Worldboss tab_worldBoss = TableManager.GetWorldbossByID(Obj_MyselfPlayer.GetMe().activeBoss.id);			
			if(tab_worldBoss == null)
			{
				Debug.LogError("world_boss id error");
			}
			else
			{
				sceneLib = tab_worldBoss.SceneLib;
				sceneName = tab_worldBoss.SceneName;
			}
			break;
		}
				
		GameObject battle_logic = GameObject.Find ("BattleLogic");
		roundCounter = battle_logic.GetComponent<BattleLogic> ().GetBattleCore ().GetBattlePlayer ().RoundCounter;
        if (roundCounter == 1)        {            Vector3 temp = transform.localPosition;            temp.y = bgPos_1;            transform.localPosition = temp;        } 
        else if (roundCounter == 2)
        {
			Vector3 temp = transform.localPosition;
			temp.y = bgPos_2;
			transform.localPosition = temp;
		} 
        else if (roundCounter == 3) 
        {
			Vector3 temp = transform.localPosition;
			temp.y = bgPos_3;
			transform.localPosition = temp;
		}
		StartCoroutine (ResourceManager.Instance.AsyncLoadBattleBackground (headSprite, sceneLib, sceneName + "-1"));
		StartCoroutine (ResourceManager.Instance.AsyncLoadBattleBackground (middleSprite, sceneLib, sceneName + "-2"));
		StartCoroutine (ResourceManager.Instance.AsyncLoadBattleBackground (bottomSprite, sceneLib, sceneName + "-3"));
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (battleType!= BattleType.CHONG_LOU && transform.localPosition.y < -1240f) {
			transform.localPosition = new Vector3 (transform.localPosition.x, -1240f, transform.localPosition.z);
		}
	}

	public void MoveFast ()
	{
		mIsMove = true;
		moveSpeed = fastSpeed;
	}

	public void MoveSlow ()
	{
		mIsMove = true;
		moveSpeed = slowSpeed;
	}

	public void Stop ()
	{
		mIsMove = false;
		moveSpeed = slowSpeed;
	}

	public void SetSubCopyID (int id)
	{
		subCopyID = id;
		Tab_Copydetail tab_detail = TableManager.GetCopydetailByID (subCopyID);
		if (tab_detail == null) {
			LogModule.ErrorLog ("SetSubCopyID(), set scene background failed! id = " + id);
		}
	}

	public void Move (float dt)
	{
//		Vector3 pos1 = headSprite.transform.localPosition;
//		Vector3 pos2 = middleSprite.transform.localPosition;
//		Vector3 pos3 = bottomSprite.transform.localPosition;
//		pos1.y -= moveSpeed * dt;
//		pos2.y -= moveSpeed * dt;
//		pos3.y -= moveSpeed * dt;
//		
//		headSprite.transform.localPosition = pos1;
//		middleSprite.transform.localPosition = pos2;
//		bottomSprite.transform.localPosition = pos3;
		if (mIsMove == false) {
			return;
		}
		
		if(battleType == BattleType.CHONG_LOU)
		{
			for(int i = 0; i < ChonglouSprite.Length; i++)
			{
				Vector3 p = ChonglouSprite[i].transform.localPosition;
				p.y = p.y - moveSpeed * dt;
				if(p.y < -768f)
				{
					p.y += 3072f;
				}
				ChonglouSprite[i].transform.localPosition = p;
			}
			return;
		}
		
		if (transform.localPosition.y <= -1240f) {
			return;
		}
		Vector3 pos = transform.localPosition;
		pos.y = pos.y - moveSpeed * dt;
		gameObject.transform.localPosition = pos;
	}

}
