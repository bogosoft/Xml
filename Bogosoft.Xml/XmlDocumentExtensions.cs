using System;
using System.Xml;

namespace Bogosoft.Xml
{
    /// <summary>
    /// Extended functionality for the <see cref="XmlDocument"/> type.
    /// </summary>
    public static class XmlDocumentExtensions
    {
        /// <summary>
        /// Append an <see cref="XmlElement"/> directly to the current <see cref="XmlDocument"/>. 
        /// </summary>
        /// <param name="document">The current <see cref="XmlDocument"/>.</param>
        /// <param name="name">
        /// The qualified name of the element. If the name contains a colon then the <see cref="XmlNode.Prefix"/> 
        /// property will reflect the part of the name preceding the colon and the
        /// <see cref="XmlDocument.LocalName"/> property will reflect the part of the name after the colon.
        /// The qualified name cannot include a prefix of'xmlns'.
        /// </param>
        /// <returns>The newly-created <see cref="XmlElement"/>.</returns>
        public static XmlElement AppendElement(this XmlDocument document, String name)
        {
            return document.AppendChild(document.CreateElement(name)) as XmlElement;
        }

        /// <summary>
        /// Append an <see cref="XmlElement"/> directly to the current <see cref="XmlDocument"/>. 
        /// </summary>
        /// <param name="document">The current <see cref="XmlDocument"/>.</param>
        /// <param name="qualifiedName">
        /// The qualified name of the element. If the name contains a colon then the <see cref="XmlNode.Prefix"/> 
        /// property will reflect the part of the name preceding the colon and the
        /// <see cref="XmlDocument.LocalName"/> property will reflect the part of the name after the colon.
        /// The qualified name cannot include a prefix of'xmlns'.
        /// </param>
        /// <param name="namespaceUri">The namespace URI of the element.</param>
        /// <returns>The newly-created <see cref="XmlElement"/>.</returns>
        public static XmlElement AppendElement(
            this XmlDocument document,
            String qualifiedName,
            String namespaceUri
            )
        {
            return document.AppendChild(document.CreateElement(qualifiedName, namespaceUri)) as XmlElement;
        }

        /// <summary>
        /// Append an <see cref="XmlElement"/> directly to the current <see cref="XmlDocument"/>. 
        /// </summary>
        /// <param name="document">The current <see cref="XmlDocument"/>.</param>
        /// <param name="prefix">
        /// The prefix of the new element (if any). String.Empty and null are equivalent.
        /// </param>
        /// <param name="localName">The local name of the new element.</param>
        /// <param name="namespaceUri">
        /// The namespace URI of the new element (if any). <see cref="String.Empty"/> and null are equivalent.
        /// </param>
        /// <returns>The newly-created <see cref="XmlElement"/>.</returns>
        public static XmlElement AppendElement(
            this XmlDocument document,
            String prefix,
            String localName,
            String namespaceUri
            )
        {
            return document.AppendChild(document.CreateElement(prefix, localName, namespaceUri)) as XmlElement;
        }
    }
}