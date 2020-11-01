using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Xml.Serialization;

namespace ImagePacker.Client.Model
{
    public class PackProject : ViewModelBase
    {
        [XmlIgnore]
        public ICommand IncrementRevision { get; set; }

        public string Name { get; set; }
        public int Revision { get; set; }
        public ObservableCollection<PackProjectFile> Files { get; set; }

        [XmlIgnore]
        public string FileName { get; set; }

        public PackProject()
        {
            Files = new ObservableCollection<PackProjectFile>();

            IncrementRevision = new RelayCommand(() => Revision += 1);
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
