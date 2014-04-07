using UnityEngine;
using System;
using System.Collections;

public class QuitAndroid : MonoBehaviour {
#if UNITY_ANDROID
	void Update()
	{
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			WebMediator.ShowExitPanel();
		}
	}
#endif
}
