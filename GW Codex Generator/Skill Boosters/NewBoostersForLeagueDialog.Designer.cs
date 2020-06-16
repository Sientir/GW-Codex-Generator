namespace GW_Codex_Generator.Skill_Boosters
{
    partial class NewBoostersForLeagueDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewBoostersForLeagueDialog));
            this.@__Pool = new GW_Codex_Generator.Skill_Boosters.SkillBoosterPoolUI();
            this.@__SkillInfoDisplay = new GW_Codex_Generator.Skill_UI.SkillInfoDisplay();
            this.@__OK_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // __Pool
            // 
            this.@__Pool.BackColor = System.Drawing.SystemColors.ControlDark;
            this.@__Pool.Location = new System.Drawing.Point(0, 122);
            this.@__Pool.Name = "__Pool";
            this.@__Pool.Size = new System.Drawing.Size(800, 293);
            this.@__Pool.TabIndex = 0;
            // 
            // __SkillInfoDisplay
            // 
            this.@__SkillInfoDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.@__SkillInfoDisplay.Location = new System.Drawing.Point(0, 0);
            this.@__SkillInfoDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.@__SkillInfoDisplay.Name = "__SkillInfoDisplay";
            this.@__SkillInfoDisplay.Size = new System.Drawing.Size(800, 119);
            this.@__SkillInfoDisplay.TabIndex = 1;
            // 
            // __OK_Button
            // 
            this.@__OK_Button.Location = new System.Drawing.Point(356, 421);
            this.@__OK_Button.Name = "__OK_Button";
            this.@__OK_Button.Size = new System.Drawing.Size(75, 23);
            this.@__OK_Button.TabIndex = 2;
            this.@__OK_Button.Text = "&OK";
            this.@__OK_Button.UseVisualStyleBackColor = true;
            this.@__OK_Button.Click += new System.EventHandler(this.@__OK_Button_Click);
            // 
            // NewBoostersForLeagueDialog
            // 
            this.AcceptButton = this.@__OK_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.@__OK_Button);
            this.Controls.Add(this.@__SkillInfoDisplay);
            this.Controls.Add(this.@__Pool);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewBoostersForLeagueDialog";
            this.Text = "Skills Added!";
            this.ResumeLayout(false);

        }

        #endregion

        private SkillBoosterPoolUI __Pool;
        private Skill_UI.SkillInfoDisplay __SkillInfoDisplay;
        private System.Windows.Forms.Button __OK_Button;
    }
}