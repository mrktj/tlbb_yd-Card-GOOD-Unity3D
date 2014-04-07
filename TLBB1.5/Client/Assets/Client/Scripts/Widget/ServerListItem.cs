using UnityEngine;
using System.Collections;

[AddComponentMenu("KDTL/UI/ServerListItem Button")]
public class ServerListItem : MonoBehaviour {
	
	public UISprite target;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public bool isEnabled
	{
		get
		{
			Collider col = collider;
			return col && col.enabled;
		}
		set
		{
			Collider col = collider;
			if (!col) return;

			if (col.enabled != value)
			{
				col.enabled = value;
				HideBackground();
			}
		}
	}
	
	void Awake () { if (target == null) target = GetComponentInChildren<UISprite>();}
	void OnEnable () { HideBackground(); }
	
	void HideBackground(){
		target.alpha = 0;
		target.MakePixelPerfect();
	}
	
	void OnHover (bool isOver)
	{
		if (isEnabled && target != null)
		{
			target.alpha = isOver ? 1 : 0;
			target.MakePixelPerfect();
		}
	}

	void OnPress (bool pressed)
	{
		if (pressed)
		{
			target.alpha = 1;
			target.MakePixelPerfect();
		}
		else HideBackground();
	}
}
