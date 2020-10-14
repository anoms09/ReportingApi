using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportApi.Model
{
    public class ResponseCode
    {
        public static readonly string Success = "AA00";
        public static readonly string InvalidRequest = "AA01";
        public static readonly string UnexpectedError = "AA02";
        public static readonly string Forbidden = "AA03";
        public static readonly string NotFound = "AA04";
        public static readonly string Unauthorized = "AA05";
    }
}
