namespace LTC
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bsChannels = new System.Windows.Forms.BindingSource(this.components);
            this.bsPrograms = new System.Windows.Forms.BindingSource(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.channelListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.getFromWebpageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.esportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.programmmsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearCurrentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllForDatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearCurrentForDatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.getAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getCurrentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.getCurrentFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDateFrom = new System.Windows.Forms.TextBox();
            this.tbDateTo = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tbAddHours = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbTimeZone = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gdvChannels = new System.Windows.Forms.DataGridView();
            this.dgvPrograms = new System.Windows.Forms.DataGridView();
            this.tbDescr = new System.Windows.Forms.TextBox();
            this.dgcPrStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcPrEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcPrTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcPrDescr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcChUse = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgcChName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcChURL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsChannels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPrograms)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdvChannels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrograms)).BeginInit();
            this.SuspendLayout();
            // 
            // bsChannels
            // 
            this.bsChannels.CurrentChanged += new System.EventHandler(this.bsChannels_CurrentChanged);
            // 
            // bsPrograms
            // 
            this.bsPrograms.CurrentChanged += new System.EventHandler(this.bsPrograms_CurrentChanged);
            this.bsPrograms.ListChanged += new System.ComponentModel.ListChangedEventHandler(this.bsPrograms_ListChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(21, 21);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.channelListToolStripMenuItem,
            this.programmmsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1000, 33);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // channelListToolStripMenuItem
            // 
            this.channelListToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator2,
            this.getFromWebpageToolStripMenuItem,
            this.getFromFileToolStripMenuItem,
            this.toolStripSeparator3,
            this.esportToolStripMenuItem});
            this.channelListToolStripMenuItem.Name = "channelListToolStripMenuItem";
            this.channelListToolStripMenuItem.Size = new System.Drawing.Size(133, 29);
            this.channelListToolStripMenuItem.Text = "Channel List";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(264, 30);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(264, 30);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(261, 6);
            // 
            // getFromWebpageToolStripMenuItem
            // 
            this.getFromWebpageToolStripMenuItem.Name = "getFromWebpageToolStripMenuItem";
            this.getFromWebpageToolStripMenuItem.Size = new System.Drawing.Size(264, 30);
            this.getFromWebpageToolStripMenuItem.Text = "Get From Webpage";
            this.getFromWebpageToolStripMenuItem.Click += new System.EventHandler(this.getFromWebpageToolStripMenuItem_Click);
            // 
            // getFromFileToolStripMenuItem
            // 
            this.getFromFileToolStripMenuItem.Name = "getFromFileToolStripMenuItem";
            this.getFromFileToolStripMenuItem.Size = new System.Drawing.Size(264, 30);
            this.getFromFileToolStripMenuItem.Text = "Get From File";
            this.getFromFileToolStripMenuItem.Click += new System.EventHandler(this.getFromFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(261, 6);
            // 
            // esportToolStripMenuItem
            // 
            this.esportToolStripMenuItem.Name = "esportToolStripMenuItem";
            this.esportToolStripMenuItem.Size = new System.Drawing.Size(264, 30);
            this.esportToolStripMenuItem.Text = "Export";
            this.esportToolStripMenuItem.Click += new System.EventHandler(this.esportToolStripMenuItem_Click);
            // 
            // programmmsToolStripMenuItem
            // 
            this.programmmsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearAllToolStripMenuItem,
            this.clearCurrentToolStripMenuItem,
            this.clearAllForDatesToolStripMenuItem,
            this.clearCurrentForDatesToolStripMenuItem,
            this.toolStripSeparator1,
            this.getAllToolStripMenuItem,
            this.getCurrentToolStripMenuItem,
            this.toolStripSeparator4,
            this.getCurrentFromFileToolStripMenuItem});
            this.programmmsToolStripMenuItem.Name = "programmmsToolStripMenuItem";
            this.programmmsToolStripMenuItem.Size = new System.Drawing.Size(108, 29);
            this.programmmsToolStripMenuItem.Text = "Programs";
            // 
            // clearAllToolStripMenuItem
            // 
            this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
            this.clearAllToolStripMenuItem.Size = new System.Drawing.Size(356, 30);
            this.clearAllToolStripMenuItem.Text = "Clear All";
            this.clearAllToolStripMenuItem.Click += new System.EventHandler(this.clearAllToolStripMenuItem_Click);
            // 
            // clearCurrentToolStripMenuItem
            // 
            this.clearCurrentToolStripMenuItem.Name = "clearCurrentToolStripMenuItem";
            this.clearCurrentToolStripMenuItem.Size = new System.Drawing.Size(356, 30);
            this.clearCurrentToolStripMenuItem.Text = "Clear Current";
            this.clearCurrentToolStripMenuItem.Click += new System.EventHandler(this.clearCurrentToolStripMenuItem_Click);
            // 
            // clearAllForDatesToolStripMenuItem
            // 
            this.clearAllForDatesToolStripMenuItem.Name = "clearAllForDatesToolStripMenuItem";
            this.clearAllForDatesToolStripMenuItem.Size = new System.Drawing.Size(356, 30);
            this.clearAllForDatesToolStripMenuItem.Text = "Clear All For Dates";
            this.clearAllForDatesToolStripMenuItem.Click += new System.EventHandler(this.clearAllForDatesToolStripMenuItem_Click);
            // 
            // clearCurrentForDatesToolStripMenuItem
            // 
            this.clearCurrentForDatesToolStripMenuItem.Name = "clearCurrentForDatesToolStripMenuItem";
            this.clearCurrentForDatesToolStripMenuItem.Size = new System.Drawing.Size(356, 30);
            this.clearCurrentForDatesToolStripMenuItem.Text = "Clear Current For Dates";
            this.clearCurrentForDatesToolStripMenuItem.Click += new System.EventHandler(this.clearCurrentForDatesToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(353, 6);
            // 
            // getAllToolStripMenuItem
            // 
            this.getAllToolStripMenuItem.Name = "getAllToolStripMenuItem";
            this.getAllToolStripMenuItem.Size = new System.Drawing.Size(356, 30);
            this.getAllToolStripMenuItem.Text = "Get All";
            this.getAllToolStripMenuItem.Click += new System.EventHandler(this.getAllToolStripMenuItem_Click);
            // 
            // getCurrentToolStripMenuItem
            // 
            this.getCurrentToolStripMenuItem.Name = "getCurrentToolStripMenuItem";
            this.getCurrentToolStripMenuItem.Size = new System.Drawing.Size(356, 30);
            this.getCurrentToolStripMenuItem.Text = "Get Current";
            this.getCurrentToolStripMenuItem.Click += new System.EventHandler(this.getCurrentToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(353, 6);
            // 
            // getCurrentFromFileToolStripMenuItem
            // 
            this.getCurrentFromFileToolStripMenuItem.Name = "getCurrentFromFileToolStripMenuItem";
            this.getCurrentFromFileToolStripMenuItem.Size = new System.Drawing.Size(356, 30);
            this.getCurrentFromFileToolStripMenuItem.Text = "Get Current From File (for test)";
            this.getCurrentFromFileToolStripMenuItem.Click += new System.EventHandler(this.getCurrentFromFileToolStripMenuItem_Click);
            // 
            // tbURL
            // 
            this.tbURL.Location = new System.Drawing.Point(64, 33);
            this.tbURL.Name = "tbURL";
            this.tbURL.Size = new System.Drawing.Size(532, 27);
            this.tbURL.TabIndex = 4;
            this.tbURL.Leave += new System.EventHandler(this.tbURL_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 22);
            this.label1.TabIndex = 5;
            this.label1.Text = "URL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 22);
            this.label2.TabIndex = 6;
            this.label2.Text = "Date from - to";
            // 
            // tbDateFrom
            // 
            this.tbDateFrom.Location = new System.Drawing.Point(157, 67);
            this.tbDateFrom.Name = "tbDateFrom";
            this.tbDateFrom.Size = new System.Drawing.Size(116, 27);
            this.tbDateFrom.TabIndex = 7;
            // 
            // tbDateTo
            // 
            this.tbDateTo.Location = new System.Drawing.Point(277, 67);
            this.tbDateTo.Name = "tbDateTo";
            this.tbDateTo.Size = new System.Drawing.Size(116, 27);
            this.tbDateTo.TabIndex = 7;
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tbAddHours
            // 
            this.tbAddHours.Location = new System.Drawing.Point(504, 67);
            this.tbAddHours.Name = "tbAddHours";
            this.tbAddHours.Size = new System.Drawing.Size(73, 27);
            this.tbAddHours.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(402, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 22);
            this.label3.TabIndex = 6;
            this.label3.Text = "Add hours";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(583, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 22);
            this.label4.TabIndex = 6;
            this.label4.Text = "Time zone";
            // 
            // tbTimeZone
            // 
            this.tbTimeZone.Location = new System.Drawing.Point(685, 67);
            this.tbTimeZone.Name = "tbTimeZone";
            this.tbTimeZone.Size = new System.Drawing.Size(73, 27);
            this.tbTimeZone.TabIndex = 7;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 100);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gdvChannels);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvPrograms);
            this.splitContainer1.Panel2.Controls.Add(this.tbDescr);
            this.splitContainer1.Size = new System.Drawing.Size(995, 477);
            this.splitContainer1.SplitterDistance = 300;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 8;
            // 
            // gdvChannels
            // 
            this.gdvChannels.AutoGenerateColumns = false;
            this.gdvChannels.BackgroundColor = System.Drawing.SystemColors.Control;
            this.gdvChannels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gdvChannels.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcChUse,
            this.dgcChName,
            this.dgcChURL});
            this.gdvChannels.DataSource = this.bsChannels;
            this.gdvChannels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gdvChannels.Location = new System.Drawing.Point(0, 0);
            this.gdvChannels.Name = "gdvChannels";
            this.gdvChannels.RowHeadersVisible = false;
            this.gdvChannels.RowTemplate.Height = 24;
            this.gdvChannels.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.gdvChannels.Size = new System.Drawing.Size(300, 477);
            this.gdvChannels.TabIndex = 2;
            // 
            // dgvPrograms
            // 
            this.dgvPrograms.AutoGenerateColumns = false;
            this.dgvPrograms.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvPrograms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPrograms.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcPrStart,
            this.dgcPrEnd,
            this.dgcPrTitle,
            this.dgcPrDescr});
            this.dgvPrograms.DataSource = this.bsPrograms;
            this.dgvPrograms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPrograms.Location = new System.Drawing.Point(0, 0);
            this.dgvPrograms.Name = "dgvPrograms";
            this.dgvPrograms.RowHeadersVisible = false;
            this.dgvPrograms.RowTemplate.Height = 24;
            this.dgvPrograms.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPrograms.Size = new System.Drawing.Size(689, 343);
            this.dgvPrograms.TabIndex = 3;
            // 
            // tbDescr
            // 
            this.tbDescr.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbDescr.Location = new System.Drawing.Point(0, 343);
            this.tbDescr.Multiline = true;
            this.tbDescr.Name = "tbDescr";
            this.tbDescr.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbDescr.Size = new System.Drawing.Size(689, 134);
            this.tbDescr.TabIndex = 4;
            // 
            // dgcPrStart
            // 
            this.dgcPrStart.DataPropertyName = "Start";
            dataGridViewCellStyle3.Format = "dd.MM HH:mm";
            this.dgcPrStart.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgcPrStart.HeaderText = "Start";
            this.dgcPrStart.Name = "dgcPrStart";
            this.dgcPrStart.Width = 120;
            // 
            // dgcPrEnd
            // 
            this.dgcPrEnd.DataPropertyName = "End";
            dataGridViewCellStyle4.Format = "dd.MM HH:mm";
            this.dgcPrEnd.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgcPrEnd.HeaderText = "End";
            this.dgcPrEnd.Name = "dgcPrEnd";
            this.dgcPrEnd.Width = 120;
            // 
            // dgcPrTitle
            // 
            this.dgcPrTitle.DataPropertyName = "Title";
            this.dgcPrTitle.HeaderText = "Title";
            this.dgcPrTitle.Name = "dgcPrTitle";
            this.dgcPrTitle.Width = 400;
            // 
            // dgcPrDescr
            // 
            this.dgcPrDescr.DataPropertyName = "Description";
            this.dgcPrDescr.HeaderText = "Descr";
            this.dgcPrDescr.Name = "dgcPrDescr";
            this.dgcPrDescr.Visible = false;
            this.dgcPrDescr.Width = 400;
            // 
            // dgcChUse
            // 
            this.dgcChUse.DataPropertyName = "Use";
            this.dgcChUse.FalseValue = "false";
            this.dgcChUse.HeaderText = "Use";
            this.dgcChUse.Name = "dgcChUse";
            this.dgcChUse.TrueValue = "true";
            this.dgcChUse.Width = 40;
            // 
            // dgcChName
            // 
            this.dgcChName.DataPropertyName = "Name";
            this.dgcChName.HeaderText = "Name";
            this.dgcChName.Name = "dgcChName";
            this.dgcChName.Width = 230;
            // 
            // dgcChURL
            // 
            this.dgcChURL.DataPropertyName = "URL";
            this.dgcChURL.HeaderText = "URL";
            this.dgcChURL.Name = "dgcChURL";
            this.dgcChURL.Visible = false;
            this.dgcChURL.Width = 470;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 579);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.tbTimeZone);
            this.Controls.Add(this.tbAddHours);
            this.Controls.Add(this.tbDateTo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbDateFrom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbURL);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "LTC EPG";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsChannels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPrograms)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gdvChannels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrograms)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource bsChannels;
        private System.Windows.Forms.BindingSource bsPrograms;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem channelListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getFromWebpageToolStripMenuItem;
        private System.Windows.Forms.TextBox tbURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbDateFrom;
        private System.Windows.Forms.TextBox tbDateTo;
        private System.Windows.Forms.ToolStripMenuItem programmmsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearCurrentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getCurrentToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem clearAllForDatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearCurrentForDatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem esportToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TextBox tbAddHours;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbTimeZone;
        private System.Windows.Forms.ToolStripMenuItem getFromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem getCurrentFromFileToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvPrograms;
        private System.Windows.Forms.DataGridView gdvChannels;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcPrStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcPrEnd;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcPrTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcPrDescr;
        private System.Windows.Forms.TextBox tbDescr;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgcChUse;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcChName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgcChURL;
    }
}

