using System;
using System.Runtime.Serialization;

namespace Goal.Infra.Crosscutting.Exceptions
{
    [Serializable]
    public class DomainViolationException : ApplicationException
    {
        public DomainViolationException()
        {
        }

        public DomainViolationException(string message)
            : base(message)
        {
        }

        public DomainViolationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected DomainViolationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
