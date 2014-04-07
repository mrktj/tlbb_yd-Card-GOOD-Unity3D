using UnityEngine;
using System;
using System.Collections.Generic;
using GCGame.Table;
using Games.CharacterLogic;

namespace Games.LogicObject
{
    public class UserCardItem
    {
        public long cardID;//卡牌GUID
        public Int32 templateID;//卡牌模板ID
        public Int32 level;
        public Int32 fightIndex;//出战(队伍)顺序 -1 = 不出站 0-4出战
        public Int32 qxzbFightIndex; //群雄争霸队伍 百位为队长 十位为星级 个位为阵型站位
		public Int32 quality;   // 质量，星级
        public Int32 skillLevel;//主动技能等级
        public Int32 addQualityAtt; //攻击强化次数
        public Int32 addQualityHp;	//血量强化次数
        public Int32 cardExp;//卡牌当前等级的经验
        public Int32 memberID;//卡牌的memberID,0为队长
        public Int32 skillStudyId;//学习技能ID
        public Int32 skillStudyLev;//学习技能等级
        public Int32 skillStudyExp;//学习技能当前经验

        public UserCardItem()
        {
            cardID = -1;
            templateID = -1;
            level = 0;
            fightIndex = -1;
            quality = 0;
            skillLevel = 0;
            addQualityAtt = 0;
            addQualityHp = 0;
            cardExp = 0;
            memberID = -1;
        }
        public bool isProtected;
        public static string[] cardFrameName = {
            "kapai_bai",	//0
			"kapai_bai",	//1
            "kapai_lv",		//2
            "kapai_lan",	//3
            "kapai_zi",	//4
            "kapai_cheng",		//5
            "kapai_hong",		//6
            "kapai_jin",	//7
        };
        public static string[] elementFrameName = {
         	"shuxing_kuang",	//0
			"shuxing_kuang",	//1
            "shuxing_kuang",		//2
            "shuxing_kuang",		//3
            "shuxing_kuang",	//4
            "shuxing_kuang",		//5
            "shuxing_kuang",		//6
            "shuxing_kuang",	//7
        };
        public static string[] elementTypeName = new string[]{
			"jin",
			"mu",
			"shui",
			"huo",
			"tu"
		};
        public static string[] iconFrameName = {
            "xiaotouxiang",	//0
			"xiaotouxiang",	//1
            "kapai_lv_150",		//2
            "xiaotouxiang",	//3
            "xiaotouxiang",	//4
            "kapai_zi_150",		//5
            "kapai_zi_150",		//6
            "kapai_cheng_150",	//7
        };

        public static string[] spriteFrameName = {
			"zhujiemian_wodetuandui_beijing_bai", //0
			"zhujiemian_wodetuandui_beijing_bai",	//1
			"zhujiemian_wodetuandui_b-lv",	//2
            "zhujiemian_wodetuandui_b-lan",		//3
            "zhujiemian_wodetuandui_b-zi",	//4
            "zhujiemian_wodetuandui_b-cheng",	//5
            "zhujiemian_wodetuandui_b-hong",		//6
            "zhujiemian_wodetuandui_b-jin",		//7
		};

        public static int[] startFactor = {
			0,
			5,
			10,
			15,
			25,
			45,
			70,
			100
		};
        public static string[] littleCardFrameName = {
			"kapai_xiao_bai",//0
			"kapai_xiao_bai",
			"kapai_xiao_lv",
			"kapai_xiao_lan",
			"kapai_xiao_zi",
			"kapai_xiao_cheng",
			"kapai_xiao_hong",
			"kapai_xiao_huang",
		};
        public static string[] littleCardBorderName = {
			"kapai_waikuang_xiao_bai",//0
			"kapai_waikuang_xiao_bai",
			"kapai_waikuang_xiao_lv",
			"kapai_waikuang_xiao_lan",
			"kapai_waikuang_xiao_zi",
			"kapai_waikuang_xiao_cheng",
			"kapai_waikuang_xiao_hong",
			"kapai_waikuang_xiao_huang",
		};


        public static string[] largeCardName = {
			"kapai_bai", //0
			"kapai_bai",
			"kapai_lv",
			"kapai_lan",
			"kapai_zi",
			"kapai_cheng",
			"kapai_hong",
			"kapai_jin",
		};

        public static string[] largeCardBorderName = {
			"kapai_waikuang_bai", //0
			"kapai_waikuang_bai",
			"kapai_waikuang_lv",
			"kapai_waikuang_lan",
			"kapai_waikuang_zi",
			"kapai_waikuang_cheng",
			"kapai_waikuang_hong",
			"kapai_waikuang_jin",
		};

        public static string[] largeCardNameBg = {
			"kapai_nameBg_bai", //0
			"kapai_nameBg_bai",
			"kapai_nameBg_lv",
			"kapai_nameBg_lan",
			"kapai_nameBg_zi",
			"kapai_nameBg_cheng",
			"kapai_nameBg_hong",
			"kapai_nameBg_jin",
		};

        public enum CardType
        {
            NORMAL_CARD = 0,
            EXP_CARD,
            EVOLUTION_CARD,
            UNDEFINED,
        }

        //         public int CompareTo(UserCardItem card)
        //         {
        //             //排序顺序按照 先星级 再等级，最后ID的方式进行
        //             if (card.quality != this.quality)
        //             {
        //                 return card.quality.CompareTo(this.quality);
        //             }
        //             else if (card.level != this.level)
        //             {
        //                 return card.level.CompareTo(this.level);
        //             }
        //             else
        //             {
        //                 return (-1 * card.templateID.CompareTo(this.templateID));
        //             }
        // 
        //         }
        //		     "ui_headframe_white",
        //            "ui_headframe_green",
        //            "ui_headframe_greenplus",
        //            "ui_headframe_blue",
        //            "ui_headframe_blueplus",
        //            "ui_headframe_purple",
        //            "ui_headframe_purpleplus",
        //            "ui_headframe_gold",
        //            "ui_headframe_goldplus",

        public static string CardFrameByTemplateID(int id)
        {
            //            int quality = TableManager.GetCardByID(id).Quality;
            //            if (quality >= 0 && quality < 9)
            //            {
            //                return cardFrameName[quality];
            //            }

            return null;
        }

        public CardType cardType
        {
            get
            {
                CardType type = CardType.UNDEFINED;
                switch (TableManager.GetCardByID(templateID).CardType)
                {
                    case 0: type = CardType.NORMAL_CARD; break;
                    case 1: type = CardType.EXP_CARD; break;
                    case 2: type = CardType.EVOLUTION_CARD; break;
                    case 3: type = CardType.UNDEFINED; break;
                }
                return type;
            }
        }

        //获得总血量
        public Int32 GetHp()
        {
            Int32 hp = GetHpBase();
            hp += this.GetHpAdd();
			hp += GetStudySkillHp();
            hp += GetFengShuiHp();
            return hp;
        }

        //获得基本血量(没有强化的值在里面)(但是有风水加成在里面）
        public Int32 GetHpBase()
        {
            Int32 hp = TableManager.GetCardByID(templateID).HpBase;
            int nLevBase = TableManager.GetCardByID(templateID).LevelBase;
            hp += TableManager.GetCardByID(templateID).HpGrow * (level - nLevBase);
            return hp;
        }

        public Int32 GetStudySkillHp()
        { 
            Tab_Skill tab_skill=TableManager.GetSkillByID(skillStudyId);
            if (tab_skill!=null)
            {
                if (tab_skill.LogicId == 13)
                {
                    return tab_skill.DamageValue;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public Int32 GetStudySkillAttack()
        {
            Tab_Skill tab_skill = TableManager.GetSkillByID(skillStudyId);
            if (tab_skill != null)
            {
                if (tab_skill.LogicId == 10)
                {
                    return tab_skill.DamageValue;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public Int32 GetFengShuiHp()
        {
            Int32 hp = 0;
            hp += (int)(GetHpBase() * FengShuiData.Instance().fengshuiAdd[this.GetAttributeID(), 2] * 0.01) / 1;
            hp += (int)FengShuiData.Instance().fengshuiAdd[this.GetAttributeID(), 1];
            return hp;
        }

        public Int32 GetFengShuiAttc()
        {
            Int32 attc = 0;
            attc += (int)FengShuiData.Instance().fengshuiAdd[this.GetAttributeID(), 0];
            return attc;
        }

        //获得加强的血量值
        public Int32 GetHpAdd()
        {
            //血量道具 10001 （每次强化增加的血量值）
            int nQualityHp = TableManager.GetItemByID(10001).Value;
            return addQualityHp * nQualityHp;
        }


        //获得总的攻击力
        public Int32 GetAttack()
        {
            Int32 att = this.GetAttackBase();
            att += this.GetAttackAdd();
            att += this.GetFengShuiAttc();
            return att;
        }

        //获得基本的攻击力
        public Int32 GetAttackBase()
        {
            Int32 att = TableManager.GetCardByID(templateID).AttackBase;
            int nLevBase = TableManager.GetCardByID(templateID).LevelBase;
            att += TableManager.GetCardByID(templateID).AttackGrow * (level - nLevBase);
            att += GetStudySkillAttack();
            return att;
        }

        //获得加强的攻击值
        public Int32 GetAttackAdd()
        {
            //攻击道具 10000 （每次强化增加的血量值）
            int nQualityAtt = TableManager.GetItemByID(10000).Value;
            return addQualityAtt * nQualityAtt;
        }


        //获得卡牌可卖的价钱
        public Int32 GetMoneyValue()
        {
            return level * TableManager.GetCardByID(templateID).SellBase;
        }

        //卡牌升级花费的钱
        public Int32 GetUpDateCostMoney(List<UserCardItem> materialCardItems)
        {
            int nMoney = 0;
            int nHpMaterialCardTotal = 0;
            int nAttackMaterialCardTotal = 0;

            foreach (var materialCard in materialCardItems)
            {
                if (materialCard != null)
                {
                    nHpMaterialCardTotal += materialCard.addQualityHp;
                    nAttackMaterialCardTotal += materialCard.addQualityAtt;
                }
            }

            nMoney += this.level * 100 * materialCardItems.Count;


            //算血量的钱
            int nHpLevelTotal = this.addQualityHp + nHpMaterialCardTotal;
            nHpLevelTotal = nHpLevelTotal > 99 ? 99 : nHpLevelTotal;
            for (int iLevel = this.addQualityHp; iLevel < nHpLevelTotal; iLevel++)
            {
                nMoney += (iLevel + 1) * 6000;
            }

            int nAttackLevelTotal = this.addQualityAtt + nAttackMaterialCardTotal;
            nAttackLevelTotal = nAttackLevelTotal > 99 ? 99 : nAttackLevelTotal;
            for (int iAttLevel = this.addQualityAtt; iAttLevel < nAttackLevelTotal; iAttLevel++)
            {
                nMoney += (iAttLevel + 1) * 6000;
            }
            /*
            //金钱
            int nMoney = 0;
            for (int i = 0; i < materialCardItems.Count; i++)
            {
                if (materialCardItems[i] != null)
                {
                    nMoney += this.GetUpdateCostByOneMaterial(materialCardItems[i]);
                }
            }
            */

            return nMoney;
        }

        //卡牌升级花费的钱
        public Int32 GetUpDateCostMoney(UserCardItem[] materialCardItems)
        {

            int nMoney = 0;
            int nHpMaterialCardTotal = 0;
            int nAttackMaterialCardTotal = 0;

            int nCardNum = 0;
            foreach (var materialCard in materialCardItems)
            {
                if (materialCard != null)
                {
                    nCardNum++;
                    nHpMaterialCardTotal += materialCard.addQualityHp;
                    nAttackMaterialCardTotal += materialCard.addQualityAtt;
                }
            }



            nMoney += this.level * 100 * nCardNum;


            //算血量的钱
            int nHpLevelTotal = this.addQualityHp + nHpMaterialCardTotal;
            nHpLevelTotal = nHpLevelTotal > 99 ? 99 : nHpLevelTotal;
            for (int iLevel = this.addQualityHp; iLevel < nHpLevelTotal; iLevel++)
            {
                nMoney += (iLevel + 1) * 6000;
            }

            int nAttackLevelTotal = this.addQualityAtt + nAttackMaterialCardTotal;
            nAttackLevelTotal = nAttackLevelTotal > 99 ? 99 : nAttackLevelTotal;
            for (int iAttLevel = this.addQualityAtt; iAttLevel < nAttackLevelTotal; iAttLevel++)
            {
                nMoney += (iAttLevel + 1) * 6000;
            }

            /*
            //材料总的强化等级(攻击+血量 的总强化等级)
            int nQualityMater = card.addQualityAtt + card.addQualityHp;
            //此卡牌的总强化等级
            int nQualityHero = this.addQualityAtt + this.addQualityHp;


            //材料的总强化等级为0 直接 此卡牌的等级*100
            if (nQualityMater == 0)
            {
                nMoney += this.level * 100;
            }
            else if (card.addQualityAtt == 0)
            {
                nMoney += (this.level * 100 + (card.addQualityHp + this.addQualityHp) * 10000);
            }
            else if (card.addQualityHp == 0)
            {
                nMoney += (this.level * 100 + (card.addQualityAtt + this.addQualityAtt) * 10000);
            }
            else
            {
                nMoney += (this.level * 100 + (nQualityMater + nQualityHero) * 10000);
            }

            return nMoney;
            //金钱
            int nMoney = 0;
            for (int i = 0; i < materialCardItems.Length; i++)
            {
                if (materialCardItems[i] != null)
                {
                    nMoney += this.GetUpdateCostByOneMaterial(materialCardItems[i]);
                }
            }
            */

            return nMoney;
        }

        //卡牌强化攻击力所要花费的钱
        public Int32 GetStrengthAttackCostMoney()
        {
            return (this.addQualityAtt + 1) * 6000;
        }

        //卡牌强化血所要花费的钱
        public Int32 GetStrengthHpCostMoney()
        {
            return (this.addQualityHp + 1) * 6000;
        }


        //升级卡牌，每消耗一个材料花的钱
        private Int32 GetUpdateCostByOneMaterial(UserCardItem card)
        {

            int nMoney = 0;
            //材料总的强化等级(攻击+血量 的总强化等级)
            int nQualityMater = card.addQualityAtt + card.addQualityHp;
            //此卡牌的总强化等级
            int nQualityHero = this.addQualityAtt + this.addQualityHp;


            //材料的总强化等级为0 直接 此卡牌的等级*100
            if (nQualityMater == 0)
            {
                nMoney += this.level * 100;
            }
            else if (card.addQualityAtt == 0)
            {
                nMoney += (this.level * 100 + (card.addQualityHp + this.addQualityHp) * 10000);
            }
            else if (card.addQualityHp == 0)
            {
                nMoney += (this.level * 100 + (card.addQualityAtt + this.addQualityAtt) * 10000);
            }
            else
            {
                nMoney += (this.level * 100 + (nQualityMater + nQualityHero) * 10000);
            }

            return nMoney;
        }

        public Int32 GetTotalExp()
        {
            return TableManager.GetCardByID(templateID).ExpBase;
        }
        public bool IsFullLevel()
        {
            bool ret = true;
            if (level < TableManager.GetCardByID(templateID).MaxLevel)
            {
                ret = false;
            }
            return ret;
        }

        //获得最大星级
        public int GetMaxquality()
        {
            return TableManager.GetCardByID(templateID).HighStarDisplay;
        }

        //当卡牌为材料卡牌时，能提供的经验
        public int GetProvidedExp()
        {
            return TableManager.GetCardByID(templateID).ExpBase * level;
        }

        //是否上场
        public bool IsInFightArray()
        {
            bool isMember = false;
            for (int i = 0; i < Obj_MyselfPlayer.GetMe().teamMemberArray.Length; i++)
            {
                if (Obj_MyselfPlayer.GetMe().teamMemberArray[i] == cardID)
                {
                    isMember = true;
                }
            }

            return isMember;
        }
		
		
		//是否在群雄争霸中上场
		public bool IsInQxzbFightArray()
		{
			if(qxzbFightIndex > 0)
			{
				return true;
			}
			
			return false;
		}

        //判断强化是否满级了
        public bool IsStrengthMax()
        {
            bool bStrengthMax = false;
            if (addQualityAtt >= 99 && addQualityHp >= 99)
            {
                bStrengthMax = true;
            }
            else
            {
                bStrengthMax = false;
            }

            return bStrengthMax;
        }


        //判断卡牌是否是专门被吞噬用的经验卡牌
        public bool IsKindOfExpCard()
        {
            //经验卡牌：黑棋子ID1388，白棋子ID1389，金棋子ID1390
            if (templateID == 1388
                   || templateID == 1389
                      || templateID == 1390)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //获得五行属性ID
        public int GetAttributeID()
        {
            return TableManager.GetCardByID(templateID).Element;
        }

        //获得五行属性IconName
        public string GetAttributeIconName()
        {
            //             string strIconName = "";
            //             switch(this.GetAttributeID())
            //             {
            //                 case 0: strIconName = elementTypeName[0]; break;
            //                 case 1: strIconName = elementTypeName[]; break;
            //                 case 2: strIconName = "shui"; break;
            //                 case 3: strIconName = "huo"; break;
            //                 case 4: strIconName = "tu"; break;
            //             }
            return elementTypeName[this.GetAttributeID()];
        }

        /// <summary>
        /// 获取总领导力
        /// </summary>
        /// <returns></returns>
        public int GetLeaderShip()
        {
            Tab_Studyskill studySkill=TableManager.GetStudyskillByID(skillStudyId);
            if (studySkill != null)
            {
                return GetLeaderShipBase() + studySkill.LeaderNum;
            }
            else
            {
                return GetLeaderShipBase();
            }
        }

        /// <summary>
        /// 获取卡牌基础领导力
        /// </summary>
        /// <returns></returns>
        public int GetLeaderShipBase()
        {
            return TableManager.GetCardByID(templateID).LeaderBase;
        }

        //        public static int CardApByLevel(Tab_Card card, int lv)
        //        {
        //            return card.AttackBase + lv * card.AttackGrow;
        //        }
        //
        //        public static int CardDpByLevel(Tab_Card card, int lv)
        //        {
        //            return card.DefenceBase + lv * card.DefenceGrow;
        //        }
        //
        //        public static int CardHpByLevel(Tab_Card card, int lv)
        //        {
        //            return card.HpBase + lv * card.HpGrow;
        //        }

    }
}
