using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ImagePacker.Client.ViewModel
{
    public class MenuViewModel : ViewModelBase, IMenuViewModel
    {
        public ICommand OpenCommand { get; set; }
        public ICommand NewCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand AddFilesCommand { get; set; }

        private IMainViewModel _mainViewModel;

        public MenuViewModel()
        {
            OpenCommand = new RelayCommand(() => OnOpen());
            NewCommand = new RelayCommand(() => OnNew());
            ExitCommand = new RelayCommand(() => OnExit(), () => _mainViewModel?.IsBusy == false);
            SaveCommand = new RelayCommand(() => OnSave(), () => _mainViewModel?.Project != null);
            AddFilesCommand = new RelayCommand(() => AddFiles(), () => _mainViewModel?.Project != null);
        }

        public void SetMainViewModel(IMainViewModel viewModel)
        {
            _mainViewModel = viewModel;
        }

        private void OnExit()
        {
            _mainViewModel?.Exit();
        }

        private void AddFiles()
        {
            _mainViewModel?.AddFiles();
        }

        private void OnSave()
        {

        }

        private void OnNew()
        {

        }

        private void OnOpen()
        {

        }
    }
}
