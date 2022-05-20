using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Municipality_WebApi.Application.Services.BillsPaymentService
{
    public interface IBillsPaymentService
    {
        bool payBillWithId(long billId);
        List<bool> payMultiBillsWithId(string Ids);
        List<bool> payBillsWithCustomerId(long customerId);
    }
}
