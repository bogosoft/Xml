using System.Xml;
using System.Xml.XPath;

namespace Bogosoft.Xml
{
    /// <summary>
    /// Extended functionality for the <see cref="IXmlSerializable"/> contract.
    /// </summary>
    public static class XmlSerializerExtensions
    {
        /// <summary>
        /// Serialize the current instance to an <see cref="XmlDocument"/>.
        /// </summary>
        /// <param name="serializable">The current <see cref="IXmlSerializable"/> implementation.</param>
        /// <returns>
        /// An XPath-navigable object.
        /// </returns>
        public static XmlDocument Serialize(this IXmlSerializable serializable)
        {
            var document = new XmlDocument();

            serializable.SerializeTo(document);

            return document;
        }
    }
}