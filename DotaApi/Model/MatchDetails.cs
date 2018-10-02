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

			Console.WriteLine("Match ID: {0}", match.Match_ID);
			Console.WriteLine("Match SeqNum: {0}", match.Match_Seq_Num);
			Console.WriteLine("Human Players: {0}", match.Human_Players);
			Console.WriteLine("Start Time: {0}", match.StartTime);
			match.Lobbytype = LobbyTypes.GetLobbyType(match.Lobby_Yype);

			var onePlayer = detail.Result.Players[0];
			DataTable t = new DataTable();

			foreach (var player in detail.Result.Players)
			{
				Console.WriteLine("Account ID: {0}", player.Account_ID);
				player.Name = Common.ConvertIDtoName(player.Hero_ID, heroes);
				player.Steamid64 = StringManipulation.SteamIDConverter(player.Account_ID);
				player.Steamid32 = StringManipulation.SteamIDConverter64to32(player.Steamid64);

				// getting item names based on the id number
				player.Item0 = Common.ConvertIDtoName(Convert.ToInt32(player.Item_0.ToString()), DotaItems);
				player.Item1 = Common.ConvertIDtoName(Convert.ToInt32(player.Item_1.ToString()), DotaItems).Replace("item ", "");
				player.Item2 = Common.ConvertIDtoName(Convert.ToInt32(player.Item_2.ToString()), DotaItems);
				player.Item3 = Common.ConvertIDtoName(Convert.ToInt32(player.Item_3.ToString()), DotaItems);
				player.Item4 = Common.ConvertIDtoName(Convert.ToInt32(player.Item_4.ToString()), DotaItems);
				player.Item5 = Common.ConvertIDtoName(Convert.ToInt32(player.Item_5.ToString()), DotaItems);

				var steamaccount = SteamAccount.GetSteamAccount(player.Account_ID);
				player.SteamVanityName = steamaccount.PlayerName;

				StringBuilder sb = new StringBuilder();

				sb.AppendLine($"Vanity Name: {player.SteamVanityName}");
				sb.AppendLine($"Hero: {player.Name}");
				sb.AppendLine($"K/D/A: {player.Kills}/{player.Deaths}/{player.Assists}");
				sb.AppendLine($"CS: {player.Last_Hits}/{player.Denies}");
				sb.AppendLine($"\tGold");
				sb.AppendLine($"\tGPM: {player.Gold_Per_Min}");
				sb.AppendLine($"\tGold Spent: {player.Gold_Spent}");
				sb.AppendLine($"\tEnd of game Gold: {player.Gold}");

				sb.AppendLine("Items");
				if (player.Item0 != null)
					sb.Append($"Slot 0: {player.Item0} |");
				if (player.Item1 != null)
					sb.Append($" Slot 1: {player.Item1} |");
				if (player.Item2 != null)
					sb.Append($" Slot 2: {player.Item2} |");
				if (player.Item3 != null)
					sb.Append($" Slot 3: {player.Item3} |");
				sb.Append($" Slot 4: {player.Item4} |");
				if (player.Item4 != null)
					sb.AppendLine($"Slot 5: {player.Item5}");
				sb.AppendLine("Ability Upgrade Path");

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
				Console.Write(sb.ToString());
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
			public string SteamVanityName { get; set; }
			public string Steamid64 { get; set; }
			public string Steamid32 { get; set; }
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
