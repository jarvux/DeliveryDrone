using System;
using System.Collections.Generic;
using System.Linq;
using DeliveryDrone.Drone;

namespace DeliveryDrone
{
    public class DeliveryManager
    {
        public IEnumerable<Memento> Execute(
            Drone.Drone drone, 
            DroneMemory droneMemory, 
            IEnumerable<string> deliveries,
            int maxNumberOfStopsPerDelivery = 3)
        {
            var result = new List<Memento>();
            drone.SetStatus(Status.ACTIVE);

            var countDelivery = 0;
            foreach (var delivery in deliveries)
            {
                foreach (var i in delivery.ToCharArray())
                {
                    if (Enum.TryParse(i.ToString(), true, out Instructions inst))
                    {
                        NextMovement(inst, drone);
                        droneMemory.AddMemento(drone.CreateMemento());
                    }
                    else
                    {
                        throw new InvalidCastException("The instruction is invalid");
                    }

                    //if(drone.GetStatus() == Status.BLOCKED)
                    //return;
                }

                var mementos = droneMemory.GetMementos().ToList();
                if (mementos.Any())
                {
                    result.Add(mementos.Last());
                    countDelivery++;
                }

                if (countDelivery == maxNumberOfStopsPerDelivery)
                {
                    drone.GetCoordinate().SetX(0);
                    drone.GetCoordinate().SetY(0);
                    drone.SetOrientation(Orientation.N);
                    droneMemory.AddMemento(drone.CreateMemento());
                    countDelivery = 0;
                }
            }

            return result;
        }

        private void NextMovement(Instructions instruction, Drone.Drone drone)
        {
            int x = drone.GetCoordinate().GetX();
            int y = drone.GetCoordinate().GetY();

            if (instruction == Instructions.D)
            {
                switch (drone.GetOrientation())
                {
                    case Orientation.N:
                        drone.SetOrientation(Orientation.E);
                        break;
                    case Orientation.E:
                        drone.SetOrientation(Orientation.S);
                        break;
                    case Orientation.W:
                        drone.SetOrientation(Orientation.N);
                        break;
                    case Orientation.S:
                        drone.SetOrientation(Orientation.W);
                        break;
                }
            }

            if (instruction == Instructions.I)
            {
                switch (drone.GetOrientation())
                {
                    case Orientation.N:
                        drone.SetOrientation(Orientation.W);
                        break;
                    case Orientation.E:
                        drone.SetOrientation(Orientation.N);
                        break;
                    case Orientation.W:
                        drone.SetOrientation(Orientation.S);
                        break;
                    case Orientation.S:
                        drone.SetOrientation(Orientation.E);
                        break;
                }
            }

            if (instruction == Instructions.A)
            {
                switch (drone.GetOrientation())
                {
                    case Orientation.N:
                        y++;
                        break;
                    case Orientation.E:
                        x++;
                        break;
                    case Orientation.W:
                        x--;
                        break;
                    case Orientation.S:
                        y--;
                        break;
                }

                //unAvailable zone
                //if((x > 10) || (y > 10)) drone.SetStatus(Status.BLOCKED);

                if (drone.GetStatus() == Status.ACTIVE)
                {
                    drone.GetCoordinate().SetX(x);
                    drone.GetCoordinate().SetY(y);
                }
            }
        }
    }
}