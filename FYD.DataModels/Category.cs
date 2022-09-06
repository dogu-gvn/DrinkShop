using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYD.Models
{
    public class Category
    {
        //Code First Migration
        // Write what we want in data base

        [Key]//Data Annotations
        public int Id { get; set; }// we make Id primary key
        [Required]
        public string Name { get; set; }//Name not a nullable property
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display Order Must be between 1 and 100 only")]// it must be between 1 and 100 if its not than return error
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;//giving Default value
    }
}
