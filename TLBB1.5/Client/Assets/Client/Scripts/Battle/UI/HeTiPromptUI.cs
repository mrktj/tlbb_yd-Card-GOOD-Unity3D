using UnityEngine;
using System.Collections;
using GCGame.Table;
using Games.LogicObject;
using Games.CharacterLogic;
using Games.Battle;

public class HeTiPromptUI : MonoBehaviour {
	
	public UILabel hetiName;
	public UILabel hetiText;
	public UISprite leftName;
	public UITexture leftPerson;
    public UISprite rightName;
	public UITexture rightPerson;
    public tk2dAnimatedSprite wengziBg;
    public GameObject wengziPos;

    private BattlePlayer mBattlePlayer;
    private int copyID;
    private Tab_Copydetail tblCopyDetail;
    private GameObject mWengzi;

	// Use this for initialization
	void Start () {
        mBattlePlayer = (GameObject.FindObjectOfType(typeof(BattleLogic)) as BattleLogic).GetBattleCore().GetBattlePlayer();

	}
	
    public void Show(int copy_id)
    {
        gameObject.SetActive(true);
        copyID = copy_id;
        tblCopyDetail = TableManager.GetCopydetailByID(copy_id);

        Tab_Card tbl_card = TableManager.GetCardByID(tblCopyDetail.NoticeLeft);
        Tab_Appearance tbl_appear = null;
        if (tbl_card != null)
        {
            tbl_appear = TableManager.GetAppearanceByID(tbl_card.Appearance);
            leftName.spriteName = tbl_appear.ImgName;
            AtlasManager.Instance.SetBodyName(leftPerson, tbl_appear.BodyIcon);
        }
        if (tbl_card != null && TableManager.GetSkillByID(tbl_card.SkillComb) != null)
        {
            int dis_id = TableManager.GetSkillByID(tbl_card.SkillComb).Effect;
            hetiName.text = LanguageManger.GetWords(TableManager.GetSkillDisplayByID(dis_id).Name);
        	hetiText.text = LanguageManger.GetWords(tblCopyDetail.NoticeText);
			
			string wengzi_anim = TableManager.GetSkillDisplayByID(TableManager.GetSkillByID(tbl_card.SkillComb).Effect).AttackAnim;

            GameObject go = Resources.Load("HetiWenZi/" + wengzi_anim, typeof(GameObject)) as GameObject;
            mWengzi = GameObject.Instantiate(go) as GameObject;
            mWengzi.transform.parent = wengziPos.transform.parent;
            mWengzi.transform.localPosition = wengziPos.transform.localPosition;
            mWengzi.transform.localScale = Vector3.one;
            mWengzi.GetComponent<Animation>().enabled = false;
        }
		else
		{
			Debug.LogError("HeTiPromptUI.Show(), card has no heti skill, card id = " + tblCopyDetail.NoticeLeft);
		}
		
        tbl_card = TableManager.GetCardByID(tblCopyDetail.NoticeRight);
        if (tbl_card != null)
        {
            tbl_appear = TableManager.GetAppearanceByID(tbl_card.Appearance);
            rightName.spriteName = tbl_appear.ImgName;
            AtlasManager.Instance.SetBodyName(rightPerson, tbl_appear.BodyIcon);
        }
    }
    public void OnHeTiPromptUIPressed()
    {
        gameObject.SetActive(false);
        EventManager.Instance.Fire(EventDefine.UI_HE_TI_PROMPT_UI_TOUCHED);
    }
}
