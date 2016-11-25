using NUnit.Framework;
using Should;
using System.Threading.Tasks;
using System.Xml;

namespace Bogosoft.Xml.Tests
{
    [TestFixture]
    public class UnitTests
    {
        [TestCase]
        public async Task CanFormatXmlDocument()
        {
            var document = new XmlDocument();

            var root = document.AppendChild<XmlElement>(document.CreateElement("test"));

            root.SetAttribute("say", "Hello, World!");

            var formatted = await new XmlFormatter().ToStringAsync(document);

            formatted.ShouldEqual("<test say=\"Hello, World!\"/>");
        }
    }
}