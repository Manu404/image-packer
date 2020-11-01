using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using ImagePacker.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ImagePacker.Client.ViewModel
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        private IFileDialogProvider _fileDialogProvider;
        private PackProject _project;
        private bool projectLoaded;

        public IMenuViewModel MenuViewModel { get; }

        public ICommand WindowClosingCommand { get; set; }

        public PackProject Project
        {
            get => _project;
            set
            {
                _project = value;
                RaisePropertyChanged();
                IsProjectLoaded = value != null;
            }
        }

        public bool IsProjectLoaded 
        { 
            get => projectLoaded;                
            set 
            { 
                projectLoaded = value;
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
            IsProjectLoaded = false;
            IsBusy = false;

            WindowClosingCommand = new RelayCommand(() => SaveOnExit());
        }

        private void SaveOnExit()
        {
            if (Project != null && !String.IsNullOrEmpty(Project.FileName)) ProjectSerializer.Save(Project.FileName, Project);
            else if (Project != null) SaveProject();
        }

        public void NewProject()
        {
            Project = new PackProject()
            {
                Name = "New project",
                Revision = 0
            };
        }

        public void SaveProject()
        {
            if (Project == null) return;
            _fileDialogProvider.ShowSaveDialog("Save Project", "project files (*.proj)|*.proj", (f) => ProjectSerializer.Save(f, Project));
        }

        public void LoadProject()
        {
            if (Project != null) SaveProject();
            _fileDialogProvider.ShowLoadDialog("Load Project", "project files (*.proj)|*.proj", (f) => Project = ProjectSerializer.Load(f));
            Project?.LoadImages();
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
