using System;
using System.Collections.Generic;
using FluentAssertions;
using Vantage.Infra.Crosscutting.Tests.Mocks;
using Xunit;

namespace Vantage.Infra.Crosscutting.Tests.Extensions
{
    public class Object_ToDictionary
    {
        [Fact]
        public void ReturnDictionaryGivenNotNullObject()
        {
            var object1 = new TestObject1 { Id = 1 };
            IDictionary<string, object> dictionary = object1.ToDictionary();

            dictionary.Should().NotBeNull().And.ContainKey("Id").And.ContainKey("Value");
            dictionary["Id"].Should().Be(1);
            dictionary["Value"].Should().BeNull();
        }

        [Fact]
        public void ReturnEmptyGivenNullObject()
        {
            TestObject1 object1 = null;
            IDictionary<string, object> dictionary = object1.ToDictionary();

            dictionary.Should().NotBeNull().And.BeEmpty().And.NotContainKey("Id").And.NotContainKey("Value");
        }

        [Fact]
        public void ReturnGenericDictonaryGivenNotNullObject()
        {
            var object1 = new TestObject1 { Id = 1 };
            IDictionary<string, string> dictionary = object1.ToDictionary<string>();

            dictionary.Should().NotBeNull().And.ContainKey("Id").And.ContainKey("Value");
            dictionary["Id"].Should().Be("1");
            dictionary["Value"].Should().BeNull();
        }

        [Fact]
        public void ReturnEmptyGenericDictionaryGivenNullObject()
        {
            TestObject1 object1 = null;
            IDictionary<string, string> dictionary = object1.ToDictionary<string>();

            dictionary.Should().NotBeNull().And.BeEmpty().And.NotContainKey("Id").And.NotContainKey("Value");
        }

        [Fact]
        public void ReturnDictionaryWithNullValuesGivenTypeMismatchObject()
        {
            var object1 = new TestObject1 { Id = 1, Value = "Test" };
            IDictionary<string, int> dictionary = object1.ToDictionary<int>();

            dictionary.Should().NotBeNull().And.ContainKey("Id").And.ContainKey("Value");
            dictionary["Id"].Should().Be(1);
            dictionary["Value"].Should().Be(0);
        }
    }
}
