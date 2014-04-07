using UnityEngine;
using System.Collections;

public class ServerDetail  {
	
	public enum ServerState
	{
		Closed = 0,
		Open, //新服
		New, //流畅
		Busy, //爆满
		Unopened, //维护
	};
	
	public int index;
	public string name;
	public string address;
	public ServerState state;//1-新服 2-流畅 3-爆满
	
	public ServerDetail(){}
	
//	public ServerDetail(int index,string name,string address,int state)
//	{
//		this.index = index;
//		this.name = name;
//		this.address = address;
//		this.state = state;
//	}
	public ServerDetail(string[] strcol)
	{
		this.index = int.Parse(strcol[0]);
		this.name = strcol[1];
		this.address = strcol[2];
		this.state = (ServerState)int.Parse(strcol[3]);
		Debug.Log("index:"+index+";+name:"+name+";address:"+address+";state:"+(int)state);
	}
}
