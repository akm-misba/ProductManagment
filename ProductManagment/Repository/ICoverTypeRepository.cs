using static ProductManagment.ProductManagmentModel;

namespace ProductManagment.Repository
{
    public interface ICoverTypeRepository: IRepository<CoverType>
    {
        void Update(CoverType obj);
    }
}
