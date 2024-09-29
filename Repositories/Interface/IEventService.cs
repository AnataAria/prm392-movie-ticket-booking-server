using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IEventService : IGenericService<Event>
    {
        Task<IEnumerable<Event>> GetAllInclude();
        Task<IEnumerable<Event>> GetAllIncludeType();
    }
}
