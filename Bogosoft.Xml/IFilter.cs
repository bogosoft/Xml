using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Bogosoft.Xml
{
    /// <summary>
    /// Indicates that an implementation is capable of applying a filter to an XML document.
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// Filter a given XML document.
        /// </summary>
        /// <param name="document">An XML document to filter.</param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        Task FilterAsync(XmlDocument document, CancellationToken token);
    }
}