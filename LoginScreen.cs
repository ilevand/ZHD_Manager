using System;
using System.Windows.Forms;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ZHD_Manager
{
    public partial class LoginScreen : Form
    {
        public LoginScreen()
        {
            InitializeComponent();
            using (var db = new TrainDatabase())
            {
                if (db == null)
                {
                    MessageBox.Show("No DB Connection");
                    return;
                }
                var rootEntry = db.PersonList.Where((person) => person.Login == "root").FirstOrDefault();
                if (rootEntry != null)
                {
                    db.PersonList.Remove(rootEntry);
                }
                db.PersonList.Add(new PersonInfo { ID = 0, Login = "root", Password = "root" });
                db.SaveChanges();
                Filler.FillDB(db);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var db = new TrainDatabase())
            {
                var login = this.passwordTextBox.Text;
                var person = db.PersonList.Where((nextPerson) => nextPerson.Login == login).FirstOrDefault();
                if (person == null)
                {
                    MessageBox.Show($"No specified user: {login}.");
                    return;
                }
                var password = this.loginTextBox.Text;
                if (person.Password != password)
                {
                    MessageBox.Show($"Specified password is incorrect.");
                    return;
                }
                new Thread(() => new StationView(person).ShowDialog()).Start();
                this.Close();
                return;
            }
                
            
        }
    }
}
