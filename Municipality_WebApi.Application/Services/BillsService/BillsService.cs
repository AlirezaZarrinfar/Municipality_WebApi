using Municipality_WebApi.Application.Services.CustomerService;
using Municipality_WebApi.Common.Models.Bills;
using Municipality_WebApi.Persistance.Connection;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Municipality_WebApi.Application.Services.BillsService
{
    public class BillsService : IBillsService
    {
        ICustomerService customerService;
        public BillsService(ICustomerService customerService)
        {
            this.customerService = customerService;
        }
        Random random = new Random();
        double addRandomPrice()
        {
            return ((random.Next() % 30001) + 15000);
        }
        long addRandomCustomerId()
        {
            string command = "select Id from Customer ";
            SqlCommand sqlcommand = new SqlCommand(command, Connection.Instance.connection());
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlcommand);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            return Convert.ToInt64(dt.Rows[random.Next() % dt.Rows.Count][0]);
        }
        string addRandomCode()
        {
           return  random.Next().ToString();
        }
        public bool addBills()
        {
            try
            {
                string command = "insert into Bill (Code , CustomerId , CreateDate , Price) values (@code , @customerId , @createDate , @price)";
                SqlCommand sqlCommand = new SqlCommand(command, Connection.Instance.connection());
                sqlCommand.Parameters.AddWithValue("@code", addRandomCode());
                sqlCommand.Parameters.AddWithValue("@customerId", addRandomCustomerId());
                sqlCommand.Parameters.AddWithValue("@createDate", DateTime.Now);
                sqlCommand.Parameters.AddWithValue("@price", addRandomPrice());
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string showAllBills()
        {
            string command = "select * from Bill ";
            SqlCommand sqlcommand = new SqlCommand(command, Connection.Instance.connection());
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlcommand);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            return JsonConvert.SerializeObject(dt);
        }

        public string showBillById(long id)
        {
            string command = "select * from Bill where id = @id";
            SqlCommand sqlcommand = new SqlCommand(command, Connection.Instance.connection());
            sqlcommand.Parameters.AddWithValue("@id" , id);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlcommand);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            return JsonConvert.SerializeObject(dt);
        }

        public DataTable showBillByCustomerId(long customerId)
        {
            string command = "select id from Bill where [CustomerId] = @id";
            SqlCommand sqlcommand = new SqlCommand(command, Connection.Instance.connection());
            sqlcommand.Parameters.AddWithValue("@id", customerId);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlcommand);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            return dt;
        }
        public string showBillandCustomer(long id)
        {
            var jsonBill = showBillById(id);
            var bill = JsonConvert.DeserializeObject<ShowBillsModel>(jsonBill.Substring(1, jsonBill.Length - 2));
            var jsonCustomer = customerService.showCustomersById(bill.CustomerId);
            return jsonBill + "\n" + jsonCustomer;
        }

        public string TFCount(int customerId)
        {
            string command = "SP_TFpivot";
            SqlCommand sqlCommand = new SqlCommand(command, Connection.Instance.connection());
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@cid", customerId);
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return JsonConvert.SerializeObject(dt);
        }
        //Sp_TotalPrice
        public string getTotalCount()
        {
            string command = "Sp_TotalPrice";
            SqlCommand sqlCommand = new SqlCommand(command, Connection.Instance.connection());
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return JsonConvert.SerializeObject(dt);
        }
    }
}
