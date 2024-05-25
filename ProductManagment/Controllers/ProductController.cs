using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging.Signing;
using NuGet.Protocol.Plugins;
using ProductManagment.Repository;
using System.IO;
using static ProductManagment.ProductManagmentModel;

namespace ProductManagment.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IWebHostEnvironment _webHostEnvironment;
        //private readonly CategoryRepository _categoryRepository;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)/*, CategoryRepository categoryRepository)*/
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            //_categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            //IEnumerable<Product> list = _unitOfWork.Product.GetAll();


            return View();
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (product.Title == product.CategoryId.ToString())
            {
                ModelState.AddModelError("name", "Same Name Plz Change");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(product);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }
        public IActionResult Edit(int? id)
        {
            ProductVM productVm = new()
            {
                product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };
            if (id == null || id == 0)
            {
                //ViewData["CoverTypeList"] = CoverTypeList;
                //ViewBag.CategoryList=CategoryList;
                return View(productVm);
            }
            else
            {
                productVm.product=_unitOfWork.Product.GetFirstOrDefult(v => v.Id == id);
                //return View(productVm);
            }
            //_unitOfWork.Find(id);
            //var k = _unitOfWork.Product.GetFirstOrDefult(v => v.Id == id);
            //var k = _unitOfWork.Category.SingleOrDefult(v => v.Id == id);

            return View(productVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product, IFormFile? file)
        {
            //Product d= new Product();
            //d.Title = product.Title;
            //d.ISBN = product.ISBN;
            //d.Author= product.Author;
            //d.Description = product.Descriptio
            //Product P=new();
            //if (Product.product.Title == Product.product.CategoryId.ToString())
            //{
            //    ModelState.AddModelError("name", "Same Name Plz Change");
            //}
            if (ModelState.IsValid)
            {
                string wwwRoot = _webHostEnvironment.WebRootPath;
                if(wwwRoot != null)
              
                {
                    string fileName=Guid.NewGuid().ToString();
                    var uploads=Path.Combine(wwwRoot,@"Images\Products");
                    if (!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }
                  var extesion=Path.GetExtension(file.FileName);
                    //var extesion = Path.GetExtension(file[0].FileName);
                    if (product.ImageUrl != null)
                  {
                    var oldImage=Path.Combine(wwwRoot, product.ImageUrl.TrimStart('\\'));
                    if(System.IO.File.Exists(oldImage))
                    {
                        System.IO.File.Delete(oldImage);
                    }
                  }

                  
                      using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extesion), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        //gtr
                         fileStream.Close();
                      }
                    
                    product.ImageUrl = @"\Images\Products\" + fileName + extesion;
                    
                    if(product.Id == 0)
                    {
                        _unitOfWork.Product.Add(product);
                    }
                    else
                    {
                        _unitOfWork.Product.Update(product);

                    }

                }
                //_unitOfWork.Product.Update(Product.product);
                _unitOfWork.Save();
                TempData["succees"] = "Product Create Sucessfully";
                return RedirectToAction(nameof(Index));
           }

            return View(product);
        }
        #region API CAll
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList=_unitOfWork.Product.GetAll();
            return Json(new { data = productList });
        }
        [HttpDelete] 
        public IActionResult Delete(int id) { 
            var productList=_unitOfWork.Product.GetFirstOrDefult(u=>u.Id==id);
            if (productList == null)
            {
                return Json(new { Success = 0, Message = "Not Delete" });
            }

            var oldImage = Path.Combine(_webHostEnvironment.WebRootPath, productList.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImage))
                {
                    System.IO.File.Delete(oldImage);
                }
            
            _unitOfWork.Product.Remove(productList);
            _unitOfWork.Save();
            return Json(new { Success = 1, Message = "delete sucessfully " });
        }
        #endregion

    }
}
