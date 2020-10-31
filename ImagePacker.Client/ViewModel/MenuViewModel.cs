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
        public ICommand OpenCommand { get; }
        public ICommand NewCommand { get; }
        public ICommand SaveCommand { get; }

        public MenuViewModel()
        {
            OpenCommand = new RelayCommand(new Action(OnOpen), true);
            NewCommand = new RelayCommand(new Action(OnNew), true);
            SaveCommand = new RelayCommand(new Action(OnSave), true);
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
