using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Application.DTOs.PaymentTransaction
{
    public class SslCommerzSuccessDto
    {
        public string tran_id { get; set; } = null!;
        public string status { get; set; } = null!;
        public string? plan_id { get; set; } 
        public string? card_no { get; set; } 
        public string? amount { get; set; }
        public string? currency { get; set; }
        public string? bank_tran_id { get; set; }
        public string? card_type { get; set; }
        public string? card_issuer { get; set; }
        public string? card_brand { get; set; }
        public string? card_issuer_country { get; set; }
        public string? risk_level { get; set; }
        public string? risk_title { get; set; }
        public string? verify_sign { get; set; }
    }

}
