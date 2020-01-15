namespace GW_Codex_Generator.Skill_UI
{
    partial class SkillDisplay
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
            this.@__ScrollBar = new System.Windows.Forms.VScrollBar();
            this.SuspendLayout();
            // 
            // __ScrollBar
            // 
            this.@__ScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.@__ScrollBar.LargeChange = 1;
            this.@__ScrollBar.Location = new System.Drawing.Point(537, 0);
            this.@__ScrollBar.Maximum = 0;
            this.@__ScrollBar.Name = "__ScrollBar";
            this.@__ScrollBar.Size = new System.Drawing.Size(17, 455);
            this.@__ScrollBar.TabIndex = 0;
            // 
            // SkillDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.@__ScrollBar);
            this.DoubleBuffered = true;
            this.Name = "SkillDisplay";
            this.Size = new System.Drawing.Size(554, 455);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.VScrollBar __ScrollBar;
    }
}
