using static ProductManagment.ProductManagmentModel;

namespace ProductManagment.Repository
{
    public interface ICategoryRepository:IRepository<Category>
    {
    
        void Update(Category obj);
        //void Save();
    }
}
