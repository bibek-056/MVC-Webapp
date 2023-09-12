using Microsoft.VisualBasic;
using MVC_Webapp.DTOs.InformationDTOs;

namespace MVC_Webapp.Repositories
{
    public interface IGenericRepos
    {
        Task<List<T>> GetAll<T>() where T : class;
        Task<T> GetById<T>( int id) where T : class;
        Task<T> AddInfo<T>(T tObj) where T : class;

        Task UpdateInfo<T>( T tObj) where T : class;

        Task DeleteInfo<T>( T tObj ) where T : class;
    }
}
