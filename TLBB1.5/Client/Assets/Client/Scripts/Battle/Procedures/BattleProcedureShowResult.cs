using UnityEngine;
using System.Collections;
using Games.CharacterLogic;

namespace Games.Battle
{
    public class BattleProcedureShowResult : BattleProcedureBase
    {
        private BattleProcedureManager m_Manager;

        private float m_DeltaTime = 0f;
        private float m_TotalTime = 2.2f;

        private GameObject m_AccelerateBtn = null;
        private GameObject m_AutoForwardBtn = null;
        private GameObject m_BattleResult = null;

        public override BattleProcedureType GetProcedureType()
        {
            return BattleProcedureType.E_BATTLE_PROCEDURE_SHOW_RESULT;
        }

        public override bool Init(BattleProcedureManager manager)
        {
            m_Manager = manager;

            m_AccelerateBtn = BattleUI.Instacne.accelerateBtn;
            m_AutoForwardBtn = BattleUI.Instacne.autoForwardBtn;
            m_BattleResult = BattleUI.Instacne.battleResult;

            return true;
        }
        public override void OnEnter()
        {
			m_DeltaTime = 0f;
			
            BattleCardManager.Instance.DestroyAllBuffs(BattleCardType.E_BATTLE_CARD_TYPE_SELF);
            Obj_MyselfPlayer.GetMe().RemoveFriendFromBattleArray();

            m_AccelerateBtn.SetActive(false);
            m_AutoForwardBtn.SetActive(false);

            Time.timeScale = 1f;
            m_Manager.GetBattlePlayer().GetBattleRoundData().isPlayed = true;

            if (!Obj_MyselfPlayer.GetMe().battleData.isWin)
            {
                //播放失败音效
                AudioManager.Instance.PlayEffectSound(SoundResource.SoundRes.battle_lose.ToString());
                m_BattleResult.SetActive(true);
                m_BattleResult.GetComponent<BattleResult>().ShowResult(false);
                BattleCardManager.Instance.DestroyCard(BattleCardType.E_BATTLE_CARD_TYPE_OTHER);
            }
            else
            {
                //播放胜利音效
                AudioManager.Instance.PlayEffectSound(SoundResource.SoundRes.battle_win.ToString());
                m_BattleResult.SetActive(true);
                m_BattleResult.GetComponent<BattleResult>().ShowResult(true);
            }
        }
        public override void OnLeave()
        {

        }
        public override void Update()
        {
            m_DeltaTime += Time.deltaTime;
            if (m_DeltaTime >= m_TotalTime)
            {
                m_Manager.ChangProcedure(BattleProcedureType.E_BATTLE_PROCEDURE_BATTLE_END);
            }
        }
    }
}

