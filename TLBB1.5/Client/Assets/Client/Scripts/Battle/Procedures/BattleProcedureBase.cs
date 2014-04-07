using UnityEngine;
using System.Collections;

namespace Games.Battle
{
    public enum BattleProcedureType
    {
        E_BATTLE_PROCEDURE_RESOURCE_LOAD,
        E_BATTLE_PROCEDURE_BATTLE_START,
        E_BATTLE_PROCEDURE_CARD_PREPARE,
        E_BATTLE_PROCEDURE_CARD_FORWARD,
        E_BATTLE_PROCEDURE_CARD_MEET,
        E_BATTLE_PROCEDURE_SHOW_TIP,
        E_BATTLE_PROCEDURE_BATTLING,
        E_BATTLE_PROCEDURE_SHOW_TROPHY,
        E_BATTLE_PROCEDURE_WAITING,
        E_BATTLE_PROCEDURE_SHOW_RESULT,
        E_BATTLE_PROCEDURE_BATTLE_END,
		E_BATTLE_PROCEDURE_REVIVE_FREE,
		E_BATTLE_PROCEDURE_NONE,
    }


    public abstract class BattleProcedureBase
    {
        protected BattleProcedureBase()
        {
        }

        public abstract BattleProcedureType GetProcedureType();
        public abstract bool Init(BattleProcedureManager manager);
        public abstract void OnEnter();
        public abstract void OnLeave();
        public abstract void Update();
    }
}




