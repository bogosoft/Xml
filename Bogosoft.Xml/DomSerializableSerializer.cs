using System;
using System.Xml;
using System.Xml.XPath;

namespace Bogosoft.Xml
{
    /// <summary>
    /// An implementation of the <see cref="ISerializer"/> and <see cref="IDomNodeSerializer"/> contracts
    /// capable of serializing objects which implement the <see cref="IDomSerializable"/> interface.
    /// </summary>
    public sealed class DomSerializableSerializer : IDomNodeSerializer, ISerializer
    {
        /// <summary>
        /// Determine whether a given object is capable of being serialized
        /// by the current serializer.
        /// </summary>
        /// <param name="object">An object to serialize.</param>
        /// <returns>
        /// True if the given object can be serialized; false otherwise.
        /// </returns>
        public bool CanSerialize(object @object)
        {
            return @object is IDomSerializable;
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
            var document = new XmlDocument();

            Serialize(@object, document);

            return document;
        }

        /// <summary>
        /// Serialize a given object to a given DOM node.
        /// </summary>
        /// <param name="object">An object to serialize.</param>
        /// <param name="node">
        /// A target DOM node to serialize the given object to.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown in the event that the given object does not implement the
        /// <see cref="IDomSerializable"/> interface.
        /// </exception>
        public void Serialize(object @object, XmlNode node)
        {
            if (!CanSerialize(@object))
            {
                throw new InvalidOperationException($"The given object does not implement the {nameof(IDomSerializable)} interface.");
            }

            (@object as IDomSerializable).SerializeTo(node);
        }
    }
}