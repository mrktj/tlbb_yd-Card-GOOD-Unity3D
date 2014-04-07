using UnityEngine;
using System.Collections;
using card.net;
using Games.CharacterLogic;

namespace Games.Battle
{
    public class BattleProcedureWaiting : BattleProcedureBase
    {
        public BattleProcedureWaiting()
        {
        }
        //private BattleProcedureManager m_Manager;

        private UISprite m_ForwardSprite = null;
        private bool m_IsAutoForwardSend = false;
		private GameObject battleQuitBtn = null;
		
        public override BattleProcedureType GetProcedureType()
        {
            return BattleProcedureType.E_BATTLE_PROCEDURE_WAITING;
        }

        public override bool Init(BattleProcedureManager manager)
        {
           // m_Manager = manager;

            m_ForwardSprite = BattleUI.Instacne.forwardSprite;
			battleQuitBtn = BattleUI.Instacne.battleQuitBtn;
            return true;
        }
        public override void OnEnter()
        {
            m_IsAutoForwardSend = false;
			if(!Obj_MyselfPlayer.GetMe().isAutoFowrard)
			{
				m_ForwardSprite.gameObject.SetActive(true);
				if(Obj_MyselfPlayer.GetMe().battleType == BattleType.CHONG_LOU)
				{
					battleQuitBtn.SetActive(true);
				}
			}
            BattleCardManager.Instance.MakeCardStateWaiting(BattleCardType.E_BATTLE_CARD_TYPE_SELF);
			for(int i = 0; i < BattleUI.Instacne.selfSlotSprites.Length; i++)
			{
				BattleUI.Instacne.selfSlotSprites[i].gameObject.SetActive(true);
			}
        }
        public override void OnLeave()
        {
			for(int i = 0; i < BattleUI.Instacne.selfSlotSprites.Length; i++)
			{
				BattleUI.Instacne.selfSlotSprites[i].gameObject.SetActive(false);
			}
			m_ForwardSprite.gameObject.SetActive(false);
			battleQuitBtn.SetActive(false);
			if(Obj_MyselfPlayer.GetMe().battleType == BattleType.CHONG_LOU && Obj_MyselfPlayer.GetMe().pataClearFlag == 2)
			{
				return;
			}
            BattleCardManager.Instance.StartMoveCard(BattleCardType.E_BATTLE_CARD_TYPE_SELF);
        }
        public override void Update()
        {
			if (Obj_MyselfPlayer.GetMe().isAutoFowrard)
            {
				if(!m_IsAutoForwardSend)
				{
					EventManager.Instance.Fire(EventDefine.BATTLE_FORWARD_BTN_CLICKED);
					m_IsAutoForwardSend = true;
				}
            }
        }
    }
}
