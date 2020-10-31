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
            var app = new App();
            app.InitializeComponent();
            app.Start();
        }

        void Start()
        {
            var boot = Bootstrapper.GetDefaultContainer();
            var window = boot.Resolve<IMainWindowFactory>().Build();
            window.ShowDialog();
        }
    }
}
