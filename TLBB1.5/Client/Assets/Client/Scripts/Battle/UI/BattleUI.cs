using UnityEngine;
using System.Collections;

public class BattleUI : MonoBehaviour {

    private static BattleUI m_Instance = null;
    public static BattleUI Instacne { get { return m_Instance; } }

    //scene objects
    public Transform battleRoot;
    public GameObject battleCardPrefab;

    //battle slots
    public UISprite[] selfSlotSprites;
    public UILabel[] selfSlotNames;
    public UISprite[] selfSlotBgs;
    public UISlider[] selfSlotHp;

    public UISprite[] otherSlotSprites;
    public UILabel[] otherSlotNames;
    public UISprite[] otherSlotBgs;
    public UISlider[] otherSlotHp;

    public BattleBackground battleBg;
    public UISprite forwardSprite;
    public GameObject greenText;
    public GameObject redText;
    public GameObject battleTopUI;
    //public GameObject comfirmDialog;

    public GameObject roundTip_1;
    public GameObject roundTip_2;
    public GameObject roundTip_3;
    public GameObject roundTip_PVP;
    public GameObject battleResult;
    public GameObject accelerateBtn;
    public GameObject autoForwardBtn;
	public GameObject battleQuitBtn;
	public GameObject ChonglouReword;
	
    void Awake()
    {
        m_Instance = this;
    }

	// Use this for initialization
	void Start () {

        battleResult.transform.localPosition = new Vector3(362.0f, 615.0f, -20.0f);
        roundTip_1.transform.localPosition = new Vector3(384f, 512f, -20f);
        roundTip_2.transform.localPosition = new Vector3(384f, 512f, -20f);
        roundTip_3.transform.localPosition = new Vector3(384f, 512f, -20f);
        roundTip_PVP.transform.localPosition = new Vector3(377f, 495f, -20f);
        forwardSprite.transform.localPosition = new Vector3(384f, 600.0f, -5.0f);

        battleResult.SetActive(false);
        roundTip_1.SetActive(false);
        roundTip_2.SetActive(false);
        roundTip_3.SetActive(false);
        roundTip_PVP.SetActive(false);
        forwardSprite.gameObject.SetActive(false);

        UIEventListener.Get(forwardSprite.gameObject).onClick = OnForwardBtn;
        UIEventListener.Get(accelerateBtn).onClick = OnAcclerateBtn;
        UIEventListener.Get(autoForwardBtn).onClick = OnAutoForwardBtn;
		UIEventListener.Get(battleQuitBtn).onClick = OnBattleQuitBtn;
	}
	

    public void OnForwardBtn(GameObject go)
    {
        EventManager.Instance.Fire(EventDefine.BATTLE_FORWARD_BTN_CLICKED);
    }

    void OnAcclerateBtn(GameObject go)
    {
        EventManager.Instance.Fire(EventDefine.BATTLE_ACCLERATE_BTN_CLIKED);
    }

    void OnAutoForwardBtn(GameObject go)
    {
        EventManager.Instance.Fire(EventDefine.BATTLE_AUTO_FORWARD_BTN_CLIKED);
    }
	
	void OnBattleQuitBtn(GameObject go)
	{
		EventManager.Instance.Fire(EventDefine.BATTLE_QUIT_BTN_CLICK);
	}
	
}
