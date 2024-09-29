using BusinessObjects;
using DataAccessLayers;
using DataAccessLayers.UnitOfWork;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class PromotionService : GenericService<Promotion>, IPromotionService
    {
        private readonly GenericRepository<Promotion> _promotionDAO;
        private readonly IUnitOfWork _unitOfWork;

        public PromotionService(GenericRepository<Promotion> promotionDAO, IUnitOfWork unitOfWork) : base(promotionDAO)
        {
            _promotionDAO = promotionDAO;
            _unitOfWork = unitOfWork;
        }

        public async Task<Promotion> CheckDiscount(int? quantity)
        {
            var promotion = new Promotion();
            if (quantity >= 10 && quantity < 20) promotion = await _unitOfWork.PromotionDAO.FindOneAsync(a => a.Condition == 10);
            if (quantity >= 20) promotion = await _unitOfWork.PromotionDAO.FindOneAsync(a => a.Condition == 20);
            if(quantity < 10) promotion = await _unitOfWork.PromotionDAO.FindOneAsync(a => a.Condition == 0);
            if (promotion == null) throw new Exception("no promotion was found");
            return promotion;
        }
    }
}
