using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Bogosoft.Xml
{
    /// <summary>Extensions for the <see cref="IFormat"/> contract.</summary>
    public static class FormatterExtensions
    {
        /// <summary>Format an <see cref="XmlNode"/> to a <see cref="TextWriter"/>.</summary>
        /// <param name="formatter">The current <see cref="IFormat"/> object.</param>
        /// <param name="node">A node to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <param name="indent">An optional identation.</param>
        public static void Format(
            this IFormat formatter,
            XmlNode node,
            TextWriter writer,
            String indent = ""
            )
        {
            Task.Run(async () => await formatter.FormatAsync(node, writer, indent)).Wait();
        }

        /// <summary>Format an <see cref="IXmlSerializable"/> to a <see cref="TextWriter"/>.</summary>
        /// <param name="formatter">The current <see cref="IFormat"/> object.</param>
        /// <param name="serializable">An XML-serializable object.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <param name="indent">An optional identation.</param>
        public static void Format(
            this IFormat formatter,
            IXmlSerializable serializable,
            TextWriter writer,
            String indent = ""
            )
        {
            Task.Run(async () => await formatter.FormatAsync(serializable, writer, indent)).Wait();
        }

        /// <summary>
        /// Format an <see cref="IXmlSerializable"/> to a <see cref="TextWriter"/>.  
        /// </summary>
        /// <param name="formatter">The current <see cref="IFormat"/> object.</param>
        /// <param name="serializable">An XML-serializable object.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <param name="indent">An optional indentation.</param>
        /// <returns>A <see cref="Task"/> representing a possibly asynchronous operation.</returns>
        public static async Task FormatAsync(
            this IFormat formatter,
            IXmlSerializable serializable,
            TextWriter writer,
            String indent = ""
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

            await formatter.FormatAsync(document, writer, indent);
        }
    }
}