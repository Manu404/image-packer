using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using ImagePacker.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ImagePacker.Client.ViewModel
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        public IMenuViewModel MenuViewModel { get; }

        public PackProject Project { get; set; }

        public bool IsBusy { get; private set; }

        public MainViewModel(IMenuViewModel menuViewModel)
        {
            MenuViewModel = menuViewModel;

            Initialize();
        }

        private void Initialize()
        {
            MenuViewModel.SetMainViewModel(this);

            /*Project = new PackProject()
            {
                Name = "Test",
                Revision = "abc"
            };*/
        }

        public void Exit()
        {

        }

        public void AddFiles()
        {
            MessageBox.Show("ok");
        }
    }
}
