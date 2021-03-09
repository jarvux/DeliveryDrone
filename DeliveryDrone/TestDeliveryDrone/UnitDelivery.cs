using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DeliveryDrone;
using DeliveryDrone.Drone;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestDeliveryDrone
{
    [TestClass]
    public class UnitDelivery
    {
        
        [TestMethod]
        public void Test_NextMovement_Empty_Input()
        {
            //arrange
            var delivery = new DeliveryManager();
            var drone = new Drone();
            var memoryDrone = new DroneMemory();
            var deliveries = new List<string> {};
            int maxNumberOfStopsPerDelivery = 3;
            
            //act
            var result = delivery.Execute(drone, memoryDrone, deliveries, maxNumberOfStopsPerDelivery);

            //assert
            Assert.IsTrue(deliveries.Count() == 0);
            Assert.IsTrue(result.Count() == 0);
        }
        
        [TestMethod]
        public void Test_NextMovement_OneRow_and_Empty_Input_content()
        {
            //arrange
            var delivery = new DeliveryManager();
            var drone = new Drone();
            var memoryDrone = new DroneMemory();
            var deliveries = new List<string> {{""}};
            int maxNumberOfStopsPerDelivery = 3;
            
            //act
            var result = delivery.Execute(drone, memoryDrone, deliveries, maxNumberOfStopsPerDelivery);

            //assert
            Assert.IsTrue(deliveries.Count() == 1);
            Assert.IsTrue(result.Count() == 0);
        }
        
        [TestMethod]
        public void Test_NextMovement_Invalid_Input()
        {
            //arrange
            var delivery = new DeliveryManager();
            var drone = new Drone();
            var memoryDrone = new DroneMemory();
            var deliveries = new List<string> {{"AAAB"}};
            int maxNumberOfStopsPerDelivery = 3;
           
            //assert
            Assert.ThrowsException<InvalidCastException>(() =>
                delivery.Execute(drone, memoryDrone, deliveries, maxNumberOfStopsPerDelivery));
        }
        
        [TestMethod]
        public void Test_NextMovement_equal_Count_Input_output()
        {
            //arrange
            var delivery = new DeliveryManager();
            var drone = new Drone();
            var memoryDrone = new DroneMemory();
            var deliveries = new List<string> {{"AIAIAIAIA"},{"DADADADADIII"},{"IIAD"}};
            int maxNumberOfStopsPerDelivery = 3;
            
            //act
            var result = delivery.Execute(drone, memoryDrone, deliveries, maxNumberOfStopsPerDelivery);

            //assert
            Assert.IsTrue(result.Count() == deliveries.Count);
        }
        
        [TestMethod]
        public void Test_NextMovement_More_than_three_inputs()
        {
            //arrange
            int maxNumberOfStopsPerDelivery = 3;
            var delivery = new DeliveryManager();
            var drone = new Drone();
            var memoryDrone = new DroneMemory();
            var deliveries = new List<string>
            {
                {"A"}, {"A"}, {"A"}, 
                {"A"}, {"A"}, {"A"},
            };
            //act
            var result = delivery.Execute(drone, memoryDrone, deliveries, maxNumberOfStopsPerDelivery);
            var enumerable = result as Memento[] ?? result.ToArray();
            
            //assert
            Assert.IsTrue(enumerable.Any());
            Assert.IsNotNull(result);
            Assert.IsNotNull(enumerable[2]);
            Assert.IsNotNull(enumerable[5]);
            
            var last1 = enumerable[2];
            var last2 = enumerable[5];
            
            Assert.IsTrue(last1.GetCoordinate().GetX() == last2.GetCoordinate().GetX());
            Assert.IsTrue(last1.GetCoordinate().GetY() == last2.GetCoordinate().GetY());
            Assert.IsTrue(last1.GetOrientation() == last2.GetOrientation());
        }
        
        [TestMethod]
        public void Test_NextMovement_Create_X_inputs_outputs_files_in_parallel()
        {
            //arrange
            var delivery = new DeliveryManager();
            var drone = new Drone();
            var memoryDrone = new DroneMemory();
            var deliveries = new List<string> {{"AAAAAAA"}};
            const string deliveryRoutes = "DeliveryRoutes";
            const int maxNumberOfStopsPerDelivery = 3;
            const int numFiles = 20;
            
            //act
            
            for (int i = 1; i <= numFiles; i++)
                File.WriteAllLines($"{deliveryRoutes}\\in{i}.txt", deliveries);
            
            var files = Directory.EnumerateFiles(deliveryRoutes, "*in*").ToList();
            Assert.IsTrue(files.Count == numFiles);
            
            Parallel.ForEach(files, async file =>
            {
               var content = await file.GetRoutesFromStringFileContent();
               
                await content
                    .ExecuteRoutes(maxNumberOfStopsPerDelivery)
                    .SetRoutesToStringFileContent($"{file.Replace("in", "out")}");
            });
            
            var outFiles = Directory.EnumerateFiles(deliveryRoutes, "*out*").ToList();
            Assert.IsTrue(outFiles.Count == numFiles);
        }
    }
}