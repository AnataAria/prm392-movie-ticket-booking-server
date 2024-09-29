using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ICategoryService : IGenericService<Category>
    {
        public Task<Category?> getByCateName(string name);
    }
}
