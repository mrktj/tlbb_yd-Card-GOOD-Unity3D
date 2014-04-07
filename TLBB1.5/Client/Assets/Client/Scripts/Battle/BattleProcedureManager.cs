using UnityEngine;
using System.Collections.Generic;

namespace Games.Battle
{
    public sealed class BattleProcedureManager
    {
        private BattlePlayer m_BattlePlayer = null;
        private BattleProcedureBase m_CurrentProcedure = null;
        private IDictionary<BattleProcedureType, BattleProcedureBase> m_ProcedureList = new Dictionary<BattleProcedureType, BattleProcedureBase>();
        //public IDictionary<BattleProcedureType, BattleProcedureBase> ProcedureList { get { return m_ProcedureList; } }

        public BattleProcedureManager(BattlePlayer player)
        {
            m_BattlePlayer = player;
            m_ProcedureList[BattleProcedureType.E_BATTLE_PROCEDURE_RESOURCE_LOAD] = new BattleProcedureResourceLoad();//加载图片资源//
            m_ProcedureList[BattleProcedureType.E_BATTLE_PROCEDURE_BATTLE_START] = new BattleProcedureBattleStart();
            m_ProcedureList[BattleProcedureType.E_BATTLE_PROCEDURE_CARD_PREPARE] = new BattleProcedureCardPrepare();
            m_ProcedureList[BattleProcedureType.E_BATTLE_PROCEDURE_CARD_FORWARD] = new BattleProcedureCardForward();
            m_ProcedureList[BattleProcedureType.E_BATTLE_PROCEDURE_CARD_MEET] = new BattleProcedureCardMeet();
            m_ProcedureList[BattleProcedureType.E_BATTLE_PROCEDURE_SHOW_TIP] = new BattleProcedureShowTip();
            m_ProcedureList[BattleProcedureType.E_BATTLE_PROCEDURE_BATTLING] = new BattleProcedureBattling();
            m_ProcedureList[BattleProcedureType.E_BATTLE_PROCEDURE_SHOW_TROPHY] = new BattleProcedureShowTrophy();
            m_ProcedureList[BattleProcedureType.E_BATTLE_PROCEDURE_WAITING] = new BattleProcedureWaiting();
            m_ProcedureList[BattleProcedureType.E_BATTLE_PROCEDURE_SHOW_RESULT] = new BattleProcedureShowResult();
			m_ProcedureList[BattleProcedureType.E_BATTLE_PROCEDURE_REVIVE_FREE] = new BattleProcedureReviveFree();
            m_ProcedureList[BattleProcedureType.E_BATTLE_PROCEDURE_BATTLE_END] = new BattleProcedureBattleEnd();
        }

        public bool Init()
        {
            foreach (KeyValuePair<BattleProcedureType, BattleProcedureBase> procedure in m_ProcedureList)
            {
                if (!procedure.Value.Init(this))
                {
                    return false;
                }
            }
            return true;
        }

        public void ChangProcedure(BattleProcedureType type)
        {
            if (m_CurrentProcedure.GetProcedureType() != type)
            {
                m_CurrentProcedure.OnLeave();
                m_CurrentProcedure = m_ProcedureList[type];
                m_CurrentProcedure.OnEnter();
            }
        }

        public BattleProcedureBase GetActiveProcedure()
        {
            return this.m_CurrentProcedure;
        }

        public void Update()
        {
            /*
            if (m_CurrentProcedure == null)
            {
                m_CurrentProcedure = m_ProcedureList[BattleProcedureType.E_BATTLE_PROCEDURE_BATTLE_START];
                m_CurrentProcedure.OnEnter();
            }*/
            if (m_CurrentProcedure == null)
            {
                m_CurrentProcedure = m_ProcedureList[BattleProcedureType.E_BATTLE_PROCEDURE_RESOURCE_LOAD];
                m_CurrentProcedure.OnEnter();
            }
            m_CurrentProcedure.Update();

            
        }

        public BattlePlayer GetBattlePlayer()
        {
            return m_BattlePlayer;
        }
		
		public BattleProcedureType GetBattleProcedureType()
		{
			BattleProcedureType type = BattleProcedureType.E_BATTLE_PROCEDURE_BATTLE_START;
			
			foreach(KeyValuePair<BattleProcedureType, BattleProcedureBase> procedure in m_ProcedureList)
			{
				if(procedure.Value == m_CurrentProcedure)
				{
					type = procedure.Key;
					break;
				}
			}
			return type;
		}

        public int GetTurnCounter() 
        {
            BattleProcedureBattling procedure = m_ProcedureList[BattleProcedureType.E_BATTLE_PROCEDURE_BATTLING] as BattleProcedureBattling;
            return procedure.TurnCounter; 
        }

    }
}
