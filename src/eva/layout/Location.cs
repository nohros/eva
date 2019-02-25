using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eva.Layout;

namespace Eva.Layout
{
  public class Location
  {
    public Location(Region region, int index) {
      Region = region;
      Index = index;
    }

    public Region Region { get; }
    public int Index {get;}
  }
}
