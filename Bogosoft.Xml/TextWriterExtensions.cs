using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Bogosoft.Xml
{
    static class TextWriterExtensions
    {
        internal static Task WriteAsync(this TextWriter writer, String format, params Object[] args)
        {
            return writer.WriteAsync(String.Format(format, args));
        }

        internal static Task WriteAsync(this TextWriter writer, string data, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            return writer.WriteAsync(data);
        }
    }
}