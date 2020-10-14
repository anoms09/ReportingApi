using Dapper;
using Microsoft.Extensions.Configuration;
using ReportApi.Interface;
using ReportApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ReportApi.Repository
{
    public interface IReportRepository
    {
        Task<IEnumerable<ReportListDto>> GetReports(DateTime startDate, DateTime endDate, int pageNumber, int pageSize, string search);
        Task<long> GetTotalTransactions(DateTime startDate, DateTime endDate);
        Task<IEnumerable<BarChartData>> GetBarChartData(DateTime startDate, DateTime endDate);
    }
    public class ReportRepository : IReportRepository
    {
        private readonly string _defaultConnectionString;

        public ReportRepository(IAppConfiguration configuration)
        {
            _defaultConnectionString = configuration.ReportDbConnectionString;
        }

        public async Task<IEnumerable<BarChartData>> GetBarChartData(DateTime startDate, DateTime endDate)
        {
            await using var connection = new SqlConnection(_defaultConnectionString);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var count = await connection.QueryAsync<BarChartData>(
                @"get_transaction_count_per_institution", new
                {
                    startDate,
                    endDate
                }, commandType: CommandType.StoredProcedure);
            return count;
        }

        public async Task<IEnumerable<ReportListDto>> GetReports(DateTime startDate, DateTime endDate, int pageNumber, int pageSize, string search)
        {
            await using var connection = new SqlConnection(_defaultConnectionString);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var report = await connection.QueryAsync<ReportListDto>(
                @"usp_search_report", new
                {
                    pageNumber,
                    pageSize,
                    search,
                    startDate,
                    endDate
                }, commandType: CommandType.StoredProcedure);
            return report;
        }

        public async Task<long> GetTotalTransactions(DateTime startDate, DateTime endDate)
        {
            await using var connection = new SqlConnection(_defaultConnectionString);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var count = await connection.QueryFirstOrDefaultAsync<long>(
                @"get_transaction_count", new
                {
                    startDate,
                    endDate
                }, commandType: CommandType.StoredProcedure);
            return count;
        }
    }
}
