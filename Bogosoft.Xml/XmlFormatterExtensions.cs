using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Bogosoft.Xml
{
    /// <summary>Extensions for the <see cref="StandardXmlFormatter"/> contract.</summary>
    public static class XmlFormatterExtensions
    {
        /// <summary>Format an <see cref="XmlNode"/> to a <see cref="TextWriter"/>.</summary>
        /// <param name="formatter">The current <see cref="StandardXmlFormatter"/> object.</param>
        /// <param name="node">A node to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <param name="indent">An optional identation.</param>
        public static void Format(
            this StandardXmlFormatter formatter,
            XmlNode node,
            TextWriter writer,
            String indent = ""
            )
        {
            Task.Run(async () => await formatter.FormatAsync(node, writer, indent)).Wait();
        }

        /// <summary>Format an <see cref="System.Xml.Serialization.IXmlSerializable"/> to a <see cref="TextWriter"/>.</summary>
        /// <param name="formatter">The current <see cref="StandardXmlFormatter"/> object.</param>
        /// <param name="serializable">An XML-serializable object.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <param name="indent">An optional identation.</param>
        public static void Format(
            this StandardXmlFormatter formatter,
            System.Xml.Serialization.IXmlSerializable serializable,
            TextWriter writer,
            String indent = ""
            )
        {
            Task.Run(async () => await formatter.FormatAsync(serializable, writer, indent)).Wait();
        }

        /// <summary>
        /// Format an <see cref="System.Xml.Serialization.IXmlSerializable"/> to a <see cref="TextWriter"/>.  
        /// </summary>
        /// <param name="formatter">The current <see cref="StandardXmlFormatter"/> object.</param>
        /// <param name="serializable">An XML-serializable object.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <param name="indent">An optional indentation.</param>
        /// <returns>A <see cref="Task"/> representing a possibly asynchronous operation.</returns>
        public static async Task FormatAsync(
            this StandardXmlFormatter formatter,
            System.Xml.Serialization.IXmlSerializable serializable,
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