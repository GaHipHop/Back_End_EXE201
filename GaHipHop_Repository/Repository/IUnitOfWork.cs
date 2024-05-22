using GaHipHop_Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaHipHop_Repository.Repository
{
    public interface IUnitOfWork
    {
        public IGenericRepository<Admin> AdminRepository { get;  }
        public IGenericRepository<Category> CategoryRepository { get; }
        public IGenericRepository<Contact> ContactRepository { get; }
        public IGenericRepository<Discount> DiscountRepository { get; }
        public IGenericRepository<Kind> KindRepository { get; }
        public IGenericRepository<Order> OrderRepository { get; }
        public IGenericRepository<OrderDetails> OrderDetailsRepository { get; }
        public IGenericRepository<Product> ProductRepository { get; }
        public IGenericRepository<Role> RoleRepository { get; }
        public IGenericRepository<UserInfo> UserInfoRepository { get; }
        void Save();
        void Dispose();
    }
}
