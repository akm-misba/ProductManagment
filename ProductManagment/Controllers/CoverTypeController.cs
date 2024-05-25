using Microsoft.AspNetCore.Mvc;
using ProductManagment.Repository;
using static ProductManagment.ProductManagmentModel;

namespace ProductManagment.Controllers
{
    public class CoverTypeController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        //private readonly CategoryRepository _categoryRepository;
        public CoverTypeController(IUnitOfWork unitOfWork)/*, CategoryRepository categoryRepository)*/
        {
            _unitOfWork = unitOfWork;
            //_categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            IEnumerable<CoverType> list = _unitOfWork.CoverType.GetAll();


            return View(list);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType coverType)
        {
            //if (coverType.Name == coverType.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "Same Name Plz Change");
            //}
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(coverType);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(coverType);
        }
        public IActionResult Edit(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }
            //_unitOfWork.Find(id);
            var k = _unitOfWork.CoverType.GetFirstOrDefult(v => v.Id == id);
            //var k = _unitOfWork.Category.SingleOrDefult(v => v.Id == id);

            return View(k);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType coverType)
        {


            //if (coverType.Name == coverType.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "Same Name Plz Change");
            //}
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(coverType);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(coverType);
        }
    }
}
