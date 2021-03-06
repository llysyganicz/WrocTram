﻿using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using WrocTram.Lib;

namespace WrocTram.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly DataProvider _dataProvider;
        private readonly INavigationService _nav;
        public MainViewModel()
        {
            _dataProvider = SimpleIoc.Default.GetInstance<DataProvider>();
            _nav = SimpleIoc.Default.GetInstance<INavigationService>();
        }

        public bool IsBusy { get; set; }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                if (_searchText?.Length > 3) ReloadBoards();
                else Boards = null;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<BoardData> Boards { get; set; }

        private BoardData _selectedBoard;
        public BoardData SelectedBoard
        {
            get => _selectedBoard;
            set
            {
                _selectedBoard = value;
                if (_selectedBoard == null) return;
                _nav.NavigateTo("BoardDetails");
                Messenger.Default.Send(_selectedBoard.Symbol);
                _selectedBoard = null;
            }
        }

        private async void ReloadBoards()
        {
            IsBusy = true;
            var result = await _dataProvider.GetBoards(_searchText);
            Boards = new ObservableCollection<BoardData>(result);
            IsBusy = false;
        }
    }
}
