using BusinessObjects;
using DataAccessLayers.UnitOfWork;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class CinemaRoomService(IUnitOfWork unitOfWork) : GenericService<CinemaRoom>(unitOfWork), ICinemaRoomService
    {
    }
}
