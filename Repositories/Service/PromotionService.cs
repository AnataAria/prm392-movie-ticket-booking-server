using BusinessObjects;
using DataAccessLayers.UnitOfWork;
using Services.Interface;

namespace Services.Service
{
    public class PromotionService(IUnitOfWork unitOfWork) : GenericService<Promotion>(unitOfWork), IPromotionService
    {

        public async Task<Promotion> CheckDiscount(int? quantity)
        {
            var promotion = new Promotion();
            if (quantity >= 10 && quantity < 20) promotion = await _unitOfWork.PromotionRepository.FindOneAsync(a => a.Condition == 10);
            if (quantity >= 20) promotion = await _unitOfWork.PromotionRepository.FindOneAsync(a => a.Condition == 20);
            if(quantity < 10) promotion = await _unitOfWork.PromotionRepository.FindOneAsync(a => a.Condition == 0);
            if (promotion == null) throw new Exception("no promotion was found");
            return promotion;
        }
    }
}
