using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYD.Models
{
    public class DrinkType
    {
        [Key]//Data Annotations
        public int Id { get; set; }// we make Id primary key
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }//Name not a nullable property
    }
}
