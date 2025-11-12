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


        partial class CommunityHubForm
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
                this.listViewIssues = new System.Windows.Forms.ListView();
                this.columnHeaderRef = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
                this.columnHeaderLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
                this.columnHeaderCategory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
                this.columnHeaderDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
                this.columnHeaderDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
                this.columnHeaderSupports = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
                this.columnHeaderStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
                this.btnSupport = new System.Windows.Forms.Button();
                this.btnBack = new System.Windows.Forms.Button();
                this.label1 = new System.Windows.Forms.Label();
                this.btnRefresh = new System.Windows.Forms.Button();
                this.txtSearch = new System.Windows.Forms.TextBox();
                this.label2 = new System.Windows.Forms.Label();
                this.cmbCategoryFilter = new System.Windows.Forms.ComboBox();
                this.label3 = new System.Windows.Forms.Label();
                this.groupBox1 = new System.Windows.Forms.GroupBox();
                this.flowLayoutRecent = new System.Windows.Forms.FlowLayoutPanel();
                this.label4 = new System.Windows.Forms.Label();
                this.groupBox2 = new System.Windows.Forms.GroupBox();
                this.flowLayoutPopular = new System.Windows.Forms.FlowLayoutPanel();
                this.label5 = new System.Windows.Forms.Label();
                this.groupBox3 = new System.Windows.Forms.GroupBox();
                this.flowLayoutStatus = new System.Windows.Forms.FlowLayoutPanel();
                this.lblAvgSupports = new System.Windows.Forms.Label();
                this.lblTotalSupports = new System.Windows.Forms.Label();
                this.lblTotalIssues = new System.Windows.Forms.Label();
                this.btnSortBySupports = new System.Windows.Forms.Button();
                this.btnSortByDate = new System.Windows.Forms.Button();
                this.groupBox1.SuspendLayout();
                this.groupBox2.SuspendLayout();
                this.groupBox3.SuspendLayout();
                this.SuspendLayout();
                // 
                // listViewIssues
                // 
                this.listViewIssues.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderRef,
            this.columnHeaderLocation,
            this.columnHeaderCategory,
            this.columnHeaderDescription,
            this.columnHeaderDate,
            this.columnHeaderSupports,
            this.columnHeaderStatus});
                this.listViewIssues.FullRowSelect = true;
                this.listViewIssues.GridLines = true;
                this.listViewIssues.HideSelection = false;
                this.listViewIssues.Location = new System.Drawing.Point(12, 120);
                this.listViewIssues.Name = "listViewIssues";
                this.listViewIssues.Size = new System.Drawing.Size(700, 250);
                this.listViewIssues.TabIndex = 0;
                this.listViewIssues.UseCompatibleStateImageBehavior = false;
                this.listViewIssues.View = System.Windows.Forms.View.Details;
                this.listViewIssues.DoubleClick += new System.EventHandler(this.listViewIssues_DoubleClick);
                // 
                // columnHeaderRef
                // 
                this.columnHeaderRef.Text = "Reference #";
                this.columnHeaderRef.Width = 80;
                // 
                // columnHeaderLocation
                // 
                this.columnHeaderLocation.Text = "Location";
                this.columnHeaderLocation.Width = 100;
                // 
                // columnHeaderCategory
                // 
                this.columnHeaderCategory.Text = "Category";
                this.columnHeaderCategory.Width = 100;
                // 
                // columnHeaderDescription
                // 
                this.columnHeaderDescription.Text = "Description";
                this.columnHeaderDescription.Width = 200;
                // 
                // columnHeaderDate
                // 
                this.columnHeaderDate.Text = "Report Date";
                this.columnHeaderDate.Width = 80;
                // 
                // columnHeaderSupports
                // 
                this.columnHeaderSupports.Text = "Supports";
                this.columnHeaderSupports.Width = 60;
                // 
                // columnHeaderStatus
                // 
                this.columnHeaderStatus.Text = "Status";
                this.columnHeaderStatus.Width = 80;
                // 
                // btnSupport
                // 
                this.btnSupport.BackColor = System.Drawing.Color.SteelBlue;
                this.btnSupport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.btnSupport.ForeColor = System.Drawing.Color.White;
                this.btnSupport.Location = new System.Drawing.Point(12, 380);
                this.btnSupport.Name = "btnSupport";
                this.btnSupport.Size = new System.Drawing.Size(150, 40);
                this.btnSupport.TabIndex = 1;
                this.btnSupport.Text = "Support Issue";
                this.btnSupport.UseVisualStyleBackColor = false;
                this.btnSupport.Click += new System.EventHandler(this.btnSupport_Click);
                // 
                // btnBack
                // 
                this.btnBack.Location = new System.Drawing.Point(562, 380);
                this.btnBack.Name = "btnBack";
                this.btnBack.Size = new System.Drawing.Size(150, 40);
                this.btnBack.TabIndex = 2;
                this.btnBack.Text = "Back to Main Menu";
                this.btnBack.UseVisualStyleBackColor = true;
                this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
                // 
                // label1
                // 
                this.label1.AutoSize = true;
                this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label1.Location = new System.Drawing.Point(250, 15);
                this.label1.Name = "label1";
                this.label1.Size = new System.Drawing.Size(224, 24);
                this.label1.TabIndex = 3;
                this.label1.Text = "Community Issues Hub";
                // 
                // btnRefresh
                // 
                this.btnRefresh.Location = new System.Drawing.Point(622, 80);
                this.btnRefresh.Name = "btnRefresh";
                this.btnRefresh.Size = new System.Drawing.Size(90, 30);
                this.btnRefresh.TabIndex = 4;
                this.btnRefresh.Text = "Refresh";
                this.btnRefresh.UseVisualStyleBackColor = true;
                this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
                // 
                // txtSearch
                // 
                this.txtSearch.Location = new System.Drawing.Point(12, 85);
                this.txtSearch.Name = "txtSearch";
                this.txtSearch.Size = new System.Drawing.Size(200, 20);
                this.txtSearch.TabIndex = 5;
                this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
                // 
                // label2
                // 
                this.label2.AutoSize = true;
                this.label2.Location = new System.Drawing.Point(12, 65);
                this.label2.Name = "label2";
                this.label2.Size = new System.Drawing.Size(44, 13);
                this.label2.TabIndex = 6;
                this.label2.Text = "Search:";
                // 
                // cmbCategoryFilter
                // 
                this.cmbCategoryFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                this.cmbCategoryFilter.FormattingEnabled = true;
                this.cmbCategoryFilter.Location = new System.Drawing.Point(218, 85);
                this.cmbCategoryFilter.Name = "cmbCategoryFilter";
                this.cmbCategoryFilter.Size = new System.Drawing.Size(150, 21);
                this.cmbCategoryFilter.TabIndex = 7;
                this.cmbCategoryFilter.SelectedIndexChanged += new System.EventHandler(this.cmbCategoryFilter_SelectedIndexChanged);
                // 
                // label3
                // 
                this.label3.AutoSize = true;
                this.label3.Location = new System.Drawing.Point(218, 65);
                this.label3.Name = "label3";
                this.label3.Size = new System.Drawing.Size(52, 13);
                this.label3.TabIndex = 8;
                this.label3.Text = "Category:";
                // 
                // groupBox1
                // 
                this.groupBox1.Controls.Add(this.flowLayoutRecent);
                this.groupBox1.Controls.Add(this.label4);
                this.groupBox1.Location = new System.Drawing.Point(12, 430);
                this.groupBox1.Name = "groupBox1";
                this.groupBox1.Size = new System.Drawing.Size(630, 120);
                this.groupBox1.TabIndex = 9;
                this.groupBox1.TabStop = false;
                this.groupBox1.Text = "Quick Access";
                // 
                // flowLayoutRecent
                // 
                this.flowLayoutRecent.AutoScroll = true;
                this.flowLayoutRecent.Location = new System.Drawing.Point(6, 35);
                this.flowLayoutRecent.Name = "flowLayoutRecent";
                this.flowLayoutRecent.Size = new System.Drawing.Size(618, 79);
                this.flowLayoutRecent.TabIndex = 1;
                // 
                // label4
                // 
                this.label4.AutoSize = true;
                this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label4.Location = new System.Drawing.Point(6, 16);
                this.label4.Name = "label4";
                this.label4.Size = new System.Drawing.Size(116, 15);
                this.label4.TabIndex = 0;
                this.label4.Text = "Recently Viewed:";
                // 
                // groupBox2
                // 
                this.groupBox2.Controls.Add(this.flowLayoutPopular);
                this.groupBox2.Controls.Add(this.label5);
                this.groupBox2.Location = new System.Drawing.Point(12, 560);
                this.groupBox2.Name = "groupBox2";
                this.groupBox2.Size = new System.Drawing.Size(630, 120);
                this.groupBox2.TabIndex = 10;
                this.groupBox2.TabStop = false;
                this.groupBox2.Text = "Community Highlights";
                // 
                // flowLayoutPopular
                // 
                this.flowLayoutPopular.AutoScroll = true;
                this.flowLayoutPopular.Location = new System.Drawing.Point(6, 35);
                this.flowLayoutPopular.Name = "flowLayoutPopular";
                this.flowLayoutPopular.Size = new System.Drawing.Size(618, 79);
                this.flowLayoutPopular.TabIndex = 1;
                // 
                // label5
                // 
                this.label5.AutoSize = true;
                this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label5.Location = new System.Drawing.Point(6, 16);
                this.label5.Name = "label5";
                this.label5.Size = new System.Drawing.Size(106, 15);
                this.label5.TabIndex = 0;
                this.label5.Text = "Popular Issues:";
                // 
                // groupBox3
                // 
                this.groupBox3.Controls.Add(this.flowLayoutStatus);
                this.groupBox3.Controls.Add(this.lblAvgSupports);
                this.groupBox3.Controls.Add(this.lblTotalSupports);
                this.groupBox3.Controls.Add(this.lblTotalIssues);
                this.groupBox3.Location = new System.Drawing.Point(718, 120);
                this.groupBox3.Name = "groupBox3";
                this.groupBox3.Size = new System.Drawing.Size(250, 250);
                this.groupBox3.TabIndex = 11;
                this.groupBox3.TabStop = false;
                this.groupBox3.Text = "Community Statistics";
                // 
                // flowLayoutStatus
                // 
                this.flowLayoutStatus.Location = new System.Drawing.Point(6, 100);
                this.flowLayoutStatus.Name = "flowLayoutStatus";
                this.flowLayoutStatus.Size = new System.Drawing.Size(238, 144);
                this.flowLayoutStatus.TabIndex = 3;
                // 
                // lblAvgSupports
                // 
                this.lblAvgSupports.AutoSize = true;
                this.lblAvgSupports.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.lblAvgSupports.Location = new System.Drawing.Point(6, 75);
                this.lblAvgSupports.Name = "lblAvgSupports";
                this.lblAvgSupports.Size = new System.Drawing.Size(100, 15);
                this.lblAvgSupports.TabIndex = 2;
                this.lblAvgSupports.Text = "Avg Supports: ";
                // 
                // lblTotalSupports
                // 
                this.lblTotalSupports.AutoSize = true;
                this.lblTotalSupports.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.lblTotalSupports.Location = new System.Drawing.Point(6, 50);
                this.lblTotalSupports.Name = "lblTotalSupports";
                this.lblTotalSupports.Size = new System.Drawing.Size(105, 15);
                this.lblTotalSupports.TabIndex = 1;
                this.lblTotalSupports.Text = "Total Supports: ";
                // 
                // lblTotalIssues
                // 
                this.lblTotalIssues.AutoSize = true;
                this.lblTotalIssues.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.lblTotalIssues.Location = new System.Drawing.Point(6, 25);
                this.lblTotalIssues.Name = "lblTotalIssues";
                this.lblTotalIssues.Size = new System.Drawing.Size(89, 15);
                this.lblTotalIssues.TabIndex = 0;
                this.lblTotalIssues.Text = "Total Issues: ";
                // 
                // btnSortBySupports
                // 
                this.btnSortBySupports.Location = new System.Drawing.Point(374, 80);
                this.btnSortBySupports.Name = "btnSortBySupports";
                this.btnSortBySupports.Size = new System.Drawing.Size(120, 30);
                this.btnSortBySupports.TabIndex = 12;
                this.btnSortBySupports.Text = "Sort by Supports";
                this.btnSortBySupports.UseVisualStyleBackColor = true;
                this.btnSortBySupports.Click += new System.EventHandler(this.btnSortBySupports_Click);
                // 
                // btnSortByDate
                // 
                this.btnSortByDate.Location = new System.Drawing.Point(500, 80);
                this.btnSortByDate.Name = "btnSortByDate";
                this.btnSortByDate.Size = new System.Drawing.Size(120, 30);
                this.btnSortByDate.TabIndex = 13;
                this.btnSortByDate.Text = "Sort by Date";
                this.btnSortByDate.UseVisualStyleBackColor = true;
                this.btnSortByDate.Click += new System.EventHandler(this.btnSortByDate_Click);
                // 
                // CommunityHubForm
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.BackColor = System.Drawing.Color.WhiteSmoke;
                this.ClientSize = new System.Drawing.Size(984, 711);
                this.Controls.Add(this.btnSortByDate);
                this.Controls.Add(this.btnSortBySupports);
                this.Controls.Add(this.groupBox3);
                this.Controls.Add(this.groupBox2);
                this.Controls.Add(this.groupBox1);
                this.Controls.Add(this.label3);
                this.Controls.Add(this.cmbCategoryFilter);
                this.Controls.Add(this.label2);
                this.Controls.Add(this.txtSearch);
                this.Controls.Add(this.btnRefresh);
                this.Controls.Add(this.label1);
                this.Controls.Add(this.btnBack);
                this.Controls.Add(this.btnSupport);
                this.Controls.Add(this.listViewIssues);
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                this.MaximizeBox = false;
                this.Name = "CommunityHubForm";
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                this.Text = "Community Collaboration Hub";
                this.Load += new System.EventHandler(this.CommunityHubForm_Load);
                this.groupBox1.ResumeLayout(false);
                this.groupBox1.PerformLayout();
                this.groupBox2.ResumeLayout(false);
                this.groupBox2.PerformLayout();
                this.groupBox3.ResumeLayout(false);
                this.groupBox3.PerformLayout();
                this.ResumeLayout(false);
                this.PerformLayout();

            }

            #endregion

            private System.Windows.Forms.ListView listViewIssues;
            private System.Windows.Forms.ColumnHeader columnHeaderRef;
            private System.Windows.Forms.ColumnHeader columnHeaderLocation;
            private System.Windows.Forms.ColumnHeader columnHeaderCategory;
            private System.Windows.Forms.ColumnHeader columnHeaderDescription;
            private System.Windows.Forms.ColumnHeader columnHeaderDate;
            private System.Windows.Forms.ColumnHeader columnHeaderSupports;
            private System.Windows.Forms.ColumnHeader columnHeaderStatus;
            private System.Windows.Forms.Button btnSupport;
            private System.Windows.Forms.Button btnBack;
            private System.Windows.Forms.Label label1;
            private System.Windows.Forms.Button btnRefresh;
            private System.Windows.Forms.TextBox txtSearch;
            private System.Windows.Forms.Label label2;
            private System.Windows.Forms.ComboBox cmbCategoryFilter;
            private System.Windows.Forms.Label label3;
            private System.Windows.Forms.GroupBox groupBox1;
            private System.Windows.Forms.FlowLayoutPanel flowLayoutRecent;
            private System.Windows.Forms.Label label4;
            private System.Windows.Forms.GroupBox groupBox2;
            private System.Windows.Forms.FlowLayoutPanel flowLayoutPopular;
            private System.Windows.Forms.Label label5;
            private System.Windows.Forms.GroupBox groupBox3;
            private System.Windows.Forms.FlowLayoutPanel flowLayoutStatus;
            private System.Windows.Forms.Label lblAvgSupports;
            private System.Windows.Forms.Label lblTotalSupports;
            private System.Windows.Forms.Label lblTotalIssues;
            private System.Windows.Forms.Button btnSortBySupports;
            private System.Windows.Forms.Button btnSortByDate;
        }
    }


