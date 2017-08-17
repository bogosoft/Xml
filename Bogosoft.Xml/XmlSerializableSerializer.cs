using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace Bogosoft.Xml
{
    /// <summary>
    /// An implementation of the <see cref="ISerializer"/> and <see cref="IDomNodeSerializer"/> contracts
    /// capable of serializing objects implementing the <see cref="IXmlSerializable"/> contract.
    /// </summary>
    public sealed class XmlSerializableSerializer : IDomNodeSerializer, ISerializer
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
            return @object is IXmlSerializable;
        }

        /// <summary>
        /// Serialize a given object to an XPath-navigable object.
        /// </summary>
        /// <param name="object">An object to serialize.</param>
        /// <returns>
        /// An XPath-navigable object representing the serialized form of the given object.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown in the event that an attempt is made to serialize an object
        /// that does not implement the <see cref="IXmlSerializable"/> interface.
        /// </exception>
        public IXPathNavigable Serialize(object @object)
        {
            if (!CanSerialize(@object))
            {
                throw new InvalidOperationException($"The given object does not implement the {nameof(IXmlSerializable)} interface.");
            }

            using (var stream = new MemoryStream())
            using (var writer = XmlWriter.Create(stream))
            {
                (@object as IXmlSerializable).WriteXml(writer);

                stream.Position = 0;

                var document = new XmlDocument();

                document.Load(stream);

                return document;
            }
        }

        /// <summary>
        /// Serialize a given object to a given DOM node.
        /// </summary>
        /// <param name="object">An object to serialize.</param>
        /// <param name="node">
        /// A target DOM node to serialize the given object to.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown in the event that an attempt is made to serialize an object
        /// that does not implement the <see cref="IXmlSerializable"/> interface.
        /// </exception>
        public void Serialize(object @object, XmlNode node)
        {
            if (!CanSerialize(@object))
            {
                throw new InvalidOperationException($"The given object does not implement the {nameof(IXmlSerializable)} interface.");
            }

            using (var stream = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(stream))
                {
                    (@object as IXmlSerializable).WriteXml(writer);
                }

                stream.Position = 0;

                using (var reader = XmlReader.Create(stream))
                {
                    node.AppendChild(node.GetOwnerDocument().ReadNode(reader));
                }
            }
        }
    }
}