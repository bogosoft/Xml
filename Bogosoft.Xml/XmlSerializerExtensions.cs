using System.Xml;

namespace Bogosoft.Xml
{
    /// <summary>
    /// Extended functionality for the <see cref="IXmlSerialize"/> contract.
    /// </summary>
    public static class XmlSerializerExtensions
    {
        /// <summary>
        /// Serialize the current instance to an <see cref="XmlDocument"/>.
        /// </summary>
        /// <param name="serializable">The current <see cref="IXmlSerialize"/> implementation.</param>
        /// <returns>A serialized <see cref="XmlDocument"/>.</returns>
        public static XmlDocument Serialize(this IXmlSerialize serializable)
        {
            var document = new XmlDocument();

            serializable.SerializeTo(document);

            return document;
        }
    }
}