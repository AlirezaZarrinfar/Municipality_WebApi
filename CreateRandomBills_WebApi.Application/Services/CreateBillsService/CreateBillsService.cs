using CreateBills_WebApi.Persistance.Connection;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CreateBills_WebApi.Application.Services.CreateBillsService
{
    public class CreateBillsService : ICreateBillsService
    {
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
            return random.Next().ToString();
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
    }
}
