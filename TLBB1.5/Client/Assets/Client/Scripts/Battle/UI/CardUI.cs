using UnityEngine;
using System.Collections;
using GCGame.Table;
using Games.LogicObject;
using Games.CharacterLogic;
using Games.Battle;

public class CardUI : MonoBehaviour {

    public enum TweenState
    {
        NONE,
        SHOW,
        DISAPPEAR,
    }
	public GameObject cardMainRoot;
	public Transform buffIconRoot;
    public UITexture cardIcon;
    public UISprite cardFrame;
    public UISprite category;
    public UISprite cardBorder;
    public UISprite cardNameBg;
    public UILabel cardName;
    public GameObject skeleton;
    public GameObject tomb;
    public GameObject angel;
    public GameObject angelBird;
    public GameObject angelWord;
    public GameObject cardBox;
    public GameObject itemBox;
    public GameObject dropMoney;
    public GameObject tornado;
    public GameObject tail;
    public GameObject smoke;

    public GameObject[] m_BuffPosArray;

    private TweenState mTweenState = TweenState.NONE;

    private BattleCard mOwner;
    private GameObject mBox;
    private BattlePlayer mBattlePlayer;

    private bool m_InReviveAnimation = false;
    public bool InReviveAnimation { get { return m_InReviveAnimation; } }

    public BattleCard Owner
    {
        set
        {
            mOwner = value;
        }
    }

    private static CardUI m_Instance;
    public static CardUI Instance { get{ return m_Instance; } }

    void Awake()
    {
        m_Instance = this;
    }

	// Use this for initialization
	void Start () {
        cardIcon.gameObject.SetActive(true);
        cardFrame.gameObject.SetActive(true);
        category.gameObject.SetActive(true);
        skeleton.SetActive(true);
        tomb.SetActive(true);
        angel.SetActive(true);
        angelBird.SetActive(true);
        angelWord.SetActive(true);
        tornado.SetActive(true); 
        tail.SetActive(true); 
        smoke.SetActive(true); 

        skeleton.gameObject.SetActive(false);
        tomb.gameObject.SetActive(false);
        angel.SetActive(false);
        tornado.SetActive(false);
        tail.SetActive(false); 
        smoke.SetActive(false); 

        UIEventListener.Get(gameObject).onClick += OnCardClicked;
	}
	

    public void Show()
    {
        mTweenState = TweenState.SHOW;

        TweenAlpha alpha = cardIcon.GetComponent<TweenAlpha>();
        ChangeAlpha(alpha, 0f, 1f, 1f);
		alpha.onFinished = null;
		
        alpha = cardFrame.GetComponent<TweenAlpha>();
        ChangeAlpha(alpha, 0f, 1f, 1f);

        alpha = category.GetComponent<TweenAlpha>();
        ChangeAlpha(alpha, 0f, 1f, 1f);

        alpha = category.GetComponent<TweenAlpha>();
        ChangeAlpha(alpha, 0f, 1f, 1f);

        alpha = cardBorder.GetComponent<TweenAlpha>();
        ChangeAlpha(alpha, 0f, 1f, 1f);

        alpha = cardNameBg.GetComponent<TweenAlpha>();
        ChangeAlpha(alpha, 0f, 1f, 1f);

        alpha = cardName.GetComponent<TweenAlpha>();
        ChangeAlpha(alpha, 0f, 1f, 1f);
    }

    public void Disappear()
    {
        mTweenState = TweenState.DISAPPEAR;

        TweenAlpha alpha = cardIcon.GetComponent<TweenAlpha>();
        ChangeAlpha(alpha, 1f, 0f, 1f);
		alpha.onFinished = OnCardIconDisappeared;
			
        alpha = cardFrame.GetComponent<TweenAlpha>();
        ChangeAlpha(alpha, 1f, 0f, 1f);

        alpha = category.GetComponent<TweenAlpha>();
        ChangeAlpha(alpha, 1f, 0f, 1f);

        alpha = category.GetComponent<TweenAlpha>();
        ChangeAlpha(alpha, 1f, 0f, 1f);

        alpha = cardBorder.GetComponent<TweenAlpha>();
        ChangeAlpha(alpha, 1f, 0f, 1f);

        alpha = cardNameBg.GetComponent<TweenAlpha>();
        ChangeAlpha(alpha, 1f, 0f, 1f);

        alpha = cardName.GetComponent<TweenAlpha>();
        ChangeAlpha(alpha, 1f, 0f, 1f);

        ShowSkeleton();
    }

    private void ChangeAlpha(TweenAlpha alpha, float from, float to, float duration)
    {
        alpha.enabled = true;
        alpha.Reset();
        alpha.from = from;
        alpha.to = to;
        alpha.duration = duration;
    }

    public void ShowSkeleton()
    {
        skeleton.gameObject.SetActive(true);

        TweenPosition tween_pos = skeleton.GetComponent<TweenPosition>();
        tween_pos.enabled = true;
        tween_pos.Reset();
        TweenAlpha tween_alpha = skeleton.GetComponent<TweenAlpha>();
        tween_alpha.enabled = true;
        tween_alpha.Reset();
    }
	
    public void OnCardIconDisappeared(UITweener tween)
    {
        skeleton.gameObject.SetActive(false);
        if (mOwner.Card_Type == BattleCardType.E_BATTLE_CARD_TYPE_SELF)
        {
            ShowTomb();
        }
        else
        {
            ShowBox();
        }
    }
	public void OnDead()
	{
		Disappear();
	}
    public void ShowTomb()
    {
        angel.SetActive(false);
        tomb.gameObject.SetActive(true);
    }

    public void ShowBox()
    {
        if (mOwner.Card_Data.MemberData.bag == null)
        {
            return;
        }

        if (mOwner.Card_Data.MemberData.bag.type == DropType.CARD)
        {
            mBox = GameObject.Instantiate(cardBox) as GameObject;
            mBox.transform.parent = gameObject.transform.parent;
            mBox.transform.localPosition = gameObject.transform.localPosition;
            mBox.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            mBox.GetComponent<Animation>().Play();

            AudioManager.Instance.PlayEffectSound(SoundResource.SoundRes.battle_itemdrop.ToString(), false, 0f, Obj_MyselfPlayer.GetMe().acceleration);
        }
        else if (mOwner.Card_Data.MemberData.bag.type == DropType.TIEM)
        {
            mBox = GameObject.Instantiate(itemBox) as GameObject;
            mBox.transform.parent = gameObject.transform.parent;
            mBox.transform.localPosition = gameObject.transform.localPosition;
            mBox.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            mBox.GetComponent<Animation>().Play();

            AudioManager.Instance.PlayEffectSound(SoundResource.SoundRes.battle_itemdrop.ToString(), false, 0f, Obj_MyselfPlayer.GetMe().acceleration);
        }
        else if (mOwner.Card_Data.MemberData.bag.type == DropType.MONEY)
        {
            mBox = GameObject.Instantiate(dropMoney) as GameObject;
            mBox.transform.parent = gameObject.transform.parent;
            mBox.transform.localPosition = gameObject.transform.localPosition;
            mBox.transform.localScale = Vector3.one;
            mBox.GetComponent<Animation>().Play();
            mBox.GetComponent<DropMoney>().Money = mOwner.Card_Data.MemberData.bag.val;

            AudioManager.Instance.PlayEffectSound(SoundResource.SoundRes.battle_moneydrop.ToString(), false, 0f, Obj_MyselfPlayer.GetMe().acceleration);
        }
    }
	
    private void ShowAngle()
    {
        angel.SetActive(true);
        tomb.gameObject.SetActive(false);
        Show();
    }

    public void SetCardTemplateID(int id)
    {
        if (TableManager.GetCardByID(id) == null || TableManager.GetAppearanceByID(TableManager.GetCardByID(id).Appearance) == null)
        {
            Debug.LogError("SetCardTemplateID(), no such template id: " + id);
            return;
        }

        AtlasManager.Instance.setBodyByTempletID(cardIcon, id);
        cardFrame.spriteName = UserCardItem.cardFrameName[TableManager.GetCardByID(id).Star];
        cardFrame.MakePixelPerfect();

        category.spriteName = UserCardItem.elementTypeName[TableManager.GetCardByID(id).Element];
        category.MakePixelPerfect();

        cardBorder.spriteName = UserCardItem.largeCardBorderName[TableManager.GetCardByID(id).Star];
        cardBorder.MakePixelPerfect();

        cardNameBg.spriteName = UserCardItem.largeCardNameBg[TableManager.GetCardByID(id).Star];
        cardNameBg.MakePixelPerfect();

        cardName.text = LanguageManger.GetWords(TableManager.GetAppearanceByID(TableManager.GetCardByID(id).Appearance).Name);
    }

    public void OnCardClicked(GameObject go)
    {
        if (gameObject.GetComponent<DraggableCard>().isDraged)
        {
            gameObject.GetComponent<DraggableCard>().isDraged = false;
            return;
        }

        if (Obj_MyselfPlayer.GetMe().isAutoFowrard)
        {
            return;
        }

        if (m_InReviveAnimation)
        {
            return;
        }

        EventManager.Instance.Fire(EventDefine.BATTLE_CARD_CLICKED, gameObject);
    }

    public void ShowReviveAnimation()
    {
        tk2dAnimatedSprite pfb = Resources.Load("Widgets/CardRevive", typeof(tk2dAnimatedSprite)) as tk2dAnimatedSprite;
        tk2dAnimatedSprite sprite = GameObject.Instantiate(pfb) as tk2dAnimatedSprite;
        if (sprite == null)
        {
            Debug.LogError("Can't find Widgets/CardRevive");
            return;
        }
        sprite.transform.parent = mOwner.BattleCardObj.gameObject.transform.parent;
        sprite.transform.localPosition = mOwner.BattleCardObj.transform.localPosition + new Vector3(0f, 20f, -100f);
        sprite.transform.localScale = Vector3.one;

        sprite.animationCompleteDelegate += OnReviveAnimationComplete;
        sprite.Play();
        m_InReviveAnimation = true;
        UIEventListener.Get(BattleUI.Instacne.forwardSprite.gameObject).onClick = null;
        pfb = null;

        //播放攻击音效
        AudioManager.Instance.PlayEffectSound(SoundResource.SoundRes.battle_revive.ToString(), false, 0f, Obj_MyselfPlayer.GetMe().acceleration);
    }

    void OnReviveAnimationComplete(tk2dAnimatedSprite sprite, int clipId)
    {
        UIEventListener.Get(BattleUI.Instacne.forwardSprite.gameObject).onClick = BattleUI.Instacne.OnForwardBtn;
        m_InReviveAnimation = false;
        mOwner.OnRevive();
        mOwner.CardUI.angel.SetActive(false);
        GameObject.Destroy(sprite.gameObject);
        Resources.UnloadUnusedAssets();
    }
}
