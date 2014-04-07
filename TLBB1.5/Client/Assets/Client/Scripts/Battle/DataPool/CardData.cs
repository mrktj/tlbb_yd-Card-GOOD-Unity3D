using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Games.Battle
{
    public class CardData
    {
		public CardData()
		{
			
		}
		
		private long m_CardGuid = -1;
        public long CardGuid { get{ return m_CardGuid;} set{m_CardGuid = value;} }
		
		private int m_CardTempId = -1;
        public int CardTempId { get{ return m_CardTempId;} set{m_CardTempId = value;} }
		
		private long m_TotalHp = 0;
        public long TotalHp { get{return m_TotalHp;} set{ m_TotalHp = value;} }
		
		private TroopMember m_MemberData = null;
        public TroopMember MemberData { get { return m_MemberData;} set{m_MemberData = value;} }
    }
}