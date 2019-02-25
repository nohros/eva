using System;

namespace Eva.Layout
{
  public class Surrounding
  {
    public Surrounding(Occupation center, Occupation left, Occupation right, Occupation top, Occupation bottom) {
      OnLeft = left;
      OnRight = right;
      OnBottom = bottom;
      OnTop = top;
      OnCenter = center;
    }

    public Occupation[] ToArray() {
      return new Occupation[] {
        OnCenter, OnLeft, OnRight, OnTop, OnBottom
      };
    }

    public readonly Occupation OnLeft;
    public readonly Occupation OnRight;
    public readonly Occupation OnTop;
    public readonly Occupation OnBottom;
    public readonly Occupation OnCenter;
  }
}
