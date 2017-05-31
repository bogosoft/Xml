using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace Bogosoft.Xml
{
    /// <summary>A standard XML formatter.</summary>
    public class StandardXmlFormatter
    {
        /// <summary>Get or set the base indentation to be used during formatting.</summary>
        public String Indent = "";

        /// <summary>Get or set the line break to be used in formatting.</summary>
        public String LBreak = "";

        /// <summary>Format an <see cref="XmlNode"/> to a <see cref="TextWriter"/>.</summary>
        /// <param name="node">A node to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <param name="indent">An optional identation.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task FormatAsync(XmlNode node, TextWriter writer, String indent = "")
        {
            switch (node.NodeType)
            {
                case XmlNodeType.Attribute:
                    return FormatAttributeAsync(node as XmlAttribute, writer, indent);
                case XmlNodeType.CDATA:
                    return FormatCDataSectionAsync(node as XmlCDataSection, writer);
                case XmlNodeType.Comment:
                    return FormatCommentAsync(node as XmlComment, writer);
                case XmlNodeType.Document:
                    return FormatDocumentAsync(node as XmlDocument, writer, indent);
                case XmlNodeType.DocumentFragment:
                    return FormatDocumentFragmentAsync(node as XmlDocumentFragment, writer, indent);
                case XmlNodeType.DocumentType:
                    return FormatDocumentTypeAsync(node as XmlDocumentType, writer);
                case XmlNodeType.Element:
                    return FormatElementAsync(node as XmlElement, writer, indent);
                case XmlNodeType.ProcessingInstruction:
                    return FormatProcessingInstructionAsync(node as XmlProcessingInstruction, writer, indent);
                case XmlNodeType.Text:
                    return FormatTextAsync(node as XmlText, writer);
                case XmlNodeType.XmlDeclaration:
                    return FormatXmlDeclarationAsync(node as XmlDeclaration, writer);
                default:
                    return writer.WriteAsync(node.OuterXml);
            }
        }

        /// <summary>
        /// Format an <see cref="XmlAttribute"/> to a <see cref="TextWriter"/>.  
        /// </summary>
        /// <param name="attribute">An <see cref="XmlAttribute"/> to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <param name="indent">An <see cref="String"/> representing the current indentation.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected virtual Task FormatAttributeAsync(
            XmlAttribute attribute,
            TextWriter writer,
            String indent
            )
        {
            return writer.WriteAsync(" " + attribute.Name + "=\"" + attribute.Value + "\"");
        }

        /// <summary>
        /// Format an <see cref="XmlCDataSection"/> to a <see cref="TextWriter"/>.  
        /// </summary>
        /// <param name="cdata">An <see cref="XmlCDataSection"/> to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected virtual Task FormatCDataSectionAsync(XmlCDataSection cdata, TextWriter writer)
        {
            return writer.WriteAsync(cdata.OuterXml);
        }

        /// <summary>
        /// Format an <see cref="XmlComment"/> to a <see cref="TextWriter"/>. 
        /// </summary>
        /// <param name="comment">An <see cref="XmlComment"/> to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected virtual Task FormatCommentAsync(XmlComment comment, TextWriter writer)
        {
            return writer.WriteAsync(comment.OuterXml);
        }

        /// <summary>
        /// Format an <see cref="XmlDocument"/> to a <see cref="TextWriter"/>.  
        /// </summary>
        /// <param name="document">An <see cref="XmlDocument"/> to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <param name="indent">An <see cref="String"/> representing the current indentation.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected virtual async Task FormatDocumentAsync(
            XmlDocument document,
            TextWriter writer,
            String indent
            )
        {
            foreach(XmlNode n in document.ChildNodes)
            {
                await FormatAsync(n, writer, indent);
            }
        }

        /// <summary>
        /// Format an <see cref="XmlDocumentFragment"/> to a <see cref="TextWriter"/>. 
        /// </summary>
        /// <param name="fragment">An <see cref="XmlDocumentFragment"/> to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <param name="indent">An <see cref="String"/> representing the current indentation.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected virtual async Task FormatDocumentFragmentAsync(
            XmlDocumentFragment fragment,
            TextWriter writer,
            String indent
            )
        {
            foreach(XmlNode n in fragment.ChildNodes)
            {
                await FormatAsync(n, writer, indent);
            }
        }

        /// <summary>
        /// Format an <see cref="XmlDocumentType"/> to a <see cref="TextWriter"/>.  
        /// </summary>
        /// <param name="doctype">An <see cref="XmlDocumentType"/> to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected virtual Task FormatDocumentTypeAsync(
            XmlDocumentType doctype,
            TextWriter writer
            )
        {
            return writer.WriteAsync(doctype.OuterXml);
        }

        /// <summary>
        /// Format an <see cref="XmlElement"/> to a <see cref="TextWriter"/>. 
        /// </summary>
        /// <param name="element">An <see cref="XmlElement"/> to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <param name="indent">An <see cref="String"/> representing the current indentation.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected virtual async Task FormatElementAsync(
            XmlElement element,
            TextWriter writer,
            String indent
            )
        {
            await writer.WriteAsync(LBreak + indent + "<" + element.Name);

            foreach(XmlAttribute a in element.Attributes)
            {
                await FormatAttributeAsync(a, writer, indent);
            }

            if (element.HasChildNodes)
            {
                await writer.WriteAsync(">");

                var ecount = 0;

                foreach(XmlNode n in element.ChildNodes)
                {
                    await FormatAsync(n, writer, indent + Indent);

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

        /// <summary>
        /// Format an <see cref="XmlProcessingInstruction"/> to a <see cref="TextWriter"/>. 
        /// </summary>
        /// <param name="pi">An <see cref="XmlProcessingInstruction"/> to format to.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <param name="indent">An <see cref="String"/> representing the current indentation.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected virtual Task FormatProcessingInstructionAsync(
            XmlProcessingInstruction pi,
            TextWriter writer,
            String indent
            )
        {
            return writer.WriteAsync(pi.OuterXml);
        }

        /// <summary>
        /// Format an <see cref="XmlText"/> to a <see cref="TextWriter"/>.  
        /// </summary>
        /// <param name="text">An <see cref="XmlText"/> to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected virtual Task FormatTextAsync(XmlText text, TextWriter writer)
        {
            return writer.WriteAsync(text.Data);
        }

        /// <summary>
        /// Format an <see cref="XmlDeclaration"/> to a <see cref="TextWriter"/>.  
        /// </summary>
        /// <param name="declaration">An <see cref="XmlDeclaration"/> to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected virtual Task FormatXmlDeclarationAsync(XmlDeclaration declaration, TextWriter writer)
        {
            return writer.WriteAsync(declaration.OuterXml);
        }
    }
}