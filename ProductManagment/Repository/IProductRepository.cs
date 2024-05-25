using static ProductManagment.ProductManagmentModel;

namespace ProductManagment.Repository
{
    public interface IProductRepository:IRepository<Product>
    {
        void Update(Product obj);
    }
}
