using System;
using System.Xml;

namespace Bogosoft.Xml
{
    /// <summary>
    /// An implementation of the <see cref="IDomNodeSerializer"/> contract capable of serializing
    /// values of enum types.
    /// </summary>
    public sealed class DefaultEnumValueSerializer : IDomNodeSerializer
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
            return @object.GetType().IsEnum;
        }

        /// <summary>
        /// Serialize a given object to a given DOM node.
        /// </summary>
        /// <param name="object">An object to serialize.</param>
        /// <param name="node">
        /// A target DOM node to serialize the given object to.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// Thrown in the event that the given object is not an enum value.
        /// </exception>
        public void Serialize(object @object, XmlNode node)
        {
            if (!CanSerialize(@object))
            {
                throw new InvalidOperationException("The current serializer can only serialize enum types.");
            }

            var type = @object.GetType();

            var child = node.AppendElement("enum");

            child.SetAttribute("namespace", type.Namespace);
            child.SetAttribute("type", type.Name);
            child.SetAttribute("value", Convert.ChangeType(@object, type.GetEnumUnderlyingType()).ToString());

            child.AppendTextNode(@object.ToString());
        }
    }
}