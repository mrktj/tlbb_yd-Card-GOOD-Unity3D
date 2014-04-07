using UnityEngine;
using System.Collections;
using GCGame.Table;

namespace Games.Battle
{
    public class SkillOnce : SkillBase
    {
        private EffectBase m_AttackEffect;

        public SkillOnce(BattleCard owner, int id)
        {
            m_SkillOwner = owner;
            m_SkillId = id;
            m_SkillType = SkillType.E_SKILL_TYPE_ONCE;
        }

        public override bool Init()
        {
            m_SkillTab = TableManager.GetSkillByID(m_SkillId);
            m_SkillDisplayTab = TableManager.GetSkillDisplayByID(m_SkillTab.Effect);
            m_BattleRoot = BattleUI.Instacne.battleRoot;

            return true;
        }

        public override bool LoadSkill()
        {
            m_AttackEffect = EffectManager.Instance.CreateEffect(this, (BattleEffectType)m_SkillDisplayTab.AttackAnimType, m_SkillDisplayTab.AttackAnim, m_SkillDisplayTab.AttackLib);
            
            if (!m_AttackEffect.Load())
            {
                Debug.LogError("Load attack effect error! " + m_SkillDisplayTab.AttackLib);
                return false;
            }

            return true;
        }

        public override void UseSkill()
        {
            m_AttackEffect.EffectEnd += SkillEndEventHandler;
            m_AttackEffect.StartPos = m_AttackEffect.EndPos = m_SkillOwner.BattleCardObj.transform;
            m_AttackEffect.MoveSpeed = 0f;

            m_AttackEffect.Play(null);
        }

        private void SkillEndEventHandler(EffectBase effect, BattleCard target, float total_time)
        {
            m_AttackEffect.EffectEnd -= SkillEndEventHandler;

            EventManager.Instance.Fire(EventDefine.BATTLE_CARD_ATTACK_END, m_SkillOwner.BattleCardObj);

            if (m_TargetList.Count > 0)
            {
                target.OnHit(m_SkillOwner, m_BehitMap[target]);
            }
        }
    }
}
