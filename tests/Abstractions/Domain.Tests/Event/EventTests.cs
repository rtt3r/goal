using System;
using FluentAssertions;
using Xunit;

namespace Goal.Domain.Tests.Event;

public class EventTests
{
    private record SampleEvent(string AggregateId) : Goal.Domain.Events.Event(AggregateId, nameof(SampleEvent))
    {
    }

    [Fact]
    public void Event_Should_Set_Default_Timestamp()
    {
        // Arrange & Act
        var @event = new SampleEvent(string.Empty);

        // Assert
        @event.Timestamp.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Event_Should_Set_EventType()
    {
        // Arrange & Act
        var @event = new SampleEvent(string.Empty);

        // Assert
        @event.EventType.Should().Be(nameof(SampleEvent));
    }

    [Fact]
    public void Event_Should_Set_AggregateId_Property()
    {
        // Arrange
        string aggregateId = Guid.NewGuid().ToString();

        // Act
        var @event = new SampleEvent(aggregateId);

        // Assert
        @event.AggregateId.Should().Be(aggregateId);
    }
}