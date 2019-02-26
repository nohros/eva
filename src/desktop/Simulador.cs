using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Eva.Layout;

namespace desktop
{
  public partial class Simulador : Form
  {
    const int kRectSize = 10;
    readonly Dictionary<int, Region> regions_;

    public Simulador() {
      InitializeComponent();

      regions_ = new Dictionary<int, Region>();

      CreateMap();
    }

    void CreateMap() {
      Region[] regions = {
        CreateRegion(0, 33, 18, 18, "R::1"),
        CreateRegion(0, 52, 9, 18, "R::2"),
        CreateRegion(0, 62, 21, 18, "R::3"),
        CreateRegion(19, 33, 10, 18, "R::4"),
        CreateRegion(19, 44, 7, 4, "R::5"),
        CreateRegion(24, 44, 7, 13, "R::6"),
        CreateRegion(19, 52, 5, 27, "R::7"),
        CreateRegion(19, 58, 7, 4, "R::8"),
        CreateRegion(19, 66, 3, 8, "R::9"),
        CreateRegion(19, 70, 13, 18, "R::10"),
        CreateRegion(24, 58, 7, 3, "R::11"),
        CreateRegion(28, 58, 11, 9, "R::12"),
        CreateRegion(13, 91, 27, 24, "R::13"),
        //CreateRegion(,,,, "R::14");
        CreateRegion(38, 58, 5, 8, "R::15"),
        CreateRegion(38, 64, 5, 8, "R::16"),
        CreateRegion(38, 70, 20, 8, "R::17"),
        CreateRegion(38, 91, 3, 8, "R::18"),
        CreateRegion(47, 0, 20, 27, "R::19"),
        CreateRegion(47, 21, 89, 6, "R::20"),
        CreateRegion(54, 21, 21, 20, "R::21"),
        CreateRegion(54, 43, 21, 20, "R::22"),
        CreateRegion(54, 65, 21, 20, "R::23"),
        CreateRegion(54, 86, 21, 20, "R::24")
      };

      // Add doors
      regions[0].AddDoor(new Location(regions[7], 1), 322);
      regions[0].AddDoor(new Location(regions[7], 2), 323);
      regions[0].AddDoor(new Location(regions[7], 3), 324);

      foreach (Region region in regions) {
        IEnumerable<Rect> doors = region.GetDoors();
        foreach (Rect door in doors) {
          Panel panel = CreateDoor(door.Left, door.Top, "");
          planta.Controls.Add(panel);
          panel.BringToFront();
        }
      }
    }

    Region CreateRegion(int top,
      int left,
      int width,
      int height,
      string name = "") {
      Region region =
        new Region(top * kRectSize, left * kRectSize, width,
          height, kRectSize);
      int i = 0;
      foreach (Rect rect in region) {
        Panel panel = CreatePanel(rect.Left, rect.Top, name);
        planta.Controls.Add(panel);
      }

      return region;
    }

    Panel CreateDoor(int left, int top, string name) {
      Panel panel = CreatePanel(left, top, name);
      panel.BackColor = System.Drawing.Color.Green;
      return panel;
    }

    Panel CreatePanel(int left, int top, string name) {
      return new Panel {
        Height = 10,
        Width = 10,
        Location = new System.Drawing.Point(left, top),
        BackColor = System.Drawing.Color.Gainsboro,
        BorderStyle = BorderStyle.FixedSingle,
        Name = name
      };
    }

    private void InitializeComponent() {
      this.planta = new System.Windows.Forms.Panel();
      this.SuspendLayout();
      // 
      // planta
      // 
      this.planta.Location = new System.Drawing.Point(13, 13);
      this.planta.Name = "planta";
      this.planta.Size = new System.Drawing.Size(1159, 737);
      this.planta.TabIndex = 0;
      // 
      // Simulador
      // 
      this.ClientSize = new System.Drawing.Size(1184, 762);
      this.Controls.Add(this.planta);
      this.Name = "Simulador";
      this.ResumeLayout(false);

    }
  }
}
