using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using DotaApi.Model;
using Newtonsoft.Json;
using static DotaApi.Model.Heroes;
using static DotaApi.Model.Item;

namespace DotaApi.Helpers
{
	public class Common
	{
		// This should be the only value that you need to change, obtain an API
		// key from steam and replace below.
		public static string API = "27675C91D5E776860B1903086197AFBA";
		//23CEC905617913D3710DC832621110F3

		public static string ITEM_FILE = @"D:\git\DotaApi\items.txt";
		public static string ABILITY_FILE = @"D:\git\DotaApi\npc_abilities.txt";

		// steam urls to get json data
		public static string MATCHHISTORYURL = @"https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/V001/?key=";
		public static string ITEMSURL = @"http://api.steampowered.com/IEconDOTA2_570/GetGameItems/v0001/?key=";
		public static string HEROSURL = @"https://api.steampowered.com/IEconDOTA2_570/GetHeroes/v0001/?key=";
		public static string MATCHDETAILSURL = @"https://api.steampowered.com/IDOTA2Match_570/GetMatchDetails/V001/?&key=";
		public static string STEAMACCOUNTURL = @"http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=";
		public static string MATCHHISTORYBYSEQURL = @"https://api.steampowered.com/IDOTA2Match_570/GetMatchHistoryBySequenceNum/v0001/?key=";

		/// <summary>
		/// Generic Method using <see cref="ISearchableDictionary"/> properties.
		/// Converts incoming List into a dictionary and then returns Name corresponding to the ID.
		/// Used for Items, Abilities and Heros.
		/// </summary>
		public static string ConvertIDtoName<T>(int id, List<T> list) where T : ISearchableDictionary
		{
			var itemDict = list.ToDictionary(x => x.ID, x => x.Name);
			string name = itemDict.Where(x => x.Key == id).FirstOrDefault().Value;

			return name != null ? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name) : "";
		}

		public static List<Ability> ParseAbilityText()
		{
			var text = File.ReadAllLines(ABILITY_FILE);
			bool itemfound = false;

			// List to hold our parsed items.
			List<Ability> abilities = new List<Ability>();

			// Item object will be populating
			Ability ability = new Ability();

			//lets go line by line to start parsing.
			int count = 0;
			foreach (string line in text)
			{
				// Clean up the text, remove quotes.
				string trimmed_clean = line.Replace("\"", "").Replace("\t", "").Trim();

				// If line starts with item_ then this is where we will start capturing.
				if (trimmed_clean.StartsWith("ID"))
				{
					ability = new Ability();
					itemfound = true;
					ability.ID = System.Convert.ToInt32(trimmed_clean.Replace("ID", "").Split('/')[0].Trim());

					// We get the hero name
					ability.HeroName = text[count - 4].Split('_')[0].Replace("\"", "").Replace("\t", "").Trim();

					// Lets remove hero name from original
					ability.Name = text[count - 4].Replace(ability.HeroName, "").Replace("_", " ").Replace("\"", "").Replace("\t", "").Trim();
				}

				// If we are on a current item then lets do some other operations to gather details
				if (itemfound == true)
				{
					if (trimmed_clean.StartsWith("AbilityBehavior"))
						ability.AbilityBehavior = trimmed_clean.Replace("AbilityBehavior", "");
					if (trimmed_clean.StartsWith("AbilityUnitTargetTeam"))
						ability.AbilityUnitTargetTeam = trimmed_clean.Replace("AbilityUnitTargetTeam", "");
					if (trimmed_clean.StartsWith("AbilityUnitTargetType"))
						ability.AbilityUnitTargetType = trimmed_clean.Replace("AbilityUnitTargetType", "");
					if (trimmed_clean.StartsWith("AbilityUnitDamageType"))
						ability.AbilityUnitDamageType = trimmed_clean.Replace("AbilityUnitDamageType", "");
					if (trimmed_clean.StartsWith("AbilityCastRange"))
						ability.AbilityCastRange = trimmed_clean.Replace("AbilityCastRange", "");
					if (trimmed_clean.StartsWith("AbilityCastPoint"))
						ability.AbilityCastPoint = trimmed_clean.Replace("AbilityCastPoint", "");
					if (trimmed_clean.StartsWith("AbilityCooldown"))
						ability.AbilityCooldown = trimmed_clean.Replace("AbilityCooldown", "");
					if (trimmed_clean.StartsWith("AbilityManaCost"))
						ability.AbilityManaCost = trimmed_clean.Replace("AbilityManaCost", "");

					// End current item, save to list
					if (trimmed_clean.StartsWith("//="))
					{
						// Add to our list of items/
						abilities.Add(ability);
						itemfound = false;
					}
				}
				count++;
			}
			return abilities;
		}

		public static List<Item> GetGameItems(bool verbose)
		{
			string response = GetWebResponse.DownloadSteamAPIString(ITEMSURL, API);
			ItemsObject heroesObject = JsonConvert.DeserializeObject<ItemsObject>(response);
			List<Item> resultItems = new List<Item>();

			//if verbose flag is set to true, then show console
			//message.
			if (verbose == true)
				Console.WriteLine("Count of Heroes {0}", heroesObject.Result.Items.Count);

			int herocountInt = 0;
			Item Item;
			foreach (var item in heroesObject.Result.Items)
			{
				if (verbose == true)
					Console.Write("{0} of {1}. Hero orig-name: {2}|", herocountInt, heroesObject.Result.Items.Count, item.Name);
				string rawItemName = item.Name.Replace("item_", "");
				string itemImage = $"http://cdn.dota2.com/apps/dota2/images/items/{rawItemName}_lg.png";
				Item = new Item
				{
					Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(rawItemName.Replace("_", " ")),
					ID = item.ID,
					ItemImage = itemImage
				};
				if (verbose == true)
					Console.WriteLine(" cleaned: {0}", Item.Name);

				resultItems.Add(Item);
				herocountInt++;
			}

			return resultItems;
		}
		/// <summary>
		/// Gets the latest list of heroes from Steam, requires "HerosClass".
		/// </summary>
		public static List<Hero> GetHeroes(bool verbose)
		{
			string response = GetWebResponse.DownloadSteamAPIString(Common.HEROSURL, Common.API);
			HeroesObject heroesObject = JsonConvert.DeserializeObject<HeroesObject>(response);
			List<Hero> resultHeroes = new List<Hero>();

			//if verbose flag is set to true, then show console
			//message.
			if (verbose == true)
				Console.WriteLine("Count of Heroes {0}", heroesObject.Result.Heroes.Count);

			int herocountInt = 1;

			//hero id 0 is private profile i think?
			Hero Hero = new Hero
			{
				ID = 0,
				Name = "Npc_dota_hero_Private Profile"
			};
			if (verbose == true)
				Console.WriteLine("Hero orig-name: {0}", Hero.Name);

			resultHeroes.Add(Hero);

			foreach (var hero in heroesObject.Result.Heroes)
			{
				if (verbose == true)
					Console.Write("{0} of {1}. Hero orig-name: {2}|", herocountInt, heroesObject.Result.Heroes.Count, hero.Name);

				Hero = new Hero
				{
					Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(hero.Name.Replace("npc_dota_hero_", "").Replace("_", " ")),
					ID = hero.ID,
					OrigName = hero.Name
				};
				if (verbose == true)
					Console.WriteLine(" cleaned: {0}", Hero.Name);

				resultHeroes.Add(Hero);
				herocountInt++;
			}

			return resultHeroes;
		}

		//public static List<Item> ParseItemsText(string[] text)
		//{
		//	bool itemfound = false;

		//	// List to hold our parsed items.
		//	List<Item> items = new List<Item>();

		//	// This will be used to store this section of the item.
		//	List<string> curitem = new List<string>();

		//	// Item object will be populating
		//	Item item = new Item();

		//	// Lets go line by line to start parsing.
		//	foreach (string line in text)
		//	{
		//		// Clean up the text, remove quotes.
		//		string line_noquotes = line.Replace("\"", "");
		//		string trimmed_clean = line.Replace("\"", "").Replace("\t", "").Replace("_", " ").Trim();

		//		// If line starts with item_ then
		//		// This is where we will start capturing.
		//		if (line_noquotes.StartsWith("	item_"))
		//		{
		//			item = new Item();
		//			itemfound = true;
		//			item.Name = trimmed_clean.Replace("item ", "");
		//			item.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.Name);
		//			curitem.Add(trimmed_clean);
		//		}

		//		// If we are on a current item then lets do
		//		// Some other operations to gather details
		//		if (itemfound == true)
		//		{
		//			if (trimmed_clean.StartsWith("ID"))
		//			{
		//				item.ID = System.Convert.ToInt32(trimmed_clean.Replace("ID", "").Split('/')[0]);
		//				curitem.Add(line);
		//			}

		//			if (trimmed_clean.StartsWith("AbilityCastRange"))
		//			{
		//				item.CastRange = trimmed_clean.Replace("AbilityCastRange", "");
		//				curitem.Add(trimmed_clean);
		//			}

		//			if (trimmed_clean.StartsWith("AbilityCastPoint"))
		//			{
		//				item.CastPoint = trimmed_clean.Replace("AbilityCastPoint", "");
		//				curitem.Add(trimmed_clean);
		//			}

		//			if (trimmed_clean.StartsWith("AbilityCooldown"))
		//			{
		//				item.Cooldown = trimmed_clean.Replace("AbilityCooldown", "");
		//				curitem.Add(trimmed_clean);
		//			}

		//			if (trimmed_clean.StartsWith("AbilityManaCost"))
		//			{
		//				item.ManaCost = trimmed_clean.Replace("AbilityManaCost", "");
		//				curitem.Add(trimmed_clean);
		//			}

		//			if (trimmed_clean.StartsWith("ItemCost"))
		//			{
		//				item.ItemCost = trimmed_clean.Replace("ItemCost", "");
		//				curitem.Add(trimmed_clean);
		//			}

		//			if (trimmed_clean.StartsWith("ItemShopTags"))
		//			{
		//				item.ItemShopTags = trimmed_clean.Replace("ItemShopTags", "");
		//				curitem.Add(trimmed_clean);
		//			}

		//			if (trimmed_clean.StartsWith("ItemQuality"))
		//			{
		//				item.ItemQuality = trimmed_clean.Replace("ItemQuality", "");
		//				curitem.Add(trimmed_clean);
		//			}

		//			if (trimmed_clean.StartsWith("ItemAliases"))
		//			{
		//				if (string.IsNullOrEmpty(item.ItemAliases))
		//				{
		//					item.ItemAliases = trimmed_clean.Replace("ItemAliases", "");
		//					int index = item.ItemAliases.LastIndexOf(";") == -1 ? 0 : item.ItemAliases.LastIndexOf(";");
		//					item.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.ItemAliases.Substring(index).Replace(";", ""));
		//					curitem.Add(trimmed_clean);
		//				}
		//			}

		//			if (trimmed_clean.StartsWith("ItemStackable"))
		//			{
		//				item.ItemStackable = trimmed_clean.Replace("ItemStackable", "");
		//				curitem.Add(trimmed_clean);
		//			}

		//			if (trimmed_clean.StartsWith("ItemShareability"))
		//			{
		//				item.ItemShareability = trimmed_clean.Replace("ItemShareability", "");
		//				curitem.Add(trimmed_clean);
		//			}

		//			if (trimmed_clean.StartsWith("ItemShareability"))
		//			{
		//				item.ItemShareability = trimmed_clean.Replace("ItemShareability", "");
		//				;
		//				curitem.Add(trimmed_clean);
		//			}

		//			//end current item, save to list
		//			if (trimmed_clean.StartsWith("//="))
		//			{
		//				//add to our list of items/
		//				items.Add(item);
		//				curitem.Add(trimmed_clean);
		//				itemfound = false;
		//			}
		//		}
		//	}
		//	return items;
		//}
	}
}
