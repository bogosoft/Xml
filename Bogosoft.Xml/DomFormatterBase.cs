using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Bogosoft.Xml
{
    /// <summary>
    /// A partial implementation of the <see cref="IDomFormatter"/> contract that allows
    /// for pre-format transformations.
    /// </summary>
    public abstract class DomFormatterBase : IDomFormatter
    {
        /// <summary>
        /// Get a list of DOM transformers which will be applied before formatting.
        /// </summary>
        public readonly List<AsyncDomTransformer> Transformers = new List<AsyncDomTransformer>();

        /// <summary>
        /// Format a given DOM node to an output writer.
        /// </summary>
        /// <param name="node">A DOM node to format.</param>
        /// <param name="output">An output writer.</param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public abstract Task FormatAsync(XmlNode node, TextWriter output, CancellationToken token);
    }
}