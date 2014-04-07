using System;

namespace card
{
	public enum EventCode {
		LoginSucess = 1,
	}
	
	public class GameEvent
	{
		public GameEvent ()
		{
		}

		public static void fireEvent (object loginSucess, Object param)
		{
			throw new NotImplementedException ();
		}
	}
}

