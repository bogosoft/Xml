using System.Xml;
using System.Xml.XPath;

namespace Bogosoft.Xml
{
    /// <summary>
    /// An implementation of the <see cref="ISerializer"/> and <see cref="IDomNodeSerializer"/>
    /// contracts that only serializes null references.
    /// </summary>
    public sealed class NullSerializer : IDomNodeSerializer, ISerializer
    {
        /// <summary>
        /// Determine whether a given object is capable of being serialized
        /// by the current serializer.
        /// </summary>
        /// <param name="object">An object to serialize.</param>
        /// <returns>
        /// True if the given object is null; false otherwise.
        /// </returns>
        public bool CanSerialize(object @object)
        {
            return ReferenceEquals(null, @object);
        }

        /// <summary>
        /// Serialize a given object to an XPath-navigable object.
        /// </summary>
        /// <param name="object">An object to serialize.</param>
        /// <returns>
        /// An XPath-navigable object representing the serialized form of the given object.
        /// </returns>
        public IXPathNavigable Serialize(object @object)
        {
            return new XmlDocument();
        }

        /// <summary>
        /// Serialize a given object to a given DOM node.
        /// </summary>
        /// <param name="object">An object to serialize.</param>
        /// <param name="node">
        /// A target DOM node to serialize the given object to. The given target node will have
        /// an empty child element called 'null' appended to it.
        /// </param>
        public void Serialize(object @object, XmlNode node)
        {
            node.AppendElement("null");
        }
    }
}