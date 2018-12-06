using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;

namespace OurToday
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DiaryPage : ContentPage
	{
        public static Action ac;
        string DB_PATH = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "Diary.db");

        private Diary _diary;

        public DiaryPage (Diary diary)
		{
			InitializeComponent ();
            _diary = diary;
            title_label.Text = diary.Title;
            content_label.Text = diary.Content;
		}

        private void OnDeleteBtnListener(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(DB_PATH);
            db.Delete(_diary);
            Application.Current.MainPage.Navigation.PopAsync();
            ac();
        }
    }
}