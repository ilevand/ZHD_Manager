using System;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace ZHD_Manager
{
    public partial class StationView : Form
    {
        private TableFilterData data = new TableFilterData();
        private TrainDatabase db = new TrainDatabase();
        private IEnumerable<TrainInfo> ShownTrains = null;
        private PersonInfo person;
        public StationView(PersonInfo person)
        {
            this.person = person;
            InitializeComponent();
            UpdateShownTrains();
            UpdateGrid();
            UpdateStations();
            this.button2.Text = this.person.Login;
        }

        private void UpdateStations()
        {
            this.comboBox1.Items.Clear();
            this.comboBox2.Items.Clear();
            this.comboBox1.Items.AddRange(db.Stations.Select(station => station.Name).ToArray());
            this.comboBox2.Items.AddRange(db.Stations.Select(station => station.Name).ToArray());
            this.comboBox1.SelectedIndex = 0;
            this.comboBox2.SelectedIndex = 0;
        }

        private void UpdateShownTrains()
        {
            IEnumerable<TrainInfo> trains = db.Trains;
            if (data.UseDepartureDate)
            {
                trains = trains.Where((train) => train.Departure.Date == this.data.DepartureDate.Date);
            }
            if (data.UseDepartureTime)
            {
                trains = trains
                    .Where((train) => this.data.DepartureFromTime <= train.Departure.TimeOfDay)
                    .Where((train) => train.Departure.TimeOfDay <= this.data.DepartureToTime);
            }
            if (data.UseDepartureStation) {
                var ValidDestinationStation = db.Stations.Where((station) => station.Name == data.DepartureStation).First();
                trains = trains.Where((train) => train.StationOfDeparture == ValidDestinationStation);
            }
            if (data.UseArrivalStation)
            {
                var ValidArrivalStation = db.Stations.Where((station) => station.Name == data.ArrivalStation).First();
                trains = trains.Where((train) => ValidArrivalStation == train.StationOfArrival);
            }
            if (data.UseFreeClosedTrains)
            {
                trains = trains
                    .Where(train =>
                        train
                        .Wagons
                        .Where(wagon => wagon.Type == WagonInfo.WagonType.Closed)
                        .Any(wagon => wagon.Places
                            .Any(place => place.IsAvailable)));
            }
            if (data.UseFreeOpenTrains)
            {
                trains = trains
                     .Where(train =>
                         train
                         .Wagons
                         .Where(wagon => wagon.Type == WagonInfo.WagonType.Open)
                         .Any(wagon => wagon.Places
                             .Any(place => place.IsAvailable)));
            }
            this.ShownTrains = trains;
        }

        private void UpdateGrid()
        {
            this.dataGridView1.Rows.Clear();
            foreach (var train in ShownTrains)
            {
                DataGridViewRow row = this.dataGridView1.Rows[this.dataGridView1.Rows.Add()];
                row.Cells["TrainNumber"].Value = train.ID;
                row.Cells["DepartureStation"].Value = train.StationOfDeparture.Name;
                row.Cells["ArrivalStation"].Value = train.StationOfArrival.Name;
                row.Cells["DepartureDate"].Value = train.Departure.Date;
                row.Cells["DepartureTime"].Value = train.Departure.TimeOfDay;
                row.Cells["ArrivalTime"].Value = train.Arrival.TimeOfDay;
                row.Cells["FreeClosedPlaces"].Value = train.Wagons.Where(wagon => wagon.Type == WagonInfo.WagonType.Closed).SelectMany(wagon => wagon.Places).Select(place => place.IsAvailable ? 1 : 0).Sum();
                row.Cells["FreeOpenPlaces"].Value = train.Wagons.Where(wagon => wagon.Type == WagonInfo.WagonType.Open).SelectMany(wagon => wagon.Places).Select(place => place.IsAvailable ? 1 : 0).Sum();
            }
            this.dataGridView1.Update();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            this.data.UseDepartureDate = this.checkBox6.Checked;
            this.data.DepartureDate = this.dateTimePicker3.Value.Date;
            UpdateShownTrains();
            UpdateGrid();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.data.UseDepartureTime = this.checkBox1.Checked;
            this.data.DepartureFromTime = this.dateTimePicker1.Value.TimeOfDay;
            this.data.DepartureToTime = this.dateTimePicker2.Value.TimeOfDay;
            UpdateShownTrains();
            UpdateGrid();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.data.UseArrivalStation = this.checkBox2.Checked;
            this.data.ArrivalStation = this.comboBox1.Text;
            UpdateShownTrains();
            UpdateGrid();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            this.data.UseDepartureStation = this.checkBox3.Checked;
            this.data.DepartureStation = this.comboBox2.Text;
            UpdateShownTrains();
            UpdateGrid();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            this.data.UseFreeClosedTrains = this.checkBox4.Checked;
            UpdateShownTrains();
            UpdateGrid();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            this.data.UseFreeOpenTrains = this.checkBox5.Checked;
            UpdateShownTrains();
            UpdateGrid();
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            this.data.DepartureDate = this.dateTimePicker3.Value.Date;
            if (this.data.UseDepartureDate)
            {
                UpdateShownTrains();
                UpdateGrid();
            }
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.data.DepartureFromTime = this.dateTimePicker1.Value.TimeOfDay;
            if (this.data.UseDepartureTime)
            {
                UpdateShownTrains();
                UpdateGrid();
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            this.data.DepartureToTime = this.dateTimePicker2.Value.TimeOfDay;
            if (this.data.UseDepartureTime)
            {
                UpdateShownTrains();
                UpdateGrid();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.data.ArrivalStation = this.comboBox1.Text;
            if (this.data.UseArrivalStation)
            {
                UpdateShownTrains();
                UpdateGrid();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.data.DepartureStation = this.comboBox2.Text;
            if (this.data.UseDepartureStation)
            {
                UpdateShownTrains();
                UpdateGrid();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var index = e.RowIndex;
            var train_index = Convert.ToInt32(dataGridView1.Rows[index].Cells["TrainNumber"].Value.ToString());
            var train = db.Trains.Where(train => train.ID == train_index).First();
            TrainEditor trainEditor = TrainEditor.OpenInEditioningMode(train, db);
            trainEditor.ShowDialog();
            UpdateShownTrains();
            UpdateGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var train = new TrainInfo();
            TrainEditor trainEditor = TrainEditor.OpenInAddingMode(train, db);
            trainEditor.ShowDialog();
            UpdateShownTrains();
            UpdateGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PersonEditor personEditor = new PersonEditor(db);
            personEditor.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StationEditor stationEditor = new StationEditor(db);
            stationEditor.ShowDialog();
            UpdateStations();
            UpdateShownTrains();
            UpdateGrid();
        }
    }
}
