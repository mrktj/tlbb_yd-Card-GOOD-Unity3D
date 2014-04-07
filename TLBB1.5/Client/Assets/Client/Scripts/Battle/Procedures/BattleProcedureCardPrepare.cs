using UnityEngine;
using System.Collections;

namespace Games.Battle
{
    public class BattleProcedureCardPrepare : BattleProcedureBase
    {

        private BattleProcedureManager m_Manager;


        public override BattleProcedureType GetProcedureType()
        {
            return BattleProcedureType.E_BATTLE_PROCEDURE_CARD_PREPARE;
        }

        public override bool Init(BattleProcedureManager manager)
        {
            m_Manager = manager;
            return true;
        }
        public override void OnEnter()
        {

        }
        public override void OnLeave()
        {

        }
        public override void Update()
        {

        }
    }
}
