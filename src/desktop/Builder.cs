using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Eva;
using Eva.Layout;

namespace desktop
{
  public partial class Form1 : Form
  {
    const int kRectSize = 10;
    readonly Dictionary<int, Region> regions_;
    readonly Dictionary<Position, Panel> positions_;

    public Form1() {
      InitializeComponent();

      regions_ = new Dictionary<int, Region>();
      positions_ = new Dictionary<Position, Panel>();

      CreateMap();
    }

    void CreateMap() {
      CreateRegion(0, 23, 18, 18, 1);
      CreateRegion(0, 42, 9, 18, 2);
      CreateRegion(0, 52, 21, 18, 3);
      CreateRegion(19, 23, 10, 18, 4);
      CreateRegion(19, 34, 7, 4, 5);
      CreateRegion(24, 34, 7, 13, 6);
      CreateRegion(19, 42, 5, 23, 7);
      CreateRegion(19, 48, 7, 4, 8);
      CreateRegion(19, 56, 3, 8, 9);
      CreateRegion(19, 60, 13, 14, 10);
      CreateRegion(24, 48, 7, 3, 11);
      CreateRegion(28, 48, 11, 5, 12);
      CreateRegion(16, 78, 19, 17, 13);
      CreateRegion(34, 48, 5, 8, 14);
      CreateRegion(34, 54, 5, 8, 15);
      CreateRegion(34, 60, 17, 8, 16);
      CreateRegion(34, 78, 3, 8, 17);
      CreateRegion(43, 0, 10, 27, 18);
      CreateRegion(43, 11, 86, 6, 19);
      CreateRegion(50, 11, 21, 20, 20);
      CreateRegion(50, 33, 21, 20, 21);
      CreateRegion(50, 55, 21, 20, 22);
      CreateRegion(50, 77, 20, 20, 23);
      CreateRegion(43, 98, 15, 27, 24);
      CreateRegion(38, 0, 41, 4, 25);
      CreateRegion(34, 82, 31, 8, 26);
    }

    Region CreateRegion(int top,
      int left,
      int width,
      int height,
      int index) {
      Region region =
        new Region(top * kRectSize, left * kRectSize, width,
          height, kRectSize);
      int i = 1;
      foreach (Rect rect in region) {
        string new_name = "T:" + rect.Top + "::L:" + rect.Left + "::" + i++ + "::R" + index;
        Panel panel = CreatePanel(rect.Left, rect.Top, new_name);
        var position = new Position(rect.Top, rect.Left);
        positions_[position] = panel;
        panel.MouseHover += OnMouseHover;
        planta.Controls.Add(panel);
      }

      return region;
    }

    void OnMouseHover(object sender ,EventArgs e) {
      Panel panel = (Panel) sender;
      txtRegion.Text = panel.Name;
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
      this.planta = new System.Windows.Forms.PictureBox();
      this.txtId = new System.Windows.Forms.TextBox();
      this.button1 = new System.Windows.Forms.Button();
      this.txtWidth = new System.Windows.Forms.TextBox();
      this.txtLeft = new System.Windows.Forms.TextBox();
      this.txtHeight = new System.Windows.Forms.TextBox();
      this.txtTop = new System.Windows.Forms.TextBox();
      this.txtRegion = new System.Windows.Forms.TextBox();
      ((System.ComponentModel.ISupportInitialize)(this.planta)).BeginInit();
      this.SuspendLayout();
      // 
      // planta
      // 
      this.planta.Location = new System.Drawing.Point(12, 12);
      this.planta.Name = "planta";
      this.planta.Size = new System.Drawing.Size(1160, 740);
      this.planta.TabIndex = 0;
      this.planta.TabStop = false;
      // 
      // txtId
      // 
      this.txtId.Location = new System.Drawing.Point(12, 12);
      this.txtId.Name = "txtId";
      this.txtId.Size = new System.Drawing.Size(206, 20);
      this.txtId.TabIndex = 1;
      this.txtId.GotFocus += new System.EventHandler(this.GotFocus);
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(12, 89);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 23);
      this.button1.TabIndex = 6;
      this.button1.Text = "Build";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // txtWidth
      // 
      this.txtWidth.Location = new System.Drawing.Point(12, 63);
      this.txtWidth.Name = "txtWidth";
      this.txtWidth.Size = new System.Drawing.Size(100, 20);
      this.txtWidth.TabIndex = 4;
      this.txtWidth.GotFocus += new System.EventHandler(this.GotFocus);
      // 
      // txtLeft
      // 
      this.txtLeft.Location = new System.Drawing.Point(118, 37);
      this.txtLeft.Name = "txtLeft";
      this.txtLeft.Size = new System.Drawing.Size(100, 20);
      this.txtLeft.TabIndex = 3;
      this.txtLeft.GotFocus += new System.EventHandler(this.GotFocus);
      // 
      // txtHeight
      // 
      this.txtHeight.Location = new System.Drawing.Point(118, 63);
      this.txtHeight.Name = "txtHeight";
      this.txtHeight.Size = new System.Drawing.Size(100, 20);
      this.txtHeight.TabIndex = 5;
      this.txtHeight.GotFocus += new System.EventHandler(this.GotFocus);
      // 
      // txtTop
      // 
      this.txtTop.Location = new System.Drawing.Point(12, 38);
      this.txtTop.Name = "txtTop";
      this.txtTop.Size = new System.Drawing.Size(100, 20);
      this.txtTop.TabIndex = 2;
      this.txtTop.GotFocus += new System.EventHandler(this.GotFocus);
      // 
      // txtRegion
      // 
      this.txtRegion.Location = new System.Drawing.Point(13, 137);
      this.txtRegion.Name = "txtRegion";
      this.txtRegion.Size = new System.Drawing.Size(205, 20);
      this.txtRegion.TabIndex = 7;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1184, 762);
      this.Controls.Add(this.txtRegion);
      this.Controls.Add(this.txtTop);
      this.Controls.Add(this.txtHeight);
      this.Controls.Add(this.txtLeft);
      this.Controls.Add(this.txtWidth);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.txtId);
      this.Controls.Add(this.planta);
      this.Name = "Form1";
      this.Text = "Form1";
      ((System.ComponentModel.ISupportInitialize)(this.planta)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    private void button1_Click(object sender, EventArgs e) {
      string key = "R" + txtId.Text;

      button1.BackColor = System.Drawing.Color.Black;

      int top = int.Parse(txtTop.Text);
      int left = int.Parse(txtLeft.Text);
      int width = int.Parse(txtWidth.Text);
      int height = int.Parse(txtHeight.Text);

      var controls = planta.Controls;
      var removable = new List<Control>();
      for (int i = 0; i < controls.Count; i++) {
        Control control = controls[i];
        if (control.Name.Contains(key)) {
          removable.Add(control);
        }
      }

      foreach (var control in removable) {
        controls.Remove(control);
      }

      planta.Refresh();

      CreateRegion(top, left, width, height, int.Parse(txtId.Text));

      button1.BackColor = System.Drawing.Color.Green;
    }

    private void GotFocus(object sender, EventArgs e)
    {
      ((TextBox)sender).SelectAll();
    }
  }
}
