using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zulu_MVC
{
    public class StaticApi
    {
        public static string APIBaseUrl = "https://localhost:44342/"; 
        public static string CompanyAPIBaseUrl = APIBaseUrl + "api/v1/Company"; 
        public static string BranchAPIBaseUrl = APIBaseUrl + "api/v1/Branch"; 
    }
}
