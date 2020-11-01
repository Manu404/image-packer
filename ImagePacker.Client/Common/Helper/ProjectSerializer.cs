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
                using (var stream = new FileStream(file, FileMode.Create, FileAccess.ReadWrite))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(PackProject));
                    ser.Serialize(stream, project);
                    project.FileName = file;
                }
            }
            catch (Exception e)
            {

            }
        }

        public static PackProject Load(string file)
        {
            try
            {
                PackProject project;
                using (var stream = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(PackProject));
                    project = (PackProject)ser.Deserialize(stream);
                    if (project != null) project.FileName = file;
                }
                return project;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
