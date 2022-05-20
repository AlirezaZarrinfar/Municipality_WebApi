using Municipality_WebApi.Common.Models.Customers;
using Municipality_WebApi.Persistance.Connection;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace Municipality_WebApi.Application.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        public bool addCustomers(AddCustomersModel model)
        {
            try
            {
                if (model.Name == null || model.LastName == null || model.NationalCode == null || model.PhoneNumber == null)
                {
                    return false;
                }
                string command = "insert into Customer ([Name], [LastName],[NationalCode],[PhoneNumber],[ExpireDate]) values (@name , @lastname , @nationalcode , @phonenumber , @expiredate)";
                SqlCommand sqlcommand = new SqlCommand();
                sqlcommand.Parameters.AddWithValue("@name", model.Name);
                sqlcommand.Parameters.AddWithValue("@lastname", model.LastName);
                sqlcommand.Parameters.AddWithValue("@nationalcode", model.NationalCode);
                sqlcommand.Parameters.AddWithValue("@phonenumber", model.PhoneNumber);
                sqlcommand.Parameters.AddWithValue("@expiredate", model.ExpireDate);
                sqlcommand.CommandText = command;
                sqlcommand.Connection = Connection.Instance.connection();
                sqlcommand.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool deleteCustomers(long id)
        {
            if (id == 0)
            {
                return false;
            }
            string command = "delete from Customer where [Id] = @id";
            SqlCommand sqlcommand = new SqlCommand();
            sqlcommand.Parameters.AddWithValue("@id", id);
            sqlcommand.CommandText = command;
            sqlcommand.Connection = Connection.Instance.connection();
            sqlcommand.ExecuteNonQuery();
            return true;
        }

        public string showCustomers()
        {
            string command = "select * from Customer";
            SqlCommand sqlcommand = new SqlCommand(command,Connection.Instance.connection());
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlcommand);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            return JsonConvert.SerializeObject(dt);
        }

        public string showCustomersById(long id)
        {
            
            string command = "select * from Customer where Id = @id";
            SqlCommand sqlcommand = new SqlCommand(command, Connection.Instance.connection());
            sqlcommand.Parameters.AddWithValue("@id", id);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlcommand);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            return JsonConvert.SerializeObject(dt);
        }

        public bool updateCustomers(UpdateCustomersModel model)
        {
            try
            {
                if (model.Name == null || model.LastName == null || model.NationalCode == null || model.PhoneNumber == null)
                {
                    return false;
                }
                string command = "update Customer set [Name] = @name , [LastName] = @lastname ,[NationalCode] = @nationalcode ,[PhoneNumber] = @phonenumber ,[ExpireDate] = @expiredate where [Id] = @id";
                SqlCommand sqlcommand = new SqlCommand();
                sqlcommand.Parameters.AddWithValue("@id", model.Id);
                sqlcommand.Parameters.AddWithValue("@name", model.Name);
                sqlcommand.Parameters.AddWithValue("@lastname", model.LastName);
                sqlcommand.Parameters.AddWithValue("@nationalcode", model.NationalCode);
                sqlcommand.Parameters.AddWithValue("@phonenumber", model.PhoneNumber);
                sqlcommand.Parameters.AddWithValue("@expiredate", model.ExpireDate);
                sqlcommand.CommandText = command;
                sqlcommand.Connection = Connection.Instance.connection();
                sqlcommand.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool changeExpireDate(int year , int yearAdd)
        {
            try
            {
                string command = "SP_changeExpireDate";
                SqlCommand sqlCommand = new SqlCommand(command, Connection.Instance.connection());
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@year", year);
                sqlCommand.Parameters.AddWithValue("@yearadd", yearAdd);
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
