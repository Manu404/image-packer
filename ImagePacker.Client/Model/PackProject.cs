using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
