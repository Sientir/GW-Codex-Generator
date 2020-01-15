namespace GW_Codex_Generator.Codex_Method_Settings
{
    partial class RegularFromAttributesElitesFromProfession
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.@__EliteSkillsPerProfession = new System.Windows.Forms.NumericUpDown();
            this.@__RegularSkillsPerAttribute = new System.Windows.Forms.NumericUpDown();
            this.@__PvEOnlySkills = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.@__EliteSkillsPerProfession)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.@__RegularSkillsPerAttribute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.@__PvEOnlySkills)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Regular skills per attribute";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Elite skills per profession";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "PvE-Only skills (if included in pool)";
            // 
            // __EliteSkillsPerProfession
            // 
            this.@__EliteSkillsPerProfession.Location = new System.Drawing.Point(176, 29);
            this.@__EliteSkillsPerProfession.Name = "__EliteSkillsPerProfession";
            this.@__EliteSkillsPerProfession.Size = new System.Drawing.Size(120, 20);
            this.@__EliteSkillsPerProfession.TabIndex = 3;
            this.@__EliteSkillsPerProfession.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // __RegularSkillsPerAttribute
            // 
            this.@__RegularSkillsPerAttribute.Location = new System.Drawing.Point(176, 3);
            this.@__RegularSkillsPerAttribute.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.@__RegularSkillsPerAttribute.Name = "__RegularSkillsPerAttribute";
            this.@__RegularSkillsPerAttribute.Size = new System.Drawing.Size(120, 20);
            this.@__RegularSkillsPerAttribute.TabIndex = 4;
            this.@__RegularSkillsPerAttribute.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // __PvEOnlySkills
            // 
            this.@__PvEOnlySkills.Location = new System.Drawing.Point(176, 55);
            this.@__PvEOnlySkills.Name = "__PvEOnlySkills";
            this.@__PvEOnlySkills.Size = new System.Drawing.Size(120, 20);
            this.@__PvEOnlySkills.TabIndex = 5;
            this.@__PvEOnlySkills.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // RegularFromAttributesElitesFromProfession
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.@__PvEOnlySkills);
            this.Controls.Add(this.@__RegularSkillsPerAttribute);
            this.Controls.Add(this.@__EliteSkillsPerProfession);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "RegularFromAttributesElitesFromProfession";
            this.Size = new System.Drawing.Size(302, 80);
            ((System.ComponentModel.ISupportInitialize)(this.@__EliteSkillsPerProfession)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.@__RegularSkillsPerAttribute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.@__PvEOnlySkills)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown __EliteSkillsPerProfession;
        private System.Windows.Forms.NumericUpDown __RegularSkillsPerAttribute;
        private System.Windows.Forms.NumericUpDown __PvEOnlySkills;
    }
}
