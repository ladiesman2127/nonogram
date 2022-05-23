
using nonogram_final_v2._0;

//using Windows.Media.Playback;

namespace nonogram_final
{
	internal static class Program
	{
		/// <summary>
		/// Главная точка входа для приложения.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//SoundPlayer sp = new SoundPlayer("Gemie - YouSeeBIGGIRL _ T_T .wav");
			//sp.Play();
			Application.Run(new StartForm());
		}
	}
}
