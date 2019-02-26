using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Eva.Layout;

namespace desktop
{
  public partial class Form1 : Form
  {
    const int kRectSize = 10;
    readonly Dictionary<int, Region> regions_;

    public Form1() {
      InitializeComponent();

      regions_ = new Dictionary<int, Region>();

      CreateMap();
    }

    void CreateMap() {
      CreateRegion(0, 33, 18, 18, "R::1");
      CreateRegion(0, 52, 9, 18, "R::2");
      CreateRegion(0, 62, 21, 18, "R::3");
      CreateRegion(19, 33, 10, 18, "R::4");
      CreateRegion(19, 44, 7, 4, "R::5");
      CreateRegion(24, 44, 7, 13, "R::6");
      CreateRegion(19, 52, 5, 27, "R::7");
      CreateRegion(19, 58, 7, 4, "R::8");
      CreateRegion(19, 66, 3, 8, "R::9");
      CreateRegion(19, 70, 13, 18, "R::10");
      CreateRegion(24, 58, 7, 3, "R::11");
      CreateRegion(28, 58, 11, 9, "R::12");
      CreateRegion(13, 91, 27, 24, "R::13");
      //CreateRegion(,,,, "R::14");
      CreateRegion(38, 58, 5, 8, "R::15");
      CreateRegion(38, 64, 5, 8, "R::16");
      CreateRegion(38, 70, 20, 8, "R::17");
      CreateRegion(38, 91, 3, 8, "R::18");
      CreateRegion(47, 0, 20, 27, "R::19");
      CreateRegion(47, 21, 89, 6, "R::20");
      CreateRegion(54, 21, 21, 20, "R::21");
      CreateRegion(54, 43, 21, 20, "R::22");
      CreateRegion(54, 65, 21, 20, "R::23");
      CreateRegion(54, 86, 21, 20, "R::24");
    }

    Region CreateRegion(int top,
      int left,
      int width,
      int height,
      string name = "") {
      Region region =
        new Region(top * kRectSize, left * kRectSize, width,
          height, kRectSize);
      foreach (Rect rect in region) {
        Panel panel = CreatePanel(rect.Left, rect.Top, name);
        planta.Controls.Add(panel);
      }

      return region;
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
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1184, 762);
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
      string key = "R::" + txtId.Text;

      button1.BackColor = System.Drawing.Color.Black;

      int top = int.Parse(txtTop.Text);
      int left = int.Parse(txtLeft.Text);
      int width = int.Parse(txtWidth.Text);
      int height = int.Parse(txtHeight.Text);

      var controls = planta.Controls;
      var removable = new List<Control>();
      for (int i = 0; i < controls.Count; i++) {
        Control control = controls[i];
        if (control.Name == key) {
          removable.Add(control);
        }
      }

      foreach (var control in removable) {
        controls.Remove(control);
      }

      planta.Refresh();

      CreateRegion(top, left, width, height, key);

      button1.BackColor = System.Drawing.Color.Green;
    }

    private void GotFocus(object sender, EventArgs e)
    {
      ((TextBox)sender).SelectAll();
    }
  }
}
