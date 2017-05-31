using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Bogosoft.Xml
{
    /// <summary>
    /// Indicates that an implementation is capable of formatting an <see cref="XmlNode"/>
    /// and writing the resulting serialization to a <see cref="TextWriter"/>.
    /// </summary>
    public interface IFormatXml
    {
        /// <summary>
        /// Format a given <see cref="XmlNode"/> to a given <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="node">A node to format.</param>
        /// <param name="writer">A target to write the resulting serialization to.</param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        Task FormatAsync(XmlNode node, TextWriter writer, CancellationToken token);
    }
}