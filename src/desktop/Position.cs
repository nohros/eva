using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eva.Layout;

namespace Eva
{
  class Position
  {
    public Position(int top, int left) {
      Top = top;
      Left = left;
    }

    public static Position FromLocation(Location location) {
      Region region = location.Region;
      Rect rect = region.GetRectByIndex(location.Index);
      return new Position(rect.Top, rect.Left);
    }

    public int Top { get; }
    public int Left { get; }

    protected bool Equals(Position other) {
      return Top == other.Top && Left == other.Left;
    }

    public override bool Equals(object obj) {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((Position) obj);
    }

    public override int GetHashCode() {
      unchecked {
        return (Top * 397) ^ Left;
      }
    }
  }
}
