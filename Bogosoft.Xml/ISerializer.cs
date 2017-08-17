using System.Xml.XPath;

namespace Bogosoft.Xml
{
    /// <summary>
    /// Defines a contract for serializing objects to XPath-navigable structures.
    /// </summary>
    public interface ISerializer
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
        /// Serialize a given object to an XPath-navigable object.
        /// </summary>
        /// <param name="object">An object to serialize.</param>
        /// <returns>
        /// An XPath-navigable object representing the serialized form of the given object.
        /// </returns>
        IXPathNavigable Serialize(object @object);
    }
}