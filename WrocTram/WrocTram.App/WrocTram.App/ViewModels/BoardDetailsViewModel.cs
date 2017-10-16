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

        private void ReloadBoards(string boardSymbol)
        {
            Lines = new ObservableCollection<LineData>(_dataProvider.GetLines(boardSymbol));
        }
    }
}
