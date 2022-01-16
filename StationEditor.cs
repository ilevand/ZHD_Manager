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
    public partial class StationEditor : Form
    {
        internal delegate void Redraw();
        TrainDatabase db;

        public StationEditor(TrainDatabase db)
        {
            InitializeComponent();
            this.db = db;
            Redraw_impl();
        }

        public void Redraw_impl()
        {
            this.flowLayoutPanel1.Controls.Clear();
            foreach (var station in db.Stations.ToArray())
            {
                this.flowLayoutPanel1.Controls.Add(new StationControl(station, db));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var stationCreateScreen = new StationCreateScreen(db);
            stationCreateScreen.ShowDialog();
            Redraw_impl();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
