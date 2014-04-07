using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Games.CharacterLogic;

namespace Games.Battle
{
    public sealed class BattleCardManager : Singleton<BattleCardManager>
    {
        private BattleCore m_BattleCore = null;
        private IDictionary<BattleCardType, List<BattleCard>> m_BattleCardList = new Dictionary<BattleCardType, List<BattleCard>>();
        private IDictionary<BattleCardType, BattleSlot[]> m_BattleSlotArray = new Dictionary<BattleCardType, BattleSlot[]>();
        public IDictionary<BattleCardType, BattleSlot[]> BattleSlotArray { get { return m_BattleSlotArray; } }

        public BattleCardManager()
        {
            m_BattleCardList[BattleCardType.E_BATTLE_CARD_TYPE_SELF] = new List<BattleCard>();
            m_BattleCardList[BattleCardType.E_BATTLE_CARD_TYPE_OTHER] = new List<BattleCard>();

            m_BattleSlotArray[BattleCardType.E_BATTLE_CARD_TYPE_SELF] = new BattleSlot[6];
            m_BattleSlotArray[BattleCardType.E_BATTLE_CARD_TYPE_OTHER] = new BattleSlot[6];
        }

        public bool Init(BattleCore core)
        {
            m_BattleCore = core;
            BattleSlot[] self_slots = new BattleSlot[6];
            for (int i = 0; i < 6; i++)
            {
                self_slots[i] = new BattleSlot(i);
            }
            m_BattleSlotArray[BattleCardType.E_BATTLE_CARD_TYPE_SELF] = self_slots;

            BattleSlot[] other_slots = new BattleSlot[6];
            for (int i = 0; i < 6; i++)
            {
                other_slots[i] = new BattleSlot(i + 6);
            }
            m_BattleSlotArray[BattleCardType.E_BATTLE_CARD_TYPE_OTHER] = other_slots; 

            return true;
        }


        public BattlePlayer GetBattlePlayer() { return m_BattleCore.GetBattlePlayer(); }

        public BattleCard CreateCard(TroopMember member)
        {
            BattleCard battle_card = new BattleCard(member);
            if (!battle_card.Init())
            {
                Debug.LogError("CreateCard");
                return null;
            }
            if (!battle_card.PreLoad())
            {
                Debug.LogError("CreateCard");
                return null;
            }
            battle_card.ChangeState(CardStateType.E_CARD_STATE_PREPARE);
            m_BattleCardList[battle_card.Card_Type].Add(battle_card);

            return battle_card;
        }

        public void CreateCard(List<TroopMember> members)
        {
            foreach (TroopMember member in members)
            {
                CreateCard(member);
            }
        }

        public void DestroyCard(BattleCard battle_card)
        {
            m_BattleCardList[battle_card.Card_Type].Remove(battle_card);
            battle_card.Destroy();
        }

        public void DestroyCard(BattleCardType type)
        {
            if (type == BattleCardType.E_BATTLE_CARD_TYPE_ALL)
            {
                foreach (BattleCard battle_card in m_BattleCardList[BattleCardType.E_BATTLE_CARD_TYPE_SELF])
                {
                    battle_card.Destroy();
                }
                m_BattleCardList[BattleCardType.E_BATTLE_CARD_TYPE_SELF].Clear();
                foreach (BattleCard battle_card in m_BattleCardList[BattleCardType.E_BATTLE_CARD_TYPE_OTHER])
                {
                    battle_card.Destroy();
                }
                m_BattleCardList[BattleCardType.E_BATTLE_CARD_TYPE_OTHER].Clear();
            }
            else
            {
                foreach (BattleCard battle_card in m_BattleCardList[type])
                {
                    battle_card.Destroy();
                }
                m_BattleCardList[type].Clear();
            }
        }

        public void ResetCard(BattleCardType type)
        {
            if (type == BattleCardType.E_BATTLE_CARD_TYPE_ALL)
            {
                foreach (BattleCard battle_card in m_BattleCardList[BattleCardType.E_BATTLE_CARD_TYPE_SELF])
                {
                    battle_card.Reset();
                }

                foreach (BattleCard battle_card in m_BattleCardList[BattleCardType.E_BATTLE_CARD_TYPE_OTHER])
                {
                    battle_card.Reset();
                }
            }
            else
            {
                foreach (BattleCard battle_card in m_BattleCardList[type])
                {
                    battle_card.Reset();
                }
            }
        }

        public void StartMoveCard(BattleCardType type)
        {
            foreach (BattleCard battle_card in m_BattleCardList[type])
            {
                if (!battle_card.IsDead)
                {
                    battle_card.ChangeState(CardStateType.E_CARD_STATE_FORWARD);
                }
                else
                {
                    battle_card.ChangeState(CardStateType.E_CARD_STATE_DEAD);
                }
            }
        }

        public void StopMoveCard(BattleCardType type)
        {
            foreach (BattleCard battle_card in m_BattleCardList[type])
            {
                if (!battle_card.IsDead)
                {
                    battle_card.ChangeState(CardStateType.E_CARD_STATE_NORMAL);
                }
                else
                {
                    battle_card.ChangeState(CardStateType.E_CARD_STATE_DEAD);
                }
            }
        }

        public void MakeCardInBattling()
        {
            foreach (BattleCard battle_card in m_BattleCardList[BattleCardType.E_BATTLE_CARD_TYPE_SELF])
            {
                if (!battle_card.IsDead)
                {
                    battle_card.ChangeState(CardStateType.E_CARD_STATE_IN_BATTLE);
                }
                else
                {
                    battle_card.ChangeState(CardStateType.E_CARD_STATE_DEAD);
                }
            }

            foreach (BattleCard battle_card in m_BattleCardList[BattleCardType.E_BATTLE_CARD_TYPE_OTHER])
            {
                if (!battle_card.IsDead)
                {
                    battle_card.ChangeState(CardStateType.E_CARD_STATE_IN_BATTLE);
                }
                else
                {
                    battle_card.ChangeState(CardStateType.E_CARD_STATE_DEAD);
                }
            }
        }
		
		public void MakeCardStatePrepareRevive()
		{
			foreach (BattleCard battle_card in m_BattleCardList[BattleCardType.E_BATTLE_CARD_TYPE_SELF])
            {
				if(battle_card.IsDead)
				{
					battle_card.ChangeState(CardStateType.E_CARD_STATE_PREPARE);
				}
            }
		}
		
        public void MakeCardStateWaiting(BattleCardType type)
        {
            if (type == BattleCardType.E_BATTLE_CARD_TYPE_ALL)
            {
                foreach (BattleCard battle_card in m_BattleCardList[BattleCardType.E_BATTLE_CARD_TYPE_SELF])
                {
                    battle_card.ChangeState(CardStateType.E_CARD_STATE_WAITING);
                }

                foreach (BattleCard battle_card in m_BattleCardList[BattleCardType.E_BATTLE_CARD_TYPE_OTHER])
                {
                    battle_card.ChangeState(CardStateType.E_CARD_STATE_WAITING);
                }
            }
            else
            {
                foreach (BattleCard battle_card in m_BattleCardList[type])
                {
                    battle_card.ChangeState(CardStateType.E_CARD_STATE_WAITING);
                }
            }
        }

		public BattleCard FindCardByGUID(long guid)
		{
            List<BattleCard> card_list = m_BattleCardList[BattleCardType.E_BATTLE_CARD_TYPE_SELF];
            BattleCard battle_card = card_list.Find(delegate(BattleCard cd) { return cd.Card_Data.CardGuid == guid; });
            if (battle_card != null)
            {
                return battle_card;
            }

            card_list = m_BattleCardList[BattleCardType.E_BATTLE_CARD_TYPE_OTHER];
            battle_card = card_list.Find(delegate(BattleCard cd) { return cd.Card_Data.CardGuid == guid; });
            if (battle_card != null)
            {
                return battle_card;
            }

            return null;
		}

        public BattleCard FindCardBySlotIndex(int index)
        {
            List<BattleCard> card_list =  m_BattleCardList[BattleCardType.E_BATTLE_CARD_TYPE_SELF];
            BattleCard battle_card = card_list.Find(delegate(BattleCard cd) { return cd.Battle_Slot.SlotIndex == index; });
            if (battle_card != null)
            {
                return battle_card;
            }

            card_list = m_BattleCardList[BattleCardType.E_BATTLE_CARD_TYPE_OTHER];
            battle_card = card_list.Find(delegate(BattleCard cd) { return cd.Battle_Slot.SlotIndex == index; });
            if (battle_card != null)
            {
                return battle_card;
            }

            return null;
        }

        public void DestroyAllBuffs(BattleCardType type)
        {
            if (type == BattleCardType.E_BATTLE_CARD_TYPE_ALL)
            {
                foreach (BattleCard battle_card in m_BattleCardList[BattleCardType.E_BATTLE_CARD_TYPE_SELF])
                {
                    battle_card.RemoveAllBuff();
                }

                foreach (BattleCard battle_card in m_BattleCardList[BattleCardType.E_BATTLE_CARD_TYPE_OTHER])
                {
                    battle_card.RemoveAllBuff();
                }
            }
            else
            {
                foreach (BattleCard battle_card in m_BattleCardList[type])
                {
                    battle_card.RemoveAllBuff();
                }
            }
        }
		
		public void ReviveAllCardFree()
		{
			foreach (BattleCard card in m_BattleCardList[BattleCardType.E_BATTLE_CARD_TYPE_SELF])
            {
                if(card.IsDead)
				{
					card.Revive();
				}
            }
		}
		
		public bool IsAllSelfCardAlive()
		{
			foreach (BattleCard card in m_BattleCardList[BattleCardType.E_BATTLE_CARD_TYPE_SELF])
            {
                if (card.IsDead)
                {
                    return false;
                }
            }
			return true;
		}
		
        //回合完后检查卡牌的hp与卡牌的死亡状态--
        public void CheckAllCardState()
        {
            bool self_alive = false;
            bool other_alive = false;
            foreach (BattleCard card in m_BattleCardList[BattleCardType.E_BATTLE_CARD_TYPE_SELF])
            {
                if (card.CurrentHp > 0)
                {
                    self_alive = true;
                    break;
                }
            }
            foreach (BattleCard card in m_BattleCardList[BattleCardType.E_BATTLE_CARD_TYPE_OTHER])
            {
                if (card.CurrentHp > 0)
                {
                    other_alive = true;
                    break;
                }
            }

            if (self_alive && (!other_alive))
            {
                Debug.Log("self card alive, other card dead !");
            }
            else if ((!self_alive) && other_alive)
            {
                Debug.Log("self card dead, other card alive !");
            }
            else if (self_alive && other_alive)
            {
                Debug.LogError("self card alive, other card alive !");
            }
            else if ((!self_alive) && (!other_alive))
            {
                Debug.LogError("self card dead, other card dead !");
            }
        }

        public void UpdateCardState()
        {
            foreach (BattleCard card in m_BattleCardList[BattleCardType.E_BATTLE_CARD_TYPE_SELF])
            {
                int isDead = 0;
                if (card.GetCardState() == CardStateType.E_CARD_STATE_DEAD)
                {
                    isDead = 1;
                }
                Obj_MyselfPlayer.GetMe().cardStates[card.Battle_Slot.SlotIndex] = isDead;
            }
        }

		public void OnCardPosChanged(int srcPos, int tgtPos)
		{
			if(srcPos < 0 || srcPos >5)
			{
				return;
			}
			if(tgtPos < 0 || tgtPos > 5)
			{
				return;
			}
			if(srcPos == tgtPos)
			{
				return;
			}
			
			BattleSlot srcSlot = m_BattleSlotArray[BattleCardType.E_BATTLE_CARD_TYPE_SELF][srcPos];
            BattleCard srcCard = m_BattleSlotArray[BattleCardType.E_BATTLE_CARD_TYPE_SELF][srcPos].Battle_Card;
            BattleSlot tgtSlot = m_BattleSlotArray[BattleCardType.E_BATTLE_CARD_TYPE_SELF][tgtPos];
            BattleCard tgtCard = m_BattleSlotArray[BattleCardType.E_BATTLE_CARD_TYPE_SELF][tgtPos].Battle_Card;

			srcCard.OnChangeSlot(tgtSlot);
			tgtSlot.OnChangeCard(srcCard);
			
			if(tgtCard != null)
			{
				tgtCard.OnChangeSlot(srcSlot);
				srcSlot.OnChangeCard(tgtCard);
			}
			else
			{
				srcSlot.OnChangeCard(null);
			}
		}
		
		public void SetAllSlotFrameVisible(BattleCardType type, bool visible)
        {
            if (type == BattleCardType.E_BATTLE_CARD_TYPE_ALL)
            {
                foreach (BattleCard battle_card in m_BattleCardList[BattleCardType.E_BATTLE_CARD_TYPE_SELF])
                {
                    battle_card.Battle_Slot.SetSlotFrameVisible(visible);
                }

                foreach (BattleCard battle_card in m_BattleCardList[BattleCardType.E_BATTLE_CARD_TYPE_OTHER])
                {
                    battle_card.Battle_Slot.SetSlotFrameVisible(visible);
                }
            }
            else
            {
                foreach (BattleCard battle_card in m_BattleCardList[type])
                {
                    battle_card.Battle_Slot.SetSlotFrameVisible(visible);
                }
            }
        }
		
        public void OnDestroy()
        {
            DestroyCard(BattleCardType.E_BATTLE_CARD_TYPE_ALL);
            m_BattleCore = null;
            m_BattleCardList.Clear();
            m_BattleSlotArray = null;
            Debug.Log("BattleCardManager.OnDestroy()");
        }
    }
}
