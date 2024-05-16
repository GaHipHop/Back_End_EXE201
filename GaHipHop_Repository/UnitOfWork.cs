
using GaHipHop_Repository.Entity;
using GaHipHop_Repository.Repository;

namespace GaHipHop_Repository
{
    public class UnitOfWork : IDisposable
    {
        private MyDbContext _context = new MyDbContext();
        private GenericRepository<Admin> _adminRepository;
        private GenericRepository<Product> _productRepository;
        private GenericRepository<Contact> _contactRepository;
        private GenericRepository<Discount> _discountRepository;
        private GenericRepository<Img> _imgRepository;
        private GenericRepository<Order> _orderRepository;
        private GenericRepository<OrderDetails> _orderDetailsRepository;
        private GenericRepository<Role> _roleRepository;
        private GenericRepository<UserInfo> _userInfoRepository;


        public UnitOfWork()
        {
            //Ang Tem GAY lam
            //Ang Phu co ny
        }

        public GenericRepository<Admin> AdminRepository
        {
            get
            {

                if (this._adminRepository == null)
                {
                    this._adminRepository = new GenericRepository<Admin>(_context);
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
                    this._productRepository = new GenericRepository<Product>(_context);
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
                    this._contactRepository = new GenericRepository<Contact>(_context);
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
                    this._discountRepository = new GenericRepository<Discount>(_context);
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
                    this._imgRepository = new GenericRepository<Img>(_context);
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
                    this._orderDetailsRepository = new GenericRepository<OrderDetails>(_context);
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
                    this._roleRepository = new GenericRepository<Role>(_context);
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
                    this._userInfoRepository = new GenericRepository<UserInfo>(_context);
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
                    this._orderRepository = new GenericRepository<Order>(_context);
                }
                return _orderRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
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