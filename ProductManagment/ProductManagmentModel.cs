using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ProductManagment
{
    public class ProductManagmentModel
    {
        public class Product
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            [Required]
            public string Title { get; set; }
            [Required]
            public string Description { get; set; }
            [Required]
            public string ISBN { get; set; }
            [Required]
            public string Author { get; set; }
            [Required]
            [Range(0, 10000)]
            public double ListPrice { get; set; }
            [Required]
            [Range(0, 10000)]
            public double Price50 { get; set; }
            [Required]
            [Range(0, 10000)]
            public double Price100 { get; set; }
            [ValidateNever] 
            public string ImageUrl { get; set; }
            [Required]
            public int CategoryId { get; set; }
            [ForeignKey(("CategoryId"))]
            [ValidateNever]
            public Category Category { get; set; }
            [Required]
            public int CoverTypeId { get; set; }
            //[ForeignKey(("CoverTypeId"))]
            [ValidateNever]
            public CoverType CoverType { get; set; }
        }

        public class Category
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int? Id { get; set; }
            [Required]
           
            public string  Name { get; set; }
            [DisplayName("Dispaly order")]
            [Range(1,100,ErrorMessage ="Value 1 to 100")]
   
            public int  DisplayOrder { get; set; }
            public DateTime CreateDateTime { get; set; }= DateTime.Now;


        }
        public class CoverType
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            [Display(Name="Cover Type Name")]
            [Required]
            [MaxLength(50)]
            public string  Name { get; set; }
            
         

        }
    }
}
