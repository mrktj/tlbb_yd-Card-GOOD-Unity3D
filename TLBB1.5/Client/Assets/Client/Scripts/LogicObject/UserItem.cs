using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.LogicObject
{
	public class UserItem
	{
        public Int32 itemID;//item没有GUID
        public Int32 itemCount;
		
		public UserItem()
		{
			itemID = -1;
			itemCount = 0;
		}
		public UserItem(Int32 id, Int32 count)
		{
			itemID = id;
			itemCount = count;
		}
	}

}
