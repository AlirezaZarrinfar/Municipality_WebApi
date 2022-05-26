using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Municipality_WebApi.Application.Services.BillsService;
using Municipality_WebApi.Application.Services.CustomerService;
using Municipality_WebApi.Application.UnitOfWork;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Municipality_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        IUnitofwork Unitofwork;
        IDatabase database;
        public BillsController(IUnitofwork Unitofwork,IDatabase database)
        {
            this.database = database;
            this.Unitofwork = Unitofwork;
        }
        [HttpGet("GetBillAndCustomer")]
        public IActionResult getBillandCustomer(long billId)
        {
            var res = Unitofwork.billsService.showBillandCustomer(billId);
            return Ok(res);
        }
        [HttpGet("GetAllBills")]
        public IActionResult getAllBills()
        {
            if (database.StringGet("bills").IsNull)
            {
                database.StringSet("bills", Unitofwork.billsService.showAllBills());
                database.KeyExpire("bills", TimeSpan.FromSeconds(45));
            }
            return Ok(database.StringGet("bills"));
        }
        [HttpGet("SuccessAndFailedCount")]
        public IActionResult SuccessAndFailedCount(int customerId)
        {
            var res = Unitofwork.billsService.SuccessAndFailedCount(customerId);
            return Ok(res);
        }
        //Sp_TotalPrice
        [HttpGet("GetTotalPrice")]
        public IActionResult GetTotalPrice()
        {
            var res = Unitofwork.billsService.getTotalCount();
            return Ok(res);
        }
    }
}