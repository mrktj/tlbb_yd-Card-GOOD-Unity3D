using UnityEngine;
using System.Collections;
using Games.CharacterLogic;

namespace Games.Battle
{
    public class BattleProcedureCardForward : BattleProcedureBase
    {
        private BattleProcedureManager m_Manager;

        private float m_DeltaTime = 0f;
        private float m_TotalTime = 2.1f;
        private UISprite m_ForwardSprite = null;
        private BattleBackground m_BattleBg = null;

        public override BattleProcedureType GetProcedureType()
        {
            return BattleProcedureType.E_BATTLE_PROCEDURE_CARD_FORWARD;
        }

        public override bool Init(BattleProcedureManager manager)
        {
            m_Manager = manager;

            m_ForwardSprite = BattleUI.Instacne.forwardSprite;
            m_BattleBg = BattleUI.Instacne.battleBg;

            return true;
        }

        public override void OnEnter()
        {
			m_DeltaTime = 0f;
			
            BattleCardManager.Instance.StartMoveCard(BattleCardType.E_BATTLE_CARD_TYPE_SELF);

            AudioManager.Instance.PlayEffectSound(SoundResource.SoundRes.battle_go.ToString(), true, 0, Obj_MyselfPlayer.GetMe().acceleration);
            m_ForwardSprite.gameObject.SetActive(false);
        }

        public override void OnLeave()
        {
            BattleCardManager.Instance.StopMoveCard(BattleCardType.E_BATTLE_CARD_TYPE_SELF);
            AudioManager.Instance.StopSound(SoundResource.SoundRes.battle_go.ToString());
        }

        public override void Update()
        {
            m_DeltaTime += Time.deltaTime;
            if (m_DeltaTime >= m_TotalTime)
            {
                m_Manager.ChangProcedure(BattleProcedureType.E_BATTLE_PROCEDURE_CARD_MEET);
            }

            m_BattleBg.Move(Time.deltaTime);
        }
    }
}
