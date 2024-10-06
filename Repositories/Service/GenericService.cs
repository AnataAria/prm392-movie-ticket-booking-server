using DataAccessLayers;
using DataAccessLayers.UnitOfWork;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        protected readonly GenericRepository<T> _genericDAO;
        protected readonly IUnitOfWork _unitOfWork;

        public GenericService (IUnitOfWork unitOfWork) {
            this._unitOfWork = unitOfWork;
            _genericDAO = _unitOfWork.GenericRepository<T> ();
        }

        public async Task<T?> GetById(int id) { return await _genericDAO.GetByIdAsync(id); }
        public async Task<IEnumerable<T>> GetAll() { return await _genericDAO.GetAllAsync(); }
        public async Task<T> Add(T entity) { return await _genericDAO.AddAsync(entity); }
        public async Task Update(T entity) { await _genericDAO.UpdateAsync(entity); }
        public async Task Delete(int id) { await _genericDAO.DeleteAsync(id); }
    }
}
