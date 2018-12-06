using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OurToday
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DiaryPage : ContentPage
	{
		public DiaryPage (string title, string content)
		{
			InitializeComponent ();
            title_label.Text = title;
            content_label.Text = content;
		}
	}
}