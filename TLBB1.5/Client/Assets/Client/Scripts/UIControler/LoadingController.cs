using UnityEngine;
using System.Collections;
using Games.CharacterLogic;

public class LoadingController : MonoBehaviour 
{

    public GameObject load;
    public float process;
    bool beStart;
    public GameObject LabelLoading;
    public GameObject LabelTip;
    public GameObject SpriteLoading;
    public GameObject LoadingProgress;
    float time;
    float scale = 1.0f;//66666496f;

    private static LoadingController _instance;
    public static LoadingController Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindGameObjectWithTag("LoadingController").GetComponent<LoadingController>() ;
            }
            return _instance;
        }

    }
	void Start () 
    {
        load = gameObject;
        Obj_MyselfPlayer.GetMe().battel_sign = 0;
        process = 0f;
        beStart = false;
        time = 0.0f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (beStart)
        {
            if(GameManager.Instance.sceneName.Equals("MainUI"))
            {
                slide_in_miainui();
            }
            if (GameManager.Instance.sceneName.Equals("BattleUI"))
            {
                slide_in_battel();
            }

        }
	}

    public void Reset()
    {
        load.transform.localPosition = new Vector3(10000f, 10000f, -500f);
        Obj_MyselfPlayer.GetMe().battel_sign = 0;
        ResourceManager.Instance.load_map_num = 0;
        process = 0f;
        beStart = false;
        Obj_MyselfPlayer.GetMe().loading_process = 0;
        time = 0.0f;
		enabled = false;
    }

    public void show_load()
    {
        //load = GameObject.Find("LoadingController").gameObject;
        load.transform.localPosition = new Vector3(0f, 0f, -500f);
        //LabelTip.GetComponent<UILabel>().text = "小窍门:如果您第一次加载游戏，可能需要几秒钟的时间，请等待。。。";
        if (GameManager.Instance.sceneName.Equals("MainUI"))
        {
            Obj_MyselfPlayer.GetMe().battel_sign = 0;
        }
        if (GameManager.Instance.sceneName.Equals("BattleUI"))
        {

            load.transform.localScale = new Vector3(scale, scale, 1);
            process = Obj_MyselfPlayer.GetMe().loading_process;
            Debug.Log("process: " + process);
            LabelLoading.GetComponent<UILabel>().text = "正在载入地图，请稍候...";
        }
        beStart = true;
		enabled = true;
    }

    void SetSliderValue(float value)
    {
//        Vector3 pos = SpriteLoading.transform.localPosition;
//        float sizeX = LoadingProgress.transform.FindChild("Foreground").gameObject.transform.localScale.x;
//        pos.Set((float)(-0.5 * sizeX + sizeX * value), pos.y, pos.z);
//        SpriteLoading.gameObject.transform.localPosition = pos;
        LoadingProgress.GetComponent<UISlider>().sliderValue = value;
    }


    void slide_in_battel()
    {
        process += Time.smoothDeltaTime * 1.1f;
        if (process > 0.95f)
        {
            process = 1.0f;
            SetSliderValue(1.0f);
        }
        else
        {
            SetSliderValue(process);
        }
    }

    void slide_in_miainui()
    {
        if (Obj_MyselfPlayer.GetMe().battel_sign == 0)
        {
            time += Time.deltaTime;
            if (time > 15.0f)   //如果等待时间超过15秒默认认为没有收到战斗返回包
            {
                Reset();
            }
            SetSliderValue(process);
            if (process > 0.55f)
            {
                //通信传输进度条数值，剩下40%留给场景加载
                process = 0.6f;
                SetSliderValue(0.6f);
            }
            else
            {
                process += Time.smoothDeltaTime * 0.8f;
            }
        }
        if (Obj_MyselfPlayer.GetMe().battel_sign == 1)   //网络通信出错退回原界面//loading界面清除//
        {
            Reset();
        }
        if (Obj_MyselfPlayer.GetMe().battel_sign == 2 )  //通讯成功，收到返回包//
        {
            if (process < 0.55f)
            {
                process += Time.smoothDeltaTime * 0.8f;
            }
            else if (process < 0.8f)
            {
                process += Time.smoothDeltaTime * 0.3f;
            }
            else
            {
                process = 0.8f;
            }
            SetSliderValue(process);
            LabelLoading.GetComponent<UILabel>().text = "正在加载场景，请稍候...";
            Obj_MyselfPlayer.GetMe().loading_process = process;
        }
    }

}
