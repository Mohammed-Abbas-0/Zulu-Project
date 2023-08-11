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
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDBContext _context;
        public CompanyRepository(AppDBContext context)
        {
            _context = context;
        }

        #region C R E A T E
        public async Task<Company> Create(Company company)
        {
           await _context.Companies.AddAsync(company);
            if(await Save())
                return company;
           else 
                throw new Exception("Wrong To Save Company");
        }
        #endregion

        #region  Delete Company
        public async Task<Company> Delete(Company company)
        {
            _context.Companies.Remove(company);
            if (await Save())
                return company;
            else
                throw new Exception("Wrong To Delete Company");
        }

        public Task<Company> Delete(int Id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region  Get All Companies
        public async Task<List<Company>> GetAll()
        {
            // IQueryable<Company> GetCompanies=  _context.Companies.Where(idx=>idx.Deleted != true).AsNoTracking().AsSplitQuery().AsQueryable();
            List<Company> GetCompanies = await _context.Companies.Where(idx => idx.Deleted != true).AsNoTracking().OrderByDescending(idx=>idx.Id).ToListAsync();
            return GetCompanies;
        }
        #endregion

        #region  Get All Company By Id
        public async Task<Company> GetById(int Id)
        {
            Company GetCompanies = await _context.Companies.Where(idx => idx.Deleted != true && idx.Id == Id).AsNoTracking().FirstOrDefaultAsync();
            return GetCompanies;
        }

        public Task<Company> GetById()
        {
            throw new NotImplementedException();
        }
        #endregion
    

        #region  Check If Exsited
        public async Task<bool> IsExsited(int Id) =>
            await _context.Companies.Where(idx => idx.Deleted != true && idx.Id == Id).AnyAsync();

        public async Task<bool> IsExsited(string CompanyName) =>
             await _context.Companies.Where(idx => idx.Deleted != true && idx.CompanyName.Contains(CompanyName.Trim())).AnyAsync();
        #endregion

        #region Save Data on Db
        public async Task<bool> Save() => await _context.SaveChangesAsync() >= 0 ? true : false;
        #endregion

        #region U P D A T E
        public async Task<Company> Update(Company company)
        {
             _context.Companies.Update(company);
            if (await Save())
                return company;
            else
                throw new Exception("Wrong To Save Company");
        }
        #endregion


        /*
    public async Task<bool> IsExsited(Expression<Func<T, bool>> filter = null)
    {
        if (filter is null)
             throw new Exception("Please Use Filter To Check if this Existed before");
        var CheckIsExisted = await _context.Set<T>().Where(filter).FirstOrDefaultAsync();
        if (CheckIsExisted is null)
            return false;
        return true;
    }
    */
    }
}
