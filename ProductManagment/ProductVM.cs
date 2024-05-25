using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using static ProductManagment.ProductManagmentModel;

namespace ProductManagment
{
    public class ProductVM
    {
        public Product product {  get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList {  get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CoverTypeList { get; set; }
    }
}
