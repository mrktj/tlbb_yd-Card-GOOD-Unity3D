using System;
////using Module.Log;
using System.Collections;
using xjgame.message;
using UnityEngine;
using Games.CharacterLogic;
using Games.LogicObject;
namespace card.net
{
	public class NetManager
	{
		
		static ArrayList packetHandlerList = new ArrayList();
        static Queue netPacketQueue = new Queue(); 
		
		public NetManager ()
		{
		}
		
		public static UIListener.OnReceive lastFunReceive=null;
		public static PacketDistributed lastPacket=null;
		public static bool lastIsblockScreen=false;
        public static bool is_relink = false;
		
		public static void init() {
			Queue.Synchronized(netPacketQueue);
		}
		
		public static void tick() {
			while(netPacketQueue.Count > 0) {
				NetPacket pt = netPacketQueue.Dequeue() as NetPacket;
				if(pt != null) {
					handleNetPacket(pt.opcode, pt.data);
				}
			}
		}
		
		public static void registierPacketHandler(HTTPPacketHandler handler) {
			foreach(HTTPPacketHandler packetHandler in packetHandlerList) {
				Debug.Log("packetHandlerList[i].GetType()=" + packetHandler.GetType());
				Debug.Log("handler.GetType()=" + handler.GetType());
				if(packetHandler.GetType().Equals(handler.GetType())){
					return;
				}
			}
			packetHandlerList.Add(handler);
		}
		
		private static void sendLastPacket(GameObject button)
		{
			LoginDone(true);
		}
		
		private static void returnLoginScene(GameObject button)
		{
			//清空session id
			HTTPClientAPI.cleanSessionId();
			//reset login当前菜单为splahcontroller
			LoginLogic.needResetLogin = true;
			MainUILogic.needResetLogin = true;
			//清除临时切换数据
			//update
			//Obj_MyselfPlayer.GetMe().updateHeroItem = null;
			//Obj_MyselfPlayer.GetMe().updateMaterialItems = new UserCardItem[6];
			//evolution
			Obj_MyselfPlayer.GetMe().evolutionHeroItem = null;
			Obj_MyselfPlayer.GetMe().evolutionMaterialItems = new UserCardItem[5];
			//strengthen
			Obj_MyselfPlayer.GetMe().strengthenHeroItem = null;
			//清空新手引导状态
			GuideManager.Instance.guideTimeOut(1);
			AccountManager.Instance.initAccount();
			if (AccountManager.userType != AccountManager.UserType.OldUser)
				PlayerPrefs.SetInt("InGameBackLogin",1); //标记玩家是否在游戏中超时发送登陆包
			NetworkSender.Instance().Login(LoginDone, AccountManager.Instance.GetLoginAccountID(),0);
			//回到主菜单
			//GameManager.Instance.LoadLevel(Utils.UI_NAME_Login);
		}
		public static void LoginDone(bool bSuccess)
		{
			NetworkSender.Instance().send(lastFunReceive,lastPacket,lastIsblockScreen);
		}
		public static void ReLink(GameObject go)
		{
			NetworkSender.Instance().send(lastFunReceive,lastPacket,lastIsblockScreen);
		}
		private static void handleNetPacket(int retOpcode, byte[] retData) {
			if(retOpcode == -1) {
				UIListener.Instance().CleanAll();
				NetworkSender.Instance().sendFinish(false);
				Debug.LogError("retOpcode:"+retOpcode+" exception no reviced retOpcode. return null");
				return;
			}else if(retOpcode == 20001)
			{
				Debug.Log("retOpcode = 20001");
//				BoxManager.showMessage("请重新登录");
//				Debug.Log("Lase Packet = " + lastPacket.getMessageID());
//				if (lastPacket.getMessageID() == MessageID.CSLogin)
//				{
//					NetworkSender.Instance().sendFinish(true);
//					BoxManager.showMessageByID((int)MessageIdEnum.Msg20);
//					UIEventListener.Get(BoxManager.getYesButton()).onClick += sendLastPacket;
//					return;
//				}
//				else 
//				if (lastPacket.getMessageID() == MessageID.CSBindAccount)
//				{
//					NetworkSender.Instance().sendFinish(true);
////					LoginDone(true);
//					BoxManager.showMessageByID((int)MessageIdEnum.Msg24);
//					return;
//				}
				
				BoxManager.showMessageByID((int)MessageIdEnum.Msg20);
				UIEventListener.Get(BoxManager.getYesButton()).onClick += returnLoginScene;
                is_relink = true;
				UIListener.Instance().CleanAll();
				NetworkSender.Instance().sendFinish(true);
			}
			else {//正常协议处理
				byte[] newdata;
				//----------尝试解压Data开始----------
				if(ClientConfigure.useMrdCompress)
				{
					newdata = retData;
//					uint out_len = (uint)retData.Length;
//					Debug.Log("out_len:"+out_len);
//					uint in_len = MRDCompressLib.MRDCompressLib_GetDecompressSize(retData, out_len);
//					Debug.Log("in_len:"+in_len);
//					newdata = new byte[in_len];
//					uint new_len = MRDCompressLib.MRDCompressLib_Decompress(retData, out_len, newdata);
//					if ( in_len == new_len )
//					{
//						Debug.Log("Decompress Successed: from " + out_len + " bytes to " + new_len + " bytes");
//					}
//					else
//					{
//						Debug.Log("Decompress Failed");
//					}
				}else
				{
					newdata = retData;
				}
//				CSLogin login = new CSLogin();//Serializer.Deserialize<SCErrorMsg>(msData);
//				login.ParseFrom(newdata);
//				Debug.Log("login mid:"+login.Mid);
//				Debug.Log("login version:"+ login.Version);
				//----------尝试解压Data结束----------

				bool hadHandled = false;
				foreach(HTTPPacketHandler packetHandler in NetManager.packetHandlerList) {
					hadHandled |= packetHandler.handle((MessageID)retOpcode, newdata);
					if(hadHandled) {
						break;
					}
				} 
				Debug.LogWarning("reviced a packet. retOpcode = " + (MessageID)retOpcode);
				if(!hadHandled) {
					Debug.LogError("exception reviced a NO have Handler packet. retOpcode = " + retOpcode);
				} 
			}
		}

		public static void addPacket (int retOpcode, byte[] retData)
		{

			NetPacket pt = new NetPacket(retOpcode, retData);
			netPacketQueue.Enqueue(pt);
		}
		
		class NetPacket {
			public int opcode;
			public byte[] data;
			
			public NetPacket(int opcode, byte[] data){
				this.opcode = opcode;
				this.data = data;
			}
		}
	}
}

