using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace ImagePacker.Client.Model
{
    public class PackProjectFile : ViewModelBase
    {
        private ImageSource _image;
        private string _keywordInput;

        public string ImageUrl { get; set; }
        public ObservableCollection<string> Keywords { get; set; }

        public string KeywordInput
        {
            get => _keywordInput; 
            set 
            { 
                _keywordInput = value;
                RaisePropertyChanged();
            }
        }

        [XmlIgnore]
        public ICommand AddKeyword { get; set; }

        [XmlIgnore]
        public ICommand DeleteKeyword { get; set; }

        [XmlIgnore]
        public ImageSource Image
        {
            get { return _image; }
            set
            {
                _image = value;
                RaisePropertyChanged();
            }
        }

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
            if (Image != null) return;
            Image = await Task.Run(() =>
            {
                using (var fileStream = new FileStream(ImageUrl, FileMode.Open, FileAccess.Read))
                {
                    return BitmapFrame.Create(fileStream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                }
            });
        }
    }
}
