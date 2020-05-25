namespace GW_Codex_Generator
{
    partial class TemplateCodeDebugger
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
            this.@__TemplateInformation = new System.Windows.Forms.TextBox();
            this.@__TemplateCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.@__TemplateInformation);
            this.groupBox1.Controls.Add(this.@__TemplateCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(454, 345);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Template Code Debugger";
            // 
            // __TemplateInformation
            // 
            this.@__TemplateInformation.Location = new System.Drawing.Point(6, 45);
            this.@__TemplateInformation.Multiline = true;
            this.@__TemplateInformation.Name = "__TemplateInformation";
            this.@__TemplateInformation.ReadOnly = true;
            this.@__TemplateInformation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.@__TemplateInformation.Size = new System.Drawing.Size(437, 294);
            this.@__TemplateInformation.TabIndex = 2;
            // 
            // __TemplateCode
            // 
            this.@__TemplateCode.Location = new System.Drawing.Point(91, 19);
            this.@__TemplateCode.Name = "__TemplateCode";
            this.@__TemplateCode.Size = new System.Drawing.Size(352, 20);
            this.@__TemplateCode.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Template Code";
            // 
            // TemplateCodeDebugger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "TemplateCodeDebugger";
            this.Size = new System.Drawing.Size(454, 345);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox __TemplateInformation;
        private System.Windows.Forms.TextBox __TemplateCode;
        private System.Windows.Forms.Label label1;
    }
}
