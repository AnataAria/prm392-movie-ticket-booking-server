using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dtos.Promotion
{
    public class PromotionDto
    {
        public int Id { get; set; }

        public int? Condition { get; set; }

        public double? Discount { get; set; }
    }
}
