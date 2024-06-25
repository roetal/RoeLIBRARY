using System;
using System.ServiceModel;
using WcfServiceLibrary;

namespace WcfServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("http://localhost:8888/WcfServiceLibrary/Service1");

            using (ServiceHost host = new ServiceHost(typeof(Service1), baseAddress))
            {
                try
                {
                    host.Open();
                    Console.WriteLine("Service is running...");
                    Console.WriteLine("Press Enter to terminate the service.");
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An exception occurred: {ex.Message}");
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }
    }
}
