using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using SQLite;

namespace OurToday
{
    public partial class MainPage : ContentPage
    {
        string DB_PATH = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "Diary.db");
        Image image = new Image()
        {
            Margin = 30,
            Source = "icon.png",
            WidthRequest = 100,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
        };
        Label label = new Label()
        {
            Margin = 10,
            Text = "아직 일기가 없어요! 일기를 추가해보세요!",
            TextColor = Color.FromHex("#979797"),
            HorizontalOptions = LayoutOptions.Center,
        };

        public ObservableCollection<Diary> diaryList;

        public MainPage()
        {
            InitializeComponent();
            LoadData();
            WritePage.ac = LoadData;
            DiaryPage.ac = LoadData;
        }

        /*
         * 일기가 없을 경우, Image/Label/Button
         * 일기가 있을 경우, List/Button
         */
        public void LoadData()
        {
            var db = new SQLiteConnection(DB_PATH);
            var table = db.Table<Diary>();
            diaryList = new ObservableCollection<Diary>(table.ToList());
            
            if (diaryList.Count == 0)
            {
                diaryListView.IsVisible = false;
                var btn = add_btn;
                main_layout.Children.Remove(btn);
                main_layout.Children.Add(image);
                main_layout.Children.Add(label);
                main_layout.Children.Add(btn);
            }
            else
            {
                main_layout.Children.Remove(image);
                main_layout.Children.Remove(label);
                diaryListView.ItemsSource = diaryList;
                diaryListView.IsVisible = true;
            }
        }

        private void WriteDiary(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WritePage());
            LoadData();
        }

        private async void OnClickDiaryItem(object sender, SelectedItemChangedEventArgs e)
        {
            if ((sender as ListView).SelectedItem == null) return;
            await Navigation.PushAsync(new DiaryPage(e.SelectedItem as Diary));
            (sender as ListView).SelectedItem = null;
        }

        private void ClearDiary(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(DB_PATH);
            db.DeleteAll<Diary>();
            LoadData();
        }
    }
}
