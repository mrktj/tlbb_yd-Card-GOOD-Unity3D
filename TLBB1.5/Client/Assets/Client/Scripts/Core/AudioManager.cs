using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {
	public enum SoundType
	{
		BACKGROUND = 1,
		EFFECT = 2,
	}
	private static bool allowMusic;//音乐
	private static bool allowSound;//音效
	private Dictionary<string,AudioPlayer> players;
	
	private static AudioManager _instance;
	//注意：在逻辑类的Awake里需调用Instance.initGame()已保证可以单场景调试
	private string currentBGmusic = "";
	public static AudioManager Instance
	{
		get
		{
			if(!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(AudioManager)) as AudioManager;
				if(!_instance)
				{
					GameObject am = new GameObject("AudioManager");
					_instance = am.AddComponent(typeof(AudioManager)) as AudioManager;
				}
			}
			return _instance;			
		}

	}
	void Awake(){
		players = new Dictionary<string, AudioPlayer>();
		//-1:first time 
		if(PlayerPrefs.GetInt(SAVE_KEY_MUSIC,-1) == -1)
		{
			PlayerPrefs.SetInt(SAVE_KEY_MUSIC,1);
		}
		if(PlayerPrefs.GetInt(SAVE_KEY_SOUND,-1) == -1)
		{
			PlayerPrefs.SetInt(SAVE_KEY_SOUND,1);
		}		
		//0:not allow 1:allow
		allowMusic = (PlayerPrefs.GetInt(SAVE_KEY_MUSIC) == 1);
		allowSound = (PlayerPrefs.GetInt(SAVE_KEY_SOUND) == 1);

	}
	void OnDisable() {
		//clean
		foreach(AudioPlayer player in players.Values)
		{
			AssetBundleManager.Unload(player.myUrl,player.myVersion,true);
		}
		players.Clear();
	}
	void Start () {
		//for test :PlayEffectSound("aihenjiaojia");
		//PlayBackgroundMusic("aihenjiaojia",true);
	}
		//播放背景音乐
	public void PlayBackgroundMusic(string SoundName, bool bLoop, float fDelay)
	{
		PlaySound(SoundType.BACKGROUND, SoundName, bLoop, fDelay);
	}
	
	//播放背景音乐
	public void PlayBackgroundMusic(string SoundName, bool bLoop)
	{
		PlaySound(SoundType.BACKGROUND, SoundName, bLoop, 0);
	}
	
	//播放背景音乐
	public void PlayBackgroundMusic(string SoundName)
	{
		PlaySound(SoundType.BACKGROUND, SoundName, true, 0);
	}
		//播放音效
	public void PlayEffectSound(string SoundName , bool bLoop, float fDelay, float pitch)
	{
		PlaySound(SoundType.EFFECT, SoundName, bLoop, fDelay, pitch);
	}
	
	//播放音效
	public void PlayEffectSound(string SoundName , bool bLoop, float fDelay)
	{
		PlaySound(SoundType.EFFECT, SoundName, bLoop, fDelay, 1);
	}
	
	//播放音效
	public void PlayEffectSound(string SoundName , bool bLoop)
	{
		PlaySound(SoundType.EFFECT, SoundName, bLoop, 0, 1);
	}
	
	//播放音效
	public void PlayEffectSound(string SoundName , float pitch)
	{
		PlaySound(SoundType.EFFECT, SoundName, false, 0 ,pitch);
	}

	public void PlayEffectSound(string SoundName)
	{
		PlaySound(SoundType.EFFECT,SoundName,false);
	}
	
	public void PlaySound(SoundType type,string SoundName,bool isloop,float delay = 0,float pitch = 1.0f)
	{
		Debug.Log("play sound:"+SoundName);
		if(type == SoundType.BACKGROUND && allowMusic == false)
		{
			return;
		}
		if(type == SoundType.EFFECT && allowSound == false)
		{
			return;
		}
		//stop other background music
		if(type == SoundType.BACKGROUND)
		{
			AudioPlayer lastPlayer= getAudioPlayer(currentBGmusic);
			if(lastPlayer!=null &&lastPlayer.audio.isPlaying)
			{
				StopSound(currentBGmusic);
			}
			
		}
		
		AudioPlayer player = getAudioPlayer(SoundName);
		if(player!=null)
		{
			player.play(isloop,delay,pitch);
			//save current background music
			if(type == SoundType.BACKGROUND)
			{
				currentBGmusic = SoundName;
			}
		}else
		{
			StartCoroutine(ResourceManager.Instance.CreateAudioPlayer(SoundName,isloop,delay,pitch,type));
		}		
	}
	public void PauseSound(string SoundName)
	{
		AudioPlayer player = getAudioPlayer(SoundName);
		if(player!=null)
		{
			player.pause();
		}		
	}
	public void StopSound(string SoundName)
	{
		Debug.Log("stop sound:"+SoundName);
		AudioPlayer player = getAudioPlayer(SoundName);
		if(player!=null)
		{
			player.stop();
		}
	}
	
	public void addAudioPlayer(string SoundName,AudioPlayer player)
	{
		players.Add(SoundName,player);
	}
	
	public AudioPlayer getAudioPlayer(string SoundName)
	{
		AudioPlayer player = null;
		if(players.TryGetValue(SoundName,out player))
		{
			return player;
		}else
		{
			return null;
		}
	}
	private readonly string SAVE_KEY_MUSIC = "Music";
	private readonly string SAVE_KEY_SOUND = "Sound";
	public bool isAllowMusic()
	{
		return allowMusic;
	}
	public bool isAllowSound()
	{
		return allowSound;
	}
	public void setAllowMusic(bool isAllow)
	{
		allowMusic = isAllow;
		PlayerPrefs.SetInt(SAVE_KEY_MUSIC,allowMusic?1:0);
		if(!allowMusic)
		{
			stopAllMusic();
		}
	}
	public void setAllowSound(bool isAllow)
	{
		allowSound = isAllow;
		PlayerPrefs.SetInt(SAVE_KEY_SOUND,allowSound?1:0);
		if(allowSound)
		{
			stopAllEffect();
		}
	}
	public void stopAllMusic()
	{
		foreach(AudioPlayer player in players.Values)
		{
			if(player.myType == SoundType.BACKGROUND)
			{
				player.stop();
			}
		}
	}
	public void stopAllEffect()
	{
		foreach(AudioPlayer player in players.Values)
		{
			if(player.myType == SoundType.EFFECT)
			{
				player.stop();
			}		
		}
	}
}
