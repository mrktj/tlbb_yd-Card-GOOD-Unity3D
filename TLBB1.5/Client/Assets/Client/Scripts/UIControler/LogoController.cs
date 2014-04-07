using UnityEngine;
using System.Collections;
using System.IO;
using card;

public class LogoController : MonoBehaviour {
	private float startTime = 0;
	private int SHOW_TIME = 3;
	// Use this for initialization
#if UNITY_EDITOR
	void Start(){
		Application.runInBackground = true;
	}
	// Update is called once per frame
	void Update () {

		if(Time.time - startTime > SHOW_TIME){
			Application.LoadLevel(Utils.UI_NAME_Login);
			
		}
	}
#elif UNITY_ANDROID
	private bool isStart = false;
	void Awake(){
		WebMediator.Install();
		SDKManager.Install();
		Invoke("LoadResource",SHOW_TIME);
	}
	
	void  LoadResource()
	{
		
		Debug.Log("LoadResource");
		if (!GooglePlayDownloader.RunningOnAndroid())
		{
			//GUI.Label(new Rect(10, 10, Screen.width-10, 20), "Use GooglePlayDownloader only on Android device!");
			Debug.Log("Use GooglePlayDownloader only on Android device!");
			return;
		}
		
		string expPath = GooglePlayDownloader.GetExpansionFilePath();
		if (expPath == null)
		{
				//GUI.Label(new Rect(10, 10, Screen.width-10, 20), "External storage is not available!");
			Debug.Log("External storage is not available!");
		}
		else
		{
			string mainPath = GooglePlayDownloader.GetMainOBBPath(expPath);
			string patchPath = GooglePlayDownloader.GetPatchOBBPath(expPath);
			Debug.Log("mainPath:"+mainPath);
			Debug.Log("patchPath:"+patchPath);
			
			//GUI.Label(new Rect(10, 10, Screen.width-10, 20), "Main = ..."  + ( mainPath == null ? " NOT AVAILABLE" :  mainPath.Substring(expPath.Length)));
			//GUI.Label(new Rect(10, 25, Screen.width-10, 20), "Patch = ..." + (patchPath == null ? " NOT AVAILABLE" : patchPath.Substring(expPath.Length)));
			if (mainPath == null)
				//if (GUI.Button(new Rect(10, 100, 100, 100), "Fetch OBBs"))
			{
				GooglePlayDownloader.SetMainOBBInfo(ClientConfigure.getResourceURL());
				//GooglePlayDownloader.SetMainOBBFileName("file name");
				GooglePlayDownloader.FetchOBB();
				StartCoroutine(WaitForObbDownLoadOver());
			}
			else
			{
				//SDKManager.GetInstance();
				
				Application.LoadLevel(Utils.UI_NAME_Login);
			}
					
			     
		}
	}
	
	protected IEnumerator WaitForObbDownLoadOver()
	{		
		string mainPath;
		do
		{
			yield return new WaitForSeconds(0.5f);
			string expPath = GooglePlayDownloader.GetExpansionFilePath();
			Debug.Log("expPath is : " + expPath);
			mainPath = GooglePlayDownloader.GetMainOBBPath(expPath);
			Debug.Log("waiting mainPath "+mainPath);
		}
		while( mainPath == null);
		
		string[] files = Directory.GetFiles("/data/data/com.cyou.tlyd.android.tw/databases/");
		
		for(int i=0;i<files.Length;i++){
			//delete obb downloads' info;
			if(files[i].Contains("DownloadsDB")){
				Debug.Log("delete: "+files[i]);
				File.Delete(files[i]);
			}
		}
		
		Application.LoadLevel(Utils.UI_NAME_Login);
	}
	
#elif UNITY_IPHONE
	void Awake () {
		SDKManager.Install();
	}
	
	// Update is called once per frame
	void Update () {
		startTime += Time.deltaTime;
		if(startTime > SHOW_TIME)
		{
			Application.LoadLevel(Utils.UI_NAME_Login);
		}
	}
#endif	
}
