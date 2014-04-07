using UnityEngine;
using System.Collections;
using Games.Battle;
using Games.LogicObject;

public class BattleHpBar : MonoBehaviour {
	
	public BattleSlot owner;
	private float hpTo = 1.0f;
	private float hpCur = 1.0f;
	bool animate = false;
	private float dx = 0.01f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//owner.hpbar.sliderValue = hp_from
		if(gameObject.GetComponent<UISlider>().sliderValue == hpTo)
		{
			return;
		}
		
		if(animate)
		{
			gameObject.GetComponent<UISlider>().sliderValue += dx;
			float ds = Mathf.Abs(gameObject.GetComponent<UISlider>().sliderValue - hpTo);
			if(ds <= Mathf.Abs(dx))
			{
				gameObject.GetComponent<UISlider>().sliderValue = hpTo;
			}
		}
		else
		{
			gameObject.GetComponent<UISlider>().sliderValue = hpTo;
		}
	}
	public void Reset()
	{
		hpTo = 1.0f;
		hpCur = 1.0f;
		animate = false;
	}
	public void ShowHp(float hp_to, bool is_animate)
	{
		//直接结束上一次动画--
		if(gameObject.GetComponent<UISlider>().sliderValue != hpTo)
		{
			gameObject.GetComponent<UISlider>().sliderValue = hpTo;
		}
		if(hp_to >= 1.0f)
		{
			hp_to = 1.0f;
		}
		if(hp_to <= 0.0f)
		{
			hp_to = 0.0f;
		}
//		if(hp_to <= 0.1f && hp_to >= 0.001f)
//		{
//			hp_to = 0.1f;
//		}
		hpCur = hpTo;
		hpTo = hp_to;
		
		if((hpTo - hpCur) == 0f)
		{
			return;
		}
		else if((hpTo - hpCur) > 0f)
		{
			dx = Mathf.Max(0.0005f, Mathf.Abs((hpTo - hpCur) * 0.0625f));//除以16
		}
		else
		{
            dx = -Mathf.Max(0.0005f, Mathf.Abs((hpTo - hpCur) * 0.0625f));//除以16
			
		}
				
		animate = is_animate;
	}
}
