using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CULTMACEDONIA_v2.Repository
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface ICultMacedoniaGenericRepo<TEntity> : IDisposable
    {
        
        IQueryable<TEntity> GetAll();
        
        Task<TEntity> GetEntityByIdAsync(int id);

        Task<int> InsertEntityAsync(TEntity entity);

        Task<int> EditEntityAsync(TEntity entity);

        Task<int> DeleteEntityAsync(TEntity entity);
    }
}
