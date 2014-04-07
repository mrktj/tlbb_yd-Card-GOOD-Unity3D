using UnityEngine;
using System.Collections;
using Games.Battle;
using Games.CharacterLogic;
using GCGame.Table;

public class BattleProcedureResourceLoad : BattleProcedureBase
{

    private BattleProcedureManager m_Manager;
	private float beginTime = 0f;
    public override BattleProcedureType GetProcedureType()
    {
        return BattleProcedureType.E_BATTLE_PROCEDURE_RESOURCE_LOAD;
        
    }
    public override bool Init(BattleProcedureManager manager)
    {
        m_Manager = manager;
        return true;
    }

    public override void OnEnter()
    {
		LoadingCoverKit.ShowLoadingCover(RollingNotice.GetRandomNotice());
		beginTime = Time.time;
    }
	

    public override void OnLeave()
    {
		LoadingCoverKit.CoverOver();
    }

    public override void Update()
    {
//        if (LoadingController.Instance.isDone == true)
//        {

#if UNITY_STANDALONE 
            ResourceManager.Instance.load_map_num = 3;
#endif
//            Debug.Log(LoadingController.Instance.isDone + "--" + ResourceManager.Instance.load_map_num);
            if (ResourceManager.Instance.load_map_num == 3 && Time.time - beginTime > 0.5f)
            {
                m_Manager.ChangProcedure(BattleProcedureType.E_BATTLE_PROCEDURE_BATTLE_START);
            }
//        }
    }
}
