using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

    public class PackProject : ViewModelBase
    {
        public string Name { get; set; }
        public int Revision { get; set; }
        public ObservableCollection<PackProjectFile> Files { get; set; }

        public PackProject()
        {
            Files = new ObservableCollection<PackProjectFile>();
        }

        public void AddFile(string file)
        {
            Files.Add(new PackProjectFile()
            {
                ImageUrl = file
            });
        }

        public async void LoadImages()
        {
            foreach (var file in Files) await file.Load();
        }
    }
}
