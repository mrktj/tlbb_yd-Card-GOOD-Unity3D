using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GCGame.Table;

namespace Games.Battle
{
    public class BuffGeneric : BuffBase
    {
        protected tk2dSpriteAnimation m_BuffAnimLib = null;

        protected tk2dAnimatedSprite m_BuffSprite = null;

        protected GameObject m_ParticleAnimLib = null;

        public BuffGeneric(BattleCard owner, int id, int bf_value)
        {
            m_BuffOwner = owner;
            m_BuffId = id;
            m_BuffValue = bf_value;
        }

        public override bool Init()
        {
            m_TabBuff = TableManager.GetBuffByID(m_BuffId);
            m_TabBfEffect = TableManager.GetBuffEffectByID(m_TabBuff.EffectID);

            return true;
        }

        public override bool LoadBuff()
        {
            if (m_TabBfEffect.EffectType == (int)BuffEffectType.E_BUFF_EFFECT_TYPE_PARTICLE)
            {
                m_ParticleAnimLib = Resources.Load("EffectParticle/" + m_TabBfEffect.BuffAnim, typeof(GameObject)) as GameObject;
                if (m_ParticleAnimLib == null)
                {
                    Debug.LogError("LoadBuff failed, " + m_TabBfEffect.BuffAnim);
                    return false;
                }
            }
            else
            {
                m_BuffAnimLib = Resources.Load("Effect/" + m_TabBfEffect.BuffLib, typeof(tk2dSpriteAnimation)) as tk2dSpriteAnimation;
                if (m_BuffAnimLib == null)
                {
                    Debug.LogError("LoadBuff failed, " + m_TabBfEffect.BuffLib);
                    return false;
                }
            }

            return true;
        }

        public override void UseBuff()
        {
            if (m_TabBfEffect.EffectType == (int)BuffEffectType.E_BUFF_EFFECT_TYPE_PARTICLE)
            {
                m_BuffObj = GameObject.Instantiate(m_ParticleAnimLib) as GameObject;
                m_BuffObj.name = "Buff_" + m_BuffId;
                InitPosition();
            }
            else
            {
                m_BuffObj = new GameObject("Buff_" + m_BuffId);
                InitPosition();
                if (m_BuffAnimLib == null || m_BuffAnimLib.GetClipIdByName(m_TabBfEffect.BuffAnim) == -1)
                {
                    Debug.LogError("Can't play animation! " + m_TabBfEffect.BuffAnim);
                    return;
                }
                
                m_BuffSprite = tk2dAnimatedSprite.AddComponent(m_BuffObj, m_BuffAnimLib, m_BuffAnimLib.GetClipIdByName(m_TabBfEffect.BuffAnim));
                m_BuffSprite.Play(m_TabBfEffect.BuffAnim);
            }
        }

        public override void ShowBuffValue(GameObject pos_go = null)
        {
            if (m_BuffValue == 0)
            {
                return;
            }
            tk2dTextMesh hitText = null;
			Vector3 pos = new Vector3(0.0f, 0.0f, -25.0f);
            if (pos_go != null)
            {
                pos.x = pos_go.transform.localPosition.x;
                pos.y = pos_go.transform.localPosition.y + 10f;
            }
            else
            {
                pos.x = m_BuffOwner.BattleCardObj.transform.localPosition.x;
                pos.y = m_BuffOwner.BattleCardObj.transform.localPosition.y + 10f;
            }
            if (m_BuffValue < 0)
            {
				hitText = m_BuffOwner.CreateHitText(BattleUI.Instacne.redText,pos,Vector3.one);
                hitText.text = "" + m_BuffValue;
            }
            else if (m_BuffValue > 0)
            {
				hitText = m_BuffOwner.CreateHitText(BattleUI.Instacne.greenText,pos,Vector3.one);
                hitText.text = "+" + m_BuffValue;
            }
            hitText.Commit();
            hitText.animation.Play();
        }

        private void InitPosition()
        {
            if (m_TabBfEffect.EffectPos == (int)BuffPostionType.E_BUFF_POSTION_TYPE_CENTER)
            {
                m_BuffObj.transform.parent = m_BuffOwner.CardUI.cardMainRoot.transform;
                m_BuffObj.transform.localPosition = new Vector3(0.0f, 0.0f, -19.0f);
                if (m_TabBfEffect.EffectType == (int)BuffEffectType.E_BUFF_EFFECT_TYPE_PARTICLE)
                {
                    m_BuffObj.transform.localScale = new Vector3(512.0f, 512.0f, 1.0f);
                }
                else
                {
                    if (m_TabBfEffect.BuffType == (int)BattleBuffType.E_BATTLE_BUFF_TYPE_KUANG_BAO)
                    {
                        m_BuffObj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f)/0.63f;
                    }
                    else
                    {
                        m_BuffObj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    }
                }
            }
            else
            {
                m_BuffObj.transform.parent = m_BuffOwner.CardUI.buffIconRoot;
                m_BuffObj.transform.localPosition = new Vector3(0f, 0f, 100f);//把buff隐藏
                m_BuffObj.transform.localScale = Vector3.one;

                List<BuffBase> temp_buffs = new List<BuffBase>();
                foreach (BuffBase buff_base in m_BuffOwner.BuffList)
                {
                    if (m_TabBfEffect.EffectPos == (int)BuffPostionType.E_BUFF_POSTION_TYPE_LEFT)
                    {
                        temp_buffs.Add(buff_base);
                    }
                }

                //把BUFF添加到BUFF显示位的后面-
                if (temp_buffs.Count < m_BuffOwner.BuffPosArray.Length)
                {
                    m_BuffObj.transform.localPosition = m_BuffOwner.BuffPosArray[temp_buffs.Count].transform.localPosition;
                }
            }
        }

        public override void Destroy()
        {
            Debug.Log("BuffGeneric.Destroy()");

            if (m_BuffAnimLib != null)
            {
                m_BuffAnimLib = null;
            }
            if (m_BuffSprite != null)
            {
                m_BuffSprite.Stop();
                GameObject.Destroy(m_BuffSprite.gameObject);
                m_BuffSprite = null;
            }

            if (m_BuffObj != null)
            {
                GameObject.Destroy(m_BuffObj);
            }
        }
    }
}
