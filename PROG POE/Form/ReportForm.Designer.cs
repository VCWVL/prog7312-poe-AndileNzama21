using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG_POE.Form
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    
            partial class ReportForm
            {
                /// <summary>
                /// Required designer variable.
                /// </summary>
                private System.ComponentModel.IContainer components = null;

                /// <summary>
                /// Clean up any resources being used.
                /// </summary>
                /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
                protected override void Dispose(bool disposing)
                {
                    if (disposing && (components != null))
                    {
                        components.Dispose();
                    }
                    base.Dispose(disposing);
                }

                #region Windows Form Designer generated code

                /// <summary>
                /// Required method for Designer support - do not modify
                /// the contents of this method with the code editor.
                /// </summary>
                private void InitializeComponent()
                {
                    this.label1 = new System.Windows.Forms.Label();
                    this.label2 = new System.Windows.Forms.Label();
                    this.txtLocation = new System.Windows.Forms.TextBox();
                    this.label3 = new System.Windows.Forms.Label();
                    this.cmbCategory = new System.Windows.Forms.ComboBox();
                    this.label4 = new System.Windows.Forms.Label();
                    this.rtxtDescription = new System.Windows.Forms.RichTextBox();
                    this.label5 = new System.Windows.Forms.Label();
                    this.btnAttach = new System.Windows.Forms.Button();
                    this.lblAttachedFile = new System.Windows.Forms.Label();
                    this.btnSubmit = new System.Windows.Forms.Button();
                    this.btnBack = new System.Windows.Forms.Button();
                    this.progressBar = new System.Windows.Forms.ProgressBar();
                    this.lblProgress = new System.Windows.Forms.Label();
                    this.lblEncouragement = new System.Windows.Forms.Label();
                    this.SuspendLayout();
                    // 
                    // label1
                    // 
                    this.label1.AutoSize = true;
                    this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
                    this.label1.Location = new System.Drawing.Point(150, 20);
                    this.label1.Name = "label1";
                    this.label1.Size = new System.Drawing.Size(200, 24);
                    this.label1.TabIndex = 0;
                    this.label1.Text = "Report an Issue Form";
                    // 
                    // label2
                    // 
                    this.label2.AutoSize = true;
                    this.label2.Location = new System.Drawing.Point(50, 70);
                    this.label2.Name = "label2";
                    this.label2.Size = new System.Drawing.Size(51, 13);
                    this.label2.TabIndex = 1;
                    this.label2.Text = "Location:";
                    // 
                    // txtLocation
                    // 
                    this.txtLocation.Location = new System.Drawing.Point(120, 67);
                    this.txtLocation.Name = "txtLocation";
                    this.txtLocation.Size = new System.Drawing.Size(300, 20);
                    this.txtLocation.TabIndex = 2;
                    this.txtLocation.TextChanged += new System.EventHandler(this.txtLocation_TextChanged);
                    // 
                    // label3
                    // 
                    this.label3.AutoSize = true;
                    this.label3.Location = new System.Drawing.Point(50, 100);
                    this.label3.Name = "label3";
                    this.label3.Size = new System.Drawing.Size(52, 13);
                    this.label3.TabIndex = 3;
                    this.label3.Text = "Category:";
                    // 
                    // cmbCategory
                    // 
                    this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                    this.cmbCategory.FormattingEnabled = true;
                    this.cmbCategory.Location = new System.Drawing.Point(120, 97);
                    this.cmbCategory.Name = "cmbCategory";
                    this.cmbCategory.Size = new System.Drawing.Size(300, 21);
                    this.cmbCategory.TabIndex = 4;
                    this.cmbCategory.SelectedIndexChanged += new System.EventHandler(this.cmbCategory_SelectedIndexChanged);
                    // 
                    // label4
                    // 
                    this.label4.AutoSize = true;
                    this.label4.Location = new System.Drawing.Point(50, 130);
                    this.label4.Name = "label4";
                    this.label4.Size = new System.Drawing.Size(63, 13);
                    this.label4.TabIndex = 5;
                    this.label4.Text = "Description:";
                    // 
                    // rtxtDescription
                    // 
                    this.rtxtDescription.Location = new System.Drawing.Point(120, 130);
                    this.rtxtDescription.Name = "rtxtDescription";
                    this.rtxtDescription.Size = new System.Drawing.Size(300, 100);
                    this.rtxtDescription.TabIndex = 6;
                    this.rtxtDescription.Text = "";
                    this.rtxtDescription.TextChanged += new System.EventHandler(this.rtxtDescription_TextChanged);
                    // 
                    // label5
                    // 
                    this.label5.AutoSize = true;
                    this.label5.Location = new System.Drawing.Point(50, 250);
                    this.label5.Name = "label5";
                    this.label5.Size = new System.Drawing.Size(61, 13);
                    this.label5.TabIndex = 7;
                    this.label5.Text = "Attachment:";
                    // 
                    // btnAttach
                    // 
                    this.btnAttach.Location = new System.Drawing.Point(120, 245);
                    this.btnAttach.Name = "btnAttach";
                    this.btnAttach.Size = new System.Drawing.Size(100, 23);
                    this.btnAttach.TabIndex = 8;
                    this.btnAttach.Text = "Attach File";
                    this.btnAttach.UseVisualStyleBackColor = true;
                    this.btnAttach.Click += new System.EventHandler(this.btnAttach_Click);
                    // 
                    // lblAttachedFile
                    // 
                    this.lblAttachedFile.AutoSize = true;
                    this.lblAttachedFile.Location = new System.Drawing.Point(230, 250);
                    this.lblAttachedFile.Name = "lblAttachedFile";
                    this.lblAttachedFile.Size = new System.Drawing.Size(83, 13);
                    this.lblAttachedFile.TabIndex = 9;
                    this.lblAttachedFile.Text = "No file attached";
                    // 
                    // btnSubmit
                    // 
                    this.btnSubmit.BackColor = System.Drawing.Color.SteelBlue;
                    this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
                    this.btnSubmit.ForeColor = System.Drawing.Color.White;
                    this.btnSubmit.Location = new System.Drawing.Point(280, 320);
                    this.btnSubmit.Name = "btnSubmit";
                    this.btnSubmit.Size = new System.Drawing.Size(140, 40);
                    this.btnSubmit.TabIndex = 10;
                    this.btnSubmit.Text = "Submit Report";
                    this.btnSubmit.UseVisualStyleBackColor = false;
                    this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
                    // 
                    // btnBack
                    // 
                    this.btnBack.Location = new System.Drawing.Point(120, 320);
                    this.btnBack.Name = "btnBack";
                    this.btnBack.Size = new System.Drawing.Size(140, 40);
                    this.btnBack.TabIndex = 11;
                    this.btnBack.Text = "Back to Main Menu";
                    this.btnBack.UseVisualStyleBackColor = true;
                    this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
                    // 
                    // progressBar
                    // 
                    this.progressBar.Location = new System.Drawing.Point(120, 280);
                    this.progressBar.Name = "progressBar";
                    this.progressBar.Size = new System.Drawing.Size(300, 23);
                    this.progressBar.TabIndex = 12;
                    // 
                    // lblProgress
                    // 
                    this.lblProgress.AutoSize = true;
                    this.lblProgress.Location = new System.Drawing.Point(430, 285);
                    this.lblProgress.Name = "lblProgress";
                    this.lblProgress.Size = new System.Drawing.Size(21, 13);
                    this.lblProgress.TabIndex = 13;
                    this.lblProgress.Text = "0%";
                    // 
                    // lblEncouragement
                    // 
                    this.lblEncouragement.AutoSize = true;
                    this.lblEncouragement.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
                    this.lblEncouragement.ForeColor = System.Drawing.Color.SteelBlue;
                    this.lblEncouragement.Location = new System.Drawing.Point(210, 260);
                    this.lblEncouragement.Name = "lblEncouragement";
                    this.lblEncouragement.Size = new System.Drawing.Size(104, 13);
                    this.lblEncouragement.TabIndex = 14;
                    this.lblEncouragement.Text = "Let\'s get started!";
                    // 
                    // ReportForm
                    // 
                    this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                    this.BackColor = System.Drawing.Color.WhiteSmoke;
                    this.ClientSize = new System.Drawing.Size(500, 380);
                    this.Controls.Add(this.lblEncouragement);
                    this.Controls.Add(this.lblProgress);
                    this.Controls.Add(this.progressBar);
                    this.Controls.Add(this.btnBack);
                    this.Controls.Add(this.btnSubmit);
                    this.Controls.Add(this.lblAttachedFile);
                    this.Controls.Add(this.btnAttach);
                    this.Controls.Add(this.label5);
                    this.Controls.Add(this.rtxtDescription);
                    this.Controls.Add(this.label4);
                    this.Controls.Add(this.cmbCategory);
                    this.Controls.Add(this.label3);
                    this.Controls.Add(this.txtLocation);
                    this.Controls.Add(this.label2);
                    this.Controls.Add(this.label1);
                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                    this.MaximizeBox = false;
                    this.Name = "ReportForm";
                    this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                    this.Text = "Report an Issue";
                    this.Load += new System.EventHandler(this.ReportForm_Load);
                    this.ResumeLayout(false);
                    this.PerformLayout();

                }

                #endregion

                private System.Windows.Forms.Label label1;
                private System.Windows.Forms.Label label2;
                private System.Windows.Forms.TextBox txtLocation;
                private System.Windows.Forms.Label label3;
                private System.Windows.Forms.ComboBox cmbCategory;
                private System.Windows.Forms.Label label4;
                private System.Windows.Forms.RichTextBox rtxtDescription;
                private System.Windows.Forms.Label label5;
                private System.Windows.Forms.Button btnAttach;
                private System.Windows.Forms.Label lblAttachedFile;
                private System.Windows.Forms.Button btnSubmit;
                private System.Windows.Forms.Button btnBack;
                private System.Windows.Forms.ProgressBar progressBar;
                private System.Windows.Forms.Label lblProgress;
                private System.Windows.Forms.Label lblEncouragement;
            }
        }
    }
}
