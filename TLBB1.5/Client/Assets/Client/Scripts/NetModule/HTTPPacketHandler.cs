using System; 
using xjgame.message;
namespace card.net
{
	public interface HTTPPacketHandler
	{
		bool handle(MessageID opcode, byte[] data);  
		bool handleError(HttpErrorID errorId);
	}
}

