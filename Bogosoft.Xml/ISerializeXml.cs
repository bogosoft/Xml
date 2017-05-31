using System.Xml;

namespace Bogosoft.Xml
{
    /// <summary>
    /// Indicates that a type has the ability to serialize itself to an <see cref="XmlNode"/>.
    /// </summary>
    public interface ISerializeXml
    {
        /// <summary>
        /// Serialize the current instance to an <see cref="XmlNode"/>.
        /// </summary>
        /// <param name="target">The <see cref="XmlNode"/> to serialize to.</param>
        void SerializeTo(XmlNode target);
    }
}