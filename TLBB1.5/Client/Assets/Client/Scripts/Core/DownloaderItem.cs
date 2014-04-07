using UnityEngine;
using System.Collections;


public class DownloaderItem{
	public int id = -1;
	public string name = "-1";
	public int version = -1;
	public string md5 = "-1";
	public string type = "-1";
	
	public DownloaderItem(string param){
		string[] values = param.Split('\t');
		if(values.Length != 5){
			return ;
		}else{
			if(!int.TryParse(values[0],out id))
				return ;
			name = values[1];
			if(!int.TryParse(values[2],out version))
				return ;
			md5 = values[3];
			type = values[4];
			
		}
	}
	
	public DownloaderItem(int _id,string _name,int _version,string _md5,string _type){
		this.id = _id;
		this.name = _name;
		this.version = _version;
		this.md5 = _md5;
		this.type = _type;
	}
	
	public  override  string ToString(){
		return id.ToString()+'\t'+name+'\t'+version.ToString()+'\t'+md5+'\t'+type;
	}
	
	private DownloaderItem(){}
}	