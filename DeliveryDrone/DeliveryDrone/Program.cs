using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DeliveryDrone.Drone;

namespace DeliveryDrone
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string directoryPath = "DeliveryRoutes";
            var result = new List<string>();
            for (int i = 1; i < 6; i++)
            {
                string path = $"{directoryPath}\\in0{i}.txt";
                var r = await FileManager.ReadAllLinesAsync(path);
                result.AddRange(r);
                
                r.ForEach(Console.WriteLine);
                Console.WriteLine("-----------------");
            }
            

            foreach (var i in result)
            {
                
            }

            Console.ReadKey();
            /*
            int maxNumberOfStopsPerDelivery = 4;
            
            var delivery = new DeliveryManager();
            var drone = new Drone.Drone();
            var memoryDrone = new DroneMemory();
            var deliveries = new List<string> {{"AAAAIAA"}, {"DDDAIAD"}, {"AAIADAD"}};
            var result = delivery.Execute(drone, memoryDrone, deliveries, maxNumberOfStopsPerDelivery);

            foreach (var memento in result)
                Console.WriteLine($"({memento.GetCoordinate().GetX()},{memento.GetCoordinate().GetY()}, {memento.GetOrientation()})");
            */
        }
    }
}