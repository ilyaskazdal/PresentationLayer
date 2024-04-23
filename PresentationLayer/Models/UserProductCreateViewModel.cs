using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class UserProductCreateViewModel
    {
        [Required]
        [Display(Name = "ProductName")]
        public string ProductPostName { get; set; }

        [Required]
        [Display(Name = "ProductDescription")]
        public string ProductPostDescription { get; set; }

       
    }
}
