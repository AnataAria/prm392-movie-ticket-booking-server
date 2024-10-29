using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayers
{
    public class CinemaRoomRepository(Prn221projectContext context) : GenericRepository<CinemaRoom>(context)
    {
    }
}
