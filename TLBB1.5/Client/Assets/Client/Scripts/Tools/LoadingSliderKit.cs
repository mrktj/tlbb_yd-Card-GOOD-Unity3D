using UnityEngine;
using System.Collections;

public class LoadingSliderKit : MonoBehaviour {

	public static string sceneNameToLoad;
	public static string tipWord;
	public UILabel tipLabel;
	//private AsyncOperation asyncOp = null;
	public UISlider slider;
	public static float predictTime = 1f;
	public static float progress = 0f;
	//private const string loadingSceneName = "loading";
	void Start () 
	{
//		if(Application.loadedLevelName == loadingSceneName)
//		{
		tipLabel.text = tipWord;
		Invoke("LoadDelay",0.3f);
		//}
	}
	
	void Update () 
	{
		if(progress < 0.89f)
		{
			progress += Time.smoothDeltaTime / predictTime;
//			if(asyncOp != null)
//			{
//				progress = Mathf.Max(progress,asyncOp.progress);
//			}
		}
		slider.sliderValue = progress;
	}
	public static void AsycLoadScene(string sceneName,string tips)
	{
		sceneNameToLoad = sceneName;
		tipWord = tips;
		ResourceManager.Instance.Clean();
		progress = 0f;
		Application.LoadLevel(Utils.UI_NAME_LOADING);
	}
	
	public static void AsycLoadScene(string sceneName)
	{
		sceneNameToLoad = sceneName;
		tipWord = RollingNotice.GetRandomNotice();
		Application.LoadLevel(Utils.UI_NAME_LOADING);
	}
	
	private void LoadDelay()
	{
		//asyncOp = GameManager.Instance.async;
		GameManager.Instance.LoadLevel(sceneNameToLoad);
		
		//asyncOp = Application.LoadLevelAsync(sceneNameToLoad);
	}
}
