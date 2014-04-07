using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using GCGame.Table;

namespace Games.Battle
{
    public enum BattleCardType
    {
        E_BATTLE_CARD_TYPE_SELF,
        E_BATTLE_CARD_TYPE_OTHER,
        E_BATTLE_CARD_TYPE_ALL,
    }

    public class BattleCard
    {
        private CardData m_CardData = null;
        public CardData Card_Data { get { return m_CardData; } }

        private long m_CurrentHp = 0;
        public long CurrentHp { get { return m_CurrentHp; } }
        private bool m_IsDead = false;
        public bool IsDead { get { return m_IsDead; } }

        private BattleSlot m_BattleSlot = null;
        public BattleSlot Battle_Slot { get{ return m_BattleSlot; } set{m_BattleSlot = value;} }

        private IList<BattleCard> m_TargetList = new List<BattleCard>();
        public IList<BattleCard> TargetList { get { return m_TargetList; } set { m_TargetList = value; } }

        private BattleCardType m_CardType = BattleCardType.E_BATTLE_CARD_TYPE_SELF;
        public BattleCardType Card_Type { get { return m_CardType; } }

        private CardStateMachine m_StateMachine = null;
        public CardStateMachine StateMachine { get { return m_StateMachine; } }

        public CardStateType GetCardState() { return m_StateMachine.CurrentState.GetStateType(); }

        private SkillBase m_SkillComm = null;
        public SkillBase SkillComm { get { return m_SkillComm; } }

        private SkillBase m_SkillVol = null;
        public SkillBase SkillVol { get { return m_SkillVol; } }

        private SkillBase m_SkillComb = null;
        public SkillBase SkillComb { get { return m_SkillComb; } }

        private Tab_Card m_TabCard = null;

        private Tab_Appearance m_TabAppear = null;

        private GameObject m_BattleCardObj = null;
        public GameObject BattleCardObj { set { m_BattleCardObj = value; } get { return m_BattleCardObj; } }

        private CardUI m_CardUI = null;
        public CardUI CardUI { get { return m_CardUI; } }

        private bool m_InAttackAction = false;
        public bool InAttackAction { get { return m_InAttackAction; } set { m_InAttackAction = true; } }

        private bool m_InBehitAction = false;
        public bool InBehitAction { get { return m_InBehitAction; } set { m_InBehitAction = value; } }

        private List<BuffBase> m_BuffList = new List<BuffBase>();
        public List<BuffBase> BuffList { get { return m_BuffList; } }

        private GameObject[] m_BuffPosArray = new GameObject[5];
        public GameObject[] BuffPosArray { get { return m_BuffPosArray; } }

        private ActionBag m_CurActionBag = null;
        public ActionBag CurActionBag { get { return m_CurActionBag; } }

        private SkillBase m_CurSkill = null;
        public SkillBase CurSkill { get { return m_CurSkill; } }

        public BattleCard(TroopMember member)
        {
			m_CardData = new CardData();
            m_CardData.CardGuid = member.guid;
            m_CardData.CardTempId = member.cardID;
            m_CardData.TotalHp = member.initHp;
            m_CardData.MemberData = member;
            m_CurrentHp = member.initHp;  

            if (member.state == 1)
            {
                m_IsDead = true;
            }

            if (0 <= member.slotIndex && member.slotIndex <= 5)
            {
                m_CardType = BattleCardType.E_BATTLE_CARD_TYPE_SELF;
                m_BattleSlot = BattleCardManager.Instance.BattleSlotArray[BattleCardType.E_BATTLE_CARD_TYPE_SELF][member.slotIndex];
            }
            else
            {
                m_CardType = BattleCardType.E_BATTLE_CARD_TYPE_OTHER;
                m_BattleSlot = BattleCardManager.Instance.BattleSlotArray[BattleCardType.E_BATTLE_CARD_TYPE_OTHER][member.slotIndex - 6];
            }
			m_BattleSlot.SetCard(this);
			m_BattleSlot.SetTotalHp(member.initHp);
			m_BattleSlot.SetCurHp(member.initHp);
			
            m_StateMachine = new CardStateMachine(this);
        }

        public bool Init()
        {
            m_TabCard = TableManager.GetCardByID(m_CardData.CardTempId);
            if (m_TabCard != null)
            {
                m_TabAppear = TableManager.GetAppearanceByID(m_TabCard.Appearance);
            }
            m_BattleCardObj = GameObject.Instantiate(BattleUI.Instacne.battleCardPrefab) as GameObject;
            m_BattleCardObj.name = "BattleCard-" + m_BattleSlot.SlotIndex;
            ResetPosition();
            m_CardUI = m_BattleCardObj.GetComponent<CardUI>();
			m_CardUI.Owner = this;
            m_CardUI.SetCardTemplateID(m_CardData.CardTempId);
            m_BuffPosArray = m_CardUI.m_BuffPosArray;

            if (!m_StateMachine.Init())
            {
                Debug.Log("m_StateMachine.Init()");
            }

            Tab_Skill tab_skill1 = TableManager.GetSkillByID(m_CardData.MemberData.commSkillID);
            Tab_Skill tab_skill2 = TableManager.GetSkillByID(m_CardData.MemberData.volSkillID);
            Tab_Skill tab_skill3 = TableManager.GetSkillByID(m_CardData.MemberData.combSkillID);
            if (tab_skill1 != null)
            {
                SkillType type = (SkillType)(tab_skill1.SkillType);
                m_SkillComm = SkillManager.Instance.CreateSkill(this, m_CardData.MemberData.commSkillID, type);
            }

            if (tab_skill2 != null)
            {
                SkillType type = (SkillType)(tab_skill2.SkillType);
                m_SkillVol = SkillManager.Instance.CreateSkill(this, m_CardData.MemberData.volSkillID, type);
            }

            if (tab_skill3 != null)
            {
                SkillType type = (SkillType)(tab_skill3.SkillType);
                m_SkillComb = SkillManager.Instance.CreateSkill(this, m_CardData.MemberData.combSkillID, type);
            }

            return true;
        }

        public bool PreLoad()
        {
            if (m_SkillComm != null)
            {
                if (!m_SkillComm.LoadSkill())
                {
                    Debug.LogError(" LoadSkill failed");
                }
            }

            if (m_SkillVol != null)
            {
                if (!m_SkillVol.LoadSkill())
                {
                    Debug.LogError(" LoadSkill failed");
                }
            }

            if (m_SkillComb != null)
            {
                if (!m_SkillComb.LoadSkill())
                {
                    Debug.LogError(" LoadSkill failed");
                }
            }

            return true;
        }

        public void Reset()
        {
            m_CurrentHp = m_CardData.TotalHp;
            m_BattleSlot.Reset();

            m_BattleCardObj.SetActive(true);
            //m_BattleCardObj.GetComponent<CardUI>().SetCardTemplateID(m_CardData.CardTempId);
            ResetPosition();
            ChangeState(CardStateType.E_CARD_STATE_NORMAL);
        }

        private void ResetPosition()
        {
            //普通卡牌,slot fram 距slot hp:100
            //boss卡牌,slot fram 距slot hp:150
            float h = 1f;//Screen.height/1024f;//这里获取的屏幕高度不对--
            //float w = 1f;//Screen.width/768f;

            m_BattleCardObj.transform.parent = m_BattleSlot.SlotBorder.transform.parent;
            Vector3 pos = new Vector3(0.0f, 0.0f, -5.0f);
            pos.x = m_BattleSlot.SlotBorder.transform.localPosition.x;
            pos.y = m_BattleSlot.SlotBorder.transform.localPosition.y; //+ 12*h;
            Vector3 slot = Vector3.zero;
            slot.x = m_BattleSlot.SlotBorder.transform.localPosition.x;
            slot.y = m_BattleSlot.SlotBorder.transform.localPosition.y - 100 * h + 12 * h;
            Vector3 slot_bg = m_BattleSlot.SlotBg.transform.localPosition;
            Vector3 slot_name = m_BattleSlot.SlotName.transform.localPosition;

            if (Convert.ToBoolean(m_TabCard.IsBoss))
            {
                slot.x = m_BattleSlot.SlotBorder.transform.localPosition.x;
                slot.y = m_BattleSlot.SlotBorder.transform.localPosition.y - 100 * h + 4 * h;

                m_BattleCardObj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                m_BattleSlot.HpBar.transform.localScale = new Vector3(1.5f, 1f, 1f);
                m_BattleSlot.SlotBg.transform.localScale = new Vector3(225f, 300f, 1f);
                if (m_BattleSlot.SlotIndex >= 6 && m_BattleSlot.SlotIndex <= 8)//前排--
                {
                    pos.y += 50.0f * h;
                    slot.y += 9f * h;
                    slot_bg.y += 56f * h;
                    slot_name.y += 5f * h;
                }
                else if (m_BattleSlot.SlotIndex >= 9 && m_BattleSlot.SlotIndex <= 11)//后排--
                {
                    pos.y -= 30.0f * h;
                    slot.y -= 71 * h;
                    slot_name.y -= 81 * h;
                    slot_bg.y -= 34 * h;
                }
            }
            else
            {
                m_BattleCardObj.transform.localScale = new Vector3(0.63f, 0.63f, 0.63f);
                m_BattleSlot.HpBar.transform.localScale = new Vector3(1f, 1f, 1f);
            }

            m_BattleCardObj.transform.localPosition = pos;
            m_BattleSlot.HpBar.transform.localPosition = slot;
            m_BattleSlot.SlotName.transform.localPosition = slot_name;
            m_BattleSlot.SlotBg.transform.localPosition = slot_bg;
        }

        public void ChangeState(CardStateType type)
        {
            if (m_StateMachine != null)
            {
                m_StateMachine.ChangeState(type);
            }
        }

        //攻击--
        public void DoAttack(ActionBag bag)
        {
            Debug.LogWarning("DoAttack(), SlotIndex: " + m_BattleSlot.SlotIndex + ", skillID = " + bag.attackBag.skillID);
            m_CurrentHp = bag.attackBag.curHp;

            //在攻击前看受击对象里面有没有反伤BUFF，如果有，把BUFF的值加上，在受击对象受击时再减去--
            foreach (KeyValuePair<BattleCard, BeHitBag> kvp in bag.behitMap)
            {
                foreach(BuffInfo buff_info in kvp.Value.behitBuffs)
                {
                    if (buff_info.buf_id == (int)BattleBuffType.E_BATTLE_BUFF_TYPE_FAN_SHANG)
                    {
                        m_CurrentHp += buff_info.buf_value;
                    }
                }
            }

            SkillBase temp_skill = null;
            if (m_SkillComm != null && bag.attackBag.skillID == m_SkillComm.SkillId)
            {
                temp_skill = m_SkillComm;
            }
            else if (m_SkillVol != null && bag.attackBag.skillID == m_SkillVol.SkillId)
            {
                temp_skill = m_SkillVol;
            }
            else if (SkillComb != null && bag.attackBag.skillID == SkillComb.SkillId)
            {
                temp_skill = SkillComb;
                Debug.Log("loverIdx = " + bag.attackBag.loverIdx);
                BattleCard lover = BattleCardManager.Instance.FindCardBySlotIndex(bag.attackBag.loverIdx);
                if (lover != null)
                {
                    ((SkillCombine)SkillComb).LoverTempID = lover.Card_Data.CardTempId;
                }
                else
                {
                    Debug.LogError("Cant find lover, lover_index = " + bag.attackBag.loverIdx + ", heti skillid = " + bag.attackBag.skillID);
                }
            }
            else
            {
                Debug.LogError("DoAttack(), card has no such skill, cardid = " + m_CardData.CardTempId + ", slot_index = " + m_BattleSlot.SlotIndex + ", skillid = " + bag.attackBag.skillID);
            }

            if (temp_skill == null)
            {
                Debug.LogError("card has no such skill, can not play !");
                return;
            }

            //先显示Buff
            UpdateBuff(bag.attackBag.attackBuffs);
            ShowBuffValue(true);

            //如果技能没有目标，返回
            if ( !temp_skill.SetTarget(bag.behitMap))
            {
                Debug.Log("temp_skill.SetTarget(), return false! no target card!");
                //流血buff在攻击动作结束后结算--
                ShowBuffValue(false);

                //在出手时可能会有中毒Buff杀掉自己d--
                if (m_CurrentHp <= 0)
                {
                    OnDead();
                }

                return;
            }

            Dictionary<BattleCard, BeHitBag>.KeyCollection keyCol = bag.behitMap.Keys;
            foreach (BattleCard card in keyCol)
            {
                card.InBehitAction = true;
            }

            m_CurActionBag = bag;
            m_CurSkill = temp_skill;
            ChangeState(CardStateType.E_CARD_STATE_ATTACK);
        }

        //受击--
        public void OnHit(BattleCard attacker, BeHitBag bag)
        {
            Debug.LogWarning("OnHit(), SlotIndex: " + m_BattleSlot.SlotIndex + ", TotalHP: " + m_CardData.TotalHp + ", CurHp: " + bag.curHp + ", attackHp: " + bag.behitValue);
            m_CurrentHp = bag.curHp;

            m_InBehitAction = false;

            if (m_CurrentHp <= 0)
            {
                OnDead();
                return;
            }

            UpdateBuff(bag.behitBuffs);

            //如果卡牌身上有反击BUFF，在受击时给攻击者造成伤害--
            foreach (BuffBase bf in m_BuffList)
            {
                if (bf.TabBfEffect.BuffType == (int)BattleBuffType.E_BATTLE_BUFF_TYPE_FAN_SHANG)
                {
                    bf.ShowBuffValue(attacker.BattleCardObj);
                }
            }
        }

        //死亡--
        public void OnDead()
        {
            m_IsDead = true;
            m_StateMachine.ChangeState(CardStateType.E_CARD_STATE_DEAD);
        }

        //复活--
        public void OnRevive()
        {
            m_IsDead = false;
        }
		
		public void Revive()
		{
			if(m_IsDead)
			{
				m_CardUI.ShowReviveAnimation();
			}
		}

        //BUFF相关，更新Buff--
        public void UpdateBuff(List<BuffInfo> infoes)
        {
            foreach (BuffInfo info in infoes)
            {
                AddBuff(info);
            }

            List<BuffBase> temp_buffs = new List<BuffBase>();
            foreach (BuffBase bf in m_BuffList)
            {
                BuffInfo temp = infoes.Find(delegate(BuffInfo info) { return info.buf_id == bf.BuffId; });
                if (temp == null) temp_buffs.Add(bf);
            }
            foreach (BuffBase bf in temp_buffs)
            {
                RemoveBuff(bf);
            }

        }

        //显示攻击前结算的buff value,//显示攻击后结算的buff value
        public void ShowBuffValue(bool before_att)
        {
            if (before_att)
            {
                foreach (BuffBase bf in m_BuffList)
                {
                    if (bf.TabBfEffect.BuffType == (int)BattleBuffType.E_BATTLE_BUFF_TYPE_JIA_XIE
                        || bf.TabBfEffect.BuffType == (int)BattleBuffType.E_BATTLE_BUFF_TYPE_LIU_XIE)
                    {
                        bf.ShowBuffValue();
                        m_BattleSlot.SetCurHp(m_CurrentHp, true);
                    }
                }
            }
            else
            {
                foreach (BuffBase bf in m_BuffList)
                {
                    if (bf.TabBfEffect.BuffType == (int)BattleBuffType.E_BATTLE_BUFF_TYPE_XI_XIE)
                    {
                        bf.ShowBuffValue();
                        m_BattleSlot.SetCurHp(m_CurrentHp, true);
                    }
                }
            }
        }

        public void AddBuff(BuffInfo info)
        {
            BuffBase bf = m_BuffList.Find(delegate(BuffBase buf) { return buf.BuffId == info.buf_id; });
            if (bf == null)
            {
                bf = BuffManager.Instance.CreateBuff(this, info.buf_id, info.buf_value);
                if (bf != null)
                {
                    m_BuffList.Add(bf);
                }
            }
            else
            {
                bf.BuffValue = info.buf_value;
            }
        }
        private void RemoveBuff(BuffBase bf)
        {
            if (!m_BuffList.Contains(bf))
            {
                return;
            }

            m_BuffList.Remove(bf);
            BuffManager.Instance.DestroyBuff(bf);

            //更新其它BUF的位置
            List<BuffBase> temp_buffs = new List<BuffBase>();
            foreach (BuffBase buff_base in m_BuffList)
            {
                if (buff_base.TabBfEffect.EffectPos == (int)BuffPostionType.E_BUFF_POSTION_TYPE_LEFT)
                {
                    temp_buffs.Add(buff_base);
                }
            }

            for (int i = 0; i < temp_buffs.Count && i < m_BuffPosArray.Length; i++ )
            {
                temp_buffs[i].BuffObj.transform.localPosition = m_BuffPosArray[i].transform.localPosition;
            }
        }

        public void RemoveAllBuff()
        {
            foreach (BuffBase bf in m_BuffList)
            {
                BuffManager.Instance.DestroyBuff(bf);

                //更新其它BUF的位置
                List<BuffBase> temp_buffs = new List<BuffBase>();
                foreach (BuffBase buff_base in m_BuffList)
                {
                    if (buff_base.TabBfEffect.EffectPos == (int)BuffPostionType.E_BUFF_POSTION_TYPE_LEFT)
                    {
                        temp_buffs.Add(buff_base);
                    }
                }

                for (int i = 0; i < temp_buffs.Count && i < m_BuffPosArray.Length; i++)
                {
                    temp_buffs[i].BuffObj.transform.localPosition = m_BuffPosArray[i].transform.localPosition;
                }
            }

            m_BuffList.Clear();
        }

  
        public void AnimateBeHit(BeHitBag bag)
        {
            m_BattleSlot.SetCurHp(bag.curHp, true);
            if (bag.behitValue > 0)
            {
                return;
            }
			
            if (m_CardType == BattleCardType.E_BATTLE_CARD_TYPE_SELF)
            {
                if (!m_BattleCardObj.animation.IsPlaying("CardBehit_Self"))
                {
                    m_BattleCardObj.animation.Play("CardBehit_Self", PlayMode.StopAll);
                }
            }
            else
            {
                if (!m_BattleCardObj.animation.IsPlaying("CardBehit_Other_2"))
                {
                    m_BattleCardObj.animation.Play("CardBehit_Other_2", PlayMode.StopAll);
                }
            }
        }
        public void OnAnimateBeHitCompleted(GameObject cardobj)
        {

        }
        public void StopAnimation()
        {
            if (m_BattleCardObj != null)
            {
                ResetPosition();
            }
        }

        public void WaitingCmd()
        {
            ChangeState(CardStateType.E_CARD_STATE_WAITING);
        }

        public void OnChangeSlot(BattleSlot slot)
        {
            m_BattleSlot = slot;
            m_BattleSlot.SetTotalHp(m_CardData.TotalHp);
            m_BattleCardObj.SetActive(true);
            m_BattleCardObj.GetComponent<CardUI>().SetCardTemplateID(m_CardData.CardTempId);
            ResetPosition();
            m_BattleCardObj.GetComponent<DraggableCard>().OnChangeSlot(slot.SlotIndex);
        }

        //血条和血量数字--
        public void ShowHitValue(BeHitBag bag, SkillBase skill, bool isBuff = false)
        {
            //如果用的技能逻辑ID不是0（伤害），而且attack value 是0，则不显示闪避
            if (!isBuff && bag.behitValue == 0 && skill != null && skill.SkillTab.LogicId != 0)
            {
                return;
            }
            
            int hp = bag.behitValue;
			//计算位置坐标
			Vector3 pos = new Vector3(0.0f, 0.0f, -25.0f);
            pos.x = m_BattleCardObj.transform.localPosition.x;
            pos.y = m_BattleCardObj.transform.localPosition.y;
			if (isBuff)
            {
                pos.y += 10;
            }
			//计算local缩放
			Vector3 scale;
			if (bag.isCrit)
            {
                scale = Vector3.one * 1.5f;
            }
			else
			{
				scale = Vector3.one;
			}
            
            tk2dTextMesh hitText = null;
			
            if (hp < 0)
            {
				hitText = CreateHitText(BattleUI.Instacne.redText,pos,scale);
                hitText.text = "" + hp;
            }
            else if (hp == 0)
            {
				hitText = CreateHitText(BattleUI.Instacne.greenText,pos,scale);
                hitText.text = ".";
            }
            else
            {
				hitText = CreateHitText(BattleUI.Instacne.greenText,pos,scale);
                hitText.text = "+" + hp;
            }
            hitText.Commit();
            hitText.animation.Play();
        }
		
		public tk2dTextMesh CreateHitText(GameObject textParent,Vector3 position,Vector3 scale)
		{
			GameObject _textP = GameObject.Instantiate(textParent) as GameObject;
			_textP.transform.parent = m_BattleSlot.SlotBorder.transform.parent;
			_textP.transform.localPosition = position;
			_textP.transform.localScale = scale;
			return _textP.GetComponentInChildren<tk2dTextMesh>();
		}
		
        public void Destroy()
        {
            Debug.Log("BattleCard.Destroy(), curHp: " + m_CurrentHp + ", slot index = " + m_BattleSlot.SlotIndex);

            RemoveAllBuff();

            SkillManager.Instance.DestroySkill(m_SkillComm);
            SkillManager.Instance.DestroySkill(m_SkillVol);
            SkillManager.Instance.DestroySkill(m_SkillComb);

            m_BattleSlot.SetHpBarVisible(false);
            m_BattleSlot.SetSlotFrameVisible(false);

            GameObject.Destroy(m_BattleCardObj);
        }
    }
}
