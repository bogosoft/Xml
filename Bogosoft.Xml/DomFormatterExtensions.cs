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
        /// Synchronously format a given DOM-serializable object to an output writer.
        /// </summary>
        /// <param name="formatter">
        /// The current <see cref="AsyncDomFormatter"/> implementation.
        /// </param>
        /// <param name="object">A DOM-serializable object.</param>
        /// <param name="output">An output writer.</param>
        public static void Format(this AsyncDomFormatter formatter, IDomSerializable @object, TextWriter output)
        {
            formatter.FormatAsync(@object, output, CancellationToken.None).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Synchronously format a given XML-serializable object to an output writer.
        /// </summary>
        /// <param name="formatter">
        /// The current <see cref="AsyncDomFormatter"/> implementation.
        /// </param>
        /// <param name="object">A self-serializing object to format.</param>
        /// <param name="output">An output writer.</param>
        public static void Format(this AsyncDomFormatter formatter, IXmlSerializable @object, TextWriter output)
        {
            formatter.FormatAsync(@object, output, CancellationToken.None).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Synchronously format a given DOM node to an output writer.
        /// </summary>
        /// <param name="formatter">
        /// The current <see cref="AsyncDomFormatter"/> implementation.
        /// </param>
        /// <param name="node">A DOM node to format.</param>
        /// <param name="output">An output writer.</param>
        public static void Format(this AsyncDomFormatter formatter, XmlNode node, TextWriter output)
        {
            formatter.Invoke(node, output, CancellationToken.None).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Format a given DOM-serializable object to an output writer.
        /// </summary>
        /// <param name="formatter">
        /// The current <see cref="AsyncDomFormatter"/> implementation.
        /// </param>
        /// <param name="serializable">A DOM-serializable object.</param>
        /// <param name="output">An output writer.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public static Task FormatAsync(
            this AsyncDomFormatter formatter,
            IDomSerializable serializable,
            TextWriter output
            )
        {
            return formatter.FormatAsync(serializable, output, CancellationToken.None);
        }

        /// <summary>
        /// Format a given DOM-serializable object to an output writer.
        /// </summary>
        /// <param name="formatter">
        /// The current <see cref="AsyncDomFormatter"/> implementation.
        /// </param>
        /// <param name="serializable">A DOM-serializable object.</param>
        /// <param name="output">An output writer.</param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public static Task FormatAsync(
            this AsyncDomFormatter formatter,
            IDomSerializable serializable,
            TextWriter output,
            CancellationToken token
            )
        {
            return formatter.Invoke(serializable.Serialize(), output, token);
        }

        /// <summary>
        /// Format a given XML-serializable object to an output writer.
        /// </summary>
        /// <param name="formatter">
        /// The current <see cref="AsyncDomFormatter"/> implementation.
        /// </param>
        /// <param name="object">A self-serializing object to format.</param>
        /// <param name="output">An output writer.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
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
        /// <param name="object">A self-serializing object to format.</param>
        /// <param name="output">An output writer.</param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
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
        /// Format a given DOM node to an output writer.
        /// </summary>
        /// <param name="formatter">
        /// The current <see cref="AsyncDomFormatter"/> implementation.
        /// </param>
        /// <param name="node">A DOM node to format.</param>
        /// <param name="output">An output writer.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public static Task FormatAsync(this AsyncDomFormatter formatter, XmlNode node, TextWriter output)
        {
            return formatter.Invoke(node, output, CancellationToken.None);
        }

        /// <summary>
        /// Format a given DOM node to an output writer.
        /// </summary>
        /// <param name="formatter">
        /// The current <see cref="AsyncDomFormatter"/> implementation.
        /// </param>
        /// <param name="node">A DOM node to format.</param>
        /// <param name="output">An output writer.</param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public static Task FormatAsync(
            this AsyncDomFormatter formatter,
            XmlNode node,
            TextWriter output,
            CancellationToken token
            )
        {
            return formatter.Invoke(node, output, token);
        }

        /// <summary>
        /// Synchronously format a given DOM node to an output writer.
        /// </summary>
        /// <param name="formatter">The current <see cref="IDomFormatter"/> implementation.</param>
        /// <param name="node">A DOM node to format.</param>
        /// <param name="output">An output writer.</param>
        public static void Format(
            this IDomFormatter formatter,
            XmlNode node,
            TextWriter output
            )
        {
            formatter.FormatAsync(node, output, CancellationToken.None).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Synchronously format a given XML-serializable object to an output writer.
        /// </summary>
        /// <param name="formatter">The current <see cref="IDomFormatter"/> implementation.</param>
        /// <param name="serializable">An XML-serializable object.</param>
        /// <param name="output">An output writer.</param>
        public static void Format(
            this IDomFormatter formatter,
            IXmlSerializable serializable,
            TextWriter output
            )
        {
            formatter.FormatAsync(serializable, output, CancellationToken.None).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Synchronously format a given DOM-serializable object to an output writer.
        /// </summary>
        /// <param name="formatter">The current <see cref="IDomFormatter"/> implementation.</param>
        /// <param name="serializable">A DOM-serializable object.</param>
        /// <param name="output">An output writer.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public static void Format(
            this IDomFormatter formatter,
            IDomSerializable serializable,
            TextWriter output
            )
        {
            formatter.FormatAsync(serializable.Serialize(), output, CancellationToken.None)
                     .GetAwaiter()
                     .GetResult();
        }

        /// <summary>
        /// Format a given DOM-serializable object to an output writer.
        /// </summary>
        /// <param name="formatter">The current <see cref="IDomFormatter"/> implementation.</param>
        /// <param name="serializable">A DOM-serializable object.</param>
        /// <param name="output">An output writer.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public static Task FormatAsync(
            this IDomFormatter formatter,
            IDomSerializable serializable,
            TextWriter output
            )
        {
            return formatter.FormatAsync(serializable.Serialize(), output, CancellationToken.None);
        }

        /// <summary>
        /// Format a given DOM-serializable object to an output writer.
        /// </summary>
        /// <param name="formatter">The current <see cref="IDomFormatter"/> implementation.</param>
        /// <param name="serializable">A DOM-serializable object.</param>
        /// <param name="output">An output writer.</param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public static Task FormatAsync(
            this IDomFormatter formatter,
            IDomSerializable serializable,
            TextWriter output,
            CancellationToken token
            )
        {
            return formatter.FormatAsync(serializable.Serialize(), output, token);
        }

        /// <summary>
        /// Format a given XML-serializable object to an output writer.
        /// </summary>
        /// <param name="formatter">The current <see cref="IDomFormatter"/> implementation.</param>
        /// <param name="serializable">An XML-serializable object.</param>
        /// <param name="output">An output writer.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public static Task FormatAsync(this IDomFormatter formatter, IXmlSerializable serializable, TextWriter output)
        {
            return formatter.FormatAsync(serializable, output, CancellationToken.None);
        }

        /// <summary>
        /// Format a given XML-serializable object to an output writer.
        /// </summary>
        /// <param name="formatter">The current <see cref="IDomFormatter"/> implementation.</param>
        /// <param name="serializable">An XML-serializable object.</param>
        /// <param name="output">An output writer.</param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public static async Task FormatAsync(
            this IDomFormatter formatter,
            IXmlSerializable serializable,
            TextWriter output,
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

            await formatter.FormatAsync(document, output, token);
        }

        /// <summary>
        /// Format a given DOM node to an output writer.
        /// </summary>
        /// <param name="formatter">The current <see cref="IDomFormatter"/> implementation.</param>
        /// <param name="node">A node to format.</param>
        /// <param name="output">An output writer.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        public static Task FormatAsync(
            this IDomFormatter formatter,
            XmlNode node,
            TextWriter output
            )
        {
            return formatter.FormatAsync(node, output, CancellationToken.None);
        }
    }
}