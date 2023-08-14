using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Zulu_Project.DTO
{
    public class BranchDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public CompanyDTO Company { get; set; }
        [Required]
        public int CompanyId { get; set; }
        public DateTime? Established { get; set; }
        public bool? Deleted { get; set; }

    }
}
