using System.Windows.Forms;

namespace DotaApi
{
	static class Program
	{
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new DotaForm());
		}
	}
}
