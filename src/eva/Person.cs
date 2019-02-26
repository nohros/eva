using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eva.Layout;

namespace Eva
{
  public class Person
  {
    readonly int speed_;
    readonly Random rand_;
    bool know_exits_;
    Location location_;

    public Person(int speed, Location location, bool know_exits) {
      speed_ = speed;
      location_ = location;
      know_exits_ = know_exits;
      rand_ = new Random();
    }

    public Location Move() {
      for (int i = 0; i < speed_; i++) {
        Region region = location_.Region;
        if (know_exits_) {
          //location_ = region.GetDoors();
        } else {
          MoveRandomly();
        }
      }

      return location_;
    }

    void MoveRandomly() {
      Region region = location_.Region;
      Surrounding surrounding = region.GetSurrounding(location_.Index);
      // Since we do not know the door direction, we should move randonmly to
      // a surrounding rect that is empty.
      Occupation[] occupations = surrounding.ToArray();
      var empty_occupations = new List<Occupation>();
      foreach (Occupation occupation in occupations) {
        if (occupation != null && occupation.Person == null) {
          empty_occupations.Add(occupation);
        }
      }

      int size = empty_occupations.Count;
      if (size > 1) {
        int index = rand_.Next(0, size);
        Occupation occupation = empty_occupations[index];
        location_ = region.PlacePerson(this, occupation.Location.Index);
      }
    }

    public bool KnowExit => know_exits_;

    public Location Location => location_;
  }
}
