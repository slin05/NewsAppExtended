using NewsApp.Models;
using NewsApp.Services;
using System.Collections.ObjectModel;
namespace NewsApp.Pages
{
    public partial class NewsListPage : ContentPage
    {
        private readonly ApiService _apiService;
        private ObservableCollection<Article> _articleList;
        public NewsListPage(string categoryName)
        {
            InitializeComponent();
            Title = categoryName; // Set page title to the category name
            _apiService = new ApiService();
            _articleList = new ObservableCollection<Article>();
            CvNewsList.ItemsSource = _articleList;
            LoadNewsAsync(categoryName);
        }
        private async void LoadNewsAsync(string categoryName)
        {
            try
            {
                var newsResult = await _apiService.GetNews(categoryName);
                foreach (var article in newsResult.Articles)
                {
                    _articleList.Add(article);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load news: {ex.Message}", "OK");
            }
        }

        // Keep the original selection handler
        private async void OnNewsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Article selectedArticle)
            {
                // Clear selection
                ((CollectionView)sender).SelectedItem = null;
                // Navigate to details page with the selected article
                await Navigation.PushAsync(new NewsDetailPage(selectedArticle));
            }
        }

        // Add a button click handler as a more reliable method for Android
        private async void OnNewsButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Article selectedArticle)
            {
                // Navigate to details page with the selected article
                await Navigation.PushAsync(new NewsDetailPage(selectedArticle));
            }
        }
    }
}