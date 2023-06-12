using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Upscaler
{
	internal class Utis
	{
		public static string CollectFile ()
		{
			string path = string.Empty;

			Func<string> define_filter = () =>
			{
				string filtro = "(*.jpg);(*.png);(*.tiff)|*.jpg;*.png;*.tiff";
				return $"{filtro}|Todos (*.*)|*.*";
			};
			
			using(OpenFileDialog explorador = new OpenFileDialog()
			{
				InitialDirectory = @"C:\",
				RestoreDirectory = true,
				Title = "Selecionar imagem",
				//DefaultExt = "exe",
				Filter = define_filter(),
			})
			{
				DialogResult res = explorador.ShowDialog();

				if(res == DialogResult.Cancel)
				{
					Console.WriteLine("Sem arquivo");
					return null;
				}

				path = explorador.FileName;
				explorador.Dispose();
			}

			return path;
		}
	}
}
