using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using DotaApi.Helpers;
using DotaApi.Model;
using static DotaApi.Helpers.Lookups;

namespace DotaApi
{
	public partial class DotaForm : Form
	{
		public DotaForm()
		{
			InitializeComponent();
			dgView1.ColumnCount = 3;
			dgView1.Columns[0].Name = "Product ID";
			dgView1.Columns[1].Name = "Product Name";
			dgView1.Columns[2].Name = "Product Price";

			string[] row = new string[] { "1", "Product 1", "1000" };
			dgView1.Rows.Add(row);
			row = new string[] { "2", "Product 2", "2000" };
			dgView1.Rows.Add(row);
			row = new string[] { "3", "Product 3", "3000" };
			dgView1.Rows.Add(row);
			row = new string[] { "4", "Product 4", "4000" };
			dgView1.Rows.Add(row);
			BuildMatchData();
		}

		public static void BuildMatchData()
		{
			//parsing abilities list and storing
			//in memory.
			//var abilitiestext = File.ReadAllLines(Common.abilities_txt_path);
			//var abilities = AbilitiesClass.ParseAbilityText(abilitiestext);

			//lets parse items.txt and store this data into
			//memory so we can make the association
			//for match details.
			// string[] itemstext = File.ReadAllLines(ITEM_FILE);

			List<Item> items = Common.GetGameItems(false);
			//Common.ParseItemsText(itemstext);

			// http://api.steampowered.com/IEconDOTA2_570/GetGameItems/v0001/?key=<api_key>&language=en

			//Get match details for match id 1277955116.
			MatchDetails.MatchDetailsResult matchdetails = MatchDetails.GetMatchDetail(/*1277955116*/4142945482, items);
			// 1277955116
			// 3049649968

			var nextmatches = MatchHistory.GetMatchHistoryBySeqNum(matchdetails.Match_Seq_Num, 25, items);

			//Gets details regarding a steam account using
			//account id.
			string SteamID = "111348541";
			var steamaccount = SteamAccount.GetSteamAccount(SteamID);

			//Get list of latest heroes with names parsed/cleaned.
			List<Heroes.Hero> heros = Common.GetHeroes(true);

			//get latest 100 matches with brief details (no hero items/abilities/build info).
			List<Match> latest100matches = MatchHistory.GetMatchHistory();
		}
	}
}
