using System;

namespace Goal.Demo2.Api.Application.Dtos.Customers
{
    public class CustomerDto
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
