using System;
using System.Collections.Generic;
using System.Linq;
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
            
            //act
            var result = delivery.Execute(drone, memoryDrone, deliveries);

            //assert
            Assert.IsTrue(result.Count() == 0);
        }
        
        [TestMethod]
        public void Test_NextMovement_Empty_Input_content()
        {
            //arrange
            var delivery = new DeliveryManager();
            var drone = new Drone();
            var memoryDrone = new DroneMemory();
            var deliveries = new List<string> {{""}};
            
            //act
            var result = delivery.Execute(drone, memoryDrone, deliveries);

            //assert
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
           
            //assert
            Assert.ThrowsException<InvalidCastException>(() =>
                delivery.Execute(drone, memoryDrone, deliveries));
        }
        
        [TestMethod]
        public void Test_NextMovement_equal_Count_Input_output()
        {
            //arrange
            var delivery = new DeliveryManager();
            var drone = new Drone();
            var memoryDrone = new DroneMemory();
            var deliveries = new List<string> {{"AIAIAIAIA"},{"DADADADADIII"},{"IIAD"}};
           
            //act
            var result = delivery.Execute(drone, memoryDrone, deliveries);

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
        
        /*[TestMethod]
        public void Test_NextMovement_More_than_limit_blocks()
        {
            //arrange
            var delivery = new DeliveryManager();
            var drone = new Drone();
            var memoryDrone = new DroneMemory();
            var deliveries = new List<string> {{"AAAAAAA"},{"AAAAAAA"},{"AAAAAAA"}};
           
            //act
            var result = delivery.Execute(drone, memoryDrone, deliveries);

            //assert
            Assert.IsTrue(result.Count() == deliveries.Count);
        }*/
    }
}