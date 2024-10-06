using BusinessObjects;

namespace DataAccessLayers
{
    public class SolvedTicketRepository(Prn221projectContext context) : GenericRepository<SolvedTicket>(context)
    {
    }
}
