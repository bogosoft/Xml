using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Bogosoft.Xml
{
    /// <summary>
    /// Extended functionality for the <see cref="IFilter"/> contract.
    /// </summary>
    public static class FilterExtensions
    {
        /// <summary>
        /// Filter a given XML document.
        /// </summary>
        /// <param name="filter">The current <see cref="IFilter"/> implementation.</param>
        /// <param name="document">An XML document to filter.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public static Task FilterAsync(this IFilter filter, XmlDocument document)
        {
            return filter.FilterAsync(document, CancellationToken.None);
        }
    }
}