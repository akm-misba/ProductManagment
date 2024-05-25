using ProductManagment.Data;
using static ProductManagment.ProductManagmentModel;

namespace ProductManagment.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private readonly ApplicationDbContext _context;
        public CoverTypeRepository(ApplicationDbContext context) : base(context)

        {
            _context = context;
        }

        //public void Save()
        //{
        //    _context.SaveChanges();
        //}

        public void Update(CoverType obj)
        {
            _context.CoverTypes.Update(obj);
        }
    }
}
