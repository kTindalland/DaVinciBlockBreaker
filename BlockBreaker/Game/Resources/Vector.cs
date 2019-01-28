namespace BlockBreaker.Game.Resources
{
    public struct Vector
    {
        public double XComponent { get; set; } // How far right it goes, negative is left
        public double YComponent { get; set; } // How far down it goes, negative is up

        public Vector(double x, double y)
        {
            XComponent = x;
            YComponent = y;
        }

        public static Vector operator +(Vector a, Vector b) // Override the + operator
        {
            var finalX = a.XComponent + b.XComponent;
            var finalY = a.YComponent + b.YComponent;

            var result = new Vector(finalX, finalY);

            return result;
        }
    }
}
