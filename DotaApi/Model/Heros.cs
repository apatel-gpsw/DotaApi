using System;
using System.Collections.Generic;
using DotaApi.Helpers;
using Newtonsoft.Json;

namespace DotaApi.Model
{
	public class Heroes
	{
		/// <summary>
		/// Gets the latest list of heroes from Steam, requires "HerosClass".
		/// </summary>
		public static List<Hero> GetHeroes(bool verbose)
		{
			string response = string.Empty;
			response = GetWebResponse.DownloadSteamAPIString(Common.herosUrl, Common.API);

			HeroesObject ourResponse = JsonConvert.DeserializeObject<HeroesObject>(response);

			//we clean up the names and stuff here.
			List<Hero> Heroes = new List<Hero>();

			//if verbose flag is set to true, then show console
			//message.
			if(verbose == true)
				Console.WriteLine("Count of Heroes {0}", ourResponse.Result.Heroes.Count);

			int herocountInt = 1;

			//hero id 0 is private profile i think?
			Hero Hero = new Hero();
			Hero.ID = 0;
			Hero.Name = "Npc_dota_hero_Private Profile";
			if(verbose == true)
			{ Console.WriteLine("Hero orig-name: {0}", Hero.Name); }
			Heroes.Add(Hero);

			foreach(var hero in ourResponse.Result.Heroes)
			{
				if(verbose == true)
					Console.Write("{0} of {1}. Hero orig-name: {2}|", herocountInt, ourResponse.Result.Heroes.Count, hero.Name);

				Hero = new Hero();
				Hero.Name = StringManipulation.UppercaseFirst(hero.Name.Replace("npc_dota_hero_", "").Replace("_", " "));
				Hero.ID = hero.ID;
				Hero.OrigName = hero.Name;
				if(verbose == true)
				{ Console.WriteLine(" cleaned: {0}", Hero.Name); }
				Heroes.Add(Hero);
				herocountInt++;
			}

			return Heroes;

		}

		public class Hero
		{
			public string Name { get; set; }
			public string OrigName { get; set; }
			public int ID { get; set; }
		}

		public class HeroesRoot
		{
			public List<Hero> Heroes { get; set; }
			public int Count { get; set; }
		}

		public class HeroesObject
		{
			public HeroesRoot Result { get; set; }
		}
	}
}
