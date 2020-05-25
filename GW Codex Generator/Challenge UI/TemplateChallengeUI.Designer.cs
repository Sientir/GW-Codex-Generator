namespace GW_Codex_Generator.Challenge_UI
{
    partial class TemplateChallengeUI
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.@__RollTemplateSelection = new System.Windows.Forms.Button();
            this.@__LoadTemplateFile = new System.Windows.Forms.Button();
            this.@__SaveTemplateDatabase = new System.Windows.Forms.Button();
            this.@__TemplatesPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.@__AllowDuplicates = new System.Windows.Forms.CheckBox();
            this.@__TemplateCount = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.@__IncludeInHTMLSummary = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.@__TemplateCount)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.@__IncludeInHTMLSummary);
            this.groupBox1.Controls.Add(this.@__RollTemplateSelection);
            this.groupBox1.Controls.Add(this.@__LoadTemplateFile);
            this.groupBox1.Controls.Add(this.@__SaveTemplateDatabase);
            this.groupBox1.Controls.Add(this.@__TemplatesPanel);
            this.groupBox1.Controls.Add(this.@__AllowDuplicates);
            this.groupBox1.Controls.Add(this.@__TemplateCount);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(649, 321);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Randomly Selected Templates (from template pool)";
            // 
            // __RollTemplateSelection
            // 
            this.@__RollTemplateSelection.Location = new System.Drawing.Point(6, 49);
            this.@__RollTemplateSelection.Name = "__RollTemplateSelection";
            this.@__RollTemplateSelection.Size = new System.Drawing.Size(91, 23);
            this.@__RollTemplateSelection.TabIndex = 7;
            this.@__RollTemplateSelection.Text = "Roll Selection";
            this.@__RollTemplateSelection.UseVisualStyleBackColor = true;
            this.@__RollTemplateSelection.Click += new System.EventHandler(this.@__RollTemplateSelection_Click);
            // 
            // __LoadTemplateFile
            // 
            this.@__LoadTemplateFile.Location = new System.Drawing.Point(494, 49);
            this.@__LoadTemplateFile.Name = "__LoadTemplateFile";
            this.@__LoadTemplateFile.Size = new System.Drawing.Size(149, 23);
            this.@__LoadTemplateFile.TabIndex = 6;
            this.@__LoadTemplateFile.Text = "Re-load Template Database";
            this.@__LoadTemplateFile.UseVisualStyleBackColor = true;
            this.@__LoadTemplateFile.Click += new System.EventHandler(this.@__LoadTemplateFile_Click);
            // 
            // __SaveTemplateDatabase
            // 
            this.@__SaveTemplateDatabase.Location = new System.Drawing.Point(342, 49);
            this.@__SaveTemplateDatabase.Name = "__SaveTemplateDatabase";
            this.@__SaveTemplateDatabase.Size = new System.Drawing.Size(146, 23);
            this.@__SaveTemplateDatabase.TabIndex = 5;
            this.@__SaveTemplateDatabase.Text = "Save Template Database";
            this.@__SaveTemplateDatabase.UseVisualStyleBackColor = true;
            this.@__SaveTemplateDatabase.Click += new System.EventHandler(this.@__SaveTemplateDatabase_Click);
            // 
            // __TemplatesPanel
            // 
            this.@__TemplatesPanel.AutoScroll = true;
            this.@__TemplatesPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.@__TemplatesPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.@__TemplatesPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.@__TemplatesPanel.Location = new System.Drawing.Point(6, 78);
            this.@__TemplatesPanel.Margin = new System.Windows.Forms.Padding(1);
            this.@__TemplatesPanel.Name = "__TemplatesPanel";
            this.@__TemplatesPanel.Padding = new System.Windows.Forms.Padding(1);
            this.@__TemplatesPanel.Size = new System.Drawing.Size(637, 238);
            this.@__TemplatesPanel.TabIndex = 4;
            this.@__TemplatesPanel.WrapContents = false;
            // 
            // __AllowDuplicates
            // 
            this.@__AllowDuplicates.AutoSize = true;
            this.@__AllowDuplicates.Location = new System.Drawing.Point(219, 20);
            this.@__AllowDuplicates.Name = "__AllowDuplicates";
            this.@__AllowDuplicates.Size = new System.Drawing.Size(226, 17);
            this.@__AllowDuplicates.TabIndex = 3;
            this.@__AllowDuplicates.Text = "Allow Duplicates of Templates in Selection";
            this.@__AllowDuplicates.UseVisualStyleBackColor = true;
            // 
            // __TemplateCount
            // 
            this.@__TemplateCount.Location = new System.Drawing.Point(165, 19);
            this.@__TemplateCount.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.@__TemplateCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.@__TemplateCount.Name = "__TemplateCount";
            this.@__TemplateCount.Size = new System.Drawing.Size(48, 20);
            this.@__TemplateCount.TabIndex = 2;
            this.@__TemplateCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Number of Templates to Select";
            // 
            // __IncludeInHTMLSummary
            // 
            this.@__IncludeInHTMLSummary.AutoSize = true;
            this.@__IncludeInHTMLSummary.Location = new System.Drawing.Point(492, 20);
            this.@__IncludeInHTMLSummary.Name = "__IncludeInHTMLSummary";
            this.@__IncludeInHTMLSummary.Size = new System.Drawing.Size(151, 17);
            this.@__IncludeInHTMLSummary.TabIndex = 8;
            this.@__IncludeInHTMLSummary.Text = "Include in HTML Summary";
            this.@__IncludeInHTMLSummary.UseVisualStyleBackColor = true;
            // 
            // TemplateChallengeUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "TemplateChallengeUI";
            this.Size = new System.Drawing.Size(649, 321);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.@__TemplateCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox __AllowDuplicates;
        private System.Windows.Forms.NumericUpDown __TemplateCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button __RollTemplateSelection;
        private System.Windows.Forms.Button __LoadTemplateFile;
        private System.Windows.Forms.Button __SaveTemplateDatabase;
        private System.Windows.Forms.FlowLayoutPanel __TemplatesPanel;
        private System.Windows.Forms.CheckBox __IncludeInHTMLSummary;
    }
}
