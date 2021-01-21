using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GidaGkpWeb.Models.Masters
{
    public class ApplicationDetailModel
    {
        public string ApplicationNumber { get; set; }
        public string FullApplicantName { get; set; }
        public string CAddress { get; set; }
        public string Mobile { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? ApplicationFee { get; set; }
        public decimal? GST { get; set; }
        public decimal? EarnestMoneyDeposit { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}