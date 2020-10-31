using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagePacker.Client.Model
{
    public class PackProjectFile
    {
        public string ImageUrl { get; set; }
        public List<string> Keywords { get; set; }
    }

    public class PackProject
    {
        public string Name { get; set; }
        public string Revision { get; set; }
        public List<PackProjectFile> Files { get; set; }

        public PackProject()
        {
            Files = new List<PackProjectFile>();
        }
    }
}
