using System.Linq.Expressions;

namespace ProductManagment.Repository
{
    public interface IRepository<T> where T : class
    {
        T GetFirstOrDefult(Expression<Func<T,bool>>filter);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Remove(T entity);
        void Find(int id);
        void RemoveRange(IEnumerable<T> entity);
    }
}
