using NewsApp.Models;

namespace NewsApp.Pages
{
    public partial class NewsDetailPage : ContentPage
    {
        private readonly Article _article;

        public NewsDetailPage(Article article)
        {
            InitializeComponent();
            _article = article;

            // Populate UI with article details
            ArticleImage.Source = article.Image;
            TitleLabel.Text = article.Title;
            SourceLabel.Text = $"Source: {article.Source.Name}";
            PublishedDateLabel.Text = $"Published: {article.PublishedAt.ToString("MMM dd, yyyy - HH:mm")}";
            ContentLabel.Text = article.Content;
        }

        private async void OnReadMoreClicked(object sender, EventArgs e)
        {
            try
            {
                // Open the article URL in browser
                await Launcher.OpenAsync(new Uri(_article.Url));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Could not open the article: {ex.Message}", "OK");
            }
        }
    }
}