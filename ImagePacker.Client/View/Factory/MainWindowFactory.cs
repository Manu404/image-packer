using Castle.Windsor.Installer;
using ImagePacker.Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagePacker.Client.View.Factory
{
    public class MainWindowFactory : IMainWindowFactory
    {
        private readonly IMainViewModel _viewmodel;

        public MainWindowFactory(IMainViewModel viewmodel)
        {
            _viewmodel = viewmodel;
        }

        public IMainView Build()
        {
            return new MainWindow(_viewmodel);
        }
    }
}
