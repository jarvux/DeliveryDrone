namespace DeliveryDrone.Drone
{
    public class Memento
    {
        private readonly Coordinate _coordinate;
        //private readonly Status _status;
        private readonly Orientation _orientation;
        
        public Memento(Coordinate coordinate, Status status, Orientation orientation)
        {
            _coordinate = new Coordinate(coordinate.GetX(), coordinate.GetY());
            //_status = status;
            _orientation = orientation;
        }

        public Coordinate GetCoordinate()
        {
            return _coordinate;
        }

        /*public Status GetStatus()
        {
            return _status;
        }*/

        public Orientation GetOrientation()
        {
            return _orientation;
        }
    }
}