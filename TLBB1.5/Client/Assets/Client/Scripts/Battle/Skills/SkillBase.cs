using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GCGame.Table;

namespace Games.Battle
{
    public enum SkillType
    {
        E_SKILL_TYPE_DIRECT,
        E_SKILL_TYPE_PROJECTILE,
        E_SKILL_TYPE_STRETCH,
        E_SKILL_TYPE_COMBINE,
        E_SKILL_TYPE_ONCE,
    }

    public class SkillBase
    {
        protected int m_SkillId;
        public int SkillId { get{ return m_SkillId; } }

        protected SkillType m_SkillType = SkillType.E_SKILL_TYPE_DIRECT;
        protected BattleCard m_SkillOwner;

        protected IList<BattleCard> m_TargetList = new List<BattleCard>();

        protected IDictionary<BattleCard, BeHitBag> m_BehitMap = new Dictionary<BattleCard, BeHitBag>();

        protected Transform m_BattleRoot;

        protected Tab_Skill m_SkillTab;
        public Tab_Skill SkillTab { get { return m_SkillTab; } }

        protected Tab_SkillDisplay m_SkillDisplayTab;
        public Tab_SkillDisplay SkillDisplayTab { get { return m_SkillDisplayTab; } }

        public SkillBase()
        {

        }

        public virtual bool Init() { return false; }

        public virtual bool LoadSkill() { return false; }

        public bool SetTarget(Dictionary<BattleCard, BeHitBag> targets)
        {
            m_BehitMap.Clear();
            m_TargetList.Clear();

            m_BehitMap = targets;
            Dictionary<BattleCard, BeHitBag>.KeyCollection keyCol = targets.Keys;
            foreach (BattleCard card in keyCol)
            {
                if (card.GetCardState() != CardStateType.E_CARD_STATE_DEAD)
                {
                    m_TargetList.Add(card);
                }
                else
                {
                    Debug.LogWarning("SetTarget(), target has dead! slotindex: " + card.Battle_Slot.SlotIndex);
                }
            }
            if (m_TargetList.Count == 0)
            {
                return false;//可以没有目标，有Buff
            }
            int i = 0;
            if (i == 1)
            {
                return false;
            }
            return true;
        }

        public virtual void UseSkill() { }

        public void ShowSkillName() 
        {
            if (string.IsNullOrEmpty(m_SkillDisplayTab.SkillName) || m_SkillDisplayTab.SkillName == "-1")
            {
                return;
            }

            GameObject go = Resources.Load("Effect/Skill_name", typeof(GameObject)) as GameObject;
            GameObject text_go = GameObject.Instantiate(go) as GameObject;
            text_go.transform.parent = m_SkillOwner.BattleCardObj.transform;
            text_go.transform.localPosition = new Vector3(0f, 20f, -40f);
            text_go.transform.localScale = new Vector3(2f, 2f, 1f);

            text_go.transform.FindChild("Text").gameObject.GetComponent<UISprite>().spriteName = m_SkillDisplayTab.SkillName;
            text_go.transform.FindChild("Text").gameObject.GetComponent<UISprite>().MakePixelPerfect();
            text_go.animation.Play();
        }

        public virtual void Destory()
        {

        }
    }
}

