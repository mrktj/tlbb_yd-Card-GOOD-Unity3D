using UnityEngine;
using System.Collections;

namespace Games.Battle
{
    public enum BattleEffectType
    {
        E_BATTLE_EFFECT_TYPE_SEQUENCE   = 0,
        E_BATTLE_EFFECT_TYPE_PARTICLE   = 1,
    }
	
	//特效基类
    public class EffectBase
    {
        protected BattleEffectType m_EffectType;	//特效类型	序列帧 和 粒子
        protected SkillBase m_EffectOwner;			//特效所有者
        protected string m_EffectName;				//特效名称
        protected string m_LibName;					//特效库
        protected BattleCard m_TargetCard;			//目标卡牌

        protected GameObject m_EffectObj;
        protected Transform m_StartPos;
        public Transform StartPos { set { m_StartPos = value; } }
        protected Transform m_EndPos;
        public Transform EndPos { set { m_EndPos = value; } }
        protected Vector3 m_DirectionPoint;
        public Vector3 DirectionPoint { set { m_DirectionPoint = value; } }

        protected float m_MoveSpeed = 0f;
        public float MoveSpeed { set { m_MoveSpeed = value; } }

        public delegate void EffectStartEventHandler(EffectBase effect, BattleCard target);
        public delegate void EffectEndEventHandler(EffectBase effect, BattleCard target, float total_time);
        public delegate void EffectUpdateEventHandler(EffectBase effect, BattleCard target, float total_time, float cur_time);

        public EffectStartEventHandler EffectStart;
        public EffectEndEventHandler EffectEnd;
        public EffectUpdateEventHandler EffectUpdate;

        protected virtual void OnEffectStart(EffectBase effect) { }
        protected virtual void OnEffectEnd(EffectBase effect, float total_time) { }
        protected virtual void OnEffectUpdate(EffectBase effect, float cur_time) { }

        public virtual bool Init() { return false; }
        public virtual bool Load() { return false; }

        public virtual bool Play(BattleCard target) { return false; }
        public virtual void Stop() {  }
        public virtual void SetVisible(bool visible) { }

        public virtual EffectBase Duplicate() { return null; }
        public virtual void Update() { }
        public virtual void Destroy() { }
    }
}

