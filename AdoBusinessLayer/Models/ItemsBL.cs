using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AdoBusinessLayer.Models
{
    public class ItemsBL
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Product Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Product Description")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Product Price")]
        public decimal Price { get; set; }
    }
}
