using Castle.Windsor.Diagnostics;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using ImagePacker.Client.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImagePacker.Client.ViewModel
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        private IFileDialogProvider _fileDialogProvider;
        private PackProject _project;
        private bool projectLoaded;
        private PackProjectFile _selectedFile;

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

        public bool IsProjectLoaded { get; set; }

        public bool IsBusy { get; set; }

        public bool IsLoading { get; set; }

        public bool IsFileSelected { get; set; }

        public PackProjectFile SelectedFile
        { 
            get => _selectedFile;
            set 
            { 
                _selectedFile = value;
                new Task(async () =>
                {
                    await LoadFullResImage();
                }).Start();
                this.IsFileSelected = value != null;
            }
        }

        public ImageSource FullResolutionImage { get; set; }

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
            if (String.IsNullOrEmpty(Project.FileName))
                _fileDialogProvider.ShowSaveDialog("Save Project", "project files (*.proj)|*.proj", (f) => ProjectSerializer.Save(f, Project));
            else
                ProjectSerializer.Save(Project.FileName, Project);
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

        public async Task LoadFullResImage()
        {
            if(this.SelectedFile == null)
            {
                this.FullResolutionImage = null;
                return;
            }

            await Task.Run(() =>
            {
                this.IsLoading = true;
                this.FullResolutionImage = null;
                using (var fileStream = new FileStream(this.SelectedFile.ImageUrl, FileMode.Open, FileAccess.Read))
                {
                    BitmapImage bi = new BitmapImage();
                    try
                    {
                        bi.BeginInit();
                        bi.CacheOption = BitmapCacheOption.OnLoad;
                        bi.StreamSource = fileStream;
                        bi.EndInit();
                        bi.Freeze();
                        this.FullResolutionImage = bi;
                    }
                    finally
                    {
                        bi = null;
                    }
                }

                // CLR will use mem if available, the full res images are huge and will quickly addup
                // we force it to collect unused media in memory. There's no memleak, but it's not because resource
                // are present and GC "don't care" that we should use them without consideration
                GC.Collect();

                this.IsLoading = false;
            });
        }
    }
}
