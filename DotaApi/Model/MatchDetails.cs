using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DotaApi.Helpers;
using Newtonsoft.Json;

namespace DotaApi.Model
{
	public class MatchDetails
	{

		/// <summary>
		/// Gets match details for a single match, this includes player builds and details. Requires "MatchClass".
		/// </summary>
		public static MatchDetailsResult GetMatchDetail(long matchid, List<Item> DotaItems)
		{
			// to do
			// get match details
			// Uri is: https:// api.steampowered.com/IDOTA2Match_570/GetMatchDetails/V001/?match_id=27110133&key=<key>

			// we get a list of the latest heroes
			var heroes = Heroes.GetHeroes(false);

			// Get list of abilities
			var abilities = Common.ParseAbilityText();

			string response = GetWebResponse.DownloadSteamAPIString(Common.MATCHDETAILSURL, Common.API + "&match_id=" + matchid);

			var detail = JsonConvert.DeserializeObject<MatchDetailsRootObject>(response);
			MatchDetailsResult match = detail.Result;

			match.StartTime = StringManipulation.UnixTimeStampToDateTime(detail.Result.Start_Time);

			Console.WriteLine($"Match ID: {match.Match_ID}");
			Console.WriteLine($"Match SeqNum: {match.Match_Seq_Num}");
			Console.WriteLine($"Human Players: {match.Human_Players}");
			Console.WriteLine($"Duration: {match.Duration}");
			Console.WriteLine($"Game Mode: {match.Game_Mode}");
			match.Lobbytype = LobbyTypes.GetLobbyType(match.Lobby_Yype);

			foreach (var player in detail.Result.Players)
			{
				StringBuilder sb = new StringBuilder();

				sb.AppendLine($"\nAccount ID: {player.Account_ID}");
				player.Name = Common.ConvertIDtoName(player.Hero_ID, heroes);
				player.Steamid64 = StringManipulation.SteamIDConverter(player.Account_ID);
				player.Steamid32 = StringManipulation.SteamIDConverter64to32(player.Steamid64);

				var steamaccount = SteamAccount.GetSteamAccount(player.Account_ID);
				player.PlayerName = steamaccount.PersonaName;

				sb.AppendLine($"Player Name: {player.PlayerName}");
				sb.AppendLine($"Hero: {player.Name}");
				sb.AppendLine($"\tHero Level: {player.Level}");
				sb.AppendLine($"K/D/A: {player.Kills}/{player.Deaths}/{player.Assists}");
				sb.AppendLine($"CS: {player.Last_Hits}/{player.Denies}");
				sb.AppendLine($"\tGPM: {player.Gold_Per_Min}");
				sb.AppendLine($"\tXPM: {player.Xp_Per_Min}");

				// getting item names based on the id number
				player.Item0 = player.Item_0 > 0 ? Common.ConvertIDtoName(player.Item_0, DotaItems) : null;
				player.Item1 = player.Item_1 > 0 ? Common.ConvertIDtoName(player.Item_1, DotaItems) : null;
				player.Item2 = player.Item_2 > 0 ? Common.ConvertIDtoName(player.Item_2, DotaItems) : null;
				player.Item3 = player.Item_3 > 0 ? Common.ConvertIDtoName(player.Item_3, DotaItems) : null;
				player.Item4 = player.Item_4 > 0 ? Common.ConvertIDtoName(player.Item_4, DotaItems) : null;
				player.Item5 = player.Item_5 > 0 ? Common.ConvertIDtoName(player.Item_5, DotaItems) : null;

				player.Backpack0 = player.Backpack_0 > 0 ? Common.ConvertIDtoName(player.Backpack_0, DotaItems) : null;
				player.Backpack1 = player.Backpack_1 > 0 ? Common.ConvertIDtoName(player.Backpack_1, DotaItems) : null;
				player.Backpack2 = player.Backpack_2 > 0 ? Common.ConvertIDtoName(player.Backpack_2, DotaItems) : null;

				sb.AppendLine("Items:");
				if (!string.IsNullOrEmpty(player.Item0))
					sb.Append($"Slot 1: {player.Item0}");
				if (!string.IsNullOrEmpty(player.Item1))
					sb.Append($" | Slot 2: {player.Item1}");
				if (!string.IsNullOrEmpty(player.Item2))
					sb.Append($" | Slot 3: {player.Item2}");
				if (!string.IsNullOrEmpty(player.Item3))
					sb.Append($" | Slot 4: {player.Item3}");
				if (!string.IsNullOrEmpty(player.Item4))
					sb.Append($" | Slot 5: {player.Item4}");
				if (!string.IsNullOrEmpty(player.Item5))
					sb.Append($" | Slot 6: {player.Item5}");
				sb.AppendLine();

				if (!string.IsNullOrEmpty(player.Backpack0))
					sb.Append($"Backpack Slot 1: {player.Backpack0}");
				if (!string.IsNullOrEmpty(player.Backpack1))
					sb.Append($" | Backpack  Slot 2: {player.Backpack1}");
				if (!string.IsNullOrEmpty(player.Backpack2))
					sb.Append($" | Backpack Slot 3: {player.Backpack2}");
				sb.AppendLine();
				sb.Append("Ability Upgrade Path:");
				sb.AppendLine();

				// ability output
				// In some scenarios a user might play the game
				// but not upgrade any abilities or he/she
				// might get kicked out before they get a chance.
				// In this type of situation, we must check for null
				// before continuing.
				if (player.Ability_Upgrades != null)
				{
					foreach (var ability in player.Ability_Upgrades)
					{
						// clean up the object a bit and put
						// id where it should be.
						ability.ID = ability.Ability;

						// map the id to a readable name.
						ability.Name = Common.ConvertIDtoName(Convert.ToInt32(ability.Ability), abilities);

						// add the upgrade seconds to the original start
						// time to get the upgrade time.
						ability.UpgradeTime = match.StartTime.AddSeconds(ability.Time);

						// output to screen
						sb.AppendLine($" {ability.Name} upgraded at {ability.Level} @ {ability.UpgradeTime}");
					}
				}
				else
				{
					sb.AppendLine("No abilities data");
				}
				Console.WriteLine(sb.ToString());
			}
			return match;
		}

		public class AbilityUpgrade
		{
			public string Ability { get; set; }
			public string Name { get; set; }
			public int Time { get; set; }
			public DateTime UpgradeTime { get; set; }
			public int Level { get; set; }
			public string ID { get; set; }
		}

		public class Player
		{
			public string Account_ID { get; set; }
			public string Name { get; set; }
			public string PlayerName { get; set; }
			public string Steamid64 { get; set; }
			public string Steamid32 { get; set; }
			public string ProfileUrl { get; set; }
			public int PlayerSlot { get; set; }
			public int Hero_ID { get; set; }
			public int Item_0 { get; set; }
			public string Item0 { get; set; }
			public int Item_1 { get; set; }
			public string Item1 { get; set; }
			public int Item_2 { get; set; }
			public string Item2 { get; set; }
			public int Item_3 { get; set; }
			public string Item3 { get; set; }
			public int Item_4 { get; set; }
			public string Item4 { get; set; }
			public int Item_5 { get; set; }
			public string Item5 { get; set; }
			public int Kills { get; set; }
			public int Deaths { get; set; }
			public int Assists { get; set; }
			public int Leaver_Status { get; set; }
			public int Gold { get; set; }
			public int Last_Hits { get; set; }
			public int Denies { get; set; }
			public int Gold_Per_Min { get; set; }
			public int Xp_Per_Min { get; set; }
			public int Gold_Spent { get; set; }
			public int Hero_Damage { get; set; }
			public int Tower_Damage { get; set; }
			public int Hero_Healing { get; set; }
			public int Level { get; set; }
			public int Backpack_0 { get; set; }
			public string Backpack0 { get; set; }
			public int Backpack_1 { get; set; }
			public string Backpack1 { get; set; }
			public int Backpack_2 { get; set; }
			public string Backpack2 { get; set; }
			public List<AbilityUpgrade> Ability_Upgrades { get; set; }
		}

		public class MatchDetailsResult
		{
			public List<Player> Players { get; set; }
			public bool Radiant_Win { get; set; }
			public int Duration { get; set; }
			public int Start_Time { get; set; }
			public DateTime StartTime { get; set; }
			public long Match_ID { get; set; }
			public long Match_Seq_Num { get; set; }
			public int Tower_Status_Radiant { get; set; }
			public int Tower_Status_Dire { get; set; }
			public int Barracks_Status_Radiant { get; set; }
			public int Barracks_Status_Dire { get; set; }
			public int Cluster { get; set; }
			public int First_Blood_Time { get; set; }
			public int Lobby_Yype { get; set; }
			public string Lobbytype { get; set; }
			public int Human_Players { get; set; }
			public int Leagueid { get; set; }
			public int Positive_Votes { get; set; }
			public int Negative_Votes { get; set; }
			public int Game_Mode { get; set; }
		}

		public class MatchDetailsRootObject
		{
			public MatchDetailsResult Result { get; set; }
		}
	}
}
