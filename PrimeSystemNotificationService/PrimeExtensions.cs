using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PrimeSystemNotificationService
{
    public static class PrimeExtensions
    {
        public static T Deserialize<T>(this XDocument xmlDocument)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (XmlReader reader = xmlDocument.CreateReader())
                return (T)xmlSerializer.Deserialize(reader);
        }
        public static void Serialize<T>(this T xmlObject, string path_to_file)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (Stream writer = new FileStream(path_to_file, FileMode.Create))
            {
                xmlSerializer.Serialize(writer, xmlObject);
            }
        }
    }
}
