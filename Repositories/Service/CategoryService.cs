using BusinessObjects;
using DataAccessLayers;
using DataAccessLayers.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Services.Interface;

namespace Services.Service
{
    public class CategoryService(IUnitOfWork unitOfWork) : GenericService<Category>(unitOfWork), ICategoryService
    {
        public async Task<Category?> getByCateName(string name)
        {
            return await _unitOfWork.CategoryRepository.GetByCateName(name);
        }
    }
}