using System.Collections.Generic;

namespace DeliveryDrone.Drone
{
    public class DroneMemory
    {
        private readonly List<Memento> _savedStates = new List<Memento>();

        public void AddMemento(Memento memento)
        {
            _savedStates.Add(memento);
        }

        public IEnumerable<Memento> GetMementos()
        {
            return _savedStates;
        }
    }
}