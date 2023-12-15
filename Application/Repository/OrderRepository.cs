using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;


namespace Application.Repository;

    public class OrderRepository : GenericRepository<Order>, IOrder
    {
        private readonly DbAppContext _context;

        public OrderRepository(DbAppContext context): base(context)
        {
            _context = context;
        }
          public override async Task<(int totalRegistros, IEnumerable<Order> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
            {
                var query = _context.Orders as IQueryable<Order>;
    
                if(!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.Id.ToString() == search.ToLower());
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