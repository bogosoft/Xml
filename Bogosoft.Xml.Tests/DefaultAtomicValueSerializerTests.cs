using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Bogosoft.Xml.Tests
{
    [TestFixture, Category("Unit")]
    public class DefaultAtomicValueSerializerTests
    {
        static IEnumerable<object> AtomicValues
        {
            get
            {
                yield return true;
                yield return byte.MaxValue;
                yield return sbyte.MinValue;
                yield return short.MinValue;
                yield return ushort.MaxValue;
                yield return int.MinValue;
                yield return uint.MaxValue;
                yield return long.MinValue;
                yield return ulong.MaxValue;
                yield return float.MinValue;
                yield return double.MaxValue;
                yield return '\0';
                yield return string.Empty;
                yield return DateTime.Today;
                yield return DateTimeOffset.Now;
                yield return Guid.Empty;
            }
        }

        class Person
        {
            internal string Name { get; set; }
        }

        [TestCase]
        public void CanSerializeAtomicValues()
        {
            var serializer = new DefaultAtomicValueSerializer();

            foreach (var x in AtomicValues)
            {
                var document = new XmlDocument();

                serializer.Serialize(x, document);

                document.ChildNodes.Count.ShouldBe(1);

                document.ChildNodes[0].Name.ShouldBe(x.GetType().Name);

                document.ChildNodes[0].ChildNodes.Count.ShouldBe(1);

                document.ChildNodes[0].ChildNodes[0].InnerText.ShouldBe(x.ToString());
            }
        }

        [TestCase]
        public void IndicatesCanSerializeAtomicValues()
        {
            var serializer = new DefaultAtomicValueSerializer();

            foreach (var x in AtomicValues)
            {
                serializer.CanSerialize(x).ShouldBeTrue();
            }
        }

        [TestCase]
        public void IndicatesCannotSerializeNonAtomicValue()
        {
            var person = new Person { Name = "Ellen" };

            new DefaultAtomicValueSerializer().CanSerialize(person).ShouldBeFalse();
        }

        [TestCase]
        public void ThrowsInvalidOperationExceptionOnAttemptToSerializeNonAtomicValue()
        {
            var serializer = new DefaultAtomicValueSerializer();

            var document = new XmlDocument();

            Action action = () => serializer.Serialize(new Person { Name = "Bob" }, document);

            action.ShouldThrow<InvalidOperationException>();
        }
    }
}