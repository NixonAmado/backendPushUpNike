using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;


namespace Application.Repository;

    public class PersonRepository : GenericRepository<Person>, IPerson
    {
        private readonly DbAppContext _context;

        public PersonRepository(DbAppContext context): base(context)
        {
            _context = context;
        }
          public override async Task<(int totalRegistros, IEnumerable<Person> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.People as IQueryable<Person>;
    
                if(!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.Name.ToLower() == search.ToLower());
                }
    
                query = query.OrderBy(p => p.Id);
                var totalRegistros = await query.CountAsync();
                var registros = await query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
    
                return (totalRegistros, registros);
            }
    }