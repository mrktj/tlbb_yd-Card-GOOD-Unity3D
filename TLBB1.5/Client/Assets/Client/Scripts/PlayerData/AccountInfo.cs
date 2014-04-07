using System;
using System.Collections.Generic;

namespace Games.CharacterLogic
{
    public class AccountInfo
    {
		public long accountId;
		public string email;
		public string password;
		public int listIndex;//本地登录顺序记录，先后依次为0~3，-1为当前值
		
		public AccountInfo()
		{
			accountId = -1;
			email = "";
			password = "";
			listIndex = -1;
		}
		public static string Base64Encode(string str)
		{
			byte[]  byte_array = System.Text.Encoding.Default.GetBytes(str);
			string encode = Convert.ToBase64String(byte_array);
			byte_array = System.Text.Encoding.Default.GetBytes(encode);
			encode = Convert.ToBase64String(byte_array);
			return encode;
		}
		
		public static string Base64Decode(string str)
		{
			byte[] byte_array = Convert.FromBase64String(str);
			string decode = System.Text.Encoding.Default.GetString(byte_array);
			byte_array = Convert.FromBase64String(decode);
			decode = System.Text.Encoding.Default.GetString(byte_array);
			return decode;
		}
    }
}