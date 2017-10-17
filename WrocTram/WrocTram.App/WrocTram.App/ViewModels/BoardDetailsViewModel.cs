using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using WrocTram.Lib;
using GalaSoft.MvvmLight.Messaging;

namespace WrocTram.App.ViewModels
{
    public class BoardDetailsViewModel : ViewModelBase
    {
        private readonly DataProvider _dataProvider = SimpleIoc.Default.GetInstance<DataProvider>();
        private string _boardSymbol;

        public BoardDetailsViewModel()
        {
            Messenger.Default.Register<string>(this, ReloadBoards);
        }

        public ObservableCollection<LineData> Lines { get; set; }

        public bool IsBusy { get; set; }

        private async void ReloadBoards(string boardSymbol)
        {
            IsBusy = true;
            _boardSymbol = boardSymbol;
            var result = await _dataProvider.GetLines(boardSymbol);
            Lines = new ObservableCollection<LineData>(result);
            IsBusy = false;
        }

        private RelayCommand _refresh;
        public RelayCommand Refresh => _refresh ?? (_refresh = new RelayCommand(() => { ReloadBoards(_boardSymbol); }));
    }
}
