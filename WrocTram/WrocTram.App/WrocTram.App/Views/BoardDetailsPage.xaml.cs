using Xamarin.Forms.Xaml;

namespace WrocTram.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BoardDetailsPage
    {
        public BoardDetailsPage()
        {
            InitializeComponent();
            BindingContext = App.Locator.BoardDetailsViewModel;
        }
    }
}