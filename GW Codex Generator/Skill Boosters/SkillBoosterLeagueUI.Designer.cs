namespace GW_Codex_Generator.Skill_Boosters
{
    partial class SkillBoosterLeagueUI
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
            this.@__SaveBoosterLeagueDialog = new System.Windows.Forms.SaveFileDialog();
            this.@__OpenBoosterLeagueDialog = new System.Windows.Forms.OpenFileDialog();
            this.@__SelectedSet = new System.Windows.Forms.ComboBox();
            this.@__Button_RefreshSetList = new System.Windows.Forms.Button();
            this.@__Button_Save = new System.Windows.Forms.Button();
            this.@__Button_Open = new System.Windows.Forms.Button();
            this.@__Button_AddBoosters = new System.Windows.Forms.Button();
            this.@__NumberOfBoostersToAdd = new System.Windows.Forms.NumericUpDown();
            this.@__LeaguePoolDisplay = new GW_Codex_Generator.Skill_Boosters.SkillBoosterPoolUI();
            this.@__SortButton_Standard = new System.Windows.Forms.Button();
            this.@__SortButton_Rarity = new System.Windows.Forms.Button();
            this.@__SortButton_Rating = new System.Windows.Forms.Button();
            this.@__Button_NewLeague = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.@__NumberOfBoostersToAdd)).BeginInit();
            this.SuspendLayout();
            // 
            // __SaveBoosterLeagueDialog
            // 
            this.@__SaveBoosterLeagueDialog.DefaultExt = "gwsbl";
            this.@__SaveBoosterLeagueDialog.Filter = "GW Codex Booster League Files|*.gwsbl";
            this.@__SaveBoosterLeagueDialog.Title = "Save Booster League";
            this.@__SaveBoosterLeagueDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.@__SaveBoosterLeagueDialog_FileOk);
            // 
            // __OpenBoosterLeagueDialog
            // 
            this.@__OpenBoosterLeagueDialog.DefaultExt = "gwsbl";
            this.@__OpenBoosterLeagueDialog.Filter = "GW Codex Booster League Files|*.gwsbl";
            this.@__OpenBoosterLeagueDialog.Title = "Load Booster League";
            this.@__OpenBoosterLeagueDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.@__OpenBoosterLeagueDialog_FileOk);
            // 
            // __SelectedSet
            // 
            this.@__SelectedSet.FormattingEnabled = true;
            this.@__SelectedSet.Location = new System.Drawing.Point(3, 3);
            this.@__SelectedSet.Name = "__SelectedSet";
            this.@__SelectedSet.Size = new System.Drawing.Size(253, 21);
            this.@__SelectedSet.TabIndex = 0;
            this.@__SelectedSet.Text = "Select Set";
            // 
            // __Button_RefreshSetList
            // 
            this.@__Button_RefreshSetList.Location = new System.Drawing.Point(262, 3);
            this.@__Button_RefreshSetList.Name = "__Button_RefreshSetList";
            this.@__Button_RefreshSetList.Size = new System.Drawing.Size(101, 23);
            this.@__Button_RefreshSetList.TabIndex = 1;
            this.@__Button_RefreshSetList.Text = "Refresh Set List";
            this.@__Button_RefreshSetList.UseVisualStyleBackColor = true;
            this.@__Button_RefreshSetList.Click += new System.EventHandler(this.@__Button_RefreshSetList_Click);
            // 
            // __Button_Save
            // 
            this.@__Button_Save.Location = new System.Drawing.Point(369, 3);
            this.@__Button_Save.Name = "__Button_Save";
            this.@__Button_Save.Size = new System.Drawing.Size(101, 23);
            this.@__Button_Save.TabIndex = 2;
            this.@__Button_Save.Text = "Save League...";
            this.@__Button_Save.UseVisualStyleBackColor = true;
            this.@__Button_Save.Click += new System.EventHandler(this.@__Button_Save_Click);
            // 
            // __Button_Open
            // 
            this.@__Button_Open.Location = new System.Drawing.Point(476, 3);
            this.@__Button_Open.Name = "__Button_Open";
            this.@__Button_Open.Size = new System.Drawing.Size(101, 23);
            this.@__Button_Open.TabIndex = 3;
            this.@__Button_Open.Text = "Open League...";
            this.@__Button_Open.UseVisualStyleBackColor = true;
            this.@__Button_Open.Click += new System.EventHandler(this.@__Button_Open_Click);
            // 
            // __Button_AddBoosters
            // 
            this.@__Button_AddBoosters.Location = new System.Drawing.Point(75, 30);
            this.@__Button_AddBoosters.Name = "__Button_AddBoosters";
            this.@__Button_AddBoosters.Size = new System.Drawing.Size(181, 23);
            this.@__Button_AddBoosters.TabIndex = 4;
            this.@__Button_AddBoosters.Text = "Add 1 Booster";
            this.@__Button_AddBoosters.UseVisualStyleBackColor = true;
            this.@__Button_AddBoosters.Click += new System.EventHandler(this.@__Button_AddBoosters_Click);
            // 
            // __NumberOfBoostersToAdd
            // 
            this.@__NumberOfBoostersToAdd.Location = new System.Drawing.Point(3, 33);
            this.@__NumberOfBoostersToAdd.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.@__NumberOfBoostersToAdd.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.@__NumberOfBoostersToAdd.Name = "__NumberOfBoostersToAdd";
            this.@__NumberOfBoostersToAdd.Size = new System.Drawing.Size(66, 20);
            this.@__NumberOfBoostersToAdd.TabIndex = 5;
            this.@__NumberOfBoostersToAdd.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.@__NumberOfBoostersToAdd.ValueChanged += new System.EventHandler(this.@__NumberOfBoostersToAdd_ValueChanged);
            // 
            // __LeaguePoolDisplay
            // 
            this.@__LeaguePoolDisplay.BackColor = System.Drawing.SystemColors.ControlDark;
            this.@__LeaguePoolDisplay.Location = new System.Drawing.Point(3, 59);
            this.@__LeaguePoolDisplay.Name = "__LeaguePoolDisplay";
            this.@__LeaguePoolDisplay.Size = new System.Drawing.Size(588, 483);
            this.@__LeaguePoolDisplay.TabIndex = 6;
            // 
            // __SortButton_Standard
            // 
            this.@__SortButton_Standard.Location = new System.Drawing.Point(262, 30);
            this.@__SortButton_Standard.Name = "__SortButton_Standard";
            this.@__SortButton_Standard.Size = new System.Drawing.Size(101, 23);
            this.@__SortButton_Standard.TabIndex = 7;
            this.@__SortButton_Standard.Text = "Standard Sort";
            this.@__SortButton_Standard.UseVisualStyleBackColor = true;
            this.@__SortButton_Standard.Click += new System.EventHandler(this.@__SortButton_Standard_Click);
            // 
            // __SortButton_Rarity
            // 
            this.@__SortButton_Rarity.Location = new System.Drawing.Point(369, 30);
            this.@__SortButton_Rarity.Name = "__SortButton_Rarity";
            this.@__SortButton_Rarity.Size = new System.Drawing.Size(101, 23);
            this.@__SortButton_Rarity.TabIndex = 8;
            this.@__SortButton_Rarity.Text = "Sort by Rarity";
            this.@__SortButton_Rarity.UseVisualStyleBackColor = true;
            this.@__SortButton_Rarity.Click += new System.EventHandler(this.@__SortButton_Rarity_Click);
            // 
            // __SortButton_Rating
            // 
            this.@__SortButton_Rating.Location = new System.Drawing.Point(476, 30);
            this.@__SortButton_Rating.Name = "__SortButton_Rating";
            this.@__SortButton_Rating.Size = new System.Drawing.Size(101, 23);
            this.@__SortButton_Rating.TabIndex = 9;
            this.@__SortButton_Rating.Text = "Sort by Rating";
            this.@__SortButton_Rating.UseVisualStyleBackColor = true;
            this.@__SortButton_Rating.Click += new System.EventHandler(this.@__SortButton_Rating_Click);
            // 
            // __Button_NewLeague
            // 
            this.@__Button_NewLeague.Location = new System.Drawing.Point(583, 3);
            this.@__Button_NewLeague.Name = "__Button_NewLeague";
            this.@__Button_NewLeague.Size = new System.Drawing.Size(101, 23);
            this.@__Button_NewLeague.TabIndex = 10;
            this.@__Button_NewLeague.Text = "New League";
            this.@__Button_NewLeague.UseVisualStyleBackColor = true;
            this.@__Button_NewLeague.Click += new System.EventHandler(this.@__Button_NewLeague_Click);
            // 
            // SkillBoosterLeagueUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.@__Button_NewLeague);
            this.Controls.Add(this.@__SortButton_Rating);
            this.Controls.Add(this.@__SortButton_Rarity);
            this.Controls.Add(this.@__SortButton_Standard);
            this.Controls.Add(this.@__LeaguePoolDisplay);
            this.Controls.Add(this.@__NumberOfBoostersToAdd);
            this.Controls.Add(this.@__Button_AddBoosters);
            this.Controls.Add(this.@__Button_Open);
            this.Controls.Add(this.@__Button_Save);
            this.Controls.Add(this.@__Button_RefreshSetList);
            this.Controls.Add(this.@__SelectedSet);
            this.Name = "SkillBoosterLeagueUI";
            this.Size = new System.Drawing.Size(902, 545);
            ((System.ComponentModel.ISupportInitialize)(this.@__NumberOfBoostersToAdd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog __SaveBoosterLeagueDialog;
        private System.Windows.Forms.OpenFileDialog __OpenBoosterLeagueDialog;
        private System.Windows.Forms.ComboBox __SelectedSet;
        private System.Windows.Forms.Button __Button_RefreshSetList;
        private System.Windows.Forms.Button __Button_Save;
        private System.Windows.Forms.Button __Button_Open;
        private System.Windows.Forms.Button __Button_AddBoosters;
        private System.Windows.Forms.NumericUpDown __NumberOfBoostersToAdd;
        private SkillBoosterPoolUI __LeaguePoolDisplay;
        private System.Windows.Forms.Button __SortButton_Standard;
        private System.Windows.Forms.Button __SortButton_Rarity;
        private System.Windows.Forms.Button __SortButton_Rating;
        private System.Windows.Forms.Button __Button_NewLeague;
    }
}
