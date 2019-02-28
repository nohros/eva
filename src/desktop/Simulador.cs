using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Eva;
using Eva.Layout;
using Region = Eva.Layout.Region;

namespace desktop
{
  public partial class Simulador : Form
  {
    struct RegionPath
    {
      public Region Region { get; set; }
      public int[] Indexes { get; set; }
    }

    const int kPersonsCount = 266;
    const int kRectSize = 10;

    readonly Region[] regions_;
    readonly Person[] persons_;
    readonly Dictionary<Position, Panel> positions_;
    readonly Random rand_;

    public Simulador() {
      InitializeComponent();

      rand_ = new Random();
      positions_ = new Dictionary<Position, Panel>();
      regions_ = CreateMap();
      persons_ = CreatePersons(kPersonsCount);
    }

    Person[] CreatePersons(int count) {
      var persons = new List<Person>();
      for (int i = 0; i < count; i++) {
        int speed = rand_.Next(1, 3);
        int know_exists = rand_.Next(0, 2);
        Person person = CreatePerson(speed, know_exists == 1);
        persons.Add(person);
      }

      return persons.ToArray();
    }

    Person CreatePerson(int speed, bool know_exists) {
      Person person = null;
      while (person == null) {
        int index = rand_.Next(0, regions_.Length);

        // Do not place persons on dummy areas
        if (index > 23) {
          continue;
        }

        //int index = rand_.Next(0, 1);
        Region region = regions_[index];
        Location[] locations =
          region
            .GetEmptyLocations()
            .ToArray();

        if (locations.Length > 0) {
          index = rand_.Next(0, locations.Length - 1);
          Location location = locations[index];
          person = new Person(speed, location, know_exists);
          region.PlacePerson(person, location.Index);

          Rect rect = region.GetRectByIndex(location.Index);
          Panel panel = CreatePanel(rect.Left, rect.Top, "");
          planta.Controls.Add(panel);
          panel.BackColor = panel.BackColor = System.Drawing.Color.Red;
        }
      }

      return person;
    }

    Region[] CreateMap() {
      Region[] regions = {
        CreateRegion(0, 23, 18, 18, 1),
        CreateRegion(0, 42, 9, 18, 2),
        CreateRegion(0, 52, 21, 18, 3),
        CreateRegion(19, 23, 10, 18, 4),
        CreateRegion(19, 34, 7, 4, 5),
        CreateRegion(24, 34, 7, 13, 6),
        CreateRegion(19, 42, 5, 23, 7),
        CreateRegion(19, 48, 7, 4, 8),
        CreateRegion(19, 56, 3, 8, 9),
        CreateRegion(19, 60, 13, 14, 10),
        CreateRegion(24, 48, 7, 3, 11),
        CreateRegion(28, 48, 11, 5, 12),
        CreateRegion(16, 78, 19, 17, 13),
        CreateRegion(34, 48, 5, 8, 14),
        CreateRegion(34, 54, 5, 8, 15),
        CreateRegion(34, 60, 17, 8, 16),
        CreateRegion(34, 78, 3, 8, 17),
        CreateRegion(43, 0, 10, 27, 18),
        CreateRegion(43, 11, 86, 6, 19),
        CreateRegion(50, 11, 21, 20, 20),
        CreateRegion(50, 33, 21, 20, 21),
        CreateRegion(50, 55, 21, 20, 22),
        CreateRegion(50, 77, 20, 20, 23),
        CreateRegion(43, 98, 15, 27, 24),
        CreateRegion(38,0,41,4,25),
        CreateRegion(34, 82, 31, 8, 26)
    };

      // Add doors
      AddDoors(regions);

      //regions[1].AddDoor(new Location(regions[7], 3), 156);

      foreach (Region region in regions) {
        IEnumerable<Rect> doors = region.GetDoors();
        foreach (Rect door in doors) {
          var position = new Position(door.Top, door.Left);
          ChangePanelColor(position, Color.Green);
          //Panel panel = CreateDoor(door.Left, door.Top, "");
          //planta.Controls.Add(panel);
          //panel.BringToFront();
        }
      }

      return regions;
    }

    void AddDoors(Region[] regions) {
      // @formatter:off
      AddDoors(new RegionPath { Region = regions[0], Indexes = new[] { 322, 323, 324 } }, new RegionPath { Region = regions[4], Indexes = new[] { 5, 6, 7 } });
      AddDoors(new RegionPath { Region = regions[1], Indexes = new[] { 154, 155, 156 } }, new RegionPath { Region = regions[6], Indexes = new[] { 1, 2, 3 } });
      AddDoors(new RegionPath { Region = regions[2], Indexes = new[] { 358, 359, 360 } }, new RegionPath { Region = regions[7], Indexes = new[] { 5, 6, 7 } });
      AddDoors(new RegionPath { Region = regions[3], Indexes = new[] { 10, 20, 30 } }, new RegionPath { Region = regions[4], Indexes = new[] { 1, 8, 15 } });
      AddDoors(new RegionPath { Region = regions[4], Indexes = new[] { 7, 14, 21 } }, new RegionPath { Region = regions[6], Indexes = new[] { 1, 6, 11 } });
      AddDoors(new RegionPath { Region = regions[5], Indexes = new[] { 77, 84, 91 } }, new RegionPath { Region = regions[6], Indexes = new[] { 76, 81, 86 } });
      AddDoors(new RegionPath { Region = regions[6], Indexes = new[] { 96, 101, 106 } }, new RegionPath { Region = regions[24], Indexes = new[] { 41, 82, 123 } });
      AddDoors(new RegionPath { Region = regions[7], Indexes = new[] { 1, 8, 15 } }, new RegionPath { Region = regions[6], Indexes = new[] { 5, 10, 15 } });
      AddDoors(new RegionPath { Region = regions[8], Indexes = new[] { 1, 4, 7 } }, new RegionPath { Region = regions[7], Indexes = new[] { 7, 14, 21 } });
      AddDoors(new RegionPath { Region = regions[9], Indexes = new[] { 1, 14, 27, 40, 53, 66, 79, 92 } }, new RegionPath { Region = regions[8], Indexes = new[] { 3, 6, 9, 12, 15, 18, 21, 24 } });
      AddDoors(new RegionPath { Region = regions[10], Indexes = new[] { 2, 3, 4 } }, new RegionPath { Region = regions[7], Indexes = new[] { 23, 24, 25 } });
      AddDoors(new RegionPath { Region = regions[11], Indexes = new[] { 12, 23, 34 } }, new RegionPath { Region = regions[6], Indexes = new[] { 55, 60, 65 } });
      AddDoors(new RegionPath { Region = regions[12], Indexes = new[] { 305, 306, 307 } }, new RegionPath { Region = regions[16], Indexes = new[] { 1, 2, 3 } });
      AddDoors(new RegionPath { Region = regions[13], Indexes = new[] { 1, 6, 11 } }, new RegionPath { Region = regions[6], Indexes = new[] { 80, 85, 90 } });
      AddDoors(new RegionPath { Region = regions[14], Indexes = new[] { 1, 6, 11 } }, new RegionPath { Region = regions[13], Indexes = new[] { 5, 10, 15 } });
      AddDoors(new RegionPath { Region = regions[15], Indexes = new[] { 130, 131, 132 } }, new RegionPath { Region = regions[18], Indexes = new[] { 60, 61, 62 } });
      AddDoors(new RegionPath { Region = regions[16], Indexes = new[] { 22, 23, 24 } }, new RegionPath { Region = regions[18], Indexes = new[] { 68, 69, 70 } });
      AddDoors(new RegionPath { Region = regions[17], Indexes = new[] { 10, 20, 30 } }, new RegionPath { Region = regions[18], Indexes = new[] { 1, 87, 173 } });
      AddDoors(new RegionPath { Region = regions[18], Indexes = new[] { 72, 73, 74 } }, new RegionPath { Region = regions[25], Indexes = new[] { 218, 219, 220 } });
      AddDoors(new RegionPath { Region = regions[19], Indexes = new[] { 1, 2, 3 } }, new RegionPath { Region = regions[18], Indexes = new[] { 431, 432, 433 } });
      AddDoors(new RegionPath { Region = regions[20], Indexes = new[] { 1, 2, 3 } }, new RegionPath { Region = regions[18], Indexes = new[] { 453, 454, 455 } });
      AddDoors(new RegionPath { Region = regions[21], Indexes = new[] { 1, 2, 3 } }, new RegionPath { Region = regions[18], Indexes = new[] { 475, 476, 477 } });
      AddDoors(new RegionPath { Region = regions[22], Indexes = new[] { 1, 2, 3 } }, new RegionPath { Region = regions[18], Indexes = new[] { 497, 498, 499 } });
      AddDoors(new RegionPath { Region = regions[23], Indexes = new[] { 16, 31, 46 } }, new RegionPath { Region = regions[18], Indexes = new[] { 172, 258, 344 } });
      // @formatter:on
    }

    void AddDoors(RegionPath source, RegionPath destination) {
      for (int i = 0; i < source.Indexes.Length; i++) {
        Location location =
          new Location(destination.Region, destination.Indexes[i]);
        source.Region.AddDoor(location, source.Indexes[i]);
      }
    }

    Region CreateRegion(int top,
      int left,
      int width,
      int height,
      int index = 0) {
      Region region =
        new Region(top * kRectSize, left * kRectSize, width,
          height, kRectSize);
      int i = 0;
      foreach (Rect rect in region) {
        Panel panel = CreatePanel(rect.Left, rect.Top, index.ToString());
        var position = new Position(rect.Top, rect.Left);
        positions_[position] = panel;
        planta.Controls.Add(panel);
      }

      return region;
    }

    void ChangePanelColor(Position position, Color color) {
      Panel panel = positions_[position];
      panel.BackColor = color;
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

    void MovePersons() {
      foreach (Person person in persons_) {
        MovePerson(person);
      }
    }

    void MovePerson(Person person) {
      Position position = Position.FromLocation(person.Location);
      ChangePanelColor(position, Color.Gainsboro);

      Location location = person.Move();
      position = Position.FromLocation(location);

      ChangePanelColor(position, Color.Red);
    }

    private void InitializeComponent() {
      this.components = new System.ComponentModel.Container();
      this.planta = new System.Windows.Forms.Panel();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.SuspendLayout();
      // 
      // planta
      // 
      this.planta.Location = new System.Drawing.Point(13, 13);
      this.planta.Name = "planta";
      this.planta.Size = new System.Drawing.Size(1239, 737);
      this.planta.TabIndex = 0;
      // 
      // timer1
      // 
      this.timer1.Enabled = true;
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // Simulador
      // 
      this.ClientSize = new System.Drawing.Size(1264, 762);
      this.Controls.Add(this.planta);
      this.Name = "Simulador";
      this.ResumeLayout(false);

    }

    private void timer1_Tick(object sender, EventArgs e) {
      MovePersons();
    }
  }
}
