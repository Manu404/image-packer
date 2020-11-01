using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
        [XmlIgnore]
        public ICommand AddKeyword { get; set; }

        public PackProjectFile()
        {
            Keywords = new ObservableCollection<string>();
            AddKeyword = new RelayCommand(() => OnAddKeyword());
        }

        public string ImageUrl { get; set; }
        public ObservableCollection<string> Keywords { get; set; }

        private ImageSource image;
        [XmlIgnore]
        public ImageSource Image
        {
            get { return image; }
            set
            {
                image = value;
                RaisePropertyChanged();
            }
        }

        public void OnAddKeyword()
        {
            this.Keywords.Add("test");
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
