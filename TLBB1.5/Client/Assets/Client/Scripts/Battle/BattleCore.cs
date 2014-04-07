using UnityEngine;
using System.Collections;

namespace Games.Battle
{
	public enum BattleType
	{
		PVE,
		PVP,
		QxzbPvP,
		WORLD_BOSS,
		CHONG_LOU,
	}
	
    public class BattleCore
    {
        private BattlePlayer m_BattlePlayer;
        private BattleUI m_BattleUI;
        private BattleCardManager m_BattleCardManager = null;
        private SkillManager m_SkillManager = null;
        private BuffManager m_BuffManager = null;
        private EffectManager m_EffectManager = null;

        public BattleCore()
        {
            m_BattleUI = GameObject.Find("BattleLogic").GetComponent<BattleUI>();

            m_BattlePlayer = new BattlePlayer(this);
            m_BattleCardManager = BattleCardManager.Instance;
            m_SkillManager = SkillManager.Instance;
            m_BuffManager = BuffManager.Instance;
            m_EffectManager = EffectManager.Instance;

            if (!m_BattlePlayer.Init())
            {
                Debug.LogError("战斗播放器初始化失败！");
            }

            if (!m_BattleCardManager.Init(this))
            {
                Debug.LogError("m_BattleCardManager初始化错误！");
            }

            if (!m_SkillManager.Init(this))
            {
                Debug.LogError("m_SkillManager初始化错误！");
            }

            if (!m_EffectManager.Init(this))
            {
                Debug.LogError("m_EffectManager初始化错误！");
            }
        }

        public BattlePlayer GetBattlePlayer()
        {
            return m_BattlePlayer;
        }

        public BattleUI GetBattleUI()
        {
            return m_BattleUI;
        }

        public void Update()
        {
            m_BattlePlayer.Update();
        }

        public void OnDestroy()
        {
            BattleCardManager.Instance.OnDestroy();
            BattleCardManager.Destroy();

            SkillManager.Instance.OnDestroy();
            SkillManager.Destroy();

            BuffManager.Instance.OnDestroy();
            BuffManager.Destroy();

            EffectManager.Instance.OnDestroy();
            EffectManager.Destroy();

            m_BattlePlayer.OnDestroy();
            m_BattlePlayer = null;

            Debug.Log("BattleCore.OnDestroy()!");
        }
    }
}

