using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia.Data;

namespace Application.Repository;

    public class SizeHasProductRepository : GenericRepository<SizeHasProduct>, ISizeHasProduct
    {
        private readonly DbAppContext _context;

        public SizeHasProductRepository(DbAppContext context): base(context)
        {
            _context = context;
        }
    }