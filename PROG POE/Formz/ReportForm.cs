
namespace PROG_POE.Formz
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO;
    using System.Windows.Forms;
    using static System.Windows.Forms.VisualStyles.VisualStyleElement;


        using global::PROG_POE.Model;
        using global::PROG_POE.Service;

        
            public partial class ReportForm : Form  // Make sure it inherits from Form
            {
                private readonly IDataService _dataService;
                private string _attachedFilePath = string.Empty;
                private int _progressValue = 0;

                // Default constructor (required for Windows Forms designer)
                public ReportForm()
                {
                    InitializeComponent();
                    _dataService = new DataService(); // Create a default service instance
                }

                // Constructor that accepts IDataService
                public ReportForm(IDataService dataService) : this()
                {
                    _dataService = dataService;
                }

                private void ReportForm_Load(object sender, EventArgs e)
                {
                    cmbCategory.Items.AddRange(new string[] {
                "Sanitation",
                "Roads",
                "Utilities",
                "Public Safety",
                "Parks and Recreation",
                "Other"
            });
                    cmbCategory.SelectedIndex = 0;

                    progressBar.Value = 0;
                    lblProgress.Text = "0% Complete";
                }

                private void btnAttach_Click(object sender, EventArgs e)
                {
                    using (OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Documents|*.pdf;*.doc;*.docx|All files|*.*";
                        openFileDialog.Title = "Select a file to attach";

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            _attachedFilePath = openFileDialog.FileName;
                            lblAttachedFile.Text = Path.GetFileName(_attachedFilePath);
                            UpdateProgress(25);
                        }
                    }
                }

                private void btnSubmit_Click(object sender, EventArgs e)
                {
                    if (string.IsNullOrWhiteSpace(txtLocation.Text))
                    {
                        MessageBox.Show("Please enter a location for the issue.", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(rtxtDescription.Text))
                    {
                        MessageBox.Show("Please provide a description of the issue.", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    ReportedIssue newIssue = new ReportedIssue
                    {
                        Location = txtLocation.Text,
                        Category = cmbCategory.SelectedItem.ToString(),
                        Description = rtxtDescription.Text,
                        AttachmentPath = _attachedFilePath
                    };

                    _dataService.AddIssue(newIssue);

                    MessageBox.Show($"Thank you for reporting the issue. Your reference number is: {newIssue.ReferenceNumber}",
                        "Report Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ResetForm();
                }

                private void btnBack_Click(object sender, EventArgs e)
                {
                    MainForm mainForm = new MainForm();
                    mainForm.Show();
                    this.Close();
                }

                private void ResetForm()
                {
                    txtLocation.Clear();
                    cmbCategory.SelectedIndex = 0;
                    rtxtDescription.Clear();
                    _attachedFilePath = string.Empty;
                    lblAttachedFile.Text = "No file attached";
                    progressBar.Value = 0;
                    lblProgress.Text = "0% Complete";
                    _progressValue = 0;
                }

                private void UpdateProgress(int increment)
                {
                    _progressValue = Math.Min(_progressValue + increment, 100);
                    progressBar.Value = _progressValue;
                    lblProgress.Text = $"{_progressValue}% Complete";

                    if (_progressValue < 25)
                    {
                        lblEncouragement.Text = "Let's get started!";
                    }
                    else if (_progressValue < 50)
                    {
                        lblEncouragement.Text = "Great progress!";
                    }
                    else if (_progressValue < 75)
                    {
                        lblEncouragement.Text = "Almost there!";
                    }
                    else
                    {
                        lblEncouragement.Text = "Ready to submit!";
                    }
                }

                private void txtLocation_TextChanged(object sender, EventArgs e)
                {
                    if (!string.IsNullOrWhiteSpace(txtLocation.Text) && _progressValue < 25)
                    {
                        UpdateProgress(25);
                    }
                }

                private void rtxtDescription_TextChanged(object sender, EventArgs e)
                {
                    if (!string.IsNullOrWhiteSpace(rtxtDescription.Text) && _progressValue < 50)
                    {
                        UpdateProgress(25);
                    }
                }

                private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
                {
                    if (_progressValue < 25)
                    {
                        UpdateProgress(25);
                    }
                }
            }
        }

    
