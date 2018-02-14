using System;
using System.Collections.Generic;
using DotaApi.Helpers;
using Newtonsoft.Json;

namespace DotaApi.Model
{
	public class MatchHistory
	{
		/// <summary>
		/// Gets the latest (up to) 100 matches.
		/// </summary>
		public static List<Match> GetMatchHistory()
		{
			// to do
			// Create a player class to hold more information regarding the individual
			// Players including abilities/build info

			// we get a list of the latest heroes
			List<Heroes.Hero> heroes = Heroes.GetHeroes(false);
			List<Match> Matches = new List<Match>();

			// download the response
			string response = GetWebResponse.DownloadSteamAPIString(Common.matchhistoryUrl, Common.API);

			// Serializing json data to our class
			// This is when we parse all of the json data into our custom object classes
			MatchRootObject ourResponse = JsonConvert.DeserializeObject<MatchRootObject>(response);
			Console.WriteLine("Total Matches {0}", ourResponse.Result.Matches.Count.ToString());
			int matchcountInt = 0;
			foreach(var match in ourResponse.Result.Matches)
			{
				Console.WriteLine(" Match {0} of {1}", matchcountInt, ourResponse.Result.Matches.Count);
				// Start looking up details on first match
				Match Match = new Match();

				Console.WriteLine("     Match ID: {0}", match.Match_ID);
				Console.WriteLine("     Lobby Type: {0} ({1})", LobbyTypes.GetLobbyType(match.Lobby_Type), match.Lobby_Type);
				Console.WriteLine("     MatchSeqNum: {0}", match.Match_Seq_Num);


				Match.Match_ID = match.Match_ID;
				Match.Lobby_Type = match.Lobby_Type;
				Match.LobbyType = LobbyTypes.GetLobbyType(match.Lobby_Type);
				Match.Match_Seq_Num = match.Match_Seq_Num;
				Match.Players = match.Players;
				Match.Start_Time = match.Start_Time;
				Match.StartTime = StringManipulation.UnixTimeStampToDateTime(match.Start_Time);
				Console.WriteLine("     Start Time: {0}", Match.StartTime);

				Console.WriteLine("     Real Players: {0}", match.Players.Count.ToString());
				int playercountInt = 1;
				foreach(var player in match.Players)
				{
					Console.WriteLine("         Player {0} of {1}", playercountInt, match.Players.Count);
					string name = Common.ConvertHeroFromID(player.Hero_ID, heroes);
					player.Name = name;
					player.SteamVanityName = SteamAccount.GetSteamAccount(player.Account_ID).PlayerName;

					Console.WriteLine("             Name: {0}", name);
					Console.WriteLine("             Hero ID: {0}", player.Hero_ID);
					Console.WriteLine("             Account ID: {0}", player.Account_ID);
					Console.WriteLine("             Vanity Name: {0}", player.SteamVanityName);

					playercountInt++;
				}

				Console.WriteLine("**************************");
				Matches.Add(Match);
				matchcountInt++;
			}
			return Matches;


		}

		/// <summary>Used to get the matches in the order which they were recorded (i.e. sorted ascending by match_seq_num).
		/// This means that the first match on the first page of results returned by the call will be the very first public mm-match recorded in the stats.
		/// </summary>
		public static List<MatchDetails.MatchDetailsResult> GetMatchHistoryBySeqNum(int matchseqnumb, int requestedmatches, List<Items.Item> DotaItems)
		{
			// to do
			// create a player class to hold more information regarding the individual
			// players including abilities/build info

			// we get a list of the latest heroes
			List<Heroes.Hero> heroes = Heroes.GetHeroes(false);

			// create a container to store all of matches with everything
			List<MatchDetails.MatchDetailsResult> matchlist = new List<MatchDetails.MatchDetailsResult>();

			// download the response
			string response = GetWebResponse.DownloadSteamAPIString(Common.matchhistorybyseqUrl, Common.API + "&start_at_match_seq_num=" + matchseqnumb + "&matches_requested=" + requestedmatches);


			// serializing json data to our class
			// this is when we parse all of the json data into
			// our custom object classes
			MatchRootObject ourResponse = JsonConvert.DeserializeObject<MatchRootObject>(response);
			foreach(var match in ourResponse.Result.Matches)
			{
				var m = MatchDetails.GetMatchDetail(match.Match_ID, DotaItems);
				matchlist.Add(m);
			}
			return matchlist;
		}
	}

	public class Player
	{
		public string Account_ID { get; set; }
		public int Player_Slot { get; set; }
		public int Hero_ID { get; set; }
		public string Name { get; set; }
		public string SteamVanityName { get; set; }
	}

	public class Match
	{
		public int Match_ID { get; set; }
		public int Match_Seq_Num { get; set; }
		public int Start_Time { get; set; }
		public DateTime StartTime { get; set; }
		public int Lobby_Type { get; set; }
		public string LobbyType { get; set; }
		public List<Player> Players { get; set; }
		public int HumanPlayers { get; set; }
	}

	public class Result
	{
		public int Status { get; set; }
		public int Num_Results { get; set; }
		public int Total_Results { get; set; }
		public int Results_Remaining { get; set; }
		public List<Match> Matches { get; set; }
	}

	public class MatchRootObject
	{
		public Result Result { get; set; }
	}

}
