using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Bogosoft.Xml
{
    /// <summary>A standard XML formatter.</summary>
    public class StandardXmlFormatter : IFormatXml
    {
        /// <summary>
        /// Get or set an array of filters to be applied to the document node prior to formatting.
        /// </summary>
        protected AsyncFilter[] Filters;

        /// <summary>Get or set the base indentation to be used during formatting.</summary>
        public String Indent = "";

        /// <summary>Get or set the line break to be used in formatting.</summary>
        public String LBreak = "";

        /// <summary>Format an <see cref="XmlNode"/> to a <see cref="TextWriter"/>.</summary>
        /// <param name="node">A node to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task FormatAsync(XmlNode node, TextWriter writer, CancellationToken token)
        {
            return FormatNodeAsync(node, writer, "", token);
        }

        /// <summary>
        /// Format an <see cref="XmlAttribute"/> to a <see cref="TextWriter"/>.  
        /// </summary>
        /// <param name="attribute">An <see cref="XmlAttribute"/> to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <param name="indent">An <see cref="String"/> representing the current indentation.</param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected virtual Task FormatAttributeAsync(
            XmlAttribute attribute,
            TextWriter writer,
            String indent,
            CancellationToken token
            )
        {
            return writer.WriteAsync(" " + attribute.Name + "=\"" + attribute.Value + "\"", token);
        }

        /// <summary>
        /// Format an <see cref="XmlCDataSection"/> to a <see cref="TextWriter"/>.  
        /// </summary>
        /// <param name="cdata">An <see cref="XmlCDataSection"/> to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected virtual Task FormatCDataSectionAsync(
            XmlCDataSection cdata,
            TextWriter writer,
            CancellationToken token
            )
        {
            return writer.WriteAsync(cdata.OuterXml, token);
        }

        /// <summary>
        /// Format an <see cref="XmlComment"/> to a <see cref="TextWriter"/>. 
        /// </summary>
        /// <param name="comment">An <see cref="XmlComment"/> to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected virtual Task FormatCommentAsync(XmlComment comment, TextWriter writer, CancellationToken token)
        {
            return writer.WriteAsync(comment.OuterXml, token);
        }

        /// <summary>
        /// Format an <see cref="XmlDocument"/> to a <see cref="TextWriter"/>.  
        /// </summary>
        /// <param name="document">An <see cref="XmlDocument"/> to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected virtual async Task FormatDocumentAsync(
            XmlDocument document,
            TextWriter writer,
            CancellationToken token
            )
        {
            foreach(var filter in (Filters ?? new AsyncFilter[0]))
            {
                await filter.Invoke(document, token);
            }

            foreach (XmlNode child in document.ChildNodes)
            {
                await FormatNodeAsync(child, writer, "", token);
            }
        }

        /// <summary>
        /// Format an <see cref="XmlDocumentFragment"/> to a <see cref="TextWriter"/>. 
        /// </summary>
        /// <param name="fragment">An <see cref="XmlDocumentFragment"/> to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <param name="indent">An <see cref="String"/> representing the current indentation.</param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected virtual async Task FormatDocumentFragmentAsync(
            XmlDocumentFragment fragment,
            TextWriter writer,
            String indent,
            CancellationToken token
            )
        {
            foreach(XmlNode n in fragment.ChildNodes)
            {
                await FormatNodeAsync(n, writer, indent, token);
            }
        }

        /// <summary>
        /// Format an <see cref="XmlDocumentType"/> to a <see cref="TextWriter"/>.  
        /// </summary>
        /// <param name="doctype">An <see cref="XmlDocumentType"/> to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected virtual Task FormatDocumentTypeAsync(
            XmlDocumentType doctype,
            TextWriter writer,
            CancellationToken token
            )
        {
            return writer.WriteAsync(doctype.OuterXml, token);
        }

        /// <summary>
        /// Format an <see cref="XmlElement"/> to a <see cref="TextWriter"/>. 
        /// </summary>
        /// <param name="element">An <see cref="XmlElement"/> to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <param name="indent">An <see cref="String"/> representing the current indentation.</param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected virtual async Task FormatElementAsync(
            XmlElement element,
            TextWriter writer,
            String indent,
            CancellationToken token
            )
        {
            await writer.WriteAsync(LBreak + indent + "<" + element.Name);

            foreach(XmlAttribute a in element.Attributes)
            {
                await FormatAttributeAsync(a, writer, indent, token);
            }

            if (element.HasChildNodes)
            {
                await writer.WriteAsync(">");

                var ecount = 0;

                foreach(XmlNode n in element.ChildNodes)
                {
                    await FormatNodeAsync(n, writer, indent + Indent, token);

                    if(n.NodeType == XmlNodeType.Element)
                    {
                        ++ecount;
                    }
                }

                if(ecount > 0)
                {
                    await writer.WriteAsync(LBreak + indent);
                }

                await writer.WriteAsync("</" + element.Name + ">");
            }
            else
            {
                await writer.WriteAsync("/>");
            }
        }

        /// <summary>Format an <see cref="XmlNode"/> to a <see cref="TextWriter"/>.</summary>
        /// <param name="node">A node to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <param name="indent">An optional identation.</param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected virtual Task FormatNodeAsync(
            XmlNode node,
            TextWriter writer,
            String indent,
            CancellationToken token
            )
        {
            switch (node.NodeType)
            {
                case XmlNodeType.Attribute:
                    return FormatAttributeAsync(node as XmlAttribute, writer, indent, token);
                case XmlNodeType.CDATA:
                    return FormatCDataSectionAsync(node as XmlCDataSection, writer, token);
                case XmlNodeType.Comment:
                    return FormatCommentAsync(node as XmlComment, writer, token);
                case XmlNodeType.Document:
                    return FormatDocumentAsync(node as XmlDocument, writer, token);
                case XmlNodeType.DocumentFragment:
                    return FormatDocumentFragmentAsync(node as XmlDocumentFragment, writer, indent, token);
                case XmlNodeType.DocumentType:
                    return FormatDocumentTypeAsync(node as XmlDocumentType, writer, token);
                case XmlNodeType.Element:
                    return FormatElementAsync(node as XmlElement, writer, indent, token);
                case XmlNodeType.ProcessingInstruction:
                    return FormatProcessingInstructionAsync(node as XmlProcessingInstruction, writer, token);
                case XmlNodeType.Text:
                    return FormatTextAsync(node as XmlText, writer, token);
                case XmlNodeType.XmlDeclaration:
                    return FormatXmlDeclarationAsync(node as XmlDeclaration, writer, token);
                default:
                    return writer.WriteAsync(node.OuterXml, token);
            }
        }

        /// <summary>
        /// Format an <see cref="XmlProcessingInstruction"/> to a <see cref="TextWriter"/>. 
        /// </summary>
        /// <param name="pi">An <see cref="XmlProcessingInstruction"/> to format to.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        protected virtual Task FormatProcessingInstructionAsync(
            XmlProcessingInstruction pi,
            TextWriter writer,
            CancellationToken token
            )
        {
            return writer.WriteAsync(pi.OuterXml, token);
        }

        /// <summary>
        /// Format an <see cref="XmlText"/> to a <see cref="TextWriter"/>.  
        /// </summary>
        /// <param name="text">An <see cref="XmlText"/> to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected virtual Task FormatTextAsync(XmlText text, TextWriter writer, CancellationToken token)
        {
            return writer.WriteAsync(text.Data, token);
        }

        /// <summary>
        /// Format an <see cref="XmlDeclaration"/> to a <see cref="TextWriter"/>.  
        /// </summary>
        /// <param name="declaration">An <see cref="XmlDeclaration"/> to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <param name="token">A <see cref="CancellationToken"/> object.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected virtual Task FormatXmlDeclarationAsync(
            XmlDeclaration declaration,
            TextWriter writer,
            CancellationToken token
            )
        {
            return writer.WriteAsync(declaration.OuterXml, token);
        }

        /// <summary>
        /// Instruct the current formatter to apply the given filter to an XML document prior to formatting.
        /// </summary>
        /// <param name="filter">A filter strategy.</param>
        protected void With(AsyncFilter filter)
        {
            Filters = new AsyncFilter[] { filter };
        }

        /// <summary>
        /// Instruct the current formatter to apply a given sequence of filters
        /// to an XML document prior to formatting.
        /// </summary>
        /// <param name="filters">A sequence of filter strategies.</param>
        protected void With(IEnumerable<AsyncFilter> filters)
        {
            Filters = filters.ToArray();
        }

        /// <summary>
        /// Instruct the current formatter to apply a given sequence of filters
        /// to an XML document prior to formatting.
        /// </summary>
        /// <param name="filters">A sequence of filter strategies.</param>
        protected void With(IEnumerable<IFilter> filters)
        {
            Filters = filters.Select<IFilter, AsyncFilter>(x => x.FilterAsync).ToArray();
        }

        /// <summary>
        /// Instruct the current formatter to apply the given filter to an XML document prior to formatting.
        /// </summary>
        /// <param name="filter">A filter strategy.</param>
        protected void With(IFilter filter)
        {
            Filters = new AsyncFilter[] { filter.FilterAsync };
        }
    }
}