using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace ZHD_Manager
{
    public partial class WagonEditor : UserControl
    {
        public WagonInfo wagon;
        public WagonEditor(WagonInfo wagon)
        {
            InitializeComponent();
            this.wagon = wagon;
            this.flowLayoutPanel1.Size = new System.Drawing.Size(
                40 * 2 + 15,
                40 * 8 + 20
                );
            this.flowLayoutPanel2.Size = new System.Drawing.Size(
                40 * 2 + 15,
                40 * 8 + 20
                );
            this.Size = new System.Drawing.Size(
                this.flowLayoutPanel1.Size.Width + this.flowLayoutPanel2.Size.Width + 50,
                this.flowLayoutPanel1.Size.Height
                );
            foreach ((PlaceInfo place, int index) in wagon.Places.Select((PlaceInfo place, int index) => (place, index)))
            {
                if (index % 4 <= 1)
                {
                    this.flowLayoutPanel1.Controls.Add(new NumberedButton(place, index));
                } else
                {
                    this.flowLayoutPanel2.Controls.Add(new NumberedButton(place, index));
                }
            }
        }

        public class NumberedButton : Button
        {
            public NumberedButton(PlaceInfo place, int index)
            {
                this.place = place;
                this.Size = new System.Drawing.Size(40, 40);
                this.Text = index.ToString();
                this.BackColor = place.IsAvailable ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                this.Click += new EventHandler((object o, EventArgs e) => OnClick());
            }
            public void OnClick()
            {
                this.place.IsAvailable = this.place.IsAvailable ^ true;
                this.BackColor = this.place.IsAvailable ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            }

            public PlaceInfo place;
            public int index;
        }
    }
}
