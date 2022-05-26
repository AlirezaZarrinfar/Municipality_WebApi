using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Municipality_WebApi.Application.Services.BillsPaymentService;
using Municipality_WebApi.Application.UnitOfWork;
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
        IUnitofwork Unitofwork;
        public BillsPaymentController(IUnitofwork Unitofwork)
        {
            this.Unitofwork = Unitofwork;
            
        }
        [HttpPost("PayBillById/{billId}")]
        public IActionResult PayBillById(long billId)
        {
            var res = Unitofwork.billsPaymentService.payBillWithId(billId);
            return Ok(res);
        }
        [HttpPost("PayMultiBillsWithId/{billsId}")]
        public IActionResult PayMultiBillsWithId(string billsId)
        {
            var res = Unitofwork.billsPaymentService.payMultiBillsWithId(billsId);
            return Ok(res);
        }
        [HttpPost("payBillsWithCustomerId/{customerId}")]
        public IActionResult payBillsWithCustomerId(long customerId)
        {
            var res = Unitofwork.billsPaymentService.payBillsWithCustomerId(customerId);
            return Ok(res);
        }
    }
}
