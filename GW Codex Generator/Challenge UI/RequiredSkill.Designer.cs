namespace GW_Codex_Generator.Challenge_UI
{
    partial class RequiredSkill
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
            this.@__OnlyElites = new System.Windows.Forms.RadioButton();
            this.@__ElitesAllowed = new System.Windows.Forms.RadioButton();
            this.@__SkillIcon = new System.Windows.Forms.PictureBox();
            this.@__RollSkill = new System.Windows.Forms.Button();
            this.@__NoElites = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.@__SkillIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.@__OnlyElites);
            this.groupBox1.Controls.Add(this.@__ElitesAllowed);
            this.groupBox1.Controls.Add(this.@__SkillIcon);
            this.groupBox1.Controls.Add(this.@__RollSkill);
            this.groupBox1.Controls.Add(this.@__NoElites);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(321, 87);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Required Skill (for All Characters)";
            // 
            // __OnlyElites
            // 
            this.@__OnlyElites.AutoSize = true;
            this.@__OnlyElites.Location = new System.Drawing.Point(6, 42);
            this.@__OnlyElites.Name = "__OnlyElites";
            this.@__OnlyElites.Size = new System.Drawing.Size(131, 17);
            this.@__OnlyElites.TabIndex = 4;
            this.@__OnlyElites.Text = "Only Elites in Skill Pool";
            this.@__OnlyElites.UseVisualStyleBackColor = true;
            // 
            // __ElitesAllowed
            // 
            this.@__ElitesAllowed.AutoSize = true;
            this.@__ElitesAllowed.Location = new System.Drawing.Point(136, 19);
            this.@__ElitesAllowed.Name = "__ElitesAllowed";
            this.@__ElitesAllowed.Size = new System.Drawing.Size(107, 17);
            this.@__ElitesAllowed.TabIndex = 3;
            this.@__ElitesAllowed.Text = "Elites in Skill Pool";
            this.@__ElitesAllowed.UseVisualStyleBackColor = true;
            // 
            // __SkillIcon
            // 
            this.@__SkillIcon.BackColor = System.Drawing.SystemColors.ControlDark;
            this.@__SkillIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.@__SkillIcon.Location = new System.Drawing.Point(248, 12);
            this.@__SkillIcon.Name = "__SkillIcon";
            this.@__SkillIcon.Size = new System.Drawing.Size(66, 66);
            this.@__SkillIcon.TabIndex = 2;
            this.@__SkillIcon.TabStop = false;
            // 
            // __RollSkill
            // 
            this.@__RollSkill.Location = new System.Drawing.Point(154, 39);
            this.@__RollSkill.Name = "__RollSkill";
            this.@__RollSkill.Size = new System.Drawing.Size(75, 23);
            this.@__RollSkill.TabIndex = 1;
            this.@__RollSkill.Text = "Roll Skill";
            this.@__RollSkill.UseVisualStyleBackColor = true;
            this.@__RollSkill.Click += new System.EventHandler(this.@__RollSkill_Click);
            // 
            // __NoElites
            // 
            this.@__NoElites.AutoSize = true;
            this.@__NoElites.Checked = true;
            this.@__NoElites.Location = new System.Drawing.Point(6, 19);
            this.@__NoElites.Name = "__NoElites";
            this.@__NoElites.Size = new System.Drawing.Size(124, 17);
            this.@__NoElites.TabIndex = 0;
            this.@__NoElites.TabStop = true;
            this.@__NoElites.Text = "No Elites in Skill Pool";
            this.@__NoElites.UseVisualStyleBackColor = true;
            // 
            // RequiredSkill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "RequiredSkill";
            this.Size = new System.Drawing.Size(321, 87);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.@__SkillIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton __OnlyElites;
        private System.Windows.Forms.RadioButton __ElitesAllowed;
        private System.Windows.Forms.PictureBox __SkillIcon;
        private System.Windows.Forms.Button __RollSkill;
        private System.Windows.Forms.RadioButton __NoElites;
    }
}
