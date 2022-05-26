using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Municipality_WebApi.Application.Services.BillsService
{
    public interface IBillsService 
    {
        string showBillById(long id);
        string showBillandCustomer(long id);
        DataTable showBillByCustomerId(long customerId);
        public JArray showAllBills();
        public JArray SuccessAndFailedCount(int customerId);
        public JArray getTotalCount();
    }
}
