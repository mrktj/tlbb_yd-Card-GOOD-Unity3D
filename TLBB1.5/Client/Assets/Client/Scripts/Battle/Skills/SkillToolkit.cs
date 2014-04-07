using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Games.Battle
{
    public enum ToolKitEventType
    {
        TOOLKIT_ANIMATION_STRETCH = 0,
        TOOLKIT_ANIMATION_NEXT = 1001,
    }

    public class SkillToolkit : SkillBase
    {
        protected bool m_IsAttackEnd = false;

        protected tk2dSpriteAnimation m_AttackAnimLib;

        protected tk2dSpriteAnimation m_ProjectileAnimLib;

        protected tk2dSpriteAnimation m_BehitAnimLib;

        protected tk2dAnimatedSprite m_AttakcSprite;

        protected IDictionary<tk2dAnimatedSprite, BattleCard> m_ProjectileSpriteMap = new Dictionary<tk2dAnimatedSprite, BattleCard>();

        protected IDictionary<tk2dAnimatedSprite, BattleCard> m_BehitSpriteMap = new Dictionary<tk2dAnimatedSprite, BattleCard>();

        public override void Destory()
        {
            m_AttakcSprite = null;
            m_ProjectileSpriteMap.Clear();
            m_BehitSpriteMap.Clear();
            m_TargetList.Clear();
            m_BehitMap.Clear();

            if (m_AttackAnimLib)
            {
                //GameObject.DestroyImmediate(m_AttackAnimLib.gameObject, true);
                m_AttackAnimLib = null;
            }
            if (m_ProjectileAnimLib)
            {
                //GameObject.DestroyImmediate(m_ProjectileAnimLib.gameObject, true);
                m_ProjectileAnimLib = null;
            }
            if (m_BehitAnimLib)
            {
                //GameObject.DestroyImmediate(m_BehitAnimLib.gameObject, true);
                m_BehitAnimLib = null;
            }
        }
    }
}
