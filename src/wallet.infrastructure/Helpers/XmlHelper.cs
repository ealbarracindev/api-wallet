using System.Xml.Serialization;

namespace wallet.infrastructure.Helpers;
public static class XmlHelper
{
    public static T FromXml<T>(string xml)
    {
        using (TextReader reader = new StringReader(xml))
        {
            try
            {
                return (T)new XmlSerializer(typeof(T)).Deserialize(reader);
            }
            catch (InvalidOperationException)
            {
                return default(T);
            }
        }
    }
}