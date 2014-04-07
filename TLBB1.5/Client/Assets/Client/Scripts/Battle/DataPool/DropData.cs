using System;
using System.Collections;

namespace Games.LogicObject
{
    public enum DropType
    {
        MONEY = 1,
        CARD,
        TIEM,
    }

    public class DropBag
    {
        public DropType type;
        public Int32 val;
        public DropBag()
        {
            type = DropType.MONEY;
            val = -1;
        }
    }
}
