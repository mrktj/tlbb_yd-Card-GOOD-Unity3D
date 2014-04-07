using UnityEngine;
using System.Collections;

public class ChonglouReword : MonoBehaviour {
	
	public UILabel moneyNum = null;
	public UILabel yuanbaoNum = null;
	
	public void Display(int money,int yuanbao)
	{
		moneyNum.text = money.ToString();
		yuanbaoNum.text = yuanbao.ToString();
		this.transform.localPosition = new Vector3(0f,0f,-64.61322f);
		this.gameObject.SetActive(true);
		Invoke("Disappear",2.5f);
	}
	
	private void Disappear()
	{
		this.gameObject.SetActive(false);
	}
	
}
