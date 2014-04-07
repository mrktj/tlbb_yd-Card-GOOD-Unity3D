using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GCGame.Table;

namespace Games.Battle
{
    public class CardStateAttack : CardStateBase
    {
        public CardStateAttack()
        {
            EventManager.Instance.RegisterEventHandler(EventDefine.BATTLE_CARD_ATTACK_END, OnBattleCardAttackEnd);
        }
        public override CardStateType GetStateType()
        {
            return CardStateType.E_CARD_STATE_ATTACK;
        }

        public override void OnEnter()
        {
            m_BattleCard.InAttackAction = true;

            AnimateAttack(m_BattleCard.CurActionBag.attackBag.skillID);
            m_BattleCard.CurSkill.UseSkill();
        }

        public override void OnLeave()
        {
            m_BattleCard.InAttackAction = false;
        }

        public override void Update()
        {

        }

        private void OnBattleCardAttackEnd(EventDefine type, System.Object[] args)
        {
            if(args.Length < 0 || (args[0] as GameObject) != m_BattleCard.BattleCardObj )
            {
                return;
            }

            m_BattleCard.ShowBuffValue(false);
            //在出手时可能会有中毒Buff杀掉自己d--
            if (m_BattleCard.CurrentHp <= 0)
            {
                m_BattleCard.OnDead();
            }
        }

        //攻击和受击动画--
        private void AnimateAttack(int skillid)
        {
            if (TableManager.GetSkillByID(m_BattleCard.CurActionBag.attackBag.skillID).SkillType == 0)
            {
                m_BattleCard.BattleCardObj.animation.Play("CardAttack_Near", PlayMode.StopAll);
            }
            else
            {
                if (m_BattleCard.Card_Type == BattleCardType.E_BATTLE_CARD_TYPE_SELF)
                {
                    m_BattleCard.BattleCardObj.animation.Play("CardAttack_Range_Self", PlayMode.StopAll);
                }
                else
                {
                    m_BattleCard.BattleCardObj.animation.Play("CardAttack_Range_Other", PlayMode.StopAll);
                }
            }
        }

        private void OnAnimateAttackCompleted(GameObject go)
        {

        }
    }
}

