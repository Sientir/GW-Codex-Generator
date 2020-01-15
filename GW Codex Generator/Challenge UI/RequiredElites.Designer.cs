namespace GW_Codex_Generator.Challenge_UI
{
    partial class RequiredElites
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
            this.label3 = new System.Windows.Forms.Label();
            this.@__RangeMax = new System.Windows.Forms.NumericUpDown();
            this.@__RatingLow = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.@__IncludeInHTMLSummary = new System.Windows.Forms.CheckBox();
            this.@__RollButton = new System.Windows.Forms.Button();
            this.@__PartySize = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.@__SkillDisplay = new GW_Codex_Generator.Skill_UI.SkillDisplay();
            this.@__NoSkillsFromPrimaries = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.@__RangeMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.@__RatingLow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.@__PartySize)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.@__NoSkillsFromPrimaries);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.@__RangeMax);
            this.groupBox1.Controls.Add(this.@__RatingLow);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.@__IncludeInHTMLSummary);
            this.groupBox1.Controls.Add(this.@__RollButton);
            this.groupBox1.Controls.Add(this.@__PartySize);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.@__SkillDisplay);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(609, 160);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Required Elites (Full Party)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(253, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "to";
            // 
            // __RangeMax
            // 
            this.@__RangeMax.Location = new System.Drawing.Point(275, 19);
            this.@__RangeMax.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.@__RangeMax.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.@__RangeMax.Name = "__RangeMax";
            this.@__RangeMax.Size = new System.Drawing.Size(40, 20);
            this.@__RangeMax.TabIndex = 7;
            this.@__RangeMax.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // __RatingLow
            // 
            this.@__RatingLow.Location = new System.Drawing.Point(207, 19);
            this.@__RatingLow.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.@__RatingLow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.@__RatingLow.Name = "__RatingLow";
            this.@__RatingLow.Size = new System.Drawing.Size(40, 20);
            this.@__RatingLow.TabIndex = 6;
            this.@__RatingLow.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(128, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Rating Range";
            // 
            // __IncludeInHTMLSummary
            // 
            this.@__IncludeInHTMLSummary.AutoSize = true;
            this.@__IncludeInHTMLSummary.Location = new System.Drawing.Point(428, 12);
            this.@__IncludeInHTMLSummary.Name = "__IncludeInHTMLSummary";
            this.@__IncludeInHTMLSummary.Size = new System.Drawing.Size(152, 17);
            this.@__IncludeInHTMLSummary.TabIndex = 4;
            this.@__IncludeInHTMLSummary.Text = "Include in HTML summary.";
            this.@__IncludeInHTMLSummary.UseVisualStyleBackColor = true;
            // 
            // __RollButton
            // 
            this.@__RollButton.Location = new System.Drawing.Point(321, 16);
            this.@__RollButton.Name = "__RollButton";
            this.@__RollButton.Size = new System.Drawing.Size(75, 23);
            this.@__RollButton.TabIndex = 3;
            this.@__RollButton.Text = "Roll";
            this.@__RollButton.UseVisualStyleBackColor = true;
            this.@__RollButton.Click += new System.EventHandler(this.@__RollButton_Click);
            // 
            // __PartySize
            // 
            this.@__PartySize.Location = new System.Drawing.Point(64, 19);
            this.@__PartySize.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.@__PartySize.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.@__PartySize.Name = "__PartySize";
            this.@__PartySize.Size = new System.Drawing.Size(58, 20);
            this.@__PartySize.TabIndex = 2;
            this.@__PartySize.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Party size";
            // 
            // __SkillDisplay
            // 
            this.@__SkillDisplay.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.@__SkillDisplay.Location = new System.Drawing.Point(3, 54);
            this.@__SkillDisplay.Name = "__SkillDisplay";
            this.@__SkillDisplay.Size = new System.Drawing.Size(603, 103);
            this.@__SkillDisplay.TabIndex = 0;
            // 
            // __NoSkillsFromPrimaries
            // 
            this.@__NoSkillsFromPrimaries.AutoSize = true;
            this.@__NoSkillsFromPrimaries.Location = new System.Drawing.Point(428, 34);
            this.@__NoSkillsFromPrimaries.Name = "__NoSkillsFromPrimaries";
            this.@__NoSkillsFromPrimaries.Size = new System.Drawing.Size(175, 17);
            this.@__NoSkillsFromPrimaries.TabIndex = 9;
            this.@__NoSkillsFromPrimaries.Text = "No skills from Primary Attributes.";
            this.@__NoSkillsFromPrimaries.UseVisualStyleBackColor = true;
            // 
            // RequiredElites
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "RequiredElites";
            this.Size = new System.Drawing.Size(609, 160);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.@__RangeMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.@__RatingLow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.@__PartySize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Skill_UI.SkillDisplay __SkillDisplay;
        private System.Windows.Forms.CheckBox __IncludeInHTMLSummary;
        private System.Windows.Forms.Button __RollButton;
        private System.Windows.Forms.NumericUpDown __PartySize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown __RangeMax;
        private System.Windows.Forms.NumericUpDown __RatingLow;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox __NoSkillsFromPrimaries;
    }
}
