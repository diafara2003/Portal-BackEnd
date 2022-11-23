using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace API.Helper
{
    public class XmlHelper
    {
        public class SerializarXML<T> where T : class
        {
            public static string Serialize(List<T> obj)
            {
                var encoding = Encoding.GetEncoding("ISO-8859-1");
                //StringWriter sw = new StringWriter();
                XmlSerializer s = new XmlSerializer(obj.GetType());
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
                {
                    Indent = true,
                    OmitXmlDeclaration = false,
                    Encoding = encoding
                };
                using var stream = new MemoryStream();
                using (var xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
                {
                    s.Serialize(xmlWriter, obj);
                }
                return encoding.GetString(stream.ToArray());
            }
        }
    }
}
