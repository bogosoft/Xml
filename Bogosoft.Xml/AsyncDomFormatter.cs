using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Bogosoft.Xml
{
    /// <summary>
    /// Format a given DOM node to an output writer.
    /// </summary>
    /// <param name="node">A DOM node to format.</param>
    /// <param name="writer">An output writer.</param>
    /// <param name="token">A <see cref="CancellationToken"/> object.</param>
    /// <returns>
    /// A <see cref="Task"/> representing an asynchronous operation.
    /// </returns>
    public delegate Task AsyncDomFormatter(XmlNode node, TextWriter writer, CancellationToken token);
}