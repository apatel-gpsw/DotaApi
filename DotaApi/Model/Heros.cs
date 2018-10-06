using System;
using System.Collections.Generic;
using System.Globalization;
using DotaApi.Helpers;
using Newtonsoft.Json;

namespace DotaApi.Model
{
	public class Heroes
	{
		public class Hero : ISearchableDictionary
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
