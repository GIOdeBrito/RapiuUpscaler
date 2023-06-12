using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upscaler
{
	internal class GUI
	{
		public static void AddElementsToForm (List<dynamic> elems, Form forma)
		{
			foreach(dynamic elem in elems)
			{
				forma.Controls.Add(elem);
			}
		}

		public static void DisposeElementImage (Control _control)
		{
			if(_control == null)
			{
				return;
			}

			if(_control.BackgroundImage != null)
			{
				_control.BackgroundImage.Dispose();
			}
		}
	}
}
