using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Games.LogicObject
{
	public class FengShuiData  {
		public Dictionary<int,int> WuxingInfor {set; get;}
		public Dictionary<int,int> SuipianInfor {set; get;}
		public Int32 star {  set; get; }
		public double[,] fengshuiAdd = new double[5,3]; // 0-4 金木水火土 , 0-2 攻击加成 血量加成 加成百分比 By WML (当前显示卡牌详情的地方有两个：CardInfoController 和 MessageBoxController)

		private static FengShuiData m_instance = null;
	    public static FengShuiData Instance()
	    {
			if (null == m_instance)
	        {
				m_instance = new FengShuiData();
			}
			return m_instance;
		}
	}

}
