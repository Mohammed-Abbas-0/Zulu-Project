using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Zulu_Project.Models;

namespace Zulu_Project.Repositories.IRepositories
{
   public interface IBranchRepository
    {
        Task<List<Branch>> GetAll();
        Task<Branch> GetById(int Id);
        Task<Branch> Create(Branch Branch);
        Task<Branch> Update(Branch Branch);
        Task<bool> IsExsited(int Id);
        Task<bool> IsExsited(string Name);
        Task<Branch> Delete(Branch Branch);
        Task<bool> Save();
    }
}
