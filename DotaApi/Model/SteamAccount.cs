using System.Collections.Generic;
using DotaApi.Helpers;
using Newtonsoft.Json;

namespace DotaApi.Model
{
	/// <summary>
	/// SteamAccount user object that holds info like vanity name and avatar links.
	/// </summary>
	public class SteamAccount
	{
		/// <summary>
		/// Gets the Steam account details for a particular user ID, requires "DotaApi.Model.SteamAccount".
		/// </summary>
		public static Player GetSteamAccount(string SteamID)
		{
			Player player = new Player();

			if(string.IsNullOrEmpty(SteamID))
				return player;

			string webResponse = string.Empty;
			var steamaccount = new RootObject();
			webResponse = GetWebResponse.DownloadSteamAPIString(Common.steamaccountUrl, (Common.API + "&steamids=" + StringManipulation.SteamIDConverter(SteamID)));

			// webResponse = GetWebResponse.DownloadSteamAPIString(@"http://api.opendota.com/api/players/347142169", (Common.API + "&steamids=" + StringManipulation.SteamIDConverter(SteamID)));

			RootObject jObj = JsonConvert.DeserializeObject<RootObject>(webResponse);

			if(jObj.Response.Players.Count > 0)
				player = jObj.Response.Players[0];

			return player;
		}

		public class Player
		{
			public string SteamID { get; set; }
			public int CommunityVisibilityState { get; set; }
			public int ProfileState { get; set; }
			public string PlayerName { get; set; }
			public int LastLogOff { get; set; }
			public string ProfileUrl { get; set; }
			public string Avatar { get; set; }
			public string AvatarMedium { get; set; }
			public string AvatarFull { get; set; }
			public int CurrentStatus { get; set; }

		}

		public class Response
		{
			public List<Player> Players { get; set; }
		}

		public class RootObject
		{
			public Response Response { get; set; }
		}
	}
}
