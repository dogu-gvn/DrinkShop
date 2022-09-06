using FYD.DataAccess.Data;
using FYD.DataAccess.Repository.IRepository;
using FYD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FYD.DataAccess.Repository
{
    public class DrinkTypeRepository : Repository<DrinkType>, IDrinkTypeRepository
    {
        private readonly AppDbContext _db;

        public DrinkTypeRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(DrinkType obj)
        {
            _db.DrinkTypes.Update(obj);
        }
    }
}
