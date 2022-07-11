using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class CompanyUserRepository : Repository<CompanyUser>, ICompanyUserRepository
    {
        private ApplicationDbContext _db;

        public CompanyUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CompanyUser obj)
        {
            _db.CompanyUsers.Update(obj);
        }
    }
}
