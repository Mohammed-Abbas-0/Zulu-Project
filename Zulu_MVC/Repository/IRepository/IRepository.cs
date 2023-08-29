using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zulu_MVC.Repository.IRepository
{
    public interface IRepository<T> where T :class
    {
        Task<IEnumerable<T>> GetAllAsync(string url);
        Task<T> GetAsync(string url,int Id);
        Task<bool> CreateAync(string url,T objToCreate);
        Task<bool> UpdateAync(string url,T objToUpdate);
        Task<bool> DeleteAync(string url,int Id);
    }
}
