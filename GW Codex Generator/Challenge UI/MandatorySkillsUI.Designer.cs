namespace GW_Codex_Generator.Challenge_UI
{
    partial class MandatorySkillsUI
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
            this.@__IncludeElites = new System.Windows.Forms.CheckBox();
            this.@__RollSkillsButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.@__SkillsToPick = new System.Windows.Forms.NumericUpDown();
            this.@__SkillsDisplay = new GW_Codex_Generator.Skill_UI.SkillDisplay();
            this.@__UseAllSkills = new System.Windows.Forms.RadioButton();
            this.@__UseCodexPool = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.@__SkillsToPick)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.@__UseCodexPool);
            this.groupBox1.Controls.Add(this.@__UseAllSkills);
            this.groupBox1.Controls.Add(this.@__SkillsDisplay);
            this.groupBox1.Controls.Add(this.@__SkillsToPick);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.@__RollSkillsButton);
            this.groupBox1.Controls.Add(this.@__IncludeElites);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(652, 362);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mandatory Skills (to distribute amongst your party as you wish)";
            // 
            // __IncludeElites
            // 
            this.@__IncludeElites.AutoSize = true;
            this.@__IncludeElites.Location = new System.Drawing.Point(151, 20);
            this.@__IncludeElites.Name = "__IncludeElites";
            this.@__IncludeElites.Size = new System.Drawing.Size(89, 17);
            this.@__IncludeElites.TabIndex = 0;
            this.@__IncludeElites.Text = "Include Elites";
            this.@__IncludeElites.UseVisualStyleBackColor = true;
            // 
            // __RollSkillsButton
            // 
            this.@__RollSkillsButton.Location = new System.Drawing.Point(246, 16);
            this.@__RollSkillsButton.Name = "__RollSkillsButton";
            this.@__RollSkillsButton.Size = new System.Drawing.Size(75, 23);
            this.@__RollSkillsButton.TabIndex = 1;
            this.@__RollSkillsButton.Text = "Roll Skills";
            this.@__RollSkillsButton.UseVisualStyleBackColor = true;
            this.@__RollSkillsButton.Click += new System.EventHandler(this.@__RollSkillsButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Skills to Pick";
            // 
            // __SkillsToPick
            // 
            this.@__SkillsToPick.Location = new System.Drawing.Point(79, 19);
            this.@__SkillsToPick.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.@__SkillsToPick.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.@__SkillsToPick.Name = "__SkillsToPick";
            this.@__SkillsToPick.Size = new System.Drawing.Size(66, 20);
            this.@__SkillsToPick.TabIndex = 3;
            this.@__SkillsToPick.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // __SkillsDisplay
            // 
            this.@__SkillsDisplay.BackColor = System.Drawing.SystemColors.ControlDark;
            this.@__SkillsDisplay.Location = new System.Drawing.Point(6, 45);
            this.@__SkillsDisplay.Name = "__SkillsDisplay";
            this.@__SkillsDisplay.Size = new System.Drawing.Size(640, 311);
            this.@__SkillsDisplay.TabIndex = 4;
            // 
            // __UseAllSkills
            // 
            this.@__UseAllSkills.AutoSize = true;
            this.@__UseAllSkills.Location = new System.Drawing.Point(351, 19);
            this.@__UseAllSkills.Name = "__UseAllSkills";
            this.@__UseAllSkills.Size = new System.Drawing.Size(150, 17);
            this.@__UseAllSkills.TabIndex = 5;
            this.@__UseAllSkills.TabStop = true;
            this.@__UseAllSkills.Text = "Use all non-PvE-Only skills";
            this.@__UseAllSkills.UseVisualStyleBackColor = true;
            // 
            // __UseCodexPool
            // 
            this.@__UseCodexPool.AutoSize = true;
            this.@__UseCodexPool.Location = new System.Drawing.Point(507, 19);
            this.@__UseCodexPool.Name = "__UseCodexPool";
            this.@__UseCodexPool.Size = new System.Drawing.Size(101, 17);
            this.@__UseCodexPool.TabIndex = 6;
            this.@__UseCodexPool.TabStop = true;
            this.@__UseCodexPool.Text = "Use Codex Pool";
            this.@__UseCodexPool.UseVisualStyleBackColor = true;
            // 
            // MandatorySkillsUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "MandatorySkillsUI";
            this.Size = new System.Drawing.Size(652, 362);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.@__SkillsToPick)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Skill_UI.SkillDisplay __SkillsDisplay;
        private System.Windows.Forms.NumericUpDown __SkillsToPick;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button __RollSkillsButton;
        private System.Windows.Forms.CheckBox __IncludeElites;
        private System.Windows.Forms.RadioButton __UseCodexPool;
        private System.Windows.Forms.RadioButton __UseAllSkills;
    }
}
