using DataAccessLayers;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly GenericRepository<T> _genericDAO;

        public GenericService(GenericRepository<T> genericDAO)
        {
            _genericDAO = genericDAO;
        }

        public async Task<T?> GetById(int id) { return await _genericDAO.GetByIdAsync(id); }
        public async Task<IEnumerable<T>> GetAll() { return await _genericDAO.GetAllAsync(); }
        public async Task<T> Add(T entity) { return await _genericDAO.AddAsync(entity); }
        public async Task Update(T entity) { await _genericDAO.UpdateAsync(entity); }
        public async Task Delete(int id) { await _genericDAO.DeleteAsync(id); }
    }
}
