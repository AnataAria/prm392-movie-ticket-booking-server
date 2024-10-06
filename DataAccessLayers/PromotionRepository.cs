using BusinessObjects;

namespace DataAccessLayers
{
    public class PromotionRepository(Prn221projectContext context) : GenericRepository<Promotion>(context)
    {
    }
}
