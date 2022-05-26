using Municipality_WebApi.Application.Services.BillsPaymentService;
using Municipality_WebApi.Application.Services.BillsService;
using Municipality_WebApi.Application.Services.CustomerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Municipality_WebApi.Application.UnitOfWork
{
    public interface IUnitofwork
    {
        IBillsPaymentService billsPaymentService { get; }
        IBillsService billsService { get; }
        ICustomerService customerService { get; }
    }
}
