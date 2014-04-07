using UnityEngine;
using System.Collections;

namespace Games.Battle
{
    public class CardStatePrepare : CardStateBase
    {
        public override CardStateType GetStateType()
        {
            return CardStateType.E_CARD_STATE_PREPARE;
        }

        public override void OnEnter()
        {
            m_BattleCard.Battle_Slot.SetHpBarVisible(false);
            m_BattleCard.Battle_Slot.SetSlotFrameVisible(false);

            m_BattleCard.BattleCardObj.GetComponent<CardUI>().Show();
        }

        public override void OnLeave()
        {

        }

        public override void Update()
        {

        }
    }
}

