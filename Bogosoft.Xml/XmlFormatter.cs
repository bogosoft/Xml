using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace Bogosoft.Xml
{
    /// <summary>A standard XML formatter.</summary>
    public class XmlFormatter
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
        public async Task FormatAsync(XmlNode node, TextWriter writer, String indent = "")
        {
            switch (node.NodeType)
            {
                case XmlNodeType.Attribute:
                    await this.FormatAttributeAsync(node as XmlAttribute, writer, indent);
                    break;
                case XmlNodeType.CDATA:
                    await this.FormatCDataSectionAsync(node as XmlCDataSection, writer);
                    break;
                case XmlNodeType.Comment:
                    await this.FormatCommentAsync(node as XmlComment, writer);
                    break;
                case XmlNodeType.Document:
                    await this.FormatDocumentAsync(node as XmlDocument, writer, indent);
                    break;
                case XmlNodeType.DocumentFragment:
                    await this.FormatDocumentFragmentAsync(node as XmlDocumentFragment, writer, indent);
                    break;
                case XmlNodeType.DocumentType:
                    await this.FormatDocumentTypeAsync(node as XmlDocumentType, writer);
                    break;
                case XmlNodeType.Element:
                    await this.FormatElementAsync(node as XmlElement, writer, indent);
                    break;
                case XmlNodeType.ProcessingInstruction:
                    await this.FormatProcessingInstructionAsync(node as XmlProcessingInstruction, writer, indent);
                    break;
                case XmlNodeType.Text:
                    await this.FormatTextAsync(node as XmlText, writer);
                    break;
                case XmlNodeType.XmlDeclaration:
                    await this.FormatXmlDeclarationAsync(node as XmlDeclaration, writer);
                    break;
                default:
                    await writer.WriteAsync(node.OuterXml);
                    break;
            }
        }

        /// <summary>
        /// Format an <see cref="XmlAttribute"/> to a <see cref="TextWriter"/>.  
        /// </summary>
        /// <param name="attribute">An <see cref="XmlAttribute"/> to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <param name="indent">An <see cref="String"/> representing the current indentation.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected virtual async Task FormatAttributeAsync(
            XmlAttribute attribute,
            TextWriter writer,
            String indent
            )
        {
            await writer.WriteAsync(" " + attribute.Name + "=\"" + attribute.Value + "\"");
        }

        /// <summary>
        /// Format an <see cref="XmlCDataSection"/> to a <see cref="TextWriter"/>.  
        /// </summary>
        /// <param name="cdata">An <see cref="XmlCDataSection"/> to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected virtual async Task FormatCDataSectionAsync(XmlCDataSection cdata, TextWriter writer)
        {
            await writer.WriteAsync("<![CDATA[" + cdata.Data + "]]>");
        }

        /// <summary>
        /// Format an <see cref="XmlComment"/> to a <see cref="TextWriter"/>. 
        /// </summary>
        /// <param name="comment">An <see cref="XmlComment"/> to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected virtual async Task FormatCommentAsync(XmlComment comment, TextWriter writer)
        {
            await writer.WriteAsync("<!--" + comment.Data + "-->");
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
                await this.FormatAsync(n, writer, indent);
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
                await this.FormatAsync(n, writer, indent);
            }
        }

        /// <summary>
        /// Format an <see cref="XmlDocumentType"/> to a <see cref="TextWriter"/>.  
        /// </summary>
        /// <param name="doctype">An <see cref="XmlDocumentType"/> to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected virtual async Task FormatDocumentTypeAsync(
            XmlDocumentType doctype,
            TextWriter writer
            )
        {
            await writer.WriteAsync(doctype.OuterXml);
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
            await writer.WriteAsync(this.LBreak + indent + "<" + element.Name);

            foreach(XmlAttribute a in element.Attributes)
            {
                await this.FormatAttributeAsync(a, writer, indent);
            }

            if (element.HasChildNodes)
            {
                await writer.WriteAsync(">");

                var ecount = 0;

                foreach(XmlNode n in element.ChildNodes)
                {
                    await this.FormatAsync(n, writer, indent + this.Indent);

                    if(n.NodeType == XmlNodeType.Element)
                    {
                        ++ecount;
                    }
                }

                if(ecount > 0)
                {
                    await writer.WriteAsync(this.LBreak + indent);
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
        protected virtual async Task FormatProcessingInstructionAsync(
            XmlProcessingInstruction pi,
            TextWriter writer,
            String indent
            )
        {
            await writer.WriteAsync(pi.OuterXml);
        }

        /// <summary>
        /// Format an <see cref="XmlText"/> to a <see cref="TextWriter"/>.  
        /// </summary>
        /// <param name="text">An <see cref="XmlText"/> to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected virtual async Task FormatTextAsync(XmlText text, TextWriter writer)
        {
            await writer.WriteAsync(text.Data);
        }

        /// <summary>
        /// Format an <see cref="XmlDeclaration"/> to a <see cref="TextWriter"/>.  
        /// </summary>
        /// <param name="declaration">An <see cref="XmlDeclaration"/> to format.</param>
        /// <param name="writer">A target <see cref="TextWriter"/> to format to.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected virtual async Task FormatXmlDeclarationAsync(XmlDeclaration declaration, TextWriter writer)
        {
            await writer.WriteAsync(declaration.OuterXml);
        }
    }
}