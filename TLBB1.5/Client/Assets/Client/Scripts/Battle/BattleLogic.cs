using UnityEngine;
using System.Collections;
using Games.Battle;

public class BattleLogic : MonoBehaviour {

    private BattleCore m_BattleCore;
	public BattleCore GetBattleCore(){return m_BattleCore;}
	private float m_TestTime = 0f;
	
    void Awake()
    {
        Debug.LogWarning("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! BattleLogic.Awake()!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }

	// Use this for initialization
	void Start () {
        m_BattleCore = new BattleCore();
		GameObject go = GameObject.Find("AccountManager");
        if(go != null)
        {
            GameObject.Destroy(go);
        }
	}
	
	// Update is called once per frame
	void Update () {

        m_BattleCore.Update();
	}
	
	void OnReturnMainUI()
	{
		GameManager.Instance.LoadLevel(Utils.UI_NAME_main);
	}

    void OnDestroy()
    {
        m_BattleCore.OnDestroy();
        Debug.LogWarning("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! BattleLogic.OnDestroy()!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }
	
}
