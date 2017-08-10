using System.Xml;

namespace Bogosoft.Xml
{
    /// <summary>
    /// Extended functionality for the <see cref="IDomSerializable"/> contract.
    /// </summary>
    public static class DomSerializableExtensions
    {
        /// <summary>
        /// Convert the current object into a DOM document.
        /// </summary>
        /// <param name="serializable">
        /// The current <see cref="IDomSerializable"/> implementation.
        /// </param>
        /// <returns>
        /// The current object serialized to a DOM document.
        /// </returns>
        public static XmlDocument Serialize(this IDomSerializable serializable)
        {
            var document = new XmlDocument();

            serializable.SerializeTo(document);

            return document;
        }
    }
}