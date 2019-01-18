namespace BlockBreaker.Game.Resources
{
    public struct Vector
    {
        public double XComponent { get; set; }
        public double YComponent { get; set; }

        public Vector(double x, double y)
        {
            XComponent = x;
            YComponent = y;
        }

        public static Vector operator +(Vector a, Vector b)
        {
            var finalX = a.XComponent + b.XComponent;
            var finalY = a.YComponent + b.YComponent;

            var result = new Vector(finalX, finalY);

            return result;
        }
    }
}
