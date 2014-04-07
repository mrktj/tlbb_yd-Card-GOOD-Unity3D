using UnityEngine;
using System.Collections;

namespace Games.Battle
{
    public class CardStateInBattle : CardStateBase
    {
        public override CardStateType GetStateType()
        {
            return CardStateType.E_CARD_STATE_IN_BATTLE;
        }

        public override void OnEnter()
        {
            m_BattleCard.Battle_Slot.SetHpBarVisible(true);
        }

        public override void OnLeave()
        {

        }

        public override void Update()
        {

        }
    }
}

