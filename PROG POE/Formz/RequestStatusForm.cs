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
    public partial class RequestStatusForm : Form
    {
        private readonly IRequestService _requestService;
        private List<ServiceRequest> _currentRequests;

        public RequestStatusForm(IRequestService requestService)
        {
            InitializeComponent();
            _requestService = requestService;
        }

        private void RequestStatusForm_Load(object sender, EventArgs e)
        {
            LoadAllRequests();
            InitializeFilters();
            UpdateStatistics();
            LoadAdvancedDataStructureExamples();
        }

        private void LoadAllRequests()
        {
            _currentRequests = _requestService.GetAllRequests();
            DisplayRequests(_currentRequests);
        }

        private void InitializeFilters()
        {
            cmbStatusFilter.Items.Clear();
            cmbStatusFilter.Items.Add("All Statuses");
            foreach (RequestStatus status in Enum.GetValues(typeof(RequestStatus)))
            {
                cmbStatusFilter.Items.Add(status.ToString());
            }
            cmbStatusFilter.SelectedIndex = 0;

            cmbPriorityFilter.Items.Clear();
            cmbPriorityFilter.Items.Add("All Priorities");
            foreach (RequestPriority priority in Enum.GetValues(typeof(RequestPriority)))
            {
                cmbPriorityFilter.Items.Add(priority.ToString());
            }
            cmbPriorityFilter.SelectedIndex = 0;

            cmbCategoryFilter.Items.Clear();
            cmbCategoryFilter.Items.Add("All Categories");
            var categories = _currentRequests.Select(r => r.Category).Distinct().OrderBy(c => c);
            foreach (var category in categories)
            {
                cmbCategoryFilter.Items.Add(category);
            }
            cmbCategoryFilter.SelectedIndex = 0;
        }

        private void DisplayRequests(List<ServiceRequest> requests)
        {
            listViewRequests.Items.Clear();

            foreach (var request in requests.OrderByDescending(r => r.CreatedDate))
            {
                var item = new ListViewItem(request.ReferenceNumber);
                item.SubItems.Add(request.Title);
                item.SubItems.Add(request.Category);
                item.SubItems.Add(request.Location);
                item.SubItems.Add(request.Priority.ToString());
                item.SubItems.Add(request.Status.ToString());
                item.SubItems.Add(request.CreatedDate.ToShortDateString());
                item.SubItems.Add(request.AssignedTo ?? "Not Assigned");
                item.SubItems.Add(request.EstimatedDays.ToString());

                // Color coding based on priority and status
                if (request.Priority == RequestPriority.Critical)
                    item.BackColor = Color.LightCoral;
                else if (request.Priority == RequestPriority.High)
                    item.BackColor = Color.LightYellow;
                else if (request.Status == RequestStatus.Resolved)
                    item.BackColor = Color.LightGreen;
                else if (request.Status == RequestStatus.Closed)
                    item.BackColor = Color.LightGray;

                item.Tag = request.ReferenceNumber;
                listViewRequests.Items.Add(item);
            }

            lblResultsCount.Text = $"{requests.Count} requests found";
        }

        private void UpdateStatistics()
        {
            var stats = _requestService.GetStatistics();

            lblTotalRequests.Text = $"Total: {stats.TotalRequests}";
            lblSubmitted.Text = $"Submitted: {stats.SubmittedCount}";
            lblInProgress.Text = $"In Progress: {stats.InProgressCount}";
            lblResolved.Text = $"Resolved: {stats.ResolvedCount}";
            lblAvgResolution.Text = $"Avg Resolution: {stats.AverageResolutionDays:F1} days";

            // Update charts
            UpdatePriorityChart(stats.RequestsByPriority);
            UpdateCategoryChart(stats.RequestsByCategory);
        }

        private void UpdatePriorityChart(Dictionary<RequestPriority, int> priorityData)
        {
            flowLayoutPriority.Controls.Clear();

            // Get the total requests count from the priority data
            int totalRequests = priorityData.Sum(kvp => kvp.Value);

            foreach (var kvp in priorityData.OrderByDescending(k => k.Key))
            {
                var panel = new Panel
                {
                    Size = new Size(120, 60),
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(2)
                };

                var priorityLabel = new Label
                {
                    Text = kvp.Key.ToString(),
                    Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold),
                    Location = new Point(3, 3),
                    Size = new Size(110, 15),
                    TextAlign = ContentAlignment.MiddleLeft
                };
                panel.Controls.Add(priorityLabel);

                var countLabel = new Label
                {
                    Text = $"{kvp.Value} requests",
                    Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular),
                    Location = new Point(3, 25),
                    Size = new Size(110, 15),
                    TextAlign = ContentAlignment.MiddleLeft
                };
                panel.Controls.Add(countLabel);

                var progressBar = new ProgressBar
                {
                    Location = new Point(3, 43),
                    Size = new Size(110, 12),
                    Minimum = 0,
                    Maximum = totalRequests > 0 ? totalRequests : 1, // Avoid division by zero
                    Value = kvp.Value
                };
                panel.Controls.Add(progressBar);

                flowLayoutPriority.Controls.Add(panel);
            }
        }

        private void UpdateCategoryChart(Dictionary<string, int> categoryData)
        {
            flowLayoutCategory.Controls.Clear();

            foreach (var kvp in categoryData.OrderByDescending(k => k.Value))
            {
                var panel = new Panel
                {
                    Size = new Size(150, 40),
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(2)
                };

                var categoryLabel = new Label
                {
                    Text = kvp.Key,
                    Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold),
                    Location = new Point(3, 3),
                    Size = new Size(140, 15),
                    TextAlign = ContentAlignment.MiddleLeft
                };
                panel.Controls.Add(categoryLabel);

                var countLabel = new Label
                {
                    Text = $"{kvp.Value} requests",
                    Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular),
                    Location = new Point(3, 20),
                    Size = new Size(140, 15),
                    TextAlign = ContentAlignment.MiddleLeft
                };
                panel.Controls.Add(countLabel);

                flowLayoutCategory.Controls.Add(panel);
            }
        }

        private void LoadAdvancedDataStructureExamples()
        {
            try
            {
                // Demonstrate Binary Search Tree
                var bstRequests = _requestService.SearchRequestsBST("SR-1001", "SR-1005");
                lblBSTDemo.Text = $"BST Range Search (SR-1001 to SR-1005): {bstRequests.Count} requests found";

                // Demonstrate Priority Heap
                var priorityRequests = _requestService.GetPriorityRequests();
                lblHeapDemo.Text = $"Priority Heap - Top {priorityRequests.Count} highest priority requests";

                // Demonstrate Graph BFS
                var relatedRequests = _requestService.GetRelatedRequests("SR-1001");
                lblGraphDemo.Text = $"Graph BFS - {relatedRequests.Count} requests related to SR-1001";

                // Demonstrate Graph MST
                var criticalPath = _requestService.GetCriticalPath();
                lblMSTDemo.Text = $"MST Critical Path - {criticalPath.Count} critical requests identified";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading advanced data structure examples: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            FilterRequests();
        }

        private void FilterRequests()
        {
            string searchText = txtSearch.Text.ToLower();
            string statusFilter = cmbStatusFilter.SelectedIndex == 0 ? null : cmbStatusFilter.SelectedItem.ToString();
            string priorityFilter = cmbPriorityFilter.SelectedIndex == 0 ? null : cmbPriorityFilter.SelectedItem.ToString();
            string categoryFilter = cmbCategoryFilter.SelectedIndex == 0 ? null : cmbCategoryFilter.SelectedItem.ToString();

            var filteredRequests = _currentRequests.Where(request =>
                (string.IsNullOrEmpty(searchText) ||
                 request.ReferenceNumber.ToLower().Contains(searchText) ||
                 request.Title.ToLower().Contains(searchText) ||
                 request.Description.ToLower().Contains(searchText) ||
                 request.Location.ToLower().Contains(searchText)) &&
                (statusFilter == null || request.Status.ToString() == statusFilter) &&
                (priorityFilter == null || request.Priority.ToString() == priorityFilter) &&
                (categoryFilter == null || request.Category == categoryFilter)
            ).ToList();

            DisplayRequests(filteredRequests);
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (listViewRequests.SelectedItems.Count > 0)
            {
                string referenceNumber = listViewRequests.SelectedItems[0].Tag.ToString();
                ShowRequestDetails(referenceNumber);
            }
            else
            {
                MessageBox.Show("Please select a request to view details.", "Selection Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ShowRequestDetails(string referenceNumber)
        {
            var request = _requestService.GetRequest(referenceNumber);
            if (request != null)
            {
                var updates = _requestService.GetRequestUpdates(referenceNumber);

                var detailsForm = new RequestDetailsForm(request, updates);
                detailsForm.ShowDialog();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAllRequests();
            UpdateStatistics();
            LoadAdvancedDataStructureExamples();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                FilterRequests();
                e.Handled = true;
            }
        }

        private void cmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterRequests();
        }

        private void cmbPriorityFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterRequests();
        }

        private void cmbCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterRequests();
        }

        private void btnShowPriority_Click(object sender, EventArgs e)
        {
            var priorityRequests = _requestService.GetPriorityRequests();
            DisplayRequests(priorityRequests);
            MessageBox.Show($"Displaying {priorityRequests.Count} highest priority requests",
                "Priority View", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnShowRelated_Click(object sender, EventArgs e)
        {
            if (listViewRequests.SelectedItems.Count > 0)
            {
                string referenceNumber = listViewRequests.SelectedItems[0].Tag.ToString();
                var relatedRequests = _requestService.GetRelatedRequests(referenceNumber);
                DisplayRequests(relatedRequests);
                MessageBox.Show($"Displaying {relatedRequests.Count} related requests",
                    "Related Requests", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select a request to find related requests.",
                    "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}