using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using GCGame.Table;
using xjgame.message;
using Games.Battle;
using Games.CharacterLogic;

namespace Games.LogicObject
{
   public class BattleRoundData
   {
       public TroopData troopData = new TroopData();
       public BattleRound battleRound = new BattleRound();
       public bool isWin;
       public List<DropBag> winDropBags;
       public Int32 addExp;
       public int missionID;
       public int roundCount;

       public bool isPlayed = false;
       public bool isLastBattle = false;//是否是上一次中断返回的战斗

       //战斗结束，如果胜利，刷新用户数据
       public PBUserBaseData pbBaseData;
       public PBUserBagData pbBagData;
       public PBUserCopyData pbCopyData;
		private bool mChonglouLast = false;
		public bool ChonglouLast
		{
			get
			{
				return mChonglouLast;
			}
		}

       public BattleRoundData()
       {
           isPlayed = false;
           winDropBags = new List<DropBag>();
       }

       public void SetBattleData(SCBattleData data)
       {
           Debug.LogWarning("!!!!!!!!!!!!!!!!!!!!!! SetBattleData() !!!!!!!!!!!!!!!!!!!!!!");

           string dlog = "missionID = " + data.Battle.MissionID;
           dlog += ",\t\t roundCount = " + data.Battle.Round_count;
           dlog += ",\t\t Win_idx = " + data.Battle.Win_idx;
           Debug.LogWarning(dlog);

           ClearBattleData();//clear first
           //pb data
           pbBaseData = data.BaseData;
           pbBagData = data.BagData;
           pbCopyData = data.CopyData;
           isLastBattle = false;
           isPlayed = false;
           isWin = !Convert.ToBoolean(data.Battle.Win_idx);
           foreach (xjgame.message.DropBag bag in data.Battle.dropsList)
           {
               Games.LogicObject.DropBag drop_bag = new DropBag();
               drop_bag.type = (DropType)bag.Type;
               drop_bag.val = bag.Value;
               winDropBags.Add(drop_bag);
           }
           addExp = data.Battle.Add_exp;
           if (Obj_MyselfPlayer.GetMe().currentAssistFriend != null)
           {
               Obj_MyselfPlayer.GetMe().currentAssistFriend.friendShipNum = data.Battle.GetFriendPoint;
           }
           foreach (xjgame.message.BattleCard card in data.Battle.userCardList)
           {
               string log = "Card: slot_idx = " + card.Place_idx + ",\t\t cardid = " + card.CardID + ",\t\t guid = " + card.Cardguid;
               log += ",\t\t state = " + card.State;
               log += ",\t\t commSkillID = " + card.CommSkillId;
               log += ",\t\t volSkillID = " + card.VolSkillId;
               log += ",\t\t combSkillID = " + card.CombSkillId;

               if (card.Place_idx < 6)
               {
                   TroopMember member = new TroopMember(card.Place_idx, card.CardID, (long)card.Cardguid);
                   member.state = card.State;
                   member.initHp = card.Init_hp;
                   member.commSkillID = card.CommSkillId;
                   member.volSkillID = card.VolSkillId;
                   member.combSkillID = card.CombSkillId;
                   if (card.Bag != null)
                   {
                       member.bag.type = (DropType)card.Bag.Type;
                       member.bag.val = card.Bag.Value;
                       log += ",\t\t DropBag.Type = " + card.Bag.Type;
                       log += ",\t\t DropBag.Value = " + card.Bag.Value;
                   }
                   troopData.selfMembers.Add(member);
               }
               else
               {
                   TroopMember member = new TroopMember(card.Place_idx, card.CardID, (long)card.Cardguid);
                   member.state = card.State;
                   member.initHp = card.Init_hp;
                   member.commSkillID = card.CommSkillId;
                   member.volSkillID = card.VolSkillId;
                   member.combSkillID = card.CombSkillId;
                   if (card.Bag != null)
                   {
                       member.bag.type = (DropType)card.Bag.Type;
                       member.bag.val = card.Bag.Value;
                       log += ",\t\t DropBag.Type = " + card.Bag.Type;
                       log += ",\t\t DropBag.Value = " + card.Bag.Value;
                   }
                   troopData.otherMembers.Add(member);
               }
               Debug.LogWarning(log);
           }

           foreach (DataRound round in data.Battle.roundsList)
           {
               BattleTurn turn = new BattleTurn();
               foreach (DataAction action in round.actionsList)
               {
                   BattleStep step = new BattleStep();
                   foreach (DataSingleAction sact in action.attacker_actionsList)
                   {
                       string log = "Attack action: ";
                       log += ",\t\t card_idx = " + sact.Card_idx;
                       log += ",\t\t skillid = " + sact.Skillid;
                       log += ",\t\t att_value = " + sact.Att_value;
                       log += ",\t\t att_type = " + sact.Att_type;
                       log += ",\t\t beCrit = " + sact.BeCrit;
                       log += ",\t\t cur_hp = " + sact.Cur_hp;
                       log += ",\t\t heti_idx = " + sact.Heti_idx;

                       StepAction attack = new StepAction();
                       if (sact.buffInfoList != null)
                       {
                           foreach (DataBuffInfo buf in sact.buffInfoList)
                           {
                               BuffInfo attBuf = new BuffInfo();
                               attBuf.buf_id = buf.Buf_id;
                               attBuf.buf_value = buf.Buf_value;
                               attack.buff.Add(attBuf);
                               log += ",\t\t buf.Buf_id = " + buf.Buf_id;
                               log += ",\t\t buf.Buf_value = " + buf.Buf_value;
                           }
                       }
                       attack.slotIndex = sact.Card_idx;
                       attack.skillID = sact.Skillid;
                       attack.attackHp = sact.Att_value;
                       attack.harmType = sact.Att_type;
                       attack.isStorm = Convert.ToBoolean(sact.BeCrit);
                       attack.curHp = sact.Cur_hp;
                       if (sact.HasHeti_idx) attack.hetiIndex = sact.Heti_idx;
                       step.attacks.Add(attack);
                       Debug.LogWarning(log);
                   }

                   foreach (DataSingleAction sact in action.be_attacker_actionsList)
                   {
                       string log = "Behit action: ";
                       log += ",\t\t card_idx = " + sact.Card_idx;
                       log += ",\t\t skillid = " + sact.Skillid;
                       log += ",\t\t att_value = " + sact.Att_value;
                       log += ",\t\t att_type = " + sact.Att_type;
                       log += ",\t\t beCrit = " + sact.BeCrit;
                       log += ",\t\t cur_hp = " + sact.Cur_hp;
                       log += ",\t\t heti_idx = " + sact.Heti_idx;

                       StepAction behit = new StepAction();
                       if (sact.buffInfoList != null)
                       {
                           foreach (DataBuffInfo buf in sact.buffInfoList)
                           {
                               BuffInfo behitBuf = new BuffInfo();
                               behitBuf.buf_id = buf.Buf_id;
                               behitBuf.buf_value = buf.Buf_value;
                               behit.buff.Add(behitBuf);
                               log += ",\t\t buf.Buf_id = " + buf.Buf_id;
                               log += ",\t\t buf.Buf_value = " + buf.Buf_value;
                           }
                       }
                       behit.slotIndex = sact.Card_idx;
                       behit.skillID = sact.Skillid;
                       behit.attackHp = sact.Att_value;
                       behit.harmType = sact.Att_type;
                       behit.isStorm = Convert.ToBoolean(sact.BeCrit);
                       behit.curHp = sact.Cur_hp;
                       step.behits.Add(behit);
                       Debug.LogWarning(log);
                   }
                   turn.battleSteps.Add(step);
               }
               battleRound.battleTurns.Add(turn);
           }
       }
	   	public void SetBattleData(SCPaiTaBattleData data)
		{
			ClearBattleData();
			pbBaseData = data.BaseData;
           	pbBagData = data.BagData;
           	pbCopyData = data.CopyData;
			isLastBattle = false;
           	isPlayed = false;
			isWin = !Convert.ToBoolean(data.Battle.Win_idx);
            foreach (xjgame.message.DropBag bag in data.Battle.dropsList)
            {
                Games.LogicObject.DropBag drop_bag = new DropBag();
                drop_bag.type = (DropType)bag.Type;
                drop_bag.val = bag.Value;
                winDropBags.Add(drop_bag);
            }
            addExp = data.Battle.Add_exp;
			Debug.LogWarning("!!!!!!!!!!!!!!!!!!!!!! SetPVPBattleData() !!!!!!!!!!!!!!!!!!!!!!");
			mChonglouLast = (data.Flag == 1);
			SetUserCard(data.Battle.userCardList);
			SetDataRound(data.Battle.roundsList);
		}
		
		public void SetBattleData(SCAskWorldBossBattle data)
		{
			ClearBattleData();
			pbBaseData = data.BaseData;
			addExp = data.Battle.Add_exp;
			isLastBattle = false;
           	isPlayed = false;
			isWin = !Convert.ToBoolean(data.Battle.Win_idx);
            foreach (xjgame.message.DropBag bag in data.Battle.dropsList)
            {
                Games.LogicObject.DropBag drop_bag = new DropBag();
                drop_bag.type = (DropType)bag.Type;
                drop_bag.val = bag.Value;
                winDropBags.Add(drop_bag);
            }
			mChonglouLast = false;
			SetUserCard(data.Battle.userCardList);
			SetDataRound(data.Battle.roundsList);
		}
		
		private void SetUserCard(IList<xjgame.message.BattleCard> userCardList)
		{
			foreach (xjgame.message.BattleCard card in userCardList)
           {
               string log = "Card: slot_idx = " + card.Place_idx + ",\t\t cardid = " + card.CardID + ",\t\t guid = " + card.Cardguid;
               log += ",\t\t state = " + card.State;
               log += ",\t\t commSkillID = " + card.CommSkillId;
               log += ",\t\t volSkillID = " + card.VolSkillId;
               log += ",\t\t combSkillID = " + card.CombSkillId;

               if (card.Place_idx < 6)
               {
                   TroopMember member = new TroopMember(card.Place_idx, card.CardID, (long)card.Cardguid);
                   member.state = card.State;
                   member.initHp = card.Init_hp;
                   member.commSkillID = card.CommSkillId;
                   member.volSkillID = card.VolSkillId;
                   member.combSkillID = card.CombSkillId;
                   if (card.Bag != null)
                   {
                       member.bag.type = (DropType)card.Bag.Type;
                       member.bag.val = card.Bag.Value;
                       log += ",\t\t DropBag.Type = " + card.Bag.Type;
                       log += ",\t\t DropBag.Value = " + card.Bag.Value;
                   }
                   troopData.selfMembers.Add(member);
               }
               else
               {
                   TroopMember member = new TroopMember(card.Place_idx, card.CardID, (long)card.Cardguid);
                   member.state = card.State;
                   member.initHp = card.Init_hp;
                   member.commSkillID = card.CommSkillId;
                   member.volSkillID = card.VolSkillId;
                   member.combSkillID = card.CombSkillId;
                   if (card.Bag != null)
                   {
                       member.bag.type = (DropType)card.Bag.Type;
                       member.bag.val = card.Bag.Value;
                       log += ",\t\t DropBag.Type = " + card.Bag.Type;
                       log += ",\t\t DropBag.Value = " + card.Bag.Value;
                   }
                   troopData.otherMembers.Add(member);
               }
               Debug.LogWarning(log);
           }
		}
		
		private void SetDataRound(IList<DataRound> dataRound)
		{
			foreach (DataRound round in dataRound)
           {
               BattleTurn turn = new BattleTurn();
               foreach (DataAction action in round.actionsList)
               {
                   BattleStep step = new BattleStep();
                   foreach (DataSingleAction sact in action.attacker_actionsList)
                   {
                       string log = "Attack action: ";
                       log += ",\t\t card_idx = " + sact.Card_idx;
                       log += ",\t\t skillid = " + sact.Skillid;
                       log += ",\t\t att_value = " + sact.Att_value;
                       log += ",\t\t att_type = " + sact.Att_type;
                       log += ",\t\t beCrit = " + sact.BeCrit;
                       log += ",\t\t cur_hp = " + sact.Cur_hp;
                       log += ",\t\t heti_idx = " + sact.Heti_idx;

                       StepAction attack = new StepAction();
                       if (sact.buffInfoList != null)
                       {
                           foreach (DataBuffInfo buf in sact.buffInfoList)
                           {
                               BuffInfo attBuf = new BuffInfo();
                               attBuf.buf_id = buf.Buf_id;
                               attBuf.buf_value = buf.Buf_value;
                               attack.buff.Add(attBuf);
                               log += ",\t\t buf.Buf_id = " + buf.Buf_id;
                               log += ",\t\t buf.Buf_value = " + buf.Buf_value;
                           }
                       }
                       attack.slotIndex = sact.Card_idx;
                       attack.skillID = sact.Skillid;
                       attack.attackHp = sact.Att_value;
                       attack.harmType = sact.Att_type;
                       attack.isStorm = Convert.ToBoolean(sact.BeCrit);
                       attack.curHp = sact.Cur_hp;
                       if (sact.HasHeti_idx) attack.hetiIndex = sact.Heti_idx;
                       step.attacks.Add(attack);
                       Debug.LogWarning(log);
                   }

                   foreach (DataSingleAction sact in action.be_attacker_actionsList)
                   {
                       string log = "Behit action: ";
                       log += ",\t\t card_idx = " + sact.Card_idx;
                       log += ",\t\t skillid = " + sact.Skillid;
                       log += ",\t\t att_value = " + sact.Att_value;
                       log += ",\t\t att_type = " + sact.Att_type;
                       log += ",\t\t beCrit = " + sact.BeCrit;
                       log += ",\t\t cur_hp = " + sact.Cur_hp;
                       log += ",\t\t heti_idx = " + sact.Heti_idx;

                       StepAction behit = new StepAction();
                       if (sact.buffInfoList != null)
                       {
                           foreach (DataBuffInfo buf in sact.buffInfoList)
                           {
                               BuffInfo behitBuf = new BuffInfo();
                               behitBuf.buf_id = buf.Buf_id;
                               behitBuf.buf_value = buf.Buf_value;
                               behit.buff.Add(behitBuf);
                               log += ",\t\t buf.Buf_id = " + buf.Buf_id;
                               log += ",\t\t buf.Buf_value = " + buf.Buf_value;
                           }
                       }
                       behit.slotIndex = sact.Card_idx;
                       behit.skillID = sact.Skillid;
                       behit.attackHp = sact.Att_value;
                       behit.harmType = sact.Att_type;
                       behit.isStorm = Convert.ToBoolean(sact.BeCrit);
                       behit.curHp = sact.Cur_hp;
                       step.behits.Add(behit);
                       Debug.LogWarning(log);
                   }
                   turn.battleSteps.Add(step);
               }
               battleRound.battleTurns.Add(turn);
           }
		}
		
       public void SetPVPBattleData(SCPVPBattleData data)
       {
           ClearBattleData();//clear first

           //pb data
           pbBaseData = data.BaseData;
           pbBagData = data.BagData;
           pbCopyData = data.CopyData;

           isPlayed = false;
           isLastBattle = false;
           isWin = !Convert.ToBoolean(data.Battle.Win_idx);
           foreach (xjgame.message.DropBag bag in data.Battle.dropsList)
           {
               DropBag drop_bag = new DropBag();
               drop_bag.type = (DropType)bag.Type;
               drop_bag.val = bag.Value;
               winDropBags.Add(drop_bag);
           }
           addExp = data.Battle.Add_exp;

           Debug.LogWarning("!!!!!!!!!!!!!!!!!!!!!! SetPVPBattleData() !!!!!!!!!!!!!!!!!!!!!!");
           foreach (xjgame.message.BattleCard card in data.Battle.userCardList)
           {
               string log = "Card: slot_idx = " + card.Place_idx + ",\t\t cardid = " + card.CardID + ",\t\t guid = " + card.Cardguid;
               log += ",\t\t state = " + card.State;
               log += ",\t\t commSkillID = " + card.CommSkillId;
               log += ",\t\t volSkillID = " + card.VolSkillId;
               log += ",\t\t combSkillID = " + card.CombSkillId;

               if (card.Place_idx < 6)
               {
                   TroopMember member = new TroopMember(card.Place_idx, card.CardID, (long)card.Cardguid);
                   member.state = card.State;
                   member.initHp = card.Init_hp;
                   member.commSkillID = card.CommSkillId;
                   member.volSkillID = card.VolSkillId;
                   member.combSkillID = card.CombSkillId;
                   if (card.Bag != null)
                   {
                       member.bag.type = (DropType)card.Bag.Type;
                       member.bag.val = card.Bag.Value;
                       log += ",\t\t DropBag.Type = " + card.Bag.Type;
                       log += ",\t\t DropBag.Value = " + card.Bag.Value;
                   }
                   troopData.selfMembers.Add(member);
               }
               else
               {
                   TroopMember member = new TroopMember(card.Place_idx, card.CardID, (long)card.Cardguid);
                   member.state = card.State;
                   member.initHp = card.Init_hp;
                   member.commSkillID = card.CommSkillId;
                   member.volSkillID = card.VolSkillId;
                   member.combSkillID = card.CombSkillId;
                   if (card.Bag != null)
                   {
                       member.bag.type = (DropType)card.Bag.Type;
                       member.bag.val = card.Bag.Value;
                       log += ",\t\t DropBag.Type = " + card.Bag.Type;
                       log += ",\t\t DropBag.Value = " + card.Bag.Value;
                   }
                   troopData.otherMembers.Add(member);
               }
               Debug.LogWarning(log);
           }

           foreach (DataRound round in data.Battle.roundsList)
           {
               BattleTurn turn = new BattleTurn();
               foreach (DataAction action in round.actionsList)
               {
                   BattleStep step = new BattleStep();
                   foreach (DataSingleAction sact in action.attacker_actionsList)
                   {
                       string log = "Attack action: ";
                       log += ",\t\t card_idx = " + sact.Card_idx;
                       log += ",\t\t skillid = " + sact.Skillid;
                       log += ",\t\t att_value = " + sact.Att_value;
                       log += ",\t\t att_type = " + sact.Att_type;
                       log += ",\t\t beCrit = " + sact.BeCrit;
                       log += ",\t\t cur_hp = " + sact.Cur_hp;
                       log += ",\t\t heti_idx = " + sact.Heti_idx;

                       StepAction attack = new StepAction();
                       if (sact.buffInfoList != null)
                       {
                           foreach (DataBuffInfo buf in sact.buffInfoList)
                           {
                               BuffInfo attBuf = new BuffInfo();
                               attBuf.buf_id = buf.Buf_id;
                               attBuf.buf_value = buf.Buf_value;
                               attack.buff.Add(attBuf);
                               log += ",\t\t buf.Buf_id = " + buf.Buf_id;
                               log += ",\t\t buf.Buf_value = " + buf.Buf_value;
                           }
                       }
                       attack.slotIndex = sact.Card_idx;
                       attack.skillID = sact.Skillid;
                       attack.attackHp = sact.Att_value;
                       attack.harmType = sact.Att_type;
                       attack.isStorm = Convert.ToBoolean(sact.BeCrit);
                       attack.curHp = sact.Cur_hp;
                       if (sact.HasHeti_idx) attack.hetiIndex = sact.Heti_idx;
                       step.attacks.Add(attack);
                       Debug.LogWarning(log);
                   }

                   foreach (DataSingleAction sact in action.be_attacker_actionsList)
                   {
                       string log = "Behit action: ";
                       log += ",\t\t card_idx = " + sact.Card_idx;
                       log += ",\t\t skillid = " + sact.Skillid;
                       log += ",\t\t att_value = " + sact.Att_value;
                       log += ",\t\t att_type = " + sact.Att_type;
                       log += ",\t\t beCrit = " + sact.BeCrit;
                       log += ",\t\t cur_hp = " + sact.Cur_hp;
                       log += ",\t\t heti_idx = " + sact.Heti_idx;

                       StepAction behit = new StepAction();
                       if (sact.buffInfoList != null)
                       {
                           foreach (DataBuffInfo buf in sact.buffInfoList)
                           {
                               BuffInfo behitBuf = new BuffInfo();
                               behitBuf.buf_id = buf.Buf_id;
                               behitBuf.buf_value = buf.Buf_value;
                               behit.buff.Add(behitBuf);
                               log += ",\t\t buf.Buf_id = " + buf.Buf_id;
                               log += ",\t\t buf.Buf_value = " + buf.Buf_value;
                           }
                       }
                       behit.slotIndex = sact.Card_idx;
                       behit.skillID = sact.Skillid;
                       behit.attackHp = sact.Att_value;
                       behit.harmType = sact.Att_type;
                       behit.isStorm = Convert.ToBoolean(sact.BeCrit);
                       behit.curHp = sact.Cur_hp;
                       step.behits.Add(behit);
                       Debug.LogWarning(log);
                   }
                   turn.battleSteps.Add(step);
               }
               battleRound.battleTurns.Add(turn);
           }
       }
       public void SetLastBattleData(DataBattle data)
       {
           Debug.LogWarning("!!!!!!!!!!!!!!!!!!!!!! SetLastBattleData() !!!!!!!!!!!!!!!!!!!!!!");
           string dlog = "missionID = " + data.MissionID;
           dlog += ",\t\t roundCount = " + data.Round_count;
           dlog += ",\t\t Win_idx = " + data.Win_idx;
           Debug.LogWarning(dlog);
           ClearBattleData();//clear first
           missionID = (int)data.MissionID;
           roundCount = data.Round_count;
           isPlayed = false;
           isLastBattle = true;
           isWin = !Convert.ToBoolean(data.Win_idx);
           foreach (xjgame.message.DropBag bag in data.dropsList)
           {
               DropBag drop_bag = new DropBag();
               drop_bag.type = (DropType)bag.Type;
               drop_bag.val = bag.Value;
               winDropBags.Add(drop_bag);
           }
           addExp = data.Add_exp;
           //test
           if (Obj_MyselfPlayer.GetMe().currentAssistFriend == null)
           {
               Obj_MyselfPlayer.GetMe().currentAssistFriend = new AssistFriend();
               Obj_MyselfPlayer.GetMe().currentAssistFriend.guid = (long)data.Friendguid;
               Obj_MyselfPlayer.GetMe().currentAssistFriend.name = data.Friendname;
               Obj_MyselfPlayer.GetMe().currentAssistFriend.level = data.Friendlevel;
               Obj_MyselfPlayer.GetMe().currentAssistFriend.cardLevel = data.FriendCardLev;
               Obj_MyselfPlayer.GetMe().currentAssistFriend.friendShipNum = data.GetFriendPoint;
               if (data.IsFriend == 0)
               {
                   Obj_MyselfPlayer.GetMe().currentAssistFriend.isMyFriend = true;
               }
               else
               {
                   Obj_MyselfPlayer.GetMe().currentAssistFriend.isMyFriend = false;
               }
           }
           foreach (xjgame.message.BattleCard card in data.userCardList)
           {
               string log = "Card: slot_idx = " + card.Place_idx + ",\t\t cardid = " + card.CardID + ",\t\t guid = " + card.Cardguid;
               log += ",\t\t state = " + card.State;
               log += ",\t\t isfriend = " + card.Isfriend;
               log += ",\t\t commSkillID = " + card.CommSkillId;
               log += ",\t\t volSkillID = " + card.VolSkillId;
               log += ",\t\t combSkillID = " + card.CombSkillId;

               if (card.Place_idx < 6)
               {
                   TroopMember member = new TroopMember(card.Place_idx, card.CardID, (long)card.Cardguid);
                   member.state = card.State;
                   member.initHp = card.Init_hp;
                   member.commSkillID = card.CommSkillId;
                   member.volSkillID = card.VolSkillId;
                   member.combSkillID = card.CombSkillId;
                   if (card.Bag != null)
                   {
                       member.bag.type = (DropType)card.Bag.Type;
                       member.bag.val = card.Bag.Value;
                       log += ",\t\t DropBag.Type = " + card.Bag.Type;
                       log += ",\t\t DropBag.Value = " + card.Bag.Value;
                   }
                   troopData.selfMembers.Add(member);
                   if (card.Isfriend == 1)
                   {
                       Obj_MyselfPlayer.GetMe().currentAssistFriend.cardGuiId = (long)card.Cardguid;
                       Obj_MyselfPlayer.GetMe().currentAssistFriend.cardTempleId = card.CardID;
                       Obj_MyselfPlayer.GetMe().battleArray[card.Place_idx] = (long)card.Cardguid;
                   }
               }
               else
               {
                   TroopMember member = new TroopMember(card.Place_idx, card.CardID, (long)card.Cardguid);
                   member.state = card.State;
                   member.initHp = card.Init_hp;
                   member.commSkillID = card.CommSkillId;
                   member.volSkillID = card.VolSkillId;
                   member.combSkillID = card.CombSkillId;
                   if (card.Bag != null)
                   {
                       member.bag.type = (DropType)card.Bag.Type;
                       member.bag.val = card.Bag.Value;
                       log += ",\t\t DropBag.Type = " + card.Bag.Type;
                       log += ",\t\t DropBag.Value = " + card.Bag.Value;
                   }
                   troopData.otherMembers.Add(member);
               }
               Debug.LogWarning(log);
           }

           foreach (DataRound round in data.roundsList)
           {
               BattleTurn turn = new BattleTurn();
               foreach (DataAction action in round.actionsList)
               {
                   BattleStep step = new BattleStep();
                   foreach (DataSingleAction sact in action.attacker_actionsList)
                   {
                       string log = "Attack action: ";
                       log += ",\t\t card_idx = " + sact.Card_idx;
                       log += ",\t\t skillid = " + sact.Skillid;
                       log += ",\t\t att_value = " + sact.Att_value;
                       log += ",\t\t att_type = " + sact.Att_type;
                       log += ",\t\t beCrit = " + sact.BeCrit;
                       log += ",\t\t cur_hp = " + sact.Cur_hp;
                       log += ",\t\t heti_idx = " + sact.Heti_idx;

                       StepAction attack = new StepAction();
                       if (sact.buffInfoList != null)
                       {
                           foreach (DataBuffInfo buf in sact.buffInfoList)
                           {
                               BuffInfo attBuf = new BuffInfo();
                               attBuf.buf_id = buf.Buf_id;
                               attBuf.buf_value = buf.Buf_value;
                               attack.buff.Add(attBuf);
                               log += ",\t\t buf.Buf_id = " + buf.Buf_id;
                               log += ",\t\t buf.Buf_value = " + buf.Buf_value;
                           }
                       }
                       attack.slotIndex = sact.Card_idx;
                       attack.skillID = sact.Skillid;
                       attack.attackHp = sact.Att_value;
                       attack.harmType = sact.Att_type;
                       attack.isStorm = Convert.ToBoolean(sact.BeCrit);
                       attack.curHp = sact.Cur_hp;
                       if (sact.HasHeti_idx) attack.hetiIndex = sact.Heti_idx;
                       step.attacks.Add(attack);
                       Debug.LogWarning(log);
                   }

                   foreach (DataSingleAction sact in action.be_attacker_actionsList)
                   {
                       string log = "Behit action: ";
                       log += ",\t\t card_idx = " + sact.Card_idx;
                       log += ",\t\t skillid = " + sact.Skillid;
                       log += ",\t\t att_value = " + sact.Att_value;
                       log += ",\t\t att_type = " + sact.Att_type;
                       log += ",\t\t beCrit = " + sact.BeCrit;
                       log += ",\t\t cur_hp = " + sact.Cur_hp;
                       log += ",\t\t heti_idx = " + sact.Heti_idx;

                       StepAction behit = new StepAction();
                       if (sact.buffInfoList != null)
                       {
                           foreach (DataBuffInfo buf in sact.buffInfoList)
                           {
                               BuffInfo behitBuf = new BuffInfo();
                               behitBuf.buf_id = buf.Buf_id;
                               behitBuf.buf_value = buf.Buf_value;
                               behit.buff.Add(behitBuf);
                               log += ",\t\t buf.Buf_id = " + buf.Buf_id;
                               log += ",\t\t buf.Buf_value = " + buf.Buf_value;
                           }
                       }
                       behit.slotIndex = sact.Card_idx;
                       behit.skillID = sact.Skillid;
                       behit.attackHp = sact.Att_value;
                       behit.harmType = sact.Att_type;
                       behit.isStorm = Convert.ToBoolean(sact.BeCrit);
                       behit.curHp = sact.Cur_hp;
                       step.behits.Add(behit);
                       Debug.LogWarning(log);
                   }
                   turn.battleSteps.Add(step);
               }
               battleRound.battleTurns.Add(turn);
           }
           Obj_MyselfPlayer.GetMe().SetBattleBeforeDate();
       }
       public void ClearBattleData()
       {
           troopData.Clear();
           battleRound.Clear();
           winDropBags.Clear();
       }
       public int GetStateBySlot(int index)
       {
           return troopData.GetStateBySlot(index);
       }
       public TroopMember FindCardByGUID(long guid)
       {
           return troopData.selfMembers.Find(delegate(TroopMember mbr) { return mbr.guid == guid; });
       }
       public void AddTestData()
       {
           troopData = new TroopData();
           //troopData.AddTestData();
           battleRound = new BattleRound();
           battleRound.AddTestData();
           isPlayed = false;
           isWin = true;
       }
       public void PrintBattleData(SCBattleData data)
       {
           //			Debug.LogWarning("出战的卡：");
           //			foreach(xjgame.message.BattleCard card in data.Battle.userCardList)
           //			{				
           //				string log = "cardguid: " + card.Cardguid;
           //				log += ",\t\t cardID: " + card.CardID;
           //				log += ",\t\t place_idx: " + card.Place_idx;
           //				log += ",\t\t state: " + card.State;
           //				log += ",\t\t isfriend: " + card.Isfriend;
           //				log += ",\t\t init_hp: " + card.Init_hp;
           //				if(card.HasBag)
           //				{
           //					log += ",\t\t DropBag.Type: " + card.Bag.Type;
           //					log += ",\t\t DropBag.value: " + card.Bag.Value;
           //				}
           //				
           //				Debug.LogWarning(log);
           //			}
       }
   }
}

