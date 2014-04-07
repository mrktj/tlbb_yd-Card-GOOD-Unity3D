using UnityEngine;
using System.Collections;

public class FogKit : MonoBehaviour {
	public GameObject fogLight = null;
	public GameObject fogObj = null;
	
	public GameObject detectTarget = null;
	
	// Update is called once per frame
	void Update () 
	{
		tk2dSprite sprite = detectTarget.GetComponent<tk2dSprite>();
		if(sprite!= null)
		{
			if(sprite.Collection.spriteCollectionName == "BattleUICollection7")
			{
				fogLight.SetActive(true);
				fogObj.SetActive(true);
			}
			this.enabled = false;
		}
	}
}
