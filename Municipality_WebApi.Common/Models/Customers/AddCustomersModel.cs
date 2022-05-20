using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Municipality_WebApi.Common.Models.Customers
{
    public class AddCustomersModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string NationalCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime ExpireDate { get; set; }
    }
}
