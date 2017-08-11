using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Bogosoft.Xml
{
    /// <summary>
    /// Represents a template for a method capable of applying
    /// a DOM node transformation strategy to a given node.
    /// </summary>
    /// <param name="node">A DOM node to transform.</param>
    /// <param name="token">A <see cref="CancellationToken"/> object.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public delegate Task AsyncDomTransformer(XmlNode node, CancellationToken token);
}