﻿using System;
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
        private static object Lock = new object();

        public static void Serialize<T>(this T value, string filepath)
        {
            lock (Lock)
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

        public static T Deserialize<T>(string filepath)
        {
            lock (Lock)
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
