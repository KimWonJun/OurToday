using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using SQLite;
using System.Threading.Tasks;

namespace OurToday
{
    public partial class MainPage : ContentPage
    {
        string DB_PATH = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "Diary.db");
        Image image = new Image()
        {
            Source = "icon.png",
            WidthRequest = 100,
        };
        Label label = new Label()
        {
            Margin = 30,
            Text = "아직 일기가 없어요! 일기를 추가해보세요!",
            TextColor = Color.FromHex("#979797"),
        };

        public ObservableCollection<Diary> diaryList;

        public MainPage()
        {
            InitializeComponent();
            LoadData();
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
                // TODO: List C#으로 구현, ItemSource에 넣기
                main_layout.Children.Remove(image);
                main_layout.Children.Remove(label);
                diaryListView.ItemsSource = diaryList;
                diaryListView.IsVisible = true;
            }
        }

        private void WriteDiary(object sender, EventArgs e)
        {
            WritePage.ac = LoadData;
            Navigation.PushAsync(new WritePage());
            LoadData();
        }

        private void OnClickDiaryItem(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedDiary = (Diary) e.SelectedItem;
            Navigation.PushAsync(new DiaryPage(selectedDiary.Title, selectedDiary.Content));
        }

        private void ClearDiary(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(DB_PATH);
            db.DeleteAll<Diary>();
            LoadData();
        }
    }
}
