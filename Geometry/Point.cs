namespace Geometry
{
    /// <summary>
    /// This is used to represent a point
    /// </summary>
    public class Point
    {
        public double X { get; }
        public double Y { get; }

        /// <summary>
        /// This is a constructor for the point
        /// </summary>
        /// <param name="x"> The x-coordinate for the point </param>
        /// <param name="y"> The y-coordinate for the point</param>
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() => $"({X:F2}, {Y:F2})";

        public override bool Equals(object? obj)
        {
            if (obj is not Point other) return false;
            return X == other.X && Y == other.Y;
        }

        public double Distance(object? obj)
        {
            if (obj is not Point other) return -1;
            return Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

    }
}