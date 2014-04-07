using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

static public class AssetBundleManager {
   // A dictionary to hold the AssetBundle references
   static private Dictionary<string, AssetBundleRef> dictAssetBundleRefs;
   static AssetBundleManager (){
       dictAssetBundleRefs = new Dictionary<string, AssetBundleRef>();
   }
   // Class with the AssetBundle reference, url and version
   private class AssetBundleRef {
       public AssetBundle assetBundle = null;
       public int version;
       public string url;
       public AssetBundleRef(string strUrlIn, int intVersionIn) {
           url = strUrlIn;
           version = intVersionIn;
       }
   };
   // Get an AssetBundle
   public static AssetBundle getAssetBundle (string url, int version){
       string keyName = url + version.ToString();
       AssetBundleRef abRef;
       if (dictAssetBundleRefs.TryGetValue(keyName, out abRef))
           return abRef.assetBundle;
       else
           return null;
   }
   // Download an AssetBundle
   public static IEnumerator downloadAssetBundle (string url, int version){
       string keyName = url + version.ToString();
       if (dictAssetBundleRefs.ContainsKey(keyName))
		{
			AssetBundleRef abRef = null;
			dictAssetBundleRefs.TryGetValue(keyName,out abRef);
			while(abRef.assetBundle == null)
			{
           		yield return null;
			}
		}
       else {
			AssetBundleRef abRef = new AssetBundleRef (url, version);
			dictAssetBundleRefs.Add (keyName, abRef);
			//fix old version can not refresh new bundles
           using(WWW www = new WWW(url)){//;//WWW.LoadFromCacheOrDownload (url, version)){
               yield return www;
               if (www.error != null)
				{
                   throw new Exception("WWW download ERROR:" + www.error);
				}
				Debug.Log("Download Complete:"+ keyName);
               abRef.assetBundle = www.assetBundle;
           }
       }
   }

   //download AssetBundle synchronously
   public static AssetBundle syncDownloadAssetBundle(string url, int version)
   {
       string key_name = url + version.ToString();
       AssetBundleRef ab_ref = null;
       if (dictAssetBundleRefs.ContainsKey(key_name))
       {
           dictAssetBundleRefs.TryGetValue(key_name, out ab_ref);
       }
       else
       {
           ab_ref = new AssetBundleRef(url, version);
           dictAssetBundleRefs.Add(key_name, ab_ref);
           using (WWW www = new WWW(url))
           {
               if (www.error != null)
               {
                   Debug.LogError("WWW download ERROR:" + www.error);
               }
               Debug.Log("Download Complete:" + key_name);
               ab_ref.assetBundle = www.assetBundle;
           }
       }

       if (ab_ref != null && ab_ref.assetBundle != null)
       {
           return ab_ref.assetBundle;
       }

       return null;
   }

   // Unload an AssetBundle
   public static void Unload (string url, int version, bool allObjects){
       string keyName = url + version.ToString();
       AssetBundleRef abRef;
       if (dictAssetBundleRefs.TryGetValue(keyName, out abRef)){
           abRef.assetBundle.Unload (allObjects);
           abRef.assetBundle = null;
           dictAssetBundleRefs.Remove(keyName);
       }
   }
}