using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Zulu_Project.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string CompanyCode { get; set; }
        public byte[]? Image { get; set; }
        public int? Employees { get; set; }
        [Display(Name = "Date of Establishment")]
        public DateTime? Created { get; set; }
        public bool? Deleted { get; set; }
    }

}
