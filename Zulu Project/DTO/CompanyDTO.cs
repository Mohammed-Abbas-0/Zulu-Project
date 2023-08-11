using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zulu_Project.DTO
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public int? Employees { get; set; }
        public DateTime? Created { get; set; }
        public bool? Deleted { get; set; }
    }

}
