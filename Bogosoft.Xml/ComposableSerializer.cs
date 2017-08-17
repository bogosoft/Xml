using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.XPath;

namespace Bogosoft.Xml
{
    /// <summary>
    /// An implementation of the <see cref="ISerializer"/> contract composed of a collection of
    /// <see cref="IDomNodeSerializer"/> instances.
    /// </summary>
    public sealed class ComposableSerializer : ISerializer
    {
        /// <summary>
        /// A collection of messages related to the operation of a <see cref="ComposableSerializer"/>.
        /// </summary>
        public static class Messages
        {
            /// <summary>
            /// Get an error message related to attempting to serialize an invalid type.
            /// </summary>
            public static string NoSerializers => $"The current {nameof(ComposableSerializer)} does not contain any child {nameof(IDomNodeSerializer)} instances.";
        }

        const BindingFlags Flags = BindingFlags.Public | BindingFlags.Instance;

        /// <summary>
        /// Get a list of DOM node serializers used to serialize values considered to be atomic
        /// to DOM nodes.
        /// </summary>
        public readonly List<IDomNodeSerializer> Serializers = new List<IDomNodeSerializer>();

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
            return Serializers.Count > 0;
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
                throw new InvalidOperationException(Messages.NoSerializers);
            }

            var document = new XmlDocument();

            Serialize(@object, document);

            return document;
        }

        void Serialize(object @object, XmlNode node)
        {
            var serializer = Serializers.FirstOrDefault(x => x.CanSerialize(@object));

            if (serializer == null)
            {
                var type = @object.GetType();

                var child = node.AppendElement("object");

                child.SetAttribute("namespace", type.Namespace);
                child.SetAttribute("type", type.Name);

                foreach (var fi in type.GetFields(Flags))
                {
                    Serialize(fi, @object, child);
                }

                foreach (var pi in type.GetProperties(Flags).Where(x => x.CanRead))
                {
                    Serialize(pi, @object, child);
                }
            }
            else
            {
                serializer.Serialize(@object, node);
            }
        }

        void Serialize(FieldInfo fi, object @object, XmlNode parent)
        {
            var child = parent.AppendElement("member");

            child.SetAttribute("name", fi.Name);

            Serialize(fi.GetValue(@object), child);
        }

        void Serialize(PropertyInfo pi, object @object, XmlNode parent)
        {
            var child = parent.AppendElement("member");

            child.SetAttribute("name", pi.Name);

            Serialize(pi.GetValue(@object), child);
        }
    }
}