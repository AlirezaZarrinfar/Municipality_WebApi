using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Municipality_WebApi.Common.Models.Bills
{
    public class ShowBillsModel
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public long CustomerId { get; set; }
        public DateTime CreateDate { get; set; }
        public double Price { get; set; }
    }
}
