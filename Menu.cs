using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Upscaler
{
	internal class Menu
	{
		public static void CreateMenu ()
		{
			Form1.GetMainForm().BackColor = Color.FromArgb(49,49,49);
			
			Vector2 image_vector = new Vector2(450,450);
			
			List<dynamic> elements = new List<dynamic>()
			{
				new Button()
				{
					Name = "imagem",
					Size = new Size((int)image_vector.X,(int)image_vector.Y),
					Location = new Point(600/2 - ((int)image_vector.X/2),600/2 - ((int)image_vector.Y/2)),
					FlatStyle = FlatStyle.Flat,
					ForeColor = Color.FromArgb(202, 62, 71),
					BackgroundImageLayout = ImageLayout.Center,
					BackgroundImage = Imagem.LoadImage("graphics/add.png"),
					Cursor = Cursors.Hand,
					//BorderStyle = BorderStyle.FixedSingle,
				},
				new TextBox()
				{
					Name = "largura",
					Size = new Size(60,50),
					Location = new Point(100,5),
					ForeColor = Color.White,
					BackColor = Color.FromArgb(202,62,71),
					Font = Fontes.ApplyFont(9),
					Visible = false,
				},
				new TextBox()
				{
					Name = "altura",
					Size = new Size(60,50),
					Location = new Point(100,30),
					ForeColor = Color.White,
					BackColor = Color.FromArgb(202,62,71),
					Font = Fontes.ApplyFont(9),
					Visible = false,
				},
				new TextBox()
				{
					Name = "multiplicador",
					Text = 1.ToString(),
					Size = new Size(30,50),
					Location = new Point(187,13),
					ForeColor = Color.White,
					BackColor = Color.FromArgb(202,62,71),
					Font = Fontes.ApplyFont(14),
					Visible = false,
				},
				new Button()
				{
					Name = "transformar",
					Text = "Redimensionar",
					Size = new Size(160,30),
					Location = new Point(300 - (160/2),550),
					FlatStyle = FlatStyle.Flat,
					ForeColor = Color.White,
					BackColor = Color.FromArgb(202,62,71),
					Cursor = Cursors.Hand,
					Font = Fontes.ApplyFont(11),
					Visible = false,
				},
				// Etiquetas
				new Label()
				{
					Name = "X tamanho",
					Text = "X =",
					Size = new Size(50,20),
					Location = new Point(60,5),
					ForeColor = Color.White,
					Font = Fontes.ApplyFont(11),
					TextAlign = ContentAlignment.MiddleCenter,
					Visible = false,
				},
				new Label()
				{
					Name = "Y tamanho",
					Text = "Y =",
					Size = new Size(50,20),
					Location = new Point(60,30),
					ForeColor = Color.White,
					Font = Fontes.ApplyFont(11),
					TextAlign = ContentAlignment.MiddleCenter,
					Visible = false,
				},
				new Label()
				{
					Name = "mult",
					Text = "*",
					Size = new Size(50,20),
					Location = new Point(150,17),
					ForeColor = Color.White,
					Font = Fontes.ApplyFont(18),
					TextAlign = ContentAlignment.MiddleCenter,
					Visible = false,
				},
			};

			SetMenuControls(elements);
			GUI.AddElementsToForm(elements, Form1.GetMainForm());
		}

		public static void SetMenuControls (List<dynamic> elems)
		{
			Button image = elems.ElementAt(0);
			TextBox largura = elems.ElementAt(1);
			TextBox altura = elems.ElementAt(2);
			TextBox mult = elems.ElementAt(3);
			Button b_redimensionar = elems.ElementAt(4);

			image.Click += (s,e) =>
			{
				// Pega o caminho até a imagem
				string path = Utis.CollectFile();

				if(string.IsNullOrEmpty(path))
				{
					return;
				}

				GUI.DisposeElementImage(image);
				image.BackgroundImage = Imagem.LoadImage(path);

				// Muda a forma de apresentação se a imagem for maior que o limite
				if(image.BackgroundImage.Width > image.Width
				|| image.BackgroundImage.Height > image.Height)
				{
					image.BackgroundImageLayout = ImageLayout.Zoom;
				}

				EnableHiddenControls(elems);

				// Põe as dimensões da imagem nas caixas de largura e altura
				largura.Text = image.BackgroundImage.Width.ToString();
				altura.Text = image.BackgroundImage.Height.ToString();

				//image.BackColor = Color.Transparent;
			};

			b_redimensionar.Click += (s,e) =>
			{
				Bitmap bmp = (Bitmap) image.BackgroundImage;

				int x = 0;
				int y = 0;
				int mult_num = 0;

				if(!int.TryParse(largura.Text, out x)
				|| !int.TryParse(altura.Text, out y)
				|| !int.TryParse(mult.Text, out mult_num))
				{
					return;
				}

				x *= mult_num;
				y *= mult_num;

				Vector2 v_size = new Vector2(x, y);

				Bitmap resized_bmp = Imagem.Resize(bmp, v_size);
				Imagem.SaveImage(resized_bmp, System.Drawing.Imaging.ImageFormat.Png);

				resized_bmp.Dispose();
			};
		}

		public static void EnableHiddenControls (List<dynamic> elems)
		{
			foreach(dynamic elem in elems)
			{
				elem.Visible = true;
			}
		}
		
		public static void Teste ()
		{
			//Bitmap bmp = Imagem.LoadImage("graphics/arvore.png");
			//Imagem.Resize(bmp, new Vector2(2048,2048), System.Drawing.Imaging.ImageFormat.Png);
			//bmp.Dispose();
		}
	}
}
