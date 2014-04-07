using System;

public enum LayerDefine
{
    USERLAYER_UI = 8,
    USERLAYER_EFFECT = 9,
}

public enum LanguageType
{
	LANGUAGE_CHINESE = 0,
	LANGUAGE_ENGLISH = 1,
}

public enum CardIconType
{
	ICON_HEAD = 0,
	ICON_BODY = 1,
}

// public enum TroopType
// {
// 	NORMAL = 0,
// 	BIG_FRONT = 1,
// 	BIG_BACK = 2,
// }
// 
// public enum BattleCamp
// {
// 	SELF,
// 	OTHER
// }
// public enum FightRole
// {
// 	ATTACKER,
// 	DEFENDER,
// }
// public enum SkillType
// {
// 	DIRECT,
// 	PROJECTILE,
// 	STRETCH,
// 	COMBINE,
// }
// public enum BuffType
// {
// 	DIZZY		= 0,//眩晕--
// 	ROB_BLOOD	= 1,//--吸血buff	--
// 	ADD_BLOOD	= 2,//--加血buff--
// 	POISONING	= 3,//--流血buff	--
// }

public enum CopyType
{
	NORMAL,
	ACTIVITY,
}
public enum CopyState
{
	UNOPEN,
	OPENED,
	PASSED,
}

public enum AttackType
{
	PHYSICAL,
	MAGIC,
}

// public enum DropType
// {
// 	MONEY  = 1,
// 	CARD,
// 	TIEM,
// }
public enum CultivateType
{
	UNKNOWN,
	UPDATE,
	EVOLUTION,
	STRENGTHEN,
}
public enum ItemType
{
	ATTACK	= 10000,
	HP		= 10001,
}

public enum LoginType
{
	TYPE_PPLOGIN = 3,
}

public enum ActivityType
{
    E_ACTIVITY_TYPE_GGL,
    E_ACTIVITY_TYPE_BAZHENTU,
    E_ACTIVITY_TYPE_MONTH_CARD,
    E_ACTIVITY_TYPE_WORLD_BOSS,
	E_ACTIVITY_TYPE_PATA,
	E_ACTIVITY_TYPE_CHANGE_CARD,
}

public enum SkillHeroType
{
    LEARN_MAIN,
    LEARN_CHILD,
    UPDATE_MAIN,
    UPDATE_CHILD,
}