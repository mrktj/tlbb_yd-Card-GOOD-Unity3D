using System;
using System.Collections.Generic;
using GCGame.Table;

namespace Games.LogicObject
{
	public struct HeroTableItem
	{
		public Int32 cardID;
		public string name;
		public Int32 level;
		public Int32 hp;
		public Int32 attack;
		public Int32 star;
		public bool locked;
		public CardIcon cardIcon;
		
		public HeroTableItem(Int32 id, Int32 lvl)
		{
			cardID = id;
			//Tab_Card card = TableManager.GetCardByID(id);
			Tab_Appearance appear = TableManager.GetAppearanceByID(id);
			name = LanguageManger.GetWords(appear.Name);
			level = lvl;
			hp = TableManager.GetCardByID(id).HpBase + TableManager.GetCardByID(id).HpGrow * lvl;
			attack = TableManager.GetCardByID(id).AttackBase + TableManager.GetCardByID(id).AttackGrow * lvl;
			star = TableManager.GetCardByID(id).Star;
			locked = false;
			cardIcon = new CardIcon(id);
		}
	}
}
