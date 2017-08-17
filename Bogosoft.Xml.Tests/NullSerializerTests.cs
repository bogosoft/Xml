using NUnit.Framework;
using Should;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Bogosoft.Xml.Tests
{
    [TestFixture, Category("Unit")]
    public class NullSerializerTests
    {
        [TestCase]
        public void CanSerializeNullValue()
        {
            var serialized = new NullSerializer().Serialize(null);

            serialized.ShouldBeType<XmlDocument>();

            (serialized as XmlDocument).HasChildNodes.ShouldBeFalse();
        }

        [TestCase]
        public void CanSerializeNullValueToNode()
        {
            var document = new XmlDocument();

            new NullSerializer().Serialize(null, document);

            document.ChildNodes.Count.ShouldEqual(1);

            document.ChildNodes[0].Name.ShouldEqual("null");
        }

        [TestCase]
        public void IndicatesCanSerializeNullValue()
        {
            new NullSerializer().CanSerialize(null).ShouldBeTrue();
        }

        [TestCase]
        public void IndicatesCannotSerializeNonNullValue()
        {
            var serializer = new NullSerializer();

            serializer.CanSerialize(string.Empty).ShouldBeFalse();

            serializer.CanSerialize(Guid.NewGuid()).ShouldBeFalse();
        }

        [TestCase]
        public void ThrowsInvalidOperationExceptionOnAttemptToSerializeNonNullValue()
        {
            Action action;

            object value;

            value = string.Empty;

            action = () => new NullSerializer().Serialize(value);

            action.ShouldThrow<InvalidOperationException>();

            value = 10;

            action.ShouldThrow<InvalidOperationException>();

            var document = new XmlDocument();

            var root = document.AppendElement("root");

            action = () => new NullSerializer().Serialize(value, root);

            value = "I'm a string.";

            action.ShouldThrow<InvalidOperationException>();

            value = DateTime.Now;

            action.ShouldThrow<InvalidOperationException>();
        }
    }
}