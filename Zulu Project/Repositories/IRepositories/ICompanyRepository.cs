using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Zulu_Project.Models;

namespace Zulu_Project.Repositories.IRepositories
{
   public interface ICompanyRepository
    {
        Task<List<Company>> GetAll();
        Task<Company> GetById();
        Task<Company> Create(Company Company);
        Task<Company> Update(Company Company);
        Task<bool> IsExsited(int Id);
        Task<bool> IsExsited(string Name);
        Task<Company> Delete(int Id);
        Task<bool> Save();
    }
}
