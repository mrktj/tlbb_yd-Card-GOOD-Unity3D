using UnityEngine;
using System.Collections;

namespace Games.Battle
{
    public class EffectSequence :EffectBase
    {
        private tk2dSpriteAnimation m_EffectLib;

        public EffectSequence(SkillBase owner, string name, string lib)
        {
            m_EffectOwner = owner;
            m_EffectName = name;
            m_LibName = lib;
            m_EffectType = BattleEffectType.E_BATTLE_EFFECT_TYPE_SEQUENCE;
        }

        public override bool Init()
        {
            return true;
        }

        public override bool Load()
        {
            m_EffectLib = Resources.Load("Effect/" + m_LibName, typeof(tk2dSpriteAnimation)) as tk2dSpriteAnimation;
            if (m_EffectLib == null)
            {
                return false;
            }

            return true;
        }
		
		//播放特效
        public override bool Play(BattleCard target)
        {
            m_TargetCard = target;

            if (m_EffectLib == null || m_EffectLib.GetClipIdByName(m_EffectName) == -1)
            {
                Debug.LogError("Can't play effect! " + m_EffectName);
                if (EffectEnd != null)
                {
                    EffectEnd(this, m_TargetCard, 0f);
                }
                return false;
            }

            GameObject go = new GameObject(m_EffectName);
            go.transform.parent = m_StartPos.parent;
            Vector3 pos = new Vector3(0.0f, 0.0f, -30.0f);
            pos.x = m_StartPos.localPosition.x;
            pos.y = m_StartPos.localPosition.y;
            go.transform.localPosition = pos;
			
			Vector3 targetScale = m_StartPos.localScale;
			go.transform.localScale = targetScale / 0.63f;
			Vector3 direct = m_DirectionPoint - m_StartPos.transform.localPosition;
			
			if(direct.x != 0f || direct.y != 0f)
			{
				direct.z = 0f;
				go.transform.up = direct;
			}
            tk2dAnimatedSprite sprite = tk2dAnimatedSprite.AddComponent(go, m_EffectLib, m_EffectLib.GetClipIdByName(m_EffectName));
            sprite.animationCompleteDelegate += AnimationCompleteHandler;
            sprite.animationEventDelegate += AnimationUpdateHandler;
			
			

            //如果速度不为0，则特效需要飞行，添加iTween事件
            if (m_MoveSpeed > 0f)
            {
                go.AddComponent<iTweenHandler>().ProjectileFlyCompleteHandler = ProjectileFlyCompleteHandler;

                Hashtable args = new Hashtable();
                args.Add("time", m_MoveSpeed);
                args.Add("x", m_EndPos.position.x);
                args.Add("y", m_EndPos.position.y);
                args.Add("looptype", iTween.LoopType.none);
                args.Add("easetype", iTween.EaseType.linear);
                args.Add("oncompletetarget", go);
                args.Add("oncompleteparams", go);
                args.Add("oncomplete", "OnProjectileFlyComplete");
                iTween.MoveTo(go, args);
            }

            //播放
            sprite.Play(m_EffectName);

            m_EffectObj = go;
            return true;
        }

        private void AnimationCompleteHandler(tk2dAnimatedSprite sprite, int clipId)
        {
            if (EffectEnd != null)
            {
                EffectEnd(this, m_TargetCard, sprite.CurrentClip.frames.Length * Time.deltaTime);
            }
			EffectManager.Instance.DestroyEffect(this);
            GameObject.Destroy(sprite.gameObject);
        }

        private void AnimationUpdateHandler(tk2dAnimatedSprite sprite, tk2dSpriteAnimationClip clip, tk2dSpriteAnimationFrame frame, int frameNum)
        {
            if (EffectUpdate != null)
            {
                EffectUpdate(this, m_TargetCard, clip.frames.Length * Time.deltaTime, frameNum * Time.deltaTime);
            }
        }

        public void ProjectileFlyCompleteHandler(GameObject go)
        {
            iTween.Stop(go);
            if (EffectEnd != null)
            {
                EffectEnd(this, m_TargetCard, 1f);
            }
            EffectManager.Instance.DestroyEffect(this);
        }

        public override void Stop()
        {
            base.Stop();
        }

        public override void SetVisible(bool visible)
        {
            base.SetVisible(visible);
        }

        public override EffectBase Duplicate()
        {
            EffectSequence effect = (EffectSequence)EffectManager.Instance.CreateEffect(m_EffectOwner, m_EffectType, m_EffectName, m_LibName);
			effect.m_EffectLib = this.m_EffectLib;
            return effect;
        }

        public override void Destroy()
        {
            if (m_EffectObj != null)
            {
                m_EffectObj.GetComponent<tk2dAnimatedSprite>().Stop();
                m_EffectObj.SetActive(false);
                GameObject.Destroy(m_EffectObj);
            }
        }
    }
}