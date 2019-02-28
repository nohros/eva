using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
        if (know_exits_) {
          MoveToDoor();
        } else {
          MoveRandomly();
        }
      }

      return location_;
    }

    void MoveToDoor() {
      Region region = location_.Region;
      Surrounding surrounding = region.GetSurrounding(location_.Index);
      Rect[] doors = region.GetDoors().ToArray();

      var empty_occupations = new List<Occupation>();

      // If the room has no doors there is not place to go
      if (doors.Length == 0) {
        return;
      }

      Rect door = doors[0];
      Rect my_rect = region.GetRectByIndex(location_.Index);

      Occupation occupation;
      if (my_rect.IsOnRow(door)) {
        // If we are in the same row as the door, we should move only to left
        // or right, if we are on the left side we should move to the right, if
        // we are on the right side of the door we should move to the left.
        occupation =
          my_rect.IsOnLeft(door)
            ? surrounding.OnRight
            : surrounding.OnLeft;
        if (occupation != null) {
          empty_occupations.Add(occupation);
        }
      } else if (my_rect.IsOnColumn(door)) {
        // Here we know that we are not in the same row as the door, lets check
        // if we are in the same column
        occupation =
          my_rect.IsBelow(door)
            ? surrounding.OnTop
            : surrounding.OnBottom;
        if (occupation != null) {
          empty_occupations.Add(occupation);
        }
      } else {
        // We are not in the same row and not in the same column as the
        // door, we can be below or above, in any condition we can move
        // left or right...
        occupation =
          my_rect.IsOnLeft(door)
            ? surrounding.OnRight
            : surrounding.OnLeft;
        if (occupation != null) {
          empty_occupations.Add(occupation);
        }

        // ... and up or down.
        occupation =
          my_rect.IsBelow(door)
            ? surrounding.OnTop
            : surrounding.OnBottom;
        if (occupation != null) {
          empty_occupations.Add(occupation);
        }
      }

      Move(region, empty_occupations);
    }

    void MoveRandomly() {
      Region region = location_.Region;
      Surrounding surrounding = region.GetSurrounding(location_.Index);

      // Lets check if the room has any door, if don't, there is no reason
      // to move to anywhere.
      Rect[] doors = region.GetDoors().ToArray();
      if (doors.Length == 0) {
        return;
      }

      // Since we do not know the door direction, we should move randonmly to
      // a surrounding rect that is empty.
      Occupation[] occupations = surrounding.ToArray();
      var empty_occupations = new List<Occupation>();
      foreach (Occupation occupation in occupations) {
        if (occupation != null) {
          if (occupation.Person == null) {
            empty_occupations.Add(occupation);
          } else if (occupation.Person != null) {
            // If there is someone close to us we can learn the exit
            // path from it.
            know_exits_ = occupation.Person.KnowExit;
          }
        }
      }

      Move(region, empty_occupations);
    }

    void Move(Region region, List<Occupation> occupations) {
      int count = occupations.Count;
      if (count > 0) {
        int index = rand_.Next(0, count);
        Occupation occupation = occupations[index];
        location_ = region.PlacePerson(this, occupation.Location.Index);
      }
    }

    public bool KnowExit => know_exits_;

    public Location Location => location_;
  }
}
