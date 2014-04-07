using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Games.CharacterLogic;
using Games.LogicObject;
using GCGame.Table;
using card.net;

namespace Games.Battle
{
    public sealed class BattlePlayer
    {
        private BattleCore m_BattleCore = null;
        private BattleProcedureManager m_ProcedureManager = null;
		public BattleProcedureType GetBattleStateType(){ return m_ProcedureManager.GetBattleProcedureType();}
        //Data
        private int m_RoundCounter = 1;
        public int RoundCounter { get { return m_RoundCounter; } }
        public int GetTurnCounter() { return m_ProcedureManager.GetTurnCounter(); }

        private BattleRoundData m_BattleRoundData = null;
        public BattleRoundData GetBattleRoundData() { return m_BattleRoundData; }

        //UI
        private GameObject m_BattleTopUI = null;
        private GameObject m_AccelerateBtn = null;
        private GameObject m_AutoForwardBtn = null;
        private BattleBackground m_BattleBackground = null;

        private float m_ForwardBtnPressCounter = Time.realtimeSinceStartup;

        public BattlePlayer(BattleCore core)
        {
            m_BattleCore = core;
            m_ProcedureManager = new BattleProcedureManager(this);

            EventManager.Instance.RegisterEventHandler(EventDefine.BATTLE_FORWARD_BTN_CLICKED, OnForwardBtn);
            EventManager.Instance.RegisterEventHandler(EventDefine.BATTLE_ACCLERATE_BTN_CLIKED, OnAccelerateBtn);
            EventManager.Instance.RegisterEventHandler(EventDefine.BATTLE_AUTO_FORWARD_BTN_CLIKED, OnAutoForwardBtn);
			EventManager.Instance.RegisterEventHandler(EventDefine.BATTLE_QUIT_BTN_CLICK, OnQuitBattle);
			
        }

        public bool Init()
        {
            if (!m_ProcedureManager.Init())
            {
                Debug.LogError("战斗流程初始化错误！");
                return false;
            }

            InitPlayer();

            return true;
        }
        
        public void Update()
        {
            m_ProcedureManager.Update();
        }

        private void InitPlayer()
        {
			//Init UI
            m_BattleTopUI = BattleUI.Instacne.battleTopUI;
            m_AccelerateBtn = BattleUI.Instacne.accelerateBtn;
            m_AutoForwardBtn = BattleUI.Instacne.autoForwardBtn;
            m_BattleBackground = BattleUI.Instacne.battleBg;			
            m_RoundCounter = 1;

            for (int i = 0; i < 6; i++)
            {
                Obj_MyselfPlayer.GetMe().cardStates[i] = 0;
            }

            Obj_MyselfPlayer.GetMe().reviveCount = 0;

            SetBatttleData(Obj_MyselfPlayer.GetMe().battleData, Obj_MyselfPlayer.GetMe().lastBattleDrops);

            if (Obj_MyselfPlayer.GetMe().isGuideBattle)
            {
                m_RoundCounter = 3;
                m_AccelerateBtn.gameObject.SetActive(false);
                m_AutoForwardBtn.gameObject.SetActive(false);
            }
            else
			{
				switch(Obj_MyselfPlayer.GetMe().battleType)
				{
				case BattleType.PVP:
				case BattleType.QxzbPvP:
					m_RoundCounter = 3;
                	m_AutoForwardBtn.gameObject.SetActive(false);
					break;
				case BattleType.PVE:
	                m_AutoForwardBtn.gameObject.SetActive(true);	
	                if (Obj_MyselfPlayer.GetMe().isAutoFowrard)
	                {
	                    m_AutoForwardBtn.transform.FindChild("Sprite-AutoForward").GetComponent<UISprite>().spriteName = "zhandou_shoudongqianjin";
	                }
	                else
	                {
	                    m_AutoForwardBtn.transform.FindChild("Sprite-AutoForward").GetComponent<UISprite>().spriteName = "zhandou_zidongqianjin";
	                }	
	                m_BattleBackground.SetSubCopyID(Obj_MyselfPlayer.GetMe().curSubcopy.subCopyID);
					break;
				
				case BattleType.WORLD_BOSS:
					m_AutoForwardBtn.gameObject.SetActive(false);
					m_RoundCounter = 3;
					break;
				case BattleType.CHONG_LOU:
					m_AutoForwardBtn.gameObject.SetActive(true);
					if (Obj_MyselfPlayer.GetMe().isAutoFowrard)
	                {
	                    m_AutoForwardBtn.transform.FindChild("Sprite-AutoForward").GetComponent<UISprite>().spriteName = "zhandou_shoudongqianjin";
	                }
	                else
	                {
	                    m_AutoForwardBtn.transform.FindChild("Sprite-AutoForward").GetComponent<UISprite>().spriteName = "zhandou_zidongqianjin";
	                }
					break;
				}
				SetBattleAccelerate(Obj_MyselfPlayer.GetMe().acceleration);
			}
			
			//海外版,战斗加速逻辑修改;
            if (Obj_MyselfPlayer.GetMe().level >= 5 && !PlayerPrefs.HasKey("accelerateBtn" + Obj_MyselfPlayer.GetMe().accountID.ToString()))
            {
                m_AccelerateBtn.transform.FindChild("Sprite-Bg").gameObject.GetComponent<TweenAlpha>().enabled = true;
                m_AccelerateBtn.transform.FindChild("Sprite-Num").gameObject.GetComponent<TweenAlpha>().enabled = true;
                m_AccelerateBtn.transform.FindChild("Sprite-X").gameObject.GetComponent<TweenAlpha>().enabled = true;
            }
			
			//音乐逻辑需要修改
            if (Obj_MyselfPlayer.GetMe().isGuideBattle)
            {
                PlayBackgroundMusicRandom();
            }
            else
            {
				switch(Obj_MyselfPlayer.GetMe().battleType)
				{
				case BattleType.PVE:
					Tab_Copydetail tab_detail = TableManager.GetCopydetailByID(Obj_MyselfPlayer.GetMe().curSubcopy.subCopyID);
                	AudioManager.Instance.PlayBackgroundMusic(tab_detail.CopyMusic);
					break;
				case BattleType.PVP:
				case BattleType.QxzbPvP:
				case BattleType.CHONG_LOU:
					PlayBackgroundMusicRandom();
					break;
				case BattleType.WORLD_BOSS:
					Tab_Worldboss tab_worldBoss = TableManager.GetWorldbossByID(Obj_MyselfPlayer.GetMe().activeBoss.id);
					
					if(tab_worldBoss == null)
					{
						Debug.LogError("world_boss id error");
					}
					else
					{
						AudioManager.Instance.PlayBackgroundMusic(tab_worldBoss.CopyMusic);
					}
					break;
				}
                
            }
        }
		
		private void PlayBackgroundMusicRandom()
		{
			int copy_id = UnityEngine.Random.Range(1, 180);
            Tab_Copydetail tab_detail = TableManager.GetCopydetailByID(copy_id);
            if (tab_detail != null)
            {
                AudioManager.Instance.PlayBackgroundMusic(tab_detail.CopyMusic);
            }
            else
            {
                tab_detail = TableManager.GetCopydetailByID(1);
                AudioManager.Instance.PlayBackgroundMusic(tab_detail.CopyMusic);
            }
		}
		
        private void ResetPlayer()
        {
            SetBatttleData(Obj_MyselfPlayer.GetMe().battleData, Obj_MyselfPlayer.GetMe().lastBattleDrops);
            BattleCardManager.Instance.ResetCard(BattleCardType.E_BATTLE_CARD_TYPE_SELF);
            m_ProcedureManager.ChangProcedure(BattleProcedureType.E_BATTLE_PROCEDURE_CARD_FORWARD);
        }

        private void SetBatttleData(BattleRoundData round_data, List<LogicObject.DropBag> drop_bags)
        {
            m_BattleRoundData = round_data;

            if (m_BattleRoundData.isLastBattle)
            {
				m_RoundCounter = m_BattleRoundData.roundCount;
                int main_id = TableManager.GetCopydetailByID(m_BattleRoundData.missionID).Copyfather;
                MainCopy main_copy = new MainCopy(main_id);
                SubCopy sub_copy = new SubCopy(m_BattleRoundData.missionID);
                Obj_MyselfPlayer.GetMe().curMainCopy = main_copy;
                Obj_MyselfPlayer.GetMe().curSubcopy = sub_copy;

                string log = "setBattleData(), isLastBattle = true, roundCount = " + m_RoundCounter;
                log += ", main_copy = " + main_id;
                log += ", sub_copy = " + m_BattleRoundData.missionID;
                Debug.Log(log);

                InitCopys();
                Obj_MyselfPlayer.GetMe().UpdateCopyList(CopyType.NORMAL);
                //记录当前小副本的大副本是否已经开启 by 王明磊
                foreach (MainCopy main in Obj_MyselfPlayer.GetMe().normalMainCopys)
                {
                    if (main.copyId == sub_copy.tblCopyDetail.Copyfather && main.copyState == CopyState.PASSED)
                    {
                        Debug.Log("Next Main Copy Already Opened");
                        Obj_MyselfPlayer.GetMe().isNextMainOpened = true;
                        break;
                    }
                }

                foreach (DropBag drop_bag in drop_bags)
                {
                    if (drop_bag.type == DropType.CARD)
                    {
                        m_BattleTopUI.GetComponent<BattleTopUI>().AddCard(1);
                        Debug.Log("Add card");
                    }
                    else if (drop_bag.type == DropType.TIEM)
                    {
                        m_BattleTopUI.GetComponent<BattleTopUI>().AddItem(1);
                        Debug.Log("Add AddItem");

                    }
                    else if (drop_bag.type == DropType.MONEY)
                    {
                        m_BattleTopUI.GetComponent<BattleTopUI>().AddMoney(drop_bag.val);
                        Debug.Log("Add AddMoney");
                    }
                }
            }
        }

        void InitCopys()
        {
            Obj_MyselfPlayer.GetMe().normalMainCopys.Clear();
            Obj_MyselfPlayer.GetMe().activityMainCopys.Clear();
            Hashtable copy_table = TableManager.GetCopy();
            foreach (DictionaryEntry pair in copy_table)
            {
                MainCopy copy = new MainCopy((int)pair.Key);
                if (copy.tblCopy.GetCopyTypebyIndex(0) == (int)CopyType.NORMAL)
                {
                    Obj_MyselfPlayer.GetMe().normalMainCopys.Add(copy);
                }
                else
                {
                    Obj_MyselfPlayer.GetMe().activityMainCopys.Add(copy);
                }
            }
        }

        private void SetBattleAccelerate(float speed)
        {
            string sp_name = "zhujiemian_wodetuandui_zi_" + Obj_MyselfPlayer.GetMe().acceleration;
            UISprite num = m_AccelerateBtn.transform.FindChild("Sprite-Num").transform.gameObject.GetComponent<UISprite>();
            num.spriteName = sp_name;
            num.MakePixelPerfect();
            Time.timeScale = speed;
        }

        private void OnForwardBtn(EventDefine type, System.Object[] args)
        {
            if (GetBattleStateType() != BattleProcedureType.E_BATTLE_PROCEDURE_WAITING)
            {
                return;
            }

            if (Time.realtimeSinceStartup - m_ForwardBtnPressCounter < 3f / Time.timeScale)
            {
                return;
            }

            m_ForwardBtnPressCounter = Time.realtimeSinceStartup;

            //王明磊 : 统计模块代码 -> Statistics
            //如果是Guide阶段,需要统计此按钮的点击信息
            if (GameManager.Instance.isGuideMode())
            {
                switch (m_RoundCounter)
                {
                    case 1:
                        PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn20).ToString());
                        break;
                    case 2:
                        PlayerPrefsX.StatisticsIncrease("Btn" + ((int)StatisticsEnum.Btn21).ToString());
                        break;
                }
            }

            BattleCardManager.Instance.UpdateCardState();
			if(Obj_MyselfPlayer.GetMe().battleType == BattleType.PVE)
			{
            	NetworkSender.Instance().AskBattleData(OnAskContinueBattleRet, m_RoundCounter + 1, Obj_MyselfPlayer.GetMe().curSubcopy.subCopyID);
			}
			else if(Obj_MyselfPlayer.GetMe().battleType == BattleType.CHONG_LOU)
			{
				NetworkSender.Instance().AskChonglouBattleData(OnAskContinuePataBattleRet);
			}
        }
		
		public void OnAskContinuePataBattleRet(bool isSucceed)
        {
            if (isSucceed)
            {
				Debug.Log("OnAskContinuePataBattleRet(), isSucceed = true");
                if (m_ProcedureManager.GetActiveProcedure().GetProcedureType() == BattleProcedureType.E_BATTLE_PROCEDURE_WAITING)
                {
					m_BattleRoundData = Obj_MyselfPlayer.GetMe().battleData;
                    BattleCardManager.Instance.ResetCard(BattleCardType.E_BATTLE_CARD_TYPE_SELF);
            		m_ProcedureManager.ChangProcedure(BattleProcedureType.E_BATTLE_PROCEDURE_CARD_FORWARD);
                }
            }
            else
            {
                Debug.LogError("OnAskContinueBattleRet(), isSucceed = false");
            }
        }

        public void OnAskContinueBattleRet(bool isSucceed)
        {
            if (isSucceed)
            {
				Debug.Log("OnAskContinueBattleRet(), isSucceed = true");
                if (m_ProcedureManager.GetActiveProcedure().GetProcedureType() == BattleProcedureType.E_BATTLE_PROCEDURE_WAITING)
                {
                    m_RoundCounter++;
                    ResetPlayer();
                }
            }
            else
            {
                Debug.LogError("OnAskContinueBattleRet(), isSucceed = false");
            }
        }

        public void OnAccelerateBtn(EventDefine type, System.Object[] args)
        {
            if (Obj_MyselfPlayer.GetMe().level < 5)
            {
                //comfirmDialog.GetComponent<ComfirmDialog>().ShowDialog(ComfirmDialog.ACCELERATE_LIMIT_MESSAGE);
                BoxManager.showMessageByID((int)MessageIdEnum.Msg111, "5");
            }
            else
            {
                if (m_AccelerateBtn.transform.FindChild("Sprite-Bg").gameObject.GetComponent<TweenAlpha>().enabled == true)
                {
                    m_AccelerateBtn.transform.FindChild("Sprite-Bg").gameObject.GetComponent<TweenAlpha>().enabled = false;
                    m_AccelerateBtn.transform.FindChild("Sprite-Num").gameObject.GetComponent<TweenAlpha>().enabled = false;
                    m_AccelerateBtn.transform.FindChild("Sprite-X").gameObject.GetComponent<TweenAlpha>().enabled = false;
                    m_AccelerateBtn.transform.FindChild("Sprite-Bg").gameObject.GetComponent<UISprite>().alpha = 1f;
                    m_AccelerateBtn.transform.FindChild("Sprite-Num").gameObject.GetComponent<UISprite>().alpha = 1f;
                    m_AccelerateBtn.transform.FindChild("Sprite-X").gameObject.GetComponent<UISprite>().alpha = 1f;
                    PlayerPrefs.SetInt("accelerateBtn" + Obj_MyselfPlayer.GetMe().accountID.ToString(),1);
                }
				
				if(Obj_MyselfPlayer.GetMe().acceleration == 1 ){
					if(Obj_MyselfPlayer.GetMe().purchaseDollar <= 0 &&
						Obj_MyselfPlayer.GetMe().level < 45){
						BoxManager.showMessageByID((int)MessageIdEnum.Msg800, "45","3");
					}
					Obj_MyselfPlayer.GetMe().acceleration++;
				}else if(Obj_MyselfPlayer.GetMe().acceleration == 2){
					if(Obj_MyselfPlayer.GetMe().purchaseDollar <= 0 &&
						Obj_MyselfPlayer.GetMe().level < 45){
						Obj_MyselfPlayer.GetMe().acceleration = 1;
					}else{
						Obj_MyselfPlayer.GetMe().acceleration++;	
					}
					
				}else{
					Obj_MyselfPlayer.GetMe().acceleration++;
				}
				
				
				if(Obj_MyselfPlayer.GetMe().acceleration > 3)
				{
					Obj_MyselfPlayer.GetMe().acceleration = 1;
				}
				SetBattleAccelerate(Obj_MyselfPlayer.GetMe().acceleration);
            }
        }
		
		public void OnQuitBattle(EventDefine type, System.Object[] args)
		{
			if (GetBattleStateType() != BattleProcedureType.E_BATTLE_PROCEDURE_WAITING)
            {
                return;
            }
			Obj_MyselfPlayer.GetMe().pataClearFlag = 2;
			m_ProcedureManager.ChangProcedure(BattleProcedureType.E_BATTLE_PROCEDURE_BATTLE_END);
		}

        public void OnAutoForwardBtn(EventDefine type, System.Object[] args)
        {
            Obj_MyselfPlayer.GetMe().isAutoFowrard = !Obj_MyselfPlayer.GetMe().isAutoFowrard;
            if (Obj_MyselfPlayer.GetMe().isAutoFowrard)
            {
                m_AutoForwardBtn.transform.FindChild("Sprite-AutoForward").GetComponent<UISprite>().spriteName = "zhandou_shoudongqianjin";
            }
            else
            {
                m_AutoForwardBtn.transform.FindChild("Sprite-AutoForward").GetComponent<UISprite>().spriteName = "zhandou_zidongqianjin";
            }
        }

        public Dictionary<int, List<ActionBag>> BattleReportReader(BattleRoundData round_data)
        {
            if (round_data == null)
            {
                Debug.LogError("BattleReportReader(), m_BattleRoundData == null");
                return null;
            }
            int turn_counter = 0;

            Dictionary<int, List<ActionBag>> battle_action_dic = new Dictionary<int, List<ActionBag>>();

            foreach (BattleTurn turn in round_data.battleRound.battleTurns)
            {
                List<ActionBag> action_list = new List<ActionBag>();
                foreach (BattleStep step in turn.battleSteps)
                {
                    if (step.attacks.Count == 0)
                    {
                        Debug.LogError("BattleReportReader(), step.attacks.Count == 0");
                        continue;
                    }

                    BattleCard attack_card = BattleCardManager.Instance.FindCardBySlotIndex(step.attacks[0].slotIndex);
                    if (attack_card == null)
                    {
                        Debug.LogError("BattleReportReader(), attack_card == null");
                        continue;
                    }

                    AttackBag attack_bag = new AttackBag();
                    attack_bag.skillID = step.attacks[0].skillID;
                    attack_bag.curHp = step.attacks[0].curHp;
                    attack_bag.loverIdx = step.attacks[0].hetiIndex;
                    attack_bag.attackBuffs = step.attacks[0].buff;

                    Dictionary<BattleCard, BeHitBag> behit_map = new Dictionary<BattleCard, BeHitBag>();
                    foreach (StepAction action in step.behits)
                    {
                        BattleCard behit_card = BattleCardManager.Instance.FindCardBySlotIndex(action.slotIndex);
                        if (behit_card == null)
                        {
                            Debug.LogError("BattleReportReader(), behit_card == null");
                            continue;
                        }

                        BeHitBag behit_bag = new BeHitBag();
                        behit_bag.behitValue = action.attackHp;
                        behit_bag.curHp = action.curHp;
                        behit_bag.isCrit = action.isStorm;
                        behit_bag.behitBuffs = action.buff;

                        behit_map.Add(behit_card, behit_bag);
                    }
                    
                    if (behit_map.Count == 0)
                    {
                        Debug.LogError("BattleReportReader(), behit_map.Count == 0");
                        continue;
                    }

                    ActionBag action_bag = new ActionBag();
                    action_bag.attackCard = attack_card;
                    action_bag.attackBag = attack_bag;
                    action_bag.behitMap = behit_map;
                    action_list.Add(action_bag);
                }
                battle_action_dic.Add(turn_counter, action_list);                
            }

            return battle_action_dic;
        }

        public void OnDestroy()
        {
            m_ProcedureManager = null;
        }
    }
}
