using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Municipality_WebApi.Application.Services.BillsPaymentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Municipality_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsPaymentController : ControllerBase
    {
        IBillsPaymentService billsPaymentService;
        public BillsPaymentController(IBillsPaymentService billsPaymentService)
        {
            this.billsPaymentService = billsPaymentService;
        }
        [HttpPost("PayBillById/{billId}")]
        public IActionResult PayBillById(long billId)
        {
            var res = billsPaymentService.payBillWithId(billId);
            return Ok(res);
        }
        [HttpPost("PayMultiBillsWithId/{billsId}")]
        public IActionResult PayMultiBillsWithId(string billsId)
        {
            var res = billsPaymentService.payMultiBillsWithId(billsId);
            return Ok(res);
        }
        [HttpPost("payBillsWithCustomerId/{customerId}")]
        public IActionResult payBillsWithCustomerId(long customerId)
        {
            var res = billsPaymentService.payBillsWithCustomerId(customerId);
            return Ok(res);
        }
    }
}
