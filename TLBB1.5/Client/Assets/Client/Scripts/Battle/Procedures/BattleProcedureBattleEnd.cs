using UnityEngine;
using System.Collections;
using Games.CharacterLogic;
using GCGame.Table;
using card.net;

namespace Games.Battle
{
    public class BattleProcedureBattleEnd : BattleProcedureBase
    {
        private BattleProcedureManager m_Manager;
        private float m_DeltaTime = 0f;
        private float m_TotalTime = float.MaxValue;
        private GameObject m_CopyPromptWidget = null;

        public BattleProcedureBattleEnd()
        {
            EventManager.Instance.RegisterEventHandler(EventDefine.UI_COPY_END_PROMPT_UI_TOUCHED, OnCopyPromptUITouched);
        }

        public override BattleProcedureType GetProcedureType()
        {
            return BattleProcedureType.E_BATTLE_PROCEDURE_BATTLE_END;
        }

        public override bool Init(BattleProcedureManager manager)
        {
            m_Manager = manager;
            return true;
        }
        public override void OnEnter()
        {
            m_DeltaTime = 0f;

            //for guide
            if (Obj_MyselfPlayer.GetMe().isLastBattleNotFinish && !GuideManager.Instance.isEnd())
                Obj_MyselfPlayer.GetMe().isLastBattleNotFinish = false;
            //end


            if (Obj_MyselfPlayer.GetMe().isGuideBattle)
            {
                Obj_MyselfPlayer.GetMe().battleData.isPlayed = false;
                Obj_MyselfPlayer.GetMe().isGuideBattle = false;
                GuideManager.Instance.FinishedStep(GuideManager.GUIDE_STEP.FIRST_COPY);
            	Obj_MyselfPlayer.GetMe().OnBattleEnd();
				//LoadingSliderKit.AsycLoadScene(Utils.UI_NAME_main);
                GameManager.Instance.LoadLevel(Utils.UI_NAME_Login);
            }
            else
			{
				switch(Obj_MyselfPlayer.GetMe().battleType)
				{
				
				case BattleType.PVE:
					if (m_Manager.GetBattlePlayer().GetBattleRoundData().isWin)
	                {
	                    //副本后提示
	                    string key = "copy_back_" + Obj_MyselfPlayer.GetMe().accountID + "_" + Obj_MyselfPlayer.GetMe().curSubcopy.subCopyID;
	                    int is_shown = PlayerPrefs.GetInt(key, 0);
	                    int copy_text = TableManager.GetCopydetailByID(Obj_MyselfPlayer.GetMe().curSubcopy.subCopyID).BackText;
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
	
	                    if (m_CopyPromptWidget != null)
	                    {
	                        TroopMember trmbr = m_Manager.GetBattlePlayer().GetBattleRoundData().FindCardByGUID(Obj_MyselfPlayer.GetMe().teamMemberArray[0]);
	                        m_CopyPromptWidget.GetComponent<CopyPromptUI>().Show(Obj_MyselfPlayer.GetMe().curSubcopy.subCopyID, false, trmbr.cardID);
	
	                        m_TotalTime = 10f;
	                    }
	                    else
	                    {
	                        NetworkSender.Instance().ClearBattleData(OnClearDataRet, Obj_MyselfPlayer.GetMe().curSubcopy.subCopyID);
	                    }
	                }
	                else
	                {
	                    NetworkSender.Instance().ClearBattleData(OnClearDataRet, Obj_MyselfPlayer.GetMe().curSubcopy.subCopyID);
	                }
					break;
				case BattleType.PVP:
				case BattleType.QxzbPvP:
					Obj_MyselfPlayer.GetMe().OnBattleEnd();
					//LoadingSliderKit.AsycLoadScene(Utils.UI_NAME_main);
                	GameManager.Instance.LoadLevel(Utils.UI_NAME_main);
					break;
				case BattleType.WORLD_BOSS:
					Obj_MyselfPlayer.GetMe().SetUserBaseData(Obj_MyselfPlayer.GetMe().battleData.pbBaseData);
					NetworkSender.Instance().RequestWorldBossInfo(OnClearDataRet);
					break;
				case BattleType.CHONG_LOU:
					NetworkSender.Instance().ClearPataBattleData(OnClearDataRet);
					break;
				
				}
			}
        }

        public override void OnLeave()
        {

        }

        public override void Update()
        {
            m_DeltaTime += Time.deltaTime;
            if (m_DeltaTime >= m_TotalTime)
            {
                m_TotalTime = float.MaxValue;
                m_CopyPromptWidget.SetActive(false); 
                NetworkSender.Instance().ClearBattleData(OnClearDataRet, Obj_MyselfPlayer.GetMe().curSubcopy.subCopyID);
            }
        }

        public void OnClearDataRet(bool isSucceed)
        {
			Obj_MyselfPlayer.GetMe().OnBattleEnd();
            //更新Playerdata
            //这里的pbBaseData在Receive clear battle data里面已经刷了
            //		if(battleData.pbBaseData != null)
            //			Obj_MyselfPlayer.GetMe().SetUserBaseData(battleData.pbBaseData);
            if (m_Manager.GetBattlePlayer().GetBattleRoundData().pbBagData != null && Obj_MyselfPlayer.GetMe().battleType != BattleType.WORLD_BOSS)
                Obj_MyselfPlayer.GetMe().SetUserBagData(m_Manager.GetBattlePlayer().GetBattleRoundData().pbBagData);
			//这里的也刷过了
//            if (m_Manager.GetBattlePlayer().GetBattleRoundData().pbCopyData != null)
//                Obj_MyselfPlayer.GetMe().SetUserCopyData(m_Manager.GetBattlePlayer().GetBattleRoundData().pbCopyData);
			
			Obj_MyselfPlayer.GetMe().isSuspendinpata = true;
			//LoadingSliderKit.AsycLoadScene(Utils.UI_NAME_main);
            GameManager.Instance.LoadLevel(Utils.UI_NAME_main);
        }


        private void OnCopyPromptUITouched(EventDefine type, System.Object[] args)
        {
            if (m_Manager.GetActiveProcedure().GetProcedureType() != BattleProcedureType.E_BATTLE_PROCEDURE_BATTLE_END)
            {
                return;
            }

            m_DeltaTime = 0f;
            m_TotalTime = 20f;
            NetworkSender.Instance().ClearBattleData(OnClearDataRet, Obj_MyselfPlayer.GetMe().curSubcopy.subCopyID);
        }
    }
}

