using System;
using System.Net;

namespace DotaApi.Model
{
	public class GetWebResponse
	{
		/// <summary>
		/// Customized to download from Steam using format of Uri + api.
		/// </summary>
		public static string DownloadSteamAPIString(string uri, string api)
		{
			var response = string.Empty;
			Uri getmatchUri = new Uri(uri + api);

			// client used to download the json response
			using(WebClient client = new WebClient())
			{
				// downloading the json response
				response = client.DownloadString(getmatchUri);
			}
			return response;
		}
	}
}
