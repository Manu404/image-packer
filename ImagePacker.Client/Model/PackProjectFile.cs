using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace ImagePacker.Client.Model
{
    public class PackProjectFile : ViewModelBase
    {
        public PackProjectFile()
        {
            Keywords = new List<string>();
        }

        public string ImageUrl { get; set; }
        public List<string> Keywords { get; set; }

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
