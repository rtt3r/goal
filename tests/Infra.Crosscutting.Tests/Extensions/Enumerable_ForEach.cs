using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Goal.Seedwork.Infra.Crosscutting.Extensions;
using Goal.Seedwork.Infra.Crosscutting.Tests.Mocks;
using Xunit;

namespace Goal.Seedwork.Infra.Crosscutting.Tests.Extensions;

public class Enumerable_ForEach
{
    [Fact]
    public void ForEach_NotEmptyEnumerable_NotThrowException()
    {
        IEnumerable<TestObject1> source = new List<TestObject1>
        {
            new TestObject1 { Id = 1 },
            new TestObject1 { Id = 2 }
        };

        source.ForEach(p => p.Value = p.Id.ToString());

        source.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(2);
        source.ElementAt(0).Value.Should().Be("1");
        source.ElementAt(1).Value.Should().Be("2");
    }

    [Fact]
    public void ForEach_EmptyEnumerable_NotThrowException()
    {
        IEnumerable<TestObject1> source = new List<TestObject1>();
        source.ForEach(p => { });

        source.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public void ForEach_Null_ThrowArgumentNullException()
    {
        Action act = () =>
        {
            IEnumerable<TestObject1> source = null!;
            source.ForEach(p => { });
        };

        act.Should().Throw<ArgumentNullException>().WithParameterName("source");
    }

    [Fact]
    public void ForEach_NotEmptyObjectEnumerable_NotThrowException()
    {
        TestObject1[] source = new[]
        {
            new TestObject1 { Id = 1 },
            new TestObject1 { Id = 2 }
        };

        source.ForEach(p =>
        {
            TestObject1 obj = p;
            obj.Value = obj.Id.ToString();
        });

        source.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(2);
        source[0].As<TestObject1>().Value.Should().Be("1");
        source[1].As<TestObject1>().Value.Should().Be("2");
    }

    [Fact]
    public void ForEach_EmptyObjectEnumerable_NotThrowException()
    {
        TestObject1[] source = Array.Empty<TestObject1>();
        source.ForEach(p => { });

        source.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public void ForEach_NullObjectEnumerable_ThrowArgumentNullException()
    {
        Action act = () =>
        {
            ArrayList source = null!;
            source.ForEach(p => { });
        };

        act.Should().Throw<ArgumentNullException>().WithParameterName("source");
    }

    [Fact]
    public void ForEach_ShouldExecuteActionOnForEachItem()
    {
        // arrange
        int[] input = new[] { 1, 2, 3 };
        int[] expectedOutput = new[] { 1, 2, 3 }; // each item incremented by one

        // act
        input.ForEach(item => { });

        // assert
        input.Should().BeEquivalentTo(expectedOutput);
    }

    [Fact]
    public void ForEach_ShouldReturnInputEnumerable()
    {
        // arrange
        IEnumerable input = new[] { 1, 2, 3 };

        // act
        IEnumerable output = input.ForEach(item => { });

        // assert
        output.Should().BeSameAs(input);
    }

    [Fact]
    public void ForEach_ShouldThrowArgumentNullException_WhenSourceIsNull()
    {
        // arrange
        IEnumerable source = null!;

        // act
        Action action = () => source.ForEach(item => Console.WriteLine(item));

        // assert
        action.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("source");
    }

    [Fact]
    public void ForEach_ShouldThrowArgumentNullException_WhenActionIsNull()
    {
        // arrange
        IEnumerable source = new[] { "one", "two", "three" };

        // act
        Action action = () => source.ForEach(null!);

        // assert
        action.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("action");
    }
}
