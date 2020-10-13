using Microsoft.Extensions.Configuration;
using ReportApi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportApi.Util
{
    public class AppConfiguration : IAppConfiguration
    {
        private readonly IConfiguration _configuration;

        public AppConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ReportDbConnectionString => _configuration["ConnectionStrings:DefaultConnectionString"];

    }
}
