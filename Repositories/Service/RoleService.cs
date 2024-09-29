using BusinessObjects;
using DataAccessLayers;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class RoleService : GenericService<Role>, IRoleService
    {
        private readonly GenericRepository<Role> _roleDAO;

        public RoleService(GenericRepository<Role> roleDAO) : base(roleDAO)
        {
            _roleDAO = roleDAO;
        }
    }
}
