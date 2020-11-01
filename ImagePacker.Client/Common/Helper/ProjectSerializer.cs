using System;
using System.IO;
using System.Xml.Serialization;

namespace ImagePacker.Client.Model
{
    public class ProjectSerializer
    {
        public static void Save(string file, PackProject project)
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(PackProject));
                ser.Serialize(new FileStream(file, FileMode.Create, FileAccess.ReadWrite), project);
                project.FileName = file;
            }
            catch (Exception e)
            {

            }
        }

        public static PackProject Load(string file)
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(PackProject));
                var project = (PackProject)ser.Deserialize(new FileStream(file, FileMode.Open, FileAccess.Read));
                if(project != null) project.FileName = file;
                return project;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
