using NUnit.Framework;
using Shouldly;
using System;
using System.IO;
using System.Xml;

namespace Bogosoft.Xml.Tests
{
    [TestFixture, Category("Unit")]
    public class DefaultEnumValueSerializerTests
    {
        [TestCase]
        public void CanSerializeEnumValue()
        {
            var target = FileMode.Open;

            var document = new XmlDocument();

            var serializer = new DefaultEnumValueSerializer();

            serializer.CanSerialize(target).ShouldBeTrue();

            serializer.Serialize(target, document);

            document.HasChildNodes.ShouldBeTrue();

            var root = document.DocumentElement;

            root.ShouldNotBeNull();

            root.Name.ShouldBe("enum");

            root.HasAttribute("namespace").ShouldBeTrue();

            root.GetAttribute("namespace").ShouldBe("System.IO");

            root.HasAttribute("type").ShouldBeTrue();

            root.GetAttribute("type").ShouldBe("FileMode");

            root.HasAttribute("value").ShouldBeTrue();

            root.GetAttribute("value").ShouldBe("3");

            root.ChildNodes.Count.ShouldBe(1);

            root.ChildNodes[0].InnerText.ShouldBe("Open");
        }

        [TestCase]
        public void IndicatesCannotSerializeNonEnumValue()
        {
            new DefaultEnumValueSerializer().CanSerialize(string.Empty).ShouldBeFalse();
        }

        [TestCase]
        public void IndicatesCanSerializeEnumValue()
        {
            new DefaultEnumValueSerializer().CanSerialize(FileMode.Truncate).ShouldBeTrue();
        }

        [TestCase]
        public void ThrowsInvalidOperationExceptionWhenGivenObjectIsNotEnum()
        {
            Action action = () => new DefaultEnumValueSerializer().Serialize(1, null);

            action.ShouldThrow<InvalidOperationException>();
        }
    }
}