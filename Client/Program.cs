using PCSC;
using PCSC.Monitoring;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var readerName = "Gemplus USB SmartCard Reader 0";
            var contextFactory = ContextFactory.Instance;
            var monitorFactory = MonitorFactory.Instance;
            using var context = contextFactory.Establish(SCardScope.System);
            var monitor = monitorFactory.Create(SCardScope.System);

            monitor.CardInserted += (sender, args) =>
                Console.WriteLine($"Cart ATR: {GetReadableCardAtr(args.Atr)}");
            
            monitor.Start(readerName);
            
            Console.ReadKey(); // key to exit

            monitor.Cancel();
            monitor.Dispose();
        }

        private static string GetReadableCardAtr(byte[] atr)
        {
            if (atr == null || atr.Length == 0)
            {
                return "";
            }

            return BitConverter.ToString(atr);
        }
    }
}
