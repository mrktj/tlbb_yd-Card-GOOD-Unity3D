/*
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using GCGame.Table;
using Games.CharacterLogic;

namespace Games.Battle
{
    public class SkillDirect : SkillToolkit
    {
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

        public override bool LoadSkill()
        {
            m_AttackAnimLib = Resources.Load("Effect/" + m_SkillDisplayTab.AttackLib, typeof(tk2dSpriteAnimation)) as tk2dSpriteAnimation;
            m_BehitAnimLib = Resources.Load("Effect/" + m_SkillDisplayTab.BehitLib, typeof(tk2dSpriteAnimation)) as tk2dSpriteAnimation;

            return true;
        }

        public override void UseSkill()
        {
            PlayAttackAnimation();
        }

        public void PlayAttackAnimation()
        {
            m_IsAttackEnd = false;

            if (m_AttackAnimLib == null || m_AttackAnimLib.GetClipIdByName(m_SkillDisplayTab.AttackAnim) == -1)
            {
                Debug.LogError("Can not find res " + m_SkillDisplayTab.AttackAnim + " in " + m_SkillDisplayTab.AttackLib);
                foreach (BattleCard card in m_TargetList)
                {
                    PlayBeHitAnimation(card);
                }
                return;
            }

            GameObject go = new GameObject(m_SkillDisplayTab.AttackAnim);
            go.transform.parent = m_BattleRoot;
            Vector3 pos = new Vector3(0.0f, 0.0f, -30.0f);
            pos.x = m_SkillOwner.BattleCardObj.transform.localPosition.x;
            pos.y = m_SkillOwner.BattleCardObj.transform.localPosition.y;
            go.transform.localPosition = pos;
            go.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            tk2dAnimatedSprite sprite = tk2dAnimatedSprite.AddComponent(go, m_AttackAnimLib, m_AttackAnimLib.GetClipIdByName(m_SkillDisplayTab.AttackAnim));
            sprite.animationCompleteDelegate += AttackAnimationCompleteHandler;
            sprite.animationEventDelegate = AttackAnimationEventHandler;
            sprite.Play(m_SkillDisplayTab.AttackAnim);

            //播放攻击音效
            AudioManager.Instance.PlayEffectSound(m_SkillDisplayTab.Music, Obj_MyselfPlayer.GetMe().acceleration);

            if (Obj_MyselfPlayer.GetMe().isGuideBattle)
            {
                ShowSkillName();
            }
        }

        void AttackAnimationEventHandler(tk2dAnimatedSprite sprite, tk2dSpriteAnimationClip clip, tk2dSpriteAnimationFrame frame, int frameNum)
        {
            m_IsAttackEnd = true;
            m_BehitSpriteMap.Clear();
            foreach (BattleCard card in m_TargetList)
            {
                PlayBeHitAnimation(card);
            }
            if (frame.eventInt == (int)ToolKitEventType.TOOLKIT_ANIMATION_NEXT)
            {

            }
        }

        void AttackAnimationCompleteHandler(tk2dAnimatedSprite animSprite, int clipId)
        {
            if (!m_IsAttackEnd)
            {
                m_BehitSpriteMap.Clear();
                foreach (BattleCard card in m_TargetList)
                {
                    PlayBeHitAnimation(card);
                }
            }
            GameObject.Destroy(animSprite.gameObject);
            EventManager.Instance.Fire(EventDefine.BATTLE_CARD_ATTACK_END, m_SkillOwner.BattleCardObj);
        }

        public void PlayBeHitAnimation(BattleCard target)
        {
            if (m_BehitAnimLib == null || m_BehitAnimLib.GetClipIdByName(m_SkillDisplayTab.BehitAnim) == -1)
            {
                Debug.LogError("Can not find res " + m_SkillDisplayTab.BehitAnim + " in " + m_SkillDisplayTab.BehitLib);
                target.AnimateBeHit(m_BehitMap[target]);
                target.ShowHitValue(m_BehitMap[target], false);

                target.OnHit(m_SkillOwner, m_BehitMap[target]);
                return;
            }

            GameObject go = new GameObject(m_SkillDisplayTab.BehitAnim);
            go.transform.parent = m_BattleRoot;
            Vector3 pos = new Vector3(0.0f, 0.0f, -30.0f);
            pos.x = target.BattleCardObj.transform.localPosition.x;
            pos.y = target.BattleCardObj.transform.localPosition.y;
            go.transform.localPosition = pos;
            go.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            tk2dAnimatedSprite behit = tk2dAnimatedSprite.AddComponent(go, m_BehitAnimLib, m_BehitAnimLib.GetClipIdByName(m_SkillDisplayTab.BehitAnim));
            behit.animationCompleteDelegate += BeHitAnimationCompleteHandler;
            behit.Play(m_SkillDisplayTab.BehitAnim);

            m_BehitSpriteMap.Add(behit, target);

            target.AnimateBeHit(m_BehitMap[target]);
            target.ShowHitValue(m_BehitMap[target], false);
        }

        void BeHitAnimationCompleteHandler(tk2dAnimatedSprite animSprite, int clipId)
        {
            if (animSprite != null)
            {
                BattleCard target = m_BehitSpriteMap[animSprite];
                target.OnHit(m_SkillOwner, m_BehitMap[target]);
                m_BehitSpriteMap.Remove(animSprite);
                GameObject.Destroy(animSprite.gameObject);
            }
        }
    }
*/
