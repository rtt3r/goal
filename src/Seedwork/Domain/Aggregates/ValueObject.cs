using System;
using System.Linq;
using System.Reflection;

namespace Goal.Seedwork.Domain.Aggregates;

public class ValueObject : IEquatable<ValueObject>
{
    protected ValueObject() { }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        return obj is ValueObject valueObject && Equals(valueObject);
    }

    public virtual bool Equals(ValueObject? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        PropertyInfo[] properties = GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

        return properties.Length == 0
            || properties.All(p => Equals(p.GetValue(this, null), p.GetValue(obj, null)));
    }

    public override int GetHashCode()
    {
        int hashCode = 31;
        bool changeMultiplier = false;
        int index = 1;

        PropertyInfo[] properties = GetType().GetProperties();

        if (properties.Length > 0)
        {
            foreach (PropertyInfo item in properties)
            {
                object? value = item.GetValue(this, null);

                if (value is null)
                {
                    hashCode ^= index * 13;
                }
                else
                {
                    hashCode = (hashCode * (changeMultiplier ? 59 : 114)) + value.GetHashCode();
                    changeMultiplier = !changeMultiplier;
                }
            }
        }

        return Math.Abs(hashCode);
    }

    public static bool operator ==(ValueObject left, ValueObject right)
    {
        return left is null
            ? right is null
            : left.Equals(right);
    }

    public static bool operator !=(ValueObject left, ValueObject right) => !(left == right);
}