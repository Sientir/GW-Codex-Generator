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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.@__CodexSkillInfo = new GW_Codex_Generator.Skill_UI.SkillInfoDisplay();
            this.@__Tabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.@__CodexTabLayout = new System.Windows.Forms.TableLayoutPanel();
            this.@__CodexSkillDisplay = new GW_Codex_Generator.Skill_UI.SkillDisplay();
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
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.@__TemplateDraftUI = new GW_Codex_Generator.Template_Draft.TemplateDraftUI();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.@__HTMLSummary = new System.Windows.Forms.TextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.templateCodeDebugger1 = new GW_Codex_Generator.TemplateCodeDebugger();
            this.@__TemplateCodeParserTest = new GW_Codex_Generator.TemplateReaderDisplay();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.@__CompileTemplateFolderIntoFileButton = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.@__TemplateDraft_LoadChallengeTemplatesAsDeckButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.@__LoadTemplateDeckButton = new System.Windows.Forms.Button();
            this.@__ViewTemplateDeckButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.@__CurrentSetDescription = new System.Windows.Forms.TextBox();
            this.@__EditCurrentSkillSet = new System.Windows.Forms.Button();
            this.@__SetSelector = new System.Windows.Forms.ComboBox();
            this.@__Set_AddButton = new System.Windows.Forms.Button();
            this.@__SaveInternalIDsFile = new System.Windows.Forms.Button();
            this.@__Button_EditSkillRatingsFromBeginning = new System.Windows.Forms.Button();
            this.@__Button_EditRatings = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.@__Button_AssignRaritiesFromBeginning = new System.Windows.Forms.Button();
            this.@__Button_AssignRarities = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.@__Rarities = new System.Windows.Forms.TextBox();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.@__SkillBoosterLeagueUI = new GW_Codex_Generator.Skill_Boosters.SkillBoosterLeagueUI();
            this.@__OpenTemplateDeck = new System.Windows.Forms.OpenFileDialog();
            this.@__FolderSelectorForTemplatesToDeckFile = new System.Windows.Forms.FolderBrowserDialog();
            this.@__SaveDirectoryToDeckFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.@__Tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.@__CodexTabLayout.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.@__CodexSettingsLayoutTable.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.SuspendLayout();
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
            // __Tabs
            // 
            this.@__Tabs.Controls.Add(this.tabPage1);
            this.@__Tabs.Controls.Add(this.tabPage2);
            this.@__Tabs.Controls.Add(this.tabPage3);
            this.@__Tabs.Controls.Add(this.tabPage6);
            this.@__Tabs.Controls.Add(this.tabPage4);
            this.@__Tabs.Controls.Add(this.tabPage5);
            this.@__Tabs.Controls.Add(this.tabPage7);
            this.@__Tabs.Controls.Add(this.tabPage8);
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
            // __CodexSkillDisplay
            // 
            this.@__CodexSkillDisplay.BackColor = System.Drawing.SystemColors.ControlDark;
            this.@__CodexSkillDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.@__CodexSkillDisplay.Location = new System.Drawing.Point(3, 28);
            this.@__CodexSkillDisplay.Name = "__CodexSkillDisplay";
            this.@__CodexSkillDisplay.Size = new System.Drawing.Size(776, 516);
            this.@__CodexSkillDisplay.TabIndex = 2;
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
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.@__TemplateDraftUI);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(788, 553);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Template Draft";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // __TemplateDraftUI
            // 
            this.@__TemplateDraftUI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.@__TemplateDraftUI.Location = new System.Drawing.Point(3, 3);
            this.@__TemplateDraftUI.Name = "__TemplateDraftUI";
            this.@__TemplateDraftUI.Size = new System.Drawing.Size(782, 547);
            this.@__TemplateDraftUI.TabIndex = 0;
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
            this.tabPage5.Controls.Add(this.templateCodeDebugger1);
            this.tabPage5.Controls.Add(this.@__TemplateCodeParserTest);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(788, 553);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Template Code Parser";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // templateCodeDebugger1
            // 
            this.templateCodeDebugger1.Location = new System.Drawing.Point(6, 202);
            this.templateCodeDebugger1.Name = "templateCodeDebugger1";
            this.templateCodeDebugger1.Size = new System.Drawing.Size(454, 345);
            this.templateCodeDebugger1.TabIndex = 1;
            // 
            // __TemplateCodeParserTest
            // 
            this.@__TemplateCodeParserTest.Location = new System.Drawing.Point(6, 6);
            this.@__TemplateCodeParserTest.Name = "__TemplateCodeParserTest";
            this.@__TemplateCodeParserTest.Size = new System.Drawing.Size(524, 190);
            this.@__TemplateCodeParserTest.TabIndex = 0;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.@__CompileTemplateFolderIntoFileButton);
            this.tabPage7.Controls.Add(this.groupBox4);
            this.tabPage7.Controls.Add(this.groupBox3);
            this.tabPage7.Controls.Add(this.@__SaveInternalIDsFile);
            this.tabPage7.Controls.Add(this.@__Button_EditSkillRatingsFromBeginning);
            this.tabPage7.Controls.Add(this.@__Button_EditRatings);
            this.tabPage7.Controls.Add(this.groupBox2);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(788, 553);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "Settings";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // __CompileTemplateFolderIntoFileButton
            // 
            this.@__CompileTemplateFolderIntoFileButton.Location = new System.Drawing.Point(225, 5);
            this.@__CompileTemplateFolderIntoFileButton.Name = "__CompileTemplateFolderIntoFileButton";
            this.@__CompileTemplateFolderIntoFileButton.Size = new System.Drawing.Size(118, 67);
            this.@__CompileTemplateFolderIntoFileButton.TabIndex = 6;
            this.@__CompileTemplateFolderIntoFileButton.Text = "Compile Templates in folder into a Template Database/Deck";
            this.@__CompileTemplateFolderIntoFileButton.UseVisualStyleBackColor = true;
            this.@__CompileTemplateFolderIntoFileButton.Click += new System.EventHandler(this.@__CompileTemplateFolderIntoFileButton_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.@__TemplateDraft_LoadChallengeTemplatesAsDeckButton);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.@__LoadTemplateDeckButton);
            this.groupBox4.Controls.Add(this.@__ViewTemplateDeckButton);
            this.groupBox4.Location = new System.Drawing.Point(6, 332);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(325, 139);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Template Draft Info";
            // 
            // __TemplateDraft_LoadChallengeTemplatesAsDeckButton
            // 
            this.@__TemplateDraft_LoadChallengeTemplatesAsDeckButton.Location = new System.Drawing.Point(6, 77);
            this.@__TemplateDraft_LoadChallengeTemplatesAsDeckButton.Name = "__TemplateDraft_LoadChallengeTemplatesAsDeckButton";
            this.@__TemplateDraft_LoadChallengeTemplatesAsDeckButton.Size = new System.Drawing.Size(106, 56);
            this.@__TemplateDraft_LoadChallengeTemplatesAsDeckButton.TabIndex = 3;
            this.@__TemplateDraft_LoadChallengeTemplatesAsDeckButton.Text = "Load Challenge Templates As Deck";
            this.@__TemplateDraft_LoadChallengeTemplatesAsDeckButton.UseVisualStyleBackColor = true;
            this.@__TemplateDraft_LoadChallengeTemplatesAsDeckButton.Click += new System.EventHandler(this.@__TemplateDraft_LoadChallengeTemplatesAsDeckButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(130, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(180, 65);
            this.label3.TabIndex = 2;
            this.label3.Text = "Template draft decks consist of rows\r\nof templates, one per row, in the\r\nfollowin" +
    "g format:\r\n\r\nTemplate Name|Template_Code";
            // 
            // __LoadTemplateDeckButton
            // 
            this.@__LoadTemplateDeckButton.Location = new System.Drawing.Point(6, 48);
            this.@__LoadTemplateDeckButton.Name = "__LoadTemplateDeckButton";
            this.@__LoadTemplateDeckButton.Size = new System.Drawing.Size(106, 23);
            this.@__LoadTemplateDeckButton.TabIndex = 1;
            this.@__LoadTemplateDeckButton.Text = "Load Deck";
            this.@__LoadTemplateDeckButton.UseVisualStyleBackColor = true;
            this.@__LoadTemplateDeckButton.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // __ViewTemplateDeckButton
            // 
            this.@__ViewTemplateDeckButton.Location = new System.Drawing.Point(6, 19);
            this.@__ViewTemplateDeckButton.Name = "__ViewTemplateDeckButton";
            this.@__ViewTemplateDeckButton.Size = new System.Drawing.Size(106, 23);
            this.@__ViewTemplateDeckButton.TabIndex = 0;
            this.@__ViewTemplateDeckButton.Text = "View Current Deck";
            this.@__ViewTemplateDeckButton.UseVisualStyleBackColor = true;
            this.@__ViewTemplateDeckButton.Click += new System.EventHandler(this.@__ViewTemplateDeckButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.@__CurrentSetDescription);
            this.groupBox3.Controls.Add(this.@__EditCurrentSkillSet);
            this.groupBox3.Controls.Add(this.@__SetSelector);
            this.groupBox3.Controls.Add(this.@__Set_AddButton);
            this.groupBox3.Location = new System.Drawing.Point(159, 90);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(320, 236);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Define Sets";
            // 
            // __CurrentSetDescription
            // 
            this.@__CurrentSetDescription.Location = new System.Drawing.Point(6, 75);
            this.@__CurrentSetDescription.Multiline = true;
            this.@__CurrentSetDescription.Name = "__CurrentSetDescription";
            this.@__CurrentSetDescription.ReadOnly = true;
            this.@__CurrentSetDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.@__CurrentSetDescription.Size = new System.Drawing.Size(308, 155);
            this.@__CurrentSetDescription.TabIndex = 3;
            // 
            // __EditCurrentSkillSet
            // 
            this.@__EditCurrentSkillSet.Enabled = false;
            this.@__EditCurrentSkillSet.Location = new System.Drawing.Point(6, 46);
            this.@__EditCurrentSkillSet.Name = "__EditCurrentSkillSet";
            this.@__EditCurrentSkillSet.Size = new System.Drawing.Size(308, 23);
            this.@__EditCurrentSkillSet.TabIndex = 2;
            this.@__EditCurrentSkillSet.Text = "Edit Selected Set...";
            this.@__EditCurrentSkillSet.UseVisualStyleBackColor = true;
            this.@__EditCurrentSkillSet.Click += new System.EventHandler(this.@__EditCurrentSkillSet_Click);
            // 
            // __SetSelector
            // 
            this.@__SetSelector.FormattingEnabled = true;
            this.@__SetSelector.Location = new System.Drawing.Point(6, 19);
            this.@__SetSelector.Name = "__SetSelector";
            this.@__SetSelector.Size = new System.Drawing.Size(266, 21);
            this.@__SetSelector.TabIndex = 1;
            this.@__SetSelector.Text = "Select Set to Edit";
            this.@__SetSelector.SelectedIndexChanged += new System.EventHandler(this.@__SetSelector_SelectedIndexChanged);
            // 
            // __Set_AddButton
            // 
            this.@__Set_AddButton.Location = new System.Drawing.Point(278, 19);
            this.@__Set_AddButton.Name = "__Set_AddButton";
            this.@__Set_AddButton.Size = new System.Drawing.Size(36, 23);
            this.@__Set_AddButton.TabIndex = 0;
            this.@__Set_AddButton.Text = "Add";
            this.@__Set_AddButton.UseVisualStyleBackColor = true;
            this.@__Set_AddButton.Click += new System.EventHandler(this.@__Set_AddButton_Click);
            // 
            // __SaveInternalIDsFile
            // 
            this.@__SaveInternalIDsFile.Location = new System.Drawing.Point(129, 5);
            this.@__SaveInternalIDsFile.Name = "__SaveInternalIDsFile";
            this.@__SaveInternalIDsFile.Size = new System.Drawing.Size(90, 67);
            this.@__SaveInternalIDsFile.TabIndex = 3;
            this.@__SaveInternalIDsFile.Text = "Save file containing internal skill IDs.";
            this.@__SaveInternalIDsFile.UseVisualStyleBackColor = true;
            this.@__SaveInternalIDsFile.Click += new System.EventHandler(this.@__SaveInternalIDsFile_Click);
            // 
            // __Button_EditSkillRatingsFromBeginning
            // 
            this.@__Button_EditSkillRatingsFromBeginning.Location = new System.Drawing.Point(6, 34);
            this.@__Button_EditSkillRatingsFromBeginning.Name = "__Button_EditSkillRatingsFromBeginning";
            this.@__Button_EditSkillRatingsFromBeginning.Size = new System.Drawing.Size(117, 38);
            this.@__Button_EditSkillRatingsFromBeginning.TabIndex = 2;
            this.@__Button_EditSkillRatingsFromBeginning.Text = "Edit Skill Ratings From Beginning";
            this.@__Button_EditSkillRatingsFromBeginning.UseVisualStyleBackColor = true;
            this.@__Button_EditSkillRatingsFromBeginning.Click += new System.EventHandler(this.button1_Click);
            // 
            // __Button_EditRatings
            // 
            this.@__Button_EditRatings.Location = new System.Drawing.Point(6, 5);
            this.@__Button_EditRatings.Name = "__Button_EditRatings";
            this.@__Button_EditRatings.Size = new System.Drawing.Size(117, 23);
            this.@__Button_EditRatings.TabIndex = 1;
            this.@__Button_EditRatings.Text = "Edit Skill Ratings";
            this.@__Button_EditRatings.UseVisualStyleBackColor = true;
            this.@__Button_EditRatings.Click += new System.EventHandler(this.@__Button_EditRatings_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.@__Button_AssignRaritiesFromBeginning);
            this.groupBox2.Controls.Add(this.@__Button_AssignRarities);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.@__Rarities);
            this.groupBox2.Location = new System.Drawing.Point(6, 90);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(147, 236);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Edit Rarities for Boosters";
            // 
            // __Button_AssignRaritiesFromBeginning
            // 
            this.@__Button_AssignRaritiesFromBeginning.Location = new System.Drawing.Point(6, 48);
            this.@__Button_AssignRaritiesFromBeginning.Name = "__Button_AssignRaritiesFromBeginning";
            this.@__Button_AssignRaritiesFromBeginning.Size = new System.Drawing.Size(135, 40);
            this.@__Button_AssignRaritiesFromBeginning.TabIndex = 3;
            this.@__Button_AssignRaritiesFromBeginning.Text = "Assign Rarities From Beginning";
            this.@__Button_AssignRaritiesFromBeginning.UseVisualStyleBackColor = true;
            this.@__Button_AssignRaritiesFromBeginning.Click += new System.EventHandler(this.@__Button_AssignRaritiesFromBeginning_Click);
            // 
            // __Button_AssignRarities
            // 
            this.@__Button_AssignRarities.Location = new System.Drawing.Point(6, 19);
            this.@__Button_AssignRarities.Name = "__Button_AssignRarities";
            this.@__Button_AssignRarities.Size = new System.Drawing.Size(135, 23);
            this.@__Button_AssignRarities.TabIndex = 2;
            this.@__Button_AssignRarities.Text = "Assign Rarities";
            this.@__Button_AssignRarities.UseVisualStyleBackColor = true;
            this.@__Button_AssignRarities.Click += new System.EventHandler(this.@__Button_AssignRarities_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 39);
            this.label2.TabIndex = 1;
            this.label2.Text = "Rarities, one per line\r\nAssign rarities to save\r\nrarity labels.";
            // 
            // __Rarities
            // 
            this.@__Rarities.Location = new System.Drawing.Point(6, 133);
            this.@__Rarities.Multiline = true;
            this.@__Rarities.Name = "__Rarities";
            this.@__Rarities.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.@__Rarities.Size = new System.Drawing.Size(135, 97);
            this.@__Rarities.TabIndex = 0;
            this.@__Rarities.Text = "Common\r\nUncommon\r\nRare";
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.@__SkillBoosterLeagueUI);
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(788, 553);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "Skill Booster League";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // __SkillBoosterLeagueUI
            // 
            this.@__SkillBoosterLeagueUI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.@__SkillBoosterLeagueUI.Location = new System.Drawing.Point(0, 0);
            this.@__SkillBoosterLeagueUI.Name = "__SkillBoosterLeagueUI";
            this.@__SkillBoosterLeagueUI.Size = new System.Drawing.Size(788, 553);
            this.@__SkillBoosterLeagueUI.TabIndex = 0;
            // 
            // __OpenTemplateDeck
            // 
            this.@__OpenTemplateDeck.DefaultExt = "txt";
            this.@__OpenTemplateDeck.Filter = "Text Documents|*.txt";
            this.@__OpenTemplateDeck.Title = "Open Template Deck Document";
            this.@__OpenTemplateDeck.FileOk += new System.ComponentModel.CancelEventHandler(this.@__OpenTemplateDeck_FileOk);
            // 
            // __SaveDirectoryToDeckFileDialog
            // 
            this.@__SaveDirectoryToDeckFileDialog.DefaultExt = "txt";
            this.@__SaveDirectoryToDeckFileDialog.Filter = "Text Document|*.txt";
            this.@__SaveDirectoryToDeckFileDialog.Title = "Save Directory As File Named...";
            this.@__SaveDirectoryToDeckFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.@__SaveDirectoryToDeckFileDialog_FileOk);
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
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.@__Tabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.@__CodexTabLayout.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.@__CodexSettingsLayoutTable.ResumeLayout(false);
            this.@__CodexSettingsLayoutTable.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage8.ResumeLayout(false);
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
        private TemplateCodeDebugger templateCodeDebugger1;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.Button __Button_EditRatings;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button __Button_EditSkillRatingsFromBeginning;
        private System.Windows.Forms.TextBox __Rarities;
        private System.Windows.Forms.Button __Button_AssignRaritiesFromBeginning;
        private System.Windows.Forms.Button __Button_AssignRarities;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button __SaveInternalIDsFile;
        private Skill_Boosters.SkillBoosterLeagueUI __SkillBoosterLeagueUI;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox __SetSelector;
        private System.Windows.Forms.Button __Set_AddButton;
        private System.Windows.Forms.TextBox __CurrentSetDescription;
        private System.Windows.Forms.Button __EditCurrentSkillSet;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button __ViewTemplateDeckButton;
        private System.Windows.Forms.Button __TemplateDraft_LoadChallengeTemplatesAsDeckButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button __LoadTemplateDeckButton;
        private System.Windows.Forms.OpenFileDialog __OpenTemplateDeck;
        private System.Windows.Forms.FolderBrowserDialog __FolderSelectorForTemplatesToDeckFile;
        private System.Windows.Forms.SaveFileDialog __SaveDirectoryToDeckFileDialog;
        private System.Windows.Forms.Button __CompileTemplateFolderIntoFileButton;
        private Template_Draft.TemplateDraftUI __TemplateDraftUI;
    }
}

