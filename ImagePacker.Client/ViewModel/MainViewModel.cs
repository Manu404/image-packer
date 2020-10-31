using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagePacker.Client.ViewModel
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        public IMenuViewModel MenuViewModel { get; }

        public MainViewModel(IMenuViewModel menuViewModel)
        {
            MenuViewModel = menuViewModel;
        }
    }
}
