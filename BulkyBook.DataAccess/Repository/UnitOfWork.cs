using BulkyBook.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICategoryRepository CategoryRepository { get; private set; }
        public ICoverTypeRepository CoverTypeRepository { get; private set; }
        public IProductRepository ProductRepository { get; private set; }
        public ICompanyUserRepository CompanyUserRepository { get; private set; }
        public IApplicationUserRepository ApplicationUserRepository { get; private set; }
        public IShoppingCartRepository ShoppingCartRepository { get; private set; }
        public IOrderHeaderRepository OrderHeaderRepository { get; private set; }
        public IOrderDetailRepository OrderDetailRepository { get; private set; }


        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            CategoryRepository = new CategoryRepository(_db);
            CoverTypeRepository = new CoverTypeRepository(_db);
            ProductRepository = new ProductRepository(_db);
            CompanyUserRepository = new CompanyUserRepository(_db);
            ApplicationUserRepository = new ApplicationUserRepository(_db);
            ShoppingCartRepository = new ShoppingCartRepository(_db);
            OrderHeaderRepository = new OrderHeaderRepository(_db);
            OrderDetailRepository = new OrderDetailRepository(_db);
        }


        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
