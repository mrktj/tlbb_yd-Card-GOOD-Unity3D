using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class ProcessAtlas : EditorWindow {
	
	
	[MenuItem("Assets/搜索对应原始图片所在文件夹",true)]
	public static bool IsSearchTextures(){
		if(Selection.gameObjects.Length == 1){
			if(Selection.gameObjects[0].GetComponent<UIAtlas>() != null){
				return true;
			}else{
				return true;
			}
		}else{
			return false;
		}


	}
	[MenuItem("Assets/搜索对应原始图片所在文件夹")]
	public static void SearchTextures(){
		UIAtlas atlas = Selection.gameObjects[0].GetComponent<UIAtlas>();
		List<string> files =  GetAllFiles("Assets/Client/Textures/","*.png");
		if(atlas != null){
			UpdateSignalAtlas(atlas,files,true);
		}
	}
	
	
	[MenuItem("Assets/更新选择的图集",true)]
	public static bool IsUpdateSelectAtlas(){
		if(Selection.gameObjects.Length != 0){
			return true;
		}else{
			return false;
		}


		//Object obj = Selection.gameObjects[0];


	}
	
	[MenuItem("Assets/更新选择的图集")]
	public static void UpdateSelectAtlas(){
		GameObject[] gameObjs = Selection.gameObjects;
		List<string> files =  GetAllFiles("Assets/Client/Textures/","*.png");
		foreach(GameObject gameObj in gameObjs){
			UIAtlas atlas = gameObj.GetComponent<UIAtlas>();
			if(atlas != null){
				UpdateSignalAtlas(atlas,files,false);
			}
		}
	}	
	
	//
	private static void  UpdateSignalAtlas(UIAtlas atlas,List<string> files,bool search){
		if(atlas != null && files != null){
			/*
			if(atlas.texture.width >=2048 || atlas.texture.height >=2048){
				Debug.LogError("图集过大,请手动更新:"+atlas.name);
				return ;
			}
			*/
			List<UIAtlas.Sprite> sprites = atlas.spriteList;
			List<string> names = new List<string>();
			foreach(UIAtlas.Sprite sprite in sprites){
				//Debug.Log(sprite.name);
				names.Add(sprite.name);
			}

			List<Texture> textures = new List<Texture>();
			int i = 0;
			foreach(string filePath in files){
				if(names.Count == 0){
					Debug.Log("texture count:"+textures.Count+" find count:"+i+" search count:"+files.Count);
					Debug.Log("Process Atlas:"+atlas.name);
					break;
				}

				float process = (float)i/(float)files.Count;
				EditorUtility.DisplayProgressBar(
					"Progress Atlas:"+atlas.name,
					filePath,
					process);

				string name = Path.GetFileNameWithoutExtension(filePath);
				if(names.Contains(name)){
					Texture obj = AssetDatabase.LoadMainAssetAtPath(filePath) as Texture;
					if(isApprorateTexture(sprites,obj)){
						textures.Add(obj);
						names.Remove(name);
						//Debug.LogWarning(filePath);
						if(textures.Count >= 50){
							UIAtlasMaker.UpdateAtlasEX(atlas,textures);
							Resources.UnloadUnusedAssets();
							textures.Clear();
						}
					}
				}else{
					//Debug.Log(filePath);
				}
				i++;
			}
			
			EditorUtility.ClearProgressBar();
			foreach(string name in names){
				Debug.LogError("could not find:"+name + " of " + atlas.name+".please find the org texture");
			}
			
			if(textures.Count > 0){
				UIAtlasMaker.UpdateAtlasEX(atlas,textures);
				Resources.UnloadUnusedAssets();
				textures.Clear();	
			}
		 	/*
			if(!search){
				int lllcount = -1;
				if(textures.Count != 0 ){
					if(lllcount > 0){
						for(int m = 0;m<textures.Count;){
							UIAtlasMaker.UpdateAtlasEX(atlas,textures.GetRange(m,
								textures.Count - m >=lllcount?lllcount:textures.Count - m));
							m = m+lllcount;
							Debug.Log(m/lllcount);
							Resources.UnloadUnusedAssets();
						}
					}else{
						UIAtlasMaker.UpdateAtlasEX(atlas,textures);
						Resources.UnloadUnusedAssets();
					}
				}
			}
			*/	
		}
		
	}




	static List<string> GetAllFiles(string rootPath,string paternan){
		List<string> files = new List<string>();
		RecureGetAllFiles(rootPath,files,paternan);
		return files;
	}
	
	static void RecureGetAllFiles(string rootPath,List<string> files,string partenan){
		string[] subfiles;
		if(string.IsNullOrEmpty(partenan)){
			subfiles = Directory.GetFiles(rootPath);
		}else{
			subfiles = Directory.GetFiles(rootPath,partenan);
		}
		files.AddRange(subfiles);
		string[] subPaths = Directory.GetDirectories(rootPath);
		foreach(string sunPath in subPaths){
			RecureGetAllFiles(sunPath,files,partenan);
		}
	}

	[MenuItem("NGUI/更新所有UI图集(其他图集建议手动单独更新)")]
	static void UpdateAllAtlas(){

		List<string> atlasPrefabFiles = GetAllFiles("Assets/Client/Atlas/UI/","*.prefab");
		List<string> files =  GetAllFiles("Assets/Client/Textures/","*.png");

		foreach(string atlasPrefabFile in atlasPrefabFiles){
			GameObject gameObj = AssetDatabase.LoadMainAssetAtPath(atlasPrefabFile) as GameObject;
			UIAtlas atlas = gameObj.GetComponent<UIAtlas>();
			if(atlas != null){
				UpdateSignalAtlas(atlas,files,false);
			}
		}

	}
	
	[MenuItem("Assets/比较两个图集的不同点",true)]
	public static bool IsCompareTwoAtlas(){
		if(Selection.gameObjects.Length == 2){
			return true;
		}else{
			return false;
		}

	}	
	
	[MenuItem("Assets/比较两个图集的不同点")]
	static void CompareTwoAtlas(){
		GameObject[] gameObjs = Selection.gameObjects;
		UIAtlas oldUIAltas = gameObjs[0].GetComponent<UIAtlas>();
		UIAtlas newUIAltas = gameObjs[1].GetComponent<UIAtlas>();
		
		BetterList<string> oldNames = oldUIAltas.GetListOfSprites();
		BetterList<string> newNames = newUIAltas.GetListOfSprites();
		
		Debug.Log("Old Counts:"+oldNames.ToArray().Length);
		Debug.Log("New Counts:"+newNames.ToArray().Length);
		
		foreach(string name in oldNames){
			newNames.Remove(name);
			Debug.Log("delete:"+name);
		}
		
		foreach(string name in newNames){
			Debug.LogWarning("The New Texture:"+name);
		}
		
		
	}
	
	//
	static bool isApprorateTexture(List< UIAtlas.Sprite> sprites,Texture obj){
		List<Texture> tempTxts = new List<Texture>();
		tempTxts.Add(obj);
		List<UIAtlasMaker.SpriteEntry>  entrys = UIAtlasMaker.CreateSpritesEX(tempTxts);
		UIAtlasMaker.SpriteEntry tempEntry = entrys[0];
		
		UIAtlas.Sprite searched = null;
		foreach(UIAtlas.Sprite tempSprite in sprites){
			if(tempSprite.name == tempEntry.name){
				
				if(Mathf.Abs(tempSprite.inner.width - tempEntry.tex.width) <= (
					tempSprite.inner.width > 200 ? tempSprite.inner.width*0.1f:20)
				&& Mathf.Abs(tempSprite.inner.height - tempEntry.tex.height) <= (
					tempSprite.inner.height > 200 ? tempSprite.inner.height*0.1f:20))
				{
					searched = tempSprite;
					break;
				}else if(Mathf.Abs(tempSprite.outer.width - tempEntry.tex.width) <= (
					tempSprite.outer.width > 200 ? tempSprite.outer.width*0.1f:20)
				&& Mathf.Abs(tempSprite.outer.height - tempEntry.tex.height) <= (
					tempSprite.outer.height > 200 ? tempSprite.outer.height*0.1f:20)){
					searched = tempSprite;
					break;	
				}else{
					Debug.LogWarning("Name:"+tempSprite.name
									+" Sprite:"+tempSprite.inner
									+" Sprite:"+tempSprite.outer
									+" Entry:"+tempEntry.tex.width + " "+ tempEntry.tex.height);
				}
			}

		}
		
		return searched!= null;
	}
	
	[MenuItem("Assets/ReBuildFont",false)]
	static public bool isFont(){
		if(Selection.gameObjects != null){
			return true;
		}else{
			return false;
		
		}
		
	
	}
	
	[MenuItem("Assets/ReBuildFont",false)]
	static public void  RebuildFont(){
		foreach(GameObject obj in Selection.gameObjects){
			UIFont uifont = obj.GetComponent<UIFont>();
			if(uifont != null){
				baseFont = uifont.dynamicFont;
				if(baseFont != null){
					FixBrokenWord();
				}
				
				
			}
			
		}
	
	}
	
static string chineseTxt = null;  
static public UnityEngine.Font baseFont;  
    static public void FixBrokenWord()  
    {  
        if (chineseTxt == null) {  
            TextAsset txt = Resources.Load("font/chinese") as TextAsset;  
            chineseTxt = txt.ToString();  
        }  
   
        baseFont.RequestCharactersInTexture(chineseTxt);  
        Texture texture = baseFont.material.mainTexture;    // Font的内部纹理  
        Debug.Log(string.Format("texture:{0}   {1}", texture.width, texture.height);   // 纹理大小  
    }	
	
}
