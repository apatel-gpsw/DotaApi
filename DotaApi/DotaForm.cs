using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using DotaApi.Helpers;
using DotaApi.Model;

namespace DotaApi
{
	public partial class DotaForm : Form
	{
		public DotaForm()
		{
			InitializeComponent();
			dataGridView1.ColumnCount = 3;
			dataGridView1.Columns[0].Name = "Product ID";
			dataGridView1.Columns[1].Name = "Product Name";
			dataGridView1.Columns[2].Name = "Product Price";

			string[] row = new string[] { "1", "Product 1", "1000" };
			dataGridView1.Rows.Add(row);
			row = new string[] { "2", "Product 2", "2000" };
			dataGridView1.Rows.Add(row);
			row = new string[] { "3", "Product 3", "3000" };
			dataGridView1.Rows.Add(row);
			row = new string[] { "4", "Product 4", "4000" };
			dataGridView1.Rows.Add(row);
		}

		public static void BuildMatchData()
		{
			//parsing abilites list and storing
			//in memory.
			//var abilitiestext = File.ReadAllLines(Common.abilities_txt_path);
			//var abilities = AbilitiesClass.ParseAbilityText(abilitiestext);

			//lets parse items.txt and store this data into
			//memory so we can make the associatation
			//for match details.
			string[] itemstext = File.ReadAllLines(Common.items_txt_path);
			List<ItemsClass.Item> items = ItemsClass.ParseItemsText(itemstext);

			//Get match details for match id 1277955116.
			MatchDetails.MatchDetailsResult matchdetails = MatchDetails.GetMatchDetail(1277955116, items);


			var nextmatches = MatchHistory.GetMatchHistoryBySeqNum(matchdetails.match_seq_num, 25, items);



			//Gets details regarding a steam account using
			//account id.
			string SteamID = "111348541";
			var steamaccount = SteamAccount.GetSteamAccount(SteamID);

			//Get list of latest heroes with names parsed/cleaned.
			List<Heroes.Hero> heros = Heroes.GetHeroes(true);

			//get latest 100 matches with brief details (no hero items/abilities/build info).
			List<Match> latest100matches = MatchHistory.GetMatchHistory();
		}
	}
}
