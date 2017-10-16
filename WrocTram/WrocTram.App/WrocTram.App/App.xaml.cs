using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using WrocTram.App.ViewModels;
using WrocTram.App.Views;
using Xamarin.Forms;

namespace WrocTram.App
{
    public partial class App
    {
        private static ViewModelLocator _locator;
        public static ViewModelLocator Locator => _locator ?? (_locator = new ViewModelLocator());

        public App()
        {
            InitializeComponent();

            var navigationService = new NavigationService();
            navigationService.Configure("Main", typeof(MainPage));
            navigationService.Configure("BoardDetails", typeof(BoardDetailsPage));
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);

            var navPage = new NavigationPage(new MainPage());

            MainPage = navPage;

            navigationService.Initialize(navPage);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
