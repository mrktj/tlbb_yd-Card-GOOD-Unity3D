using UnityEngine;
using System.Collections;

public class LoadingCoverKit : MonoBehaviour {

	public static string tipWord;
	public UILabel tipLabel;
	public UISlider slider;
	public static float predictTime = 1f;
	private float progress = 0.9f;
	
	void Update () 
	{
		if(progress < 0.99f)
		{
			progress += Time.smoothDeltaTime / predictTime;
		}
		slider.sliderValue = progress;
	}
	public static void ShowLoadingCover(string tipWord)
	{
		GameObject coverObj = GameObject.FindGameObjectWithTag("LoadingController");
		if(coverObj != null)
		{
			LoadingCoverKit lck = coverObj.GetComponent<LoadingCoverKit>();
			if(lck != null)
			{
				lck.gameObject.transform.localPosition = new Vector3(0f,0f,-100f);
				if(LoadingSliderKit.progress > 0f)
				{
					lck.progress = 0.9f;
				}
				else
				{
					lck.progress = 0f;
				}
				//LoadingSliderKit.progress;
				lck.tipLabel.text = tipWord;
				lck.enabled = true;
			}
		}
	}
	
	public static void CoverOver()
	{
		GameObject coverObj = GameObject.FindGameObjectWithTag("LoadingController");
		if(coverObj != null)
		{
			LoadingCoverKit lck = coverObj.GetComponent<LoadingCoverKit>();
			if(lck != null)
			{
				lck.gameObject.transform.localPosition = new Vector3(1500f,0f,-50f);
				//LoadingSliderKit.progress = 0f;
				lck.enabled = false;
			}
		}
	}
}
