namespace GW_Codex_Generator
{
    partial class SkillValueSetter
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
            this.@__SkillInformation = new GW_Codex_Generator.Skill_UI.SkillInfoDisplay();
            this.@__SkillIcon = new System.Windows.Forms.PictureBox();
            this.@__EvaluationGroupbox = new System.Windows.Forms.GroupBox();
            this.@__EvaluationButtonsHolder = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.@__SkillIcon)).BeginInit();
            this.@__EvaluationGroupbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // __SkillInformation
            // 
            this.@__SkillInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.@__SkillInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.@__SkillInformation.Location = new System.Drawing.Point(85, 14);
            this.@__SkillInformation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.@__SkillInformation.Name = "__SkillInformation";
            this.@__SkillInformation.Size = new System.Drawing.Size(684, 134);
            this.@__SkillInformation.TabIndex = 0;
            // 
            // __SkillIcon
            // 
            this.@__SkillIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.@__SkillIcon.Location = new System.Drawing.Point(12, 14);
            this.@__SkillIcon.Name = "__SkillIcon";
            this.@__SkillIcon.Size = new System.Drawing.Size(66, 66);
            this.@__SkillIcon.TabIndex = 1;
            this.@__SkillIcon.TabStop = false;
            // 
            // __EvaluationGroupbox
            // 
            this.@__EvaluationGroupbox.Controls.Add(this.@__EvaluationButtonsHolder);
            this.@__EvaluationGroupbox.Location = new System.Drawing.Point(12, 156);
            this.@__EvaluationGroupbox.Name = "__EvaluationGroupbox";
            this.@__EvaluationGroupbox.Size = new System.Drawing.Size(758, 249);
            this.@__EvaluationGroupbox.TabIndex = 2;
            this.@__EvaluationGroupbox.TabStop = false;
            this.@__EvaluationGroupbox.Text = "groupBox1";
            // 
            // __EvaluationButtonsHolder
            // 
            this.@__EvaluationButtonsHolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.@__EvaluationButtonsHolder.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.@__EvaluationButtonsHolder.Location = new System.Drawing.Point(3, 16);
            this.@__EvaluationButtonsHolder.Name = "__EvaluationButtonsHolder";
            this.@__EvaluationButtonsHolder.Size = new System.Drawing.Size(752, 230);
            this.@__EvaluationButtonsHolder.TabIndex = 0;
            // 
            // SkillValueSetter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 417);
            this.Controls.Add(this.@__EvaluationGroupbox);
            this.Controls.Add(this.@__SkillIcon);
            this.Controls.Add(this.@__SkillInformation);
            this.Name = "SkillValueSetter";
            ((System.ComponentModel.ISupportInitialize)(this.@__SkillIcon)).EndInit();
            this.@__EvaluationGroupbox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Skill_UI.SkillInfoDisplay __SkillInformation;
        private System.Windows.Forms.PictureBox __SkillIcon;
        private System.Windows.Forms.GroupBox __EvaluationGroupbox;
        private System.Windows.Forms.FlowLayoutPanel __EvaluationButtonsHolder;
    }
}
