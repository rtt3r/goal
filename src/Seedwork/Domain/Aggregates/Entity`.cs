using System.Diagnostics.CodeAnalysis;

namespace Goal.Domain.Abstractions.Aggregates;

public abstract class Entity<TKey> : IEntity<TKey>
{
    public virtual TKey Id { get; init; } = default!;

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (obj is IEntity<TKey> item && item.Id is not null)
            return item.Id.Equals(Id);

        return false;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return GetType().GetHashCode() * 397 ^ (Id?.GetHashCode() ?? 0);
        }
    }

    [SuppressMessage("Blocker Code Smell", "S3875:\"operator==\" should not be overloaded on reference types", Justification = "<Pending>")]
    public static bool operator ==(Entity<TKey> left, Entity<TKey> right)
    {
        return Equals(left, null)
            ? Equals(right, null)
            : left.Equals(right);
    }

    public static bool operator !=(Entity<TKey> left, Entity<TKey> right)
        => !(left == right);
}