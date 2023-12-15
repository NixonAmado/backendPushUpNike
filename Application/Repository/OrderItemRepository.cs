using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;


namespace Application.Repository;

    public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItem
    {
        private readonly DbAppContext _context;

        public OrderItemRepository(DbAppContext context): base(context)
        {
            _context = context;
        }
    }