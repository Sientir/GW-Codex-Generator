namespace GW_Codex_Generator.Skill_Boosters
{
    partial class SkillSetEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SkillSetEditor));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.@__Select_RatingsList = new System.Windows.Forms.ComboBox();
            this.@__Select_Elites = new System.Windows.Forms.Button();
            this.@__Select_ProfessionList = new System.Windows.Forms.ComboBox();
            this.@__Select_AttributeList = new System.Windows.Forms.ComboBox();
            this.@__Select_NonElites = new System.Windows.Forms.Button();
            this.@__Select_Rating = new System.Windows.Forms.Button();
            this.@__Select_Profession = new System.Windows.Forms.Button();
            this.@__Select_Attribute = new System.Windows.Forms.Button();
            this.@__Select_All = new System.Windows.Forms.Button();
            this.@__Select_EyeOfTheNorth = new System.Windows.Forms.Button();
            this.@__Select_Nightfall = new System.Windows.Forms.Button();
            this.@__Select_Factions = new System.Windows.Forms.Button();
            this.@__Select_Prophecies = new System.Windows.Forms.Button();
            this.@__Select_Core = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.@__Deselect_RatingList = new System.Windows.Forms.ComboBox();
            this.@__Deselect_Elites = new System.Windows.Forms.Button();
            this.@__Deselect_ProfessionList = new System.Windows.Forms.ComboBox();
            this.@__Deselect_AttributeList = new System.Windows.Forms.ComboBox();
            this.@__Deselect_NonElites = new System.Windows.Forms.Button();
            this.@__Deselect_Rating = new System.Windows.Forms.Button();
            this.@__Deselect_Profession = new System.Windows.Forms.Button();
            this.@__Deselect_Attribute = new System.Windows.Forms.Button();
            this.@__Deselect_All = new System.Windows.Forms.Button();
            this.@__Deselect_EotN = new System.Windows.Forms.Button();
            this.@__Deselect_Nightfall = new System.Windows.Forms.Button();
            this.@__Deselect_Factions = new System.Windows.Forms.Button();
            this.@__Deselect_Prophecies = new System.Windows.Forms.Button();
            this.@__Deselect_Core = new System.Windows.Forms.Button();
            this.@__SetName = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.@__Apply_And_Close = new System.Windows.Forms.Button();
            this.@__SetStats = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.@__RarityDistribution = new System.Windows.Forms.TableLayoutPanel();
            this.@__SkillInfoDisplay = new GW_Codex_Generator.Skill_UI.SkillInfoDisplay();
            this.@__SkillSetDisplay = new GW_Codex_Generator.Skill_Boosters.SkillBoosterSetUI();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.@__Select_RatingsList);
            this.groupBox1.Controls.Add(this.@__Select_Elites);
            this.groupBox1.Controls.Add(this.@__Select_ProfessionList);
            this.groupBox1.Controls.Add(this.@__Select_AttributeList);
            this.groupBox1.Controls.Add(this.@__Select_NonElites);
            this.groupBox1.Controls.Add(this.@__Select_Rating);
            this.groupBox1.Controls.Add(this.@__Select_Profession);
            this.groupBox1.Controls.Add(this.@__Select_Attribute);
            this.groupBox1.Controls.Add(this.@__Select_All);
            this.groupBox1.Controls.Add(this.@__Select_EyeOfTheNorth);
            this.groupBox1.Controls.Add(this.@__Select_Nightfall);
            this.groupBox1.Controls.Add(this.@__Select_Factions);
            this.groupBox1.Controls.Add(this.@__Select_Prophecies);
            this.groupBox1.Controls.Add(this.@__Select_Core);
            this.groupBox1.Location = new System.Drawing.Point(12, 130);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(500, 112);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select...";
            // 
            // __Select_RatingsList
            // 
            this.@__Select_RatingsList.FormattingEnabled = true;
            this.@__Select_RatingsList.Location = new System.Drawing.Point(87, 75);
            this.@__Select_RatingsList.Name = "__Select_RatingsList";
            this.@__Select_RatingsList.Size = new System.Drawing.Size(187, 21);
            this.@__Select_RatingsList.TabIndex = 13;
            // 
            // __Select_Elites
            // 
            this.@__Select_Elites.Location = new System.Drawing.Point(280, 77);
            this.@__Select_Elites.Name = "__Select_Elites";
            this.@__Select_Elites.Size = new System.Drawing.Size(75, 23);
            this.@__Select_Elites.TabIndex = 12;
            this.@__Select_Elites.Text = "Elites";
            this.@__Select_Elites.UseVisualStyleBackColor = true;
            this.@__Select_Elites.Click += new System.EventHandler(this.@__Select_Elites_Click);
            // 
            // __Select_ProfessionList
            // 
            this.@__Select_ProfessionList.FormattingEnabled = true;
            this.@__Select_ProfessionList.Items.AddRange(new object[] {
            "Warrior",
            "Mesmer",
            "Ranger",
            "Monk",
            "Necromancer",
            "Elementalist",
            "Assassin",
            "Ritualist",
            "Dervish",
            "Paragon",
            "None"});
            this.@__Select_ProfessionList.Location = new System.Drawing.Point(365, 48);
            this.@__Select_ProfessionList.Name = "__Select_ProfessionList";
            this.@__Select_ProfessionList.Size = new System.Drawing.Size(121, 21);
            this.@__Select_ProfessionList.TabIndex = 11;
            // 
            // __Select_AttributeList
            // 
            this.@__Select_AttributeList.FormattingEnabled = true;
            this.@__Select_AttributeList.Items.AddRange(new object[] {
            "Axe Mastery",
            "Hammer Mastery",
            "Strength",
            "Swordsmanship",
            "Tactics",
            "Domination Magic",
            "Fast Casting",
            "Illusion Magic",
            "Inspiration Magic",
            "Earth Prayers",
            "Mysticism",
            "Scythe Mastery",
            "Wind Prayers",
            "Command",
            "Leadership",
            "Motivation",
            "Spear Mastery",
            "Beast Mastery",
            "Expertise",
            "Marksmanship",
            "Wilderness Survival",
            "Critical Strikes",
            "Dagger Mastery",
            "Deadly Arts",
            "Shadow Arts",
            "Divine Favor",
            "Healing Prayers",
            "Protection Prayers",
            "Smiting Prayers",
            "Blood Magic",
            "Curses",
            "Death Magic",
            "Soul Reaping",
            "Air Magic",
            "Earth Magic",
            "Energy Storage",
            "Fire Magic",
            "Water Magic",
            "Channeling Magic",
            "Communing",
            "Restoration Magic",
            "Spawning Power",
            "PvE-Only Skills",
            "No Attribute"});
            this.@__Select_AttributeList.Location = new System.Drawing.Point(87, 48);
            this.@__Select_AttributeList.Name = "__Select_AttributeList";
            this.@__Select_AttributeList.Size = new System.Drawing.Size(187, 21);
            this.@__Select_AttributeList.TabIndex = 10;
            // 
            // __Select_NonElites
            // 
            this.@__Select_NonElites.Location = new System.Drawing.Point(361, 77);
            this.@__Select_NonElites.Name = "__Select_NonElites";
            this.@__Select_NonElites.Size = new System.Drawing.Size(75, 23);
            this.@__Select_NonElites.TabIndex = 9;
            this.@__Select_NonElites.Text = "non-Elites";
            this.@__Select_NonElites.UseVisualStyleBackColor = true;
            this.@__Select_NonElites.Click += new System.EventHandler(this.@__Select_NonElites_Click);
            // 
            // __Select_Rating
            // 
            this.@__Select_Rating.Location = new System.Drawing.Point(6, 77);
            this.@__Select_Rating.Name = "__Select_Rating";
            this.@__Select_Rating.Size = new System.Drawing.Size(75, 23);
            this.@__Select_Rating.TabIndex = 8;
            this.@__Select_Rating.Text = "Rating →";
            this.@__Select_Rating.UseVisualStyleBackColor = true;
            this.@__Select_Rating.Click += new System.EventHandler(this.@__Select_Rating_Click);
            // 
            // __Select_Profession
            // 
            this.@__Select_Profession.Location = new System.Drawing.Point(280, 48);
            this.@__Select_Profession.Name = "__Select_Profession";
            this.@__Select_Profession.Size = new System.Drawing.Size(79, 23);
            this.@__Select_Profession.TabIndex = 7;
            this.@__Select_Profession.Text = "Profession →";
            this.@__Select_Profession.UseVisualStyleBackColor = true;
            this.@__Select_Profession.Click += new System.EventHandler(this.@__Select_Profession_Click);
            // 
            // __Select_Attribute
            // 
            this.@__Select_Attribute.Location = new System.Drawing.Point(6, 48);
            this.@__Select_Attribute.Name = "__Select_Attribute";
            this.@__Select_Attribute.Size = new System.Drawing.Size(75, 23);
            this.@__Select_Attribute.TabIndex = 6;
            this.@__Select_Attribute.Text = "Attribute →";
            this.@__Select_Attribute.UseVisualStyleBackColor = true;
            this.@__Select_Attribute.Click += new System.EventHandler(this.@__Select_Attribute_Click);
            // 
            // __Select_All
            // 
            this.@__Select_All.Location = new System.Drawing.Point(411, 19);
            this.@__Select_All.Name = "__Select_All";
            this.@__Select_All.Size = new System.Drawing.Size(75, 23);
            this.@__Select_All.TabIndex = 5;
            this.@__Select_All.Text = "All";
            this.@__Select_All.UseVisualStyleBackColor = true;
            this.@__Select_All.Click += new System.EventHandler(this.@__Select_All_Click);
            // 
            // __Select_EyeOfTheNorth
            // 
            this.@__Select_EyeOfTheNorth.Location = new System.Drawing.Point(330, 19);
            this.@__Select_EyeOfTheNorth.Name = "__Select_EyeOfTheNorth";
            this.@__Select_EyeOfTheNorth.Size = new System.Drawing.Size(75, 23);
            this.@__Select_EyeOfTheNorth.TabIndex = 4;
            this.@__Select_EyeOfTheNorth.Text = "EotN";
            this.@__Select_EyeOfTheNorth.UseVisualStyleBackColor = true;
            this.@__Select_EyeOfTheNorth.Click += new System.EventHandler(this.@__Select_EyeOfTheNorth_Click);
            // 
            // __Select_Nightfall
            // 
            this.@__Select_Nightfall.Location = new System.Drawing.Point(249, 19);
            this.@__Select_Nightfall.Name = "__Select_Nightfall";
            this.@__Select_Nightfall.Size = new System.Drawing.Size(75, 23);
            this.@__Select_Nightfall.TabIndex = 3;
            this.@__Select_Nightfall.Text = "Nightfall";
            this.@__Select_Nightfall.UseVisualStyleBackColor = true;
            this.@__Select_Nightfall.Click += new System.EventHandler(this.@__Select_Nightfall_Click);
            // 
            // __Select_Factions
            // 
            this.@__Select_Factions.Location = new System.Drawing.Point(168, 19);
            this.@__Select_Factions.Name = "__Select_Factions";
            this.@__Select_Factions.Size = new System.Drawing.Size(75, 23);
            this.@__Select_Factions.TabIndex = 2;
            this.@__Select_Factions.Text = "Factions";
            this.@__Select_Factions.UseVisualStyleBackColor = true;
            this.@__Select_Factions.Click += new System.EventHandler(this.@__Select_Factions_Click);
            // 
            // __Select_Prophecies
            // 
            this.@__Select_Prophecies.Location = new System.Drawing.Point(87, 19);
            this.@__Select_Prophecies.Name = "__Select_Prophecies";
            this.@__Select_Prophecies.Size = new System.Drawing.Size(75, 23);
            this.@__Select_Prophecies.TabIndex = 1;
            this.@__Select_Prophecies.Text = "Prophecies";
            this.@__Select_Prophecies.UseVisualStyleBackColor = true;
            this.@__Select_Prophecies.Click += new System.EventHandler(this.@__Select_Prophecies_Click);
            // 
            // __Select_Core
            // 
            this.@__Select_Core.Location = new System.Drawing.Point(6, 19);
            this.@__Select_Core.Name = "__Select_Core";
            this.@__Select_Core.Size = new System.Drawing.Size(75, 23);
            this.@__Select_Core.TabIndex = 0;
            this.@__Select_Core.Text = "Core";
            this.@__Select_Core.UseVisualStyleBackColor = true;
            this.@__Select_Core.Click += new System.EventHandler(this.@__Select_Core_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.@__Deselect_RatingList);
            this.groupBox2.Controls.Add(this.@__Deselect_Elites);
            this.groupBox2.Controls.Add(this.@__Deselect_ProfessionList);
            this.groupBox2.Controls.Add(this.@__Deselect_AttributeList);
            this.groupBox2.Controls.Add(this.@__Deselect_NonElites);
            this.groupBox2.Controls.Add(this.@__Deselect_Rating);
            this.groupBox2.Controls.Add(this.@__Deselect_Profession);
            this.groupBox2.Controls.Add(this.@__Deselect_Attribute);
            this.groupBox2.Controls.Add(this.@__Deselect_All);
            this.groupBox2.Controls.Add(this.@__Deselect_EotN);
            this.groupBox2.Controls.Add(this.@__Deselect_Nightfall);
            this.groupBox2.Controls.Add(this.@__Deselect_Factions);
            this.groupBox2.Controls.Add(this.@__Deselect_Prophecies);
            this.groupBox2.Controls.Add(this.@__Deselect_Core);
            this.groupBox2.Location = new System.Drawing.Point(518, 130);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(500, 112);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Deselect...";
            // 
            // __Deselect_RatingList
            // 
            this.@__Deselect_RatingList.FormattingEnabled = true;
            this.@__Deselect_RatingList.Location = new System.Drawing.Point(87, 75);
            this.@__Deselect_RatingList.Name = "__Deselect_RatingList";
            this.@__Deselect_RatingList.Size = new System.Drawing.Size(187, 21);
            this.@__Deselect_RatingList.TabIndex = 13;
            // 
            // __Deselect_Elites
            // 
            this.@__Deselect_Elites.Location = new System.Drawing.Point(280, 77);
            this.@__Deselect_Elites.Name = "__Deselect_Elites";
            this.@__Deselect_Elites.Size = new System.Drawing.Size(75, 23);
            this.@__Deselect_Elites.TabIndex = 12;
            this.@__Deselect_Elites.Text = "Elites";
            this.@__Deselect_Elites.UseVisualStyleBackColor = true;
            this.@__Deselect_Elites.Click += new System.EventHandler(this.@__Deselect_Elites_Click);
            // 
            // __Deselect_ProfessionList
            // 
            this.@__Deselect_ProfessionList.FormattingEnabled = true;
            this.@__Deselect_ProfessionList.Items.AddRange(new object[] {
            "Warrior",
            "Mesmer",
            "Ranger",
            "Monk",
            "Necromancer",
            "Elementalist",
            "Assassin",
            "Ritualist",
            "Dervish",
            "Paragon",
            "None"});
            this.@__Deselect_ProfessionList.Location = new System.Drawing.Point(365, 48);
            this.@__Deselect_ProfessionList.Name = "__Deselect_ProfessionList";
            this.@__Deselect_ProfessionList.Size = new System.Drawing.Size(121, 21);
            this.@__Deselect_ProfessionList.TabIndex = 11;
            // 
            // __Deselect_AttributeList
            // 
            this.@__Deselect_AttributeList.FormattingEnabled = true;
            this.@__Deselect_AttributeList.Items.AddRange(new object[] {
            "Axe Mastery",
            "Hammer Mastery",
            "Strength",
            "Swordsmanship",
            "Tactics",
            "Domination Magic",
            "Fast Casting",
            "Illusion Magic",
            "Inspiration Magic",
            "Earth Prayers",
            "Mysticism",
            "Scythe Mastery",
            "Wind Prayers",
            "Command",
            "Leadership",
            "Motivation",
            "Spear Mastery",
            "Beast Mastery",
            "Expertise",
            "Marksmanship",
            "Wilderness Survival",
            "Critical Strikes",
            "Dagger Mastery",
            "Deadly Arts",
            "Shadow Arts",
            "Divine Favor",
            "Healing Prayers",
            "Protection Prayers",
            "Smiting Prayers",
            "Blood Magic",
            "Curses",
            "Death Magic",
            "Soul Reaping",
            "Air Magic",
            "Earth Magic",
            "Energy Storage",
            "Fire Magic",
            "Water Magic",
            "Channeling Magic",
            "Communing",
            "Restoration Magic",
            "Spawning Power",
            "PvE-Only Skills",
            "No Attribute"});
            this.@__Deselect_AttributeList.Location = new System.Drawing.Point(87, 48);
            this.@__Deselect_AttributeList.Name = "__Deselect_AttributeList";
            this.@__Deselect_AttributeList.Size = new System.Drawing.Size(187, 21);
            this.@__Deselect_AttributeList.TabIndex = 10;
            // 
            // __Deselect_NonElites
            // 
            this.@__Deselect_NonElites.Location = new System.Drawing.Point(361, 77);
            this.@__Deselect_NonElites.Name = "__Deselect_NonElites";
            this.@__Deselect_NonElites.Size = new System.Drawing.Size(75, 23);
            this.@__Deselect_NonElites.TabIndex = 9;
            this.@__Deselect_NonElites.Text = "non-Elites";
            this.@__Deselect_NonElites.UseVisualStyleBackColor = true;
            this.@__Deselect_NonElites.Click += new System.EventHandler(this.@__Deselect_NonElites_Click);
            // 
            // __Deselect_Rating
            // 
            this.@__Deselect_Rating.Location = new System.Drawing.Point(6, 77);
            this.@__Deselect_Rating.Name = "__Deselect_Rating";
            this.@__Deselect_Rating.Size = new System.Drawing.Size(75, 23);
            this.@__Deselect_Rating.TabIndex = 8;
            this.@__Deselect_Rating.Text = "Rating →";
            this.@__Deselect_Rating.UseVisualStyleBackColor = true;
            this.@__Deselect_Rating.Click += new System.EventHandler(this.@__Deselect_Rating_Click);
            // 
            // __Deselect_Profession
            // 
            this.@__Deselect_Profession.Location = new System.Drawing.Point(280, 48);
            this.@__Deselect_Profession.Name = "__Deselect_Profession";
            this.@__Deselect_Profession.Size = new System.Drawing.Size(79, 23);
            this.@__Deselect_Profession.TabIndex = 7;
            this.@__Deselect_Profession.Text = "Profession →";
            this.@__Deselect_Profession.UseVisualStyleBackColor = true;
            this.@__Deselect_Profession.Click += new System.EventHandler(this.@__Deselect_Profession_Click);
            // 
            // __Deselect_Attribute
            // 
            this.@__Deselect_Attribute.Location = new System.Drawing.Point(6, 48);
            this.@__Deselect_Attribute.Name = "__Deselect_Attribute";
            this.@__Deselect_Attribute.Size = new System.Drawing.Size(75, 23);
            this.@__Deselect_Attribute.TabIndex = 6;
            this.@__Deselect_Attribute.Text = "Attribute →";
            this.@__Deselect_Attribute.UseVisualStyleBackColor = true;
            this.@__Deselect_Attribute.Click += new System.EventHandler(this.@__Deselect_Attribute_Click);
            // 
            // __Deselect_All
            // 
            this.@__Deselect_All.Location = new System.Drawing.Point(411, 19);
            this.@__Deselect_All.Name = "__Deselect_All";
            this.@__Deselect_All.Size = new System.Drawing.Size(75, 23);
            this.@__Deselect_All.TabIndex = 5;
            this.@__Deselect_All.Text = "All";
            this.@__Deselect_All.UseVisualStyleBackColor = true;
            this.@__Deselect_All.Click += new System.EventHandler(this.@__Deselect_All_Click);
            // 
            // __Deselect_EotN
            // 
            this.@__Deselect_EotN.Location = new System.Drawing.Point(330, 19);
            this.@__Deselect_EotN.Name = "__Deselect_EotN";
            this.@__Deselect_EotN.Size = new System.Drawing.Size(75, 23);
            this.@__Deselect_EotN.TabIndex = 4;
            this.@__Deselect_EotN.Text = "EotN";
            this.@__Deselect_EotN.UseVisualStyleBackColor = true;
            this.@__Deselect_EotN.Click += new System.EventHandler(this.@__Deselect_EotN_Click);
            // 
            // __Deselect_Nightfall
            // 
            this.@__Deselect_Nightfall.Location = new System.Drawing.Point(249, 19);
            this.@__Deselect_Nightfall.Name = "__Deselect_Nightfall";
            this.@__Deselect_Nightfall.Size = new System.Drawing.Size(75, 23);
            this.@__Deselect_Nightfall.TabIndex = 3;
            this.@__Deselect_Nightfall.Text = "Nightfall";
            this.@__Deselect_Nightfall.UseVisualStyleBackColor = true;
            this.@__Deselect_Nightfall.Click += new System.EventHandler(this.@__Deselect_Nightfall_Click);
            // 
            // __Deselect_Factions
            // 
            this.@__Deselect_Factions.Location = new System.Drawing.Point(168, 19);
            this.@__Deselect_Factions.Name = "__Deselect_Factions";
            this.@__Deselect_Factions.Size = new System.Drawing.Size(75, 23);
            this.@__Deselect_Factions.TabIndex = 2;
            this.@__Deselect_Factions.Text = "Factions";
            this.@__Deselect_Factions.UseVisualStyleBackColor = true;
            this.@__Deselect_Factions.Click += new System.EventHandler(this.@__Deselect_Factions_Click);
            // 
            // __Deselect_Prophecies
            // 
            this.@__Deselect_Prophecies.Location = new System.Drawing.Point(87, 19);
            this.@__Deselect_Prophecies.Name = "__Deselect_Prophecies";
            this.@__Deselect_Prophecies.Size = new System.Drawing.Size(75, 23);
            this.@__Deselect_Prophecies.TabIndex = 1;
            this.@__Deselect_Prophecies.Text = "Prophecies";
            this.@__Deselect_Prophecies.UseVisualStyleBackColor = true;
            this.@__Deselect_Prophecies.Click += new System.EventHandler(this.@__Deselect_Prophecies_Click);
            // 
            // __Deselect_Core
            // 
            this.@__Deselect_Core.Location = new System.Drawing.Point(6, 19);
            this.@__Deselect_Core.Name = "__Deselect_Core";
            this.@__Deselect_Core.Size = new System.Drawing.Size(75, 23);
            this.@__Deselect_Core.TabIndex = 0;
            this.@__Deselect_Core.Text = "Core";
            this.@__Deselect_Core.UseVisualStyleBackColor = true;
            this.@__Deselect_Core.Click += new System.EventHandler(this.@__Deselect_Core_Click);
            // 
            // __SetName
            // 
            this.@__SetName.Location = new System.Drawing.Point(47, 19);
            this.@__SetName.Name = "__SetName";
            this.@__SetName.Size = new System.Drawing.Size(198, 20);
            this.@__SetName.TabIndex = 15;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.@__RarityDistribution);
            this.groupBox3.Controls.Add(this.@__Apply_And_Close);
            this.groupBox3.Controls.Add(this.@__SetStats);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.@__SetName);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(341, 112);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Information";
            // 
            // __Apply_And_Close
            // 
            this.@__Apply_And_Close.Location = new System.Drawing.Point(251, 17);
            this.@__Apply_And_Close.Name = "__Apply_And_Close";
            this.@__Apply_And_Close.Size = new System.Drawing.Size(84, 23);
            this.@__Apply_And_Close.TabIndex = 14;
            this.@__Apply_And_Close.Text = "Apply && Close";
            this.@__Apply_And_Close.UseVisualStyleBackColor = true;
            this.@__Apply_And_Close.Click += new System.EventHandler(this.@__Apply_And_Close_Click);
            // 
            // __SetStats
            // 
            this.@__SetStats.Location = new System.Drawing.Point(6, 45);
            this.@__SetStats.Multiline = true;
            this.@__SetStats.Name = "__SetStats";
            this.@__SetStats.ReadOnly = true;
            this.@__SetStats.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.@__SetStats.Size = new System.Drawing.Size(133, 61);
            this.@__SetStats.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Name";
            // 
            // __RarityDistribution
            // 
            this.@__RarityDistribution.AutoScroll = true;
            this.@__RarityDistribution.ColumnCount = 2;
            this.@__RarityDistribution.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.@__RarityDistribution.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.@__RarityDistribution.Location = new System.Drawing.Point(145, 45);
            this.@__RarityDistribution.Name = "__RarityDistribution";
            this.@__RarityDistribution.RowCount = 1;
            this.@__RarityDistribution.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 61F));
            this.@__RarityDistribution.Size = new System.Drawing.Size(190, 61);
            this.@__RarityDistribution.TabIndex = 18;
            // 
            // __SkillInfoDisplay
            // 
            this.@__SkillInfoDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.@__SkillInfoDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.@__SkillInfoDisplay.Location = new System.Drawing.Point(360, 12);
            this.@__SkillInfoDisplay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.@__SkillInfoDisplay.Name = "__SkillInfoDisplay";
            this.@__SkillInfoDisplay.Size = new System.Drawing.Size(658, 112);
            this.@__SkillInfoDisplay.TabIndex = 17;
            // 
            // __SkillSetDisplay
            // 
            this.@__SkillSetDisplay.BackColor = System.Drawing.SystemColors.ControlDark;
            this.@__SkillSetDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.@__SkillSetDisplay.Location = new System.Drawing.Point(12, 248);
            this.@__SkillSetDisplay.Name = "__SkillSetDisplay";
            this.@__SkillSetDisplay.Size = new System.Drawing.Size(1263, 496);
            this.@__SkillSetDisplay.TabIndex = 1;
            // 
            // SkillSetEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1287, 756);
            this.Controls.Add(this.@__SkillInfoDisplay);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.@__SkillSetDisplay);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SkillSetEditor";
            this.Text = "Edit Skill Set";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button __Select_NonElites;
        private System.Windows.Forms.Button __Select_Rating;
        private System.Windows.Forms.Button __Select_Profession;
        private System.Windows.Forms.Button __Select_Attribute;
        private System.Windows.Forms.Button __Select_All;
        private System.Windows.Forms.Button __Select_EyeOfTheNorth;
        private System.Windows.Forms.Button __Select_Nightfall;
        private System.Windows.Forms.Button __Select_Factions;
        private System.Windows.Forms.Button __Select_Prophecies;
        private System.Windows.Forms.Button __Select_Core;
        private SkillBoosterSetUI __SkillSetDisplay;
        private System.Windows.Forms.ComboBox __Select_AttributeList;
        private System.Windows.Forms.ComboBox __Select_ProfessionList;
        private System.Windows.Forms.Button __Select_Elites;
        private System.Windows.Forms.ComboBox __Select_RatingsList;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox __Deselect_RatingList;
        private System.Windows.Forms.Button __Deselect_Elites;
        private System.Windows.Forms.ComboBox __Deselect_ProfessionList;
        private System.Windows.Forms.ComboBox __Deselect_AttributeList;
        private System.Windows.Forms.Button __Deselect_NonElites;
        private System.Windows.Forms.Button __Deselect_Rating;
        private System.Windows.Forms.Button __Deselect_Profession;
        private System.Windows.Forms.Button __Deselect_Attribute;
        private System.Windows.Forms.Button __Deselect_All;
        private System.Windows.Forms.Button __Deselect_EotN;
        private System.Windows.Forms.Button __Deselect_Nightfall;
        private System.Windows.Forms.Button __Deselect_Factions;
        private System.Windows.Forms.Button __Deselect_Prophecies;
        private System.Windows.Forms.Button __Deselect_Core;
        private System.Windows.Forms.TextBox __SetName;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox __SetStats;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button __Apply_And_Close;
        private Skill_UI.SkillInfoDisplay __SkillInfoDisplay;
        private System.Windows.Forms.TableLayoutPanel __RarityDistribution;
    }
}