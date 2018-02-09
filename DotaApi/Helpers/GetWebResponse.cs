using System;
using System.Net;

namespace DotaApi.Model
{
	public class GetWebResponse
	{
		/// <summary>Customized to download from Steam using format of Uri + api.
		/// <seealso cref="http://uglyvpn.com/"/>
		/// </summary>
		public static string DownloadSteamAPIString(string uri, string api)
		{
			var response = string.Empty;
			//I'm reading up on how to stylize code, I think
			//this below method of creating a Uri is preferred
			//over the string.Format that I originally used
			Uri getmatchUri = new Uri(uri + api);

			//client used to download the json response
			using(WebClient client = new WebClient())
			{
				//downloading the json response
				response = client.DownloadString(getmatchUri);
			}
			return response;
		}
	}
}
