using System.Collections.Generic;
using System.IO;
using System.Linq;
using DotaApi.Model;

namespace DotaApi.Helpers
{
	public class Common
	{
		// This should be the only value that you need to change, obtain an API
		// key from steam and replace below.
		public static string API = "23CEC905617913D3710DC832621110F3";

		public static string ITEM_FILE = @"D:\git\DotaApi\items.txt";
		public static string ABILITY_FILE = @"D:\git\DotaApi\npc_abilities.txt";

		// steam urls to get json data
		public static string MATCHHISTORYURL = @"https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/V001/?key=";
		public static string HEROSURL = @"https://api.steampowered.com/IEconDOTA2_570/GetHeroes/v0001/?key=";
		public static string MATCHDETAILSURL = @"https://api.steampowered.com/IDOTA2Match_570/GetMatchDetails/V001/?&key=";
		public static string STEAMACCOUNTURL = @"http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=";
		public static string MATCHHISTORYBYSEQURL = @"https://api.steampowered.com/IDOTA2Match_570/GetMatchHistoryBySequenceNum/v0001/?key=";

		/// <summary>
		/// Generic Method using <see cref="ISearchableDictionary"/> properties.
		/// Converts incoming List into a dictionary and then returns Name corresponding to the ID.
		/// Used for Items, Abilities and Heros.
		/// </summary>
		public static string ConvertIDtoName<T>(int id, List<T> items) where T : ISearchableDictionary
		{
			var itemDict = items.ToDictionary(x => x.ID, x => x.Name);

			return StringManipulation.UppercaseFirst(itemDict.Where(x => x.Key == id).FirstOrDefault().Value);
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

		public static List<Item> ParseItemsText(string[] text)
		{
			bool itemfound = false;

			// List to hold our parsed items.
			List<Item> items = new List<Item>();

			// This will be used to store this section of the item.
			List<string> curitem = new List<string>();

			// Item object will be populating
			Item item = new Item();

			// Lets go line by line to start parsing.
			foreach (string line in text)
			{
				// Clean up the text, remove quotes.
				string line_noquotes = line.Replace("\"", "");
				string trimmed_clean = line.Replace("\"", "").Replace("\t", "").Replace("_", " ").Trim();

				// If line starts with item_ then
				// This is where we will start capturing.
				if (line_noquotes.StartsWith("	item_"))
				{
					item = new Item();
					itemfound = true;
					item.Name = trimmed_clean.Replace("item ", "");
					item.Name = StringManipulation.UppercaseFirst(item.Name);
					curitem.Add(trimmed_clean);
				}

				// If we are on a current item then lets do
				// Some other operations to gather details
				if (itemfound == true)
				{
					if (trimmed_clean.StartsWith("ID"))
					{
						item.ID = System.Convert.ToInt32(trimmed_clean.Replace("ID", "").Split('/')[0]);
						curitem.Add(line);
					}

					if (trimmed_clean.StartsWith("AbilityCastRange"))
					{
						item.CastRange = trimmed_clean.Replace("AbilityCastRange", "");
						curitem.Add(trimmed_clean);
					}

					if (trimmed_clean.StartsWith("AbilityCastPoint"))
					{
						item.CastPoint = trimmed_clean.Replace("AbilityCastPoint", "");
						curitem.Add(trimmed_clean);
					}

					if (trimmed_clean.StartsWith("AbilityCooldown"))
					{
						item.Cooldown = trimmed_clean.Replace("AbilityCooldown", "");
						curitem.Add(trimmed_clean);
					}

					if (trimmed_clean.StartsWith("AbilityManaCost"))
					{
						item.ManaCost = trimmed_clean.Replace("AbilityManaCost", "");
						curitem.Add(trimmed_clean);
					}

					if (trimmed_clean.StartsWith("ItemCost"))
					{
						item.ItemCost = trimmed_clean.Replace("ItemCost", "");
						curitem.Add(trimmed_clean);
					}

					if (trimmed_clean.StartsWith("ItemShopTags"))
					{
						item.ItemShopTags = trimmed_clean.Replace("ItemShopTags", "");
						curitem.Add(trimmed_clean);
					}

					if (trimmed_clean.StartsWith("ItemQuality"))
					{
						item.ItemQuality = trimmed_clean.Replace("ItemQuality", "");
						;
						curitem.Add(trimmed_clean);
					}

					if (trimmed_clean.StartsWith("ItemAliases"))
					{
						item.ItemAliases = trimmed_clean.Replace("ItemAliases", "");
						;
						curitem.Add(trimmed_clean);
					}

					if (trimmed_clean.StartsWith("ItemStackable"))
					{
						item.ItemStackable = trimmed_clean.Replace("ItemStackable", "");
						;
						curitem.Add(trimmed_clean);
					}

					if (trimmed_clean.StartsWith("ItemShareability"))
					{
						item.ItemShareability = trimmed_clean.Replace("ItemShareability", "");
						;
						curitem.Add(trimmed_clean);
					}

					if (trimmed_clean.StartsWith("ItemShareability"))
					{
						item.ItemShareability = trimmed_clean.Replace("ItemShareability", "");
						;
						curitem.Add(trimmed_clean);
					}

					//end current item, save to list
					if (trimmed_clean.StartsWith("//="))
					{
						//add to our list of items/
						items.Add(item);
						curitem.Add(trimmed_clean);
						itemfound = false;
					}
				}
			}
			return items;
		}
	}
}
