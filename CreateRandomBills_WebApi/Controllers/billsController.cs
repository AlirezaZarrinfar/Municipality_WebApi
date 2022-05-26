using CreateBills_WebApi.Application.Services.CreateBillsService;
using CreateBills_WebApi.Application.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreateBills_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class billsController : ControllerBase
    {
        IUnitofwork Unitofwork;
        public billsController(IUnitofwork Unitofwork)
        {
            this.Unitofwork = Unitofwork;
        }
        [HttpPost("create")]
        public IActionResult createBills()
        {
            var res = Unitofwork.createBillsService.addBills();
            return Ok(res);
        }
    }
}
