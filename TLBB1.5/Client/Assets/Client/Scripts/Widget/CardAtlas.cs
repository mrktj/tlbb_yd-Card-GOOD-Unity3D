using UnityEngine;
using System.Collections;

public class CardAtlas : MonoBehaviour {
	
	public UIAtlas[] cardAtlas;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public UIAtlas GetAtlasByName(string name)
	{
		for(int i = 0; i < cardAtlas.Length; i++)
		{
			if(cardAtlas[i].transform.name == name)
			{
				return cardAtlas[i];
			}
		}
		return null;
	}
}
