using System.Collections.Generic;

namespace Bogosoft.Xml
{
    /// <summary>
    /// Extended functionality for the <see cref="IList{T}"/> contract.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Add a DOM transformer to the current list.
        /// </summary>
        /// <param name="transformers">The current list.</param>
        /// <param name="transformer">A transformation strategy.</param>
        public static void Add(this IList<AsyncDomTransformer> transformers, IDomTransformer transformer)
        {
            transformers.Add(transformer.TransformAsync);
        }
    }
}