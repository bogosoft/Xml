using System.Xml;

namespace Bogosoft.Xml
{
    /// <summary>
    /// Indicates that an implementation is capable of serializing itself directly to a DOM node.
    /// </summary>
    public interface IDomSerializable
    {
        /// <summary>
        /// Serialize the current object directly to a given DOM node.
        /// </summary>
        /// <param name="target">
        /// A DOM node to which the current object will be serialized.
        /// </param>
        void SerializeTo(XmlNode target);
    }
}