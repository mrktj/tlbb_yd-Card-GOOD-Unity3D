//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using System;
/// <summary>
/// Sample script showing how easy it is to implement a standard button that swaps sprites.
/// </summary>

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Image Button")]
public class UIImageButton : MonoBehaviour
{
	public UISprite target;
	public string normalSprite;
	public string hoverSprite;
	public string pressedSprite;
	public string disabledSprite;
	
	
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
				UpdateAllImage();
			}
		}
	}

	void Awake () { if (target == null) target = GetComponentInChildren<UISprite>();}
	void OnEnable () { UpdateAllImage(); }
	
	public void UpdateImage()
	{
		if (target != null)
		{
			if (isEnabled)
			{
				target.spriteName = UICamera.IsHighlighted(gameObject) ? hoverSprite : normalSprite;
			}
			else
			{
				target.spriteName = disabledSprite;
			}	
			target.MakePixelPerfect();
		}
	}
	
	public void UpdateAllImage()
	{
		//zbz editor(增加支持一个GameOBject 绑定多个UIImageButton)
		UIImageButton[] imgBtn =  transform.GetComponents<UIImageButton>();
		if(imgBtn== null || imgBtn.Length == 0)
		{
			return;
		}
		foreach(UIImageButton btn in imgBtn)
		{
			btn.UpdateImage();
		}
	}

	void OnHover (bool isOver)
	{
		if (isEnabled && target != null)
		{
			target.spriteName = isOver ? hoverSprite : normalSprite;
			target.MakePixelPerfect();
		}
	}

	void OnPress (bool pressed)
	{
		if (pressed)
		{
			target.spriteName = pressedSprite;
			target.MakePixelPerfect();
		}
		else UpdateAllImage();
	}
}
