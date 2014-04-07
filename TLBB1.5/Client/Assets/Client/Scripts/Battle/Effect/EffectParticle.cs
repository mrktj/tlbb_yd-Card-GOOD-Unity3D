using UnityEngine;
using System.Collections;

namespace Games.Battle
{
    public class EffectParticle : EffectBase
    {
        protected GameObject m_EffectLib;

        public EffectParticle(SkillBase owner, string name, string lib)
        {
            m_EffectOwner = owner;
            m_EffectName = name;
            m_LibName = lib;
            m_EffectType = BattleEffectType.E_BATTLE_EFFECT_TYPE_PARTICLE;
        }

        public override bool Init()
        {
            return true;
        }

        public override bool Load()
        {
            m_EffectLib = Resources.Load("EffectParticle/" + m_EffectName, typeof(GameObject)) as GameObject;
            if (m_EffectLib == null)
            {
                return false;
            }

            return true;
        }

        public override bool Play(BattleCard target)
        {
            m_TargetCard = target;

            if (m_EffectLib == null)
            {
                Debug.LogError("Can't play effect! " + m_EffectName);
                if (EffectEnd != null)
                {
                    EffectEnd(this, m_TargetCard, 0f);
                }
                return false;
            }

            GameObject go = GameObject.Instantiate(m_EffectLib) as GameObject;
			Vector3 _localScale = go.transform.localScale * m_StartPos.localScale.x;
            go.transform.parent = m_StartPos.parent;
            Vector3 pos = new Vector3(0.0f, 0.0f, -30.0f);
            pos.x = m_StartPos.localPosition.x;
            pos.y = m_StartPos.localPosition.y;
            go.transform.localPosition = pos;
            go.transform.localScale = _localScale;//new Vector3(512.0f, 512.0f, 0.0f);

            //计算旋转方向
            Vector3 direct = m_DirectionPoint - m_StartPos.transform.localPosition;
			if(direct.x != 0f || direct.y != 0f)
			{
				direct.z = 0f;
				go.transform.up = direct;
			}

            //如果速度不为0，则特效需要飞行，添加iTween事件
            if (m_MoveSpeed > 0f)
            {
                go.AddComponent<iTweenHandler>().ProjectileFlyCompleteHandler = ProjectileFlyCompleteHandler;

                Hashtable args = new Hashtable();
                args.Add("time", m_MoveSpeed);
                args.Add("x", m_StartPos.position.x);
                args.Add("y", m_StartPos.position.y);
                args.Add("looptype", iTween.LoopType.none);
                args.Add("easetype", iTween.EaseType.linear);
                args.Add("oncompletetarget", go);
                args.Add("oncompleteparams", go);
                args.Add("oncomplete", "OnProjectileFlyComplete");
                iTween.MoveTo(go, args);
            }

            DestroyParticle dp = go.AddComponent<DestroyParticle>();
            dp.particleCompleteDelegate += AnimationCompleteHandler;
            dp.DestroyTime = 1000f;

            m_EffectObj = go;
            return true;
        }

        private void AnimationCompleteHandler(GameObject go)
        {
            go.GetComponent<DestroyParticle>().particleCompleteDelegate -= AnimationCompleteHandler;

            if (EffectEnd != null)
            {
                EffectEnd(this, m_TargetCard, 1f);
            }

            GameObject.Destroy(go);
        }

        public void ProjectileFlyCompleteHandler(GameObject go)
        {
            iTween.Stop(go);
            if (EffectEnd != null)
            {
                EffectEnd(this, m_TargetCard, 1f);
            }
            GameObject.Destroy(go);
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
            EffectParticle effect = (EffectParticle)EffectManager.Instance.CreateEffect(m_EffectOwner, m_EffectType, m_EffectName, m_LibName);
            effect.m_EffectLib = this.m_EffectLib;
            return effect;
        }

        public override void Destroy()
        {
            if (m_EffectObj != null)
            {
                m_EffectObj.SetActive(false);
                GameObject.Destroy(m_EffectObj);
            }
        }
    }
}