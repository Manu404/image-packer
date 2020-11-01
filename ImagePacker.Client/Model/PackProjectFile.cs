using Castle.Windsor.Diagnostics;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ImagePacker.Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.IO.Packaging;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Xml.Serialization;

namespace ImagePacker.Client.Model
{
    public class PackProjectFile : ViewModelBase
    {
        public string ImageUrl { get; set; }
        public ObservableCollection<string> Keywords { get; set; }

        [XmlIgnore]
        public string KeywordInput { get; set; }

        [XmlIgnore]
        public ICommand AddKeyword { get; set; }

        [XmlIgnore]
        public ICommand DeleteKeyword { get; set; }

        [XmlIgnore]
        public ICommand LocateMissingImage { get; set; }

        [XmlIgnore]
        public ImageSource PreviewImage { get; set; }

        [XmlIgnore]
        public bool MarkedForSuppresion { get; set; }

        [XmlIgnore]
        public bool MissingImage { get; set; }

        [XmlIgnore]
        public IFileDialogProvider DialogProvider { get; set; }

        public PackProjectFile()
        {
            Keywords = new ObservableCollection<string>();
            AddKeyword = new RelayCommand(() => OnAddKeyword());
            DeleteKeyword = new RelayCommand<string>((k) => OnDeleteKeyword(k));
            LocateMissingImage = new RelayCommand(() => OnLocateMissingImage(), () => this.MissingImage == true);
        }

        private void OnLocateMissingImage()
        {
            DialogProvider.ShowLoadDialog("Locate missing file", "image files (*.jpg)|*.jpg", (f) => UpdateFileLocation(f));
        }

        private void UpdateFileLocation(string file)
        {
            this.ImageUrl = file;
            this.MissingImage = false;
            new Task(async () =>
            {
                await Load();
            }).Start();
        }

        private void OnAddKeyword()
        {
            if (!this.Keywords.Contains(KeywordInput))
                this.Keywords.Add(KeywordInput);
            KeywordInput = String.Empty;
        }

        private void OnDeleteKeyword(string keyword)
        {
            if (!this.Keywords.Contains(keyword)) return;
            this.Keywords.Remove(keyword);
        }

        public async Task Load()
        {
            if (PreviewImage != null) return;
            await Task.Run(() =>
            {
                if (this.MissingImage) return;

                if (!File.Exists(ImageUrl))
                {
                    HandleMissingImage();
                    return;
                }

                using (var fileStream = new FileStream(ImageUrl, FileMode.Open, FileAccess.Read))
                {
                    BitmapImage bi = new BitmapImage();
                    try
                    {
                        bi.BeginInit();
                        bi.CacheOption = BitmapCacheOption.OnLoad;
                        bi.StreamSource = fileStream;
                        bi.DecodePixelWidth = 400;
                        bi.EndInit();
                        bi.Freeze();
                        this.PreviewImage = bi;
                    }
                    finally
                    {
                        bi = null;
                    }                    
                }
            });
        }

        private void HandleMissingImage()
        {
            this.MissingImage = true;

            MessageBoxResult locateMessageBox = MessageBox.Show($"The file {ImageUrl} is missing, do you want to locate the file ?", "File missing", MessageBoxButton.YesNo);
            if (locateMessageBox == MessageBoxResult.Yes)
            {
                this.OnLocateMissingImage();
            }
            else
            {
                MessageBoxResult keepMessageBox = MessageBox.Show($"The file {ImageUrl} is missing, do you want to keep it in the project ?", "File missing", MessageBoxButton.YesNo);
                if (keepMessageBox == MessageBoxResult.No)
                {
                    this.MarkedForSuppresion = true;
                }
            }
        }
    }
}
