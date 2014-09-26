using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace WebBuildLib.Extensions
{
    public static class BuildExtensions
    {
        public static T Deserialize<T>(this XDocument xmlDocument)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (XmlReader reader = xmlDocument.CreateReader())
            {
                if (xmlSerializer.CanDeserialize(reader))
                {
                    return (T)xmlSerializer.Deserialize(reader);
                }
                return default(T);
            }
        }
        public static void Serialize<T>(this T xmlObject, string path_to_file)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (Stream writer = new FileStream(path_to_file, FileMode.Create))
            {
                xmlSerializer.Serialize(writer, xmlObject);
            }
        }
        public static void SerializeAppend<T>(this T xmlObject, string path_to_file)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (Stream writer = new FileStream(path_to_file, FileMode.Append))
            {
                xmlSerializer.Serialize(writer, xmlObject);
            }
        }
    }
}
