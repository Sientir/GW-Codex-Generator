namespace GW_Codex_Generator.Challenge_UI
{
    partial class TemplateDisplay
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
            this.@__TemplateName = new System.Windows.Forms.Label();
            this.@__TemplateCode = new System.Windows.Forms.TextBox();
            this.@__CopyTemplateCode = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // __TemplateName
            // 
            this.@__TemplateName.Location = new System.Drawing.Point(3, 0);
            this.@__TemplateName.Name = "__TemplateName";
            this.@__TemplateName.Size = new System.Drawing.Size(160, 23);
            this.@__TemplateName.TabIndex = 0;
            this.@__TemplateName.Text = "label1";
            this.@__TemplateName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // __TemplateCode
            // 
            this.@__TemplateCode.Location = new System.Drawing.Point(169, 2);
            this.@__TemplateCode.Name = "__TemplateCode";
            this.@__TemplateCode.ReadOnly = true;
            this.@__TemplateCode.Size = new System.Drawing.Size(302, 20);
            this.@__TemplateCode.TabIndex = 1;
            // 
            // __CopyTemplateCode
            // 
            this.@__CopyTemplateCode.Location = new System.Drawing.Point(477, 0);
            this.@__CopyTemplateCode.Name = "__CopyTemplateCode";
            this.@__CopyTemplateCode.Size = new System.Drawing.Size(39, 23);
            this.@__CopyTemplateCode.TabIndex = 2;
            this.@__CopyTemplateCode.Text = "Copy";
            this.@__CopyTemplateCode.UseVisualStyleBackColor = true;
            this.@__CopyTemplateCode.Click += new System.EventHandler(this.@__CopyTemplateCode_Click);
            // 
            // TemplateDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.@__CopyTemplateCode);
            this.Controls.Add(this.@__TemplateCode);
            this.Controls.Add(this.@__TemplateName);
            this.Name = "TemplateDisplay";
            this.Size = new System.Drawing.Size(517, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label __TemplateName;
        private System.Windows.Forms.TextBox __TemplateCode;
        private System.Windows.Forms.Button __CopyTemplateCode;
    }
}
