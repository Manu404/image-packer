using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
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
        public ICommand SaveAsCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand AddFilesCommand { get; set; }
        public ICommand PackCommand { get; set; }

        private IMainViewModel _mainViewModel { get; set; }

        public MenuViewModel()
        {
            OpenCommand = new RelayCommand(() => OnOpen(), () => _mainViewModel?.IsBusy == false);
            NewCommand = new RelayCommand(() => OnNew(), () => _mainViewModel?.IsBusy == false);
            ExitCommand = new RelayCommand(() => OnExit(), () => _mainViewModel?.IsBusy == false);
            SaveCommand = new RelayCommand(() => OnSave(), () => _mainViewModel?.IsProjectLoaded == true);
            SaveAsCommand = new RelayCommand(() => OnSaveAs(), () => _mainViewModel?.IsProjectLoaded == true);
            AddFilesCommand = new RelayCommand(() => AddFiles(), () => _mainViewModel?.IsProjectLoaded == true);
            PackCommand = new RelayCommand(() => OnPack(), () => _mainViewModel?.IsProjectLoaded == true);
        }

        public void InjectMainViewModel(IMainViewModel viewModel)
        {
            _mainViewModel = viewModel;
        }

        private void OnPack()
        {

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
            _mainViewModel?.SaveProject();
        }

        private void OnSaveAs()
        {
            _mainViewModel?.SaveProject(true);
        }

        private void OnNew()
        {
            _mainViewModel?.NewProject();
        }

        private void OnOpen()
        {
            _mainViewModel?.LoadProject();
        }
    }
}
