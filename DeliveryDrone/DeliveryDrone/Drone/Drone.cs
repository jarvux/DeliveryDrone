namespace DeliveryDrone.Drone
{
    public class Drone
    {
        private readonly Coordinate _coordinate;
        private Orientation _orientation;
        private Status _status;
        
        public Drone()
        {
            _orientation = Orientation.N;
            _coordinate = new Coordinate(0, 0);
            _status = Status.INACTIVE;
        }

        /*public void SetMemento(Memento memento)
        {
            _coordinate = memento.GetCoordinate();
            _status = memento.GetStatus();
            _orientation = memento.GetOrientation();
        }*/

        public Memento CreateMemento()
        {
            return new Memento(GetCoordinate(), GetStatus(), GetOrientation());
        }
        
        
        public Status GetStatus() {
            return _status;
        }

        public void SetStatus(Status status) {
            _status = status;
        }
        
        
        public Coordinate GetCoordinate()
        {
            return _coordinate;
        }

        public Orientation GetOrientation()
        {
            return _orientation;
        }
        
        public void SetOrientation(Orientation orientation) {
            _orientation = orientation;
        }   
    }
    
    public enum Orientation
    {
        N, //North
        E, //East
        W, //West
        S  //South
    }

    public enum Instructions
    {
        A, //Go,
        I, //TurnL,
        D  //TurnR
    }

    public enum Status
    {
        ACTIVE,
        BLOCKED,
        INACTIVE
    }
}