using UnityEngine;
using System.Collections;

public class RoundTip : MonoBehaviour {
	
	void Awake()
	{
		//Debug.Log("Awake(),------------------------------------------");
	}
	// Use this for initialization
	void Start () {
			//Debug.Log("Start(),------------------------------------------");
	}
	void OnEnable()
	{
		//Debug.Log("OnEnable(),------------------------------------------");
	}
	// Update is called once per frame
	void Update () {
	
	}
	
//	public delegate void AnimationCompletedHandler(GameObject go);
//	public AnimationCompletedHandler handler;
	
	public void OnAnimationCompleted()
	{
		Destroy(gameObject, 0.01f);
		//handler(gameObject);
	}
}
