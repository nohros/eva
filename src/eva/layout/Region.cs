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
    readonly int size_;
    readonly int rect_size_;
    readonly Dictionary<int, Location> doors_;
    readonly MatrixEntry[][] matrix_;

    public Region(int top, int left, int size, int rect_size) {
      top_ = top;
      left_ = left;
      size_ = size;
      rect_size_ = rect_size;
      matrix_ = new MatrixEntry[size][];

      for (int i = 0; i < size; i++) {
        MatrixEntry[] rects = matrix_[i] = new MatrixEntry[size];
        for (int j = 0; j < size; j++) {
          int index = j + i + (size_ - 1) * i + 1;
          Location location = new Location(this, index);
          rects[j] = new MatrixEntry {
            Rect = new Rect(top, left, rect_size),
            Occupation = new Occupation(location, null)
          };
          left = left + rect_size;
        }

        top = top + rect_size;
      }

      doors_ = new Dictionary<int, Location>();
    }

    public void AddDoor(Location location, int index) {
      //
      doors_[index] = location;
    }

    public Location GoToDoor(Person person, Location location) {
      throw new NotSupportedException();
    }

    public Location PlacePerson(Person person, int index) {
      for (int i = 0; i < size_; i++) {
        for (int j = 0; j < size_; j++) {
          MatrixEntry entry = matrix_[i][j];
          Location location = entry.Occupation.Location;
          if (location.Index == index) {
            entry.Occupation = new Occupation(location, person);
            return location;
          }
        }
      }

      throw new ArgumentOutOfRangeException("Invalid location index");
    }

    public Surrounding GetSurrounding(int index) {
      for (int i = 0; i < size_; i++)
      {
        for (int j = 0; j < size_; j++)
        {
          int idx = j + i + (size_ - 1) * i + 1;
          if (idx == index) {
            var occupations = new Occupation[5];
            occupations[0] = matrix_[i][j].Occupation;
            if (j == 0) {
              occupations[2] = matrix_[i][j + 1].Occupation;
            } else if (j == size_ - 1) {
              occupations[1] = matrix_[i][j - 1].Occupation;
            } else {
              occupations[1] = matrix_[i][j - 1].Occupation;
              occupations[2] = matrix_[i][j + 1].Occupation;
            }

            if (i == 0) {
              occupations[4] = matrix_[i + 1][j].Occupation;
            } else if (i == size_ - 1) {
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
        }
      }
      throw new ArgumentOutOfRangeException("The index [{0}] does not represents a valid location");
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
