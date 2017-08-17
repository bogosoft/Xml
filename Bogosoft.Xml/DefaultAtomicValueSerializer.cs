using System;
using System.Linq;
using System.Xml;

namespace Bogosoft.Xml
{
    /// <summary>
    /// An implementation of the <see cref="IDomNodeSerializer"/> contract capable of
    /// serializing atomic values to a given DOM node as its child.
    /// </summary>
    public sealed class DefaultAtomicValueSerializer : IDomNodeSerializer
    {
        static Type[] AdditionalAtomicTypes = new Type[]
        {
            typeof(DateTime),
            typeof(DateTimeOffset),
            typeof(Guid),
            typeof(string)
        };

        /// <summary>
        /// Determine whether a given object is capable of being serialized
        /// by the current serializer.
        /// </summary>
        /// <param name="object">An object to serialize.</param>
        /// <returns>
        /// True if the given object is considered to be atomic; false otherwise.
        /// </returns>
        public bool CanSerialize(object @object)
        {
            return CanSerializeType(@object.GetType());
        }

        bool CanSerializeType(Type type)
        {
            return AdditionalAtomicTypes.Contains(type) || type.IsPrimitive;
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
            var child = node.AppendElement(@object.GetType().Name);

            if (@object is string)
            {
                child.AppendCDataSection(@object as string);
            }
            else
            {
                child.AppendTextNode(@object.ToString());
            }
        }
    }
}