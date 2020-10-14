using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportApi.Model
{
    public class BarChartData
    {
        public string InstitutionName { get; set; }
        public string InstitutionCode { get; set; }
        public long Value { get; set; }
    }
}
