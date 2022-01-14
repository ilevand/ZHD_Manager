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
    public partial class PlacesEditor : Form
    {
        TrainInfo train;
        TrainDatabase db;
        public PlacesEditor(TrainInfo train, TrainDatabase db)
        {
            InitializeComponent();
            this.train = train;
            this.db = db;
            UpdateView();
        }

        public void UpdateView()
        {
            this.flowLayoutPanel1.Controls.Clear();
            this.flowLayoutPanel2.Controls.Clear();
            this.flowLayoutPanel1.Controls.AddRange(
                    this.train.Wagons
                    .Where(wagon => wagon.Type == WagonInfo.WagonType.Open)
                    .Select(wagon => new WagonEditor(wagon))
                    .ToArray());

            this.flowLayoutPanel2.Controls.AddRange(
                this.train.Wagons
                .Where(wagon => wagon.Type == WagonInfo.WagonType.Closed)
                .Select(wagon => new WagonEditor(wagon))
                .ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            db.SaveChanges();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.train.Wagons.Add(new WagonInfo()
            {
                Places = Enumerable.Range(0, 32).Select(index => new PlaceInfo()
                {
                    IsAvailable = true
                }).ToArray(),
                Type = WagonInfo.WagonType.Open
            });
            db.SaveChanges();
            UpdateView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var removed_wagon_unchecked = this.train.Wagons
                .Where(wagon => wagon.Type == WagonInfo.WagonType.Open);
            if (removed_wagon_unchecked.Count() > 0)
            {
                var removed_wagon = removed_wagon_unchecked.Last();
                this.train.Wagons.Remove(removed_wagon);
                db.SaveChanges();
                UpdateView();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.train.Wagons.Add(new WagonInfo()
            {
                Places = Enumerable.Range(0, 32).Select(index => new PlaceInfo()
                {
                    IsAvailable = true
                }).ToArray(),
                Type = WagonInfo.WagonType.Closed
            });
            db.SaveChanges();
            UpdateView();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var removed_wagon_unchecked = this.train.Wagons
                .Where(wagon => wagon.Type == WagonInfo.WagonType.Closed);
            if (removed_wagon_unchecked.Count() > 0)
            {
                var removed_wagon = removed_wagon_unchecked.Last();
                this.train.Wagons.Remove(removed_wagon);
                db.SaveChanges();
                UpdateView();
            }
        }
    }
}
