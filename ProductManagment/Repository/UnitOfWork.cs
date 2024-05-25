using ProductManagment.Data;
using static ProductManagment.ProductManagmentModel;

namespace ProductManagment.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context) 

        {
            _context = context;
            Category = new CategoryRepository(_context);
            Product = new ProductRepository(_context);
            CoverType =new CoverTypeRepository(_context);
        }
        public ICategoryRepository Category {  get; private set; }

        public IProductRepository Product { get; private set; }
        public ICoverTypeRepository CoverType { get; private set; }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
