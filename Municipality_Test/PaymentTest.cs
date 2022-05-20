using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Municipality_WebApi;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Municipality_Test
{
    public class PaymentTest
    {
        HttpClient _client;
        public PaymentTest()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = server.CreateClient();
        }
        [Fact]
        public async Task PayBillById_Test()
        {
            var response = await _client.PostAsync("/api/BillsPayment/PayBillById/12", null).Result.Content.ReadAsStringAsync();
            Assert.Equal("true", response);
        }
        [Fact]
        public async Task PayMultiBillsWithId_Test()
        {
            var response = await _client.PostAsync("/api/BillsPayment/PayMultiBillsWithId/14-15-16-17", null).Result.Content.ReadAsStringAsync();
            string myresponse = "[true,true,true,true]";
            Assert.Equal(myresponse, response);
        }
        [Fact]
        public async Task payBillsWithCustomerId_Test()
        {
            var response = await _client.PostAsync("/api/BillsPayment/payBillsWithCustomerId/7", null).Result.Content.ReadAsStringAsync();
            string myresponse = "[true,true,true,true,true]";
            Assert.Equal(myresponse, response);
        }
    }
}
