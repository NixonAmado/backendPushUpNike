using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;

namespace Application.Repository;

    public class StatusRepository : GenericRepository<Status>, IStatus
    {
        private readonly DbAppContext _context;

        public StatusRepository(DbAppContext context): base(context)
        {
            _context = context;
        }
          public override async Task<(int totalRegistros, IEnumerable<Status> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.Statuses as IQueryable<Status>;
    
                if(!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.Description.ToLower() == search.ToLower());
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