﻿namespace DotaApi.Model
{
	public class LobbyTypes
	{
		/// <summary>
		/// Converts the input int into a recognizable name
		/// -1 : Invalid
		/// 0 : Public Matching Making
		/// 1 : Practice
		/// 2 : Tournament
		/// 3 : Tutorial
		/// 4 : Co-Op with bots
		/// 5 : Team Match
		/// 6 : Solo Queue
		/// 7 : Ranked Public Matchmaking
		/// 8 : 1v1.
		/// </summary>
		public static string GetLobbyType(int lobbytypeInt)
		{
			switch(lobbytypeInt)
			{
				case -1:
					return "Invalid";
				case 0:
					return "Public Matchmaking";
				case 1:
					return "Practice";
				case 2:
					return "Tournament";
				case 3:
					return "Tutorial";
				case 4:
					return "Co-Op with Bots";
				case 5:
					return "Team Match";
				case 6:
					return "Solo Queue";
				case 7:
					return "Ranked Public Matchmaking";
				case 8:
					return "1v1 Practice Matchmaking";
				default:
					return "Invalid Match Type";
			}
		}
	}
}
