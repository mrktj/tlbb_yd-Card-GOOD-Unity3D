using UnityEngine;
using System.Collections;

namespace Games.Battle
{
    public class CardStateForward : CardStateBase
    {
        public CardStateForward()
        {
            EventManager.Instance.RegisterEventHandler(EventDefine.BATTLE_CARD_START_FLY, OnCardStartFly);
            EventManager.Instance.RegisterEventHandler(EventDefine.BATTLE_CARD_STOP_FLY, OnCardStopFly);
            EventManager.Instance.RegisterEventHandler(EventDefine.BATTLE_CARD_STOP_FORWARD, OnCardStopForward);
        }
        public override CardStateType GetStateType()
        {
            return CardStateType.E_CARD_STATE_FORWARD;
        }

        public override void OnEnter()
        {
            m_BattleCard.CardUI.tornado.SetActive(true);
            m_BattleCard.CardUI.gameObject.GetComponent<Animation>().Play("AliveCardForward");
        }

        public override void OnLeave()
        {

        }

        public override void Update()
        {

        }

        private void OnCardStartFly(EventDefine type, System.Object[] args)
        {
            if (args.Length < 0 || args[0] != m_BattleCard.BattleCardObj)
            {
                return;
            }

            m_BattleCard.CardUI.tornado.gameObject.SetActive(false);
            m_BattleCard.CardUI.tail.gameObject.SetActive(true);
            m_BattleCard.CardUI.smoke.gameObject.SetActive(false);

            BattleUI.Instacne.battleBg.MoveFast();
        }

        private void OnCardStopFly(EventDefine type, System.Object[] args)
        {
            if (args.Length < 0 || args[0] != m_BattleCard.BattleCardObj)
            {
                return;
            }

            m_BattleCard.CardUI.tornado.gameObject.SetActive(true);
            m_BattleCard.CardUI.tail.gameObject.SetActive(false);
            m_BattleCard.CardUI.smoke.gameObject.SetActive(true);

            BattleUI.Instacne.battleBg.MoveSlow();
        }

        private void OnCardStopForward(EventDefine type, System.Object[] args)
        {
            if (args.Length < 0 || args[0] != m_BattleCard.BattleCardObj)
            {
                return;
            }

            m_BattleCard.CardUI.tornado.gameObject.SetActive(false);
            m_BattleCard.CardUI.tail.gameObject.SetActive(false);
            //m_BattleCard.CardUI.smoke.gameObject.SetActive(true);

            BattleUI.Instacne.battleBg.Stop();
        }
    }
}

