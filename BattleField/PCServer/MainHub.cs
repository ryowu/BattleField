using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat
{
	/// <summary>
	/// Echoes messages sent using the Send message by calling the
	/// addMessage method on the client. Also reports to the console
	/// when clients connect and disconnect.
	/// </summary>
	public class MainHub : Hub
	{
		public void TurnEnd(int blockIndex, int tableId)
		{
			if (ServerCache.Tables.ContainsKey(tableId))
			{
				if (Context.ConnectionId == ServerCache.Tables[tableId].Player1.ConnectionId)
				{
					Clients.Client(ServerCache.Tables[tableId].Player2.ConnectionId).NotifyTurnEnd(blockIndex);
				}
				else
				{
					Clients.Client(ServerCache.Tables[tableId].Player1.ConnectionId).NotifyTurnEnd(blockIndex);
				}
			}
		}

		public void SignIn(string playerName, int tableId)
		{
			if (!ServerCache.Tables.ContainsKey(tableId))
				ServerCache.Tables.Add(tableId, new Table());

			Player currentPlayer = new Player() { Name = playerName, ConnectionId = Context.ConnectionId };

			if (ServerCache.Tables[tableId].Player1 == null)
			{
				ServerCache.Tables[tableId].Player1 = currentPlayer;
			}
			else if (ServerCache.Tables[tableId].Player2 == null)
			{
				ServerCache.Tables[tableId].Player2 = currentPlayer;
			}

			if (ServerCache.Tables[tableId].Player1 != null && ServerCache.Tables[tableId].Player2 != null)
			{
				//there are 2 players, send names to each other
				Clients.Client(ServerCache.Tables[tableId].Player1.ConnectionId).GameReady(true, ServerCache.Tables[tableId].Player2.Name);
				Clients.Client(ServerCache.Tables[tableId].Player2.ConnectionId).GameReady(false, ServerCache.Tables[tableId].Player1.Name);
			}
		}

		public override Task OnConnected()
		{
			Program.MainForm.WriteToConsole("Client connected: " + Context.ConnectionId);
			Task result = base.OnConnected();
			return result;
		}

		public override Task OnDisconnected()
		{
			Program.MainForm.WriteToConsole("Client disconnected: " + Context.ConnectionId);
			return base.OnDisconnected();
		}
	}
}
