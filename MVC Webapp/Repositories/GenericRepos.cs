using MVC_Webapp.Data;
using Microsoft.EntityFrameworkCore;
using MVC_Webapp.Models;
using Microsoft.AspNetCore.Mvc;
using MVC_Webapp.DTOs.InformationDTOs;

namespace MVC_Webapp.Repositories
{
    public class GenericRepos : IGenericRepos
    {
        private readonly MVC_WebappContext _context;
        public GenericRepos(MVC_WebappContext context)
        {
            _context = context;
        }
        public async Task<List<T>> GetAll<T>() where T : class
        {
            if (_context.Set<T>() == null)
            {
                return null;
            }

            return await this._context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById<T>(int id) where T : class
        {
            if (_context.Set<T> () == null)
            {
                throw new Exception("Not Found");
            }

            return await this._context.Set<T>().FindAsync(id);
        }

        public async Task<T> AddInfo<T>(T tObj) where T : class
        {
            if (_context.Set<T> () == null)
            {
                return null;
            }
            _context.Set<T>().Add(tObj);
            await _context.SaveChangesAsync();
            return tObj;
        }

        public async Task UpdateInfo<T>(T tObj) where T : class
        {
            if (_context.Set<T>() == null)
            {
                 throw new Exception("Bad Request");
            }
            _context.Set<T>().Update(tObj);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInfo<T>(T tObj) where T : class
        {
            if (_context.Set<T>() == null)
            {
                 throw new Exception("Bad Request");
            }
            _context.Set<T>().Remove(tObj);
            await _context.SaveChangesAsync();
        }
    }
}
