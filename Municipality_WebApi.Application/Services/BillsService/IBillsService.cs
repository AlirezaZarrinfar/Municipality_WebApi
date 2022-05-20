using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Municipality_WebApi.Application.Services.BillsService
{
    public interface IBillsService 
    {
        bool addBills();
        string showBillById(long id);
         string showBillandCustomer(long id);
        DataTable showBillByCustomerId(long customerId);
        public string showAllBills();
        public string TFCount(int customerId);
        public string getTotalCount();
    }
}
