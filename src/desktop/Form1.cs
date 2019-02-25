using System;
using System.Windows.Forms;
using Eva.Layout;

namespace desktop
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();

      CreateRegion();
    }

    void CreateRegion() {
      Region region = new Region(0, 0, 30, 10);
      foreach (Rect rect in region) {
        var panel = new Panel {
          Height = 10,
          Width = 10,
          Location = new System.Drawing.Point(rect.Left, rect.Top)
        };
        planta.Controls.Add(panel);
      }
    }
  }
}
