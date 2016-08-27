using System.Xml;

namespace Bogosoft.Xml
{
    /// <summary>
    /// Extentended functionality for the <see cref="XmlNode"/> type. 
    /// </summary>
    public static class XmlNodeExtensions
    {
        /// <summary>
        /// Append a child node to the current <see cref="XmlNode"/> object, returning the child as
        /// a specific generic type.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="XmlNode"/> that will be returned.</typeparam>
        /// <param name="parent">The current <see cref="XmlNode"/>.</param>
        /// <param name="child">The child <see cref="XmlNode"/> to append to the current node.</param>
        /// <returns>The append child node as a specified type.</returns>
        public static T AppendChild<T>(this XmlNode parent, XmlNode child) where T : XmlNode
        {
            return parent.AppendChild(child) as T;
        }

        /// <summary>
        /// Return the current <see cref="XmlNode"/> as one of its derived types.
        /// </summary>
        /// <typeparam name="T">The derived type of <see cref="XmlNode"/> to return.</typeparam>
        /// <param name="node">The current <see cref="XmlNode"/>.</param>
        /// <returns>The current <see cref="XmlNode"/> typed as one of its dervied types.</returns>
        public static T As<T>(this XmlNode node) where T : XmlNode
        {
            return node as T;
        }
    }
}