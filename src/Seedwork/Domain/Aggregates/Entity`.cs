using System.Diagnostics.CodeAnalysis;

namespace Goal.Seedwork.Domain.Aggregates;

public abstract class Entity<TKey> : IEntity<TKey>
    where TKey : struct
{
    public virtual TKey Id { get; protected set; } = default;

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj is IEntity<TKey> item)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return item.Id.Equals(Id);
        }

        return false;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (GetType().GetHashCode() * 397) ^ Id.GetHashCode();
        }
    }

    [SuppressMessage("Blocker Code Smell", "S3875:\"operator==\" should not be overloaded on reference types", Justification = "<Pending>")]
    public static bool operator ==(Entity<TKey> left, Entity<TKey> right)
    {
        if (Equals(left, null))
        {
            return Equals(right, null);
        }

        return left.Equals(right);
    }

    public static bool operator !=(Entity<TKey> left, Entity<TKey> right)
        => !(left == right);
}