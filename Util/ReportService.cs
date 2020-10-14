using ReportApi.Model;
using ReportApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ReportApi.Util
{
    public interface IReportService
    {
        Task<IEnumerable<ReportListDto>> GetReports(DateTime startDate, DateTime endDate, int pageNumber, int pageSize, string searchText);
        Task<long> GetTotalTransactions(DateTime startDate, DateTime endDate);
        Task<IEnumerable<BarChartData>> GetBarChartData(DateTime startDate, DateTime endDate);
    }
    public class ReportService : IReportService
    {
        public IReportRepository _reportRepository;
        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<IEnumerable<BarChartData>> GetBarChartData(DateTime startDate, DateTime endDate)
        {
            return await _reportRepository.GetBarChartData(startDate, endDate);
        }

        public async Task<IEnumerable<ReportListDto>> GetReports(DateTime startDate, DateTime endDate, int pageNumber, int pageSize, string searchText)
        {

            if (startDate > endDate)
                throw new AppException("StartDate Cannot be greater than enddate");

            return await _reportRepository.GetReports(startDate, endDate, pageNumber, pageSize, searchText);
        }

        public async Task<long> GetTotalTransactions(DateTime startDate, DateTime endDate)
        {
            return await _reportRepository.GetTotalTransactions(startDate, endDate);
        }
    }

}
