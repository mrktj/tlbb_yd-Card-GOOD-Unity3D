using System.Collections;
using System.Linq;
using System.Text;
using System;

namespace Games.LogicObject
{
	public class YunyingHuodong  {
	
		public  Int32 ItemType ;	// 类型，1，卡牌，2，金钱，3，道具， 4，元宝
		public Int32 ItemValue ;	// 卡牌id，金钱数量，道具ID，元宝数量
		public Int32 ItemNumber ;	// 卡牌数量，道具数量
		
		
		public YunyingHuodong()
		{
			ItemType = 0;
			ItemValue = 0;
			ItemNumber = 0;
		}
		public YunyingHuodong(Int32 Type, Int32 Value,Int32 Number)
		{
			ItemType = Type;
			ItemValue = Value;
			ItemNumber = Number;
		}
	}

}

