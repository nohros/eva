using System;

namespace Eva.Layout
{
  [Flags]
  public enum RectLock
  {
    None = 0,
    Top = 1,
    Left = 2,
    Bottom = 4,
    Right = 8
  }
}
