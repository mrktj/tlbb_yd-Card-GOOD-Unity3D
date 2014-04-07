using UnityEngine;
using System.Collections;
using GCGame.Table;
using Games.LogicObject;

namespace Games.Battle
{
    public class BattleSlot
    {
        private int m_SlotIndex = -1;
        public int SlotIndex { get { return m_SlotIndex; } }

        private BattleCard m_BattleCard = null;
        public BattleCard Battle_Card { get { return m_BattleCard; } }

        private long m_TotalHp = 0;

        private long m_CurHp = 0;

        private UISlider m_HpBar;
        public UISlider HpBar { get { return m_HpBar; } }

        private UISprite m_SlotBg;
        public UISprite SlotBg { get { return m_SlotBg; } }

        private UILabel m_SlotName;
        public UILabel SlotName { get { return m_SlotName; } }

        private UISprite m_SlotBorder;
        public UISprite SlotBorder { get { return m_SlotBorder;}}

        public BattleSlot(int index)
        {
            m_SlotIndex = index;
            m_BattleCard = null;
            if (0 <= index && index <= 5)
            {
                m_HpBar = BattleUI.Instacne.selfSlotHp[index];
                m_SlotBg = BattleUI.Instacne.selfSlotBgs[index];
                m_SlotName = BattleUI.Instacne.selfSlotNames[index];
                m_SlotBorder = BattleUI.Instacne.selfSlotSprites[index];
            }
            else if (6 <= index && index <= 11)
            {
                m_HpBar = BattleUI.Instacne.otherSlotHp[index - 6];
                m_SlotBg = BattleUI.Instacne.otherSlotBgs[index - 6];
                m_SlotName = BattleUI.Instacne.otherSlotNames[index - 6];
                m_SlotBorder = BattleUI.Instacne.otherSlotSprites[index - 6];
            }

            m_HpBar.gameObject.AddComponent<BattleHpBar>();
            SetHpBarVisible(false);
            SetSlotFrameVisible(false);
            m_HpBar.sliderValue = 1.0f;
        }

        public void Reset()
		{
			m_CurHp = m_TotalHp;
			m_HpBar.sliderValue = 1.0f;
			SetHpBarVisible(false);
            SetSlotFrameVisible(false);
			m_HpBar.GetComponent<BattleHpBar>().Reset();
		}
        public void SetCard(BattleCard cd)
        {
            m_BattleCard = cd;
            int name_id = TableManager.GetCardByID(cd.Card_Data.CardTempId).Appearance;
            m_SlotName.text = LanguageManger.GetWords(TableManager.GetAppearanceByID(name_id).Name);
        }
		public void SetTotalHp(long hp)
		{
			m_CurHp = m_TotalHp = hp;
		}
		public void SetCurHp(long hp, bool animte = false)
		{
			m_CurHp = hp;
			float percentHp = (float)((double)m_CurHp / (double)m_TotalHp);
			if(percentHp < 0.05f && percentHp > 0f && m_TotalHp < 1E6)
			{
				percentHp = 0.05f;
			}
            m_HpBar.GetComponent<BattleHpBar>().ShowHp(percentHp, animte);
		}
		public void OnChangeCard(BattleCard bcard)
		{
			m_BattleCard = bcard;
		}
		public void SetHpBarVisible(bool isVisible)
		{
			if(m_HpBar == null)
			{
				return;
			}
			if(isVisible)
			{
				if(m_HpBar && !m_HpBar.gameObject.activeSelf) m_HpBar.gameObject.SetActive(true);
			}
			else
			{
                if (m_HpBar && m_HpBar.gameObject.activeSelf) m_HpBar.gameObject.SetActive(false);
			}
		}
		public void SetSlotFrameVisible(bool isVisible)
		{
			if(isVisible)
			{
				if(m_SlotBorder && !m_SlotBorder.gameObject.activeSelf) m_SlotBorder.gameObject.SetActive(true);
			}
			else
			{
                if (m_SlotBorder && m_SlotBorder.gameObject.activeSelf) m_SlotBorder.gameObject.SetActive(false);
			}
		}

//         private void SetSlotBgVisible(bool isVisible)
//         {
//             if (isVisible) 
//             {
//                 if(m_SlotBg && !m_SlotBg.gameObject.activeSelf) m_SlotBg.gameObject.SetActive(true);
//             }
//             else
//             {
//                 if (m_SlotBg && m_SlotBg.gameObject.activeSelf) m_SlotBg.gameObject.SetActive(false);
//             }
//         }
// 
//         private void SetSlotNameVisible(bool isVisible)
//         {
//             if (isVisible)
//             {
//                 m_SlotName.gameObject.SetActive(true);
// 				int name_id = TableManager.GetCardByID(m_BattleCard.Card_Data.CardTempId).Appearance;
//                 if (m_SlotName && !m_SlotName.gameObject.activeSelf) m_SlotName.text = LanguageManger.GetWords(TableManager.GetAppearanceByID(name_id).Name);
//             }
//             else
//             {
//                 if (m_SlotName && m_SlotName.gameObject.activeSelf) m_SlotName.gameObject.SetActive(false);
//             }
//         }

	}
}


