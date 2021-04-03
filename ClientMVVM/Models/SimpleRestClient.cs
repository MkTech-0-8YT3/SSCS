using Common.DataModels.DtoModels;
using RestSharp;
using System;

namespace ClientMVVM.Models
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
        public IRestResponse GetStudentDetailsByCardNumber(CardNumberDto cardNumber)
        {
            var request = new RestRequest("getStudent", Method.POST);
            request.AddJsonBody(cardNumber);
            return _restClient.Post(request);
        }
        public IRestResponse GetStudentScheduleForCurrentWeek(CardNumberDto cardNumber)
        {
            var request = new RestRequest("getSchedule", Method.POST);
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
