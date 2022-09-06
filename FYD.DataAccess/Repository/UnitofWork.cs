using FYD.DataAccess.Data;
using FYD.DataAccess.Repository.IRepository;
using FYD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYD.DataAccess.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private readonly AppDbContext _db;
        public UnitofWork(AppDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            DrinkType = new DrinkTypeRepository(_db);
            Product = new ProductRepository(_db);
            Company = new CompanyRepository(_db);
        }
        public ICategoryRepository Category { get; private set; }
        public IDrinkTypeRepository DrinkType { get; private set; }
        public IProductRepository Product { get; private set; }
        public ICompanyRepository Company { get; private set; }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
