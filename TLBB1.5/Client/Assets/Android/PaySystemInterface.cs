using UnityEngine;
using System.Collections;

/// <summary>
/// 游戏SDK调用入口类
/// </summary>
public class PaySystemInterface : MonoBehaviour
{
#if UNITY_ANDROID
	private const string SDK_JAVA_CLASS = "com.cyou.tlyd.android.tw.Payment";
	
	
	public static void doSdk(string func,string json)
	{
		onFuncCall("jniCall", func,json);
	}
	
	public static void onFuncCall(string func,params object[] args)
	{
		Debug.Log("Unity3D onFuncCall calling..."+func);

		using (AndroidJavaClass cls = new AndroidJavaClass(SDK_JAVA_CLASS))
		{
			cls.CallStatic(func, args);
		}
	}

#endif
}

