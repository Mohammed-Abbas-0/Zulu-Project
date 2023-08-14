using AspCoreApI_Project.AppContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Zulu_Project.Models;
using Zulu_Project.Repositories.IRepositories;

namespace Zulu_Project.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly AppDBContext _context;
        public BranchRepository(AppDBContext context)
        {
            _context = context;
        }

        #region C R E A T E
        public async Task<Branch> Create(Branch branch)
        {
           await _context.Branches.AddAsync(branch);
            if(await Save())
                return branch;
           else 
                throw new Exception("Wrong To Save Branch");
        }
        #endregion

        #region  Delete Branch
        public async Task<Branch> Delete(Branch branch)
        {
            _context.Branches.Remove(branch);
            if (await Save())
                return branch;
            else
                throw new Exception("Wrong To Delete Branch");
        }

        public Task<Branch> Delete(int Id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region  Get All Branches
        public async Task<List<Branch>> GetAll()
        {
            // IQueryable<Branch> GetBranches=  _context.Branches.Where(idx=>idx.Deleted != true).AsNoTracking().AsSplitQuery().AsQueryable();
            List<Branch> GetBranches = await _context.Branches.Include(idx=>idx.Company).Where(idx => idx.Deleted != true).AsNoTracking().OrderByDescending(idx=>idx.Id).ToListAsync();
            return GetBranches;
        }
        #endregion

        #region  Get  Branch By Id
        public async Task<Branch> GetById(int Id)
        {
            Branch GetBranches = await _context.Branches.Include(idx => idx.Company).Where(idx => idx.Deleted != true && idx.Id == Id).AsNoTracking().FirstOrDefaultAsync();
            return GetBranches;
        }

        public Task<Branch> GetById()
        {
            throw new NotImplementedException();
        }
        #endregion
    

        #region  Check If Exsited
        public async Task<bool> IsExsited(int Id) =>
            await _context.Branches.Where(idx => idx.Deleted != true && idx.Id == Id).AnyAsync();

        public async Task<bool> IsExsited(string BranchName) =>
             await _context.Branches.Where(idx => idx.Deleted != true && idx.Name.Contains(BranchName.Trim())).AnyAsync();
        #endregion

        #region Save Data on Db
        public async Task<bool> Save() => await _context.SaveChangesAsync() >= 0 ? true : false;
        #endregion

        #region U P D A T E
        public async Task<Branch> Update(Branch branch)
        {
             _context.Branches.Update(branch);
            if (await Save())
                return branch;
            else
                throw new Exception("Wrong To Save Branch");
        }
        #endregion


    }
}
