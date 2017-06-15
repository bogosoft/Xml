using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Bogosoft.Xml
{
    /// <summary>
    /// Apply a filter to a given XML document.
    /// </summary>
    /// <param name="document">An XML document to apply the filter against.</param>
    /// <param name="token">A <see cref="CancellationToken"/> object.</param>
    /// <returns>
    /// A <see cref="Task"/> representing the asynchronous operation.
    /// </returns>
    public delegate Task AsyncFilter(XmlDocument document, CancellationToken token);
}