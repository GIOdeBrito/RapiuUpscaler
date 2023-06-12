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
	internal class Imagem
	{
		public static Bitmap LoadImage (string path)
		{
			if(!File.Exists(path))
			{
				throw new FileNotFoundException($"Arquivo {path} não pôde ser encontrado");
			}

			Bitmap img = null;

			using(FileStream stream = new FileStream(path, FileMode.Open))
			{
				try
				{
					Bitmap bmp = new Bitmap(stream);
					img = (Bitmap) Image.FromStream(stream);
					bmp.Dispose();
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			
			return img;
		}
		
		public static Bitmap Resize (Bitmap bmp, Vector2 neo_size = default(Vector2))
		{
			if(neo_size == default(Vector2))
			{
				neo_size.X = bmp.Width;
				neo_size.Y = bmp.Height;
			}
			
			Bitmap image = new Bitmap((int)neo_size.X, (int)neo_size.Y, PixelFormat.Format32bppArgb);

			using(Graphics graphics = Graphics.FromImage(image))
			{
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.DrawImage(bmp, new Rectangle(0,0, (int)neo_size.X,(int)neo_size.Y));
			}

			//image = SharpenImage(image);

			//bmp.Dispose();
			//image.Dispose();

			return image;
		}

		public static Bitmap SharpenImage (Bitmap bmp)
		{
			Bitmap image = new Bitmap(bmp.Width, bmp.Height);

			using (Graphics g = Graphics.FromImage(image))
			{
				g.DrawImage(image, 0, 0);
				g.Flush();

				using(ImageAttributes attributes = new ImageAttributes())
				{
					ColorMatrix colorMatrix = new ColorMatrix(
					new float[][]
					{
						new float[] { 0, -1, 0, 0, 0 },
						new float[] { -1, 5, -1, 0, 0 },
						new float[] { 0, -1, 0, 0, 0 },
						new float[] { 0, 0, 0, 1, 0 },
						new float[] { 0, 0, 0, 0, 1 }
					});

					attributes.SetColorMatrix(colorMatrix);

					using(Graphics sharpeningGraphics = Graphics.FromImage(image))
					{
						sharpeningGraphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
							0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
					}
				}
			}
			
			return image;
		}

		public static void SaveImage(Bitmap image, ImageFormat format = default(ImageFormat))
		{
			if(format == default(ImageFormat))
			{
				format = ImageFormat.Png;
			}

			image.Save($"saida/imagem.{format.ToString().ToLower()}", format);
		}
	}
}
