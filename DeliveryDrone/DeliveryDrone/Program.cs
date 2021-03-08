using System;
using System.Collections.Generic;
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
            Console.WriteLine("Hello World!");

            int maxNumberOfStopsPerDelivery = 4;
            string directoryPath = "DeliveryRoutes";

            for (int i = 1; i <= 6; i++)
            {
                string path = $"{directoryPath}\\in0{i}.txt";
                var deliveries = await FileManager.ReadAllLinesAsync(path);

                var delivery = new DeliveryManager();
                var drone = new Drone.Drone();
                var memoryDrone = new DroneMemory();
                var results = delivery.Execute(drone, memoryDrone, deliveries, maxNumberOfStopsPerDelivery);
                

                var fileName = $"{directoryPath}\\out0{i}.txt";
                await FileManager.WriteFile(fileName,
                    results.Select(j => $"({j.GetCoordinate().GetX()},{j.GetCoordinate().GetY()},{j.GetOrientation()})"));
            }
            

            Console.ReadKey();
        }
    }
}