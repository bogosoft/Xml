using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Bogosoft.Xml
{
    /// <summary>
    /// Extended functionality for the <see cref="IDomTransformer"/> contract.
    /// </summary>
    public static class DomTransformerExtensions
    {
        /// <summary>
        /// Synchronously apply a DOM node transformation strategy to a given node.
        /// </summary>
        /// <param name="transformer">The current <see cref="AsyncDomTransformer"/> implementation.</param>
        /// <param name="node">A DOM node to transform.</param>
        public static void Transform(this AsyncDomTransformer transformer, XmlNode node)
        {
            Task.Run(async () => await transformer.Invoke(node, CancellationToken.None).ConfigureAwait(false)).Wait();
        }

        /// <summary>
        /// Apply a DOM node transformation strategy to a given node.
        /// </summary>
        /// <param name="transformer">The current <see cref="AsyncDomTransformer"/> implementation.</param>
        /// <param name="node">A DOM node to transform.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static Task TransformAsync(this AsyncDomTransformer transformer, XmlNode node)
        {
            return transformer.Invoke(node, CancellationToken.None);
        }

        /// <summary>
        /// Apply a DOM node transformation strategy to a given node.
        /// </summary>
        /// <param name="transformer">The current <see cref="AsyncDomTransformer"/> implementation.</param>
        /// <param name="node">A DOM node to transform.</param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static Task TransformAsync(
            this AsyncDomTransformer transformer,
            XmlNode node,
            CancellationToken token
            )
        {
            return transformer.Invoke(node, token);
        }

        /// <summary>
        /// Synchronously apply a DOM node transformation strategy to a given node.
        /// </summary>
        /// <param name="transformer">The current <see cref="IDomTransformer"/> implementation.</param>
        /// <param name="node">A DOM node to transform.</param>
        public static void Transform(this IDomTransformer transformer, XmlNode node)
        {
            Task.Run(async () => await transformer.TransformAsync(node, CancellationToken.None).ConfigureAwait(false)).Wait();
        }

        /// <summary>
        /// Apply a DOM node transformation strategy to a given node.
        /// </summary>
        /// <param name="transformer">The current <see cref="IDomTransformer"/> implementation.</param>
        /// <param name="node">A DOM node to transform.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static Task TransformAsync(this IDomTransformer transformer, XmlNode node)
        {
            return transformer.TransformAsync(node, CancellationToken.None);
        }
    }
}