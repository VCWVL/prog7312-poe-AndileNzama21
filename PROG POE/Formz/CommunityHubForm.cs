using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_POE.Formz
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using PROG_POE.Service;
    using PROG_POE.Model;

    public partial class CommunityHubForm : Form
        {
            private readonly IDataService _dataService;
            private List<CommunityIssue> _communityIssues;
            private Stack<CommunityIssue> _recentlyViewed;
            private Queue<CommunityIssue> _popularIssuesQueue;

            public CommunityHubForm(IDataService dataService)
            {
                InitializeComponent();
                _dataService = dataService;
                _recentlyViewed = new Stack<CommunityIssue>();
                _popularIssuesQueue = new Queue<CommunityIssue>();
            }

            private void CommunityHubForm_Load(object sender, EventArgs e)
            {
                LoadCommunityIssues();
                InitializeCategoriesFilter();
                UpdateStatistics();
            }

            private void LoadCommunityIssues()
            {
                _communityIssues = _dataService.GetCommunityIssues();
                DisplayCommunityIssues(_communityIssues);
                UpdatePopularIssuesQueue();
            }

            private void InitializeCategoriesFilter()
            {
                cmbCategoryFilter.Items.Clear();
                cmbCategoryFilter.Items.Add("All Categories");

                // Use HashSet to get unique categories
                var categories = new HashSet<string>();
                foreach (var issue in _communityIssues)
                {
                    categories.Add(issue.Category);
                }

                foreach (var category in categories.OrderBy(c => c))
                {
                    cmbCategoryFilter.Items.Add(category);
                }
                cmbCategoryFilter.SelectedIndex = 0;
            }

            private void DisplayCommunityIssues(List<CommunityIssue> issues)
            {
                listViewIssues.Items.Clear();

                foreach (var issue in issues.OrderByDescending(i => i.SupportCount))
                {
                    var item = new ListViewItem(issue.ReferenceNumber);
                    item.SubItems.Add(issue.Location);
                    item.SubItems.Add(issue.Category);
                    item.SubItems.Add(issue.Description);
                    item.SubItems.Add(issue.ReportDate.ToShortDateString());
                    item.SubItems.Add(issue.SupportCount.ToString());
                    item.SubItems.Add(issue.Status);

                    // Color coding based on status
                    if (issue.Status == "In Progress")
                        item.BackColor = Color.LightYellow;
                    else if (issue.Status == "Resolved")
                        item.BackColor = Color.LightGreen;
                    else if (issue.Status == "Urgent")
                        item.BackColor = Color.LightCoral;

                    item.Tag = issue.ReferenceNumber;
                    listViewIssues.Items.Add(item);
                }

                UpdateRecentIssuesStack();
            }

            private void UpdateRecentIssuesStack()
            {
                flowLayoutRecent.Controls.Clear();

                var recentItems = _recentlyViewed.Take(3).ToList();
                if (!recentItems.Any())
                {
                    var label = new Label
                    {
                        Text = "No recently viewed issues",
                        Font = new Font("Microsoft Sans Serif", 9, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        AutoSize = true,
                        Margin = new Padding(5)
                    };
                    flowLayoutRecent.Controls.Add(label);
                    return;
                }

                foreach (var issue in recentItems)
                {
                    var panel = CreateRecentIssuePanel(issue);
                    flowLayoutRecent.Controls.Add(panel);
                }
            }

            private void UpdatePopularIssuesQueue()
            {
                _popularIssuesQueue.Clear();
                var popularIssues = _communityIssues
                    .OrderByDescending(i => i.SupportCount)
                    .Take(5);

                foreach (var issue in popularIssues)
                {
                    _popularIssuesQueue.Enqueue(issue);
                }

                DisplayPopularIssues();
            }

            private void DisplayPopularIssues()
            {
                flowLayoutPopular.Controls.Clear();

                if (!_popularIssuesQueue.Any())
                {
                    var label = new Label
                    {
                        Text = "No popular issues yet",
                        Font = new Font("Microsoft Sans Serif", 9, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        AutoSize = true,
                        Margin = new Padding(5)
                    };
                    flowLayoutPopular.Controls.Add(label);
                    return;
                }

                foreach (var issue in _popularIssuesQueue.Take(3))
                {
                    var panel = CreatePopularIssuePanel(issue);
                    flowLayoutPopular.Controls.Add(panel);
                }
            }

            private Panel CreateRecentIssuePanel(CommunityIssue issue)
            {
                var panel = new Panel
                {
                    Size = new Size(200, 80),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.LightBlue,
                    Margin = new Padding(3),
                    Padding = new Padding(3)
                };

                var titleLabel = new Label
                {
                    Text = $"Ref: {issue.ReferenceNumber}",
                    Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold),
                    Location = new Point(3, 3),
                    Size = new Size(190, 15),
                    AutoEllipsis = true
                };
                panel.Controls.Add(titleLabel);

                var locationLabel = new Label
                {
                    Text = issue.Location,
                    Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular),
                    Location = new Point(3, 20),
                    Size = new Size(190, 12),
                    ForeColor = Color.DarkBlue
                };
                panel.Controls.Add(locationLabel);

                var categoryLabel = new Label
                {
                    Text = issue.Category,
                    Font = new Font("Microsoft Sans Serif", 8, FontStyle.Italic),
                    Location = new Point(3, 35),
                    Size = new Size(190, 12),
                    ForeColor = Color.DarkGreen
                };
                panel.Controls.Add(categoryLabel);

                var supportLabel = new Label
                {
                    Text = $"{issue.SupportCount} supports",
                    Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular),
                    Location = new Point(3, 50),
                    Size = new Size(190, 12),
                    ForeColor = Color.DarkRed
                };
                panel.Controls.Add(supportLabel);

                // Add click event to view details
                panel.Click += (s, e) => ShowIssueDetails(issue);
                foreach (Control control in panel.Controls)
                {
                    control.Click += (s, e) => ShowIssueDetails(issue);
                }

                return panel;
            }

            private Panel CreatePopularIssuePanel(CommunityIssue issue)
            {
                var panel = new Panel
                {
                    Size = new Size(200, 80),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.LightGoldenrodYellow,
                    Margin = new Padding(3),
                    Padding = new Padding(3)
                };

                var titleLabel = new Label
                {
                    Text = $"🔥 {issue.ReferenceNumber}",
                    Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold),
                    Location = new Point(3, 3),
                    Size = new Size(190, 15),
                    AutoEllipsis = true
                };
                panel.Controls.Add(titleLabel);

                var locationLabel = new Label
                {
                    Text = issue.Location,
                    Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular),
                    Location = new Point(3, 20),
                    Size = new Size(190, 12),
                    ForeColor = Color.DarkBlue
                };
                panel.Controls.Add(locationLabel);

                var descLabel = new Label
                {
                    Text = GetShortDescription(issue.Description, 25),
                    Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular),
                    Location = new Point(3, 35),
                    Size = new Size(190, 30),
                    ForeColor = Color.DimGray
                };
                panel.Controls.Add(descLabel);

                var supportLabel = new Label
                {
                    Text = $"{issue.SupportCount} people support this",
                    Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold),
                    Location = new Point(3, 60),
                    Size = new Size(190, 12),
                    ForeColor = Color.DarkRed
                };
                panel.Controls.Add(supportLabel);

                // Add click event
                panel.Click += (s, e) => ShowIssueDetails(issue);
                foreach (Control control in panel.Controls)
                {
                    control.Click += (s, e) => ShowIssueDetails(issue);
                }

                return panel;
            }

            private string GetShortDescription(string description, int maxLength)
            {
                if (string.IsNullOrEmpty(description) || description.Length <= maxLength)
                    return description;

                return description.Substring(0, maxLength) + "...";
            }

            private void ShowIssueDetails(CommunityIssue issue)
            {
                // Add to recently viewed stack
                _recentlyViewed.Push(issue);
                UpdateRecentIssuesStack();

                MessageBox.Show(
                    $"Issue Details:\n\n" +
                    $"Reference: {issue.ReferenceNumber}\n" +
                    $"Location: {issue.Location}\n" +
                    $"Category: {issue.Category}\n" +
                    $"Description: {issue.Description}\n" +
                    $"Reported: {issue.ReportDate.ToShortDateString()}\n" +
                    $"Supports: {issue.SupportCount}\n" +
                    $"Status: {issue.Status}",
                    "Issue Details",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }

            private void btnSupport_Click(object sender, EventArgs e)
            {
                if (listViewIssues.SelectedItems.Count > 0)
                {
                    string referenceNumber = listViewIssues.SelectedItems[0].Tag.ToString();
                    _dataService.SupportIssue(referenceNumber);

                    // Update the display
                    LoadCommunityIssues();
                    UpdateStatistics();

                    MessageBox.Show("Thank you for supporting this issue! Your support helps prioritize it.",
                        "Issue Supported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Please select an issue to support.",
                        "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            private void UpdateStatistics()
            {
                var totalIssues = _communityIssues.Count;
                var totalSupports = _communityIssues.Sum(i => i.SupportCount);
                var avgSupports = totalIssues > 0 ? totalSupports / totalIssues : 0;

                var statusGroups = _communityIssues
                    .GroupBy(i => i.Status)
                    .ToDictionary(g => g.Key, g => g.Count());

                lblTotalIssues.Text = $"Total Issues: {totalIssues}";
                lblTotalSupports.Text = $"Total Supports: {totalSupports}";
                lblAvgSupports.Text = $"Avg Supports: {avgSupports}";

                // Update status breakdown
                flowLayoutStatus.Controls.Clear();
                foreach (var statusGroup in statusGroups)
                {
                    var statusPanel = new Panel
                    {
                        Size = new Size(120, 40),
                        BorderStyle = BorderStyle.FixedSingle,
                        Margin = new Padding(2)
                    };

                    var statusLabel = new Label
                    {
                        Text = statusGroup.Key,
                        Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold),
                        Location = new Point(3, 3),
                        Size = new Size(110, 15),
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                    statusPanel.Controls.Add(statusLabel);

                    var countLabel = new Label
                    {
                        Text = $"{statusGroup.Value} issues",
                        Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular),
                        Location = new Point(3, 20),
                        Size = new Size(110, 15),
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                    statusPanel.Controls.Add(countLabel);

                    flowLayoutStatus.Controls.Add(statusPanel);
                }
            }

            private void btnBack_Click(object sender, EventArgs e)
            {
                MainForm mainForm = new MainForm();
                mainForm.Show();
                this.Close();
            }

            private void btnRefresh_Click(object sender, EventArgs e)
            {
                LoadCommunityIssues();
                UpdateStatistics();
            }

            private void txtSearch_TextChanged(object sender, EventArgs e)
            {
                FilterIssues();
            }

            private void cmbCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
            {
                FilterIssues();
            }

            private void FilterIssues()
            {
                string searchText = txtSearch.Text.ToLower();
                string selectedCategory = cmbCategoryFilter.SelectedIndex == 0 ?
                    null : cmbCategoryFilter.SelectedItem.ToString();

                var filteredIssues = _communityIssues.Where(issue =>
                    (string.IsNullOrEmpty(searchText) ||
                     issue.Description.ToLower().Contains(searchText) ||
                     issue.Location.ToLower().Contains(searchText) ||
                     issue.ReferenceNumber.ToLower().Contains(searchText)) &&
                    (selectedCategory == null || issue.Category == selectedCategory)
                ).ToList();

                DisplayCommunityIssues(filteredIssues);
            }

            private void btnSortBySupports_Click(object sender, EventArgs e)
            {
                var sortedIssues = _communityIssues.OrderByDescending(i => i.SupportCount).ToList();
                DisplayCommunityIssues(sortedIssues);
            }

            private void btnSortByDate_Click(object sender, EventArgs e)
            {
                var sortedIssues = _communityIssues.OrderByDescending(i => i.ReportDate).ToList();
                DisplayCommunityIssues(sortedIssues);
            }

            private void listViewIssues_DoubleClick(object sender, EventArgs e)
            {
                if (listViewIssues.SelectedItems.Count > 0)
                {
                    string referenceNumber = listViewIssues.SelectedItems[0].Tag.ToString();
                    var issue = _communityIssues.FirstOrDefault(i => i.ReferenceNumber == referenceNumber);
                    if (issue != null)
                    {
                        ShowIssueDetails(issue);
                    }
                }
            }
        }

    }
