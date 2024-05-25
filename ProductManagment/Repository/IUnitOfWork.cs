namespace ProductManagment.Repository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        ICoverTypeRepository CoverType { get; } 

        void Save();
    }
}
