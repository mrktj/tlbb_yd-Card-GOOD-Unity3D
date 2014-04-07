using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using card.net;
using GCGame.Table;
public class AtlasManager : MonoBehaviour {

	private static AtlasManager _instance;
	public static AtlasManager Instance
	{
		get
		{
			if(!_instance)
			{
				_instance = GameObject.FindObjectOfType(typeof(AtlasManager)) as AtlasManager;
				if(!_instance)
				{
					GameObject gm = new GameObject("AtlasManager");
					_instance = gm.AddComponent(typeof(AtlasManager)) as AtlasManager;
				}
				
			}
			return _instance;			
		}

	}

	private Dictionary<string,UIAtlas> atlasPool = null;
	private Dictionary<string,Texture> texPool = null;
//	enum SPRITE_TYPE
//	{
//		HEAD,
//		BODY,
//	}
	//设置带框半身像 by id
	public bool setHeadByTempletID(UISprite sprite,int templetID)
	{
		return setHeadName(sprite,TableManager.GetAppearanceByID(TableManager.GetCardByID(templetID).Appearance).HeadIcon);
	}
	//设置半身像 by id
	public void setBodyByTempletID(UITexture tex,int templetID)
	{
//		Debug.Log("setBodyByTempletID:"+templetID);
		SetBodyName(tex,TableManager.GetAppearanceByID(TableManager.GetCardByID(templetID).Appearance).BodyIcon);
	}
	//设置带框半身像 by name
	public bool setHeadName(UISprite sprite,string spriteName)
	{
		return SetSprite(sprite,spriteName/*,SPRITE_TYPE.HEAD*/);
	}
	//设置半身像 by name
	public void SetBodyName(UITexture tex,string texName)
	{
		Debug.Log("SetBodyName:"+texName);
//		Debug.LogError("GetInstanceID:"+tex.GetInstanceID());
//		return SetTexture(tex,texName/*,SPRITE_TYPE.BODY*/);
		//TODO: add a default texture.
//		tex.mainTexture = null;
		StartCoroutine(ResourceManager.Instance.SetCardTexture(tex,texName,false));
	}
	
	public IEnumerator SetBodyNameWithShader (UITexture tex, string texName, string shaderName)
	{
		yield return StartCoroutine(ResourceManager.Instance.SetCardTexture(tex,texName,false));
		tex.shader = Shader.Find(shaderName);
	}
	
	//王明磊 保持缩放
	public void SetBodyNameRemainScale (UITexture tex, string texName)
	{
		StartCoroutine(ResourceManager.Instance.SetCardTexture(tex,texName,true));
	}
	
	public bool SetShopBottomTexture (UITexture tex, string texName)
	{
		if(tex==null || texName == null || texName == "")
		{
			return false;
		}
		if(texPool==null)
		{
			texPool = new Dictionary<string, Texture>();
		}

		if(!texPool.ContainsKey(texName))
		{
			Texture newTex = ResourceManager.Instance.LoadShopBottomPicTexture(texName);
			if(newTex==null)
			{
				return false;
			}
			texPool.Add(texName,newTex);
		}
		tex.shader = Shader.Find("Unlit/Transparent Colored");
		tex.mainTexture = texPool[texName];
		tex.MakePixelPerfect();
		return true;
	}
	

	private bool SetSprite(UISprite sprite,string spriteName/*,SPRITE_TYPE type*/)
	{
		if(sprite==null || spriteName == null || spriteName == "")
		{
			return false;
		}
		if(atlasPool==null)
		{
			atlasPool = new Dictionary<string, UIAtlas>();
		}
		
//		if(type == SPRITE_TYPE.HEAD)
//		{
			Hashtable cardList = TableManager.GetAppearance();
			foreach(DictionaryEntry dic in cardList)
			{
				Tab_Appearance appearance = (Tab_Appearance)dic.Value;
				if(appearance.HeadIcon == spriteName)
				{
					//已找到对应名字，查询是否atlas已经被缓存过
					if(!atlasPool.ContainsKey(appearance.HeadAtlas))
					{
						UIAtlas newAtlas = ResourceManager.Instance.LoadCardAtlas(appearance.HeadAtlas);
						atlasPool.Add(appearance.HeadAtlas,newAtlas);
					}
					sprite.atlas = atlasPool[appearance.HeadAtlas];
					sprite.spriteName = spriteName;
					sprite.MakePixelPerfect();
					return true;
				}
			}			
//		}
//		else if(type == SPRITE_TYPE.BODY)
//		{
//			if(!atlasPool.ContainsKey(spriteName))
//			{
//				Texture newTex = ResourceManager.Instance.LoadCardTexture(spriteName);
//				atlasPool.Add(spriteName,newTex);
//			}	
//			sprite.atlas = atlasPool[spriteName];
//			sprite.spriteName = spriteName;
//			sprite.MakePixelPerfect();
//			return true;
//			foreach(DictionaryEntry dic in cardList)
//			{
//				Tab_Appearance appearance = (Tab_Appearance)dic.Value;
//				if(appearance.BodyIcon == spriteName)
//				{
//					//已找到对应名字，查询是否atlas已经被缓存过
//					if(!atlasPool.ContainsKey(appearance.BodyAtlas))
//					{
//						//未缓存
//						UIAtlas newAtlas = ResourceManager.Instance.LoadCardAtlas(appearance.BodyAtlas);
//						atlasPool.Add(appearance.BodyAtlas,newAtlas);
//					}
//					sprite.atlas = atlasPool[appearance.BodyAtlas];
//					sprite.spriteName = spriteName;
//					sprite.MakePixelPerfect();
//					return true;
//				}
//			}			
//		}


		return false;
	}
	public void Clean()
	{
		if(_instance==null)
			return;
		if(atlasPool!=null)
		{
			foreach(UIAtlas atlas in atlasPool.Values)
			{
				Resources.UnloadAsset(atlas.texture);
			}
			atlasPool.Clear();
		}
		if(texPool!=null)
		{
			foreach(Texture tex in texPool.Values)
			{
				Resources.UnloadAsset(tex);
			}
			texPool.Clear();
		}
		Debug.Log("clear atlaspool and texpool");
	}
	//TODO:池的大小有控制，每次设置都把使用的排在最前面，如果池达到上限，就替换，旧的删除，新的加上

}
