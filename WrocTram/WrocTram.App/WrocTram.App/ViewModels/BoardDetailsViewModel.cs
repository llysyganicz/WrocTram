using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using WrocTram.Lib;
using GalaSoft.MvvmLight.Messaging;

namespace WrocTram.App.ViewModels
{
    public class BoardDetailsViewModel : ViewModelBase
    {
        private readonly DataProvider _dataProvider = SimpleIoc.Default.GetInstance<DataProvider>();

        public BoardDetailsViewModel()
        {
            Messenger.Default.Register<string>(this, ReloadBoards);
        }

        public ObservableCollection<LineData> Lines { get; set; }

        private async void ReloadBoards(string boardSymbol)
        {
            var result = await _dataProvider.GetLines(boardSymbol);
            Lines = new ObservableCollection<LineData>(result);
        }
    }
}
