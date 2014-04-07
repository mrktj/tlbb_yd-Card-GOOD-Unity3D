using System;
using System.Collections.Generic;
using GCGame.Table;

namespace Games.LogicObject
{
	public class LanguageManger
	{	
		private LanguageType mLangType;
		public void SetLangType(LanguageType type)
		{
			mLangType = type;
		}
		public LanguageType GetLangType() {return mLangType;}
		
		public LanguageManger()
		{
			mLangType = LanguageType.LANGUAGE_CHINESE;
		}
		
		private static LanguageManger mInstance = null;
		public static LanguageManger GetMe()
		{
			if(mInstance == null)
			{
				mInstance = new LanguageManger();
			}
			return mInstance;
		}
		public static string GetWords(Int32 id)
		{
			string words = "";
			if(id>0)
			{
				LanguageType type = LanguageManger.GetMe().GetLangType();
				Tab_Language lang = TableManager.GetLanguageByID(id);
				if(lang == null)
				{
					return "";
				}
				
				switch(type)
				{
				case LanguageType.LANGUAGE_CHINESE:
					words = lang.Chinese;
					break;
				case LanguageType.LANGUAGE_ENGLISH:
					words = lang.English;
					break;
				default:
					break;
				}
			}
			return words;
		}
	}
}
