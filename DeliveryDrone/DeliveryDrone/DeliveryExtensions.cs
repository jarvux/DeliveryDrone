using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DeliveryDrone.Drone;

namespace DeliveryDrone
{
    public static class DeliveryExtensions
    {
        public static async Task<string[]> GetRoutesFromStringFileContent(this string fileContent)
        {
           return await File.ReadAllLinesAsync(fileContent);
        }
        
        public static IEnumerable<Memento> ExecuteRoutes(this IEnumerable<string> deliveries, int maxNumberOfStopsPerDelivery = 3)
        {
            return 
                new DeliveryManager()
                    .Execute(new Drone.Drone(), new DroneMemory(), deliveries, maxNumberOfStopsPerDelivery);
        }

        public static async Task SetRoutesToStringFileContent(this IEnumerable<Memento> results, string outputFileName)
        {
            await File.WriteAllLinesAsync(outputFileName, 
                results
                    .Select(j => $"({j.GetCoordinate().GetX()},{j.GetCoordinate().GetY()},{j.GetOrientation()})"));
        }
    }
}