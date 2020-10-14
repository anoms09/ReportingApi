using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportApi.Model
{
    public class ReportList
    {
        public int TransactionId { get; set; }
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public string InstitutionCode { get; set; }
        public string InstitutionName { get; set; }
        public DateTime PaymentDate { get; set; }

    }

    public class ReportListDto : ReportList
    {
        public int TotalCount { get; set; }
    }
}
