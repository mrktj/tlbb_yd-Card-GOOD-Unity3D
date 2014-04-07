using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GCGame.Table;

namespace Games.Battle
{
    public sealed class SkillManager : Singleton<SkillManager>
    {
        private BattleCore m_BattleCore = null;
        private IList<SkillBase> m_SkillList = new List<SkillBase>();

        public SkillManager()
        {
        }

        public bool Init(BattleCore core)
        {
            m_BattleCore = core;
            return true;
        }

        public SkillBase CreateSkill(BattleCard owner, int skillid, SkillType type)
        {
            SkillBase skill_base = null;

            switch (type)
            {
                case SkillType.E_SKILL_TYPE_DIRECT:
                    skill_base = new SkillDirect(owner, skillid);
                    break;
                case SkillType.E_SKILL_TYPE_PROJECTILE:
                    skill_base = new SkillProjectile(owner, skillid);
                    break;
                case SkillType.E_SKILL_TYPE_STRETCH:
                    skill_base = new SkillStretch(owner, skillid);
                    break;
                case SkillType.E_SKILL_TYPE_COMBINE:
                    skill_base = new SkillCombine(owner, skillid);
                    break;
                default:
                    break;
            }
            
            if (skill_base != null)
            {
                skill_base.Init();
                m_SkillList.Add(skill_base);
            }
			else
			{
				Debug.LogError("CreateSkill() failed! skill id = " + skillid);
			}

            return skill_base;
        }

        public void DestroySkill(SkillBase skill)
        {
            if (skill == null)
            {
                return;
            }

            skill.Destory();

            m_SkillList.Remove(skill);
        }

        public void OnDestroy()
        {
            foreach (SkillBase skill in m_SkillList)
            {
                skill.Destory();
				//DestroySkill(skill);
            }
            m_SkillList.Clear();
            Resources.UnloadUnusedAssets();
            
        }
    }
}

