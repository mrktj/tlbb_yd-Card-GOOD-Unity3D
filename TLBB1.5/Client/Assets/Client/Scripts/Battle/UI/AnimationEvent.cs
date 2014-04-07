using UnityEngine;
using System.Collections;

public class AnimationEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCardStartFly()
    {
        EventManager.Instance.Fire(EventDefine.BATTLE_CARD_START_FLY, gameObject);
    }

    void OnCardStopFly()
    {
        EventManager.Instance.Fire(EventDefine.BATTLE_CARD_STOP_FLY, gameObject);
    }

    void OnCardStopForward()
    {
        EventManager.Instance.Fire(EventDefine.BATTLE_CARD_STOP_FORWARD, gameObject);
    }

    void OnRoundTipComplete_1()
    {
        //gameObject.SetActive(false);
        Destroy(gameObject, 0.1f);
    }
    void OnRoundTipComplete_2()
    {
        Destroy(gameObject, 0.1f);
    }
    void OnRoundTipComplete_3()
    {
        Destroy(gameObject, 0.1f);
    }
    void OnRoundTipComplete_PVP()
    {
        Destroy(gameObject, 0.1f);
    }
}
