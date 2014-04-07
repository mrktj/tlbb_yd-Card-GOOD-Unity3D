 
namespace card.net
{
	public enum HttpErrorID
	{
		PLAYER_ACCOUNT_EXCEPTION = 25,  // 账号异常
		VERSION_ERROR = 27,
		PLAYER_ID_ERROR = 28,  // 请设置有效ID
		/**
	 * 出战index越界 应该设置为0-5；
	 */
		ERROR_CODE_GO_FIGHT_INDEX_ERROR = 63,
		/**
	 * 出战cardId错误；
	 */
		ERROR_CODE_GO_FIGHT_CARDID_ERROR = 64,
		/**
	 * 战斗battleId错误；
	 */
		ERROR_BATTLE_ID_ERROR = 65,
		/**
	 * 战斗 错误 超过副本次数限制；
	 */
		ERROR_BATTLE_LIMIT_TIMES_ERROR = 65,
		/**
	 * 体力不足；
	 */
		ERROR_PLAYER_LIMIT_POWER_ERROR = 66,
		/**
	 * 当前经验下不需要升级；
	 */
		ERROR_PLAYER_UPGREAD_LEVEL_ERROR = 67,
	}
}

