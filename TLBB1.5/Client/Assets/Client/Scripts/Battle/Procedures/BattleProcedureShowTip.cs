using UnityEngine;
using System.Collections;
using Games.CharacterLogic;

namespace Games.Battle
{
    public class BattleProcedureShowTip : BattleProcedureBase
    {
        private BattleProcedureManager m_Manager;

        private float m_DeltaTime = 0f;
        private float m_TotalTime = 2f;

        private GameObject m_RoundTip_PVP = null;
        private GameObject m_RoundTip_1 = null;
        private GameObject m_RoundTip_2 = null;
        private GameObject m_RoundTip_3 = null;

        public override BattleProcedureType GetProcedureType()
        {
            return BattleProcedureType.E_BATTLE_PROCEDURE_SHOW_TIP;
        }

        public override bool Init(BattleProcedureManager manager)
        {
            m_Manager = manager;
            
            m_RoundTip_PVP = BattleUI.Instacne.roundTip_PVP;
            m_RoundTip_1 = BattleUI.Instacne.roundTip_1;
            m_RoundTip_2 = BattleUI.Instacne.roundTip_2;
            m_RoundTip_3 = BattleUI.Instacne.roundTip_3;

            return true;
        }

        public override void OnEnter()
        {
			m_DeltaTime = 0f;
			m_TotalTime = 2f;
            AudioManager.Instance.PlayEffectSound(SoundResource.SoundRes.battle_phase.ToString(), Obj_MyselfPlayer.GetMe().acceleration);
			BattleType battleType = Games.CharacterLogic.Obj_MyselfPlayer.GetMe().battleType;
			switch(battleType)
			{
			case BattleType.PVE:
				switch(m_Manager.GetBattlePlayer().RoundCounter)
				{
				case 1:
					m_RoundTip_1.SetActive(true);
                	m_RoundTip_1.GetComponent<Animation>().Play();
					break;
				case 2:
					m_RoundTip_2.SetActive(true);
                	m_RoundTip_2.GetComponent<Animation>().Play();
					break;
				case 3:
					m_RoundTip_3.SetActive(true);
                	m_RoundTip_3.GetComponent<Animation>().Play();
					break;
				}
				break;
			case BattleType.PVP:
			case BattleType.QxzbPvP:
				m_RoundTip_PVP.SetActive(true);
                m_RoundTip_PVP.GetComponent<Animation>().Play();
				break;
			case BattleType.WORLD_BOSS:
				//是世界boss开始战斗动画
				m_RoundTip_PVP.SetActive(true);
                m_RoundTip_PVP.GetComponent<Animation>().Play();
				break;
			case BattleType.CHONG_LOU:
				m_TotalTime = 0.1f;
				break;
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
                m_Manager.ChangProcedure(BattleProcedureType.E_BATTLE_PROCEDURE_BATTLING);
            }
        }
    }
}
