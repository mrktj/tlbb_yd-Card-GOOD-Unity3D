/*
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Games.CharacterLogic;
using GCGame.Table;

namespace Games.Battle
{
    public class SkillProjectile : SkillToolkit
    {
        public SkillProjectile(BattleCard owner, int id)
        {
            m_SkillOwner = owner;
            m_SkillId = id;
            m_SkillType = SkillType.E_SKILL_TYPE_PROJECTILE;

            EventManager.Instance.RegisterEventHandler(EventDefine.SKILL_PROJECTILE_FLY_COMPLETE, OnProjectileFlyComplete);
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
            m_ProjectileAnimLib = Resources.Load("Effect/" + m_SkillDisplayTab.ProjectileLib, typeof(tk2dSpriteAnimation)) as tk2dSpriteAnimation;                        
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

            if (m_AttackAnimLib != null && m_AttackAnimLib.GetClipIdByName(m_SkillDisplayTab.AttackAnim) != -1)
            {
                GameObject go = new GameObject(m_SkillDisplayTab.AttackLib);
                go.transform.parent = m_BattleRoot;
                Vector3 pos = new Vector3(0.0f, 0.0f, -30.0f);
                pos.x = m_SkillOwner.BattleCardObj.transform.localPosition.x;
                pos.y = m_SkillOwner.BattleCardObj.transform.localPosition.y;
                go.transform.localPosition = pos;
                go.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                tk2dAnimatedSprite sprite = tk2dAnimatedSprite.AddComponent(go, m_AttackAnimLib, m_AttackAnimLib.GetClipIdByName(m_SkillDisplayTab.AttackAnim));
                sprite.animationCompleteDelegate += AttackAnimationCompleteHandler;
                sprite.animationEventDelegate = AttackAnimationEventHandler;

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
                //2.根据中间点算方向--
                float x = mid.x - m_SkillOwner.BattleCardObj.transform.localPosition.x;
                float y = mid.y - m_SkillOwner.BattleCardObj.transform.localPosition.y;
                float t = (float)System.Math.Atan(System.Math.Abs(y / x));
                t = (float)(t * 180 / System.Math.PI);
                if (x >= 0 && y >= 0)//1
                {
                    t = -(90 - t);
                }
                else if (x < 0 && y >= 0)//2
                {
                    t = 90 - t;
                }
                else if (x < 0 && y < 0)//3
                {
                    t = 90 + t;
                }
                else
                {
                    t = -(90 + t);
                }
                if (m_SkillOwner.Card_Type == BattleCardType.E_BATTLE_CARD_TYPE_OTHER)
                {
                    sprite.FlipY();
                    t = t - 180;
                }
                sprite.transform.localRotation = Quaternion.AngleAxis(t, Vector3.forward);
                sprite.Play(m_SkillDisplayTab.AttackAnim);

                //播放攻击音效
                AudioManager.Instance.PlayEffectSound(m_SkillDisplayTab.Music, Obj_MyselfPlayer.GetMe().acceleration);
            }
            else
            {
                Debug.LogError("Can not find res " + m_SkillDisplayTab.AttackAnim + " in " + m_SkillDisplayTab.AttackLib);
                PlayProjectileAnimation();
                AudioManager.Instance.PlayEffectSound(m_SkillDisplayTab.Music, Obj_MyselfPlayer.GetMe().acceleration);
            }

            if (Obj_MyselfPlayer.GetMe().isGuideBattle)
            {
                ShowSkillName();
            }
        }

        void AttackAnimationEventHandler(tk2dAnimatedSprite sprite, tk2dSpriteAnimationClip clip, tk2dSpriteAnimationFrame frame, int frameNum)
        {
            m_IsAttackEnd = true;
            PlayProjectileAnimation();
        }

        void AttackAnimationCompleteHandler(tk2dAnimatedSprite animSprite, int clipId)
        {
            if (!m_IsAttackEnd)
            {
                foreach (BattleCard card in m_TargetList)
                {
                    PlayBeHitAnimation(card);
                }
            }
            GameObject.Destroy(animSprite.gameObject);
            EventManager.Instance.Fire(EventDefine.BATTLE_CARD_ATTACK_END, m_SkillOwner.BattleCardObj);
        }

        public void PlayProjectileAnimation()
        {
            if (m_ProjectileAnimLib == null || m_ProjectileAnimLib.GetClipIdByName(m_SkillDisplayTab.ProjectileAnim) == -1)
            {
                Debug.LogError("no projectile res, " + m_SkillDisplayTab.ProjectileAnim + " in " + m_SkillDisplayTab.ProjectileLib);
                foreach (BattleCard card in m_TargetList)
                {
                    PlayBeHitAnimation(card);
                }
                return;
            }

            float ftime = m_SkillDisplayTab.FlyTime * 0.001f;  //除以1000
            if (ftime < 0)
            {
                Debug.LogWarning("ftime = " + ftime);
                ftime = 0.5f;
            }
            m_ProjectileSpriteMap.Clear();
            for (int i = 0; i < m_TargetList.Count; i++)
            {
                if (m_TargetList[i].BattleCardObj == null)
                {
                    Debug.LogWarning("PlayProjectileAnimation(), target obj is null");
                    continue;
                }

                GameObject go = new GameObject(m_SkillDisplayTab.ProjectileAnim);
                go.transform.parent = m_BattleRoot;
                Vector3 pos = new Vector3(0.0f, 0.0f, -30.0f);
                pos.x = m_SkillOwner.BattleCardObj.transform.localPosition.x;
                pos.y = m_SkillOwner.BattleCardObj.transform.localPosition.y;
                go.transform.localPosition = pos;
                go.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                float x = m_TargetList[i].BattleCardObj.transform.localPosition.x - m_SkillOwner.BattleCardObj.transform.localPosition.x;
                float y = m_TargetList[i].BattleCardObj.transform.localPosition.y - m_SkillOwner.BattleCardObj.transform.localPosition.y;
                float t = (float)System.Math.Atan(System.Math.Abs(y / x));
                t = (float)(t * 180 / System.Math.PI);
                if (x >= 0 && y >= 0)//1
                {
                    t = -(90 - t);
                }
                else if (x < 0 && y >= 0)//2
                {
                    t = 90 - t;
                }
                else if (x < 0 && y < 0)//3
                {
                    t = 90 + t;
                }
                else
                {
                    t = -(90 + t);
                }

                tk2dAnimatedSprite projectile = tk2dAnimatedSprite.AddComponent(go, m_ProjectileAnimLib, m_ProjectileAnimLib.GetClipIdByName(m_SkillDisplayTab.ProjectileAnim));
                if (m_SkillOwner.Card_Type == BattleCardType.E_BATTLE_CARD_TYPE_OTHER)
                {
                    projectile.FlipY();
                    t = t - 180;
                }
                projectile.transform.localRotation = Quaternion.AngleAxis(t, Vector3.forward);

                projectile.Play(m_SkillDisplayTab.ProjectileAnim);

                go.AddComponent<iTweenHandler>();

                Hashtable args = new Hashtable();
                args.Add("time", ftime);
                args.Add("x", m_TargetList[i].BattleCardObj.transform.position.x);
                args.Add("y", m_TargetList[i].BattleCardObj.transform.position.y);
                args.Add("looptype", iTween.LoopType.none);
                args.Add("easetype", iTween.EaseType.linear);
                args.Add("oncompletetarget", go);
                args.Add("oncompleteparams", projectile.gameObject);
                args.Add("oncomplete", "OnProjectileFlyComplete");
                iTween.MoveTo(projectile.gameObject, args);

                m_ProjectileSpriteMap.Add(projectile, m_TargetList[i]);
            }
        }

        public void OnProjectileFlyComplete(EventDefine type, System.Object[] args)
        {
            if (args.Length < 0)
            {
                return;
            }
            GameObject go = args[0] as GameObject;
            tk2dAnimatedSprite sprite = go.GetComponent<tk2dAnimatedSprite>();
            if (sprite == null)
            {
                return;
            }

            if (!m_ProjectileSpriteMap.ContainsKey(sprite))
            {
                return;
            }

            iTween.Stop(go);
            PlayBeHitAnimation(m_ProjectileSpriteMap[sprite]);
            m_ProjectileSpriteMap.Remove(sprite);

            GameObject.Destroy(go);
        }

        public void PlayBeHitAnimation(BattleCard target)
        {
            if (target == null)
            {
                Debug.LogError("PlayBeHitAnimation(), target is null !");
                return;
            }
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
            BattleCard target = m_BehitSpriteMap[animSprite];
            target.OnHit(m_SkillOwner, m_BehitMap[target]);
            m_BehitSpriteMap.Remove(animSprite);
            GameObject.Destroy(animSprite.gameObject);
        }
    }
}
*/
