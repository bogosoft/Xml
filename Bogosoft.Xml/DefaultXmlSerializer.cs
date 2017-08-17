using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace Bogosoft.Xml
{
    /// <summary>
    /// An implementation of the <see cref="IDomNodeSerializer"/> and <see cref="ISerializer"/> contracts
    /// capable of serializing objects using an internally generated <see cref="XmlSerializer"/>.
    /// </summary>
    public sealed class DefaultXmlSerializer : IDomNodeSerializer, ISerializer
    {
        /// <summary>
        /// A collection of messages related to the operation of a <see cref="DefaultXmlSerializer"/>.
        /// </summary>
        public static class Messages
        {
            /// <summary>
            /// Get an error message related to attempting to serialize an invalid type.
            /// </summary>
            public static string InvalidType => $"The given object cannot be serialized by the current {nameof(DefaultXmlSerializer)}.";
        }

        Func<object, bool> qualifier;

        /// <summary>
        /// Create a new instance of the <see cref="DefaultXmlSerializer"/> class.
        /// </summary>
        public DefaultXmlSerializer()
        {
            qualifier = x => true;
        }

        /// <summary>
        /// Create a new instance of the <see cref="DefaultXmlSerializer"/> class.
        /// </summary>
        /// <param name="qualifier">
        /// A selection strategy for determining which objects can be serialized.
        /// </param>
        public DefaultXmlSerializer(Func<object, bool> qualifier)
        {
            this.qualifier = qualifier;
        }

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
            return qualifier.Invoke(@object);
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
            if (!CanSerialize(@object))
            {
                throw new InvalidOperationException(Messages.InvalidType);
            }

            using (var stream = new MemoryStream())
            {
                new XmlSerializer(@object.GetType()).Serialize(stream, @object);

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
        public void Serialize(object @object, XmlNode node)
        {
            if (!CanSerialize(@object))
            {
                throw new InvalidOperationException(Messages.InvalidType);
            }

            using (var stream = new MemoryStream())
            {
                var document = new XmlDocument();

                new XmlSerializer(@object.GetType()).Serialize(stream, @object);

                stream.Position = 0;

                document.Load(stream);

                node.AppendChild(document.RemoveChild(document.FirstChild));
            }
        }
    }
}