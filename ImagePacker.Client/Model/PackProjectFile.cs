using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ImagePacker.Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
        public ImageSource PreviewImage { get; set; }

        public PackProjectFile()
        {
            Keywords = new ObservableCollection<string>();
            AddKeyword = new RelayCommand(() => OnAddKeyword());
            DeleteKeyword = new RelayCommand<string>((k) => OnDeleteKeyword(k));
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
    }
}
