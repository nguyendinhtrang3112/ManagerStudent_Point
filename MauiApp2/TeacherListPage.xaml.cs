namespace MauiApp2;

public partial class TeacherListPage : ContentPage
{
    public TeacherListPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ViewModels.TeacherListViewModel vm)
            await vm.LoadTeachersCommand.ExecuteAsync(null);
    }

    private async void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
    {
        if (BindingContext is ViewModels.TeacherListViewModel vm)
            await vm.LoadTeachersCommand.ExecuteAsync(null);
    }
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}
