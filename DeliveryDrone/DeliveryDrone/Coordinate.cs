namespace DeliveryDrone
{
    public class Coordinate
    {
        private int _x;
        private int _y;

        public Coordinate(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public int GetX()
        {
            return _x;
        }

        public void SetX(int x)
        {
            _x = x;
        }

        public int GetY()
        {
            return _y;
        }

        public void SetY(int y)
        {
            _y = y;
        }
    }
}