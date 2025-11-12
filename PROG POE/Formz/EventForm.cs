using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_POE.Formz
{

    using PROG_POE.Model;
    using PROG_POE.Service;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;


        public partial class EventsForm : Form
        {
            private readonly IEventService _eventService;
            private List<Event> _currentEvents;
            private List<Event> _recommendedEvents;
            private string _currentUserId = "user_" + Guid.NewGuid().ToString().Substring(0, 8);

            public EventsForm(IEventService eventService)
            {
                InitializeComponent();
                _eventService = eventService;
            }

            private void EventsForm_Load(object sender, EventArgs e)
            {
                LoadAllEvents();
                LoadCategories();
                LoadDates();
                UpdateRecommendations();
            }

            private void LoadAllEvents()
            {
                _currentEvents = _eventService.GetAllEvents();
                DisplayEvents(_currentEvents);
            }

            private void LoadCategories()
            {
                cmbCategory.Items.Clear();
                cmbCategory.Items.Add("All Categories");
                var categories = _eventService.GetUniqueCategories();
                foreach (var category in categories)
                {
                    cmbCategory.Items.Add(category);
                }
                cmbCategory.SelectedIndex = 0;
            }

            private void LoadDates()
            {
                cmbDate.Items.Clear();
                cmbDate.Items.Add("All Dates");
                var dates = _eventService.GetUniqueDates();
                foreach (var date in dates)
                {
                    cmbDate.Items.Add(date.ToShortDateString());
                }
                cmbDate.SelectedIndex = 0;
            }

            private void DisplayEvents(List<Event> events)
            {
                flowLayoutEvents.Controls.Clear();

                if (!events.Any())
                {
                    var noResultsLabel = new Label
                    {
                        Text = "No events found matching your criteria.",
                        Font = new Font("Microsoft Sans Serif", 10, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        AutoSize = true,
                        Margin = new Padding(10)
                    };
                    flowLayoutEvents.Controls.Add(noResultsLabel);
                    return;
                }

                foreach (var eventItem in events)
                {
                    var eventPanel = CreateEventPanel(eventItem);
                    flowLayoutEvents.Controls.Add(eventPanel);
                }
            }

            private Panel CreateEventPanel(Event eventItem)
            {
                var panel = new Panel
                {
                    Size = new Size(350, 180),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = eventItem.IsFeatured ? Color.LightGoldenrodYellow : Color.White,
                    Margin = new Padding(5),
                    Padding = new Padding(5),
                    Cursor = Cursors.Hand
                };

                // Add hover effects
                panel.MouseEnter += (s, e) =>
                {
                    panel.BackColor = Color.LightBlue;
                    panel.BorderStyle = BorderStyle.Fixed3D;
                };

                panel.MouseLeave += (s, e) =>
                {
                    panel.BackColor = eventItem.IsFeatured ? Color.LightGoldenrodYellow : Color.White;
                    panel.BorderStyle = BorderStyle.FixedSingle;
                };

                // Title
                var titleLabel = new Label
                {
                    Text = eventItem.Title,
                    Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold),
                    Location = new Point(5, 5),
                    Size = new Size(330, 20),
                    AutoEllipsis = true
                };
                panel.Controls.Add(titleLabel);

                // Date and Location
                var detailsLabel = new Label
                {
                    Text = $"{eventItem.EventDate:dd MMM yyyy} | {eventItem.Location}",
                    Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular),
                    Location = new Point(5, 30),
                    Size = new Size(330, 15),
                    ForeColor = Color.DarkBlue
                };
                panel.Controls.Add(detailsLabel);

                // Category
                var categoryLabel = new Label
                {
                    Text = $"Category: {eventItem.Category}",
                    Font = new Font("Microsoft Sans Serif", 9, FontStyle.Italic),
                    Location = new Point(5, 50),
                    Size = new Size(330, 15),
                    ForeColor = Color.DarkGreen
                };
                panel.Controls.Add(categoryLabel);

                // Description
                var descLabel = new Label
                {
                    Text = eventItem.Description,
                    Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular),
                    Location = new Point(5, 70),
                    Size = new Size(330, 60),
                    ForeColor = Color.DimGray
                };
                panel.Controls.Add(descLabel);

                // Organizer and Attendees
                var infoLabel = new Label
                {
                    Text = $"By: {eventItem.Organizer} | Attendees: {eventItem.AttendeeCount}",
                    Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular),
                    Location = new Point(5, 135),
                    Size = new Size(330, 15),
                    ForeColor = Color.Gray
                };
                panel.Controls.Add(infoLabel);

                // Featured badge
                if (eventItem.IsFeatured)
                {
                    var featuredLabel = new Label
                    {
                        Text = "FEATURED",
                        Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold),
                        Location = new Point(280, 5),
                        Size = new Size(60, 15),
                        ForeColor = Color.Red,
                        TextAlign = ContentAlignment.MiddleRight
                    };
                    panel.Controls.Add(featuredLabel);
                }

                // Click event for interaction tracking
                panel.Click += (s, e) =>
                {
                    _eventService.RecordUserInteraction(_currentUserId, eventItem.Id, InteractionType.Click);
                    ShowEventDetails(eventItem);
                };

                return panel;
            }

            private void ShowEventDetails(Event eventItem)
            {
                try
                {
                    MessageBox.Show(
                        $"Event Details:\n\n" +
                        $"Title: {eventItem.Title}\n" +
                        $"Date: {eventItem.EventDate:dd MMM yyyy}\n" +
                        $"Location: {eventItem.Location}\n" +
                        $"Category: {eventItem.Category}\n" +
                        $"Description: {eventItem.Description}\n" +
                        $"Organizer: {eventItem.Organizer}\n" +
                        $"Attendees: {eventItem.AttendeeCount}",
                        "Event Details",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error showing event details: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private void btnSearch_Click(object sender, EventArgs e)
            {
                PerformSearch();
            }

            private void PerformSearch()
            {
                try
                {
                    string searchTerm = txtSearch.Text.Trim();
                    string category = cmbCategory.SelectedIndex == 0 ? null : cmbCategory.SelectedItem.ToString();
                    DateTime? selectedDate = null;

                    if (cmbDate.SelectedIndex > 0 && DateTime.TryParse(cmbDate.SelectedItem.ToString(), out DateTime date))
                    {
                        selectedDate = date;
                    }

                    _currentEvents = _eventService.SearchEvents(searchTerm, category, selectedDate);
                    DisplayEvents(_currentEvents);
                    UpdateRecommendations();

                    // Record search for recommendations
                    _eventService.RecordUserSearch(searchTerm, category);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Search failed: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private void UpdateRecommendations()
            {
                try
                {
                    string currentSearch = txtSearch.Text.Trim();
                    string currentCategory = cmbCategory.SelectedIndex == 0 ? null : cmbCategory.SelectedItem?.ToString();

                    _recommendedEvents = _eventService.GetRecommendedEvents(currentSearch, currentCategory);
                    DisplayRecommendations();
                }
                catch (Exception ex)
                {
                    // Don't show error to user for recommendations - use fallback
                    _recommendedEvents = _eventService.GetAllEvents()
                        .Where(e => e.IsFeatured)
                        .Take(3)
                        .ToList();
                    DisplayRecommendations();
                }
            }

            private void DisplayRecommendations()
            {
                flowLayoutRecommendations.Controls.Clear();

                if (!_recommendedEvents.Any())
                {
                    var noRecsLabel = new Label
                    {
                        Text = "No recommendations available. Try searching for events to get personalized suggestions!",
                        Font = new Font("Microsoft Sans Serif", 9, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        AutoSize = false,
                        Size = new Size(700, 40),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Margin = new Padding(5)
                    };
                    flowLayoutRecommendations.Controls.Add(noRecsLabel);
                    return;
                }

                var headerLabel = new Label
                {
                    Text = "Recommended for you:",
                    Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold),
                    AutoSize = true,
                    Margin = new Padding(5),
                    ForeColor = Color.DarkBlue
                };
                flowLayoutRecommendations.Controls.Add(headerLabel);

                foreach (var eventItem in _recommendedEvents.Take(3))
                {
                    var recPanel = CreateRecommendationPanel(eventItem);
                    flowLayoutRecommendations.Controls.Add(recPanel);
                }
            }

            private Panel CreateRecommendationPanel(Event eventItem)
            {
                var panel = new Panel
                {
                    Size = new Size(250, 80),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.LightCyan,
                    Margin = new Padding(3),
                    Padding = new Padding(3),
                    Cursor = Cursors.Hand
                };

                // Add hover effect
                panel.MouseEnter += (s, e) => panel.BackColor = Color.LightSkyBlue;
                panel.MouseLeave += (s, e) => panel.BackColor = Color.LightCyan;

                // Click event
                panel.Click += (s, e) =>
                {
                    _eventService.RecordUserInteraction(_currentUserId, eventItem.Id, InteractionType.Click);
                    ShowEventDetails(eventItem);
                };

                var titleLabel = new Label
                {
                    Text = eventItem.Title,
                    Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold),
                    Location = new Point(3, 3),
                    Size = new Size(240, 15),
                    AutoEllipsis = true
                };
                panel.Controls.Add(titleLabel);

                var dateLabel = new Label
                {
                    Text = eventItem.EventDate.ToString("dd MMM yyyy"),
                    Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular),
                    Location = new Point(3, 25),
                    Size = new Size(240, 12),
                    ForeColor = Color.DarkBlue
                };
                panel.Controls.Add(dateLabel);

                var categoryLabel = new Label
                {
                    Text = eventItem.Category,
                    Font = new Font("Microsoft Sans Serif", 8, FontStyle.Italic),
                    Location = new Point(3, 40),
                    Size = new Size(240, 12),
                    ForeColor = Color.DarkGreen
                };
                panel.Controls.Add(categoryLabel);

                var locationLabel = new Label
                {
                    Text = eventItem.Location,
                    Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular),
                    Location = new Point(3, 55),
                    Size = new Size(240, 12),
                    ForeColor = Color.DimGray
                };
                panel.Controls.Add(locationLabel);

                return panel;
            }

            private void btnBack_Click(object sender, EventArgs e)
            {
                MainForm mainForm = new MainForm();
                mainForm.Show();
                this.Close();
            }

            private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    PerformSearch();
                    e.Handled = true;
                }
            }

            private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
            {
                PerformSearch();
            }

            private void cmbDate_SelectedIndexChanged(object sender, EventArgs e)
            {
                PerformSearch();
            }

            private void btnClear_Click(object sender, EventArgs e)
            {
                txtSearch.Clear();
                cmbCategory.SelectedIndex = 0;
                cmbDate.SelectedIndex = 0;
                LoadAllEvents();
                UpdateRecommendations();
            }
        }
    }

