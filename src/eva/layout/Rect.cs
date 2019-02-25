using System;
using System.Collections.Generic;

namespace Eva.Layout
{
  /// <summary>
  /// Represents a rectangular unit of space.
  /// </summary>
  public class Rect
  {
    readonly int top_;
    readonly int left_;
    readonly int size_;
    readonly RectLock locks_;

    public Rect(int top, int left, int size, RectLock locks = RectLock.None) {
      top_ = top;
      left_ = left;
      size_ = size;
      locks_ = locks;

      Top = top;
      Left = left;
      Right = left + size;
      Bottom = top + size;

      double middle = size / 2d;
      Center = new Point(left + middle, top + middle);
    }

    public int Top { get; }
    public int Bottom { get; }
    public int Left { get; }
    public int Right { get; }
    public Point Center { get; }
  }
}
