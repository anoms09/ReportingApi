using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportApi.Interface
{
    public interface IAppConfiguration
    {

        string ReportDbConnectionString { get; }
    }
}
