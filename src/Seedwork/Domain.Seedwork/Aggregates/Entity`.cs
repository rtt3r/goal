namespace Goal.Domain.Aggregates
{
    public abstract class Entity<TKey> : IEntity<TKey>
    {
        public virtual TKey Id { get; protected set; }

        protected Entity() { }

        public override bool Equals(object obj)
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
                return GetType().GetHashCode() * 397 ^ Id.GetHashCode();
            }
        }

        public static bool operator ==(Entity<TKey> left, Entity<TKey> right)
        {
            if (Equals(left, null))
            {
                return Equals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(Entity<TKey> left, Entity<TKey> right) => !(left == right);
    }
}
