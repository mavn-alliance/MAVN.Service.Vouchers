using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using MAVN.Service.Vouchers.Client.Api;
using MAVN.Service.Vouchers.Client.Models.Reports;
using MAVN.Service.Vouchers.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace MAVN.Service.Vouchers.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportsController : ControllerBase, IReportsApi
    {
        private readonly IReportsService _reportsService;
        private readonly IMapper _mapper;

        public ReportsController(IReportsService reportsService, IMapper mapper)
        {
            _reportsService = reportsService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns report of sold vouchers.
        /// </summary>
        /// <param name="spendRuleId">The spend rule identifier.</param>
        /// <returns>
        /// 200 - The report of sold vouchers.
        /// </returns>
        [HttpGet("spendRuleVouchers")]
        [ProducesResponseType(typeof(SpendRuleVouchersReportModel), (int) HttpStatusCode.OK)]
        public async Task<SpendRuleVouchersReportModel> GetSpendRuleVouchersAsync(Guid spendRuleId)
        {
            var report = await _reportsService.GetSpendRuleVouchersAsync(spendRuleId);

            var model = _mapper.Map<SpendRuleVouchersReportModel>(report);

            return model;
        }
    }
}
