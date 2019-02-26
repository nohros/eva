using System;

namespace Eva.Layout
{
  public class Point
  {
    public Point(double x, double y) {
      X = x;
      Y = y;
    }

    public double X { get; }

    public double Y { get; }

    protected bool Equals(Point other) {
      return X.Equals(other.X) && Y.Equals(other.Y);
    }

    public override bool Equals(object obj) {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((Point) obj);
    }

    public override int GetHashCode() {
      unchecked {
        return (X.GetHashCode() * 397) ^ Y.GetHashCode();
      }
    }
  }
}
