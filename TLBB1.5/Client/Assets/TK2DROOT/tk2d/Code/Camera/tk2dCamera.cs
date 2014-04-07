using UnityEngine;
using System.Collections;

[System.Serializable]
/// <summary>
/// Controls camera scale for different resolutions.
/// Use this to display at 0.5x scale on iPhone3G or 2x scale on iPhone4
/// </summary>
public class tk2dCameraResolutionOverride
{
	/// <summary>
	/// Name of the override
	/// </summary>
	public string name;
	
	/// <summary>
	/// Screen width to match. Set to -1 to match any width.
	/// </summary>
	public int width;
	/// <summary>
	/// Screen height to match. Set to -1 to match any height.
	/// </summary>
	public int height;
	
	/// <summary>
	/// Amount to scale the matched resolution by
	/// 1.0 = pixel perfect, 0.5 = 50% of pixel perfect size
	/// </summary>
	public float scale = 1.0f;
	
	/// <summary>
	/// Amount to offset from the bottom left, in number of pixels in target resolution. Example, if override resolution is
	/// 1024x768, an offset of 20 will offset in by 20 pixels
	/// </summary>
	public Vector2 offsetPixels = new Vector2(0, 0);
	
	public enum AutoScaleMode
	{
		None, // explicitly use the scale parameter
		FitWidth, // fits the width to the current resolution
		FitHeight, // fits the height to the current resolution
		FitVisible, // best fit (either width or height)
		StretchToFit, // stretch to fit, could be non-uniform and/or very ugly
		PixelPerfectFit, // fits to the closest power of two
	};
	public AutoScaleMode autoScaleMode = AutoScaleMode.None;
	
	public enum FitMode
	{
		Constant,	// Use the screenOffset
		Center, 	// Align to center of screen
	};
	public FitMode fitMode = FitMode.Constant;
	
	
	/// <summary>
	/// Returns true if this instance of tk2dCameraResolutionOverride matches the curent resolution.
	/// In future versions this may  change to support ranges of resolutions in addition to explict ones.
	/// </summary>
	public bool Match(int pixelWidth, int pixelHeight)
	{
		return ((width == -1 || pixelWidth == width) && (height == -1 || pixelHeight == height));
	}
}

[AddComponentMenu("2D Toolkit/Camera/tk2dCamera")]
[ExecuteInEditMode]
/// <summary>
/// Maintains a screen resolution camera. 
/// Whole number increments seen through this camera represent one pixel.
/// For example, setting an object to 300, 300 will position it at exactly that pixel position.
/// </summary>
public class tk2dCamera : MonoBehaviour 
{
	/// <summary>
	/// Resolution overrides, if necessary. See <see cref="tk2dCameraResolutionOverride"/>
	/// </summary>
	public tk2dCameraResolutionOverride[] resolutionOverride = null;
	tk2dCameraResolutionOverride currentResolutionOverride = null;

	public tk2dCamera inheritSettings = null;
	
	/// <summary>
	/// Native resolution width of the camera. Override this in the inspector.
	/// </summary>
	public int nativeResolutionWidth = 960;
	/// <summary>
	/// Native resolution height of the camera. Override this in the inspector.
	/// </summary>
	public int nativeResolutionHeight = 640;
	public bool enableResolutionOverrides = true;
	
	/// <summary>
	/// The camera this script is attached to is treated as the main camera in the scene.
	/// </summary>
	[HideInInspector]
	public Camera mainCamera;
	
	/// <summary>
	/// Global instance, used by sprite and textmesh class to quickly find the tk2dCamera instance.
	/// </summary>
	public static tk2dCamera inst;
	
	/// <summary>
	/// Non centered ortho size of this camera
	/// </summary>
	[System.NonSerialized]
	public float orthoSize = 1.0f;
	
	/// <summary>
	/// Scaled resolution of screen.
	/// The top right point in screen space.
	/// </summary>
	public Vector2 ScaledResolution { get { return _scaledResolution; } }

	/// <summary>
	/// Returns screen extents - top, bottom, left and right will be the extent of the screen
	/// Regardless of resolution or override
	/// </summary>
	public Rect ScreenExtents { get { return _screenExtents; } }

	/// <summary>
	/// Offset in pixels used to center content
	/// </summary>
	public Vector2 ScreenOffset { get { return _screenOffset; } }

	[System.Obsolete]
	public Vector2 resolution { get { return ScaledResolution; } }


	/// <summary>
	/// Enable/disable viewport clipping.
	/// ScreenCamera must be valid for it to be actually enabled when rendering.
	/// </summary>
	public bool viewportClippingEnabled = false;

	/// <summary>
	/// Viewport clipping region.
	/// </summary>
	public Vector4 viewportRegion = new Vector4(0, 0, 100, 100);

	/// <summary>
	/// Camera for viewportClipping
	/// </summary>
	public Camera screenCamera = null;


	/// <summary>
	/// Target resolution
	/// The target resolution currently being used.
	/// If displaying on a 960x640 display, this will be the number returned here, regardless of scale, etc.
	/// If the editor resolution is forced, the returned value will be the forced resolution.
	/// </summary>
	public Vector2 TargetResolution { get { return _targetResolution; } }

	Vector2 _targetResolution = Vector2.zero;
	Vector2 _scaledResolution = Vector2.zero;
	Vector2 _screenOffset = Vector2.zero;
	
	/// <summary>
	/// Zooms the current display
	/// Anchors will still be anchored, but will be scaled with the zoomScale.
	/// It is recommended to use a second camera for HUDs if necessary to avoid this behaviour.
	/// </summary>
	[System.NonSerialized]
	public float zoomScale = 1.0f;


	[HideInInspector]
	/// <summary>
	/// Forces the resolution in the editor - The game window in the Unity editor returns the actual resolution of the window
	/// regardless of what is set in "Build Settigs". So if the game window size is set to 1024x768 in "Build Settings", and
	/// you scale down the physical window in the editor, the camera.pixelWidth / height functions the actual pixel count.
	/// </summary>
	public bool forceResolutionInEditor = false;
	
	[HideInInspector]
	/// <summary>
	/// The resolution to force the game window to when <see cref="forceResolutionInEditor"/> is enabled.
	/// </summary>
	public Vector2 forceResolution = new Vector2(960, 640);
	
	// Use this for initialization
	void Awake () 
	{
		mainCamera = GetComponent<Camera>();
		if (mainCamera != null) {
			UpdateCameraMatrix();
		}
		
		if (!viewportClippingEnabled) // the main camera can't display rect
			inst = this;
	}
	
	void LateUpdate() 
	{
		UpdateCameraMatrix();
	}

	Rect _screenExtents;

	Rect unitRect = new Rect(0, 0, 1, 1);
	
	public Vector2 dumbOffset = Vector2.zero;

	// Trace back to the source, however far up the hierarchy that may be
	tk2dCamera Settings {
		get { 
			return (inheritSettings == null || inheritSettings == this) ? this : inheritSettings.Settings;
		}
	}
	
	/// <summary>
	/// Updates the camera matrix to ensure 1:1 pixel mapping
	/// </summary>
	public void UpdateCameraMatrix()
	{
		if (!this.viewportClippingEnabled)
			inst = this;

		if (!mainCamera.orthographic)
		{
			// Must be orthographic
			Debug.LogError("tk2dCamera must be orthographic");
			mainCamera.orthographic = true;
		}

		tk2dCamera settings = Settings;

		bool viewportClippingEnabled = this.viewportClippingEnabled && this.screenCamera != null && this.screenCamera.rect == unitRect;
		Camera screenCamera = viewportClippingEnabled ? this.screenCamera : mainCamera;

		float pixelWidth = screenCamera.pixelWidth;
		float pixelHeight = screenCamera.pixelHeight;

#if UNITY_EDITOR
		if (settings.forceResolutionInEditor)
		{
			pixelWidth = settings.forceResolution.x;
			pixelHeight = settings.forceResolution.y;
		}
#endif

		_targetResolution = new Vector2(pixelWidth, pixelHeight);
		
		// Find an override if necessary
		if (!settings.enableResolutionOverrides)
			currentResolutionOverride = null;
		
		if (settings.enableResolutionOverrides && 
			(currentResolutionOverride == null ||
			(currentResolutionOverride != null && (currentResolutionOverride.width != pixelWidth || currentResolutionOverride.height != pixelHeight))
			))
		{
			currentResolutionOverride = null;
			// find one if it matches the current resolution
			if (settings.resolutionOverride != null)
			{
				foreach (var ovr in settings.resolutionOverride)
				{
					if (ovr.Match((int)pixelWidth, (int)pixelHeight))
					{
						currentResolutionOverride = ovr;
						break;
					}
				}
			}
		}
		
		Vector2 scale = new Vector2(1, 1);
		Vector2 offset = new Vector2(0, 0);
		float s = 0.0f;
		if (currentResolutionOverride != null)
		{
			switch (currentResolutionOverride.autoScaleMode)
			{
			case tk2dCameraResolutionOverride.AutoScaleMode.FitHeight: 
				s = pixelHeight / settings.nativeResolutionHeight; 
				scale.Set(s, s);
				break;

			case tk2dCameraResolutionOverride.AutoScaleMode.FitWidth: 
				s = pixelWidth / settings.nativeResolutionWidth; 
				scale.Set(s, s);
				break;

			case tk2dCameraResolutionOverride.AutoScaleMode.FitVisible:
			case tk2dCameraResolutionOverride.AutoScaleMode.PixelPerfectFit:
				float nativeAspect = (float)settings.nativeResolutionWidth / settings.nativeResolutionHeight;
				float currentAspect = pixelWidth / pixelHeight;
				if (currentAspect < nativeAspect)
					s = pixelWidth / settings.nativeResolutionWidth;
				else
					s = pixelHeight / settings.nativeResolutionHeight;
				
				if (currentResolutionOverride.autoScaleMode == tk2dCameraResolutionOverride.AutoScaleMode.PixelPerfectFit)
				{
					if (s > 1.0f)
						s = Mathf.Floor(s); // round number
					else
						s = Mathf.Pow(2, Mathf.Floor(Mathf.Log(s, 2))); // minimise only as power of two
				}
				
				scale.Set(s, s);
				break;

			case tk2dCameraResolutionOverride.AutoScaleMode.StretchToFit:
				scale.Set(pixelWidth / settings.nativeResolutionWidth, pixelHeight / settings.nativeResolutionHeight);
				break;

			default:
			case tk2dCameraResolutionOverride.AutoScaleMode.None: 
				s = currentResolutionOverride.scale;
				scale.Set(s, s);
				break;
			}
			
			scale *= zoomScale;
			
			// no offset when ScaleToFit
			if (currentResolutionOverride.autoScaleMode != tk2dCameraResolutionOverride.AutoScaleMode.StretchToFit)
			{
				switch (currentResolutionOverride.fitMode)
				{
				case tk2dCameraResolutionOverride.FitMode.Center:
					offset = new Vector2(Mathf.Round((settings.nativeResolutionWidth  * scale.x - pixelWidth ) / 2.0f), 
										 Mathf.Round((settings.nativeResolutionHeight * scale.y - pixelHeight) / 2.0f));
					break;
					
				default:
				case tk2dCameraResolutionOverride.FitMode.Constant: 
					offset = -currentResolutionOverride.offsetPixels; break;
				}
			}
		}
		
		float left = offset.x, bottom = offset.y;
		float right = pixelWidth + offset.x, top = pixelHeight + offset.y;

		// Correct for viewport clipping rendering
		// Coordinates in subrect are "native" pixels, but origin is from the extrema of screen
		if (viewportClippingEnabled) {
			float vw = (right - left) / scale.x;
			float vh = (top - bottom) / scale.y;

			Vector4 sr = new Vector4((int)viewportRegion.x, (int)viewportRegion.y,
									 (int)viewportRegion.z, (int)viewportRegion.w);

			float viewportLeft = -offset.x / pixelWidth + sr.x / vw;
			float viewportBottom = -offset.y / pixelHeight + sr.y / vh;
			float viewportWidth = sr.z / vw;
			float viewportHeight = sr.w / vh;

			Rect r = new Rect( viewportLeft, viewportBottom, viewportWidth, viewportHeight );
			if (mainCamera.rect.x != viewportLeft ||
				mainCamera.rect.y != viewportBottom ||
				mainCamera.rect.width != viewportWidth ||
				mainCamera.rect.height != viewportHeight) {
				mainCamera.rect = r;
			}

			float maxWidth = Mathf.Min( 1.0f - r.x, r.width );
			float maxHeight = Mathf.Min( 1.0f - r.y, r.height );

			float rectOffsetX = sr.x * scale.x - offset.x;
			float rectOffsetY = sr.y * scale.y - offset.y;

			if (r.x < 0.0f) {
				rectOffsetX += -r.x * pixelWidth;
				maxWidth = (r.x + r.width);
			}
			if (r.y < 0.0f) {
				rectOffsetY += -r.y * pixelHeight;
				maxHeight = (r.y + r.height);
			}

			left += rectOffsetX;
			bottom += rectOffsetY;
			right = pixelWidth * maxWidth + offset.x + rectOffsetX;
			top = pixelHeight * maxHeight + offset.y +  rectOffsetY;
		}
		else {
			Rect targetRect = new Rect(0, 0, 1, 1);
			if (mainCamera.rect != targetRect) {
				mainCamera.rect = targetRect;
			}
		}

		_screenExtents.Set(left / scale.x, top / scale.y, (right - left) / scale.x, (bottom - top) / scale.y);
		float far = mainCamera.farClipPlane;
		float near = mainCamera.near;
		
		// set up externally used variables
		orthoSize = (top - bottom) / 2.0f;
		_scaledResolution = new Vector2((right - left) / scale.x, (top - bottom) / scale.y);
		_screenOffset = offset;
		
		// Additional half texel offset
		// Takes care of texture unit offset, if necessary.
		
		// should be off on all opengl platforms
		// and on on PC/D3D
		bool halfTexelOffset = false;
		halfTexelOffset = (Application.platform == RuntimePlatform.WindowsPlayer ||
						   Application.platform == RuntimePlatform.WindowsWebPlayer ||
						   Application.platform == RuntimePlatform.WindowsEditor);
		
		float halfTexelOffsetAmount = (halfTexelOffset)?1.0f:0.0f;

		float x =  (2.0f) / (right - left) * scale.x;
		float y = (2.0f) / (top - bottom) * scale.y;
		float z = -2.0f / (far - near);

		float a = -(right + left + halfTexelOffsetAmount) / (right - left);
		float b = -(bottom + top - halfTexelOffsetAmount) / (top - bottom);
		float c = -(far + near) / (far - near);
		
		Matrix4x4 m = new Matrix4x4();
		m[0,0] = x;  m[0,1] = 0;  m[0,2] = 0;  m[0,3] = a;
		m[1,0] = 0;  m[1,1] = y;  m[1,2] = 0;  m[1,3] = b;
		m[2,0] = 0;  m[2,1] = 0;  m[2,2] = z;  m[2,3] = c;
		m[3,0] = 0;  m[3,1] = 0;  m[3,2] = 0;  m[3,3] = 1;

		if (mainCamera.projectionMatrix != m) {
			mainCamera.projectionMatrix = m;
		}
	}
}
