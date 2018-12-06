using System;
using SQLite;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OurToday
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WritePage : ContentPage
	{
        static string DB_PATH = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "Diary.db");
        public static Action ac;
        public WritePage ()
		{
			InitializeComponent ();
		}

        private void OnClickListener(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(DB_PATH);
            db.CreateTable<Diary>();

            if (title_edit.Text.Length > 0 && title_edit.Text.Length <= 100)
            {
                var newDiary = new Diary(title_edit.Text, content_edit.Text);
                db.Insert(newDiary);
                ac();
                Application.Current.MainPage.Navigation.PopAsync();
            }
        }
    }
}