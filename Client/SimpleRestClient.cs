using Common.DataModels.DtoModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class SimpleRestClient
    {
        private readonly IRestClient _restClient;
        public SimpleRestClient()
        {
            _restClient = new RestClient();
            _restClient.BaseUrl = new Uri("https://localhost:44325/api/student/");
        }

        public IRestResponse CreateTestStudent()
        {
            var request = new RestRequest("createTest", Method.POST);
            return _restClient.Post(request);
        }
        public IRestResponse GetStudentDetailsByCardNumber(CardNumber cardNumber)
        {
            var request = new RestRequest("getStudent", Method.POST);
            request.AddJsonBody(cardNumber);
            return _restClient.Post(request);
        }
        public IRestResponse DeleteTestStudent()
        {
            var request = new RestRequest("deleteTest");
            return _restClient.Delete(request);
        }
    }
}
