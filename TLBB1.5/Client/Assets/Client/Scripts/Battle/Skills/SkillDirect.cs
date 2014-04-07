using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using GCGame.Table;
using Games.CharacterLogic;

namespace Games.Battle
{
    public class SkillDirect : SkillBase
    {
        private EffectBase m_AttackEffect;
        private EffectBase m_BehitEffect;

        private bool m_IsAttackEffectEnd = true;

        public SkillDirect(BattleCard owner, int id)
        {
            m_SkillOwner = owner;
            m_SkillId = id;
            m_SkillType = SkillType.E_SKILL_TYPE_DIRECT;
        }

        public override bool Init()
        {
            m_SkillTab = TableManager.GetSkillByID(m_SkillId);
            m_SkillDisplayTab = TableManager.GetSkillDisplayByID(m_SkillTab.Effect);
            m_BattleRoot = BattleUI.Instacne.battleRoot;

            return true;
        }
		//加载技能
        public override bool LoadSkill()
        {
            m_AttackEffect = EffectManager.Instance.CreateEffect(this, (BattleEffectType)m_SkillDisplayTab.AttackAnimType, m_SkillDisplayTab.AttackAnim, m_SkillDisplayTab.AttackLib);

            if (!m_AttackEffect.Load())
            {
                Debug.LogError("Load attack effect error! " + m_SkillDisplayTab.AttackLib);
            }

            m_BehitEffect = EffectManager.Instance.CreateEffect(this, (BattleEffectType)m_SkillDisplayTab.BehitAnimType, m_SkillDisplayTab.BehitAnim, m_SkillDisplayTab.BehitLib);

            if (!m_BehitEffect.Load())
            {
                Debug.LogError("Load behit effect error! " + m_SkillDisplayTab.BehitLib);
            }

            return true;
        }

        public override void UseSkill()
        {
            PlayAttackEffect();
        }

        //攻击动画-
        private void PlayAttackEffect()
        {
            m_IsAttackEffectEnd = false;

            EffectBase effect = m_AttackEffect;//m_AttackEffect.Duplicate();
            effect.EffectEnd += AttackEffectEndEventHandler;
            effect.EffectUpdate += AttackEffectUpdateEventHandler;
            effect.StartPos = effect.EndPos = m_SkillOwner.BattleCardObj.transform;
            effect.MoveSpeed = 0f;
            effect.DirectionPoint = CalculateEffectDirection();

            effect.Play(null);

            //播放攻击音效
            AudioManager.Instance.PlayEffectSound(m_SkillDisplayTab.Music, Obj_MyselfPlayer.GetMe().acceleration);

            if (Obj_MyselfPlayer.GetMe().isGuideBattle)
            {
                ShowSkillName();
            }
        }

        private void AttackEffectEndEventHandler(EffectBase effect, BattleCard target, float total_time)
        {
            effect.EffectEnd -= AttackEffectEndEventHandler;
            EffectManager.Instance.DestroyEffect(effect);

            if (!m_IsAttackEffectEnd)
            {
                PlayBehitEffect();
            }

            m_IsAttackEffectEnd = true;
            EventManager.Instance.Fire(EventDefine.BATTLE_CARD_ATTACK_END, m_SkillOwner.BattleCardObj);
        }

        private void AttackEffectUpdateEventHandler(EffectBase effect, BattleCard target, float total_time, float cur_time)
        {
            effect.EffectUpdate -= AttackEffectUpdateEventHandler;

            m_IsAttackEffectEnd = true;

            PlayBehitEffect();
        }

        //受击动画-
        private void PlayBehitEffect()
        {
            foreach (BattleCard battle_card in m_TargetList)
            {
                EffectBase behit_effect = m_BehitEffect.Duplicate();
                behit_effect.EffectEnd += BehitEffectEndEventHandler;
                behit_effect.StartPos = behit_effect.EndPos = battle_card.BattleCardObj.transform;
                behit_effect.MoveSpeed = 0f;
                behit_effect.DirectionPoint = battle_card.BattleCardObj.transform.localPosition;//CalculateEffectDirection();

                battle_card.AnimateBeHit(m_BehitMap[battle_card]);
                battle_card.ShowHitValue(m_BehitMap[battle_card], this, false);

                behit_effect.Play(battle_card);//播放最后调用
            }
        }

        private void BehitEffectEndEventHandler(EffectBase effect, BattleCard target, float total_time)
        {
            effect.EffectEnd -= BehitEffectEndEventHandler;
            EffectManager.Instance.DestroyEffect(effect);

            target.OnHit(m_SkillOwner, m_BehitMap[target]);
        }

        private Vector3 CalculateEffectDirection()
        {
            //计算目标的方向, 1.找到目标的中间点--
            List<Vector3> pos_list = new List<Vector3>();
            for (int i = 0; i < m_TargetList.Count; i++)
            {
                pos_list.Add(m_TargetList[i].BattleCardObj.transform.localPosition);
            }
            pos_list.Sort(delegate(Vector3 v1, Vector3 v2) { return Comparer<float>.Default.Compare(v1.x, v2.x); });
            Vector3 mid, left, right;
            left = pos_list[0];
            right = pos_list[pos_list.Count - 1];
            mid = Vector3.zero;
            mid.x = Math.Abs((right.x + left.x) * 0.5f);
            mid.y = Math.Abs((right.y + left.y) * 0.5f);

            return mid;
        }
    }
}
