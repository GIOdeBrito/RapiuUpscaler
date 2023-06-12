namespace Upscaler
{
	public partial class Form1 : Form
	{
		private static Form forma;

		public Form1()
		{
			InitializeComponent();

			forma = this;
		}

		public static Form GetMainForm()
		{
			return forma;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			Menu.CreateMenu();
		}
	}
}