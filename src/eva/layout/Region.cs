using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;

namespace Eva.Layout
{
  public class Region : IEnumerable<Rect>
  {
    class MatrixEntry
    {
      public Rect Rect { get; set; }
      public Occupation Occupation { get; set; }
    }

    readonly int top_;
    readonly int left_;
    readonly int width_;
    readonly int height_;
    readonly int rect_size_;
    readonly Dictionary<int, Location> doors_;
    readonly MatrixEntry[][] matrix_;
    readonly Dictionary<int, Tuple<int, int>> matrix_index_;
    readonly Random rand_;

    public Region(int top, int left, int width, int height, int rect_size) {
      top_ = top;
      left_ = left;
      width_ = width;
      height_ = height;
      rect_size_ = rect_size;
      matrix_ = new MatrixEntry[height_][];
      matrix_index_ = new Dictionary<int, Tuple<int, int>>();
      rand_ = new Random();

      int next_top = top, next_left = left;
      for (int i = 0; i < height_; i++) {
        MatrixEntry[] rects = matrix_[i] = new MatrixEntry[width_];
        for (int j = 0; j < width_; j++) {
          int index = j + i + (width_ - 1) * i + 1;
          Location location = new Location(this, index);
          rects[j] = new MatrixEntry {
            Rect = new Rect(next_top, next_left, rect_size),
            Occupation = new Occupation(location, null)
          };
          matrix_index_[index] = new Tuple<int, int>(i, j);
          next_left = next_left + rect_size;
        }

        next_top = next_top + rect_size;
        next_left = left;
      }

      doors_ = new Dictionary<int, Location>();
    }

    public void AddDoor(Location location, int index) {
      if (location.Region == this) {
        throw new ArgumentOutOfRangeException(
          "Location should point to another region");
      }

      GetEntryByIndex(index);

      doors_[index] = location;
    }

    public IEnumerable<Rect> GetDoors() {
      foreach (KeyValuePair<int, Location> pair in doors_) {
        Tuple<int, int> tuple = matrix_index_[pair.Key];
        yield return matrix_[tuple.Item1][tuple.Item2].Rect;
      }
    }

    public IEnumerable<Location> GetEmptyLocations() {
      for (int i = 0; i < height_; i++) {
        for (int j = 0; j < width_; j++) {
          MatrixEntry entry = matrix_[i][j];
          if (entry.Occupation.Person == null) {
            yield return entry.Occupation.Location;
          }
        }
      }
    }

    public Location PlacePerson(Person person, int index) {
      RemovePerson(person);

      MatrixEntry entry = GetEntryByIndex(index);
      Location location = entry.Occupation.Location;
      entry.Occupation = new Occupation(location, person);

      return location;
    }

    void RemovePerson(Person person) {
      Location location = person.Location;
      MatrixEntry entry = GetEntryByIndex(location.Index);
      entry.Occupation = new Occupation(location, null);
    }

    /*public bool PlacePerson(Person person, out Location location) {
      int[] indexes = matrix_index_.Keys.ToArray();
      for (int i = 0; i < 10; i++) {
        int key = rand_.Next(0, indexes.Length - 1);
        int index = indexes[key];
        MatrixEntry entry = GetEntryByIndex(index);
        Occupation occupation = entry.Occupation;
        location = occupation.Location;
        if (occupation.Person == null) {
          entry.Occupation = new Occupation(location, person);
          return true;
        }
      }
      
      location = null;
      return false;
    }*/

    public Surrounding GetSurrounding(int index) {
      Tuple<int, int> tuple = GetMatrixTuple(index);
      int i = tuple.Item1, j = tuple.Item2;
      var occupations = new Occupation[5];
      occupations[0] = matrix_[i][j].Occupation;
      if (j == 0) {
        occupations[2] = matrix_[i][j + 1].Occupation;
      } else if (j == width_ - 1) {
        occupations[1] = matrix_[i][j - 1].Occupation;
      } else {
        occupations[1] = matrix_[i][j - 1].Occupation;
        occupations[2] = matrix_[i][j + 1].Occupation;
      }

      if (i == 0) {
        occupations[4] = matrix_[i + 1][j].Occupation;
      } else if (i == height_ - 1) {
        occupations[3] = matrix_[i - 1][j].Occupation;
      } else {
        occupations[3] = matrix_[i - 1][j].Occupation;
        occupations[4] = matrix_[i + 1][j].Occupation;
      }

      return new Surrounding(
        occupations[0], occupations[1],
        occupations[2], occupations[3],
        occupations[4]);
    }

    public Rect GetRectByIndex(int index) {
      return GetEntryByIndex(index).Rect;
    }

    MatrixEntry GetEntryByIndex(int index) {
      Tuple<int, int> loc = GetMatrixTuple(index);

      int i = loc.Item1, j = loc.Item2;
      return matrix_[i][j];
    }

    Tuple<int,int> GetMatrixTuple(int index) {
      if (!matrix_index_.TryGetValue(index, out var loc)) {
        throw new ArgumentOutOfRangeException("Invalid location index");
      }

      return loc;
    }

    IEnumerator IEnumerable.GetEnumerator() {
      return GetEnumerator();
    }

    public IEnumerator<Rect> GetEnumerator() {
      foreach (MatrixEntry[] entries in matrix_) {
        foreach (MatrixEntry entry in entries) {
          yield return entry.Rect;
        }
      }
    }
  }
}
