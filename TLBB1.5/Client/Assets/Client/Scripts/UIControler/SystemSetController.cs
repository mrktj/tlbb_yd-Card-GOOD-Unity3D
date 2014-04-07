using UnityEngine;
using System.Collections;

/* Create at 7/31/2013 by Jack Wen
 * */
public class SystemSetController : MonoBehaviour {
	
	public GameObject mainUILogic;
	//public GameObject soundManager;
	public UICheckbox music;
	public UICheckbox sound;
	
//	private bool musicPlay;
//	private bool soundPlay;
	
	void Awake(){
		mainUILogic = GameObject.Find("MainUILogic");
		//music.isChecked = (PlayerPrefs.GetInt("Music",-1) == 1) ? true:false;
		//sound.isChecked = (PlayerPrefs.GetInt("Sound",-1) == 1) ? true:false;
		//music.isChecked = AudioManager.Instance.isAllowMusic();//soundManager.GetComponent<AudioManager>().music;
		//sound.isChecked = AudioManager.Instance.isAllowSound();//SoundManager.GetComponent<AudioManager>().sound;
//		musicPlay = music.isChecked;
//		soundPlay = sound.isChecked;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnEnable(){
		music.isChecked = (PlayerPrefs.GetInt("Music",-1) == 1) ? true:false;
		sound.isChecked = (PlayerPrefs.GetInt("Sound",-1) == 1) ? true:false;
//		musicPlay = music.isChecked;
//		soundPlay = sound.isChecked;
	}
	
	public void SetMusic(){
		//soundManager.GetComponent<AudioManager>().SetMusic(!musicPlay);
//		musicPlay = music.isChecked;
		//int musicTemp = musicPlay?1:0;
		//PlayerPrefs.SetInt("Music",musicTemp);
	}
	
	public void SetSound(){
		//soundManager.GetComponent<AudioManager>().SetSound(!soundPlay);
//		soundPlay = sound.isChecked;
		//int soundTemp = soundPlay?1:0;
		//PlayerPrefs.SetInt("Sound",soundTemp);
	}
	
	public void Cancel(){
		mainUILogic.SendMessage("OnHelpWindow");
	}
	
	public void SaveChange(){
		AudioManager.Instance.setAllowMusic(music.isChecked);
		if(music.isChecked)
		{
			AudioManager.Instance.PlayBackgroundMusic("background");
		}
		//soundManager.GetComponent<AudioManager>().SetMusic(musicPlay);
//		AudioController.Play(musicPlay);
//		int musicTemp = musicPlay?1:0;
//		PlayerPrefs.SetInt("Music",musicTemp);
		AudioManager.Instance.setAllowSound(sound.isChecked);
//		soundManager.GetComponent<AudioManager>().SetSound(soundPlay);
//		AudioController.Play(musicPlay);
//		int soundTemp = soundPlay?1:0;
//		PlayerPrefs.SetInt("Sound",soundTemp);
		
		mainUILogic.SendMessage("OnHelpWindow");
	}
}
