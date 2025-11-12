
using PROG_POE.Formz;
using PROG_POE.Model;
using PROG_POE.Service;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROG_POE
{
    public partial class MainForm : Form
    {
        private readonly IDataService _dataService;
        private readonly IEventService _eventService;
        private readonly IRequestService _requestService;

        public MainForm()
        {
            InitializeComponent();
            _dataService = new DataService();
            _eventService = new EventService();
            _requestService = new RequestService(); // Initialize the request service
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            btnEvents.Enabled = true;
            btnStatus.Enabled = true; // Enable the status button
            btnCommunityHub.Enabled = true;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            ReportForm reportForm = new ReportForm(_dataService);
            reportForm.Show();
            this.Hide();
        }

        private void btnEvents_Click(object sender, EventArgs e)
        {
            EventsForm eventsForm = new EventsForm(_eventService);
            eventsForm.Show();
            this.Hide();
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            var requestService = new RequestService();
            var statusForm = new PROG_POE.Forms.RequestStatusForm(requestService); // Full namespace
            statusForm.Show();
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
    }
}