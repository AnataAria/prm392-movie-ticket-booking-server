using BusinessObjects;
using DataAccessLayers;
using DataAccessLayers.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class CategoryService : GenericService<Category>, ICategoryService
    {
        private readonly GenericRepository<Category> _categoryDAO;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(GenericRepository<Category> categoryDAO, IUnitOfWork unitOfWork) : base(categoryDAO)
        {
            _categoryDAO = categoryDAO;
            _unitOfWork = unitOfWork;
        }
        public async Task<Category?> getByCateName(string name)
        {
            return await _unitOfWork.CategoryDAO.getByCateName(name);
        }
    }
}