using System.Collections.Generic;
using DotaApi.Helpers;


namespace DotaApi.Model
{
	public class Items
	{
		public class Item
		{
			public string Name { get; set; }
			public string ID { get; set; }
			public string CastRange { get; set; }
			public string CastPoint { get; set; }
			public string Cooldown { get; set; }
			public string ManaCost { get; set; }
			public string ItemCost { get; set; }
			public string ItemShopTags { get; set; }
			public string ItemQuality { get; set; }
			public string ItemAliases { get; set; }
			public string ItemStackable { get; set; }
			public string ItemShareability { get; set; }
			public string ItemPermanent { get; set; }
			public string ItemInitialCharges { get; set; }
			public string SideShop { get; set; }
			public string AbilitySharedCooldown { get; set; }
			public string AbilityChannelTime { get; set; }
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
			foreach(string line in text)
			{
				// Clean up the text, remove quotes.
				string line_noquotes = line.Replace("\"", "");
				string trimmed_clean = line.Replace("\"", "").Replace("\t", "").Replace("_", " ").Trim();

				// If line starts with item_ then
				// This is where we will start capturing.
				if(line_noquotes.StartsWith("	item_"))
				{
					item = new Item();
					itemfound = true;
					item.Name = trimmed_clean.Replace("item ", "");
					item.Name = StringManipulation.UppercaseFirst(item.Name);
					curitem.Add(trimmed_clean);
				}

				// If we are on a current item then lets do
				// Some other operations to gather details
				if(itemfound == true)
				{
					if(trimmed_clean.StartsWith("ID"))
					{
						item.ID = trimmed_clean.Replace("ID", "").Split('/')[0];
						curitem.Add(line);
					}

					if(trimmed_clean.StartsWith("AbilityCastRange"))
					{
						item.CastRange = trimmed_clean.Replace("AbilityCastRange", "");
						curitem.Add(trimmed_clean);
					}

					if(trimmed_clean.StartsWith("AbilityCastPoint"))
					{
						item.CastPoint = trimmed_clean.Replace("AbilityCastPoint", "");
						curitem.Add(trimmed_clean);
					}

					if(trimmed_clean.StartsWith("AbilityCooldown"))
					{
						item.Cooldown = trimmed_clean.Replace("AbilityCooldown", "");
						curitem.Add(trimmed_clean);
					}

					if(trimmed_clean.StartsWith("AbilityManaCost"))
					{
						item.ManaCost = trimmed_clean.Replace("AbilityManaCost", "");
						curitem.Add(trimmed_clean);
					}

					if(trimmed_clean.StartsWith("ItemCost"))
					{
						item.ItemCost = trimmed_clean.Replace("ItemCost", "");
						curitem.Add(trimmed_clean);
					}

					if(trimmed_clean.StartsWith("ItemShopTags"))
					{
						item.ItemShopTags = trimmed_clean.Replace("ItemShopTags", "");
						curitem.Add(trimmed_clean);
					}

					if(trimmed_clean.StartsWith("ItemQuality"))
					{
						item.ItemQuality = trimmed_clean.Replace("ItemQuality", "");
						;
						curitem.Add(trimmed_clean);
					}

					if(trimmed_clean.StartsWith("ItemAliases"))
					{
						item.ItemAliases = trimmed_clean.Replace("ItemAliases", "");
						;
						curitem.Add(trimmed_clean);
					}

					if(trimmed_clean.StartsWith("ItemStackable"))
					{
						item.ItemStackable = trimmed_clean.Replace("ItemStackable", "");
						;
						curitem.Add(trimmed_clean);
					}

					if(trimmed_clean.StartsWith("ItemShareability"))
					{
						item.ItemShareability = trimmed_clean.Replace("ItemShareability", "");
						;
						curitem.Add(trimmed_clean);
					}

					if(trimmed_clean.StartsWith("ItemShareability"))
					{
						item.ItemShareability = trimmed_clean.Replace("ItemShareability", "");
						;
						curitem.Add(trimmed_clean);
					}

					//end current item, save to list
					if(trimmed_clean.StartsWith("//="))
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
