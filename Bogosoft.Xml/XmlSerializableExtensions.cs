using System.Xml;

namespace Bogosoft.Xml
{
    /// <summary>
    /// Extended functionality for the <see cref="IXmlSerializable"/> contract.
    /// </summary>
    public static class XmlSerializableExtensions
    {
        /// <summary>
        /// Serialize the current instance to an XML document.
        /// </summary>
        /// <param name="serializable">The current <see cref="IXmlSerializable"/> implementation.</param>
        /// <returns>
        /// An XML document.
        /// </returns>
        public static XmlDocument Serialize(this IXmlSerializable serializable)
        {
            var document = new XmlDocument();

            serializable.SerializeTo(document);

            return document;
        }
    }
}