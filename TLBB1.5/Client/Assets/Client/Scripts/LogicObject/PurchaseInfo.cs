using System.Collections;
using xjgame.message;
using Games.LogicObject;

public class PurchaseInfo
{
	public int goodsId;// 商品id
	public string productId;// pid 商品苹果商店id
	public string goodsIcon;// icon
	public int goodsNumber;//  游戏内代币数量
	public float goodsPrice;//   美元价值
	public string goodsName;// 
	public string goodDec;
	
	public PurchaseInfo ()
	{
	}
	
	public PurchaseInfo (ProductInfo msg)
	{
		goodsId=msg.GoodsId;
		productId=msg.ProductId;
		goodsIcon=msg.GoodsIcon;
		goodsNumber=msg.GoodsNumber;
		//string[] price=msg.GoodsPrice.Split('.');
		goodsPrice= float.Parse(msg.GoodsPrice);
		goodsName=msg.GoodsName;
		goodDec=msg.GoodDec;
	}
}

