using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportApi.Model;
using ReportApi.Util;

namespace ReportApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        public readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("myreport")]
        public async Task<IActionResult> GetReport([FromQuery] DateTime startDate, DateTime endDate, int pageNumber = 1, int pageSize =10, string searchText = null)
        {
            try
            {
                var report = await _reportService.GetReports(startDate, endDate, pageNumber, pageSize, searchText);
                int totalCount = report != null && report.Any() ? report.FirstOrDefault().TotalCount : 0;

                return Ok(new {
                    responseCode = ResponseCode.Success,
                    totalCount,
                    pageNumber,
                    pageSize,
                    data = report?.Select(x=> new
                    {
                        x.TransactionId,
                        x.CustomerId,
                        x.CustomerName,
                        x.CustomerAddress,
                        x.Amount,
                        x.PaymentDate,
                        x.Comment
                    })
                });
            }
            catch (AppException ex)
            {
                return BadRequest(new { responseCode = ResponseCode.UnexpectedError, responseDescription = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new {responseCode = ResponseCode.UnexpectedError, responseDescription="An unexpected error occurred. Please try again"});
            }
        }


        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard([FromQuery] DateTime startDate, DateTime endDate)
        {
            try
            {
                if (startDate > endDate)
                    throw new AppException("StartDate cannot be greater than endDate");

                var transactionCount = await _reportService.GetTotalTransactions(startDate, endDate);


                return Ok(new {
                    responseCode = ResponseCode.Success,
                    TotalTransactions = transactionCount
                });
            }
            catch (AppException ex)
            {
                return BadRequest(new { responseCode = ResponseCode.InvalidRequest, responseDescription = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { responseCode = ResponseCode.UnexpectedError, responseDescription = "An unexpected error occurred. Please try again" });
            }
        }

    }
}
