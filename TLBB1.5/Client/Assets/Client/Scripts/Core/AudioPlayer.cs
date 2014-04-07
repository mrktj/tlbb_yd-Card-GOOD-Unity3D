using UnityEngine;
using System.Collections;

public class AudioPlayer : MonoBehaviour {
	public string myUrl;
	public int myVersion;
	public AudioManager.SoundType myType;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void play(bool isloop,float delay = 0,float pitch = 1.0f){
		if(!audio.isPlaying && audio.clip!=null)
		{
			audio.pitch = pitch;
			audio.loop = isloop;
			if(delay==0)
			{
				audio.Play();
			}else
			{
				audio.PlayDelayed(delay);
			}
		}
	}
	public void pause()
	{
		if(audio.isPlaying && audio.clip!=null)
		{
			audio.Pause();
		}		
	}
	public void stop()
	{
		if(audio.isPlaying && audio.clip!=null)
		{
			audio.Stop();
		}
	}
	
	public void OnApplicationPause(bool status)
	{
		//add interrupt control if need.
	}
}
