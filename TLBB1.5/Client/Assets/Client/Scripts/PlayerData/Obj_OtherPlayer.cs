using System;
using System.Collections.Generic;

namespace Games.CharacterLogic
{
    public class Obj_OtherPlayer : Obj_Character
    {
        protected Int32 m_Jade;
        protected Int32 m_Gold;
        protected Int32 m_Energy;
        protected Int32 m_EXP;
        protected Int32 m_PlayerLevel;

        protected int Score;
        protected int CombatCount;

        protected string PlayerName { set; get; }

        public int GetScore() { return Score; }
        public int GetCombatListCount() { return CombatCount; }

        private string m_UserAccount { get; set; }

        public int GetJade() { return m_Jade; }
        public void SetJade(int val) { m_Jade = val; }
        public int GetGold() { return m_Gold; }
        public void SetGold(int val) { m_Gold = val; }
        public int GetEnergy() { return m_Energy; }
        public int GetEXP() { return m_EXP; }
        public int GetPlayerLevel() { return m_PlayerLevel; }

        public string GetAccountName()
        {
            return m_UserAccount;
        }

        public void SetAccountName(string userName)
        {
            m_UserAccount = userName;
        }

        
    }
}

