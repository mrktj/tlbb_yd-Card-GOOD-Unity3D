using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace card.net
{
	public class UIListener
	{
		public bool isBlock;
        private static UIListener m_instance;
        public static UIListener Instance()
        {
            if (null == m_instance)
            {
                m_instance = new UIListener();
            }

            return m_instance;
        }

        public void OnReceiveMsg(bool bSuccess)
        {
//            if (msgQueue.Count == 0) return;
//            OnReceive fun = msgQueue.Dequeue();
			if(UIListener.Instance().isBlock)
			{
//					Debug.Log("OnReceiveMsg->removeMessage");
                if (GameManager.Instance.sceneName.Equals(Utils.UI_NAME_Login))
                {
                    //此处代码用来解决登录游戏时要发送好几个包导致登录loading会产生闪烁
                    if ((int)GuideManager.Instance.currentStep == 0)
                    {
                        BoxManager.removeMessage();
                    }
                 
                }
                else
                {
                    BoxManager.removeMessage();
                }
                //BoxManager.removeMessage();
				UIListener.Instance().isBlock = false;
			}
            if (onReceive != null && bSuccess)
            {
                	onReceive(bSuccess);
            }
        }

        public void AddReciever(OnReceive fun)
        {
//          msgQueue.Enqueue(fun);
			onReceive = fun;
        }

        public void CleanAll()
        {
			isBlock = false;
			onReceive =null;
//            msgQueue.Clear();
        }

//        public OnReceive DequeueReciever()
//        {
//            if (msgQueue.Count == 0) return null;
//            return msgQueue.Dequeue();
//        }
        public delegate void OnReceive(bool bSuccess);
        private OnReceive onReceive;

//        private Queue<OnReceive> msgQueue = new Queue<OnReceive>();
	}
	
}


