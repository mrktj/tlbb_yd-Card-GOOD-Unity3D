//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using System.Collections.Generic;
#if UNITY_ANDROID
using card;
#endif
/// <summary>
/// This is a script used to keep the game object scaled to 2/(Screen.height).
/// If you use it, be sure to NOT use UIOrthoCamera at the same time.
/// </summary>

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Root")]
public class UIRoot : MonoBehaviour
{
	static List<UIRoot> mRoots = new List<UIRoot>();

	/// <summary>
	/// List of all UIRoots present in the scene.
	/// </summary>

	static public List<UIRoot> list { get { return mRoots; } }

	public enum Scaling
	{
		PixelPerfect,
		FixedSize,
		FixedSizeOnMobiles,
	}

	/// <summary>
	/// Type of scaling used by the UIRoot.
	/// </summary>

	public Scaling scalingStyle = Scaling.FixedSize;

	/// <summary>
	/// Obsolete. Do not use.
	/// </summary>

	//[System.Obsolete("Use UIRoot's scalingStyle property instead")]
	[HideInInspector] public bool automatic = false;

	/// <summary>
	/// Height of the screen when 'automatic' is turned off.
	/// </summary>

	public int manualHeight = 720;

	/// <summary>
	/// If the screen height goes below this value, it will be as if the scaling style
	/// is set to FixedSize with manualHeight of this value.
	/// </summary>

	public int minimumHeight = 320;

	/// <summary>
	/// If the screen height goes above this value, it will be as if the scaling style
	/// is set to FixedSize with manualHeight of this value.
	/// </summary>

	public int maximumHeight = 1536;

	/// <summary>
	/// UI Root's active height, based on the size of the screen.
	/// </summary>

	public int activeHeight
	{
		get
		{
			int height = Mathf.Max(2, Screen.height);
			if (scalingStyle == Scaling.FixedSize) return manualHeight;

#if UNITY_IPHONE
		//ipad 4
		if(Screen.width > 768)
		{
			return manualHeight;
		}
#endif

			if (height < minimumHeight) return minimumHeight;
			if (height > maximumHeight) return maximumHeight;
			return height;
		}
	}

	/// <summary>
	/// Pixel size adjustment. Most of the time it's at 1, unless 'automatic' is turned off.
	/// </summary>

	public float pixelSizeAdjustment { get { return GetPixelSizeAdjustment(Screen.height); } }

	/// <summary>
	/// Helper function that figures out the pixel size adjustment for the specified game object.
	/// </summary>

	static public float GetPixelSizeAdjustment (GameObject go)
	{
		UIRoot root = NGUITools.FindInParents<UIRoot>(go);
		return (root != null) ? root.pixelSizeAdjustment : 1f;
	}

	/// <summary>
	/// Calculate the pixel size adjustment at the specified screen height value.
	/// </summary>

	public float GetPixelSizeAdjustment (int height)
	{
	//注释掉为了测试ip5的缩放，目前的情况应该是完全不缩放才对	
#if UNITY_EDITOR
		height = Mathf.Max(2, height);

		if (scalingStyle == Scaling.FixedSize)
			return (float)manualHeight / height;
		if (height < minimumHeight) return (float)minimumHeight / height;
		if (height > maximumHeight) return (float)maximumHeight / height;
		return 1f;
#elif UNITY_IPHONE
		//ipad 4
		if(Screen.width > 768)
		{
			height = Mathf.Max(2, height);
			return (float)manualHeight / height;
		}
		if(Application.loadedLevelName == Utils.UI_NAME_Battle || Application.loadedLevelName == Utils.UI_NAME_LOADING)//battleui
		{
			height = Mathf.Max(2, height);
	
			if (scalingStyle == Scaling.FixedSize)
				return (float)manualHeight / height;
			if (height < minimumHeight) return (float)minimumHeight / height;
			if (height > maximumHeight) return (float)maximumHeight / height;
			return 1f;			
		}
		return 1f;
#else
		return 1f;
#endif
	}

	Transform mTrans;

	void Awake ()
	{
		mTrans = transform;
		mRoots.Add(this);

		// Backwards compatibility
		if (automatic)
		{
			scalingStyle = Scaling.PixelPerfect;
			automatic = false;
		}
//为了方便调整，在编辑器中均用缩放

		if(Application.loadedLevelName == Utils.UI_NAME_Battle || Application.loadedLevelName == Utils.UI_NAME_LOADING)
		{
			scalingStyle = Scaling.FixedSize;
		}else
		{
			scalingStyle = Scaling.PixelPerfect;
		}
	}

	void OnDestroy () { mRoots.Remove(this); }

	void Start ()
	{
		UIOrthoCamera oc = GetComponentInChildren<UIOrthoCamera>();
		
		if (oc != null)
		{
			Debug.LogWarning("UIRoot should not be active at the same time as UIOrthoCamera. Disabling UIOrthoCamera.", oc);
			Camera cam = oc.gameObject.GetComponent<Camera>();
			oc.enabled = false;
			if (cam != null) cam.orthographicSize = 1f;
		}
	}

	void Update ()
	{
		if (mTrans != null)
		{
			float calcActiveHeight = activeHeight;

			if (calcActiveHeight > 0f )
			{
				float size = 2f / calcActiveHeight;
				
				Vector3 ls = mTrans.localScale;
	
				if (!(Mathf.Abs(ls.x - size) <= float.Epsilon) ||
					!(Mathf.Abs(ls.y - size) <= float.Epsilon) ||
					!(Mathf.Abs(ls.z - size) <= float.Epsilon))
				{
					mTrans.localScale = new Vector3(size, size, size);
				}
			}
		}
	}

	/// <summary>
	/// Broadcast the specified message to the entire UI.
	/// </summary>

	static public void Broadcast (string funcName)
	{
		for (int i = 0, imax = mRoots.Count; i < imax; ++i)
		{
			UIRoot root = mRoots[i];
			if (root != null) root.BroadcastMessage(funcName, SendMessageOptions.DontRequireReceiver);
		}
	}

	/// <summary>
	/// Broadcast the specified message to the entire UI.
	/// </summary>

	static public void Broadcast (string funcName, object param)
	{
		if (param == null)
		{
			// More on this: http://answers.unity3d.com/questions/55194/suggested-workaround-for-sendmessage-bug.html
			Debug.LogError("SendMessage is bugged when you try to pass 'null' in the parameter field. It behaves as if no parameter was specified.");
		}
		else
		{
			for (int i = 0, imax = mRoots.Count; i < imax; ++i)
			{
				UIRoot root = mRoots[i];
				if (root != null) root.BroadcastMessage(funcName, param, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
