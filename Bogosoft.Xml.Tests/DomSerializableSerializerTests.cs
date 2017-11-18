using NUnit.Framework;
using Shouldly;
using System;
using System.Xml;

namespace Bogosoft.Xml.Tests
{
    [TestFixture, Category("Unit")]
    public class DomSerializableSerializerTests
    {
        struct Location : IDomSerializable
        {
            internal double Latitude;

            internal double Longitude;

            public void SerializeTo(XmlNode target)
            {
                var child = target.AppendElement("location");

                child.SetAttribute("lat", Latitude.ToString());
                child.SetAttribute("long", Longitude.ToString());
            }
        }

        [TestCase]
        public void CanSerializeDomSerializableObject()
        {
            var location = new Location { Latitude = 30, Longitude = -101 };

            var serialized = new DomSerializableSerializer().Serialize(location);

            serialized.ShouldBeOfType<XmlDocument>();

            var document = serialized as XmlDocument;

            document.ChildNodes.Count.ShouldBe(1);

            var root = document.ChildNodes[0] as XmlElement;

            root.Name.ShouldBe("location");

            root.HasAttribute("lat").ShouldBeTrue();

            root.GetAttribute("lat").ShouldBe("30");

            root.HasAttribute("long").ShouldBeTrue();

            root.GetAttribute("long").ShouldBe("-101");
        }

        [TestCase]
        public void CanSerializeDomSerializableObjectToNode()
        {
            var location = new Location { Latitude = -20, Longitude = -180 };

            var document = new XmlDocument();

            new DomSerializableSerializer().Serialize(location, document);

            document.ChildNodes.Count.ShouldBe(1);

            var root = document.ChildNodes[0] as XmlElement;

            root.Name.ShouldBe("location");

            root.HasAttribute("lat").ShouldBeTrue();

            root.GetAttribute("lat").ShouldBe("-20");

            root.HasAttribute("long").ShouldBeTrue();

            root.GetAttribute("long").ShouldBe("-180");
        }

        [TestCase]
        public void IndicatesCanSerializeDomSerializableObject()
        {
            new DomSerializableSerializer().CanSerialize(new Location()).ShouldBeTrue();
        }

        [TestCase]
        public void IndicatesCannotSerializeNonDomSerializableObject()
        {
            new DomSerializableSerializer().CanSerialize(Guid.Empty).ShouldBeFalse();
        }

        [TestCase]
        public void ThrowsInvalidOperationExceptionOnAttemptToSerializeNonDomSerializableObject()
        {
            var serializer = new DomSerializableSerializer();

            Action action = () => serializer.Serialize(DateTime.Now);

            action.ShouldThrow<InvalidOperationException>();

            var document = new XmlDocument();

            action = () => serializer.Serialize(Guid.Empty, document);

            action.ShouldThrow<InvalidOperationException>();
        }
    }
}