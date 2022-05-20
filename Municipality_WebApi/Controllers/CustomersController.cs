using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Municipality_WebApi.Application.Services.CustomerService;
using Municipality_WebApi.Common.Models.Customers;
using Newtonsoft.Json;
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
        ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpPost("AddCustomers")]
        public IActionResult AddCustomer(AddCustomersModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var res = _customerService.addCustomers(model);
            return Ok(res);
        }
        [HttpPut("UpdateCustomers")]
        public IActionResult UpdateCustomer(UpdateCustomersModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var res = _customerService.updateCustomers(model);
            return Ok(res);
        }
        [HttpDelete("DeleteCustomers")]
        public IActionResult DeleteCustomer(long id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var res = _customerService.deleteCustomers(id);
            return Ok(res);
        }
        [HttpGet("GetCustomers")]
        public IActionResult GetCustomer()
        {
            var res = _customerService.showCustomers();
            return Ok(res);
        }
        [HttpPut ("ChangeExpireDate")]
        public IActionResult ChangeExpiredate(int year , int yearadd)
        {
            var res = _customerService.changeExpireDate(year, yearadd);
            return Ok(res);
        }
    }
}
