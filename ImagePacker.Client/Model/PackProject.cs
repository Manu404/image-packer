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
        private int _revision;
        private string _name;

        [XmlIgnore]
        public ICommand IncrementRevision { get; set; }
        [XmlIgnore]
        public string FileName { get; set; }

        public string Name { get => _name; set { _name = value;  RaisePropertyChanged(); } }
        public int Revision { get => _revision; set { _revision = value; RaisePropertyChanged(); } }
        public ObservableCollection<PackProjectFile> Files { get; set; }

        public PackProject()
        {
            Files = new ObservableCollection<PackProjectFile>();

            IncrementRevision = new RelayCommand(() => Revision += 1);
        }

        private void AddFile(string file)
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
