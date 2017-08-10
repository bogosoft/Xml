using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Bogosoft.Xml
{
    /// <summary>
    /// Format an <see cref="XmlNode"/> and any of its descendants by serializing
    /// it to a <see cref="TextWriter"/> object.
    /// </summary>
    /// <param name="node">
    /// A node to serialize.
    /// </param>
    /// <param name="writer">
    /// A target <see cref="TextWriter"/> object to write the node serialization to.
    /// </param>
    /// <param name="token">
    /// A <see cref="CancellationToken"/> object.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> representing an asynchronous operation.
    /// </returns>
    public delegate Task AsyncDomFormatter(XmlNode node, TextWriter writer, CancellationToken token);
}