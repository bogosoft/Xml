using NUnit.Framework;
using Should;
using System;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;

namespace Bogosoft.Xml.Tests
{
    [TestFixture, Category("Unit")]
    public class XmlSerializableSerializerTests
    {
        static void Validate(XmlElement element, Address address)
        {
            element.Name.ShouldEqual("address");

            element.ChildNodes.Count.ShouldEqual(3);

            element.ChildNodes[0].Name.ShouldEqual("city");

            element.ChildNodes[0].ChildNodes.Count.ShouldEqual(1);

            element.ChildNodes[0].ChildNodes[0].InnerText.ShouldEqual(address.City);

            element.ChildNodes[1].Name.ShouldEqual("postal-code");

            element.ChildNodes[1].ChildNodes.Count.ShouldEqual(1);

            element.ChildNodes[1].ChildNodes[0].InnerText.ShouldEqual(address.PostalCode);

            element.ChildNodes[2].Name.ShouldEqual("region");

            element.ChildNodes[2].ChildNodes.Count.ShouldEqual(1);

            element.ChildNodes[2].ChildNodes[0].InnerText.ShouldEqual(address.Region);
        }

        class Address : IXmlSerializable
        {
            internal static readonly Address Denver = new Address
            {
                City = "Denver",
                PostalCode = "80123",
                Region = "CO"
            };

            internal string City { get; set; }

            internal string PostalCode { get; set; }

            internal string Region { get; set; }

            public XmlSchema GetSchema()
            {
                throw new NotImplementedException();
            }

            public void ReadXml(XmlReader reader)
            {
                throw new NotImplementedException();
            }

            public void WriteXml(XmlWriter writer)
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("address");
                writer.WriteElementString("city", City);
                writer.WriteElementString("postal-code", PostalCode);
                writer.WriteElementString("region", Region);
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
            }
        }

        [TestCase]
        public void CanSerializeXmlSerializableObject()
        {
            var address = Address.Denver;

            var serialized = new XmlSerializableSerializer().Serialize(address);

            serialized.ShouldBeType<XmlDocument>();

            var document = serialized as XmlDocument;

            document.ChildNodes.Count.ShouldEqual(2);

            document.ChildNodes[1].NodeType.ShouldEqual(XmlNodeType.Element);

            Validate(document.ChildNodes[1] as XmlElement, address);
        }

        [TestCase]
        public void CanSerializeXmlSerializableObjectToNode()
        {
            var address = Address.Denver;

            var document = new XmlDocument();

            new XmlSerializableSerializer().Serialize(address, document);

            document.ChildNodes.Count.ShouldEqual(2);

            document.ChildNodes[1].NodeType.ShouldEqual(XmlNodeType.Element);

            Validate(document.ChildNodes[1] as XmlElement, address);
        }

        [TestCase]
        public void IndicatesCanSerializeXmlSerializableObject()
        {
            new XmlSerializableSerializer().CanSerialize(Address.Denver).ShouldBeTrue();
        }

        [TestCase]
        public void IndicatesCannotSerializeNonXmlSerializableObject()
        {
            new XmlSerializableSerializer().CanSerialize(DateTimeOffset.Now).ShouldBeFalse();
        }

        [TestCase]
        public void ThrowsInvalidOperationExceptionOnAttemptToSerializeNonXmlSerializableObject()
        {
            Action action;

            var serializer = new XmlSerializableSerializer();

            action = () => serializer.Serialize(800);

            action.ShouldThrow<InvalidOperationException>();

            var document = new XmlDocument();

            action = () => serializer.Serialize(string.Empty, document);

            action.ShouldThrow<InvalidOperationException>();
        }
    }
}