using System;
////using Module.Log;
using UnityEngine; 
using System.Net.NetworkInformation;

#if UNITY_ANDROID
using System.Text.RegularExpressions;
#endif

	public class Utils
	{
		public const string UI_NAME_Logo = "LogoScene";
		public const string UI_NAME_Login = "LoginScene";
		public const string UI_NAME_main = "MainUI";
		public const string UI_NAME_Battle = "BattleUI";
		public const string UI_NAME_LOADING = "loading";
		
		public Utils ()
		{
			
		}
		
		public static void ShowTip(string _tip) {
			Debug.Log(_tip);
			//TipUILogic.tipUILogic.ShowTip(_tip, "confirm");
		}
		
		public static string TestMacAddress() {
			NetworkInterface[] ni = NetworkInterface.GetAllNetworkInterfaces();
			if (ni.Length > 0 )
			{
				foreach (NetworkInterface curNI in ni)
				{
					if (curNI.GetPhysicalAddress().ToString().Length > 0 )
					{
						return curNI.GetPhysicalAddress().ToString();
					}
				}
			}
			Debug.LogError("Can not find Mac addr");
			return null;
		}
		
#if UNITY_ANDROID
        public static void EraseColorStrSpace(ref string rString)
        {
            Regex regex = new Regex("\\[.*\\]",RegexOptions.None);
            //Regex regex = new Regex("%d",RegexOptions.None);
            if(regex.IsMatch(rString))
            {
                Match tmpMatch = regex.Match(rString);
                while(tmpMatch.Success == true)
                {
                    rString = regex.Replace(rString, new MatchEvaluator(ReplaceString),1);
                    tmpMatch = tmpMatch.NextMatch();
                }
            }
        }

        static string ReplaceString(Match match)
        {
            return match.Value.Replace(" ","");
        }
#endif		
	}


