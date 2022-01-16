using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZHD_Manager
{
    public partial class StationControl : UserControl
    {
        public StationInfo station;
        TrainDatabase db;
        public StationControl(StationInfo station, TrainDatabase db)
        {
            InitializeComponent();
            this.station = station;
            this.db = db;
            this.button1.Text = this.station.Name;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            db.Stations.Remove(this.station);
            db.SaveChanges();
            StationEditor.Redraw del = ((StationEditor)this.ParentForm).Redraw_impl;
            this.ParentForm.Invoke(del);
        }
    }
}
