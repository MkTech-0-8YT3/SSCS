using Common.DataModels.DtoModels;
using PCSC;
using PCSC.Monitoring;
using System;

namespace Client
{
    public class Program
    {
        public static void Main()
        {
            var simpleRestClient = new SimpleRestClient();
            simpleRestClient.CreateTestStudent();

            var readerName = "Gemplus USB SmartCard Reader 0";
            var contextFactory = ContextFactory.Instance;
            var monitorFactory = MonitorFactory.Instance;
            using var context = contextFactory.Establish(SCardScope.System);
            var monitor = monitorFactory.Create(SCardScope.System);

            monitor.CardInserted += (sender, args) =>
            {
                var cardNumber = new CardNumber(GetReadableCardAtr(args.Atr));
                var response = simpleRestClient.GetStudentDetailsByCardNumber(cardNumber);
                Console.WriteLine("Response from server:");
                Console.WriteLine(response.Content);
            };
            
            monitor.Start(readerName);
            
            Console.ReadKey(); // press any key to exit

            monitor.Cancel();
            monitor.Dispose();
            simpleRestClient.DeleteTestStudent();
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
