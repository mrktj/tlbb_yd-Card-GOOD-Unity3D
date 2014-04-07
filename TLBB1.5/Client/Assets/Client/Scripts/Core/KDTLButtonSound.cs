//----------------------------------------------
// 2013-8-5	Jack Wen
//----------------------------------------------
using UnityEngine;
using System.Collections;

[AddComponentMenu("KDTL/Button/Button Sound")]
public class KDTLButtonSound : MonoBehaviour {
	
	public enum Trigger
	{
		OnClick,
		OnMouseOver,
		OnMouseOut,
		OnPress,
		OnRelease,
	}
	public Trigger trigger = Trigger.OnClick;
	public string soundName = "click";
	
	public GameObject soundManager;
	
	void OnHover (bool isOver)
	{
		if (enabled && ((isOver && trigger == Trigger.OnMouseOver) || (!isOver && trigger == Trigger.OnMouseOut)))
		{
			AudioManager.Instance.PlayEffectSound(soundName);
			//AudioController.Play(soundName);
		}
	}

	void OnPress (bool isPressed)
	{
		if (enabled && ((isPressed && trigger == Trigger.OnPress) || (!isPressed && trigger == Trigger.OnRelease)))
		{
			AudioManager.Instance.PlayEffectSound(soundName);
			//AudioController.Play(soundName);
		}
	}

	void OnClick ()
	{
		if (enabled && trigger == Trigger.OnClick)
		{
			AudioManager.Instance.PlayEffectSound(soundName);
//			AudioController.Play(soundName);
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
