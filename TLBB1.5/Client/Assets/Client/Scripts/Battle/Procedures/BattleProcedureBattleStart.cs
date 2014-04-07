using UnityEngine;
using System.Collections;
using Games.CharacterLogic;
using GCGame.Table;

namespace Games.Battle
{
    public class BattleProcedureBattleStart : BattleProcedureBase
    {
        private BattleProcedureManager m_Manager;

        private GameObject m_HetiPromptWidget = null;
        private GameObject m_CopyPromptWidget = null;
        private float m_DeltaTime = 0f;
        private float m_TotalTime = 1f;

        public BattleProcedureBattleStart()
        {
            EventManager.Instance.RegisterEventHandler(EventDefine.UI_HE_TI_PROMPT_UI_TOUCHED, OnHetiPromptUITouched);
            EventManager.Instance.RegisterEventHandler(EventDefine.UI_COPY_START_PROMPT_UI_TOUCHED, OnCopyPromptUITouched);
        }

        public override BattleProcedureType GetProcedureType()
        {
            return BattleProcedureType.E_BATTLE_PROCEDURE_BATTLE_START;
        }

        public override bool Init(BattleProcedureManager manager)
        {
            m_Manager = manager;
            return true;
        }
        public override void OnEnter()
        {
            m_DeltaTime = 0f;
			//增加的世界boss 和 重楼的处理在哪里？
            if (Obj_MyselfPlayer.GetMe().battleType == BattleType.PVE && !Obj_MyselfPlayer.GetMe().isGuideBattle)
            {
                //合全技提示
                string key = "heti_" + Obj_MyselfPlayer.GetMe().accountID + "_" + Obj_MyselfPlayer.GetMe().curSubcopy.subCopyID;
                int is_shown = PlayerPrefs.GetInt(key, 0);
                bool heti_notice = TableManager.GetCopydetailByID(Obj_MyselfPlayer.GetMe().curSubcopy.subCopyID).HetiNotice == 1 ? true : false;
                if (is_shown != 1 && heti_notice)
                {
                    GameObject heti_prompt_asset = Resources.Load("Widgets/HeTiPromptUI", typeof(GameObject)) as GameObject;
                    GameObject heti_prompt = GameObject.Instantiate(heti_prompt_asset) as GameObject;
                    GameObject anchor = GameObject.Find("UI Root (2D)/Camera/Anchor/Panel/Anchor/Panel-Top");
                    heti_prompt.transform.parent = anchor.transform;
                    heti_prompt.transform.localPosition = new Vector3(156f, 328f, -20);
                    heti_prompt.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                    heti_prompt.SetActive(false);
                    m_HetiPromptWidget = heti_prompt;
                    PlayerPrefs.SetInt(key, 1);
                    Debug.Log("Load Widgets/HeTiPromptUI");
                }

                //副本前提示
                key = "copy_front_" + Obj_MyselfPlayer.GetMe().accountID + "_" + Obj_MyselfPlayer.GetMe().curSubcopy.subCopyID;
                is_shown = PlayerPrefs.GetInt(key, 0);
                int copy_text = TableManager.GetCopydetailByID(Obj_MyselfPlayer.GetMe().curSubcopy.subCopyID).FontText;
                if (is_shown != 1 && copy_text != -1)
                {
                    GameObject copy_prompt_asset = Resources.Load("Widgets/CopyPromptUI", typeof(GameObject)) as GameObject;
                    GameObject copy_prompt = GameObject.Instantiate(copy_prompt_asset) as GameObject;
                    GameObject anchor = GameObject.Find("UI Root (2D)/Camera/Anchor/Panel/Anchor/Panel-Top");
                    copy_prompt.transform.parent = anchor.transform;
                    copy_prompt.transform.localPosition = new Vector3(267f, -93f, -20f);
                    copy_prompt.transform.localScale = new Vector3(1f, 1f, 1f);
                    copy_prompt.SetActive(false);
                    m_CopyPromptWidget = copy_prompt;
                    Debug.Log("Load Windows/CopyPromptUI");
                }
            }

            //创建卡牌
            BattleCardManager.Instance.CreateCard(m_Manager.GetBattlePlayer().GetBattleRoundData().troopData.selfMembers);
			//世界boss进去时的血量需要特殊初始化
			
        }
        public override void OnLeave()
        {
            if (m_CopyPromptWidget != null)
            {
                GameObject.Destroy(m_CopyPromptWidget);
                m_CopyPromptWidget = null;
            }
            if (m_HetiPromptWidget != null)
            {
                GameObject.Destroy(m_HetiPromptWidget);
                m_HetiPromptWidget = null;
            }
        }
        public override void Update()
        {
            m_DeltaTime += Time.deltaTime;
            if (m_DeltaTime >= m_TotalTime)
            {
                m_Manager.ChangProcedure(BattleProcedureType.E_BATTLE_PROCEDURE_CARD_FORWARD);
            }

            if (m_CopyPromptWidget != null && !m_CopyPromptWidget.activeInHierarchy)
            {
                TroopMember trmbr = m_Manager.GetBattlePlayer().GetBattleRoundData().FindCardByGUID(Obj_MyselfPlayer.GetMe().teamMemberArray[0]);
                m_CopyPromptWidget.GetComponent<CopyPromptUI>().Show(Obj_MyselfPlayer.GetMe().curSubcopy.subCopyID, true, trmbr.cardID);
                m_TotalTime = 10f;
            }

            if (m_CopyPromptWidget == null || !m_CopyPromptWidget.activeSelf)
            {
                if (m_HetiPromptWidget != null && !m_HetiPromptWidget.activeInHierarchy)
                {
                    m_HetiPromptWidget.GetComponent<HeTiPromptUI>().Show(Obj_MyselfPlayer.GetMe().curSubcopy.subCopyID);
                    m_TotalTime = 10f;
                }
            }
        }

        private void OnCopyPromptUITouched(EventDefine type, System.Object[] args)
        {
            GameObject.Destroy(m_CopyPromptWidget);
            m_CopyPromptWidget = null;
            m_TotalTime = 0.1f;
        }

        private void OnHetiPromptUITouched(EventDefine type, System.Object[] args)
        {
            GameObject.Destroy(m_HetiPromptWidget);
            m_HetiPromptWidget = null;
            m_TotalTime = 0.1f;
        }
    }
}
