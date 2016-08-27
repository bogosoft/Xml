using System;
using System.IO;
using System.Threading.Tasks;

namespace Bogosoft.Xml
{
    internal static class TextWriterExtensions
    {
        internal static async Task WriteAsync(this TextWriter writer, String format, params Object[] args)
        {
            await writer.WriteAsync(String.Format(format, args));
        }
    }
}