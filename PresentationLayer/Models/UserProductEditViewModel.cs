using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class UserProductEditViewModel
    {

        [Required]
        [Display(Name = "ProductId")]
        public string ProductPostId { get; set; }

        [Required]
        [Display(Name = "ProductName")]
        public string ProductPostName { get; set; }

        [Required]
        [Display(Name = "ProductDescription")]
        public string ProductPostDescription { get; set; }

        [Required]
        [Display(Name = "SaleState")]
        public bool StillOnSale { get; set; }

        
    }
}
