namespace DotaApi.Model
{
	public class Item : ISearchableDictionary
	{
		public string Name { get; set; }
		public int ID { get; set; }
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
}
