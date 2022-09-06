using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYD.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        
        [Required]
        public string MadeFrom { get; set; }
        [Required]
        [Range(1, 1000)]
        public int ListPrice { get; set; }
        [Required]
        [Range(1, 10000)]
        public int Price { get; set; }
        [Required]
        [Range(1, 10000)]
        public int Price20 { get; set; }        
        [ValidateNever]
        public string ImageUrl { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
        [Required]
        public int DrinkTypeId { get; set; }
        [ValidateNever]
        public DrinkType DrinkType { get; set; }
    }
}
