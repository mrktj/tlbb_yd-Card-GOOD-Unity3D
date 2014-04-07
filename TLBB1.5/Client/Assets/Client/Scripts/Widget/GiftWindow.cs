using UnityEngine;
using System.Collections;
using card.net;

public class GiftWindow : MonoBehaviour {

    public UIInput labelInput;

    public static bool bSuccessGet = false;
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void OpenWindow(GameObject parentWidget)
    {
        GameObject window = ResourceManager.Instance.loadWidget("GiftWindow");
        window.transform.parent = parentWidget.transform;
        window.transform.localScale = Vector3.one;
        window.transform.localPosition = Vector3.zero;
        bSuccessGet = false;
    }

    public void CloseWindow()
    {
        Destroy(this.gameObject);
    }

    public void OnGet()
    {
        string strCode = labelInput.GetComponent<UIInput>().text;

        if (strCode.Length == 0)
        {
            BoxManager.showMessageByID((int)MessageIdEnum.Msg146);
            return;
        }
        NetworkSender.Instance().VarifyActiveCode(Ret_OnGet, strCode);
    }

    void Ret_OnGet(bool bSuccess)
    {
        if (bSuccess)
        {
            GameObject.FindWithTag("main_controller").SendMessage("updateUserInfo");
            CloseWindow();
//            BoxManager.showMessageByID((int)MessageIdEnum.Msg135);
			BoxManager.showMessageByID((int)MessageIdEnum.Msg150);
        }
    }
}
