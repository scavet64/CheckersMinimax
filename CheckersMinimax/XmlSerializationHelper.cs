using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CheckersMinimax
{
    public static class XmlSerializationHelper
    {
        public static void Serialize<T>(this T value)
        {
            XmlSerializer xmlserializer = new XmlSerializer(typeof(T));
            using (StringWriter stringWriter = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(stringWriter))
                {
                    xmlserializer.Serialize(writer, value);
                }
            }
        }

        public static T Deserialize<T>(string filepath)
        {
            XmlSerializer xmlserializer = new XmlSerializer(typeof(T));
            using (StreamReader streamReader = new StreamReader(filepath))
            {
                using (XmlReader xmlWriter = XmlReader.Create(streamReader))
                {
                    return (T)xmlserializer.Deserialize(xmlWriter);
                }
            }
        }
    }
}
