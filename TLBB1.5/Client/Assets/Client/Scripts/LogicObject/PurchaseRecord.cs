using System.Collections;
using xjgame.message;
using Games.LogicObject;

public class PurchaseRecord
{
	public string goodsId;// 商品id
	public string date;//充值时间
	public float goodsPrice;//充值金额
	public string channel;//充值渠道
	public int goodState;//订单状态
	
	public PurchaseRecord ()
	{
	}
}

