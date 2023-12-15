using Application.Repository;
using Domain.Entities;
using Domain.Interfaces;
using Persistencia.Data;

namespace Application.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly DbAppContext _context;
        private RoleRepository _roles;
        private UserRepository _users;
        private CartRepository _Cart;
        private CartItemRepository _CartItem;
        private CategoryRepository _Category;
        private OrderRepository _Order;
        private OrderItemRepository _OrderItem;
        private PaymentRepository _Payment;
        private PaymentMethodRepository _PaymentMethod;
        private PersonRepository _Person;
        private ProductRepository _Product;
        private SizeRepository _Size;
        private SizeHasProductRepository _SizeHasProduct;
        private StatusRepository _Status;
        public UnitOfWork(DbAppContext context)
        {
            _context = context;
        }

        public IRole Roles
        {
            get
            {
                _roles ??= new RoleRepository(_context);
                return _roles;
            }
        }
        public IUser Users{
            get{
                _users ??= new UserRepository(_context);
                return _users;
            }
        }
        public ICart Carts {
            get{
                _Cart ??= new CartRepository(_context);
                return _Cart;
            }
        }
        public ICartItem CartItems {
            get{
                _CartItem ??= new CartItemRepository(_context);
                return _CartItem;
            }
        }
        public ICategory Categories {
            get{
                _Category ??= new CategoryRepository(_context);
                return _Category;
            }
        }
        public IOrder Orders {
            get{
                _Order ??= new OrderRepository(_context);
                return _Order;
            }
        }
        public IOrderItem OrderItems {
            get{
                _OrderItem ??= new OrderItemRepository(_context);
                return _OrderItem;
            }            
        }
        public IPayment Payments {
            get{
                _Payment ??= new PaymentRepository(_context);
                return _Payment;
            }            
        }
        public IPaymentMethod PaymentMethods {
            get{
                _PaymentMethod ??= new PaymentMethodRepository(_context);
                return _PaymentMethod;
            }
        }
        public IPerson People {
            get{
                _Person ??= new PersonRepository(_context);
                return _Person;
            }
        }
        public IProduct Products {
            get{
                _Product ??= new ProductRepository(_context);
                return _Product;
            }
        }
        public ISize Sizes {
            get{
                _Size ??= new SizeRepository(_context);
                return _Size;
            }
        }
        public ISizeHasProduct SizeHasProducts {
            get{
                _SizeHasProduct ??= new SizeHasProductRepository(_context);
                return _SizeHasProduct;
            }
        }
        public IStatus Statuses {
            get{
                _Status ??= new StatusRepository(_context);
                return _Status;
            }
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        
    }
}