using Municipality_WebApi.Application.Services.BillsService;
using Municipality_WebApi.Application.Services.CustomerService;
using Municipality_WebApi.Common.Models.Bills;
using Municipality_WebApi.Common.Models.Customers;
using Municipality_WebApi.Persistance.Connection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Municipality_WebApi.Application.Services.BillsPaymentService
{
    public class BillsPaymentService : IBillsPaymentService
    {
        ICustomerService customerService;
        IBillsService billsService;
        public BillsPaymentService(IBillsService billsService, ICustomerService customerService)
        {
            this.customerService = customerService;
            this.billsService = billsService;
        }
        public bool payBillWithId(long billId)
        {
            try
            {
                var jsonBill = billsService.showBillById(billId);
                var bill = JsonConvert.DeserializeObject<ShowBillsModel>(jsonBill.Substring(1, jsonBill.Length - 2));
                var jsonCustomer = customerService.showCustomersById(bill.CustomerId);
                var customer = JsonConvert.DeserializeObject<ShowCustomersModel>(jsonCustomer.Substring(1,jsonCustomer.Length-2));
                string command = "insert into BillsPayment ([BillId] , [IsPaid] , [Price] , [Date]) values (@billid , @ispaid , @price , @date)";
                SqlCommand sqlcommand = new SqlCommand();
                sqlcommand.CommandText = command;
                sqlcommand.Connection = Connection.Instance.connection();
                if ((customer.ExpireDate < DateTime.Now))
                {
                    sqlcommand.Parameters.AddWithValue("@billid", billId);
                    sqlcommand.Parameters.AddWithValue("@ispaid", false);
                    sqlcommand.Parameters.AddWithValue("@price", bill.Price);
                    sqlcommand.Parameters.AddWithValue("@date", DateTime.Now);
                    sqlcommand.ExecuteNonQuery();
                    return false;
                }
                else
                {
                    sqlcommand.Parameters.AddWithValue("@billid", billId);
                    sqlcommand.Parameters.AddWithValue("@ispaid", true);
                    sqlcommand.Parameters.AddWithValue("@price", bill.Price);
                    sqlcommand.Parameters.AddWithValue("@date", DateTime.Now);
                    sqlcommand.ExecuteNonQuery();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public List<bool> payMultiBillsWithId(string Ids)
        {
            String[] strlist = Ids.Split('-');
            List<bool> results = new List<bool>();
            foreach (var item in strlist)
            {
              bool flag = payBillWithId(Convert.ToInt64(item));
              results.Add(flag);
            }
            return results ;
        }
        public List<bool> payBillsWithCustomerId(long customerId)
        {
            var Ids = billsService.showBillByCustomerId(customerId);
            List<bool> results = new List<bool>();
            for (int i = 0; i<Ids.Rows.Count; i++)
            {
                bool flag = payBillWithId(Convert.ToInt64(Ids.Rows[i][0]));
                results.Add(flag);
            }
            return results;
        }


    }
}
