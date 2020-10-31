using ImagePacker.Client.View.Factory;
using ImagePacker.Client.ViewModel;
using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ImagePacker.Client
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread()]
        public static void Main()
        {
            var boot = Bootstrapper.GetDefaultContainer();
            var app = boot.Resolve<IMainWindowFactory>().Build();
            app.ShowDialog();
        }
    }
}
