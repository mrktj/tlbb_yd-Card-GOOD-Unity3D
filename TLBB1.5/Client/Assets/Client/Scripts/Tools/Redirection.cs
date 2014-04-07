using UnityEngine;
using System.Collections;

public class Redirection : MonoBehaviour {
	public Vector3 direction;
	// Use this for initialization
	void Start () {
		transform.eulerAngles = direction;
	}
	
	
}
