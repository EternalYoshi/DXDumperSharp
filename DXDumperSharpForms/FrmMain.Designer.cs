namespace DXDumperSharpForms
{
    partial class FrmMain
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
            this.btnFileOpen = new System.Windows.Forms.Button();
            this.btnOpenTbl = new System.Windows.Forms.Button();
            this.btnDump = new System.Windows.Forms.Button();
            this.txtOffsetStart = new System.Windows.Forms.TextBox();
            this.txtOffsetFin = new System.Windows.Forms.TextBox();
            this.lblOffsetA = new System.Windows.Forms.Label();
            this.lblOffsetB = new System.Windows.Forms.Label();
            this.lblOpenedSptFile = new System.Windows.Forms.Label();
            this.lblOpenedTableFile = new System.Windows.Forms.Label();
            this.lblReminder = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnFileOpen
            // 
            this.btnFileOpen.Location = new System.Drawing.Point(12, 12);
            this.btnFileOpen.Name = "btnFileOpen";
            this.btnFileOpen.Size = new System.Drawing.Size(75, 23);
            this.btnFileOpen.TabIndex = 0;
            this.btnFileOpen.Text = "Open SPT";
            this.btnFileOpen.UseVisualStyleBackColor = true;
            this.btnFileOpen.Click += new System.EventHandler(this.btnFileOpen_Click);
            // 
            // btnOpenTbl
            // 
            this.btnOpenTbl.Location = new System.Drawing.Point(12, 41);
            this.btnOpenTbl.Name = "btnOpenTbl";
            this.btnOpenTbl.Size = new System.Drawing.Size(75, 23);
            this.btnOpenTbl.TabIndex = 1;
            this.btnOpenTbl.Text = "Open Table";
            this.btnOpenTbl.UseVisualStyleBackColor = true;
            this.btnOpenTbl.Click += new System.EventHandler(this.btnOpenTbl_Click);
            // 
            // btnDump
            // 
            this.btnDump.Location = new System.Drawing.Point(342, 181);
            this.btnDump.Name = "btnDump";
            this.btnDump.Size = new System.Drawing.Size(75, 23);
            this.btnDump.TabIndex = 2;
            this.btnDump.Text = "Dump";
            this.btnDump.UseVisualStyleBackColor = true;
            this.btnDump.Click += new System.EventHandler(this.btnDump_Click);
            // 
            // txtOffsetStart
            // 
            this.txtOffsetStart.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtOffsetStart.Location = new System.Drawing.Point(171, 103);
            this.txtOffsetStart.Name = "txtOffsetStart";
            this.txtOffsetStart.Size = new System.Drawing.Size(100, 20);
            this.txtOffsetStart.TabIndex = 3;
            this.txtOffsetStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtOffsetFin
            // 
            this.txtOffsetFin.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.txtOffsetFin.Location = new System.Drawing.Point(171, 138);
            this.txtOffsetFin.Name = "txtOffsetFin";
            this.txtOffsetFin.Size = new System.Drawing.Size(100, 20);
            this.txtOffsetFin.TabIndex = 4;
            this.txtOffsetFin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblOffsetA
            // 
            this.lblOffsetA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblOffsetA.AutoSize = true;
            this.lblOffsetA.Location = new System.Drawing.Point(12, 106);
            this.lblOffsetA.Name = "lblOffsetA";
            this.lblOffsetA.Size = new System.Drawing.Size(119, 13);
            this.lblOffsetA.TabIndex = 5;
            this.lblOffsetA.Text = "Offset to start scanning:";
            this.lblOffsetA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOffsetB
            // 
            this.lblOffsetB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblOffsetB.AutoSize = true;
            this.lblOffsetB.Location = new System.Drawing.Point(12, 141);
            this.lblOffsetB.Name = "lblOffsetB";
            this.lblOffsetB.Size = new System.Drawing.Size(96, 13);
            this.lblOffsetB.TabIndex = 6;
            this.lblOffsetB.Text = "Offset to stop scan";
            this.lblOffsetB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOpenedSptFile
            // 
            this.lblOpenedSptFile.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblOpenedSptFile.AutoSize = true;
            this.lblOpenedSptFile.Location = new System.Drawing.Point(119, 22);
            this.lblOpenedSptFile.Name = "lblOpenedSptFile";
            this.lblOpenedSptFile.Size = new System.Drawing.Size(130, 13);
            this.lblOpenedSptFile.TabIndex = 7;
            this.lblOpenedSptFile.Text = "Choose a .spt file to open.";
            // 
            // lblOpenedTableFile
            // 
            this.lblOpenedTableFile.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblOpenedTableFile.AutoSize = true;
            this.lblOpenedTableFile.Location = new System.Drawing.Point(119, 51);
            this.lblOpenedTableFile.Name = "lblOpenedTableFile";
            this.lblOpenedTableFile.Size = new System.Drawing.Size(198, 13);
            this.lblOpenedTableFile.TabIndex = 8;
            this.lblOpenedTableFile.Text = "Choose a .tbl or .txt file to use as a table.";
            // 
            // lblReminder
            // 
            this.lblReminder.AutoSize = true;
            this.lblReminder.Location = new System.Drawing.Point(12, 78);
            this.lblReminder.Name = "lblReminder";
            this.lblReminder.Size = new System.Drawing.Size(292, 13);
            this.lblReminder.TabIndex = 9;
            this.lblReminder.Text = "Remember! Anything entered below is treated as hex values.";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 216);
            this.Controls.Add(this.lblReminder);
            this.Controls.Add(this.lblOpenedTableFile);
            this.Controls.Add(this.lblOpenedSptFile);
            this.Controls.Add(this.lblOffsetB);
            this.Controls.Add(this.lblOffsetA);
            this.Controls.Add(this.txtOffsetFin);
            this.Controls.Add(this.txtOffsetStart);
            this.Controls.Add(this.btnDump);
            this.Controls.Add(this.btnOpenTbl);
            this.Controls.Add(this.btnFileOpen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmMain";
            this.Text = "DX Text Dumper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFileOpen;
        private System.Windows.Forms.Button btnOpenTbl;
        private System.Windows.Forms.Button btnDump;
        private System.Windows.Forms.TextBox txtOffsetStart;
        private System.Windows.Forms.TextBox txtOffsetFin;
        private System.Windows.Forms.Label lblOffsetA;
        private System.Windows.Forms.Label lblOffsetB;
        private System.Windows.Forms.Label lblOpenedSptFile;
        private System.Windows.Forms.Label lblOpenedTableFile;
        private System.Windows.Forms.Label lblReminder;
    }
}

