using Goal.Domain;
using System.Diagnostics;

namespace Goal.Demo.Domain.Aggregates.People
{
    [DebuggerDisplay("Full Name = {FullName()}")]
    public class Name : ValueObject
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        protected Name()
            : base()
        {
        }

        public Name(string firstName, string lastName)
            : this()
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FullName()
        {
            return $"{FirstName} {LastName}".Trim();
        }

        public static Name CreateName(string firstName, string lastName)
        {
            return new Name(firstName, lastName);
        }
    }
}
