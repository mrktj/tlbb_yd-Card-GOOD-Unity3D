using UnityEngine;
using System.Collections;

public class InitScale : MonoBehaviour {
	public Vector3 scale;
	// Use this for initialization
	void Start () {
		transform.localScale = scale;
	}
}
