using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Municipality_WebApi.Application.Services.BillsService;
using Municipality_WebApi.Application.Services.CustomerService;
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
        IBillsService _billsService;
        public BillsController(IBillsService billsService)
        {
            _billsService = billsService;
        }
        [HttpPost("CreateRandomBills")]
        public IActionResult addBills()
        {
            var res = _billsService.addBills();
            return Ok(res);
        }
        [HttpGet("GetBillAndCustomer")]
        public IActionResult getBillandCustomer(long billId)
        {
            var res = _billsService.showBillandCustomer(billId);
            return Ok(res);
        }
        [HttpGet("GetAllBills")]
        public IActionResult getAllBills()
        {
            var res = _billsService.showAllBills();
            return Ok(res);
        }
        [HttpGet("TFCount")]
        public IActionResult TFCount(int customerId)
        {
            var res = _billsService.TFCount(customerId);
            return Ok(res);
        }
        //Sp_TotalPrice
        [HttpGet("GetTotalPrice")]
        public IActionResult GetTotalPrice()
        {
            var res = _billsService.getTotalCount();
            return Ok(res);
        }
    }
}