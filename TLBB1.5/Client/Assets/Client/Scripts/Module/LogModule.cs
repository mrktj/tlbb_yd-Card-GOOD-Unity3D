using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;


namespace Module.Log
{
   
	class LogModule
	{
        const string ERRORLOG = "./error_{0}.log";
        const string DEBUGLOG = "./debug_{0}.log";
        const string WARNINGLOG = "./warning_{0}.log";

        enum LOG_TYPE
        {
            DEGUG_LOG =0,
            WARNING_LOG,
            ERROR_LOG
        }
        //private static byte deleteOtherLog ()
        //{
        //    string todays = string.Format("{0:yyyy_MM_dd}", System.DateTime.Now);
        //    string logfiledebug = string.Format(DEBUGLOG,todays);
        //     string logfilewarning = string.Format(WARNINGLOG,todays);
        //     string logfiledError = string.Format(ERRORLOG,todays);
        //    string[] fileList = Directory.GetFiles("./");
        //    if (fileList.Length>0)
        //    {
        //        foreach (string file in fileList)
        //        {

        //            if (file != logfiledebug || file != logfilewarning || file != logfiledError)
        //            {
        //                File.Delete(file);
        //            }

        //            Debug.Log(file);
                    
        //        }
        //    }
        //    else
        //    {
        //        Debug.Log("not any file");
        //    }
        //    return 1;
        //}
        ////Help 
        //static byte deleteother = deleteOtherLog();
		
		public delegate void OnOutputLog(string _msg); 
		static public OnOutputLog onOutputLog = null;
		
#if UNITY_IPHONE
		//private static void WriteLog (string msg ,LOG_TYPE ID){}
        private static void WriteLog (string msg ,LOG_TYPE ID, bool _showInConsole = false){}
#else
		private static void WriteLog (string msg ,LOG_TYPE ID)
		{
			WriteLog(msg,ID,true);
		}
        private static void WriteLog (string msg ,LOG_TYPE ID, bool _showInConsole)
        {
            string result = string.Format("{0} T0={1:yyyy-MM-dd HH:mm:ss}\n", msg, System.DateTime.Now);
            string todays = string.Format("{0:yyyy_MM_dd}",System.DateTime.Now);
            string logfile = "" ;
            
           

            if (ID == LOG_TYPE.DEGUG_LOG)
            {
           		if(_showInConsole)
				{
					Debug.Log(result);
				}
                logfile = string.Format(DEBUGLOG,todays);
            }
            else if (ID == LOG_TYPE.WARNING_LOG)
            {
				if(_showInConsole)
				{
					Debug.LogWarning(result);
				}
                logfile = string.Format(WARNINGLOG, todays);
            }
            else if (ID == LOG_TYPE.ERROR_LOG)
            {
				if(_showInConsole)
				{
					Debug.LogError(result);
				}
                logfile = string.Format(ERRORLOG, todays);
            }
			if (onOutputLog != null)
			{
				onOutputLog(result);
			}
#if UNITY_ANDROID
            return;
#else
            if (logfile.Length>0)
            {
                //Need write file
                try
                {
                    FileStream _fs = new FileStream(logfile, FileMode.OpenOrCreate | FileMode.Append, FileAccess.Write);
                    byte[] data = new UTF8Encoding().GetBytes(result);
                    _fs.Write(data, 0, data.Length);
                    _fs.Flush();
                    _fs.Close();
                }
                catch (System.Exception ex)
                {
                    Debug.LogError(ex.ToString());
                }
            }
#endif
        }
#endif
       
		public static void ErrorLog(string fort,params object[] areges)
		{
			if (areges.Length>0)
			{
				string msg = string.Format(fort, areges);
				WriteLog(msg, LOG_TYPE.ERROR_LOG, true);
			}
			else
			{
				WriteLog(fort, LOG_TYPE.ERROR_LOG, true);
			}
			
		}
		public static void WarningLog(string fort, params object[] areges)
		{
			if (areges.Length > 0)
			{
				string msg = string.Format(fort, areges);
				WriteLog(msg, LOG_TYPE.WARNING_LOG, true);
			}
			else
			{
				WriteLog(fort, LOG_TYPE.WARNING_LOG, true);
			}
		}
		public static void DebugLog(string fort, params object[] areges)
		{
			if (areges.Length > 0)
			{
				string msg = string.Format(fort, areges);
				WriteLog(msg, LOG_TYPE.DEGUG_LOG, true);
			}
			else
			{
				WriteLog(fort, LOG_TYPE.DEGUG_LOG, true);
			}
		}
		
		private static void ErrorLog(string msg)
		{
			WriteLog(msg, LOG_TYPE.ERROR_LOG);
		}
		
		private static void WarningLog(string msg)
		{
			WriteLog(msg, LOG_TYPE.WARNING_LOG);
		}
		
		public static void DebugLog(string msg)
		{
			WriteLog(msg, LOG_TYPE.DEGUG_LOG);
		}
		
      	public static void Log(string logString, string stackTrace, LogType type)
		{
			switch (type)
			{
			case LogType.Log:
				Debug.Log(logString);
				break;
			case LogType.Warning:
				LogModule.WarningLog(logString);
				break;
			case LogType.Error:
				LogModule.ErrorLog(logString);
				break;
			}
		}
        
	}
}
