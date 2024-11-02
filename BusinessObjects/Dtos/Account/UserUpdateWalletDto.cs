using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Dtos.Account
{
    public class UserUpdateWalletDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public double? Wallet { get; set; }
    }
}
