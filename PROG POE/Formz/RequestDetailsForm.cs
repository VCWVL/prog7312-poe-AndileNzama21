using PROG_POE.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROG_POE.Formz
{
    public partial class RequestDetailsForm : Form
    {
        private readonly ServiceRequest _request;
        private readonly List<RequestUpdate> _updates;

        public RequestDetailsForm(ServiceRequest request, List<RequestUpdate> updates)
        {
            InitializeComponent();
            _request = request;
            _updates = updates;
        }

        private void RequestDetailsForm_Load(object sender, EventArgs e)
        {
            DisplayRequestDetails();
            DisplayRequestUpdates();
        }

        private void DisplayRequestDetails()
        {
            lblReferenceNumber.Text = _request.ReferenceNumber;
            lblTitle.Text = _request.Title;
            lblCategory.Text = _request.Category;
            lblLocation.Text = _request.Location;
            lblPriority.Text = _request.Priority.ToString();
            lblStatus.Text = _request.Status.ToString();
            lblCreatedDate.Text = _request.CreatedDate.ToString("f");
            lblAssignedTo.Text = _request.AssignedTo ?? "Not Assigned";
            lblEstimatedDays.Text = _request.EstimatedDays.ToString();
            txtDescription.Text = _request.Description;

            // Color coding based on priority
            switch (_request.Priority)
            {
                case RequestPriority.Critical:
                    lblPriority.ForeColor = Color.Red;
                    break;
                case RequestPriority.High:
                    lblPriority.ForeColor = Color.Orange;
                    break;
                case RequestPriority.Medium:
                    lblPriority.ForeColor = Color.Blue;
                    break;
                case RequestPriority.Low:
                    lblPriority.ForeColor = Color.Green;
                    break;
            }

            // Color coding based on status
            switch (_request.Status)
            {
                case RequestStatus.Resolved:
                case RequestStatus.Closed:
                    lblStatus.ForeColor = Color.Green;
                    break;
                case RequestStatus.InProgress:
                    lblStatus.ForeColor = Color.Blue;
                    break;
                case RequestStatus.UnderReview:
                    lblStatus.ForeColor = Color.Orange;
                    break;
                default:
                    lblStatus.ForeColor = Color.Black;
                    break;
            }
        }

        private void DisplayRequestUpdates()
        {
            listViewUpdates.Items.Clear();

            foreach (var update in _updates.OrderByDescending(u => u.UpdateDate))
            {
                var item = new ListViewItem(update.UpdateDate.ToString("g"));
                item.SubItems.Add(update.Description);
                item.SubItems.Add(update.UpdatedBy);
                item.SubItems.Add(update.PreviousStatus.ToString());
                item.SubItems.Add(update.NewStatus.ToString());

                // Color coding for status changes
                if (update.NewStatus == RequestStatus.Resolved || update.NewStatus == RequestStatus.Closed)
                    item.BackColor = Color.LightGreen;
                else if (update.NewStatus == RequestStatus.InProgress)
                    item.BackColor = Color.LightBlue;

                listViewUpdates.Items.Add(item);
            }

            lblUpdatesCount.Text = $"{_updates.Count} updates";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Request details would be printed or exported here.",
                "Print Feature", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

