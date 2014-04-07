using UnityEngine;
using System.Collections;
using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;
using Games.Battle;

public class CopyPromptUI : MonoBehaviour {

    public UILabel headerName;
    public UILabel copyText;
    public UITexture headerTex;
    private BattlePlayer mBattlePlayer;
    private bool mBefore = false;

	// Use this for initialization
	void Start () 
    {
        mBattlePlayer = (GameObject.FindObjectOfType(typeof(BattleLogic)) as BattleLogic).GetBattleCore().GetBattlePlayer();

	}
	

    public void Show(int copy_id, bool before, int header_id)
    {
		gameObject.SetActive(true);
        mBefore = before;
        Tab_Card tab_card = TableManager.GetCardByID(header_id);
        if (tab_card != null)
        {
            AtlasManager.Instance.SetBodyName(headerTex, TableManager.GetAppearanceByID(tab_card.Appearance).BodyIcon);
            headerName.text = LanguageManger.GetWords(TableManager.GetAppearanceByID(tab_card.Appearance).Name);
        }

        Tab_Copydetail tab_detail = TableManager.GetCopydetailByID(copy_id);
        if (before)
        {
            if (tab_detail != null)
            {
                copyText.text = LanguageManger.GetWords(tab_detail.FontText);
            }
            string key = "copy_front_" + Obj_MyselfPlayer.GetMe().accountID + "_" + Obj_MyselfPlayer.GetMe().curSubcopy.subCopyID;
            PlayerPrefs.SetInt(key, 1);
            
        }
        else
        {
            if (tab_detail != null)
            {
                copyText.text = LanguageManger.GetWords(tab_detail.BackText);
            }
            string key = "copy_back_" + Obj_MyselfPlayer.GetMe().accountID + "_" + Obj_MyselfPlayer.GetMe().curSubcopy.subCopyID;
            PlayerPrefs.SetInt(key, 1);
            
        }
    }

    public void OnScreenTouched()
    {
        gameObject.SetActive(false);
        if (mBefore)
        {
            EventManager.Instance.Fire(EventDefine.UI_COPY_START_PROMPT_UI_TOUCHED);
        }
        else
        {
            EventManager.Instance.Fire(EventDefine.UI_COPY_END_PROMPT_UI_TOUCHED);
        }
    }
}
