using Municipality_WebApi.Application.Services.BillsPaymentService;
using Municipality_WebApi.Application.Services.BillsService;
using Municipality_WebApi.Application.Services.CustomerService;

namespace Municipality_WebApi.Application.UnitOfWork
{
    public class Unitofwork : IUnitofwork
    {
        private IBillsService _billsService;
        public IBillsService billsService
        {
            get
            {
                if (_billsService == null)
                {
                    _billsService = new BillsService(customerService);
                }
                return _billsService;
            }
        }
        private IBillsPaymentService _billsPaymentService;
        public IBillsPaymentService billsPaymentService
        {
            get
            {
                if (_billsPaymentService == null)
                {
                    _billsPaymentService = new BillsPaymentService(billsService, customerService);
                }
                return _billsPaymentService;
            }
        }
        private ICustomerService _customerService;
        public ICustomerService customerService
        {
            get
            {
                if (_customerService == null)
                {
                    _customerService = new CustomerService();
                }
                return _customerService;
            }
        }
    }
}
