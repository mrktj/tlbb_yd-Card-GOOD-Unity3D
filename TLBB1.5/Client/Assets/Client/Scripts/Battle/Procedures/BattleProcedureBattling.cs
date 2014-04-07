using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Games.CharacterLogic;
using Games.LogicObject;

namespace Games.Battle
{
    public class BattleProcedureBattling : BattleProcedureBase
    {
        private BattleProcedureManager m_Manager;
        private int m_TurnCounter = 0;
        public int TurnCounter { get { return m_TurnCounter; } }
        private int m_StepCounter = 0;
        private BattleRoundData m_BattleRoundData = null;
        public Dictionary<int, List<ActionBag>> m_BattleActionBagList = new Dictionary<int, List<ActionBag>>();

        private BattleCard m_CurAttackCard = null;
        private ActionBag m_CurActionBag = null;

        public override BattleProcedureType GetProcedureType()
        {
            return BattleProcedureType.E_BATTLE_PROCEDURE_BATTLING;
        }

        public override bool Init(BattleProcedureManager manager)
        {
            m_Manager = manager;
            return true;
        }
        public override void OnEnter()
        {
            m_TurnCounter = 0;
            m_StepCounter = 0;
            m_BattleRoundData = m_Manager.GetBattlePlayer().GetBattleRoundData();
            //m_BattleActionBagList = m_Manager.GetBattlePlayer().BattleReportReader(m_BattleRoundData);
            m_CurAttackCard = null;
            m_CurActionBag = null;
            Obj_MyselfPlayer.GetMe().reviveCount = 0;

            BattleCardManager.Instance.MakeCardInBattling();
        }
        public override void OnLeave()
        {
            BattleCardManager.Instance.DestroyAllBuffs(BattleCardType.E_BATTLE_CARD_TYPE_ALL);
        }
        public override void Update()
        {
			//全部被攻击者动画播放完以后可以可以进行一下次攻击
            bool can_next = false;
            if (m_CurAttackCard != null)
            {
                Dictionary<BattleCard, BeHitBag>.KeyCollection key_col = m_CurActionBag.behitMap.Keys;
                bool end = true;
                foreach (BattleCard card in key_col)
                {
                    if (card.InBehitAction == true)
                    {
                        end = false;
                        break;
                    }
                }
                if (end)
                {
                    m_CurAttackCard = null;
                    m_CurActionBag = null;
                    can_next = true;
                }
            }
            else
            {
                can_next = true;
            }

            if (can_next)
            {
                ExecuteOneAttack();
            }
        }

        private bool CanExcuteNext()
        {
            if(m_CurAttackCard == null || m_CurAttackCard.InAttackAction)
            {
                return false;
            }

            if (!m_BattleActionBagList.ContainsKey(m_TurnCounter))
            {
                return false;
            }

            if (m_StepCounter < m_BattleActionBagList[m_TurnCounter].Count)
            {
                ActionBag action_bag = m_BattleActionBagList[m_TurnCounter][m_StepCounter];
                if (action_bag.attackCard.InBehitAction)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                m_StepCounter = 0;
                m_TurnCounter++;
                return CanExcuteNext();
            }
        }

        private void ExcuteOnCommand()
        {
            if (m_BattleActionBagList.ContainsKey(m_TurnCounter))
            {
                if (m_StepCounter < m_BattleActionBagList[m_TurnCounter].Count)
                {
                    ActionBag action_bag = m_BattleActionBagList[m_TurnCounter][m_StepCounter];
                    m_CurAttackCard = action_bag.attackCard;
                    m_CurActionBag = action_bag;
                    action_bag.attackCard.DoAttack(action_bag);
                    m_StepCounter++;
                }
                else
                {
                    m_StepCounter = 0;
                    m_TurnCounter++;
                    ExcuteOnCommand();
                }
            }
            else
            {
                 m_Manager.ChangProcedure(BattleProcedureType.E_BATTLE_PROCEDURE_SHOW_TROPHY);
            }
        }

        //播放 废弃--
        public void ExecuteOneAttack()
        {
            if (m_BattleRoundData == null)
            {
                Debug.Log("Execute(), m_BattleRoundData == null");
                return;
            }

            Debug.Log("DoPlayOneStep(), turnCount = " + m_TurnCounter + ", stepCount = " + m_StepCounter);

            if (m_TurnCounter < m_BattleRoundData.battleRound.battleTurns.Count)
            {
                if (m_StepCounter < m_BattleRoundData.battleRound.battleTurns[m_TurnCounter].battleSteps.Count)
                {
                    BattleStep tStep = m_BattleRoundData.battleRound.battleTurns[m_TurnCounter].battleSteps[m_StepCounter];

                    BattleCard tAttackcard = null;
                    AttackBag attack_bag = new AttackBag();
                    if (tStep.attacks.Count > 0)
                    {
                        StepAction tAttack = tStep.attacks[0];
                        tAttackcard = BattleCardManager.Instance.FindCardBySlotIndex(tAttack.slotIndex);
                        if (tAttackcard != null)
                        {
                            Debug.Log("Attack, slotindex: " + tAttack.slotIndex + ", CardID: " + tAttackcard.Card_Data.CardTempId + ", SkillID: " + tAttack.skillID);
                            attack_bag.skillID = tAttack.skillID;
                            attack_bag.curHp = tAttack.curHp;
                            attack_bag.loverIdx = tAttack.hetiIndex;
                            if (tAttack.buff != null && tAttack.buff.Count != 0)
                            {
                                foreach (BuffInfo info in tAttack.buff)
                                {
                                    attack_bag.attackBuffs.Add(info);
                                }
                            }
                        }
                        else
                        {
                            Debug.LogError("DoPlayOneStep(), can not find attack card! " + tAttack.slotIndex);
                            m_StepCounter++;
                            return;
                        }
                    }
                    else
                    {
                        Debug.LogError("DoPlayOneStep(), no attack card !");
                        m_StepCounter++;
                        return;
                    }

                    if (tStep.attacks.Count > 1)
                    {
                        Debug.LogError("DoPlayOneStep(), attack card more then one ! " + tStep.attacks.Count);
                    }

                    Dictionary<BattleCard, BeHitBag> behit_map = new Dictionary<BattleCard, BeHitBag>();
                    foreach (StepAction tBeAtt in tStep.behits)
                    {
                        BeHitBag behit_bag = new BeHitBag();
                        BattleCard tDefenceCard = BattleCardManager.Instance.FindCardBySlotIndex(tBeAtt.slotIndex);
                        if (tDefenceCard != null)
                        {
                            Debug.Log("Behit, slotindex: " + tBeAtt.slotIndex + ", CardID: " + tDefenceCard.Card_Data.CardTempId + ", attackHp: " + tBeAtt.attackHp);
                            behit_bag.behitValue = tBeAtt.attackHp;
                            behit_bag.curHp = tBeAtt.curHp;
                            behit_bag.isCrit = tBeAtt.isStorm;
                            if (tBeAtt.buff != null && tBeAtt.buff.Count != 0)
                            {
                                foreach (BuffInfo info in tBeAtt.buff)
                                {
                                    behit_bag.behitBuffs.Add(info);
                                }
                            }
                            behit_map.Add(tDefenceCard, behit_bag);
                        }
                    }

                    //send bag
                    ActionBag action_bag = new ActionBag();
                    action_bag.attackBag = attack_bag;
                    action_bag.behitMap = behit_map;
                    m_CurAttackCard = tAttackcard;
                    m_CurActionBag = action_bag;
                    tAttackcard.DoAttack(action_bag);

                    m_StepCounter++;
                }
                else
                {
                    m_StepCounter = 0;
                    m_TurnCounter++;
                    ExecuteOneAttack();
                }
            }
            else
            {
                m_Manager.ChangProcedure(BattleProcedureType.E_BATTLE_PROCEDURE_SHOW_TROPHY);
            }
        }
    }
}

