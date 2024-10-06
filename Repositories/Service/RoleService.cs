using BusinessObjects;
using DataAccessLayers;
using Services.Interface;
using System;
using System.Collections.Generic;
using DataAccessLayers.UnitOfWork;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class RoleService(IUnitOfWork unitOfWork) : GenericService<Role>(unitOfWork), IRoleService
    {
    }
}
