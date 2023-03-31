using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace N5Company.Business.Interfaces
{
    public interface IElasticSearchBusiness<T> where T : class
    {
        Task<bool> IndexAsync(T document);
        Task<bool> DeleteAsync(string id);
        Task<IEnumerable<T>> SearchAsync(string query);
    }
}
