using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Municipality_WebApi.Application.Services.CustomerService;
using Municipality_WebApi.Application.UnitOfWork;
using Municipality_WebApi.Common.Models.Customers;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Municipality_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        IDatabase database;
      //  IDistributedCache distributedCache;
        IUnitofwork Unitofwork;
        public CustomersController(IUnitofwork Unitofwork,  IDatabase database)
        {
            this.database = database;
            this.Unitofwork = Unitofwork;
        }
        [HttpPost("AddCustomers")]
        public IActionResult AddCustomer(AddCustomersModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var res = Unitofwork.customerService.addCustomers(model);
            return Ok(res);
        }
        [HttpPut("UpdateCustomers")]
        public IActionResult UpdateCustomer(UpdateCustomersModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var res = Unitofwork.customerService.updateCustomers(model);
            return Ok(res);
        }
        [HttpDelete("DeleteCustomers")]
        public IActionResult DeleteCustomer(long id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var res = Unitofwork.customerService.deleteCustomers(id);
            return Ok(res);
        }
        [HttpGet("GetCustomers")]
        public IActionResult GetCustomer()
        {
            if (database.StringGet("customers").IsNull)
            {
                database.StringSet("customers", Unitofwork.customerService.showCustomers().ToString());
                database.KeyExpire("customers", TimeSpan.FromSeconds(45));
            }
            return Ok(database.StringGet("customers").ToString()) ;
        }
        [HttpPut ("ChangeExpireDate")]
        public IActionResult ChangeExpiredate(int customerId , DateTime newdate)
        {
            var res = Unitofwork.customerService.changeExpireDate(newdate, customerId);
            return Ok(res);
        }
        [HttpGet("MaxandminPrice")]
        public IActionResult MaxandminPrice(long customerid)
        {
            if (database.StringGet("maxminprice" + customerid.ToString()).IsNull)
            {
                database.StringSet("maxminprice" + customerid.ToString(), Unitofwork.customerService.ShowMinAndMaxPayment(customerid).ToString());
                database.KeyExpire("maxminprice" + customerid.ToString(), TimeSpan.FromSeconds(45));
            }
            return Ok(database.StringGet("maxminprice" + customerid.ToString()).ToString());
        }
    }
}
