using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FYD.Models;

namespace FYD.DataAccess.Repository.IRepository
{
    public interface IDrinkTypeRepository : IRepository<DrinkType>
    {
        void Update(DrinkType obj);
    }
}
