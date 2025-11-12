using PROG_POE.Form;
using PROG_POE.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PROG_POE.Form;
using PROG_POE.Service;




namespace PROG_POE
{


    public partial class MainForm : Form
    {
        private readonly IDataService _dataService;
        private readonly IEventService _eventService;

        public MainForm()
        {
            InitializeComponent();
            _dataService = new DataService();
            _eventService = new EventService();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            btnEvents.Enabled = true; // Now enabled for Part 2
            btnStatus.Enabled = false;
            btnCommunityHub.Enabled = true;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            ReportForm reportForm = new ReportForm(_dataService);
            reportForm.Show();
            this.Hide();
        }

        private void Hide()
        {
            throw new NotImplementedException();
        }

        private void btnEvents_Click(object sender, EventArgs e)
        {
            EventsForm eventsForm = new EventsForm(_eventService);
            eventsForm.Show();
            this.Hide();
        }

        private void btnCommunityHub_Click(object sender, EventArgs e)
        {
            CommunityHubForm communityForm = new CommunityHubForm(_dataService);
            communityForm.Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        internal void Show()
        {
            throw new NotImplementedException();
        }
    }

}
