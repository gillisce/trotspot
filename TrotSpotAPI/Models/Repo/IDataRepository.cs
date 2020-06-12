using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrotSpotAPI.Models.Repo
{
    public interface IDataRepository<TEntity>
    {
        Task<IList<TEntity>> GetAll();
        Task<TEntity> Get(int id);
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(int id, TEntity entity);
        Task<TEntity> Delete(TEntity entity);
    }
}
