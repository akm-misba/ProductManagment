using ProductManagment.Data;
using static ProductManagment.ProductManagmentModel;

namespace ProductManagment.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) :base(context) 

        {
            _context = context;
        }

        //public void Save()
        //{
        //    _context.SaveChanges();
        //}

        public void Update(Category obj)
        {
            _context.Categories.Update(obj);
        }
    }
}
