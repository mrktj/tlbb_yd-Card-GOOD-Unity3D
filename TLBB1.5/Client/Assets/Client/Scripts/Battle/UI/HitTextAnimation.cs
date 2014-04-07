using UnityEngine;
using System.Collections;

public class HitTextAnimation : MonoBehaviour {
	
	public delegate void TextShowDelegate(int cur_hp);
	public TextShowDelegate showDelegate;
	public  int curHp = 0;
	
	// Use this for initialization
	void Start () {
	
	}
	
	public void AnimationCompleted(GameObject text_object)
	{
		Destroy(transform.parent.gameObject);
	}
	public void OnTextShow(GameObject go)
	{
		if(showDelegate != null)
		{
			showDelegate(curHp);
		}
	}
}
