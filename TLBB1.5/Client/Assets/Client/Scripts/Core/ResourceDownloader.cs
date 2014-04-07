using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;

public class ResourceDownloader : MonoBehaviour {
	
	
	void ReconnectResources(GameObject obj)
	{
		StartCoroutine("LoadAndSaveResources");
	}	
	
	public void SetLabelText(string msg){
		
		
	}
	
	
	private  float downLoadPercent = 0;
	public float DownLoadPercent{
		get{return downLoadPercent;}
	}
	private int index = 0;
	private int resourcesCount;
	private bool downLoadOver = false;
	public bool DownLoadOver{
		get{return downLoadOver;}
	}
	private readonly string resourcesUrl = "http://114.255.34.1:280/Release/";
	
#if UNITY_EDITOR	
	private string destDir = Application.dataPath;
#else
	private string destDir = ResourceManager.PathURL.Replace("file://","");
#endif	
	
	
	public IEnumerator LoadAndSaveResources(){
		downLoadPercent = 0;
		index = 0 ;
		string resourcesFileUrl = resourcesUrl+ClientConfigure.resourceTxtName;
		string resourceFilePath = Path.Combine(destDir,ClientConfigure.resourceTxtName);
		WWW www = null;

		
		//下载资源文件,如果资源文件下载失败,则重复下载资源文件;
		SetLabelText("正在获取资源列表");
		www = new WWW (resourcesFileUrl);
		yield return www;
		if (!string.IsNullOrEmpty (www.error)) {
			Debug.LogError (www.error);
			BoxManager.showMessage("获取资源列表失败,请重试",ClientConfigure.title);
			UIEventListener.Get(BoxManager.buttonYes).onClick += ReconnectResources;			
			yield break;
		}
		
		//分离资源,判断资源文件的有效性;
		string[] resourcesRow = www.text.Split(new string[]{"\r\n"},StringSplitOptions.RemoveEmptyEntries);
		yield return null;
		
		if(resourcesRow.Length == 0){
			Debug.Log("Doesn't have resources");
			yield break;	
		}
		
		List<string> filePathLists = new List<string>();
		
		for(int i = 0;i<resourcesRow.Length;i++){
			string[] strs = resourcesRow[i].Split('\t');
			filePathLists.Add(strs[0]);
		}
		
		string[] filePaths = filePathLists.ToArray();
		filePathLists.Clear();
		/*
		 * 资源存在版本校验的一些处理;目前不做了;
		//资源文件列表字典化,方便与本地资源列表对比;
		Dictionary<string,int> resourcesDic = new Dictionary<string, int>();
		foreach(string resourceUrl in resourcesRow){
			DownloaderItem resourceItem = new DownloaderItem(resourceUrl);
			resourcesDic.Add(resourceItem.name,resourceItem.version);
		}
		yield return null;
		
		//如果存在本地资源文件列表,则提取本地资源文件列表并字典化;
		//否则直接存储至目标位置;
		string[] localResourcesRow = null;
		Dictionary<string,int> localResourcesDic = new Dictionary<string, int>();
		bool useLocalResourcesTxt = false;
		string resourceFilePath = destDir+"/resources.txt";
		if(!File.Exists(resourceFilePath)){
			Debug.Log("write resource txt file");
			File.WriteAllText(resourceFilePath,www.text);
		}else{
			MemoryStream wwwTxtMemory = new MemoryStream(www.bytes);
			MemoryStream localTxtMemory = new MemoryStream(File.ReadAllBytes(resourceFilePath));
			
			if(wwwTxtMemory.GetHashCode() == localTxtMemory.GetHashCode()){
				useLocalResourcesTxt = false;
			}else{
				useLocalResourcesTxt = true;
				localResourcesRow = File.ReadAllLines(resourceFilePath);
				foreach(string resourceUrl in localResourcesRow){
					DownloaderItem resourceItem = new DownloaderItem(resourceUrl);
					localResourcesDic.Add(resourceItem.name,resourceItem.version);
				}
			}
		}
		yield return null;
		*/
		
		SetLabelText("正在下载资源文件");
		resourcesCount = resourcesRow.Length;
		List<string> downloadFailedResources = new List<string>();
		foreach(string resourcesName in filePaths){
			string filePath = Path.Combine(destDir,resourcesName);
			if(!File.Exists(filePath)){
				www = new WWW(resourcesUrl + resourcesName);
				yield return www;
				if (!string.IsNullOrEmpty (www.error)) {
					Debug.LogError (www.error);
					downloadFailedResources.Add(resourcesName);
				}else{
					string dirPath = Path.GetDirectoryName(filePath);
					if(!Directory.Exists(dirPath)){
						Debug.Log("create dir:"+dirPath);
						Directory.CreateDirectory(dirPath);
					}
					
					Debug.Log("asyn write start:"+filePath);
					if(www.bytes.Length < 1024*256){
						File.WriteAllBytes(filePath,www.bytes);
					}else{
						FileStream fileStream = File.OpenWrite(filePath);
						fileStream.BeginWrite(www.bytes,0,www.bytes.Length,new AsyncCallback(WriteDone),
							fileStream);
					}
					//File.WriteAllBytes(filePath,www.bytes);
				}
			}
			index ++;
			downLoadPercent = ((float)index)/resourcesCount;
			yield return null;
		}
		
		//继续下载下载过程中下载失败的资源文件;
		while (downloadFailedResources.Count != 0){
			string resourcesName = downloadFailedResources[0];
			string filePath = Path.Combine(destDir,resourcesName);
			if(!File.Exists(filePath)){
				www = new WWW(resourcesUrl + resourcesName);
				yield return www;
				if (!string.IsNullOrEmpty (www.error)) {
					Debug.LogError ("reDownload:"+www.error);
				}else{
					string dirPath = Path.GetDirectoryName(filePath);
					if(!Directory.Exists(dirPath)){
						Debug.LogWarning("reDownload create dir:"+dirPath);
						Directory.CreateDirectory(dirPath);
					}
					
					Debug.Log("asyn write start:"+filePath);
					if(www.bytes.Length < 1024*256){
						File.WriteAllBytes(filePath,www.bytes);
					}else{
						FileStream fileStream = File.OpenWrite(filePath);
						fileStream.BeginWrite(www.bytes,0,www.bytes.Length,new AsyncCallback(WriteDone),
							fileStream);
					}
					
					downloadFailedResources.RemoveAt(0);
  				}
			}
			yield return null;
		}
		
		if(!File.Exists(resourceFilePath)){
			Debug.Log("write resource txt file");
			File.WriteAllText(resourceFilePath,www.text);
		}
		downLoadOver = true;
		yield return null;
		
	}	

	private void WriteDone(IAsyncResult asr){
		using (Stream str = (Stream) asr.AsyncState){
			str.EndWrite(asr);
			Debug.Log("asyn write end");
		}
		
	}
}
