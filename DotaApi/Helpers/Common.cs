using System.Collections.Generic;
using DotaApi.Model;

namespace DotaApi.Helpers
{
	public class Common
	{
		// This should be the only value that you need to change, obtain an API
		// key from steam and replace below.
		public static string API = "23CEC905617913D3710DC832621110F3";

		public static string items_txt_path = @"C:\dota\items.txt";
		public static string abilities_txt_path = @"C:\dota\npc_abilities.txt";

		// steam url to get json data
		public static string matchhistoryUrl = @"https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/V001/?key=";
		public static string herosUrl = @"https://api.steampowered.com/IEconDOTA2_570/GetHeroes/v0001/?key=";
		public static string matchdetailsUrl = @"https://api.steampowered.com/IDOTA2Match_570/GetMatchDetails/V001/?&key=";
		public static string steamaccountUrl = @"http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=";
		public static string matchhistorybyseqUrl = @"https://api.steampowered.com/IDOTA2Match_570/GetMatchHistoryBySequenceNum/v0001/?key=";

		/// <summary>
		/// Loops through the list of heroes and returns hero's name.
		/// </summary>
		public static string ConvertHeroFromID(int id, List<Heroes.Hero> heroes)
		{
			string heronamestr = string.Empty;

			foreach(var hero in heroes)
			{
				if(hero.ID == id)
				{
					heronamestr = StringManipulation.UppercaseFirst(hero.Name);
					break;
				}
			}
			return heronamestr;
		}

		/// <summary>
		/// Loops through the list of DotaItems and returns item's name
		/// </summary>
		public static string ConvertItemIDtoName(string id, List<Items.Item> DotaItems)
		{
			string itemname = string.Empty;
			foreach(var item in DotaItems)
			{
				if(item.ID == id)
				{
					itemname = StringManipulation.UppercaseFirst(item.Name);
					break;
				}
			}
			return itemname;
		}

		/// <summary>
		/// Loops through the list of Abilities  and returns ability's name
		/// </summary>
		public static string ConvertAbilityIDtoName(string id, List<Abilities.Ability> Abilities)
		{
			string abilityname = string.Empty;
			foreach(var ability in Abilities)
			{
				if(ability.ID == id)
				{
					abilityname = StringManipulation.UppercaseFirst(ability.Name);
					break;
				}
			}
			return abilityname;
		}
	}
}
