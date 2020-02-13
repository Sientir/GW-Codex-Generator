namespace GW_Codex_Generator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.@__Tabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.@__CodexTabLayout = new System.Windows.Forms.TableLayoutPanel();
            this.@__GenerateCodexButton = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.@__CodexSettingsLayoutTable = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.@__LoadRatingFilebutton = new System.Windows.Forms.Button();
            this.@__GenerateRatingsFile = new System.Windows.Forms.Button();
            this.@__Codex_Pool_PvEOnly = new System.Windows.Forms.CheckBox();
            this.@__Codex_Pool_EotN = new System.Windows.Forms.CheckBox();
            this.@__Codex_Pool_Nightfall = new System.Windows.Forms.CheckBox();
            this.@__Codex_Pool_Factions = new System.Windows.Forms.CheckBox();
            this.@__Codex_Pool_Prophecies = new System.Windows.Forms.CheckBox();
            this.@__Codex_Pool_Core = new System.Windows.Forms.CheckBox();
            this.@__Codex_SelectMethod = new System.Windows.Forms.ComboBox();
            this.@__Codex_UseRatings = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.@__HTMLSummary = new System.Windows.Forms.TextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.@__CodexSkillInfo = new GW_Codex_Generator.Skill_UI.SkillInfoDisplay();
            this.@__CodexSkillDisplay = new GW_Codex_Generator.Skill_UI.SkillDisplay();
            this.@__TemplateCodeParserTest = new GW_Codex_Generator.TemplateReaderDisplay();
            this.@__Tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.@__CodexTabLayout.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.@__CodexSettingsLayoutTable.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // __Tabs
            // 
            this.@__Tabs.Controls.Add(this.tabPage1);
            this.@__Tabs.Controls.Add(this.tabPage2);
            this.@__Tabs.Controls.Add(this.tabPage3);
            this.@__Tabs.Controls.Add(this.tabPage4);
            this.@__Tabs.Controls.Add(this.tabPage5);
            this.@__Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.@__Tabs.Location = new System.Drawing.Point(0, 0);
            this.@__Tabs.Name = "__Tabs";
            this.@__Tabs.SelectedIndex = 0;
            this.@__Tabs.Size = new System.Drawing.Size(796, 579);
            this.@__Tabs.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.@__CodexTabLayout);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(788, 553);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Codex";
            // 
            // __CodexTabLayout
            // 
            this.@__CodexTabLayout.ColumnCount = 1;
            this.@__CodexTabLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.@__CodexTabLayout.Controls.Add(this.@__CodexSkillDisplay, 0, 1);
            this.@__CodexTabLayout.Controls.Add(this.@__GenerateCodexButton, 0, 0);
            this.@__CodexTabLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.@__CodexTabLayout.Location = new System.Drawing.Point(3, 3);
            this.@__CodexTabLayout.Name = "__CodexTabLayout";
            this.@__CodexTabLayout.RowCount = 2;
            this.@__CodexTabLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.@__CodexTabLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.@__CodexTabLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.@__CodexTabLayout.Size = new System.Drawing.Size(782, 547);
            this.@__CodexTabLayout.TabIndex = 0;
            // 
            // __GenerateCodexButton
            // 
            this.@__GenerateCodexButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.@__GenerateCodexButton.Location = new System.Drawing.Point(3, 3);
            this.@__GenerateCodexButton.Name = "__GenerateCodexButton";
            this.@__GenerateCodexButton.Size = new System.Drawing.Size(776, 19);
            this.@__GenerateCodexButton.TabIndex = 1;
            this.@__GenerateCodexButton.Text = "&Generate Codex";
            this.@__GenerateCodexButton.UseVisualStyleBackColor = true;
            this.@__GenerateCodexButton.Click += new System.EventHandler(this.@__GenerateCodexButton_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.@__CodexSettingsLayoutTable);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(788, 553);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Codex Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // __CodexSettingsLayoutTable
            // 
            this.@__CodexSettingsLayoutTable.ColumnCount = 1;
            this.@__CodexSettingsLayoutTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.@__CodexSettingsLayoutTable.Controls.Add(this.groupBox1, 0, 0);
            this.@__CodexSettingsLayoutTable.Controls.Add(this.@__Codex_SelectMethod, 0, 1);
            this.@__CodexSettingsLayoutTable.Controls.Add(this.@__Codex_UseRatings, 0, 2);
            this.@__CodexSettingsLayoutTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.@__CodexSettingsLayoutTable.Location = new System.Drawing.Point(3, 3);
            this.@__CodexSettingsLayoutTable.Name = "__CodexSettingsLayoutTable";
            this.@__CodexSettingsLayoutTable.RowCount = 4;
            this.@__CodexSettingsLayoutTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.@__CodexSettingsLayoutTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.@__CodexSettingsLayoutTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.@__CodexSettingsLayoutTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.@__CodexSettingsLayoutTable.Size = new System.Drawing.Size(782, 547);
            this.@__CodexSettingsLayoutTable.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.@__LoadRatingFilebutton);
            this.groupBox1.Controls.Add(this.@__GenerateRatingsFile);
            this.groupBox1.Controls.Add(this.@__Codex_Pool_PvEOnly);
            this.groupBox1.Controls.Add(this.@__Codex_Pool_EotN);
            this.groupBox1.Controls.Add(this.@__Codex_Pool_Nightfall);
            this.groupBox1.Controls.Add(this.@__Codex_Pool_Factions);
            this.groupBox1.Controls.Add(this.@__Codex_Pool_Prophecies);
            this.groupBox1.Controls.Add(this.@__Codex_Pool_Core);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 69);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Pool";
            // 
            // __LoadRatingFilebutton
            // 
            this.@__LoadRatingFilebutton.Location = new System.Drawing.Point(563, 38);
            this.@__LoadRatingFilebutton.Name = "__LoadRatingFilebutton";
            this.@__LoadRatingFilebutton.Size = new System.Drawing.Size(186, 23);
            this.@__LoadRatingFilebutton.TabIndex = 7;
            this.@__LoadRatingFilebutton.Text = "Load Ratings File";
            this.@__LoadRatingFilebutton.UseVisualStyleBackColor = true;
            this.@__LoadRatingFilebutton.Click += new System.EventHandler(this.@__LoadRatingFilebutton_Click);
            // 
            // __GenerateRatingsFile
            // 
            this.@__GenerateRatingsFile.Location = new System.Drawing.Point(563, 11);
            this.@__GenerateRatingsFile.Name = "__GenerateRatingsFile";
            this.@__GenerateRatingsFile.Size = new System.Drawing.Size(186, 23);
            this.@__GenerateRatingsFile.TabIndex = 6;
            this.@__GenerateRatingsFile.Text = "Generate Ratings File";
            this.@__GenerateRatingsFile.UseVisualStyleBackColor = true;
            this.@__GenerateRatingsFile.Click += new System.EventHandler(this.@__GenerateRatingsFile_Click);
            // 
            // __Codex_Pool_PvEOnly
            // 
            this.@__Codex_Pool_PvEOnly.AutoSize = true;
            this.@__Codex_Pool_PvEOnly.Checked = true;
            this.@__Codex_Pool_PvEOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.@__Codex_Pool_PvEOnly.Location = new System.Drawing.Point(275, 42);
            this.@__Codex_Pool_PvEOnly.Name = "__Codex_Pool_PvEOnly";
            this.@__Codex_Pool_PvEOnly.Size = new System.Drawing.Size(232, 17);
            this.@__Codex_Pool_PvEOnly.TabIndex = 5;
            this.@__Codex_Pool_PvEOnly.Text = "Allow PvE-Only Skills (across all campaigns)";
            this.@__Codex_Pool_PvEOnly.UseVisualStyleBackColor = true;
            // 
            // __Codex_Pool_EotN
            // 
            this.@__Codex_Pool_EotN.AutoSize = true;
            this.@__Codex_Pool_EotN.Checked = true;
            this.@__Codex_Pool_EotN.CheckState = System.Windows.Forms.CheckState.Checked;
            this.@__Codex_Pool_EotN.Location = new System.Drawing.Point(6, 42);
            this.@__Codex_Pool_EotN.Name = "__Codex_Pool_EotN";
            this.@__Codex_Pool_EotN.Size = new System.Drawing.Size(168, 17);
            this.@__Codex_Pool_EotN.TabIndex = 4;
            this.@__Codex_Pool_EotN.Text = "Include Eye of the North Skills";
            this.@__Codex_Pool_EotN.UseVisualStyleBackColor = true;
            // 
            // __Codex_Pool_Nightfall
            // 
            this.@__Codex_Pool_Nightfall.AutoSize = true;
            this.@__Codex_Pool_Nightfall.Checked = true;
            this.@__Codex_Pool_Nightfall.CheckState = System.Windows.Forms.CheckState.Checked;
            this.@__Codex_Pool_Nightfall.Location = new System.Drawing.Point(412, 19);
            this.@__Codex_Pool_Nightfall.Name = "__Codex_Pool_Nightfall";
            this.@__Codex_Pool_Nightfall.Size = new System.Drawing.Size(129, 17);
            this.@__Codex_Pool_Nightfall.TabIndex = 3;
            this.@__Codex_Pool_Nightfall.Text = "Include Nightfall Skills";
            this.@__Codex_Pool_Nightfall.UseVisualStyleBackColor = true;
            // 
            // __Codex_Pool_Factions
            // 
            this.@__Codex_Pool_Factions.AutoSize = true;
            this.@__Codex_Pool_Factions.Checked = true;
            this.@__Codex_Pool_Factions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.@__Codex_Pool_Factions.Location = new System.Drawing.Point(275, 19);
            this.@__Codex_Pool_Factions.Name = "__Codex_Pool_Factions";
            this.@__Codex_Pool_Factions.Size = new System.Drawing.Size(131, 17);
            this.@__Codex_Pool_Factions.TabIndex = 2;
            this.@__Codex_Pool_Factions.Text = "Include Factions Skills";
            this.@__Codex_Pool_Factions.UseVisualStyleBackColor = true;
            // 
            // __Codex_Pool_Prophecies
            // 
            this.@__Codex_Pool_Prophecies.AutoSize = true;
            this.@__Codex_Pool_Prophecies.Checked = true;
            this.@__Codex_Pool_Prophecies.CheckState = System.Windows.Forms.CheckState.Checked;
            this.@__Codex_Pool_Prophecies.Location = new System.Drawing.Point(125, 19);
            this.@__Codex_Pool_Prophecies.Name = "__Codex_Pool_Prophecies";
            this.@__Codex_Pool_Prophecies.Size = new System.Drawing.Size(144, 17);
            this.@__Codex_Pool_Prophecies.TabIndex = 1;
            this.@__Codex_Pool_Prophecies.Text = "Include Prophecies Skills";
            this.@__Codex_Pool_Prophecies.UseVisualStyleBackColor = true;
            // 
            // __Codex_Pool_Core
            // 
            this.@__Codex_Pool_Core.AutoSize = true;
            this.@__Codex_Pool_Core.Checked = true;
            this.@__Codex_Pool_Core.CheckState = System.Windows.Forms.CheckState.Checked;
            this.@__Codex_Pool_Core.Location = new System.Drawing.Point(6, 19);
            this.@__Codex_Pool_Core.Name = "__Codex_Pool_Core";
            this.@__Codex_Pool_Core.Size = new System.Drawing.Size(113, 17);
            this.@__Codex_Pool_Core.TabIndex = 0;
            this.@__Codex_Pool_Core.Text = "Include Core Skills";
            this.@__Codex_Pool_Core.UseVisualStyleBackColor = true;
            // 
            // __Codex_SelectMethod
            // 
            this.@__Codex_SelectMethod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.@__Codex_SelectMethod.FormattingEnabled = true;
            this.@__Codex_SelectMethod.Items.AddRange(new object[] {
            "Pure random selection",
            "Even skills across professions",
            "Even skills across attributes",
            "Non-elite skills even across attributes, elite skills even across professions",
            "Pure random within ratings (e.g. 20 random skills with a rating of 1, 50 with a r" +
                "ating of 2, etc.)"});
            this.@__Codex_SelectMethod.Location = new System.Drawing.Point(3, 78);
            this.@__Codex_SelectMethod.Name = "__Codex_SelectMethod";
            this.@__Codex_SelectMethod.Size = new System.Drawing.Size(776, 21);
            this.@__Codex_SelectMethod.TabIndex = 1;
            // 
            // __Codex_UseRatings
            // 
            this.@__Codex_UseRatings.AutoSize = true;
            this.@__Codex_UseRatings.Location = new System.Drawing.Point(3, 105);
            this.@__Codex_UseRatings.Name = "__Codex_UseRatings";
            this.@__Codex_UseRatings.Size = new System.Drawing.Size(435, 17);
            this.@__Codex_UseRatings.TabIndex = 2;
            this.@__Codex_UseRatings.Text = "Use Skill ratings to adjust probabilities for skills being selected in codex and " +
    "challenges.";
            this.@__Codex_UseRatings.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.AutoScroll = true;
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(788, 553);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Challenge Settings";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.@__HTMLSummary);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(788, 553);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "HTML Summary";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // __HTMLSummary
            // 
            this.@__HTMLSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.@__HTMLSummary.Location = new System.Drawing.Point(3, 3);
            this.@__HTMLSummary.Multiline = true;
            this.@__HTMLSummary.Name = "__HTMLSummary";
            this.@__HTMLSummary.ReadOnly = true;
            this.@__HTMLSummary.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.@__HTMLSummary.Size = new System.Drawing.Size(782, 547);
            this.@__HTMLSummary.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.@__TemplateCodeParserTest);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(788, 553);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Template Code Parser";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.@__CodexSkillInfo);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.@__Tabs);
            this.splitContainer1.Size = new System.Drawing.Size(800, 704);
            this.splitContainer1.SplitterDistance = 117;
            this.splitContainer1.TabIndex = 1;
            // 
            // __CodexSkillInfo
            // 
            this.@__CodexSkillInfo.BackColor = System.Drawing.Color.White;
            this.@__CodexSkillInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.@__CodexSkillInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.@__CodexSkillInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.@__CodexSkillInfo.Location = new System.Drawing.Point(0, 0);
            this.@__CodexSkillInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.@__CodexSkillInfo.Name = "__CodexSkillInfo";
            this.@__CodexSkillInfo.Size = new System.Drawing.Size(796, 113);
            this.@__CodexSkillInfo.TabIndex = 0;
            // 
            // __CodexSkillDisplay
            // 
            this.@__CodexSkillDisplay.BackColor = System.Drawing.SystemColors.ControlDark;
            this.@__CodexSkillDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.@__CodexSkillDisplay.Location = new System.Drawing.Point(3, 28);
            this.@__CodexSkillDisplay.Name = "__CodexSkillDisplay";
            this.@__CodexSkillDisplay.Size = new System.Drawing.Size(776, 516);
            this.@__CodexSkillDisplay.TabIndex = 2;
            // 
            // __TemplateCodeParserTest
            // 
            this.@__TemplateCodeParserTest.Location = new System.Drawing.Point(6, 6);
            this.@__TemplateCodeParserTest.Name = "__TemplateCodeParserTest";
            this.@__TemplateCodeParserTest.Size = new System.Drawing.Size(399, 130);
            this.@__TemplateCodeParserTest.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 704);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Guild Wars Codex & Challenges Generator";
            this.@__Tabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.@__CodexTabLayout.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.@__CodexSettingsLayoutTable.ResumeLayout(false);
            this.@__CodexSettingsLayoutTable.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl __Tabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel __CodexTabLayout;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button __GenerateCodexButton;
        private Skill_UI.SkillInfoDisplay __CodexSkillInfo;
        private Skill_UI.SkillDisplay __CodexSkillDisplay;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox __HTMLSummary;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel __CodexSettingsLayoutTable;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox __Codex_Pool_PvEOnly;
        private System.Windows.Forms.CheckBox __Codex_Pool_EotN;
        private System.Windows.Forms.CheckBox __Codex_Pool_Nightfall;
        private System.Windows.Forms.CheckBox __Codex_Pool_Factions;
        private System.Windows.Forms.CheckBox __Codex_Pool_Prophecies;
        private System.Windows.Forms.CheckBox __Codex_Pool_Core;
        private System.Windows.Forms.ComboBox __Codex_SelectMethod;
        private System.Windows.Forms.CheckBox __Codex_UseRatings;
        private System.Windows.Forms.Button __GenerateRatingsFile;
        private System.Windows.Forms.Button __LoadRatingFilebutton;
        private System.Windows.Forms.TabPage tabPage5;
        private TemplateReaderDisplay __TemplateCodeParserTest;
    }
}

