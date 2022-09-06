using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FYD.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class//Generic
    {
        //T== Category Hipoteticly
        T GetFirstorDefault(Expression<Func<T, bool>> filter, string? includeProperties = null);// T = generic class out putwil be bool -- var categoryFromDb = _db.Categories.FirstOrDefault(c => c.Id == id);
        IEnumerable<T> GetAll(string? includeProperties = null); //var objCategoryList = _db.Categories;
        void Add(T entity); // _db.Categories.Add(obj);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
