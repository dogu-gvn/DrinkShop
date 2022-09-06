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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _db;

        public ProductRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                obj.MadeFrom = objFromDb.MadeFrom;
                objFromDb.Price = obj.Price;
                objFromDb.Price20 = obj.Price20;               
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Description = obj.Description;
                objFromDb.CategoryId = obj.CategoryId;                
                objFromDb.DrinkTypeId = obj.DrinkTypeId;
                if (objFromDb.ImageUrl != null)
                {
                    objFromDb.ImageUrl = objFromDb.ImageUrl;
                }


            }
        }
        
    }
}
