using UnityEngine;
using System.Collections;
using GCGame.Table;
using Games.CharacterLogic;

namespace Games.Battle
{
    public class SkillCombine : SkillBase
    {
        private GameObject m_HetiObject = null;
        private GameObject m_BattlePlayer = null;
        private GameObject m_RootPanel = null;
        private int m_LoverTempID = -1;
        public int LoverTempID { get { return m_LoverTempID; } set { m_LoverTempID = value; } }

        private GameObject m_AttackWengzi = null;
        private GameObject m_BehitParticle = null;
        
        private GameObject m_HetiWengziObj;

        public SkillCombine(BattleCard owner, int id)
        {
            m_SkillOwner = owner;
            m_SkillId = id;
            m_SkillType = SkillType.E_SKILL_TYPE_COMBINE;
        }

        public override bool Init()
        {
            m_SkillTab = TableManager.GetSkillByID(m_SkillId);
            m_SkillDisplayTab = TableManager.GetSkillDisplayByID(m_SkillTab.Effect);
            m_BattleRoot = BattleUI.Instacne.battleRoot;

            return true;
        }

        public override bool LoadSkill()
        {
            //m_AttackAnimLib = Resources.Load("Effect/" + m_SkillDisplayTab.AttackLib, typeof(tk2dSpriteAnimation)) as tk2dSpriteAnimation;
            //m_BehitAnimLib = Resources.Load("Effect/" + m_SkillDisplayTab.BehitLib, typeof(tk2dSpriteAnimation)) as tk2dSpriteAnimation;

            m_AttackWengzi = Resources.Load("HetiWenZi/" + m_SkillDisplayTab.AttackAnim, typeof(GameObject)) as GameObject;
            if (m_SkillOwner.Card_Type == BattleCardType.E_BATTLE_CARD_TYPE_OTHER && m_SkillDisplayTab.BehitAnimSelf != "-1")
            {
                m_BehitParticle = Resources.Load("HTJ/" + m_SkillDisplayTab.BehitAnimSelf, typeof(GameObject)) as GameObject;
            }
            else
            {
                m_BehitParticle = Resources.Load("HTJ/" + m_SkillDisplayTab.BehitAnim, typeof(GameObject)) as GameObject;
            }

            m_HetiObject = Resources.Load("Widgets/He_Ti", typeof(GameObject)) as GameObject;
            if (m_HetiObject == null)
            {
                Debug.LogError("Can't load Widgets/He_Ti");
            }

            m_BattlePlayer = GameObject.Find("BattleLogic");
            if (m_BattlePlayer == null)
            {
                Debug.LogError("Cant not find battle player !");
            }
            m_RootPanel = GameObject.Find("Camera/Anchor/Panel");
            if (m_RootPanel == null)
            {
                Debug.LogError("Can't find Shake Panel !");
            }
            return true;
        }

        public override void UseSkill()
        {
            PlayAttackAnimation();
        }

        public void PlayAttackAnimation()
        {
            if (m_AttackWengzi == null)
            {
                Debug.LogError("Can not find res " + m_SkillDisplayTab.AttackAnim + " in " + m_SkillDisplayTab.AttackLib);
                EventManager.Instance.Fire(EventDefine.BATTLE_CARD_ATTACK_END, m_SkillOwner.BattleCardObj);
                PlayBehitAnimParticle();
                return;
            }
            GameObject go = (GameObject)GameObject.Instantiate(m_HetiObject);
            go.transform.parent = m_BattleRoot;
            go.transform.localPosition = new Vector3(384f, 512f, -70f);
            go.transform.localScale = Vector3.one;
            HeTi he_ti = go.GetComponent<HeTi>();
            he_ti.hetiCompleteDelegate += AttackAnimationCompleteHandler;
            he_ti.CardTempID = m_SkillOwner.Card_Data.CardTempId;
            he_ti.LoverTempID = m_LoverTempID;
            he_ti.WengziObj = m_AttackWengzi;
            he_ti.Play();

            //播放攻击音效
			
            AudioManager.Instance.PlayEffectSound(m_SkillDisplayTab.Music, Obj_MyselfPlayer.GetMe().acceleration);
        }
        
        void AttackAnimationCompleteHandler(GameObject go)
        {
            GameObject.Destroy(go, 0.01f);
            EventManager.Instance.Fire(EventDefine.BATTLE_CARD_ATTACK_END, m_SkillOwner.BattleCardObj);

            PlayBehitAnimParticle();
        }

        private void PlayBehitAnimParticle()
        {
            if (m_BehitParticle == null)
            {
                Debug.LogError("Can not find res  " + m_SkillDisplayTab.BehitAnim);
                for (int i = 0; i < m_TargetList.Count; i++)
                {
                    m_TargetList[i].AnimateBeHit(m_BehitMap[m_TargetList[i]]);
                    m_TargetList[i].ShowHitValue(m_BehitMap[m_TargetList[i]], this, false);
                    m_TargetList[i].OnHit(m_SkillOwner, m_BehitMap[m_TargetList[i]]);
                }
                return;
            }
						
            GameObject go = null;
            go = GameObject.Instantiate(m_BehitParticle) as GameObject;
            go.transform.parent = null;

            if (m_SkillOwner.Card_Type == BattleCardType.E_BATTLE_CARD_TYPE_SELF)
            {
                go.transform.position = new Vector3(0f, 0f, -2f);
            }
            else
            {
                if (m_SkillDisplayTab.BehitAnimSelf != "-1")
                {					
                	go.transform.position = new Vector3(0f, 0f, -2f);
                }
                else
                {
                	go.transform.position = new Vector3(0f, -1f, -2f);
                }
            }
            DestroyParticle dp = DestroyParticle.AddComponent(go, m_SkillDisplayTab.NumDelay, m_SkillDisplayTab.BehitAnimTime);
            dp.particleCompleteDelegate = BehitParticleCompleteHandler;
            dp.skillOnhitDelegate = SkillOnhtiHandler;

        }

        void BeHitAnimationCompleteHandler(tk2dAnimatedSprite animSprite, int clipId)
        {
            for (int i = 0; i < m_TargetList.Count; i++)
            {
                m_TargetList[i].OnHit(m_SkillOwner, m_BehitMap[m_TargetList[i]]);
            }
            GameObject.Destroy(animSprite.gameObject);
            m_RootPanel.SendMessage("StopShake");
        }

        void BehitParticleCompleteHandler(GameObject go)
        {
            for (int i = 0; i < m_TargetList.Count; i++)
            {
                m_TargetList[i].OnHit(m_SkillOwner, m_BehitMap[m_TargetList[i]]);
            }

            m_RootPanel.SendMessage("StopShake");
        }

        void SkillOnhtiHandler(GameObject go)
        {
            m_RootPanel.SendMessage("StartShake");

            for (int i = 0; i < m_TargetList.Count; i++)
            {
                m_TargetList[i].AnimateBeHit(m_BehitMap[m_TargetList[i]]);
                m_TargetList[i].ShowHitValue(m_BehitMap[m_TargetList[i]], this, false);
            }
        }

//         public void PlayBeHitAnimation()
//         {
//             if (m_BehitAnimLib == null || m_BehitAnimLib.GetClipIdByName(m_SkillDisplayTab.BehitAnim) == -1)
//             {
//                 Debug.LogError("Can not find res " + m_SkillDisplayTab.BehitAnim + " in " + m_SkillDisplayTab.BehitLib);
//                 for (int i = 0; i < m_TargetList.Count; i++)
//                 {
//                     m_TargetList[i].AnimateBeHit(m_BehitMap[m_TargetList[i]]);
//                     m_TargetList[i].ShowHitValue(m_BehitMap[m_TargetList[i]], false);
//                     m_TargetList[i].OnHit(m_BehitMap[m_TargetList[i]]);
//                 }
//                 return;
//             }
// 
//             GameObject go = new GameObject(m_SkillDisplayTab.BehitAnim);
//             go.transform.parent = m_BattleRoot;
//             if (m_SkillOwner.Card_Type == BattleCardType.E_BATTLE_CARD_TYPE_SELF)
//             {
//                 go.transform.localPosition = new Vector3(384f, 768f, -30);
//             }
//             else
//             {
//                 go.transform.localPosition = new Vector3(384f, 256f, -30);
//             }
//             go.transform.localScale = Vector3.one * 2;
//             tk2dAnimatedSprite behit = tk2dAnimatedSprite.AddComponent(go, m_BehitAnimLib, m_BehitAnimLib.GetClipIdByName(m_SkillDisplayTab.BehitAnim));
//             behit.animationCompleteDelegate += BeHitAnimationCompleteHandler;
//             behit.Play(m_SkillDisplayTab.BehitAnim);
// 
//             m_RootPanel.SendMessage("StartShake");
//         }

    }
}

