using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Bogosoft.Xml
{
    /// <summary>Extensions for the <see cref="IDomFormatter"/> contract.</summary>
    public static class DomFormatterExtensions
    {
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