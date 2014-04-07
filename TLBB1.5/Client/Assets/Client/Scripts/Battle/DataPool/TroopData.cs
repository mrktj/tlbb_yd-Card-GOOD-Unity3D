using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Games.LogicObject;

namespace Games.Battle
{
    public class TroopMember
    {
        public Int32 slotIndex;
        public Int64 guid;
        public Int32 cardID;
        public Int32 state;
        public DropBag bag;
        public Int64 initHp;
        public Int32 commSkillID;
        public Int32 volSkillID;
        public Int32 combSkillID;
		
        public TroopMember(Int32 index, Int32 id, Int64 gid = 0)
        {
            slotIndex = index;
            guid = gid;
            cardID = id;
            bag = new DropBag();
            initHp = 0;
            commSkillID = -1;
            volSkillID = -1;
            combSkillID = -1;
        }
    }

    public class TroopData
    {
        public List<TroopMember> selfMembers = new List<TroopMember>();
        public List<TroopMember> otherMembers = new List<TroopMember>();

        public void Clear()
        {
            selfMembers.Clear();
            otherMembers.Clear();
        }
        public int GetStateBySlot(int index)
        {
            foreach (TroopMember member in selfMembers)
            {
                if (member.slotIndex == index)
                {
                    return member.state;
                }
            }
            foreach (TroopMember member in otherMembers)
            {
                if (member.slotIndex == index)
                {
                    return member.state;
                }
            }
            return -1;
        }
    }
}

