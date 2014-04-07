using UnityEngine;
using System.Collections;

namespace Games.Battle
{
    public enum CardStateType
    {
        E_CARD_STATE_PREPARE,
        E_CARD_STATE_FORWARD,
        E_CARD_STATE_NORMAL,
        E_CARD_STATE_IN_BATTLE,
        E_CARD_STATE_ATTACK,
        E_CARD_STATE_BEHIT,
        E_CARD_STATE_WAITING,
        E_CARD_STATE_DEAD,
    }

    public class CardStateBase
    {
        public CardStateBase()
        {

        }

        protected CardStateMachine m_StateMachine = null;
        protected BattleCard m_BattleCard = null;

        public virtual CardStateType GetStateType(){ return CardStateType.E_CARD_STATE_PREPARE; }
        public virtual bool Init(CardStateMachine machine)
        {
            m_StateMachine = machine;
            m_BattleCard = machine.Owner;

            return true;
        }
        public virtual void OnEnter(){}
        public virtual void OnLeave(){}
        public virtual void Update(){}
    }
}
