using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GCGame.Table;
using xjgame.message;

namespace Games.LogicObject
{
	public class SubCopy
	{
        public Int32 subCopyID;
        public Int32 count;
		public Tab_Copydetail tblCopyDetail;
		public CopyState copyState;
        public Int32 maxStar;
		
		public SubCopy(Int32 id)
		{
			subCopyID = id;
			count = 0;
			copyState = CopyState.UNOPEN;
			tblCopyDetail = TableManager.GetCopydetailByID(subCopyID);
		}
		
		public SubCopy()
		{
			subCopyID = -1;
			count = 0;
			copyState = CopyState.UNOPEN;
		}

		public void SetData(MissionInfo info)
		{
			subCopyID = info.BattleId;
			count = info.Count;
            maxStar = info.MaxStarHis;
		}
	}

}
