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
    public interface IDomFormatter
    {
        /// <summary>
        /// Format a given DOM node to an output writer.
        /// </summary>
        /// <param name="node">A DOM node to format.</param>
        /// <param name="output">An output writer.</param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        Task FormatAsync(XmlNode node, TextWriter output, CancellationToken token);
    }
}