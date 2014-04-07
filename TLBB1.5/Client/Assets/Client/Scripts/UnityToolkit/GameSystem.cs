using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ClientConfigure
{
    public static string GetClientVersion()
    {
        return "1.2.0.9";
    }
	
	public static int VersionNumber{
		get{
			return 1209;
		}
	}
	
	public static string resourceTxtName{
		get{
			return "resources.txt";
		}
	}
	
	public const string title = "溫馨提示";
	
    public static string getUrl() {
		//return "10.6.97.48:8080";
//		return "10.6.97.6:8080";
		//return "10.6.97.40:8080";
		return "127.0.0.1:8080";
		//For demonstrate
		
	}
	
	public enum VersionStatic{
		Develop,
		Test,
		Release,
	} 
	
	public static VersionStatic versionStaic = VersionStatic.Test;
	
	public static string getResourceURL()
	{
		if(versionStaic == VersionStatic.Release){
		//release
		return "http://cdn.gotoplay.com/tlbb/main."+VersionNumber+".com.cyou.tlyd.android.tw.obb";
		}else if(versionStaic == VersionStatic.Test){
		//test
		//return "http://54.254.182.11:81/main."+VersionNumber+".com.cyou.tlyd.android.tw.obb";
			
		return "http://63.221.201.77:81/main."+VersionNumber+".com.cyou.tlyd.android.tw.obb";	
		}else if(versionStaic == VersionStatic.Develop){
		//dev
		return  "http://10.6.152.123:280/main."+VersionNumber+".com.cyou.tlyd.android.tw.obb";
		}else{
		//release
		return "http://cdn.gotoplay.com/tlbb/main."+VersionNumber+".com.cyou.tlyd.android.tw.obb";
		}
	}
	
	
	public static string getServersListURL()
	{
		if(versionStaic == VersionStatic.Release){
		//release
		return "http://download.gotoplay.com/tlbb/servers.txt";
		}else if(versionStaic == VersionStatic.Test){
		//test
		//return "http://54.254.182.11:81/servers.txt";
		
		return "http://63.221.201.77:81/servers.txt";	
		}else if(versionStaic == VersionStatic.Develop){
		//dev
		return "http://63.221.201.75:81/servers.txt";
		}else{
		//release
		return "http://download.gotoplay.com/tlbb/servers.txt";
		}
	}
	
	
	public static string getiOSServerLIstURL(){
		if(versionStaic == VersionStatic.Release){
		//release
		return "http://download.gotoplay.com/tlbb/ios/servers.txt";
		}else if(versionStaic == VersionStatic.Test){
		//test
		//return "http://54.254.230.167:81/servers.txt";
		
		return "http://63.221.201.77:81/servers.txt";	
		}else if(versionStaic == VersionStatic.Develop){
		//dev
		return "http://63.221.201.75:81/servers.txt";
		}else{
		//release
		return "http://download.gotoplay.com/tlbb/ios/servers.txt";
		}
		
	}
	
	public static string getOtherPurchaseUrl(){
		if(versionStaic == VersionStatic.Release){
		//release
		return "http://service.billing.gotoplay.com/webcharge/page3_pay2.jsp";
		}else if(versionStaic == VersionStatic.Test){
		//test
		return "http://service.billingtest.gotoplay.com/webcharge/page3_pay2.jsp";
		}else if(versionStaic == VersionStatic.Develop){
		
		//dev
		return "http://115.160.140.208:8083/service/page3_pay2.jsp";
		}else{
		//release
		return "http://service.billing.gotoplay.com/webcharge/page3_pay2.jsp";
		}
	}
	
	
	public static string getNoticeURL(){
		if(versionStaic == VersionStatic.Release){
		//release
		return "http://mobtlbb.gotoplay.com/notice/notice.html";			
		}else if(versionStaic == VersionStatic.Test){
		//test
		return "http://mobtlbb-test.gotoplay.com/notice/notice.html";
		}else{
		//release
		return "http://mobtlbb.gotoplay.com/notice/notice.html";
			
		}
	}
	
	public static string getIOSNoticeURL(){
		if(versionStaic == VersionStatic.Release){
		//release
		return "http://mobtlbb.gotoplay.com/ios/notice.html";			
		}else if(versionStaic == VersionStatic.Test){
		//test
		return "http://mobtlbb-test.gotoplay.com/ios/notice.html";
		}else{
		//release
		return "http://mobtlbb.gotoplay.com/ios/notice.html";
			
		}
	}
	
	public static string getHelpURL(){
		if(versionStaic == VersionStatic.Release){
		//release
		return "http://mobtlbb.gotoplay.com/help/help.html";	

		}else if(versionStaic == VersionStatic.Test){
			
		//test
		return "http://mobtlbb-test.gotoplay.com/help/help.html";
		}else{
		//release
		return "http://mobtlbb.gotoplay.com/help/help.html";	
			
		}
		
		
	}
	
	public static string getForgetPasswordURL(){
		if(versionStaic == VersionStatic.Release){
		//release
		return "http://mobtlbb.gotoplay.com/findpw/index.html";			

		}else if(versionStaic == VersionStatic.Test){
		//test
		return "http://mobtlbb-test.gotoplay.com/findpw/index.html";
		}else{
		//release
		return "http://mobtlbb.gotoplay.com/findpw/index.html";	
			
		}
	}
	
	public static string getUserAgreementURL(){
		if(versionStaic == VersionStatic.Release){
		//release
		return "http://mobtlbb.gotoplay.com/useragreement";

		}else if(versionStaic == VersionStatic.Test){
		//test
		return "http://mobtlbb-test.gotoplay.com/useragreement";
		}else{
		//release
		return "http://mobtlbb.gotoplay.com/useragreement";
			
		}
	}
	
	public static string getFacebookUrl(){
		return "https://www.facebook.com/mobtlbb";
	}
	
	public static string getGooglePlayUrl(){
		return "https://play.google.com/store/apps/details?id=com.cyou.tlyd.android.tw";
	}
	
	public static string getAppstoreUrl(){
		return "https://itunes.apple.com/us/app/tian-long-ba-bu-yi-dong-ban/id744943894?ls=1&mt=8";
	}
	
	public static bool useMrdCompress = false;
}

