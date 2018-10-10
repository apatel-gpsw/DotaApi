using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static DotaApi.Model.MatchDetails;

namespace DotaApi
{
	public partial class DotaForm : Form
	{
		public DotaForm()
		{
			InitializeComponent();

			DataTable table = new DataTable();

			table.Columns.Add("Hero");
			table.Columns.Add("Player");
			table.Columns.Add("Kills");
			table.Columns.Add("Deaths");
			table.Columns.Add("Assists");
			table.Columns.Add("Net");
			table.Columns.Add("LH/DN");
			table.Columns.Add("GPM/XPM");
			table.Columns.Add("Damage");
			table.Columns.Add("Heal");

			MatchDetailsResult matchdetails = BuildMatchData(dgView1);
			DataRow row;

			foreach (var player in matchdetails.Players)
			{
				row = table.NewRow();//(DataGridViewRow)dgView1.Rows[0].Clone();

				row["Hero"] = player.Name;
				row["Player"] = player.PlayerName;
				row["Kills"] = player.Kills;
				row["Deaths"] = player.Deaths;
				row["Assists"] = player.Assists;
				row["Net"] = player.Gold;
				row["LH/DN"] = $"{player.Last_Hits}/{player.Denies}";
				row["GPM/XPM"] = $"{player.Gold_Per_Min}/{player.Xp_Per_Min}";
				row["Damage"] = player.Hero_Damage;
				row["Heal"] = player.Hero_Healing;

				table.Rows.Add(row);
			}

			row = table.NewRow();
			row[4] = "The";
			row[5] = "Radiant";
			table.Rows.InsertAt(row, 0);

			row = table.NewRow();
			row[4] = "The";
			row[5] = "Dire";
			table.Rows.InsertAt(row, 6);

			dgView1.DataSource = table;

			dgView1.Rows[0].DefaultCellStyle.BackColor = Color.Green;
			dgView1.Rows[5].DefaultCellStyle.BackColor = Color.Red;

			//int i = 0;
			//foreach (DataGridViewRow curr in dgView1.Rows)
			//{

			//	if (i % 5 == 0)
			//	{
			//		curr.DefaultCellStyle.BackColor = Color.Red;
			//	}
			//	i++;
			//}

			//for (int i = 0; i < 5; i++)
			//{
			//	foreach (DataGridViewCell cell in dgView1.Rows[i].Cells)
			//		cell.Style.ForeColor = Color.Green;
			//}

			//for (int i = 5; i < 10; i++)
			//{
			//	dgView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
			//}
		}

		public static MatchDetailsResult BuildMatchData(DataGridView dg)
		{
			//Get match details for match id 1277955116.
			MatchDetailsResult matchdetails = GetMatchDetail(/*1277955116*/4142945482);
			// 1277955116
			// 3049649968

			return matchdetails;

			//var nextmatches = MatchHistory.GetMatchHistoryBySeqNum(matchdetails.Match_Seq_Num, 25);

			//Gets details regarding a steam account using
			//account id.
			//string SteamID = "111348541";
			//var steamaccount = SteamAccount.GetSteamAccount(SteamID);

			//get latest 100 matches with brief details (no hero items/abilities/build info).
			//List<MatchDetailsResult> latest100matches = MatchHistory.GetMatchHistory();
		}
	}
}
