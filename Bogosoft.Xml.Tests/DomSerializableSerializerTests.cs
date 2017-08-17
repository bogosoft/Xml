using NUnit.Framework;
using Should;
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

            serialized.ShouldBeType<XmlDocument>();

            var document = serialized as XmlDocument;

            document.ChildNodes.Count.ShouldEqual(1);

            var root = document.ChildNodes[0] as XmlElement;

            root.Name.ShouldEqual("location");

            root.HasAttribute("lat").ShouldBeTrue();

            root.GetAttribute("lat").ShouldEqual("30");

            root.HasAttribute("long").ShouldBeTrue();

            root.GetAttribute("long").ShouldEqual("-101");
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
        }
    }
}