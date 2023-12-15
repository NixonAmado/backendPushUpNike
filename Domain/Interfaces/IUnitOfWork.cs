using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        public IRole Roles {get;}
        public IUser Users {get;}
        public ICart Carts {get;}
        public ICartItem CartItems {get;}
        public ICategory Categories {get;}
        public IOrder Orders {get;}
        public IOrderItem OrderItems {get;}
        public IPayment Payments {get;}
        public IPaymentMethod PaymentMethods {get;}
        public IPerson People {get;}
        public IProduct Products {get;}
        public ISize Sizes {get;}
        public ISizeHasProduct SizeHasProducts {get;}
        public IStatus Statuses {get;}
        Task<int> SaveAsync();
    }
}