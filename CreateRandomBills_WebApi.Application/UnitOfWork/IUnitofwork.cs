using CreateBills_WebApi.Application.Services.CreateBillsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateBills_WebApi.Application.UnitOfWork
{
    public interface IUnitofwork
    {
        ICreateBillsService createBillsService { get; }
    }
    public class Unitofwork : IUnitofwork
    {
        public ICreateBillsService _createBillsService;
        public ICreateBillsService createBillsService
        {
            get
            {
                if (_createBillsService == null)
                {
                    _createBillsService = new CreateBillsService();
                }
                return _createBillsService;
            }
        }
    }
}
