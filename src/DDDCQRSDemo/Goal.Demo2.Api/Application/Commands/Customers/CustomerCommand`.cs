using System;
using Goal.Domain.Seedwork.Commands;

namespace Goal.Demo2.Api.Application.Commands.Customers
{
    public abstract class CustomerCommand<T> : Command<T>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
