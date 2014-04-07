using System;
using System.Collections.Generic;
using GCGame.Table;

namespace Games.LogicObject
{
	public struct CardIcon
	{
		public Int32 cardID;
		public string icon;
		public string frame;
		
		public CardIcon(Int32 id)
		{
			cardID = id;
			icon = TableManager.GetAppearanceByID(id).HeadIcon;//
			frame = "card_icon_frame_white";
		}
	}
}