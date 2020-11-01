using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ImagePacker.Client.Model
{

    public class PackProject : ViewModelBase
    {
        public string Name { get; set; }
        public int Revision { get; set; }
        public ObservableCollection<PackProjectFile> Files { get; set; }

        [XmlIgnore]
        public string FileName { get; set; }

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
