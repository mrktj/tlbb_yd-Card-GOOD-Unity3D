using UnityEngine;
using System.Collections;

public class BattleReslution : MonoBehaviour {
#if UNITY_ANDROID
	private bool isSet;
	void Awake(){
		if(isSet)
			return;
		// 设置UI Root
		GameObject UIRoot = GameObject.Find("UI Root (2D)");
		if(UIRoot != null){
			UIRoot uiroot = UIRoot.GetComponent<UIRoot>();
			if(uiroot != null){
				// (1024 + 1536) / 2 = 1280
				uiroot.manualHeight = 1024;
				uiroot.maximumHeight = 1536;
				uiroot.minimumHeight = 320;
			}
		}
		// 计算分辨率
        int h = Screen.height;
		int w = Screen.width;
		float r = 1.0f;
		/*float rmax = 1.779f;
		float rmin = 1.333f;
		float roff = 0.13f;// 1~1.13
		if(h > w){
		    r = (float)h/w;
		}else{
		    r = (float)w/h;
		}
		camera.orthographicSize = (1+(r - rmin)*roff/(rmax - rmin))*0.93f*r;
		*/

		int rw = 620;
		int rh = 1080;
		
		if(h < w){
			int tmp = h;
			h = w;
			w = tmp;
		}

		float r1 = (float)rw/w;
		float r2 = (float)rh/h;
		
		r = r1 > r2 ? r1 : r2;

		if(r > 1.06f)
		{
		    r = 1.06f;
	 	}
		else if(r < 1.03f)
		{
			r = 1.03f;
		}
		camera.orthographicSize = r;

		isSet = true;
	}
/*	
	// Use this for initialization
	void Start () {
	    isSet = false;
	}
	
	// Update is called once per frame
	void Update () {	
	    if(isSet)
			return;
		
        int h = Screen.height;
		int w = Screen.width;
		float rmax = 1.779f;
		float rmin = 1.333f;
		float roff = 0.13f;// 1~1.13
		float hw = 1.0f;
		if(h > w){
		    hw = (float)h/w;
		}else{
		    hw = (float)w/h;
		}
		camera.orthographicSize = 1+(hw - rmin)*roff/(rmax - rmin);

		isSet = true;
	}
*/
#endif
}
