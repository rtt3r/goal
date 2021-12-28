using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Goal.DemoCqrs.Domain.Aggregates.People;
using Goal.Infra.Crosscutting.Collections;
using Goal.Infra.Crosscutting.Extensions;
using Goal.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Goal.DemoCqrs.Infra.Data.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(DbContext context)
            : base(context)
        {
        }

        public override Person Find(Guid id)
        {
            return Context
                .Set<Person>()
                .Include(p => p.Cpf)
                .FirstOrDefault(p => p.Id == id);
        }

        public override async Task<Person> FindAsync(Guid id)
        {
            return await Context
                .Set<Person>()
                .Include(p => p.Cpf)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public override ICollection<Person> Find()
        {
            return Context
                .Set<Person>()
                .Include(p => p.Cpf)
                .ToList();
        }

        public override async Task<ICollection<Person>> FindAsync()
        {
            return await Context
                .Set<Person>()
                .Include(p => p.Cpf)
                .ToListAsync();
        }

        public override IPagedCollection<Person> Find(IPagination pagination)
        {
            return Context
                .Set<Person>()
                .Include(p => p.Cpf)
                .PaginateList(pagination);
        }

        public override async Task<IPagedCollection<Person>> FindAsync(IPagination pagination)
        {
            return await Context
                .Set<Person>()
                .AsNoTracking()
                .Include(p => p.Cpf)
                .PaginateListAsync(pagination);
        }
    }
}
