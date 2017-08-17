using System.Xml;

namespace Bogosoft.Xml
{
    /// <summary>
    /// Defines a contract for any type capable of serializing an
    /// object to a DOM node.
    /// </summary>
    public interface IDomNodeSerializer
    {
        /// <summary>
        /// Determine whether a given object is capable of being serialized
        /// by the current serializer.
        /// </summary>
        /// <param name="object">An object to serialize.</param>
        /// <returns>
        /// True if the given object can be serialized; false otherwise.
        /// </returns>
        bool CanSerialize(object @object);

        /// <summary>
        /// Serialize a given object to a given DOM node.
        /// </summary>
        /// <param name="object">An object to serialize.</param>
        /// <param name="node">
        /// A target DOM node to serialize the given object to.
        /// </param>
        void Serialize(object @object, XmlNode node);
    }
}