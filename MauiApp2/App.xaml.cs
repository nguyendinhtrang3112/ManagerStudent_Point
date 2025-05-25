namespace MauiApp2
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; } = default!;
        public App()
        {
            InitializeComponent();

            // ⚡ Tắt dark mode, cố định theme sáng
            Application.Current!.UserAppTheme = AppTheme.Light;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}