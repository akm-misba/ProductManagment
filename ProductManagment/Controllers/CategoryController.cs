using Microsoft.AspNetCore.Mvc;
using ProductManagment.Repository;
using static ProductManagment.ProductManagmentModel;

namespace ProductManagment.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDataUtility _dataUtility;
        //private readonly CategoryRepository _categoryRepository;
        public CategoryController(IUnitOfWork unitOfWork, IDataUtility dataUtility)/*, CategoryRepository categoryRepository)*/
        {
            _unitOfWork = unitOfWork;
            _dataUtility = dataUtility;
            //_categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> Index()
        {
            TempData["Message"] = "Data Load Successfully";
            TempData["Status"] = "1";
            //IEnumerable<Category> list = _unitOfWork.Category.GetAll();
            string sql = "Select * from categorys ";
            var data = await _dataUtility.GetDataAsync(sql, null/*, System.Data.CommandType.StoredProcedure*/);



            return View();
        }
        public IActionResult Create()
        {
            ViewBag.Title = "Create";
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if(category.Name==category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name","Same Name Plz Change");
            }
            if (ModelState.IsValid)
            {
                if(category.Id>0)
                {

                    if (category.Name == category.DisplayOrder.ToString())
                    {
                        ModelState.AddModelError("name", "Same Name Plz Change");
                    }
                    else
                    {
                        _unitOfWork.Category.Update(category);
                        _unitOfWork.Save();
                        return RedirectToAction(nameof(Index));
                    }

                }
                else
                {
                    _unitOfWork.Category.Add(category);
                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
              
            }

            return View(category);
        }
        public IActionResult Update(int? id)
        {
            ViewBag.Title = "Edit";
            if (id == null || id==0)
            {
                return NotFound();  
            }
            ViewBag.Title = "Edit";
            //_unitOfWork.Find(id);
            var k=_unitOfWork.Category.GetFirstOrDefult(v=>v.Id==id);
            //var k = _unitOfWork.Category.SingleOrDefult(v => v.Id == id);

            //return View(k);
            return View("Create", k);
          
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Update(Category category)
        {


            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Same Name Plz Change");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }
        public IActionResult DeleteCategory(int? id)
        {
            ViewBag.Title = "Delete";
            if (id == null)
            {
                return NotFound();
            }

            var category = _unitOfWork.Category.GetFirstOrDefult(u => u.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            //ViewBag.Title = "Delete";
            //_unitOfWork.Category.Remove(category);
            //_unitOfWork.Save();
            ViewBag.Title = "Delete";
            return View("Create",category);
        }
        [HttpPost, ActionName("DeleteCategory")]
        public IActionResult DeleteCategoryConfirmed(int? id)
        {
          
            try
            {
                var category = _unitOfWork.Category.GetFirstOrDefult(u => u.Id == id);
                _unitOfWork.Category.Remove(category);
                _unitOfWork.Save();

                TempData["Message"] = "Data Delete Successfully";
                TempData["Status"] = "3";
                //tranlog.TransactionLog(RouteData.Values["controller"].ToString(), RouteData.Values["action"].ToString(), TempData["Message"].ToString(), Cat_PoliceStation.PStationId.ToString(), "Delete", Cat_PoliceStation.PStationName);

                return Json(new { Success = 1, Id = category.Id, ex = TempData["Message"].ToString() });
            }
            catch (Exception ex)
            {
                return Json(new { Success = 0, ex = ex.InnerException.InnerException.Message.ToString() });
            }
        }
    }
}
