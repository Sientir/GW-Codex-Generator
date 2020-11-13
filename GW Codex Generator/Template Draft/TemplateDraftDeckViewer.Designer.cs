namespace GW_Codex_Generator.Template_Draft
{
    partial class TemplateDraftDeckViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplateDraftDeckViewer));
            this.@__Contents = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.@__SkillInfo = new GW_Codex_Generator.Skill_UI.SkillInfoDisplay();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // __Contents
            // 
            this.@__Contents.AutoScroll = true;
            this.@__Contents.BackColor = System.Drawing.SystemColors.ControlDark;
            this.@__Contents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.@__Contents.Location = new System.Drawing.Point(0, 0);
            this.@__Contents.Name = "__Contents";
            this.@__Contents.Size = new System.Drawing.Size(800, 486);
            this.@__Contents.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.@__SkillInfo);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.@__Contents);
            this.splitContainer1.Size = new System.Drawing.Size(800, 641);
            this.splitContainer1.SplitterDistance = 151;
            this.splitContainer1.TabIndex = 1;
            // 
            // __SkillInfo
            // 
            this.@__SkillInfo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.@__SkillInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.@__SkillInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.@__SkillInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.@__SkillInfo.Location = new System.Drawing.Point(0, 0);
            this.@__SkillInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.@__SkillInfo.Name = "__SkillInfo";
            this.@__SkillInfo.Size = new System.Drawing.Size(800, 151);
            this.@__SkillInfo.TabIndex = 0;
            // 
            // TemplateDraftDeckViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 641);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TemplateDraftDeckViewer";
            this.Text = "Template Deck Viewer";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel __Contents;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Skill_UI.SkillInfoDisplay __SkillInfo;
    }
}