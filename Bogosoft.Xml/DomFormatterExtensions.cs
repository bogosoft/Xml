using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Bogosoft.Xml
{
    /// <summary>Extensions for the <see cref="AsyncDomFormatter"/> and
    /// <see cref="IDomFormatter"/> contracts.</summary>
    public static class DomFormatterExtensions
    {
        /// <summary>
        /// Synchronously format a given XML-serializable object to an output writer.
        /// </summary>
        /// <param name="formatter">
        /// The current <see cref="AsyncDomFormatter"/> implementation.
        /// </param>
        /// <param name="object">A self-serializing object to format.</param>
        /// <param name="writer">A target to format to.</param>
        public static void Format(this AsyncDomFormatter formatter, IXmlSerializable @object, TextWriter writer)
        {
            formatter.FormatAsync(@object, writer, CancellationToken.None).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Synchronously format a given XML-serializable object to an output writer.
        /// </summary>
        /// <param name="formatter">
        /// The current <see cref="AsyncDomFormatter"/> implementation.
        /// </param>
        /// <param name="node">A DOM node to format to the output.</param>
        /// <param name="writer">A target to format to.</param>
        public static void Format(this AsyncDomFormatter formatter, XmlNode node, TextWriter writer)
        {
            formatter.Invoke(node, writer, CancellationToken.None).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Format a given XML-serializable object to an output writer.
        /// </summary>
        /// <param name="formatter">
        /// The current <see cref="AsyncDomFormatter"/> implementation.
        /// </param>
        /// <param name="object">A self-serializing object to format.</param>
        /// <param name="output">A target to format to.</param>
        /// <returns>A <see cref="Task"/> representing an asynchronous operation.</returns>
        public static Task FormatAsync(
            this AsyncDomFormatter formatter,
            IXmlSerializable @object,
            TextWriter output
            )
        {
            return formatter.FormatAsync(@object, output, CancellationToken.None);
        }

        /// <summary>
        /// Format a given XML-serializable object to an output writer.
        /// </summary>
        /// <param name="formatter">
        /// The current <see cref="AsyncDomFormatter"/> implementation.
        /// </param>
        /// <param name="node">A DOM node to format to the output.</param>
        /// <param name="output">A target to format to.</param>
        /// <returns>A <see cref="Task"/> representing an asynchronous operation.</returns>
        public static Task FormatAsync(this AsyncDomFormatter formatter, XmlNode node, TextWriter output)
        {
            return formatter.Invoke(node, output, CancellationToken.None);
        }

        /// <summary>
        /// Format a given XML-serializable object to an output writer.
        /// </summary>
        /// <param name="formatter">
        /// The current <see cref="AsyncDomFormatter"/> implementation.
        /// </param>
        /// <param name="object">A self-serializing object to format.</param>
        /// <param name="output">A target to format to.</param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>A <see cref="Task"/> representing an asynchronous operation.</returns>
        public static Task FormatAsync(
            this AsyncDomFormatter formatter,
            IXmlSerializable @object,
            TextWriter output,
            CancellationToken token
            )
        {
            var document = new XmlDocument();

            using (var stream = new MemoryStream())
            using (var writer = XmlWriter.Create(stream))
            {
                @object.WriteXml(writer);

                stream.Position = 0;

                document.Load(stream);
            }

            return formatter.Invoke(document, output, token);
        }

        /// <summary>
        /// Format a given XML-serializable object to an output writer.
        /// </summary>
        /// <param name="formatter">
        /// The current <see cref="AsyncDomFormatter"/> implementation.
        /// </param>
        /// <param name="node">A DOM node to format to the output.</param>
        /// <param name="output">A target to format to.</param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>A <see cref="Task"/> representing an asynchronous operation.</returns>
        public static Task FormatAsync(
            this AsyncDomFormatter formatter,
            XmlNode node,
            TextWriter output,
            CancellationToken token
            )
        {
            return formatter.Invoke(node, output, token);
        }

        /// <summary>Synchronously format an <see cref="XmlNode"/> to a <see cref="TextWriter"/>.</summary>
        /// <param name="formatter">The current <see cref="IDomFormatter"/> implementation.</param>
        /// <param name="node">A node to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        public static void Format(
            this IDomFormatter formatter,
            XmlNode node,
            TextWriter writer
            )
        {
            formatter.FormatAsync(node, writer, CancellationToken.None).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Synchronously ormat an <see cref="IXmlSerializable"/>
        /// to a <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="formatter">The current <see cref="IDomFormatter"/> implementation.</param>
        /// <param name="serializable">An XML-serializable object.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        public static void Format(
            this IDomFormatter formatter,
            IXmlSerializable serializable,
            TextWriter writer
            )
        {
            formatter.FormatAsync(serializable, writer, CancellationToken.None).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Synchronously format a self-serializing object to a given <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="formatter">The current <see cref="IDomFormatter"/> implementation.</param>
        /// <param name="serializable">A DOM-serializable object.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <returns>A <see cref="Task"/> representing a possibly asynchronous operation.</returns>
        public static void Format(
            this IDomFormatter formatter,
            IDomSerializable serializable,
            TextWriter writer
            )
        {
            formatter.FormatAsync(serializable.Serialize(), writer, CancellationToken.None)
                     .GetAwaiter()
                     .GetResult();
        }

        /// <summary>
        /// Format an <see cref="IXmlSerializable"/> to a <see cref="TextWriter"/>.  
        /// </summary>
        /// <param name="formatter">The current <see cref="IDomFormatter"/> implementation.</param>
        /// <param name="serializable">An XML-serializable object.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>A <see cref="Task"/> representing a possibly asynchronous operation.</returns>
        public static async Task FormatAsync(
            this IDomFormatter formatter,
            IXmlSerializable serializable,
            TextWriter writer,
            CancellationToken token
            )
        {
            var document = new XmlDocument();

            using (var stream = new MemoryStream())
            {
                using (var xmlWriter = XmlWriter.Create(stream))
                {
                    serializable.WriteXml(xmlWriter);

                    stream.Position = 0;

                    document.Load(stream);
                }
            }

            await formatter.FormatAsync(document, writer, token);
        }

        /// <summary>
        /// Format a self-serializing object to a given <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="formatter">The current <see cref="IDomFormatter"/> implementation.</param>
        /// <param name="serializable">A DOM-serializable object.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <returns>A <see cref="Task"/> representing a possibly asynchronous operation.</returns>
        public static Task FormatAsync(
            this IDomFormatter formatter,
            IDomSerializable serializable,
            TextWriter writer
            )
        {
            return formatter.FormatAsync(serializable.Serialize(), writer, CancellationToken.None);
        }

        /// <summary>
        /// Format a self-serializing object to a given <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="formatter">The current <see cref="IDomFormatter"/> implementation.</param>
        /// <param name="serializable">A DOM-serializable object.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>A <see cref="Task"/> representing a possibly asynchronous operation.</returns>
        public static Task FormatAsync(
            this IDomFormatter formatter,
            IDomSerializable serializable,
            TextWriter writer,
            CancellationToken token
            )
        {
            return formatter.FormatAsync(serializable.Serialize(), writer, token);
        }

        /// <summary>
        /// Format a given <see cref="XmlNode"/> to a given <see cref="TextWriter"/>. Calling this method is
        /// equivalent to calling <see cref="IDomFormatter.FormatAsync(XmlNode, TextWriter, CancellationToken)"/>
        /// with a value of <see cref="CancellationToken.None"/>.
        /// </summary>
        /// <param name="formatter">The current <see cref="IDomFormatter"/> implementation.</param>
        /// <param name="node">A node to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public static Task FormatAsync(
            this IDomFormatter formatter,
            XmlNode node,
            TextWriter writer
            )
        {
            return formatter.FormatAsync(node, writer, CancellationToken.None);
        }
    }
}