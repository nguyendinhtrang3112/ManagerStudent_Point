namespace MauiApp2
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Điều hướng đến trang đăng nhập khi khởi chạy
            GoToAsync("//LoginPage");
        }
    }

}
