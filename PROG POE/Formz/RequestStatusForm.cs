using PROG_POE.Formz;
using PROG_POE.Model;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PROG_POE.Forms
{
    public partial class RequestStatusForm : Form
    {
        private readonly IRequestService _requestService;
        private List<ServiceRequest> _currentRequests;
        private bool _isInitializing = true;

        public RequestStatusForm(IRequestService requestService)
        {
            InitializeComponent();
            _requestService = requestService;
            _currentRequests = new List<ServiceRequest>(); // Initialize to empty list
        }

        private void RequestStatusForm_Load(object sender, EventArgs e)
        {
            _isInitializing = true;
            LoadAllRequests();
            InitializeFilters();
            UpdateStatistics();
            LoadAdvancedDataStructureExamples();
            _isInitializing = false;
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

            cmbPriorityFilter.Items.Clear();
            cmbPriorityFilter.Items.Add("All Priorities");
            foreach (RequestPriority priority in Enum.GetValues(typeof(RequestPriority)))
            {
                cmbPriorityFilter.Items.Add(priority.ToString());
            }

            cmbCategoryFilter.Items.Clear();
            cmbCategoryFilter.Items.Add("All Categories");

            // Safely get categories
            var categories = _currentRequests?.Select(r => r.Category).Distinct().OrderBy(c => c).ToList() ?? new List<string>();
            foreach (var category in categories)
            {
                cmbCategoryFilter.Items.Add(category);
            }

            // Set initial selections
            cmbStatusFilter.SelectedIndex = 0;
            cmbPriorityFilter.SelectedIndex = 0;
            cmbCategoryFilter.SelectedIndex = 0;
        }

        private void DisplayRequests(List<ServiceRequest> requests)
        {
            if (listViewRequests == null) return;

            listViewRequests.Items.Clear();

            if (requests == null || !requests.Any())
            {
                var noResultsItem = new ListViewItem("No requests found");
                noResultsItem.SubItems.Add("");
                noResultsItem.SubItems.Add("");
                noResultsItem.SubItems.Add("");
                noResultsItem.SubItems.Add("");
                noResultsItem.SubItems.Add("");
                noResultsItem.SubItems.Add("");
                noResultsItem.SubItems.Add("");
                noResultsItem.SubItems.Add("");
                listViewRequests.Items.Add(noResultsItem);

                if (lblResultsCount != null)
                    lblResultsCount.Text = "0 requests found";
                return;
            }

            foreach (var request in requests.OrderByDescending(r => r.CreatedDate))
            {
                var item = new ListViewItem(request.ReferenceNumber ?? "N/A");
                item.SubItems.Add(request.Title ?? "N/A");
                item.SubItems.Add(request.Category ?? "N/A");
                item.SubItems.Add(request.Location ?? "N/A");
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

            if (lblResultsCount != null)
                lblResultsCount.Text = $"{requests.Count} requests found";
        }

        private void UpdateStatistics()
        {
            var stats = _requestService.GetStatistics();

            if (lblTotalRequests != null) lblTotalRequests.Text = $"Total: {stats.TotalRequests}";
            if (lblSubmitted != null) lblSubmitted.Text = $"Submitted: {stats.SubmittedCount}";
            if (lblInProgress != null) lblInProgress.Text = $"In Progress: {stats.InProgressCount}";
            if (lblResolved != null) lblResolved.Text = $"Resolved: {stats.ResolvedCount}";
            if (lblAvgResolution != null) lblAvgResolution.Text = $"Avg Resolution: {stats.AverageResolutionDays:F1} days";

            UpdatePriorityChart(stats.RequestsByPriority);
            UpdateCategoryChart(stats.RequestsByCategory);
        }

        private void UpdatePriorityChart(Dictionary<RequestPriority, int> priorityData)
        {
            if (flowLayoutPriority == null) return;

            flowLayoutPriority.Controls.Clear();

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
                    Maximum = totalRequests > 0 ? totalRequests : 1,
                    Value = kvp.Value
                };
                panel.Controls.Add(progressBar);

                flowLayoutPriority.Controls.Add(panel);
            }
        }

        private void UpdateCategoryChart(Dictionary<string, int> categoryData)
        {
            if (flowLayoutCategory == null) return;

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
                var bstRequests = _requestService.SearchRequestsBST("SR-1001", "SR-1005");
                if (lblBSTDemo != null)
                    lblBSTDemo.Text = $"BST Range Search (SR-1001 to SR-1005): {bstRequests.Count} requests found";

                var priorityRequests = _requestService.GetPriorityRequests();
                if (lblHeapDemo != null)
                    lblHeapDemo.Text = $"Priority Heap - Top {priorityRequests.Count} highest priority requests";

                var relatedRequests = _requestService.GetRelatedRequests("SR-1001");
                if (lblGraphDemo != null)
                    lblGraphDemo.Text = $"Graph BFS - {relatedRequests.Count} requests related to SR-1001";

                var criticalPath = _requestService.GetCriticalPath();
                if (lblMSTDemo != null)
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
            // Ensure _currentRequests is not null
            if (_currentRequests == null)
            {
                _currentRequests = new List<ServiceRequest>();
            }

            // Don't filter during initialization
            if (_isInitializing) return;

            string searchText = txtSearch?.Text?.ToLower() ?? "";
            string statusFilter = cmbStatusFilter?.SelectedIndex == 0 ? null : cmbStatusFilter?.SelectedItem?.ToString();
            string priorityFilter = cmbPriorityFilter?.SelectedIndex == 0 ? null : cmbPriorityFilter?.SelectedItem?.ToString();
            string categoryFilter = cmbCategoryFilter?.SelectedIndex == 0 ? null : cmbCategoryFilter?.SelectedItem?.ToString();

            var filteredRequests = _currentRequests.Where(request =>
                (string.IsNullOrEmpty(searchText) ||
                 (request.ReferenceNumber?.ToLower().Contains(searchText) ?? false) ||
                 (request.Title?.ToLower().Contains(searchText) ?? false) ||
                 (request.Description?.ToLower().Contains(searchText) ?? false) ||
                 (request.Location?.ToLower().Contains(searchText) ?? false)) &&
                (statusFilter == null || request.Status.ToString() == statusFilter) &&
                (priorityFilter == null || request.Priority.ToString() == priorityFilter) &&
                (categoryFilter == null || request.Category == categoryFilter)
            ).ToList();

            DisplayRequests(filteredRequests);
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (listViewRequests?.SelectedItems?.Count > 0 && listViewRequests.SelectedItems[0].Tag != null)
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
            if (listViewRequests?.SelectedItems?.Count > 0 && listViewRequests.SelectedItems[0].Tag != null)
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