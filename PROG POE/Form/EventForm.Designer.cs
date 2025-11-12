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
    using PROG_POE.Service;
    using PROG_POE.Model;

    
        partial class EventsForm
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
                this.txtSearch = new System.Windows.Forms.TextBox();
                this.cmbCategory = new System.Windows.Forms.ComboBox();
                this.cmbDate = new System.Windows.Forms.ComboBox();
                this.btnSearch = new System.Windows.Forms.Button();
                this.flowLayoutEvents = new System.Windows.Forms.FlowLayoutPanel();
                this.btnBack = new System.Windows.Forms.Button();
                this.label2 = new System.Windows.Forms.Label();
                this.label3 = new System.Windows.Forms.Label();
                this.flowLayoutRecommendations = new System.Windows.Forms.FlowLayoutPanel();
                this.label4 = new System.Windows.Forms.Label();
                this.btnClear = new System.Windows.Forms.Button();
                this.SuspendLayout();
                // 
                // label1
                // 
                this.label1.AutoSize = true;
                this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label1.Location = new System.Drawing.Point(250, 15);
                this.label1.Name = "label1";
                this.label1.Size = new System.Drawing.Size(300, 24);
                this.label1.TabIndex = 0;
                this.label1.Text = "Local Events and Announcements";
                // 
                // txtSearch
                // 
                this.txtSearch.Location = new System.Drawing.Point(12, 60);
                this.txtSearch.Name = "txtSearch";
                this.txtSearch.Size = new System.Drawing.Size(200, 20);
                this.txtSearch.TabIndex = 1;
                this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
                // 
                // cmbCategory
                // 
                this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                this.cmbCategory.FormattingEnabled = true;
                this.cmbCategory.Location = new System.Drawing.Point(218, 60);
                this.cmbCategory.Name = "cmbCategory";
                this.cmbCategory.Size = new System.Drawing.Size(150, 21);
                this.cmbCategory.TabIndex = 2;
                this.cmbCategory.SelectedIndexChanged += new System.EventHandler(this.cmbCategory_SelectedIndexChanged);
                // 
                // cmbDate
                // 
                this.cmbDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                this.cmbDate.FormattingEnabled = true;
                this.cmbDate.Location = new System.Drawing.Point(374, 60);
                this.cmbDate.Name = "cmbDate";
                this.cmbDate.Size = new System.Drawing.Size(150, 21);
                this.cmbDate.TabIndex = 3;
                this.cmbDate.SelectedIndexChanged += new System.EventHandler(this.cmbDate_SelectedIndexChanged);
                // 
                // btnSearch
                // 
                this.btnSearch.BackColor = System.Drawing.Color.SteelBlue;
                this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.btnSearch.ForeColor = System.Drawing.Color.White;
                this.btnSearch.Location = new System.Drawing.Point(530, 55);
                this.btnSearch.Name = "btnSearch";
                this.btnSearch.Size = new System.Drawing.Size(80, 30);
                this.btnSearch.TabIndex = 4;
                this.btnSearch.Text = "Search";
                this.btnSearch.UseVisualStyleBackColor = false;
                this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
                // 
                // flowLayoutEvents
                // 
                this.flowLayoutEvents.AutoScroll = true;
                this.flowLayoutEvents.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                this.flowLayoutEvents.Location = new System.Drawing.Point(12, 100);
                this.flowLayoutEvents.Name = "flowLayoutEvents";
                this.flowLayoutEvents.Size = new System.Drawing.Size(750, 350);
                this.flowLayoutEvents.TabIndex = 5;
                // 
                // btnBack
                // 
                this.btnBack.Location = new System.Drawing.Point(612, 55);
                this.btnBack.Name = "btnBack";
                this.btnBack.Size = new System.Drawing.Size(150, 30);
                this.btnBack.TabIndex = 6;
                this.btnBack.Text = "Back to Main Menu";
                this.btnBack.UseVisualStyleBackColor = true;
                this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
                // 
                // label2
                // 
                this.label2.AutoSize = true;
                this.label2.Location = new System.Drawing.Point(218, 40);
                this.label2.Name = "label2";
                this.label2.Size = new System.Drawing.Size(52, 13);
                this.label2.TabIndex = 7;
                this.label2.Text = "Category:";

                // label3

                this.label3.AutoSize = true;
                this.label3.Location = new System.Drawing.Point(374, 40);
                this.label3.Name = "label3";
                this.label3.Size = new System.Drawing.Size(33, 13);
                this.label3.TabIndex = 8;
                this.label3.Text = "Date:";

                // flowLayoutRecommendations

                this.flowLayoutRecommendations.AutoScroll = true;
                this.flowLayoutRecommendations.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                this.flowLayoutRecommendations.Location = new System.Drawing.Point(12, 470);
                this.flowLayoutRecommendations.Name = "flowLayoutRecommendations";
                this.flowLayoutRecommendations.Size = new System.Drawing.Size(750, 120);
                this.flowLayoutRecommendations.TabIndex = 9;

                // label4

                this.label4.AutoSize = true;
                this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label4.Location = new System.Drawing.Point(12, 450);
                this.label4.Name = "label4";
                this.label4.Size = new System.Drawing.Size(123, 17);
                this.label4.TabIndex = 10;
                this.label4.Text = "Recommendations";
                // 
                // btnClear
                // 
                this.btnClear.Location = new System.Drawing.Point(682, 15);
                this.btnClear.Name = "btnClear";
                this.btnClear.Size = new System.Drawing.Size(80, 30);
                this.btnClear.TabIndex = 11;
                this.btnClear.Text = "Clear Filters";
                this.btnClear.UseVisualStyleBackColor = true;
                this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
                // 
                // EventsForm
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.BackColor = System.Drawing.Color.WhiteSmoke;
                this.ClientSize = new System.Drawing.Size(784, 611);
                this.Controls.Add(this.btnClear);
                this.Controls.Add(this.label4);
                this.Controls.Add(this.flowLayoutRecommendations);
                this.Controls.Add(this.label3);
                this.Controls.Add(this.label2);
                this.Controls.Add(this.btnBack);
                this.Controls.Add(this.flowLayoutEvents);
                this.Controls.Add(this.btnSearch);
                this.Controls.Add(this.cmbDate);
                this.Controls.Add(this.cmbCategory);
                this.Controls.Add(this.txtSearch);
                this.Controls.Add(this.label1);
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                this.MaximizeBox = false;
                this.Name = "EventsForm";
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                this.Text = "Local Events and Announcements";
                this.Load += new System.EventHandler(this.EventsForm_Load);
                this.ResumeLayout(false);
                this.PerformLayout();

            }

            #endregion

            private System.Windows.Forms.Label label1;
            private System.Windows.Forms.TextBox txtSearch;
            private System.Windows.Forms.ComboBox cmbCategory;
            private System.Windows.Forms.ComboBox cmbDate;
            private System.Windows.Forms.Button btnSearch;
            private System.Windows.Forms.FlowLayoutPanel flowLayoutEvents;
            private System.Windows.Forms.Button btnBack;
            private System.Windows.Forms.Label label2;
            private System.Windows.Forms.Label label3;
            private System.Windows.Forms.FlowLayoutPanel flowLayoutRecommendations;
            private System.Windows.Forms.Label label4;
            private System.Windows.Forms.Button btnClear;
        }
    }

