using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Games.Battle
{
    public sealed class EffectManager : Singleton<EffectManager>
    {
        private BattleCore m_BattleCore = null;
        private IList<EffectBase> m_EffectList = new List<EffectBase>();

        public EffectManager()
        {
        }

        public bool Init(BattleCore core)
        {
            m_BattleCore = core;
            return true;
        }
		
		//创建一个技能特效，此时还没有对资源进行加载
        public EffectBase CreateEffect(SkillBase owner, BattleEffectType type, string name, string lib)
        {
            EffectBase effect = null;
            switch (type)
            {
                case BattleEffectType.E_BATTLE_EFFECT_TYPE_SEQUENCE:
                    effect = new EffectSequence(owner, name, lib);
                    break;
                case BattleEffectType.E_BATTLE_EFFECT_TYPE_PARTICLE:
                    effect = new EffectParticle(owner, name, lib);
                    break;
                default:
                    break;
            }

            if (effect == null)
            {
                Debug.LogError("CreateEffect failed!");
            }

            if (!effect.Init())
            {
                Debug.LogError("load effect failed!");
            }

            m_EffectList.Add(effect);

            return effect;
        }

        public void DestroyEffect(EffectBase effect)
        {
            effect.Destroy();
            m_EffectList.Remove(effect);
        }

        public void OnDestroy()
        {
            foreach (EffectBase effect in m_EffectList)
            {
                effect.Destroy();
            }

            m_EffectList.Clear();

            Resources.UnloadUnusedAssets();
        }
    }
}

