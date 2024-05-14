
using GaHipHop_Repository.Entity;

namespace Respository
{
    public class UnitOfWork : IDisposable
    {
        private MyDbContext context = new MyDbContext();
        private GenericRepository<Admin> _adminRepository;
        private GenericRepository<Product> _productRepository;
        private GenericRepository<Contact> _contactRepository;
        private GenericRepository<Discount> _discountRepository;
        private GenericRepository<Img> _imgRepository;
        private GenericRepository<Order> _orderRepository;
        private GenericRepository<OrderDetails> _orderDetailsRepository;
        private GenericRepository<Role> _roleRepository;
        private GenericRepository<UserInfo> _userInfoRepository;


        //public UnitOfWork(SqldataContext context)
        //{
        //    this.context = context;
        //}

        public GenericRepository<Admin> AdminRepository
        {
            get
            {

                if (this._adminRepository == null)
                {
                    this._adminRepository = new GenericRepository<Admin>(context);
                }
                return _adminRepository;
            }
        }
        public GenericRepository<Product> ProductRepository
        {
            get
            {

                if (this._productRepository == null)
                {
                    this._productRepository = new GenericRepository<Product>(context);
                }
                return _productRepository;
            }
        }
        public GenericRepository<Contact> ContactRepository
        {
            get
            {

                if (this._contactRepository == null)
                {
                    this._contactRepository = new GenericRepository<Contact>(context);
                }
                return _contactRepository;
            }
        }
        public GenericRepository<Discount> DiscountRepository
        {
            get
            {

                if (this._discountRepository == null)
                {
                    this._discountRepository = new GenericRepository<Discount>(context);
                }
                return _discountRepository;
            }
        }
        public GenericRepository<Img> ImgRepository
        {
            get
            {

                if (this._imgRepository == null)
                {
                    this._imgRepository = new GenericRepository<Img>(context);
                }
                return _imgRepository;
            }
        }
        public GenericRepository<OrderDetails> OrderDetailsRepository
        {
            get
            {

                if (this._orderDetailsRepository == null)
                {
                    this._orderDetailsRepository = new GenericRepository<OrderDetails>(context);
                }
                return _orderDetailsRepository;
            }
        }
        public GenericRepository<Role> RoleRepository
        {
            get
            {

                if (this._roleRepository == null)
                {
                    this._roleRepository = new GenericRepository<Role>(context);
                }
                return _roleRepository;
            }
        }
        public GenericRepository<UserInfo> UserInfoRepository
        {
            get
            {

                if (this._userInfoRepository == null)
                {
                    this._userInfoRepository = new GenericRepository<UserInfo>(context);
                }
                return _userInfoRepository;
            }
        }
        public GenericRepository<Order> OrderRepository
        {
            get
            {

                if (this._orderRepository == null)
                {
                    this._orderRepository = new GenericRepository<Order>(context);
                }
                return _orderRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}