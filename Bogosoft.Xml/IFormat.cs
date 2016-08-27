using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace Bogosoft.Xml
{
    /// <summary>
    /// Provides a means of formatting an <see cref="XmlNode"/> to a <see cref="TextWriter"/>. 
    /// </summary>
    public interface IFormat
    {
        /// <summary>Format an <see cref="XmlNode"/> to a <see cref="TextWriter"/>.</summary>
        /// <param name="node">A node to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <param name="indent">An optional identation.</param>
        /// <returns>A <see cref="Task"/> representing a possibly asynchronous operation.</returns>
        Task FormatAsync(XmlNode node, TextWriter writer, String indent = "");
    }
}