using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upscaler
{
	internal class Fontes
	{
		private static PrivateFontCollection fontes = new PrivateFontCollection();

		static Fontes ()
		{
			LoadFonts();
		}

		public static Font ApplyFont (int size = 11)
		{
			return new Font(fontes.Families[0], size);
		}

		public static void LoadFonts ()
		{
			string[] path =
			{
				@"acephimere.otf",
			};

			foreach(string f in path)
			{
				fontes.AddFontFile(f);
			}
		}
	}
}
