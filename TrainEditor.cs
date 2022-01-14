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
    public partial class TrainEditor : Form
    {
        private Mode mode;
        public TrainInfo train = null;
        private TrainDatabase db = null;
        private TrainEditor(TrainDatabase db)
        {
            InitializeComponent();
            this.db = db;
            this.comboBox1.Items.AddRange(db.Stations.Select(station => station.Name).ToArray());
            this.comboBox2.Items.AddRange(db.Stations.Select(station => station.Name).ToArray());
            this.comboBox1.SelectedIndex = 0;
            this.comboBox2.SelectedIndex = 0;
            this.dateTimePicker1.Value = DateTime.Now;
            this.dateTimePicker2.Value = DateTime.Now;
            this.dateTimePicker3.Value = DateTime.Now;
        }

        public static TrainEditor OpenInEditioningMode(TrainInfo train, TrainDatabase db)
        {
            TrainEditor editor = new TrainEditor(db)
            {
                mode = Mode.Edit,
            };
            editor.train = train;
            editor.comboBox1.Text = train.StationOfDeparture.Name;
            editor.comboBox2.Text = train.StationOfArrival.Name;
            editor.dateTimePicker1.Value = train.Departure.Date;
            editor.dateTimePicker2.Value = train.Departure;
            editor.dateTimePicker3.Value = train.Arrival;
            return editor;
        }

        public static TrainEditor OpenInAddingMode(TrainInfo train, TrainDatabase db)
        {
            TrainEditor editor = new TrainEditor(db)
            {
                mode = Mode.Add,
            };
            editor.train = train;
            return editor;
        }
        public enum Mode
        {
            Edit,
            Add
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PlacesEditor placesEditor = new PlacesEditor(train, db);
            placesEditor.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.train.StationOfDeparture = db.Stations.Where(station => station.Name == this.comboBox1.Text).First();
            this.train.StationOfArrival = db.Stations.Where(station => station.Name == this.comboBox2.Text).First();
            this.train.Departure = this.dateTimePicker1.Value.Date + this.dateTimePicker2.Value.TimeOfDay;
            this.train.Arrival = this.dateTimePicker3.Value;
            if (this.mode == Mode.Add)
            {
                db.Trains.Add(this.train);
            }
            db.SaveChanges();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.mode == Mode.Edit)
            {
                db.Trains.Remove(this.train);
                db.SaveChanges();
            }
            this.Close();
        }
    }
}
