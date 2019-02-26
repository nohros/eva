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
      persons_ = CreatePersons(30);
    }

    Person[] CreatePersons(int count) {
      var persons = new List<Person>();
      for (int i = 0; i < count; i++) {
        Person person = CreatePerson();
        persons.Add(person);
      }

      return persons.ToArray();
    }

    Person CreatePerson() {
      Person person = null;
      while (person == null) {
        int index = rand_.Next(0, regions_.Length);
        Region region = regions_[index];
        Location[] locations =
          region
            .GetEmptyLocations()
            .ToArray();

        if (locations.Length > 0) {
          index = rand_.Next(0, locations.Length - 1);
          Location location = locations[index];
          person = new Person(1, location, false);
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
          var position = new Position(door.Top, door.Left);
          ChangePanelColor(position, Color.Green);
          //Panel panel = CreateDoor(door.Left, door.Top, "");
          //planta.Controls.Add(panel);
          //panel.BringToFront();
        }
      }

      return regions;
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
      position = Position.FromLocation(person.Location);

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
