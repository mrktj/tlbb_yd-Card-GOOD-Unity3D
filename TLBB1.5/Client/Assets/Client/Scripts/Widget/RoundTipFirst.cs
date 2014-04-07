using UnityEngine;
using System.Collections;

public class RoundTipFirst : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public delegate void AnimationCompletedHandler(GameObject go);
	public AnimationCompletedHandler handler;
	
	public void OnAnimationCompleted()
	{
		handler(gameObject);
	}
}
