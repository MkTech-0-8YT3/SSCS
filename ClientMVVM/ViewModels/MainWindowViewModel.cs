using ClientMVVM.Models;
using Common.DataModels.DtoModels;
using DynamicData.Binding;
using PCSC;
using PCSC.Monitoring;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ClientMVVM.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        private static readonly string CARD_READER_ID = "Gemplus USB SmartCard Reader 0";
        private readonly SimpleRestClient _simpleRestClient;
        private List<ClassEventDto> classEvents;
        private string studentName;
        
        public MainWindowViewModel()
        {
            _simpleRestClient = new SimpleRestClient();
            ConfigureCardEventListeners();
        }
        public string StudentName 
        {
            get => studentName;
            set => this.RaiseAndSetIfChanged(ref studentName, value);
        }
        public List<ClassEventDto> ClassEvents 
        { 
            get => classEvents;
            set => this.RaiseAndSetIfChanged(ref classEvents, value);
        }

        private void ConfigureCardEventListeners()
        {
            var monitor = MonitorFactory.Instance.Create(SCardScope.System);

            monitor.CardInserted += (sender, args) =>
            {
                //Testing purposes
                //_simpleRestClient.CreateTestStudent();
                var cardNumber = new CardNumberDto(GetReadableCardAtr(args.Atr));
                var responseStudentDetails = _simpleRestClient.GetStudentDetailsByCardNumber(cardNumber);
                StudentName = Newtonsoft.Json.JsonConvert.DeserializeObject<StudentNameDto>(responseStudentDetails.Content).FirstName;
                var responseSchedule = _simpleRestClient.GetStudentScheduleForCurrentWeek(cardNumber);
                ClassEvents = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ClassEventDto>>(responseSchedule.Content);
            };

            monitor.CardRemoved += (sender, args) =>
            {
                //Testing purposes
                //_simpleRestClient.DeleteTestStudent();
                StudentName = "";
                ClassEvents = new List<ClassEventDto>();
            };

            monitor.Start(CARD_READER_ID);
        }
        private static string GetReadableCardAtr(byte[] atr)
        {
            if (atr is null || atr.Length == 0)
            {
                return "";
            }
            return BitConverter.ToString(atr);
        }
    }
}
