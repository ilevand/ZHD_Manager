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
    public partial class PersonControl : UserControl
    {
        public PersonInfo person;
        TrainDatabase db;
        public PersonControl(PersonInfo person, TrainDatabase db)
        {
            InitializeComponent();
            this.person = person;
            this.db = db;
            this.button1.Text = this.person.Login;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            db.PersonList.Remove(this.person);
            db.SaveChanges();
            PersonEditor.Redraw del = ((PersonEditor)this.ParentForm).Redraw_impl;
            this.ParentForm.Invoke(del);
        }
    }
}
