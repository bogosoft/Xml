using System.Xml;

namespace Bogosoft.Xml
{
    /// <summary>
    /// Extentended functionality for the <see cref="XmlNode"/> type. 
    /// </summary>
    public static class XmlNodeExtensions
    {
        /// <summary>
        /// Append a CDATA section to the current node.
        /// </summary>
        /// <param name="parent">The current node.</param>
        /// <param name="data">
        /// Non-parsed character data to append to the current node as a CDATA section.
        /// </param>
        /// <returns>The newly created CDATA section.</returns>
        public static XmlCDataSection AppendCDataSection(this XmlNode parent, string data)
        {
            var child = parent.GetOwnerDocument().CreateCDataSection(data);

            parent.AppendChild(child);

            return child;
        }

        /// <summary>
        /// Append a child node to the current node, returning the child as the specified DOM node type.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="XmlNode"/> that will be returned.</typeparam>
        /// <param name="parent">The current node.</param>
        /// <param name="child">The child <see cref="XmlNode"/> to append to the current node.</param>
        /// <returns>The append child node as a specified type.</returns>
        public static T AppendChild<T>(this XmlNode parent, XmlNode child) where T : XmlNode
        {
            return parent.AppendChild(child) as T;
        }

        /// <summary>
        /// Append a new DOM element to the current node.
        /// </summary>
        /// <param name="parent">The current node.</param>
        /// <param name="name">
        /// The qualified name of the element. If the name contains a colon then the <see cref="XmlNode.Prefix"/> 
        /// property will reflect the part of the name preceding the colon and the
        /// <see cref="XmlDocument.LocalName"/> property will reflect the part of the name after the colon.
        /// The qualified name cannot include a prefix of 'xmlns'.
        /// </param>
        /// <returns>The newly created DOM element.</returns>
        public static XmlElement AppendElement(this XmlNode parent, string name)
        {
            var child = parent.GetOwnerDocument().CreateElement(name);

            parent.AppendChild(child);

            return child;
        }

        /// <summary>
        /// Append a new DOM element to the current node.
        /// </summary>
        /// <param name="parent">The current node.</param>
        /// <param name="qualifiedName">
        /// The qualified name of the element. If the name contains a colon then the <see cref="XmlNode.Prefix"/> 
        /// property will reflect the part of the name preceding the colon and the
        /// <see cref="XmlDocument.LocalName"/> property will reflect the part of the name after the colon.
        /// The qualified name cannot include a prefix of 'xmlns'.
        /// </param>
        /// <param name="namespaceUri">The namespace URI of the element.</param>
        /// <returns>The newly created DOM element.</returns>
        public static XmlElement AppendElement(this XmlNode parent, string qualifiedName, string namespaceUri)
        {
            var child = parent.GetOwnerDocument().CreateElement(qualifiedName, namespaceUri);

            parent.AppendChild(child);

            return child;
        }

        /// <summary>
        /// Append a new DOM element to the current node.
        /// </summary>
        /// <param name="parent">The current node.</param>
        /// <param name="prefix">
        /// The prefix of the new element (if any). String.Empty and null are equivalent.
        /// </param>
        /// <param name="localName">The local name of the new element.</param>
        /// <param name="namespaceUri">
        /// The namespace URI of the new element (if any). <see cref="string.Empty"/> and null are equivalent.
        /// </param>
        /// <returns>The newly created DOM element.</returns>
        public static XmlElement AppendElement(
            this XmlNode parent,
            string prefix,
            string localName,
            string namespaceUri
            )
        {
            var child = parent.GetOwnerDocument().CreateElement(prefix, localName, namespaceUri);

            parent.AppendChild(child);

            return child;
        }

        /// <summary>
        /// Append a DOM text node to the current node.
        /// </summary>
        /// <param name="parent">The current node.</param>
        /// <param name="data">
        /// Parsed character data to append to the current node as a text node.
        /// </param>
        /// <returns>The newly created DOM text node.</returns>
        public static XmlText AppendTextNode(this XmlNode parent, string data)
        {
            var child = parent.GetOwnerDocument().CreateTextNode(data);

            parent.AppendChild(child);

            return child;
        }

        /// <summary>
        /// Return the current node as one of its derived types.
        /// </summary>
        /// <typeparam name="T">The derived type of <see cref="XmlNode"/> to return.</typeparam>
        /// <param name="node">The current <see cref="XmlNode"/>.</param>
        /// <returns>The current <see cref="XmlNode"/> typed as one of its dervied types.</returns>
        public static T As<T>(this XmlNode node) where T : XmlNode
        {
            return node as T;
        }

        /// <summary>
        /// Get the owning document of the current node or itself.
        /// </summary>
        /// <param name="node">The current <see cref="XmlNode"/> object.</param>
        /// <returns>
        /// The owning document of the current node or the node itself if it is a document.
        /// </returns>
        public static XmlDocument GetOwnerDocument(this XmlNode node)
        {
            return node is XmlDocument ? node as XmlDocument : node.OwnerDocument;
        }
    }
}