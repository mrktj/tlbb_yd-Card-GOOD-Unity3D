using UnityEngine;
using System.Collections;
using System;
using Chartboost;


public class CBUIManager : MonoBehaviour
{
#if UNITY_ANDROID || UNITY_IPHONE

	public void Update()
	{
#if UNITY_ANDROID
		// Handle the Android back button
		if (Input.GetKeyUp(KeyCode.Escape)) {
			// Check if Chartboost wants to respond to it
			if (CBBinding.onBackPressed()) {
				// If so, return and ignore it
				return;
			} else {
				// Otherwise, handle it ourselves -- let's close the app
				Application.Quit();
			}
		}
#endif
	}

	void OnEnable()
	{
		// Initialize the Chartboost plugin
#if UNITY_ANDROID
		// Remember to set the Android app ID and signature in the file `/Plugins/Android/res/values/strings.xml`
		CBBinding.init();
#elif UNITY_IPHONE
		// Replace these with your own app ID and signature from the Chartboost web portal
		CBBinding.init( "4f21c409cd1cb2fb7000001b", "92e2de2fd7070327bdeb54c15a5295309c6fcd2d" );
#endif
	}


	void OnDisable()
	{
		//
	}
	
	void OnGUI()
	{
#if UNITY_ANDROID
		// Disable user input for GUI when impressions are visible
		// This is only necessary on Android if we have called CBBinding.setImpressionsUseActivities(false),
		//   as that allows touch events to leak through Chartboost impressions
		GUI.enabled = !CBBinding.isImpressionVisible();
#endif
		
		GUI.matrix = Matrix4x4.Scale( new Vector3( 2, 2, 2 ) );
		
		if( GUILayout.Button( "Cache Interstitial" ) ) {
			CBBinding.cacheInterstitial( "default" );
		}
		
		if( GUILayout.Button( "Show Interstitial" ) ) {
			CBBinding.showInterstitial( "default" );
		}
		
		if( GUILayout.Button( "Cache More Apps" ) ) {
			CBBinding.cacheMoreApps();
		}
		
		if( GUILayout.Button( "Show More Apps" ) ) {
			CBBinding.showMoreApps();
		}
	}
	
#endif
}
