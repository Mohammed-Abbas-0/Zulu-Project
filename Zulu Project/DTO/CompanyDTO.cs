using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Zulu_Project.DTO
{
    public class CompanyDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string CompanyCode { get; set; }
        public int? Employees { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public bool? Deleted { get; set; }
    }

}
