
using System.Collections; 
using System.ComponentModel ; 
using System; 
using System.IO;
using System.Text; 
using System.Net; 
using System.Net.Sockets;
using System.Security;
////using Module.Log; 
using xjgame.message;
using UnityEngine;

namespace card.net
{
	public class HTTPClientAPI
	{
		
		private static String url = "http://"+ClientConfigure.getUrl()+"/XJGameServer/s";//"/NNJD/s";
		public static Uri uri = new Uri(url);
		private const String KEY_GAME_SESSION = "XJGame-VALUEID";  
		
		private static String sessionId = "";
		public static void cleanSessionId()
		{
			sessionId = "";
		}
        public static uint SendByPost(String json)
        { 
            try
            { 
				WebClient client = new WebClient(); 
				String ret = client.UploadString(url, json); 
				Debug.LogError(ret);
            }
            catch (SocketException e)
            {
            	Debug.LogError(e.ToString());
            }
            return 0xFFFFFFFF;
        }
//		const int output_max_len = 2000;
//		static byte[] output_buffer = new byte[output_max_len];
		public static void SendByProtoBuf(PacketDistributed packet)
        { 
			WebClient client = null;
            try
            {

				int opcode = (int)packet.getMessageID();
				byte[] data = packet.ToByteArray();
				Debug.Log("send packet opcode[" + packet.getMessageID() + "] data.Length=" + data.Length); 
				byte[] output;
				if(ClientConfigure.useMrdCompress)
				{
					output = data;
				}else
				{
					output = data;
				}
				//------------尝试接入消息包压缩库结束-----------
            	client = new WebClient(); 
				client.Headers.Set("opcode", "" + opcode);
				client.Headers.Set(KEY_GAME_SESSION, sessionId);
                Debug.Log("sessionId = " + sessionId);
				client.UploadDataCompleted += new UploadDataCompletedEventHandler(uploadComplate);

                client.UploadDataAsync(uri, output); 
				
				Debug.Log("Send opcode = " + client.Headers.ToString());
            }
			catch (WebException we) {//超时
				Debug.LogError(we.ToString());
				if(client!=null)
				{
					client.CancelAsync();
				}
				NetworkSender.Instance().sendFinish(false);
			}
            catch (SocketException e)
            {
            	Debug.LogError(e.ToString());
				NetworkSender.Instance().sendFinish(false);
            } 
        }

		static void uploadComplate(System.Object obj, UploadDataCompletedEventArgs args) { 
			WebClient clientObj = (WebClient) obj;
			byte[] retData = args.Result;
			
			if(retData == null) {
				//TODO 网络错误的处理
				clientObj.CancelAsync();
				NetworkSender.Instance().sendFinish(false);
				Debug.LogError("net error:" + args.Error.Message);
//				BoxManager.showMessage("请重新登录");
//				UIEventListener.Get(BoxManager.getYesButton()).onClick += returnLoginScene;
//				UIListener.Instance().CleanAll();
				return;
			}

			sessionId = clientObj.ResponseHeaders.Get(KEY_GAME_SESSION); 
			string opcodeStr = clientObj.ResponseHeaders.Get("opcode");
				int retOpcode = -1;
				
				if(Int32.TryParse(opcodeStr, out retOpcode)) {
					NetManager.addPacket(retOpcode, retData);
				}else {
					NetworkSender.Instance().sendFinish(false);
					//exception 收到一个没有opcode的包
					Debug.LogError("exception reviced a NO have opcode packet");
				}
		}
        
	}
}
