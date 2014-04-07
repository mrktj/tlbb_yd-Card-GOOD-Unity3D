using UnityEngine;
using System.Collections;

using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;

public class HeTi : MonoBehaviour {
	
	public UITexture[] leftPerson;
	public UITexture[] rightPerson;
	public tk2dAnimatedSprite wengziBg;

    private GameObject wengzi;

	public delegate void HeTiCompleteDelegate(GameObject go);
	public HeTiCompleteDelegate hetiCompleteDelegate;

	private int mCardTempID;
	private int mLoverTempID;
    private GameObject mWengziObj;

	public int CardTempID
	{
		set
		{
			mCardTempID = value;
		}
	}
	public int LoverTempID { set { mLoverTempID = value; } }
    public GameObject WengziObj
	{
		set
		{
            mWengziObj = value;
		}
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	
	public void Play()
	{
		if(TableManager.GetCardByID(mLoverTempID) != null)
		{
            string heti_icon_1 = TableManager.GetAppearanceByID(TableManager.GetCardByID(mLoverTempID).Appearance).BodyIcon;
			if(heti_icon_1 == "-1")
				Debug.LogError("heti_icon_1 = -1");
			for(int i = 0; i < leftPerson.Length; i++)
			{
				AtlasManager.Instance.SetBodyName(leftPerson[i], heti_icon_1);
			}
            
		}
		else
		{
			Debug.LogError("Cant find lover, loverTempID is " + mLoverTempID);
		}
        
        string heti_icon_2 = TableManager.GetAppearanceByID(TableManager.GetCardByID(mCardTempID).Appearance).BodyIcon;
        if (heti_icon_2 == "-1")
            Debug.Log("heti_icon_2 = -1");
		for(int i = 0; i < rightPerson.Length; i++)
		{
			AtlasManager.Instance.SetBodyName(rightPerson[i], heti_icon_2);
		}
		//AtlasManager.Instance.SetBodyName(rightPerson, heti_icon_2);

        wengzi = GameObject.Instantiate(mWengziObj) as GameObject;
        wengzi.transform.parent = transform;
        wengzi.transform.localPosition = new Vector3(0f, 100f, -40f);
        wengzi.transform.localScale = Vector3.one;

        wengzi.gameObject.SetActive(false);
        wengziBg.gameObject.SetActive(false);

		gameObject.GetComponent<Animation>().Play();
	}
	public void ShowWords()
	{
        wengzi.gameObject.SetActive(true);
        wengzi.animation.Play();

        wengziBg.gameObject.SetActive(true);
        wengziBg.animationCompleteDelegate += WengziBgAnimationCompleteHandler;
        wengziBg.Play();
	}
	public void OnHeTiAnimationCompleted()
	{
        Destroy(gameObject, 0.1f);
        if (hetiCompleteDelegate != null)
        {
            hetiCompleteDelegate(gameObject);
        }
	}
	void WengziBgAnimationCompleteHandler(tk2dAnimatedSprite sprite, int clipId)
	{
	}
	
	void OnDestroy()
	{
		
	}
}