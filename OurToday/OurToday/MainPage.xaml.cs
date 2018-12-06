using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using SQLite;

namespace OurToday
{
    public partial class MainPage : ContentPage
    {
        string DB_PATH = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "Diary.db");
        Button btn = new Button()
        {
            BackgroundColor = Color.FromHex("#86B7DC"),
            Text = "추가하기",
            TextColor = Color.FromHex("#ffffff"),
        };
        public ObservableCollection<Diary> diaryList;

        public MainPage()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            var db = new SQLiteConnection(DB_PATH);
            var table = db.Table<Diary>();
            diaryList = new ObservableCollection<Diary>(table.ToList());

            main_layout.Children.Clear();
            if (diaryList.Count == 0)
            {
                diaryListView.MinimumWidthRequest = 0;
                diaryListView.IsVisible = false;
                var image = new Image()
                {
                    Source = "icon.png",
                    WidthRequest = 100,
                };
                var label = new Label()
                {
                    Margin = 30,
                    Text = "아직 일기가 없어요! 일기를 추가해보세요!",
                    TextColor = Color.FromHex("#979797"),
                };
                main_layout.Children.Add(image);
                main_layout.Children.Add(label);
            }
            else
            {
                diaryListView.VerticalOptions = LayoutOptions.FillAndExpand;
                diaryListView.IsVisible = true;
                diaryListView.ItemsSource = diaryList;
            }
            btn.Clicked += WriteDiary;
            main_layout.Children.Add(btn);
        }

        private async void WriteDiary(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WritePage());
            LoadData();
        }

        private async void OnClickDiaryItem(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedDiary = (Diary) e.SelectedItem;
            await Navigation.PushAsync(new DiaryPage(selectedDiary.Title, selectedDiary.Content));
        }

        private void ClearDiary(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(DB_PATH);
            db.DeleteAll<Diary>();
            LoadData();
        }
    }
}
