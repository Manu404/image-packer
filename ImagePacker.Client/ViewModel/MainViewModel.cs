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
        private IFileDialogProvider _fileDialogProvider;
        private PackProject _project;

        public IMenuViewModel MenuViewModel { get; }

        public PackProject Project
        {
            get => _project;
            set
            {
                _project = value;
                RaisePropertyChanged();
            }
        }

        public bool IsBusy { get; private set; }

        public MainViewModel(IMenuViewModel menuViewModel, IFileDialogProvider fileDialogProvider)
        {
            MenuViewModel = menuViewModel;
            _fileDialogProvider = fileDialogProvider;
            Initialize();
        }

        private void Initialize()
        {
            MenuViewModel.SetMainViewModel(this);
        }

        public void NewProject()
        {
            Project = new PackProject()
            {
                Name = "New project",
                Revision = 0
            };
        }

        public void Exit()
        {
            Application.Current.Shutdown();
        }

        public void AddFiles()
        {
            if (Project == null) return;
            _fileDialogProvider.ShowLoadMultipleDialog("Load image files", "image files (*.jpg)|*.jpg", (f) => f.ToList().ForEach(Project.AddFile));
            RaisePropertyChanged("Project");
            Project.LoadImages();
        }
    }
}
