using UnityEngine;
using System.Collections;
using GCGame.Table;

namespace Games.Battle
{
    public enum BattleBuffType
    {
        E_BATTLE_BUFF_TYPE_XUAN_YUN     = 0,//眩晕
        E_BATTLE_BUFF_TYPE_XI_XIE       = 1,//吸血
        E_BATTLE_BUFF_TYPE_JIA_XIE      = 2,//加血
        E_BATTLE_BUFF_TYPE_LIU_XIE      = 3,//中毒

        E_BATTLE_BUFF_TYPE_GONG_JI_ADD = 4, //增加攻击
        E_BATTLE_BUFF_TYPE_GONG_JI_DEC = 5, //降低攻击
        E_BATTLE_BUFF_TYPE_WU_FANG_ADD = 6, //增加物防
        E_BATTLE_BUFF_TYPE_WU_FANG_DEC = 7, //降低物防
        E_BATTLE_BUFF_TYPE_MIN_ZHONG_ADD = 8, //增加命中
        E_BATTLE_BUFF_TYPE_MIN_ZHONG_DEC = 9, //降低命中
        E_BATTLE_BUFF_TYPE_SHAN_BI_ADD = 10,//增加闪避
        E_BATTLE_BUFF_TYPE_SHAN_BI_DEC = 11,//降低闪避
        E_BATTLE_BUFF_TYPE_BAO_JI_ADD = 12,//增加暴击
        E_BATTLE_BUFF_TYPE_BAO_JI_DEC = 13,//降低暴击
        E_BATTLE_BUFF_TYPE_REN_XIN_ADD = 14,//增加韧性
        E_BATTLE_BUFF_TYPE_REN_XIN_DEC = 15,//降低韧性

        E_BATTLE_BUFF_TYPE_KUANG_BAO    = 16,//狂暴
        E_BATTLE_BUFF_TYPE_SHAN_BI      = 17,//闪避，无资源
        E_BATTLE_BUFF_TYPE_LIAN_JI      = 18,//连击，无资源
        E_BATTLE_BUFF_TYPE_FAN_SHANG    = 19,//反伤
        E_BATTLE_BUFF_TYPE_DZXY         = 20,//斗转星移
        E_BATTLE_BUFF_TYPE_HU_DUN       = 21,//护盾
        E_BATTLE_BUFF_TYPE_MIAN_SHANG   = 22,//免伤，无
        E_BATTLE_BUFF_TYPE_YIN_SHEN     = 23,//隐身，无
        E_BATTLE_BUFF_TYPE_FAN_YIN      = 23,//反隐身，不需要
    }

    public enum BuffEffectType
    {
        E_BUFF_EFFECT_TYPE_SEQUENCE,
        E_BUFF_EFFECT_TYPE_PARTICLE,
    }

    public enum BuffPostionType
    {
        E_BUFF_POSTION_TYPE_CENTER,
        E_BUFF_POSTION_TYPE_LEFT,
    }

    public class BuffBase
    {
        protected int m_BuffId = -1;
        public int BuffId { get { return m_BuffId; } }

        protected int m_BuffValue = 0;
        public int BuffValue { get { return m_BuffValue; } set { m_BuffValue = value; } }

        protected BattleCard m_BuffOwner = null;

        protected GameObject m_BuffObj = null;
        public GameObject BuffObj { get { return m_BuffObj; } }

        protected Tab_Buff m_TabBuff = null;
        public Tab_Buff TabBuff { get { return m_TabBuff; } }

        protected Tab_BuffEffect m_TabBfEffect = null;
        public Tab_BuffEffect TabBfEffect { get { return m_TabBfEffect; } }

        public virtual bool Init() { return false; }

        public virtual bool LoadBuff() { return false; }

        public virtual void UseBuff() { }

        public virtual void ShowBuffValue(GameObject pos_go = null) { }

        public virtual void SetPostion(GameObject point) { }

        public virtual void Destroy() { }
    }
}

