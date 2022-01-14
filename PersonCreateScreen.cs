using System;
using System.Windows.Forms;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ZHD_Manager
{
    public partial class PersonCreateScreen : Form
    {
        TrainDatabase db;
        public PersonCreateScreen(TrainDatabase db)
        {
            this.db = db;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.db.PersonList.Add(new PersonInfo
            {
                Login = this.loginTextBox.Text,
                Password = this.passwordTextBox.Text
            });
            db.SaveChanges();
            this.Close();
        }
    }
}
