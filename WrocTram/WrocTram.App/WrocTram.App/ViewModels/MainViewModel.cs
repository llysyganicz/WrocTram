using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using WrocTram.Lib;

namespace WrocTram.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly DataProvider _dataProvider;
        public MainViewModel()
        {
            _dataProvider = new DataProvider();
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                if (_searchText?.Length > 3) ReloadBoards();
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<BoardData> Boards { get; set; }

        private void ReloadBoards()
        {
            Boards = new ObservableCollection<BoardData>(_dataProvider.GetBoards(_searchText));
        }
    }
}
