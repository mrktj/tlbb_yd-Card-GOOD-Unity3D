using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GCGame.Table;
using xjgame.message;
using System.IO;

/*
 * By cb, 用来读取，删除，缓存，再生资源，方便日后改为assetbundle模式
 * 
 *
 */
public class ResourceManager : MonoBehaviour
{
	//不同平台下StreamingAssets的路径不同
	public static readonly string PathURL =
#if UNITY_EDITOR
        "file://" + Application.dataPath + "/StreamingAssets/";
#elif UNITY_ANDROID
		"jar:file://" + Application.dataPath + "!/assets/";
        //"file://" +  Application.persistentDataPath + "/TableData/;
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
    "file://" + Application.dataPath + "/StreamingAssets/";
#elif UNITY_IPHONE
      "file://" +  Application.dataPath + "/Raw/";
#else
        string.Empty;
#endif
    public int load_map_num;
	private static ResourceManager _instance;

	public static ResourceManager Instance {
		get {
			if (!_instance) {
				_instance = GameObject.FindObjectOfType (typeof(ResourceManager)) as ResourceManager;
				if (!_instance) {
					GameObject gm = new GameObject ("ResourceManager");
					_instance = gm.AddComponent (typeof(ResourceManager)) as ResourceManager;
				}
			}
			return _instance;			
		}

	}

	void Awake ()
	{
        load_map_num = 0;
		//现在每次切换
//		DontDestroyOnLoad(transform.gameObject);
	}

	public GameObject LoadPopUp (string boxName)
	{
		return LoadGameObject ("PopUp/" + boxName);
	}

	public GameObject LoadWindow (string windowName)
	{
		return LoadGameObject ("Windows/" + windowName);
	}
	
	public GameObject loadWidget (string widgetName)
	{
		return LoadGameObject ("Widgets/" + widgetName);
	}

    public GameObject loadEffect(string effectName)
    {
        return LoadGameObject("EffectParticle/" + effectName);
    }

	public GameObject LoadGameObject (string objPath)
	{
		if (GameManager.isQuiting)
			return null;
		else {
			Object res = null;
			
			res = LoadObject (objPath);
			if (res == null) {
				return null;
			}
			
			
			GameObject obj = Instantiate (res) as GameObject;
			obj.transform.position = Vector3.zero;
			obj.transform.localScale = Vector3.one;
			return obj;
		}
	}
	
	public Object LoadObject (string objPath)
	{
		if (GameManager.isQuiting)
			return null;
		else {
			Object res = Resources.Load (objPath);
			if (res == null) {
				Debug.LogError (objPath + " IS NOT EXIST");
				return null;
			}
			
			return res;
		}
	}
	
	public AudioClip LoadSound (string soundName)
	{
		if (GameManager.isQuiting)
			return null;
		else {
			Object res = null;
			res = LoadObject ("Sounds/" + soundName);
			if (res == null) {
				return null;
			}
			
			AudioClip audClip = Instantiate (res) as AudioClip;
			return audClip;
		}
	}
	
	public SCBattleData LoadGuideBattle ()
	{
		if (GameManager.isQuiting)
			return null;
		else {
			TextAsset battleAsset = Resources.Load ("Guide/ss", typeof(TextAsset)) as TextAsset;
			SCBattleData battleData = new SCBattleData ();//Serializer.Deserialize<SCBattleData>(msData);
			battleData.ParseFrom (battleAsset.bytes);
			return battleData;
		}
//		Obj_MyselfPlayer.GetMe().SetBattleData(battleData);
//		Debug.Log("battleData.Battle.roundsList.Count:"+battleData.Battle.roundsList.Count);
//		Debug.Log("battleData.Battle.Win_idx:"+battleData.Battle.Win_idx);
//		Debug.Log("battleData.Battle.MissionID:"+battleData.Battle.MissionID);
//		Debug.Log("battleData.Battle.roundsCount:"+battleData.Battle.roundsCount);
//		Debug.Log("battleData.Battle.dropsList.Count:"+battleData.Battle.dropsList.Count);
//		Debug.Log("battleData.Battle.Add_exp:"+battleData.Battle.Add_exp);
		
	}

	public UIAtlas LoadCardAtlas (string atlasName)
	{
		if (GameManager.isQuiting)
			return null;
		else
			return Resources.Load ("KaPai/" + atlasName, typeof(UIAtlas)) as UIAtlas;
	}
//	public Texture LoadCardTexture(UITexture uitex, string cardName)
//	{
//		if(GameManager.isQuiting)
//			return null;
//		else
//		{
//			Object tex = null;
//			StartCoroutine(LoadAssetBundle("banshenxiang/",cardName,uitex));
//			return tex as Texture;
//		}
//	}
	
	public Texture LoadShopBottomPicTexture (string picName)
	{
		if (GameManager.isQuiting)
			return null;
		return Resources.Load ("shangpubeijing/" + picName, typeof(Texture)) as Texture;
	}

	public IEnumerator CreateAudioPlayer (string soundName, bool isloop, float delay, float pitch, AudioManager.SoundType type)
	{
		
		
		if (soundName != null && soundName != "") {

			string path = PathURL + "Sounds/" + soundName + ".assetbundle";
			int version = 0;
			//test start
//			Debug.Log("sound exist:"+System.IO.File.Exists(path));
//			string combinepath = "file://" + System.IO.Path.Combine(Application.persistentDataPath, "Sounds/"+soundName);
//			Debug.Log("sound exist:"+System.IO.File.Exists(combinepath));
//			Debug.Log("Sounds/"+soundName);
//			Object go = Resources.Load("Sounds/"+soundName) ;
//			GameObject obj =  Instantiate(go) as GameObject;
//			obj.transform.parent = AudioManager.Instance.transform;
//			AudioPlayer newPlayer = obj.GetComponent<AudioPlayer>();
//			newPlayer.myUrl = path;
//			newPlayer.myVersion = version;
//			AudioManager.Instance.addAudioPlayer(soundName,newPlayer);		
//			yield return null;
			//test end
			
			AssetBundle bundle = AssetBundleManager.getAssetBundle (path, version);
			if (!bundle) {
				Debug.Log ("start download:" + path + " version:" + version.ToString ());
				yield return StartCoroutine(AssetBundleManager.downloadAssetBundle(path,version));				
				bundle = AssetBundleManager.getAssetBundle (path, version);
			}
			if (!AudioManager.Instance.getAudioPlayer (soundName)) {
				Object go = bundle.mainAsset;
				GameObject obj = Instantiate (go) as GameObject;
				obj.transform.parent = AudioManager.Instance.transform;
				AudioPlayer newPlayer = obj.GetComponent<AudioPlayer> ();
				newPlayer.myUrl = path;
				newPlayer.myVersion = version;
				newPlayer.myType = type;
				Debug.Log ("Create Sound Player:" + "url=" + path + "version=" + version + "type=" + type);
				AudioManager.Instance.addAudioPlayer (soundName, newPlayer);			
				
			}
			AudioManager.Instance.PlaySound (type, soundName, isloop, delay, pitch);
			
			//bundle.Unload(false);
			AssetBundleManager.Unload (path, version, false);
		}
	}

	public Texture defaultTex = null;// = new Texture();
	void OnEnable ()
	{
		if (!defaultTex) {
			defaultTex = new Texture ();
			StartCoroutine (LoadDefaultCardTexture ());
		}
	}

	public IEnumerator LoadDefaultCardTexture ()
	{
		string path = PathURL + "banshenxiang/shadow.assetbundle";
		int version = 0;
		AssetBundle bundle = AssetBundleManager.getAssetBundle (path, version);
		if (!bundle) {
			Debug.Log ("start download:" + path + " version:" + version.ToString ());
			yield return StartCoroutine(AssetBundleManager.downloadAssetBundle(path,version));				
			bundle = AssetBundleManager.getAssetBundle (path, version);
		}
//			Debug.Log("set Texture:"+path+version.ToString());
	
		defaultTex = bundle.mainAsset as Texture;
//			uitex.mainTexture = go;
//			uitex.shader = Shader.Find("Unlit/Transparent Colored");
//			if (!isScale)
//				uitex.MakePixelPerfect();
		
	}
	
	//By 王明磊 isScale 是否保持缩放 true = 保持缩放 2013.10.12
	public IEnumerator SetCardTexture (UITexture uitex, string texName, bool isScale)
	{
		if (uitex != null && texName != null && texName != "") {
			
//			string path = PathURL + "banshenxiang/" + texName + ".assetbundle";
			string path = "file://" + Application.persistentDataPath + "/Textures/" + texName + ".assetbundle";
			if (!File.Exists(Application.persistentDataPath + "/Textures/" + texName + ".assetbundle"))
			{
				path = PathURL + "banshenxiang/" + texName + ".assetbundle";
			}
			int version = 0;
			AssetBundle bundle = AssetBundleManager.getAssetBundle (path, version);
			
			if (!bundle) {
				uitex.mainTexture = defaultTex;
				uitex.shader = Shader.Find ("Unlit/Transparent Colored");
				if (!isScale)
					uitex.MakePixelPerfect ();
				Debug.Log ("start download:" + path + " version:" + version.ToString ());
				yield return StartCoroutine(AssetBundleManager.downloadAssetBundle(path,version));				
				bundle = AssetBundleManager.getAssetBundle (path, version);
			}
			Debug.Log ("set Texture:" + path + version.ToString ());
	
			Texture go = bundle.mainAsset as Texture;
			uitex.mainTexture = go;
			uitex.shader = Shader.Find ("Unlit/Transparent Colored");
			if (!isScale)
				uitex.MakePixelPerfect ();
		}

	}

	public IEnumerator AsyncLoadBattleBackground (GameObject bg_pos, string bg_lib, string bg_name)
	{
		if (bg_pos == null || string.IsNullOrEmpty (bg_lib) || string.IsNullOrEmpty (bg_name)) {
			Debug.LogError ("LoadBattleBackground error!");
			yield break;
		}

		string path = PathURL + "BattleBackground/" + bg_lib + ".assetbundle";
		int version = 0;
		AssetBundle bundle = AssetBundleManager.getAssetBundle (path, version);

		if (bundle == null) {
			yield return StartCoroutine(AssetBundleManager.downloadAssetBundle(path, version));
			bundle = AssetBundleManager.getAssetBundle (path, version);
		}

		if (bundle == null) {
			Debug.LogError ("LoadBattleBackground(), bg_lib = " + bg_lib);
			yield break;
		}
		GameObject go = bundle.mainAsset as GameObject;
		tk2dSpriteCollectionData collection = go.GetComponent<tk2dSpriteCollectionData> ();
		tk2dSprite.AddComponent (bg_pos, collection, bg_name);
        load_map_num++;
	}

	public tk2dSpriteCollectionData SyncLoadBattleBackground (string bg_lib)
	{
		if (string.IsNullOrEmpty (bg_lib)) {
			Debug.LogError ("LoadBattleBackground error! bg_lib == null");
			return null;
		}

		string path = PathURL + "BattleBackground/" + bg_lib + ".assetbundle";
		int version = 0;
		AssetBundle bundle = AssetBundleManager.getAssetBundle (path, version);

		if (bundle == null) {
			bundle = AssetBundleManager.syncDownloadAssetBundle (path, version);
		}

		if (bundle == null) {
			Debug.LogError ("LoadBattleBackground() failed, bg_lib = " + bg_lib);
			return null;
		}
		GameObject go = bundle.mainAsset as GameObject;
		tk2dSpriteCollectionData collection = go.GetComponent<tk2dSpriteCollectionData> ();

		return collection;
	}

//		Debug.Log("dataPath:"+Application.dataPath);
//		Debug.Log("persistentDataPath:"+Application.persistentDataPath);
//		Debug.Log("streamingAssetsPath:"+Application.streamingAssetsPath);
//		Debug.Log("temporaryCachePath:"+Application.temporaryCachePath);
//		Debug.Log("path:" + path);
//		WWW download = WWW.LoadFromCacheOrDownload(path,0);//new WWW(path);
//		yield return download;
//		if(download.error!=null)
//		{
//			Debug.Log("error:"+download.error);
//		}
//		AssetBundle assetBundle = download.assetBundle;
//		if(assetBundle!=null)
//		{
//			//go = assetBundle.Load(name
//			Texture go = assetBundle.mainAsset as Texture;
//			//Instantiate(go);
//			uitex.mainTexture = go;
//			uitex.shader = Shader.Find("Unlit/Transparent Colored");
//			uitex.MakePixelPerfect();
//			assetBundle.Unload(false);
//		}else
//		{
//			Debug.Log("Couldn't load resource: "+path);
//		}
//	}
//	IEnumerator DownloadTexture(string path,int version)
//	{
//		yield return StartCoroutine(AssetBundleManager.downloadAssetBundle(path,version));
//		bundle = AssetBundleManager.getAssetBundle(path,version);
//	}	
	public void Clean ()
	{
		Debug.Log ("clearObjPool");
		Resources.UnloadUnusedAssets ();
//		foreach(Object res in objectPool)
//		{
//			Resources.UnloadAsset(res);
//		}
	}
	
}

