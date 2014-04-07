using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xjgame.message;
using GCGame.Table;

namespace Games.LogicObject
{
	public class MainCopy
	{
		
        public Int32 copyId;
        public Int32 startTime;//--剩余多少时间开启复本，单位：秒--
		public Int32 clostTime;//--还有多长时间就要关闭了，单位：秒--
		public List<SubCopy> subCopys;
		public CopyState copyState;
		public Tab_Copy tblCopy;
		public int listOrder;
		
		public MainCopy(Int32 id)
		{
			copyId = id;
			startTime = -1;
			clostTime = -1;
			tblCopy = TableManager.GetCopyByID(id);
			copyState = CopyState.UNOPEN;
			subCopys = new List<SubCopy>();
			listOrder = -1;
			for(int i = 0; i < 15; i++)
			{
				if(tblCopy.GetSubcopybyIndex(i) != -1)
				{
					SubCopy sub_copy = new SubCopy(tblCopy.GetSubcopybyIndex(i));
					subCopys.Add(sub_copy);
				}
			}
		}
		
		public MainCopy()
		{
			copyId = -1;
			startTime = -1;
			clostTime = -1;
			listOrder = -1;
			subCopys = new List<SubCopy>();
		}
		public void UpdateState()
		{
			//update subcoyt first
			if(tblCopy.GetCopyTypebyIndex(0) == (int)CopyType.NORMAL)
			{
				foreach(SubCopy copy in subCopys)
				{
					if(copy.count > 0)
					{
						copy.copyState = CopyState.PASSED;
					}
					else
					{
						copy.copyState = CopyState.UNOPEN;
					}
				}
			}
			
			CopyState state = CopyState.UNOPEN;
			
			foreach(SubCopy copy in subCopys)
			{
				state = copy.copyState;
				if(state == CopyState.OPENED)
				{
					copyState = CopyState.OPENED;
					return;
				}
			}
			copyState = state;
		}
		public bool UnlockNextSubCopy()
		{
			return true;
		}
		public void SetData(CopyInfo info)
		{
			copyId = info.CopyId;
			startTime = info.Start_rest_time;
			clostTime = info.Close_rest_time;
			foreach(MissionInfo mission in info.subcopysList)
			{
				SubCopy sbcopy = new SubCopy();
				sbcopy.SetData(mission);
				subCopys.Add(sbcopy);
			}
		}
		
		public void AddTestData()
		{
			copyId = 1;
			SubCopy scopy1 = new SubCopy();
			scopy1.subCopyID = 1;
			subCopys.Add(scopy1);
			
			SubCopy scopy2 = new SubCopy();
			scopy2.subCopyID = 1;
			subCopys.Add(scopy2);
		}
	}
}
