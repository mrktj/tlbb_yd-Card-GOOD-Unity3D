using UnityEngine;
using System.Collections;

public class LogoReslution : MonoBehaviour
{
#if UNITY_ANDROID
	private const string version_time = "09_26_2110";
	private bool isSet;
	//public float LARGE_R = 1.28267689f;
	//public float SMALL_R = 0.942595f;
	// Use this for initialization
	void Start () {
	    isSet = false;
		QuitApplication.hideQuitPrefs();
	    //version_time = System.DateTime.Now.Month+"_"+System.DateTime.Now.Day+"_"+System.DateTime.Now.Hour+"_"+System.DateTime.Now.Minute;
	}
	// Update is called once per frame
	void Update () {
		// press back key quit
	    if(isSet)
			return;
		// 设置UI Root
		GameObject UIRoot = GameObject.Find("UI Root (2D)");
		if(UIRoot != null){
			UIRoot uiroot = UIRoot.GetComponent<UIRoot>();
			if(uiroot != null){
				// (320 + 2240) / 2 = 1280
				uiroot.manualHeight = 1024;
				uiroot.maximumHeight = 2240;
				uiroot.minimumHeight = 320;
			}
		}
		// 计算分辨率
		int h = Screen.height;
		int w = Screen.width;

		float r = 1.0f;
		
		if(h < w){
			int tmp = h;
			h = w;
			w = tmp;
		}

		/*if(w >= 768)
		{
			int rw = 768;
			int rh = 1280;
			
			float r1 = (float)rw/w;
			float r2 = (float)rh/h;
			
			r = r1 > r2 ? r2 : r1;
			camera.orthographicSize = r*0.98f;
		}
		else
		{*/
			int rw = 768;
			int rh = 1280;

			float r1 = (float)rw/w;
			float r2 = (float)rh/h;
		
			r = r1 > r2 ? r2 : r1;
			//if(w == 1080){
			//	r += 0.105f;
			//}
			camera.orthographicSize = r*0.98f;
		//}
		
		
		/*int h = Screen.height;
		int w = Screen.width;

		if(w >= 1280 || h >= 1280)
		{
			float rh = 1.0f;
			float rw = 1.0f;

			if(h > w){
				rh = 1208.0f / h;//参考1280和1136
				rw = 768.0f / w;
			}
			else
			{
				rh = 1208.0f / w;
				rw = 768.0f / h;
			}
			
			if(rh > rw){
			    camera.orthographicSize = rw*0.95f*LARGE_R;
			}else{
				camera.orthographicSize = rh*0.95f*LARGE_R;
			}
			Debug.Log("--------------camera.orthographicSize: "+camera.orthographicSize+",rw: "+rw+",rh: "+rh);
		}
		else
		{
			float hw = 1.0f;
			
			if(h > w){
			    hw = (float)h/w;
			}
			else
			{
			    hw = (float)w/h;
			}
			if(hw <= 1.28f)
			{
				hw = 0.99f;
			}
			else if(hw > 1.28f && hw <= 1.4f)
			{
				hw = 1.05f;
			}
			else if(hw > 1.4f && hw <= 1.45f)
			{
				hw = 1.13f;
			}
			else if(hw > 1.45f && hw <= 1.57f)
			{
				hw = 1.18f;
			}
			else if(hw > 1.57f && hw <= 1.607f)
			{
				hw = 1.23f;
			}
			else if(hw > 1.607f && hw <= 1.718f)
			{
				hw = 1.28f;
			}
			else if(hw > 1.718f && hw <= 1.828f) 
			{
				hw = 1.33f;
			}
			else
			{
				hw = 1.42f;
			}
			camera.orthographicSize = hw * 0.92f*SMALL_R;
			Debug.Log("----------------camera.orthographicSize: "+camera.orthographicSize+",hw: "+hw);
		}*/
		
		isSet = true;
	}
	
#endif
}
