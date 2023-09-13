using Microsoft.VisualBasic;
using MVC_Webapp.DTOs.InformationDTOs;
using System.Linq.Expressions;

namespace MVC_Webapp.Repositories
{
    public interface IGenericRepos
    {
        Task<List<T>> GetAll<T>() where T : class;
        Task<T> GetById<T>( int id) where T : class;
        Task<T> GetByName<T>( Expression<Func<T, bool>> ForUser) where T : class;
        Task<T> AddInfo<T>(T tObj) where T : class;
        Task UpdateInfo<T>( T tObj) where T : class;
        Task DeleteInfo<T>( T tObj ) where T : class;
        Task<List<T>> GetUserData<T>(Expression<Func<T, bool>> ForUser) where T : class;
    }
}
