using UnityEngine;
using System.Collections;
using Games.CharacterLogic;

namespace Games.Battle
{
    public class CardStateDead : CardStateBase
    {
        public override CardStateType GetStateType()
        {
            return CardStateType.E_CARD_STATE_DEAD;
        }

        public override void OnEnter()
        {
            m_BattleCard.Battle_Slot.SetHpBarVisible(false);
            m_BattleCard.RemoveAllBuff();

            if (m_BattleCard.CurrentHp > 0)
            {
                m_BattleCard.CardUI.angel.gameObject.SetActive(true);
                m_BattleCard.CardUI.angelBird.gameObject.SetActive(true);
                m_BattleCard.CardUI.angelWord.gameObject.SetActive(false);
                m_BattleCard.CardUI.angelBird.GetComponent<TweenAlpha>().alpha = 1f;
                m_BattleCard.CardUI.angelBird.GetComponent<TweenAlpha>().enabled = false;
            }
            else
            {
                m_BattleCard.BattleCardObj.GetComponent<CardUI>().OnDead();

                //己方卡牌阵亡
                if (m_BattleCard.Card_Type == BattleCardType.E_BATTLE_CARD_TYPE_SELF)
                {
                    AudioManager.Instance.PlayEffectSound(SoundResource.SoundRes.battle_carddie.ToString(), false, 0f, Obj_MyselfPlayer.GetMe().acceleration);
                }
                else
                {
                    AudioManager.Instance.PlayEffectSound(SoundResource.SoundRes.battle_enemydie.ToString(), false, 0f, Obj_MyselfPlayer.GetMe().acceleration);
                }
            }
        }

        public override void OnLeave()
        {
            m_BattleCard.CardUI.tomb.gameObject.SetActive(false);
        }

        public override void Update()
        {

        }
    }
}

