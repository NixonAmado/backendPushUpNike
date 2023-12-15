using Domain.Entities;
using Domain.Interfaces;
using Persistencia.Data;


namespace Application.Repository;

    public class CartItemRepository : GenericRepository<CartItem>, ICartItem
    {
        private readonly DbAppContext _context;

        public CartItemRepository(DbAppContext context): base(context)
        {
            _context = context;
        }
    }