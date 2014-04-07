using UnityEngine;
using System.Collections;
using Games.CharacterLogic;
using card.net;

namespace Games.Battle
{
    public class CardStateWaiting : CardStateBase
    {
        public CardStateWaiting()
        {
            EventManager.Instance.RegisterEventHandler(EventDefine.BATTLE_CARD_CLICKED, OnBattleCardClicked);
        }
        public override CardStateType GetStateType()
        {
            return CardStateType.E_CARD_STATE_WAITING;
        }

        public override void OnEnter()
        {
            m_BattleCard.Battle_Slot.SetHpBarVisible(false);
            m_BattleCard.Battle_Slot.SetSlotFrameVisible(true);

            m_BattleCard.CardUI.tomb.gameObject.SetActive(false);
            if (m_BattleCard.IsDead)
            {
                m_BattleCard.CardUI.angel.SetActive(true);
                m_BattleCard.CardUI.angelBird.gameObject.SetActive(true);
                m_BattleCard.CardUI.angelBird.GetComponent<TweenAlpha>().enabled = true;
                m_BattleCard.CardUI.angelWord.gameObject.SetActive(true);
                m_BattleCard.CardUI.Show();
            }

            m_BattleCard.RemoveAllBuff();
        }

        public override void OnLeave()
        {

        }

        public override void Update()
        {

        }

        private void OnBattleCardClicked(EventDefine type, System.Object[] args)
        {
            if (args.Length < 0 || args[0] != m_BattleCard.BattleCardObj)
            {
                return;
            }

            if (m_BattleCard.GetCardState() != CardStateType.E_CARD_STATE_WAITING)
            {
                return;
            }

            if (!m_BattleCard.IsDead)
            {
                return;
            }

            BoxManager.showMessageByID((int)MessageIdEnum.Msg135, Obj_MyselfPlayer.GetMe().money.ToString(), ((Obj_MyselfPlayer.GetMe().reviveCount + 1) * 1000).ToString());
            UIEventListener.Get(BoxManager.buttonYes).onClick += ReviveComfirmHandler;
        }

        void ReviveComfirmHandler(GameObject obj)
        {
            int money = (Obj_MyselfPlayer.GetMe().reviveCount + 1) * 1000;
            if (Obj_MyselfPlayer.GetMe().money >= money)
            {
                m_BattleCard.CardUI.angel.SetActive(true);
                m_BattleCard.CardUI.angelBird.gameObject.SetActive(true);
                m_BattleCard.CardUI.angelWord.gameObject.SetActive(true);
                Obj_MyselfPlayer.GetMe().reviveCount++;
                Obj_MyselfPlayer.GetMe().money -= money;
                m_BattleCard.CardUI.ShowReviveAnimation();
            }
            else
            {
				NetworkSender.Instance().buyGold(BuyGoldFinish,2);
            }
        }
		
		
		
		public void BuyGoldFinish(bool isSucess)
		{
			if (isSucess)
			{
				BoxManager.showMessageByID((int)MessageIdEnum.Msg59);
				Debug.Log("Buy Gold Finish");
			}
			else
				Debug.LogError("Buy Gold Error");
		}
    }
}

