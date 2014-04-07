using System.Collections;
using card.net;
using xjgame.message;

public class ScrollData
{
    public float scrollValue = 0;//scrollbar位置
    public int page = 0;//页数

    public ScrollData()
    { 
    }
    public ScrollData(float scValue,int paValue)
    {
        scrollValue = scValue;
        page = paValue;
    }
    public ScrollData(float scValue)
    {
        scrollValue = scValue;
        page = 0;
    }
}
