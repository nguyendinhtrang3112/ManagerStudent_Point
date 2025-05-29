using MauiApp2.ViewModels;

namespace MauiApp2;

public partial class StudentPage : ContentPage
{
    private readonly StudentViewModel viewModel;

    public StudentPage()
    {
        InitializeComponent();
        viewModel = new StudentViewModel();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.LoadStudentsAsync(); // Gọi hàm tải dữ liệu tại đúng thời điểm
    }
    private async void OnBackToLoginClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//LoginPage"); // hoặc tùy route bạn định nghĩa
    }
}
