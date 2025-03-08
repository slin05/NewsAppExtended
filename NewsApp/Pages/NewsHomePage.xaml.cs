using NewsApp.Models;
using NewsApp.Services;

namespace NewsApp.Pages;

public partial class NewsHomePage : ContentPage
{
    public List<Article> ArticleList;
    public List<Category> CategoryList = new List<Category>()
    {
        new Category(){Name="World", ImageUrl = "world.png"},
        new Category(){Name = "Nation" , ImageUrl="nation.png"},
        new Category(){Name = "Business" , ImageUrl="business.png"},
        new Category(){Name = "Technology" , ImageUrl="technology.png"},
        new Category(){Name = "Entertainment", ImageUrl = "entertainment.png"},
        new Category(){Name = "Sports" , ImageUrl="sports.png"},
        new Category(){Name = "Science", ImageUrl= "science.png"},
        new Category(){Name = "Health", ImageUrl="health.png"},
    };

    public NewsHomePage()
    {
        InitializeComponent();
        ArticleList = new List<Article>();
        CvCategories.ItemsSource = CategoryList;

        // Load data when page appears
        this.Appearing += OnPageAppearing;
    }

    private void OnPageAppearing(object? sender, EventArgs e)
    {
        // Load breaking news when page appears
        LoadBreakingNews();
    }

    private async void LoadBreakingNews()
    {
        await GetBreakingNews();
    }

    private async Task GetBreakingNews()
    {
        try
        {
            var apiService = new ApiService();
            var newsResult = await apiService.GetNews("Sports");
            ArticleList = new List<Article>(newsResult.Articles);
            CvNews.ItemsSource = ArticleList;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load news: {ex.Message}", "OK");
        }
    }

    private async void OnCategorySelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Category selectedCategory)
        {
            // Clear selection
            ((CollectionView)sender).SelectedItem = null;

            // Navigate to news list page with the selected category
            await Navigation.PushAsync(new NewsListPage(selectedCategory.Name));
        }
    }
}