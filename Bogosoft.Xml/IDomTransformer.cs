using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Bogosoft.Xml
{
    /// <summary>
    /// Indicates that an implementation is capable of transforming a given DOM node.
    /// </summary>
    public interface IDomTransformer
    {
        /// <summary>
        /// Apply a DOM node transformation strategy to a given node.
        /// </summary>
        /// <param name="node">A DOM node to transform.</param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task TransformAsync(XmlNode node, CancellationToken token);
    }
}