using System;
using System.Xml;

namespace Bogosoft.Xml
{
    /// <summary>
    /// Extended functionality for the <see cref="XmlElement"/> type.
    /// </summary>
    public static class XmlElementExtensions
    {
        /// <summary>
        /// Append a child <see cref="XmlElement"/> to the current <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="parent">The current <see cref="XmlElement"/> object.</param>
        /// <param name="name">
        /// The qualified name of the element. If the name contains a colon then the <see cref="XmlNode.Prefix"/> 
        /// property will reflect the part of the name preceding the colon and the
        /// <see cref="XmlDocument.LocalName"/> property will reflect the part of the name after the colon.
        /// The qualified name cannot include a prefix of'xmlns'.
        /// </param>
        /// <returns>The newly-created <see cref="XmlElement"/>.</returns>
        public static XmlElement AppendElement(this XmlElement parent, String name)
        {
            return parent.AppendChild(parent.OwnerDocument.CreateElement(name)) as XmlElement;
        }

        /// <summary>
        /// Append a child <see cref="XmlElement"/> to the current <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="parent">The current <see cref="XmlElement"/> object.</param>
        /// <param name="qualifiedName">
        /// The qualified name of the element. If the name contains a colon then the <see cref="XmlNode.Prefix"/> 
        /// property will reflect the part of the name preceding the colon and the
        /// <see cref="XmlDocument.LocalName"/> property will reflect the part of the name after the colon.
        /// The qualified name cannot include a prefix of'xmlns'.
        /// </param>
        /// <param name="namespaceUri">The namespace URI of the element.</param>
        /// <returns>The newly-created <see cref="XmlElement"/>.</returns>
        public static XmlElement AppendElement(
            this XmlElement parent,
            String qualifiedName,
            String namespaceUri
            )
        {
            return parent.AppendChild(parent.OwnerDocument.CreateElement(qualifiedName, namespaceUri)) as XmlElement;
        }

        /// <summary>
        /// Append a child <see cref="XmlElement"/> to the current <see cref="XmlElement"/>.
        /// </summary>
        /// <param name="parent">The current <see cref="XmlElement"/> object.</param>
        /// <param name="prefix">
        /// The prefix of the new element (if any). String.Empty and null are equivalent.
        /// </param>
        /// <param name="localName">The local name of the new element.</param>
        /// <param name="namespaceUri">
        /// The namespace URI of the new element (if any). <see cref="String.Empty"/> and null are equivalent.
        /// </param>
        /// <returns>The newly-created <see cref="XmlElement"/>.</returns>
        public static XmlElement AppendElement(
            this XmlElement parent,
            String prefix,
            String localName,
            String namespaceUri
            )
        {
            return parent.AppendChild(parent.OwnerDocument.CreateElement(prefix, localName, namespaceUri)) as XmlElement;
        }
    }
}