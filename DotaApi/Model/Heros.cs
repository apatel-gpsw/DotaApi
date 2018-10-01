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
					Name = StringManipulation.UppercaseFirst(hero.Name.Replace("npc_dota_hero_", "").Replace("_", " ")),
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
