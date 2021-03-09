using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryDrone.Drone;

namespace DeliveryDrone
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Begin executing delivery");
            
            const int maxNumberOfStopsPerDelivery = 3;
            const string deliveryRoutes = "DeliveryRoutes";

            
           Parallel.ForEach(Directory.EnumerateFiles(deliveryRoutes, "*in*"), async file =>
           {
               Console.WriteLine($"Begin: file:{file}");
               var content = await file.GetRoutesFromStringFileContent();
               
               await content
                   .ExecuteRoutes(maxNumberOfStopsPerDelivery)
                   .SetRoutesToStringFileContent($"{file.Replace("in", "out")}");
               
               Console.WriteLine($"End: file:{file}");
           });

           Console.WriteLine("End executing delivery");
           Console.ReadKey();
        }
    }
}