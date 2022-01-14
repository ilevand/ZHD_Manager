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
    public partial class PersonEditor : Form
    {
        internal delegate void Redraw();
        TrainDatabase db;

        public PersonEditor(TrainDatabase db)
        {
            InitializeComponent();
            this.db = db;
            Redraw_impl();
        }

        public void Redraw_impl()
        {
            this.flowLayoutPanel1.Controls.Clear();
            foreach (var person in db.PersonList.ToArray())
            {
                this.flowLayoutPanel1.Controls.Add(new PersonControl(person, db));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var personCreateScreen = new PersonCreateScreen(db);
            personCreateScreen.ShowDialog();
            Redraw_impl();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
