using System.Diagnostics.CodeAnalysis;

namespace Goal.Seedwork.Domain.Aggregates;

public abstract class Entity<TKey> : IEntity<TKey>
{
    public virtual TKey Id { get; protected set; } = default;

    public override bool Equals(object obj) => obj is not null && obj is IEntity<TKey> item && (ReferenceEquals(this, obj) || item.Id.Equals(Id));

    public override int GetHashCode()
    {
        unchecked
        {
            return (GetType().GetHashCode() * 397) ^ Id.GetHashCode();
        }
    }

    [SuppressMessage("Blocker Code Smell", "S3875:\"operator==\" should not be overloaded on reference types", Justification = "<Pending>")]
    public static bool operator ==(Entity<TKey> left, Entity<TKey> right) => Equals(left, null) ? Equals(right, null) : left.Equals(right);

    public static bool operator !=(Entity<TKey> left, Entity<TKey> right) => !(left == right);
}
