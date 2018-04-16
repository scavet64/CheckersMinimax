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
        private static object lockObject = new object();

        /// <summary>
        /// Serializes the specified object to the specified filepath.
        /// </summary>
        /// <typeparam name="T">Type of the object being serialized</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="filepath">The filepath.</param>
        public static void Serialize<T>(this T value, string filepath)
        {
            lock (lockObject)
            {
                XmlSerializer xmlserializer = new XmlSerializer(typeof(T));
                using (StreamWriter stringWriter = new StreamWriter(filepath))
                {
                    using (XmlWriter writer = XmlWriter.Create(stringWriter))
                    {
                        xmlserializer.Serialize(writer, value);
                    }
                }
            }
        }

        /// <summary>
        /// Deserializes the specified filepath.
        /// </summary>
        /// <typeparam name="T">Type of the object being deserialized</typeparam>
        /// <param name="filepath">The filepath.</param>
        /// <returns>The deserialized object</returns>
        public static T Deserialize<T>(string filepath)
        {
            lock (lockObject)
            {
                XmlSerializer xmlserializer = new XmlSerializer(typeof(T));
                using (StreamReader streamReader = new StreamReader(filepath))
                {
                    return (T)xmlserializer.Deserialize(streamReader);
                }
            }
        }
    }
}
