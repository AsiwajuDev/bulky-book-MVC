using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BulkyBook.Models
{
    public class CoverTypes
    {
        [Key]
        public int Id { get; set; }

        [Display(Name =("Cover Types"))]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
