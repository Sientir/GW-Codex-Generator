namespace GW_Codex_Generator.Skill_Boosters
{
    partial class SkillBoosterSetUI
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
            this.@__SaveSetButton = new System.Windows.Forms.Button();
            this.@__Name = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // __SaveSetButton
            // 
            this.@__SaveSetButton.Location = new System.Drawing.Point(248, 3);
            this.@__SaveSetButton.Name = "__SaveSetButton";
            this.@__SaveSetButton.Size = new System.Drawing.Size(45, 23);
            this.@__SaveSetButton.TabIndex = 0;
            this.@__SaveSetButton.Text = "Sa&ve";
            this.@__SaveSetButton.UseVisualStyleBackColor = true;
            this.@__SaveSetButton.Click += new System.EventHandler(this.@__SaveSetButton_Click);
            // 
            // __Name
            // 
            this.@__Name.Location = new System.Drawing.Point(3, 5);
            this.@__Name.Name = "__Name";
            this.@__Name.Size = new System.Drawing.Size(239, 20);
            this.@__Name.TabIndex = 1;
            // 
            // SkillBoosterSetUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.@__Name);
            this.Controls.Add(this.@__SaveSetButton);
            this.Name = "SkillBoosterSetUI";
            this.Size = new System.Drawing.Size(296, 273);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button __SaveSetButton;
        private System.Windows.Forms.TextBox __Name;
    }
}
