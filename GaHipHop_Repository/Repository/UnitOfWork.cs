using GaHipHop_Repository.Entity;

namespace GaHipHop_Repository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private MyDbContext _context = new MyDbContext();
        private IGenericRepository<Admin> _adminRepository;
        private IGenericRepository<Category> _categoryRepository;
        private IGenericRepository<Product> _productRepository;
        private IGenericRepository<Contact> _contactRepository;
        private IGenericRepository<Discount> _discountRepository;
        private IGenericRepository<Kind> _kindRepository;
        private IGenericRepository<Order> _orderRepository;
        private IGenericRepository<OrderDetails> _orderDetailsRepository;
        private IGenericRepository<Role> _roleRepository;
        private IGenericRepository<UserInfo> _userInfoRepository;


        public UnitOfWork()
        {
        }

        public IGenericRepository<Admin> AdminRepository
        {
            get
            {

                if (_adminRepository == null)
                {
                    _adminRepository = new GenericRepository<Admin>(_context);
                }
                return _adminRepository;
            }
        }
        public IGenericRepository<Product> ProductRepository
        {
            get
            {

                if (_productRepository == null)
                {
                    _productRepository = new GenericRepository<Product>(_context);
                }
                return _productRepository;
            }
        }
        public IGenericRepository<Contact> ContactRepository
        {
            get
            {

                if (_contactRepository == null)
                {
                    _contactRepository = new GenericRepository<Contact>(_context);
                }
                return _contactRepository;
            }
        }
        public IGenericRepository<Discount> DiscountRepository
        {
            get
            {

                if (_discountRepository == null)
                {
                    _discountRepository = new GenericRepository<Discount>(_context);
                }
                return _discountRepository;
            }
        }
        public IGenericRepository<Kind> KindRepository
        {
            get
            {

                if (_kindRepository == null)
                {
                    _kindRepository = new GenericRepository<Kind>(_context);
                }
                return _kindRepository;
            }
        }
        public IGenericRepository<OrderDetails> OrderDetailsRepository
        {
            get
            {

                if (_orderDetailsRepository == null)
                {
                    _orderDetailsRepository = new GenericRepository<OrderDetails>(_context);
                }
                return _orderDetailsRepository;
            }
        }
        public IGenericRepository<Role> RoleRepository
        {
            get
            {

                if (_roleRepository == null)
                {
                    _roleRepository = new GenericRepository<Role>(_context);
                }
                return _roleRepository;
            }
        }
        public IGenericRepository<UserInfo> UserInfoRepository
        {
            get
            {

                if (_userInfoRepository == null)
                {
                    _userInfoRepository = new GenericRepository<UserInfo>(_context);
                }
                return _userInfoRepository;
            }
        }
        public IGenericRepository<Order> OrderRepository
        {
            get
            {

                if (_orderRepository == null)
                {
                    _orderRepository = new GenericRepository<Order>(_context);
                }
                return _orderRepository;
            }
        }

        public IGenericRepository<Category> CategoryRepository
        {
            get
            {

                if (_categoryRepository == null)
                {
                    _categoryRepository = new GenericRepository<Category>(_context);
                }
                return _categoryRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}