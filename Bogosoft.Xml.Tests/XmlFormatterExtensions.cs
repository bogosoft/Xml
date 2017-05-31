using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Bogosoft.Xml.Tests
{
    static class XmlFormatterExtensions
    {
        internal static async Task<String> ToStringAsync(
            this StandardXmlFormatter formatter,
            XmlNode node,
            CancellationToken token = default(CancellationToken)
            )
        {
            using (var writer = new StringWriter())
            {
                await formatter.FormatAsync(node, writer, token);

                return writer.ToString();
            }
        }
    }
}