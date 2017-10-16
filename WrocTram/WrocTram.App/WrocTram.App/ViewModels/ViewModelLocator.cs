using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using WrocTram.Lib;

namespace WrocTram.App.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<BoardDetailsViewModel>();
            SimpleIoc.Default.Register<DataProvider>();
        }

        public MainViewModel MainViewModel => ServiceLocator.Current.GetInstance<MainViewModel>();

        public BoardDetailsViewModel BoardDetailsViewModel => ServiceLocator.Current
            .GetInstance<BoardDetailsViewModel>();
    }
}
