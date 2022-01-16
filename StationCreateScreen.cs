using System;
using System.Windows.Forms;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ZHD_Manager
{
    public partial class StationCreateScreen : Form
    {
        TrainDatabase db;
        public StationCreateScreen(TrainDatabase db)
        {
            this.db = db;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.db.Stations.Add(new StationInfo
            {
                Name = this.nameTextBox.Text
            });
            db.SaveChanges();
            this.Close();
        }
    }
}
