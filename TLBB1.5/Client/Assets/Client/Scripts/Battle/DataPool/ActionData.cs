using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Games.Battle
{
    public class StepAction
    {
        public Int32 slotIndex;
        public Int32 skillID;
        public Int32 attackHp;
        public List<BuffInfo> buff;
        public Int32 harmType;
        public bool isStorm;
        public Int64 curHp;
        public int hetiIndex;

        public StepAction()
        {
            buff = new List<BuffInfo>();
            hetiIndex = -1;
        }
    }
    public class BattleStep
    {
        public List<StepAction> attacks;
        public List<StepAction> behits;
        public BattleStep()
        {
            attacks = new List<StepAction>();
            behits = new List<StepAction>();
        }
    }

    public class BattleTurn
    {
        public List<BattleStep> battleSteps;
        public BattleTurn()
        {
            battleSteps = new List<BattleStep>();
        }
    }

    public class BattleRound
    {
        public List<BattleTurn> battleTurns;
        public BattleRound()
        {
            battleTurns = new List<BattleTurn>();
        }
        public void Clear()
        {
            battleTurns.Clear();
        }
        public void AddTestData()
        {

            for (int turnCount = 0; turnCount < 1; turnCount++)
            {
                BattleTurn turn = new BattleTurn();
                for (int stepCount = 0; stepCount < 6; stepCount++)
                {
                    BattleStep step1 = new BattleStep();
                    StepAction attackaction1 = new StepAction();
                    attackaction1.slotIndex = stepCount;
                    attackaction1.skillID = 10000;

                    StepAction behitaction1 = new StepAction();
                    behitaction1.slotIndex = stepCount + 6;
                    behitaction1.attackHp = -1000;
                    step1.attacks.Add(attackaction1);
                    step1.behits.Add(behitaction1);


                    BattleStep step2 = new BattleStep();

                    StepAction attackaction2 = new StepAction();
                    attackaction2.slotIndex = stepCount + 6;
                    attackaction2.skillID = 10000;

                    StepAction behitaction2 = new StepAction();
                    behitaction2.slotIndex = stepCount;
                    behitaction2.attackHp = -1000;

                    step2.attacks.Add(attackaction2);
                    step2.behits.Add(behitaction2);

                    turn.battleSteps.Add(step1);
                    turn.battleSteps.Add(step2);
                }
                battleTurns.Add(turn);
            }
        }
    }

    public class ActionBag
    {
        public BattleCard attackCard;
        public AttackBag attackBag;
        public Dictionary<BattleCard, BeHitBag> behitMap;
        public ActionBag()
        {
            behitMap = new Dictionary<BattleCard, BeHitBag>();
        }
    }
    public class BeHitBag
    {
        public Int32 behitValue;

        public Int64 curHp;
        public bool isCrit;
        public List<BuffInfo> behitBuffs;

        public BeHitBag()
        {
            behitValue = 0;
            curHp = 0;
            isCrit = false;
            behitBuffs = new List<BuffInfo>();
        }
    }
    public class AttackBag
    {
        public Int32 skillID;

        public Int64 curHp;
        public List<BuffInfo> attackBuffs;
        public Int32 loverIdx;

        public AttackBag()
        {
            skillID = -1;
            curHp = 0;
            loverIdx = -1;
            attackBuffs = new List<BuffInfo>();
        }
    }
}
