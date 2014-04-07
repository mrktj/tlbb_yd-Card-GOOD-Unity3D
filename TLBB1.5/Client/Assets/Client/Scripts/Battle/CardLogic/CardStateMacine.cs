using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Games.Battle
{
    public class CardStateMachine
    {
        private BattleCard m_Owner = null;
        public BattleCard Owner { set { m_Owner = value; } get { return m_Owner; } }

        private CardStateBase m_CurrentState = null;
        public CardStateBase CurrentState { set { m_CurrentState = value; } get { return m_CurrentState; } }

        private CardStateBase m_PreState = null;
        public CardStateBase PreState { set { m_PreState = value; } get { return m_PreState; } }

        private CardStateBase m_GlobalState = null;
        public CardStateBase GlobalState { set { m_GlobalState = value; } get { return m_GlobalState; } }

        private CardStateBase m_PreGlobalState = null;
        public CardStateBase PreGlobalState { set { m_PreGlobalState = value; } get { return m_PreGlobalState; } }

        private IDictionary<CardStateType, CardStateBase> m_StateList = new Dictionary<CardStateType, CardStateBase>();

        public CardStateMachine(BattleCard owner)
        {
            m_Owner = owner;
            m_StateList[CardStateType.E_CARD_STATE_ATTACK] = new CardStateAttack();
            m_StateList[CardStateType.E_CARD_STATE_BEHIT] = new CardStateBehit();
            m_StateList[CardStateType.E_CARD_STATE_DEAD] = new CardStateDead();
            m_StateList[CardStateType.E_CARD_STATE_FORWARD] = new CardStateForward();
            m_StateList[CardStateType.E_CARD_STATE_IN_BATTLE] = new CardStateInBattle();
            m_StateList[CardStateType.E_CARD_STATE_NORMAL] = new CardStateNormal();
            m_StateList[CardStateType.E_CARD_STATE_PREPARE] = new CardStatePrepare();
            m_StateList[CardStateType.E_CARD_STATE_WAITING] = new CardStateWaiting();
        }
        
        public bool Init()
        {
            foreach (KeyValuePair<CardStateType, CardStateBase> state in m_StateList)
            {
                if (!state.Value.Init(this))
                {
                    Debug.LogError("初始化状态出错");
                    return false;
                }
            }

            return true;
        }

        public void ChangeState(CardStateBase state)
        {
            if (state != null)
            {
                m_PreState = m_CurrentState;
                if (m_CurrentState != null)
                {
                    m_CurrentState.OnLeave();
                }

                m_CurrentState = state;

                m_CurrentState.OnEnter();
            }
        }
        
        public void ChangeState(CardStateType type)
        {
            ChangeState(m_StateList[type]);
        }

        public void RevertState()
        {
            ChangeState(m_PreState);
        }

        public void ChangeGobalState(CardStateBase state)
        {
            if (state != null)
            {
                m_PreGlobalState = m_GlobalState;
                if (m_GlobalState != null)
                {
                    m_GlobalState.OnLeave();
                }

                m_GlobalState = state;

                m_GlobalState.OnEnter();
            }
        }

        public void RevertGlobalState()
        {
            ChangeGobalState(m_PreGlobalState);
        }

        public void Update()
        {
            if (m_CurrentState != null)
            {
                m_CurrentState.Update();
            }

            if (m_GlobalState != null)
            {
                m_GlobalState.Update();
            }
        }

        public bool IsInState(CardStateBase state)
        {
            if (m_CurrentState == state)
            {
                return true;
            }

            return false;
        }
    }
}
