
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OurToday
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SplashPage : ContentPage
	{
		public SplashPage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await logo.ScaleTo(1, 1500);
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }
	}
}